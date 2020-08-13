using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class OpsEdit : UserControl
    {
        /// <summary>
        /// 工序列表窗体
        /// </summary>
        public Ops Ops { get; set; }

        private int opsID = 0;

        public OpsEdit()
        {
            InitializeComponent();
        }

        private void OpsEdit_Load(object sender, EventArgs e)
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
            //提交与删除按钮
            this.ibSubmit.Location = new Point((this.Width - 200 - 20 - 200) / 2, this.Height - 50 - 50);
            this.ibDelete.Location = new Point(this.ibSubmit.Location.X + 200 + 20, this.Height - 50 - 50);
        }

        /// <summary>
        /// 控件绑定数据
        /// </summary>
        /// <param name="ops"></param>
        /// <param name="opsID"></param>
        public void Bind(Ops ops, int opsID)
        {
            this.Ops = ops;

            if (opsID > 0)
            {
                this.opsID = opsID;
                var info = Business.Instance.OpsInfo(this.opsID);
                if (info != null)
                {
                    this.txtName.Text = info.ops_name;
                    this.txtVal.Text = info.ops_val;
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

            if (Business.Instance.OpsEdit(this.opsID, this.txtName.Text, this.txtVal.Text, this.txtRemark.Text))
            {
                MessageBox.Show(this, "工序编辑成功，请重新启动软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Ops.BindOps();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show(this, "工序编辑失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ibDelete_Click(object sender, EventArgs e)
        {
            //删除
            DialogResult result = MessageBox.Show("确认删除工序吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (Business.Instance.OpsRemove(this.opsID))
                {
                    MessageBox.Show(this, "工序删除成功，请重新启动软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Ops.BindOps();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show(this, "工序删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

    }//end class
}