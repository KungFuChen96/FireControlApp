using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CskinExt
{
    public class SkinBtnPlus : SkinButtonExt
    {
        private static Action<SkinBtnPlus, string> UpdateText = null;

        public SkinBtnPlus()
        {
            if (UpdateText == null)
                UpdateText = (btn, text) => btn.Text = text;
        }

        private bool useStar = false;

        public bool UseStar
        {
            get { return useStar; }
            set
            {
                if (value == useStar) return;
                useStar = value;
                if (value && Code.Length > StarLength)
                {
                    this.Text = GetDisplay(Code);
                }
                else
                    this.Text = Code;
            }
        }

        private int starLength = 12;

        public int StarLength
        {
            get { return starLength; }
            set
            {
                if (value == starLength) return;
                starLength = value;
            }
        }

        private string _barcode;

        [Category("0Mine")]
        public string Code
        {
            get { return this._barcode; }
            set
            {
                bool isEmpty = string.IsNullOrEmpty(value);
                if (!isEmpty && value.Length > StarLength && UseStar)
                {
                    //this.Text(GetDisplay(value));
                    if (this.InvokeRequired) this.BeginInvoke(UpdateText, this, GetDisplay(value));
                    else this.Text = GetDisplay(value);
                    this.TipMsg = "点击复制" + Environment.NewLine + value;
                }
                else
                {
                    //this.Text(value);
                    if (this.InvokeRequired) this.BeginInvoke(UpdateText, this, value);
                    else this.Text = value;
                    this.TipMsg = isEmpty ? string.Empty : "点击复制";
                }
                _barcode = value;
            }
        }

        public string GetDisplay(string barcode)
        {
            return barcode.Substring(0, 5) + "***" + barcode.Remove(0, barcode.Length - 4);
        }

        public override void BindCopy(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this._barcode);
        }
    }
}