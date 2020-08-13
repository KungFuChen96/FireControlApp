using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class SysLogDetail : UserControl
    {
        public SysLogDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 日志或事件
        /// </summary>
        public bool IsLog { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string LogID { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Cont { get; set; }

        /// <summary>
        /// 控件绑定数据
        /// </summary>
        public void Bind()
        {
            this.labTitle.Text = this.IsLog ? "日志详情" : "事件详情";
            this.labID.Text = this.IsLog ? "日志ID：" : "事件ID：";
            this.dataLogID.Text = this.LogID;
            this.labType.Text = this.IsLog ? "类型：" : "级别：";
            this.dataLogType.Text = this.Type;
            this.dataCreateTime.Text = this.CreateTime;
            this.dataTitle.Text = this.Title;
            this.dataCont.Text = this.Cont;
        }

        private void SysLogDetail_Load(object sender, EventArgs e)
        {
            this.labClose.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labClose.Text = "\uf00d";

            this.Width = (int)(Tools.ScreenWidth * 0.7);
            this.Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2;
            this.Location = new Point((Tools.ScreenWidth - this.Width) / 2, 0);
            //关闭按钮位置
            this.panTop.Width = this.Width;
            this.labClose.Location = new Point(this.Width - 40, 12);
            //日志ID、类型、时间
            this.panHeader.Width = this.Width - 20 * 2;
            int _item_width = (this.panHeader.Width - 14 * 2) / 3;

            this.labType.Location = new Point(14 + _item_width, 14);
            this.dataLogType.Location = new Point(this.labType.Location.X + 48, 14);

            this.labTime.Location = new Point(14 + _item_width * 2, 14);
            this.dataCreateTime.Location = new Point(this.labTime.Location.X + 48, 14);
            //标题
            this.dataTitle.Width = this.Width - 20 * 2;
            //内容
            this.dataCont.Width = this.Width - 20 * 2;
            this.dataCont.Height = this.Height - 50 - 20 * 2 - 50 - 10 - 50 - 10;
        }

        private void labClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void labClose_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(231, 7, 34);
        }

        private void labClose_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(202, 23, 44);
        }

    }//end class
}