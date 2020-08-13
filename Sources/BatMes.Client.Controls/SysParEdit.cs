using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class SysParEdit : UserControl
    {
        public SysParEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 系统参数列表窗体
        /// </summary>
        public SysPar SysPar { get; set; }

        /// <summary>
        /// 参数类型，用于提交时辅助验证
        /// </summary>
        private Enums.SysParaType sysParaType;

        /// <summary>
        /// 控件绑定数据
        /// </summary>
        public void Bind(SysPar sysPar, string paraID)
        {
            this.SysPar = sysPar;

            paraID = XY.Text.Trim(paraID);
            if(paraID.Length > 0)
            {
                var info = Business.Instance.SysParaInfo(paraID);
                if(info != null)
                {
                    this.dataName.Text = info.name;
                    this.dataParaID.Text = info.para_id;
                    this.dataParaType.Text = Enums.Tools.SysParaType(info.para_type);
                    this.sysParaType = (Enums.SysParaType)info.para_type;
                    this.dataRemark.Text = info.remark;
                    this.dataParaVal.Text = info.para_val;
                }
            }
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
            //只读区域
            this.panHeader.Width = this.Width - 20 * 2;
            this.panHeader.Height = (this.Height - 50 - 20 - 20 - 20 - 50 - 20) / 2;
            //参数值
            this.dataParaVal.Width = this.panHeader.Width;
            this.dataParaVal.Height = this.panHeader.Height;
            this.dataParaVal.Location = new Point(20, 50 + 20 + this.panHeader.Height + 20);
            //保存按钮
            this.ibSave.Location = new Point((this.Width - 200) / 2, this.Height - 50 - 20);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibSave_Click(object sender, EventArgs e)
        {
            //参数验证
            string paraVal = XY.Text.Trim(this.dataParaVal.Text);
            switch(this.sysParaType)
            {
                default:
                case Enums.SysParaType.Text:
                    break;
                case Enums.SysParaType.Integer:
                    if(!XY.Regex.IsInt(paraVal))
                    {
                        MessageBox.Show(this, "参数值必须为整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.dataParaVal.Focus();
                        return;
                    }
                    break;
                case Enums.SysParaType.Decimals:
                    if (!XY.Regex.IsFloat(paraVal))
                    {
                        MessageBox.Show(this, "参数值必须为浮点数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.dataParaVal.Focus();
                        return;
                    }
                    break;
                case Enums.SysParaType.Boolean:
                    if (!XY.Regex.IsBool(paraVal))
                    {
                        MessageBox.Show(this, "参数值必须为布尔字符串（True或False）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.dataParaVal.Focus();
                        return;
                    }
                    break;
            }

            //更新数据库
            if(!Business.Instance.SysParaEdit(this.dataParaID.Text, paraVal))
            {
                MessageBox.Show(this, "系统参数DB更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //更新缓存
            if (!this.SysPar.Client.SysParaEdit(this.dataParaID.Text, paraVal))
            {
                MessageBox.Show(this, "系统参数CACHE更新失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(this, "系统参数更新成功，请重新启动软件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.SysPar.BindPar();
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

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

    }//end class
}