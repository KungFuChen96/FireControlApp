using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BatMes.Client.Controls
{
    public partial class Top : UserControl
    {
        private Form formDefault;
        private Control panMain;
        private Control panData;
        private Control panLog;
        private Control panEvent;
        private Control panPar;
        private Control panOps;
        private Control panCode;
        private Control panService;

        public Top()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string TopTitle
        {
            get
            {
                return this.topTitle;
            }
            set
            {
                this.topTitle = value;
                this.labTitle.Text = this.topTitle;
            }
        }
        private string topTitle = string.Empty;

        private void Top_Load(object sender, EventArgs e)
        {
            int _width = Tools.ScreenWidth;
            if (_width < Tools.SCREEN_WIDTH_MIN)
                _width = Tools.SCREEN_WIDTH_MIN;
            //if (_width > Tools.SCREEN_WIDTH_MAX)
            //    _width = Tools.SCREEN_WIDTH_MAX;
            //设置控件宽高
            this.Width = _width;
            this.Height = Tools.TOP_HEIGHT;
            //设置主容器宽高
            this.panTop.Width = _width;
            this.panTop.Height = Tools.TOP_HEIGHT;
            //最小化按钮
            this.labTopMinimize.Location = new Point(_width - 86, 13);
            this.labTopMinimize.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 14);
            this.labTopMinimize.Text = "\uf068";
            //关闭按钮
            this.labTopClose.Location = new Point(_width - 46, 10);
            this.labTopClose.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labTopClose.Text = "\uf00d";
            
            this.formDefault = (Form)this.Parent;
            if(this.formDefault != null)
            {
                this.panMain = this.formDefault.Controls.Find("panMain", false).FirstOrDefault();

                this.panData = new Panel { Name = "panData", Visible = false, Width = Tools.ScreenWidth - 20 * 2, Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2 };
                this.formDefault.Controls.Add(this.panData);

                this.panLog = new Panel { Name = "panLog", Visible = false, Width = Tools.ScreenWidth - 20 * 2, Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2 };
                this.formDefault.Controls.Add(this.panLog);

                this.panEvent = new Panel { Name = "panEvent", Visible = false, Width = Tools.ScreenWidth - 20 * 2, Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2 };
                this.formDefault.Controls.Add(this.panEvent);

                this.panPar = new Panel { Name = "panPar", Visible = false, Width = Tools.ScreenWidth - 20 * 2, Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2 };
                this.formDefault.Controls.Add(this.panPar);

                this.panOps = new Panel { Name = "panOps", Visible = false, Width = Tools.ScreenWidth - 20 * 2, Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2 };
                this.formDefault.Controls.Add(this.panOps);

                this.panCode = new Panel { Name = "panCode", Visible = false, Width = Tools.ScreenWidth - 20 * 2, Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2 };
                this.formDefault.Controls.Add(this.panCode);

                this.panService = new Panel { Name = "panService", Visible = false, Width = Tools.ScreenWidth - 20 * 2, Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2 };
                this.formDefault.Controls.Add(this.panService);
            }

            if (this.IsData)
            {
                this.labTopData.Text = this.DataText;
                this.labTopData.Visible = true;
                this.panData.Location = new Point(20, 120);
            }
            if (this.IsLog)
            {
                this.labTopLog.Text = this.LogText;
                this.labTopLog.Visible = true;
                this.panLog.Location = new Point(20, 120);
            }
            if(this.IsEvent)
            {
                this.labTopEvent.Text = this.EventText;
                this.labTopEvent.Visible = true;
                this.panEvent.Location = new Point(20, 120);
            }
            if (this.IsPar)
            {
                this.labTopPar.Text = this.ParText;
                this.labTopPar.Visible = true;
                this.panPar.Location = new Point(20, 120);
            }
            if (this.IsOps)
            {
                this.labTopOps.Text = this.OpsText;
                this.labTopOps.Visible = true;
                this.panOps.Location = new Point(20, 120);
            }
            if (this.IsCode)
            {
                this.labTopCode.Text = this.CodeText;
                this.labTopCode.Visible = true;
                this.panCode.Location = new Point(20, 120);
            }
            if (this.IsService)
            {
                this.labTopService.Text = this.ServiceText;
                this.labTopService.Visible = true;
                this.panService.Location = new Point(20, 120);
            }
        }

        #region 拖拽

        private void labTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //为当前应用程序释放鼠标捕获
                ReleaseCapture();
                //发送消息 让系统误以为在标题栏上按下鼠标
                SendMessage((IntPtr)this.Parent.Handle, VM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        private void panTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage((IntPtr)this.Parent.Handle, VM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private const int VM_NCLBUTTONDOWN = 0XA1;//定义鼠标左键按下
        private const int HTCAPTION = 2;

        #endregion

        #region 最小化、最大化、关闭

        //最小化
        private void labTopMinimize_MouseHover(object sender, EventArgs e)
        {
            this.labTopMinimize.ForeColor = Color.FromArgb(160, 215, 233);
        }

        private void labTopMinimize_MouseLeave(object sender, EventArgs e)
        {
            this.labTopMinimize.ForeColor = Color.White;
        }

        private void labTopMinimize_Click(object sender, EventArgs e)
        {
            this.formDefault.WindowState = FormWindowState.Minimized;
        }

        //最大化
        private void labTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.formDefault.Location = new Point(0, 0);
        }
        private void panTop_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.formDefault.Location = new Point(0, 0);
        }

        //关闭
        private void labTopClose_MouseHover(object sender, EventArgs e)
        {
            this.labTopClose.ForeColor = Color.FromArgb(160, 215, 233);
        }

        private void labTopClose_MouseLeave(object sender, EventArgs e)
        {
            this.labTopClose.ForeColor = Color.White;
        }

        private void labTopClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定退出吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.formDefault.Close();
            }
        }

        #endregion

        #region 主界面

        /// <summary>
        /// 主界面按钮文本
        /// </summary>
        public string MainText
        {
            get
            {
                return this.mainText;
            }
            set
            {
                this.mainText = value;
                this.labTopMain.Text = this.mainText;
            }
        }
        private string mainText = "主界面";

        private void labTopMain_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopMain_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(160, 215, 233);
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopMain_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.labTopData.Font = new Font("微软雅黑", 14);
                this.labTopData.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopData.BackColor = Color.Transparent;
                this.labTopData.Cursor = Cursors.Hand;

                this.labTopLog.Font = new Font("微软雅黑", 14);
                this.labTopLog.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopLog.BackColor = Color.Transparent;
                this.labTopLog.Cursor = Cursors.Hand;

                this.labTopEvent.Font = new Font("微软雅黑", 14);
                this.labTopEvent.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopEvent.BackColor = Color.Transparent;
                this.labTopEvent.Cursor = Cursors.Hand;

                this.labTopPar.Font = new Font("微软雅黑", 14);
                this.labTopPar.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopPar.BackColor = Color.Transparent;
                this.labTopPar.Cursor = Cursors.Hand;

                this.labTopOps.Font = new Font("微软雅黑", 14);
                this.labTopOps.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopOps.BackColor = Color.Transparent;
                this.labTopOps.Cursor = Cursors.Hand;

                this.labTopCode.Font = new Font("微软雅黑", 14);
                this.labTopCode.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopCode.BackColor = Color.Transparent;
                this.labTopCode.Cursor = Cursors.Hand;

                this.labTopService.Font = new Font("微软雅黑", 14);
                this.labTopService.ForeColor = Color.White;
                this.labTopService.BackColor = Color.Transparent;
                this.labTopService.Cursor = Cursors.Hand;

                this.panMain.Visible = true;
                this.panData.Visible = false;
                this.panLog.Visible = false;
                this.panEvent.Visible = false;
                this.panPar.Visible = false;
                this.panOps.Visible = false;
                this.panCode.Visible = false;
                this.panService.Visible = false;
            }
        }

        #endregion

        #region 数据查询

        /// <summary>
        /// 是否开启数据查询
        /// </summary>
        public bool IsData
        {
            get
            {
                return this.isData;
            }
            set
            {
                this.isData = value;
                if (this.isData)
                    this.labTopData.Visible = true;
                else
                    this.labTopData.Visible = false;
            }
        }
        private bool isData = true;

        /// <summary>
        /// 数据查询按钮文本
        /// </summary>
        public string DataText
        {
            get
            {
                return this.dataText;
            }
            set
            {
                this.dataText = value;
                this.labTopData.Text = this.dataText;
            }
        }
        private string dataText = "数据查询";

        /// <summary>
        /// 数据查询用户控件
        /// </summary>
        public BatMes.Client.Controls.BatteryData BatteryData 
        { 
            get
            {
                return this.batteryData;
            }
            set
            {
                this.batteryData = value;
                if(this.batteryData != null)
                {
                    this.panData.Controls.Add(this.batteryData);
                }
            }
        }
        private BatMes.Client.Controls.BatteryData batteryData = null;

        private void labTopData_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopData_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(160, 215, 233);
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopData_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                this.labTopMain.Font = new Font("微软雅黑", 14);
                this.labTopMain.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopMain.BackColor = Color.Transparent;
                this.labTopMain.Cursor = Cursors.Hand;

                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.labTopLog.Font = new Font("微软雅黑", 14);
                this.labTopLog.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopLog.BackColor = Color.Transparent;
                this.labTopLog.Cursor = Cursors.Hand;

                this.labTopEvent.Font = new Font("微软雅黑", 14);
                this.labTopEvent.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopEvent.BackColor = Color.Transparent;
                this.labTopEvent.Cursor = Cursors.Hand;

                this.labTopPar.Font = new Font("微软雅黑", 14);
                this.labTopPar.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopPar.BackColor = Color.Transparent;
                this.labTopPar.Cursor = Cursors.Hand;

                this.labTopOps.Font = new Font("微软雅黑", 14);
                this.labTopOps.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopOps.BackColor = Color.Transparent;
                this.labTopOps.Cursor = Cursors.Hand;

                this.labTopCode.Font = new Font("微软雅黑", 14);
                this.labTopCode.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopCode.BackColor = Color.Transparent;
                this.labTopCode.Cursor = Cursors.Hand;

                this.labTopService.Font = new Font("微软雅黑", 14);
                this.labTopService.ForeColor = Color.White;
                this.labTopService.BackColor = Color.Transparent;
                this.labTopService.Cursor = Cursors.Hand;

                this.panMain.Visible = false;
                this.panData.Visible = true;
                this.panLog.Visible = false;
                this.panEvent.Visible = false;
                this.panPar.Visible = false;
                this.panOps.Visible = false;
                this.panCode.Visible = false;
                this.panService.Visible = false;
            }
        }

        #endregion

        #region 交互日志

        /// <summary>
        /// 是否开启交互日志
        /// </summary>
        public bool IsLog
        {
            get
            {
                return this.isLog;
            }
            set
            {
                this.isLog = value;
                if (this.isLog)
                    this.labTopLog.Visible = true;
                else
                    this.labTopLog.Visible = false;
            }
        }
        private bool isLog = true;

        /// <summary>
        /// 交互日志按钮文本
        /// </summary>
        public string LogText
        {
            get
            {
                return this.logText;
            }
            set
            {
                this.logText = value;
                this.labTopLog.Text = this.logText;
            }
        }
        private string logText = "交互日志";

        /// <summary>
        /// 交互日志用户控件
        /// </summary>
        public BatMes.Client.Controls.SysLog SysLog
        {
            get
            {
                return this.sysLog;
            }
            set
            {
                this.sysLog = value;
                if (this.sysLog != null)
                {
                    this.panLog.Controls.Add(this.sysLog);
                }
            }
        }
        private BatMes.Client.Controls.SysLog sysLog = null;

        private void labTopLog_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopLog_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(160, 215, 233);
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopLog_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                this.labTopMain.Font = new Font("微软雅黑", 14);
                this.labTopMain.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopMain.BackColor = Color.Transparent;
                this.labTopMain.Cursor = Cursors.Hand;

                this.labTopData.Font = new Font("微软雅黑", 14);
                this.labTopData.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopData.BackColor = Color.Transparent;
                this.labTopData.Cursor = Cursors.Hand;

                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.labTopEvent.Font = new Font("微软雅黑", 14);
                this.labTopEvent.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopEvent.BackColor = Color.Transparent;
                this.labTopEvent.Cursor = Cursors.Hand;

                this.labTopPar.Font = new Font("微软雅黑", 14);
                this.labTopPar.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopPar.BackColor = Color.Transparent;
                this.labTopPar.Cursor = Cursors.Hand;

                this.labTopOps.Font = new Font("微软雅黑", 14);
                this.labTopOps.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopOps.BackColor = Color.Transparent;
                this.labTopOps.Cursor = Cursors.Hand;

                this.labTopCode.Font = new Font("微软雅黑", 14);
                this.labTopCode.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopCode.BackColor = Color.Transparent;
                this.labTopCode.Cursor = Cursors.Hand;

                this.labTopService.Font = new Font("微软雅黑", 14);
                this.labTopService.ForeColor = Color.White;
                this.labTopService.BackColor = Color.Transparent;
                this.labTopService.Cursor = Cursors.Hand;

                this.panMain.Visible = false;
                this.panData.Visible = false;
                this.panLog.Visible = true;
                this.panEvent.Visible = false;
                this.panPar.Visible = false;
                this.panOps.Visible = false;
                this.panCode.Visible = false;
                this.panService.Visible = false;
            }
        }

        #endregion

        #region 系统事件

        /// <summary>
        /// 是否开启系统事件
        /// </summary>
        public bool IsEvent
        {
            get
            {
                return this.isEvent;
            }
            set
            {
                this.isEvent = value;
                if (this.isEvent)
                    this.labTopEvent.Visible = true;
                else
                    this.labTopEvent.Visible = false;
            }
        }
        private bool isEvent = true;

        /// <summary>
        /// 系统事件按钮文本
        /// </summary>
        public string EventText
        {
            get
            {
                return this.eventText;
            }
            set
            {
                this.eventText = value;
                this.labTopEvent.Text = this.eventText;
            }
        }
        private string eventText = "系统事件";

        /// <summary>
        /// 系统事件用户控件
        /// </summary>
        public BatMes.Client.Controls.SysEvent SysEvent
        {
            get
            {
                return this.sysEvent;
            }
            set
            {
                this.sysEvent = value;
                if (this.sysEvent != null)
                {
                    this.panEvent.Controls.Add(this.sysEvent);
                }
            }
        }
        private BatMes.Client.Controls.SysEvent sysEvent = null;

        private void labTopEvent_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopEvent_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(160, 215, 233);
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopEvent_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                this.labTopMain.Font = new Font("微软雅黑", 14);
                this.labTopMain.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopMain.BackColor = Color.Transparent;
                this.labTopMain.Cursor = Cursors.Hand;

                this.labTopData.Font = new Font("微软雅黑", 14);
                this.labTopData.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopData.BackColor = Color.Transparent;
                this.labTopData.Cursor = Cursors.Hand;

                this.labTopLog.Font = new Font("微软雅黑", 14);
                this.labTopLog.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopLog.BackColor = Color.Transparent;
                this.labTopLog.Cursor = Cursors.Hand;

                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.labTopPar.Font = new Font("微软雅黑", 14);
                this.labTopPar.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopPar.BackColor = Color.Transparent;
                this.labTopPar.Cursor = Cursors.Hand;

                this.labTopOps.Font = new Font("微软雅黑", 14);
                this.labTopOps.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopOps.BackColor = Color.Transparent;
                this.labTopOps.Cursor = Cursors.Hand;

                this.labTopCode.Font = new Font("微软雅黑", 14);
                this.labTopCode.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopCode.BackColor = Color.Transparent;
                this.labTopCode.Cursor = Cursors.Hand;

                this.labTopService.Font = new Font("微软雅黑", 14);
                this.labTopService.ForeColor = Color.White;
                this.labTopService.BackColor = Color.Transparent;
                this.labTopService.Cursor = Cursors.Hand;

                this.panMain.Visible = false;
                this.panData.Visible = false;
                this.panLog.Visible = false;
                this.panEvent.Visible = true;
                this.panPar.Visible = false;
                this.panOps.Visible = false;
                this.panCode.Visible = false;
                this.panService.Visible = false;
            }
        }

        #endregion

        #region 系统参数

        /// <summary>
        /// 是否开启系统参数
        /// </summary>
        public bool IsPar
        {
            get
            {
                return this.isPar;
            }
            set
            {
                this.isPar = value;
                if (this.isPar)
                    this.labTopPar.Visible = true;
                else
                    this.labTopPar.Visible = false;
            }
        }
        private bool isPar = true;

        /// <summary>
        /// 系统参数按钮文本
        /// </summary>
        public string ParText
        {
            get
            {
                return this.parText;
            }
            set
            {
                this.parText = value;
                this.labTopPar.Text = this.parText;
            }
        }
        private string parText = "系统参数";

        /// <summary>
        /// 系统参数用户控件
        /// </summary>
        public BatMes.Client.Controls.SysPar SysPar
        {
            get
            {
                return this.sysPar;
            }
            set
            {
                this.sysPar = value;
                if (this.sysPar != null)
                {
                    this.panPar.Controls.Add(this.sysPar);
                }
            }
        }
        private BatMes.Client.Controls.SysPar sysPar = null;

        private void labTopPar_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopPar_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(160, 215, 233);
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopPar_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                this.labTopMain.Font = new Font("微软雅黑", 14);
                this.labTopMain.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopMain.BackColor = Color.Transparent;
                this.labTopMain.Cursor = Cursors.Hand;

                this.labTopData.Font = new Font("微软雅黑", 14);
                this.labTopData.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopData.BackColor = Color.Transparent;
                this.labTopData.Cursor = Cursors.Hand;

                this.labTopLog.Font = new Font("微软雅黑", 14);
                this.labTopLog.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopLog.BackColor = Color.Transparent;
                this.labTopLog.Cursor = Cursors.Hand;

                this.labTopEvent.Font = new Font("微软雅黑", 14);
                this.labTopEvent.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopEvent.BackColor = Color.Transparent;
                this.labTopEvent.Cursor = Cursors.Hand;

                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.labTopOps.Font = new Font("微软雅黑", 14);
                this.labTopOps.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopOps.BackColor = Color.Transparent;
                this.labTopOps.Cursor = Cursors.Hand;

                this.labTopCode.Font = new Font("微软雅黑", 14);
                this.labTopCode.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopCode.BackColor = Color.Transparent;
                this.labTopCode.Cursor = Cursors.Hand;

                this.labTopService.Font = new Font("微软雅黑", 14);
                this.labTopService.ForeColor = Color.White;
                this.labTopService.BackColor = Color.Transparent;
                this.labTopService.Cursor = Cursors.Hand;

                this.panMain.Visible = false;
                this.panData.Visible = false;
                this.panLog.Visible = false;
                this.panEvent.Visible = false;
                this.panPar.Visible = true;
                this.panOps.Visible = false;
                this.panCode.Visible = false;
                this.panService.Visible = false;
            }
        }

        #endregion

        #region 工序管理
        
        /// <summary>
        /// 是否开启工序管理
        /// </summary>
        public bool IsOps
        {
            get
            {
                return this.isOps;
            }
            set
            {
                this.isOps = value;
                if (this.isOps)
                    this.labTopOps.Visible = true;
                else
                    this.labTopOps.Visible = false;
            }
        }
        private bool isOps = false;
        
        /// <summary>
        /// 工序管理按钮文本
        /// </summary>
        public string OpsText
        {
            get
            {
                return this.opsText;
            }
            set
            {
                this.opsText = value;
                this.labTopOps.Text = this.opsText;
            }
        }
        private string opsText = "工序管理";

        /// <summary>
        /// 工序管理用户控件
        /// </summary>
        public BatMes.Client.Controls.Ops Ops
        {
            get
            {
                return this.ops;
            }
            set
            {
                this.ops = value;
                if (this.ops != null)
                {
                    this.panOps.Controls.Add(this.ops);
                }
            }
        }
        private BatMes.Client.Controls.Ops ops = null;

        private void labTopOps_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopOps_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(160, 215, 233);
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopOps_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                this.labTopMain.Font = new Font("微软雅黑", 14);
                this.labTopMain.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopMain.BackColor = Color.Transparent;
                this.labTopMain.Cursor = Cursors.Hand;

                this.labTopData.Font = new Font("微软雅黑", 14);
                this.labTopData.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopData.BackColor = Color.Transparent;
                this.labTopData.Cursor = Cursors.Hand;

                this.labTopLog.Font = new Font("微软雅黑", 14);
                this.labTopLog.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopLog.BackColor = Color.Transparent;
                this.labTopLog.Cursor = Cursors.Hand;

                this.labTopEvent.Font = new Font("微软雅黑", 14);
                this.labTopEvent.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopEvent.BackColor = Color.Transparent;
                this.labTopEvent.Cursor = Cursors.Hand;

                this.labTopPar.Font = new Font("微软雅黑", 14);
                this.labTopPar.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopPar.BackColor = Color.Transparent;
                this.labTopPar.Cursor = Cursors.Hand;

                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.labTopCode.Font = new Font("微软雅黑", 14);
                this.labTopCode.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopCode.BackColor = Color.Transparent;
                this.labTopCode.Cursor = Cursors.Hand;

                this.labTopService.Font = new Font("微软雅黑", 14);
                this.labTopService.ForeColor = Color.White;
                this.labTopService.BackColor = Color.Transparent;
                this.labTopService.Cursor = Cursors.Hand;

                this.panMain.Visible = false;
                this.panData.Visible = false;
                this.panLog.Visible = false;
                this.panEvent.Visible = false;
                this.panPar.Visible = false;
                this.panOps.Visible = true;
                this.panCode.Visible = false;
                this.panService.Visible = false;
            }
        }

        #endregion

        #region 结果代码

        /// <summary>
        /// 是否开启结果代码
        /// </summary>
        public bool IsCode
        {
            get
            {
                return this.isCode;
            }
            set
            {
                this.isCode = value;
                if (this.isCode)
                    this.labTopCode.Visible = true;
                else
                    this.labTopCode.Visible = false;
            }
        }
        private bool isCode = false;

        /// <summary>
        /// 结果代码按钮文本
        /// </summary>
        public string CodeText
        {
            get
            {
                return this.codeText;
            }
            set
            {
                this.codeText = value;
                this.labTopCode.Text = this.codeText;
            }
        }
        private string codeText = "结果代码";

        /// <summary>
        /// 结果代码用户控件
        /// </summary>
        public BatMes.Client.Controls.BRCode BRCode
        {
            get
            {
                return this.brCode;
            }
            set
            {
                this.brCode = value;
                if (this.brCode != null)
                {
                    this.panCode.Controls.Add(this.brCode);
                }
            }
        }
        private BatMes.Client.Controls.BRCode brCode = null;

        private void labTopCode_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopCode_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(160, 215, 233);
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopCode_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                this.labTopMain.Font = new Font("微软雅黑", 14);
                this.labTopMain.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopMain.BackColor = Color.Transparent;
                this.labTopMain.Cursor = Cursors.Hand;

                this.labTopData.Font = new Font("微软雅黑", 14);
                this.labTopData.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopData.BackColor = Color.Transparent;
                this.labTopData.Cursor = Cursors.Hand;

                this.labTopLog.Font = new Font("微软雅黑", 14);
                this.labTopLog.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopLog.BackColor = Color.Transparent;
                this.labTopLog.Cursor = Cursors.Hand;

                this.labTopEvent.Font = new Font("微软雅黑", 14);
                this.labTopEvent.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopEvent.BackColor = Color.Transparent;
                this.labTopEvent.Cursor = Cursors.Hand;

                this.labTopPar.Font = new Font("微软雅黑", 14);
                this.labTopPar.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopPar.BackColor = Color.Transparent;
                this.labTopPar.Cursor = Cursors.Hand;

                this.labTopOps.Font = new Font("微软雅黑", 14);
                this.labTopOps.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopOps.BackColor = Color.Transparent;
                this.labTopOps.Cursor = Cursors.Hand;

                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.labTopService.Font = new Font("微软雅黑", 14);
                this.labTopService.ForeColor = Color.White;
                this.labTopService.BackColor = Color.Transparent;
                this.labTopService.Cursor = Cursors.Hand;

                this.panMain.Visible = false;
                this.panData.Visible = false;
                this.panLog.Visible = false;
                this.panEvent.Visible = false;
                this.panPar.Visible = false;
                this.panOps.Visible = false;
                this.panCode.Visible = true;
                this.panService.Visible = false;
            }
        }

        #endregion

        #region 软件服务

        /// <summary>
        /// 是否开启软件服务
        /// </summary>
        public bool IsService
        {
            get
            {
                return this.isService;
            }
            set
            {
                this.isService = value;
                if (this.isService)
                    this.labTopService.Visible = true;
                else
                    this.labTopService.Visible = false;
            }
        }
        private bool isService = true;

        /// <summary>
        /// 软件服务按钮文本
        /// </summary>
        public string ServiceText
        {
            get
            {
                return this.serviceText;
            }
            set
            {
                this.serviceText = value;
                this.labTopService.Text = this.serviceText;
            }
        }
        private string serviceText = "软件服务";

        /// <summary>
        /// 软件服务用户控件
        /// </summary>
        public BatMes.Client.Controls.Service Service
        {
            get
            {
                return this.service;
            }
            set
            {
                this.service = value;
                if (this.service != null)
                {
                    this.panService.Controls.Add(this.service);
                }
            }
        }
        private BatMes.Client.Controls.Service service = null;

        private void labTopService_MouseHover(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
            }
        }

        private void labTopService_MouseLeave(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                c.ForeColor = Color.White;
                c.BackColor = Color.Transparent;
            }
        }

        private void labTopService_Click(object sender, EventArgs e)
        {
            var c = (Label)sender;
            if (!c.Font.Bold)
            {
                this.labTopMain.Font = new Font("微软雅黑", 14);
                this.labTopMain.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopMain.BackColor = Color.Transparent;
                this.labTopMain.Cursor = Cursors.Hand;

                this.labTopData.Font = new Font("微软雅黑", 14);
                this.labTopData.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopData.BackColor = Color.Transparent;
                this.labTopData.Cursor = Cursors.Hand;

                this.labTopLog.Font = new Font("微软雅黑", 14);
                this.labTopLog.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopLog.BackColor = Color.Transparent;
                this.labTopLog.Cursor = Cursors.Hand;

                this.labTopEvent.Font = new Font("微软雅黑", 14);
                this.labTopEvent.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopEvent.BackColor = Color.Transparent;
                this.labTopEvent.Cursor = Cursors.Hand;

                this.labTopPar.Font = new Font("微软雅黑", 14);
                this.labTopPar.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopPar.BackColor = Color.Transparent;
                this.labTopPar.Cursor = Cursors.Hand;

                this.labTopOps.Font = new Font("微软雅黑", 14);
                this.labTopOps.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopOps.BackColor = Color.Transparent;
                this.labTopOps.Cursor = Cursors.Hand;

                this.labTopCode.Font = new Font("微软雅黑", 14);
                this.labTopCode.ForeColor = Color.FromArgb(160, 215, 233);
                this.labTopCode.BackColor = Color.Transparent;
                this.labTopCode.Cursor = Cursors.Hand;

                c.Font = new Font("微软雅黑", 14, FontStyle.Bold);
                c.ForeColor = Color.FromArgb(0, 125, 184);
                c.BackColor = Color.White;
                c.Cursor = Cursors.Arrow;

                this.panMain.Visible = false;
                this.panData.Visible = false;
                this.panLog.Visible = false;
                this.panEvent.Visible = false;
                this.panPar.Visible = false;
                this.panOps.Visible = false;
                this.panCode.Visible = false;
                this.panService.Visible = true;
            }
        }

        #endregion

    }//end class
}