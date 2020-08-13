namespace BatMes.Client.Controls
{
    partial class BRCodeEdit
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panTop = new System.Windows.Forms.Panel();
            this.labClose = new System.Windows.Forms.Label();
            this.labTitle = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.labCustomCodeMust = new System.Windows.Forms.Label();
            this.txtCustomeCode = new System.Windows.Forms.TextBox();
            this.txtResultCode = new System.Windows.Forms.TextBox();
            this.ibSubmit = new BatMes.Client.Controls.IconButton();
            this.labRemark = new System.Windows.Forms.Label();
            this.labCustomCode = new System.Windows.Forms.Label();
            this.labResultCode = new System.Windows.Forms.Label();
            this.ibDelete = new BatMes.Client.Controls.IconButton();
            this.panTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.panTop.Controls.Add(this.labClose);
            this.panTop.Controls.Add(this.labTitle);
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(750, 50);
            this.panTop.TabIndex = 3;
            // 
            // labClose
            // 
            this.labClose.AutoSize = true;
            this.labClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(23)))), ((int)(((byte)(44)))));
            this.labClose.Location = new System.Drawing.Point(708, 12);
            this.labClose.Name = "labClose";
            this.labClose.Size = new System.Drawing.Size(29, 12);
            this.labClose.TabIndex = 1;
            this.labClose.Text = "关闭";
            this.labClose.Click += new System.EventHandler(this.labClose_Click);
            this.labClose.MouseLeave += new System.EventHandler(this.labClose_MouseLeave);
            this.labClose.MouseHover += new System.EventHandler(this.labClose_MouseHover);
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.Location = new System.Drawing.Point(14, 14);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(106, 21);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "编辑结果代码";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(138, 208);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(538, 192);
            this.txtRemark.TabIndex = 29;
            // 
            // labCustomCodeMust
            // 
            this.labCustomCodeMust.AutoSize = true;
            this.labCustomCodeMust.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCustomCodeMust.ForeColor = System.Drawing.Color.Red;
            this.labCustomCodeMust.Location = new System.Drawing.Point(681, 160);
            this.labCustomCodeMust.Name = "labCustomCodeMust";
            this.labCustomCodeMust.Size = new System.Drawing.Size(21, 27);
            this.labCustomCodeMust.TabIndex = 28;
            this.labCustomCodeMust.Text = "*";
            // 
            // txtCustomeCode
            // 
            this.txtCustomeCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCustomeCode.Location = new System.Drawing.Point(138, 154);
            this.txtCustomeCode.Name = "txtCustomeCode";
            this.txtCustomeCode.Size = new System.Drawing.Size(538, 34);
            this.txtCustomeCode.TabIndex = 26;
            // 
            // txtResultCode
            // 
            this.txtResultCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResultCode.Location = new System.Drawing.Point(138, 100);
            this.txtResultCode.Name = "txtResultCode";
            this.txtResultCode.ReadOnly = true;
            this.txtResultCode.Size = new System.Drawing.Size(538, 34);
            this.txtResultCode.TabIndex = 25;
            // 
            // ibSubmit
            // 
            this.ibSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibSubmit.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibSubmit.IconType = BatMes.Client.Controls.IconButtonType.Save;
            this.ibSubmit.IconWidth = 90;
            this.ibSubmit.IsActive = true;
            this.ibSubmit.Location = new System.Drawing.Point(165, 420);
            this.ibSubmit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ibSubmit.Name = "ibSubmit";
            this.ibSubmit.Size = new System.Drawing.Size(200, 50);
            this.ibSubmit.TabIndex = 24;
            this.ibSubmit.TextValue = "保存";
            this.ibSubmit.Click += new System.EventHandler(this.ibSubmit_Click);
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRemark.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labRemark.Location = new System.Drawing.Point(82, 214);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(58, 21);
            this.labRemark.TabIndex = 23;
            this.labRemark.Text = "备注：";
            // 
            // labCustomCode
            // 
            this.labCustomCode.AutoSize = true;
            this.labCustomCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCustomCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labCustomCode.Location = new System.Drawing.Point(50, 160);
            this.labCustomCode.Name = "labCustomCode";
            this.labCustomCode.Size = new System.Drawing.Size(90, 21);
            this.labCustomCode.TabIndex = 22;
            this.labCustomCode.Text = "转换显示：";
            // 
            // labResultCode
            // 
            this.labResultCode.AutoSize = true;
            this.labResultCode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labResultCode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labResultCode.Location = new System.Drawing.Point(50, 106);
            this.labResultCode.Name = "labResultCode";
            this.labResultCode.Size = new System.Drawing.Size(90, 21);
            this.labResultCode.TabIndex = 21;
            this.labResultCode.Text = "结果代码：";
            // 
            // ibDelete
            // 
            this.ibDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibDelete.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibDelete.IconType = BatMes.Client.Controls.IconButtonType.Trash;
            this.ibDelete.IconWidth = 90;
            this.ibDelete.IsActive = true;
            this.ibDelete.Location = new System.Drawing.Point(385, 420);
            this.ibDelete.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ibDelete.Name = "ibDelete";
            this.ibDelete.Size = new System.Drawing.Size(200, 50);
            this.ibDelete.TabIndex = 30;
            this.ibDelete.TextValue = "删除";
            this.ibDelete.Click += new System.EventHandler(this.ibDelete_Click);
            // 
            // BRCodeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.ibDelete);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labCustomCodeMust);
            this.Controls.Add(this.txtCustomeCode);
            this.Controls.Add(this.txtResultCode);
            this.Controls.Add(this.ibSubmit);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labCustomCode);
            this.Controls.Add(this.labResultCode);
            this.Controls.Add(this.panTop);
            this.Name = "BRCodeEdit";
            this.Size = new System.Drawing.Size(750, 520);
            this.Load += new System.EventHandler(this.BRCodeEdit_Load);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Label labClose;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label labCustomCodeMust;
        private System.Windows.Forms.TextBox txtCustomeCode;
        private System.Windows.Forms.TextBox txtResultCode;
        private IconButton ibSubmit;
        private System.Windows.Forms.Label labRemark;
        private System.Windows.Forms.Label labCustomCode;
        private System.Windows.Forms.Label labResultCode;
        private IconButton ibDelete;
    }
}
