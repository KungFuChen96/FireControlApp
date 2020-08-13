using CCWin;
using CCWin.SkinControl;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CskinExt
{
    public class SkinButtonExt : SkinButton
    {
        [Category("0Mine")]
        [DefaultValue(null)]
        [Description("绑定一个对象到该控件")]
        public object Tag2 { get; set; }

        private SkinToolTip _skinToolTip;

        private string _tipMsg; private bool onece = true;

        [Category("0Mine")]
        [DefaultValue(null)]
        [Description("鼠标悬浮提示信息，为空不显示")]
        public string TipMsg
        {
            get { return _tipMsg; }
            set
            {
                if (string.IsNullOrEmpty(value.Trim()))
                {
                    this.MouseEnter -= MouseEnterEvent;
                    this.MouseLeave -= MouseLeaveEvent;
                    onece = true;
                    _tipMsg = string.Empty;
                }
                else
                {
                    if (onece)
                    {
                        this.MouseEnter += MouseEnterEvent;
                        this.MouseLeave += MouseLeaveEvent;
                        onece = false;
                    }
                    _tipMsg = value.Trim();
                }
            }
        }

        private bool _isUseBindCopy;

        [Category("0Mine")]
        [DefaultValue(false)]
        [Description("点击是否复制Text属性内容到剪切板")]
        public bool IsBindCopy
        {
            get { return _isUseBindCopy; }
            set
            {
                if (value) this.Click += BindCopy;
                else this.Click -= BindCopy;
                _isUseBindCopy = value;
            }
        }

        private void MouseEnterEvent(object sender, EventArgs e)
        {
            (this._skinToolTip = SkinToolTipFactory.Tip).SetToolTip(this, _tipMsg);
        }

        private void MouseLeaveEvent(object sender, EventArgs e)
        {
            this._skinToolTip?.Dispose();
        }

        public virtual void BindCopy(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.Text);
        }
    }
}