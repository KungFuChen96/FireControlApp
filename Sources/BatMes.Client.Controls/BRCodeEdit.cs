using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class BRCodeEdit : UserControl
    {
        /// <summary>
        /// 结果代码列表窗体
        /// </summary>
        public BRCode BRCode { get; set; }

        public BRCodeEdit()
        {
            InitializeComponent();
        }

        private void BRCodeEdit_Load(object sender, EventArgs e)
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
            //转换显示
            this.txtCustomeCode.Width = this.Width - 138 - 50 - 21 - 5;
            this.labCustomCodeMust.Location = new Point(this.txtCustomeCode.Width + 138 + 5, 160);
            //备注
            this.txtRemark.Width = this.Width - 138 - 50 - 21 - 5;
            this.txtRemark.Height = this.Height - 50 - 50 - 34 - 20 - 34 - 20 - 50 - 50 - 20;
            //提交与删除按钮
            this.ibSubmit.Location = new Point((this.Width - 200 - 20 - 200) / 2, this.Height - 50 - 50);
            this.ibDelete.Location = new Point(this.ibSubmit.Location.X + 200 + 20, this.Height - 50 - 50);
        }

        /// <summary>
        /// 控件绑定数据
        /// </summary>
        /// <param name="brCode"></param>
        /// <param name="resultCode"></param>
        public void Bind(BRCode brCode, string resultCode)
        {
            this.BRCode = brCode;

            resultCode = XY.Text.Trim(resultCode);
            if (resultCode.Length > 0)
            {
                var info = Business.Instance.BatteryResultCodeInfo(resultCode);
                if (info != null)
                {
                    this.txtResultCode.Text = info.result_code;
                    this.txtCustomeCode.Text = info.custom_code;
                    this.txtRemark.Text = info.remark;
                }
            }
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
            //保存
            if (this.txtCustomeCode.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入转换显示！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtCustomeCode.Focus();
                return;
            }

            if (Business.Instance.BatteryResultCodeEdit(this.txtResultCode.Text, this.txtCustomeCode.Text, this.txtRemark.Text))
            {
                MessageBox.Show(this, "结果代码编辑成功，请重新启动软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.BRCode.BindBRCode();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show(this, "结果代码编辑失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ibDelete_Click(object sender, EventArgs e)
        {
            //删除
            DialogResult result = MessageBox.Show("确认删除结果代码吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (Business.Instance.BatteryResultCodeRemove(this.txtResultCode.Text))
                {
                    MessageBox.Show(this, "结果代码删除成功，请重新启动软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.BRCode.BindBRCode();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show(this, "结果代码删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

    }//end class
}