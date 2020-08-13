using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    /// <summary>
    /// 主界面母板
    /// </summary>
    public partial class MasterDefault : Form
    {
        public MasterDefault()
        {
            InitializeComponent();
        }

        private void MasterDefault_Load(object sender, EventArgs e)
        {
            //启动时最大化
            this.Location = new Point(0, 0);
            //软件宽为屏幕分辨率宽
            this.Width = BatMes.Client.Controls.Tools.ScreenWidth;
            //软件高为屏幕分辨率高减去任务栏高后的工作区域高
            this.Height = BatMes.Client.Controls.Tools.ScreenHeight;
            //BOTTOM位置
            this.bottom.Location = new Point(0, this.Height - BatMes.Client.Controls.Tools.BOTTOM_HEIGHT);
            //主容器适应分辨率
            this.panMain.Size = new Size(
                BatMes.Client.Controls.Tools.ScreenWidth - 20 * 2,
                BatMes.Client.Controls.Tools.ScreenHeight - BatMes.Client.Controls.Tools.TOP_HEIGHT - BatMes.Client.Controls.Tools.BOTTOM_HEIGHT - 20 * 2);
            //日志控件适应分辨率
            this.realLog.Width = this.panMain.Width;
            this.realLog.Location = new Point(0, this.panMain.Height - this.realLog.Height);
        }

        #region 实时日志

        private delegate void logAppendDelegate(string cont, Color color);

        /// <summary>
        /// 日志显示
        /// </summary>
        /// <param name="cont">日志内容</param>
        /// <param name="level">级别</param>
        public void LogShow(string cont, BatMes.Client.Enums.SysEventLevel level = BatMes.Client.Enums.SysEventLevel.Info)
        {
            Color color = Color.Black;
            switch (level)
            {
                case BatMes.Client.Enums.SysEventLevel.Warn:
                    color = Color.Orange;
                    break;
                case BatMes.Client.Enums.SysEventLevel.Error:
                    color = Color.Red;
                    break;
            }

            //判断是否为跨线程访问
            if (this.realLog.InvokeRequired)
            {
                while (!this.realLog.IsHandleCreated)
                {
                    //防止窗体关闭时出现“访问已释放句柄“的异常
                    if (this.realLog.Disposing || this.realLog.IsDisposed)
                        return;
                }
                logAppendDelegate lad = new logAppendDelegate(this.realLog.LogAppend);
                this.realLog.Invoke(lad, $"【{DateTime.Now.ToString("MM-dd HH:mm:ss")}】 {cont}", color);
            }
            else
            {
                this.realLog.LogAppend($"【{DateTime.Now.ToString("MM-dd HH:mm:ss")}】 {cont}", color);
                this.realLog.Update();
            }
        }

        #endregion

        #region 底部状态

        private delegate void bottomStatusDelegate(string cont);

        /// <summary>
        /// 设置底部状态文本内容
        /// </summary>
        /// <param name="cont"></param>
        public void BottomShow(string cont)
        {
            if (this.bottom.InvokeRequired)
            {
                while (!this.bottom.IsHandleCreated)
                {
                    if (this.bottom.Disposing || this.bottom.IsDisposed)
                        return;
                }
                bottomStatusDelegate bsd = new bottomStatusDelegate(this.bottom.StatusCont);
                this.bottom.Invoke(bsd, new object[] { cont });
            }
            else
            {
                this.bottom.StatusCont(cont);
            }
        }

        #endregion

    }
}
