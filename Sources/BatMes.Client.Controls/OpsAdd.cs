using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    /// <summary>
    /// 新增工序
    /// </summary>
    public partial class OpsAdd : UserControl
    {
        /// <summary>
        /// 工序列表窗体
        /// </summary>
        public Ops Ops { get; set; }

        public OpsAdd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控件绑定数据
        /// </summary>
        /// <param name="ops"></param>
        public void Bind(Ops ops)
        {
            this.Ops = ops;
            this.txtName.Text = string.Empty;
            this.txtVal.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
        }

        private void OpsAdd_Load(object sender, EventArgs e)
        {
            this.labClose.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 18);
            this.labClose.Text = "\uf00d";

            this.Width = (int)(Tools.ScreenWidth * 0.6);
            this.Height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2;
            this.Location = new Point((Tools.ScreenWidth - this.Width) / 2, 0);
            //关闭按钮位置
            this.panTop.Width = this.Width;
            this.labClose.Location = new Point(this.Width - 40, 12);
            //名称
            this.txtName.Width = this.Width - 108 - 50 - 21 - 5;
            this.labNameMust.Location = new Point(this.txtName.Width + 108 + 5, 106);
            //值
            this.txtVal.Width = this.Width - 108 - 50 - 21 - 5;
            this.labValMust.Location = new Point(this.txtVal.Width + 108 + 5, 160);
            //备注
            this.txtRemark.Width = this.Width - 108 - 50 - 21 - 5;
            this.txtRemark.Height = this.Height - 50 - 50 - 34 - 20 - 34 - 20 - 50 - 50 - 20;
            //提交按钮
            this.ibSubmit.Location = new Point((this.Width - 200) / 2, this.Height - 50 - 50);
        }

        private void labClose_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(231, 7, 34);
        }

        private void labClose_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(202, 23, 44);
        }

        private void labClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void ibSubmit_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入工序名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtName.Focus();
                return;
            }

            if (this.txtVal.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入工序值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtVal.Focus();
                return;
            }

            if(Business.Instance.OpsAdd(this.txtName.Text, this.txtVal.Text, this.txtRemark.Text))
            {
                MessageBox.Show(this, "工序新增成功，请重新启动软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Ops.BindOps();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show(this, "工序新增失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }//end class
}
