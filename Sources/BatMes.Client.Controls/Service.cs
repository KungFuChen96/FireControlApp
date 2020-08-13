using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace BatMes.Client.Controls
{
    public partial class Service : UserControl
    {
        /// <summary>
        /// 客户端开发规范
        /// </summary>
        public BatMes.Client.IClient Client { get; set; }

        public Service()
        {
            InitializeComponent();
        }

        private void Service_Load(object sender, EventArgs e)
        {
            #region 自适应分辨率

            //宽
            int _width = Tools.ScreenWidth;
            if (_width < Tools.SCREEN_WIDTH_MIN)
                _width = Tools.SCREEN_WIDTH_MIN;
            _width = _width - 20 * 2;

            //高
            int _height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2;

            //设置用户控件宽高
            this.Width = _width;
            this.Height = _height;

            this.panHeader.Width = _width - 20 * 2;
            if(_width == 1024 - 20 * 2)
            {
                this.labServiceCode.Font = new Font("微软雅黑", 13);
                this.serviceCode.Font = new Font("微软雅黑", 13, FontStyle.Bold);
                this.labFreeTime.Font = new Font("微软雅黑", 13);
                this.freeTime.Font = new Font("微软雅黑", 13);
                this.freeStatus.Font = new Font("微软雅黑", 13);
                this.labVersion.Font = new Font("微软雅黑", 13);
                this.version.Font = new Font("微软雅黑", 13);

                this.labRequireCont1.Width = 395;
                this.labRequireCont2.Width = 395;
            }

            //分隔条
            this.panSpacer.Height = this.Height - 20 * 2 - 40 - this.panHeader.Height;
            this.panSpacer.Location = new Point((this.Width - this.panSpacer.Width) / 2, this.panHeader.Height + 20 + 40);

            //软件需求与需求热线
            this.panRequire.Location = new Point((this.Width - this.panSpacer.Width) / 2 + 40, this.panHeader.Height + 20 + 40);
            this.panTel.Location = new Point((this.Width - this.panSpacer.Width) / 2 + 40, this.panLocation.Location.Y);

            #endregion

            this.labQrIcon.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labQrIcon.Text = "\uf029";

            this.labLocationIcon.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labLocationIcon.Text = "\uf3c5";

            this.labRequireIcon.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labRequireIcon.Text = "\uf2b5";

            this.labTelIcon.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labTelIcon.Text = "\uf095";

            this.serviceCode.Text = this.Client.ServiceCode;

            this.freeTime.Text = $"{this.Client.DeliveryTime.ToString("yyyy-MM-dd")} 至 {this.Client.DeliveryTime.AddDays(this.Client.FreeDays).ToString("yyyy-MM-dd")}";
            if(DateTime.Now > this.Client.DeliveryTime.AddDays(this.Client.FreeDays))
            {
                this.freeStatus.Text = "失效";
                this.freeStatus.ForeColor = BatMes.Client.CustomColor.Red;
            }
            else
            {
                this.freeStatus.Text = "有效";
                this.freeStatus.ForeColor = BatMes.Client.CustomColor.Green;
            }

            this.version.Text = this.Client.Version;

            Bitmap qrImage = new System.Drawing.Bitmap(Path.Combine(XY.NetF.IO.WinFormRoot(), "servicecode.png"));
            this.pbQrImage.Image = qrImage;
        }
    }
}
