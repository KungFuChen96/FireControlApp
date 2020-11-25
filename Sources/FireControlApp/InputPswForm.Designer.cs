namespace FireControlApp
{
    partial class InputPswForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPsw = new System.Windows.Forms.Label();
            this.txtPsw = new System.Windows.Forms.TextBox();
            this.btnSave = new BatMes.Client.Controls.IconButton();
            this.btnCancel = new BatMes.Client.Controls.IconButton();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPsw
            // 
            this.lblPsw.AutoSize = true;
            this.lblPsw.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPsw.Location = new System.Drawing.Point(107, 44);
            this.lblPsw.Name = "lblPsw";
            this.lblPsw.Size = new System.Drawing.Size(58, 24);
            this.lblPsw.TabIndex = 0;
            this.lblPsw.Text = "密码";
            // 
            // txtPsw
            // 
            this.txtPsw.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPsw.Location = new System.Drawing.Point(196, 38);
            this.txtPsw.Name = "txtPsw";
            this.txtPsw.PasswordChar = '*';
            this.txtPsw.Size = new System.Drawing.Size(272, 34);
            this.txtPsw.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.IconType = BatMes.Client.Controls.IconButtonType.Save;
            this.btnSave.IconWidth = 50;
            this.btnSave.IsActive = true;
            this.btnSave.Location = new System.Drawing.Point(111, 144);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 50);
            this.btnSave.TabIndex = 22;
            this.btnSave.TextValue = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.IconType = BatMes.Client.Controls.IconButtonType.Stop;
            this.btnCancel.IconWidth = 50;
            this.btnCancel.IsActive = true;
            this.btnCancel.Location = new System.Drawing.Point(355, 144);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 50);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.SystemColors.Control;
            this.lblMsg.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(118, 101);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 24);
            this.lblMsg.TabIndex = 23;
            this.lblMsg.Visible = false;
            // 
            // InputPswForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 236);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtPsw);
            this.Controls.Add(this.lblPsw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputPswForm";
            this.Text = "请输入密码";
            this.Load += new System.EventHandler(this.InputPswForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPsw;
        private System.Windows.Forms.TextBox txtPsw;
        private BatMes.Client.Controls.IconButton btnSave;
        private BatMes.Client.Controls.IconButton btnCancel;
        private System.Windows.Forms.Label lblMsg;
    }
}