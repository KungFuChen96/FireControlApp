using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FireControlApp
{
    public partial class MyGroupBox : GroupBox//Component
    {
        private Color mBorderColor = Color.Black;
        [Browsable(true), Description("边框颜色"), Category("自定义分组")]
        public Color BorderColor
        {
            get { return mBorderColor; }
            set { mBorderColor = value; }
        }

        public MyGroupBox()
        {
            InitializeComponent();
        }

        public MyGroupBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        // 重写
        protected override void OnPaint(PaintEventArgs e)
        {
            var vSize = e.Graphics.MeasureString(this.Text, this.Font);

            e.Graphics.Clear(this.BackColor);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), 10, 1);
            Pen vPen = new Pen(this.mBorderColor); // 用属性颜色来画边框颜色
            e.Graphics.DrawLine(vPen, 1, vSize.Height / 2, 8, vSize.Height / 2);
            e.Graphics.DrawLine(vPen, vSize.Width + 8, vSize.Height / 2, this.Width - 2, vSize.Height / 2);
            e.Graphics.DrawLine(vPen, 1, vSize.Height / 2, 1, this.Height - 2);
            e.Graphics.DrawLine(vPen, 1, this.Height - 2, this.Width - 2, this.Height - 2);
            e.Graphics.DrawLine(vPen, this.Width - 2, vSize.Height / 2, this.Width - 2, this.Height - 2);
        }
    }
}
