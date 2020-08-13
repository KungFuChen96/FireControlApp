using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class BRCodeAdd : UserControl
    {
        /// <summary>
        /// 结果代码列表窗体
        /// </summary>
        public BRCode BRCode { get; set; }

        public BRCodeAdd()
        {
            InitializeComponent();
        }

        private void BRCodeAdd_Load(object sender, EventArgs e)
        {
            this.labClose.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labClose.Text = "\uf00d";

            this.Width = (int)(Tools.ScreenWidth * 0.6);
            this.Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2;
            this.Location = new Point((Tools.ScreenWidth - this.Width) / 2, 0);
            //关闭按钮位置
            this.panTop.Width = this.Width;
            this.labClose.Location = new Point(this.Width - 40, 12);
            //结果代码
            this.txtResultCode.Width = this.Width - 138 - 50 - 21 - 5;
            this.labResultCodeMust.Location = new Point(this.txtResultCode.Width + 138 + 5, 106);
            //转换显示
            this.txtCustomeCode.Width = this.Width - 138 - 50 - 21 - 5;
            this.labCustomCodeMust.Location = new Point(this.txtCustomeCode.Width + 138 + 5, 160);
            //备注
            this.txtRemark.Width = this.Width - 138 - 50 - 21 - 5;
            this.txtRemark.Height = this.Height - 50 - 50 - 34 - 20 - 34 - 20 - 50 - 50 - 20;
            //提交按钮
            this.ibSubmit.Location = new Point((this.Width - 200) / 2, this.Height - 50 - 50);
        }

        /// <summary>
        /// 控件绑定数据
        /// </summary>
        /// <param name="brCode"></param>
        public void Bind(BRCode brCode)
        {
            this.BRCode = brCode;
            this.txtResultCode.Text = string.Empty;
            this.txtCustomeCode.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
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

        private void ibSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtResultCode.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入结果代码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtResultCode.Focus();
                return;
            }

            if (this.txtCustomeCode.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入转换显示！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtCustomeCode.Focus();
                return;
            }

            if (Business.Instance.BatteryResultCodeAdd(this.txtResultCode.Text, this.txtCustomeCode.Text, this.txtRemark.Text))
            {
                MessageBox.Show(this, "结果代码新增成功，请重新启动软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.BRCode.BindBRCode();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show(this, "结果代码新增失败，请确认结果代码是否已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }//end class
}
