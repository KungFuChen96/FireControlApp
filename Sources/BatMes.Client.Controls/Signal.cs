using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class Signal : UserControl
    {
        public Signal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Signal_Load(object sender, EventArgs e)
        {
            this.labSignalStatus.Font = new Font("微软雅黑", this.Font.Size, this.Font.Style);
            this.labSignalText.Font = new Font("微软雅黑", this.Font.Size, this.Font.Style);
        }

        /// <summary>
        /// 变更大小
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            this.labSignalText.Width = this.Width / 2;
            this.labSignalText.Height = this.Height;

            this.labSignalStatus.Width = this.Width - this.labSignalText.Width;
            this.labSignalStatus.Location = new Point(this.labSignalText.Width - 1, 0);
            this.labSignalStatus.Height = this.Height;

            base.OnResize(e);
        }

        /// <summary>
        /// 变更字体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            this.labSignalText.Font = new Font("微软雅黑", this.Font.Size, this.Font.Style);
            this.labSignalStatus.Font = new Font("微软雅黑", this.Font.Size, this.Font.Style);

            base.OnFontChanged(e);
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string SignalName
        {
            get { return this.labSignalText.Text; }
            set { this.labSignalText.Text = value; }
        }

        private Enums.SignalStatus status = Enums.SignalStatus.Offline;
        /// <summary>
        /// 状态
        /// </summary>
        public Enums.SignalStatus SignalStatus
        {
            get { return this.status; }
            set
            {
                this.status = value;
                switch (this.status)
                {
                    case Enums.SignalStatus.Offline:
                        this.labSignalStatus.ForeColor = Color.FromName("ControlText");
                        this.labSignalStatus.BackColor = Color.FromArgb(240, 240, 240);
                        this.labSignalStatus.Text = "离线";
                        break;
                    case Enums.SignalStatus.Online:
                        this.labSignalStatus.ForeColor = Color.White;
                        this.labSignalStatus.BackColor = CustomColor.GreenGrass;
                        this.labSignalStatus.Text = "在线";
                        break;
                    case Enums.SignalStatus.Exception:
                        this.labSignalStatus.ForeColor = Color.White;
                        this.labSignalStatus.BackColor = CustomColor.Red;
                        this.labSignalStatus.Text = "异常";
                        break;
                }
            }
        }

    }//end class
}
