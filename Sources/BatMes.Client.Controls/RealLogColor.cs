using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    /// <summary>
    /// 实时日志
    /// </summary>
    public partial class RealLogColor : UserControl
    {
        public RealLogColor()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            this.panLog.Width = this.Width;
            this.panLog.Height = this.Height;

            this.rtbLog.Width = this.Width;
            this.rtbLog.Height = this.Height;

            base.OnResize(e);
        }

        private void RealLog_Load(object sender, EventArgs e)
        {
            //
        }

        private void rtbLog_TextChanged(object sender, EventArgs e)
        {
            //滚动条居底
            //Set the current caret position at the end
            this.rtbLog.SelectionStart = this.rtbLog.Text.Length;
            //Now scroll it automatically
            this.rtbLog.ScrollToCaret();
        }

        /// <summary>
        /// 追加日志
        /// </summary>
        /// <param name="cont">日志内容</param>
        /// <param name="color">显示颜色</param>
        public void LogAppend(string cont, Color color)
        {
            //消除当前选中内容将光标移动到最后
            this.rtbLog.Select(this.rtbLog.Text.Length, 0);

            this.rtbLog.SelectionColor = color;

            //正序
            this.rtbLog.AppendText($"{cont}\r\n");

            //倒序（文字颜色丢失）
            //this.rtbLog.Text = this.rtbLog.Text.Insert(0, $"{cont}\r\n");
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        public void LogClear()
        {
            this.rtbLog.Text = string.Empty;
        }

    }//end class
}
