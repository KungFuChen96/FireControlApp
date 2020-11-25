using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireControlApp
{
    public partial class InputPswForm : Form
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 点击保存OK
        /// </summary>
        public bool IsOk { get; set; } = false;

        public InputPswForm()
        {
            InitializeComponent();
        }

        private void InputPswForm_Load(object sender, EventArgs e)
        {
            txtPsw.TabIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var inputVal = txtPsw.Text ?? string.Empty;
            if (string.IsNullOrWhiteSpace(inputVal) || inputVal.Trim() != ControlMap.Password)
            {
                IsOk = false;
                lblMsg.Text = "密码有误";
                lblMsg.Visible = true;
            }
            else
            {
                IsOk = true;
                PassWord = inputVal.Trim();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsOk = false;
            this.Close();
        }
    }
}
