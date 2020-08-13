using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    /// <summary>
    /// 底部
    /// </summary>
    public partial class Bottom : UserControl
    {
        public Bottom()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置状态文本内容（左侧）
        /// </summary>
        /// <param name="cont">文本内容</param>
        public void StatusCont(string cont)
        {
            this.labStatus.Text = cont;
        }

        /// <summary>
        /// 设置固定文本内容（右侧）
        /// </summary>
        /// <param name="cont">文本内容</param>
        public void FixedCont(string cont)
        {
            this.labFixed.Text = cont;
        }

        private void Bottom_Load(object sender, EventArgs e)
        {
            int _width = Tools.ScreenWidth;
            if (_width < Tools.SCREEN_WIDTH_MIN)
                _width = Tools.SCREEN_WIDTH_MIN;
            //if (_width > Tools.SCREEN_WIDTH_MAX)
            //    _width = Tools.SCREEN_WIDTH_MAX;
            //设置控件宽
            this.Width = _width;
            //设置状态区域宽
            this.labStatus.Location = new Point(10, 5);
            this.labStatus.Width = (int)(_width / 2) - 10;
            //设置固定区域宽
            this.labFixed.Location = new Point((int)(_width / 2), 5);
            this.labFixed.Width = (int)(_width / 2) - 10;

            this.Height = Tools.BOTTOM_HEIGHT;
            this.labStatus.Text = string.Empty;
        }

    }//end class
}
