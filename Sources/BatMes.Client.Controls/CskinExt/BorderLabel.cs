using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CskinExt
{
    public class BorderLabel : Label
    {
        public BorderLabel()
        {
            this.BorderStyle = BorderStyle.None;
            this.TextAlign = ContentAlignment.MiddleCenter;

            this.outBorderColor = Color.FromArgb(146, 146, 146);
            this.outBorderPen = new Pen(new SolidBrush(this.outBorderColor), 1);

            this.Invalidate();
        }

        private Color outBorderColor;
        private Pen outBorderPen;

        [Browsable(true)]
        [Category("Appearance")]
        [Description("外边框颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color OutBorderColor
        {
            get { return outBorderColor; }
            set
            {
                this.outBorderColor = value;
                this.outBorderPen.Color = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 画label外边框
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.DrawRectangle(outBorderPen, new Rectangle(this.DisplayRectangle.X, this.DisplayRectangle.Y, this.DisplayRectangle.Width - 1, this.DisplayRectangle.Height - 1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                outBorderPen?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}