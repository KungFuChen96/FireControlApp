namespace BatMes.Client.Controls
{
    partial class OpsEdit
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
            this.labRemark = new System.Windows.Forms.Label();
            this.ibDelete = new BatMes.Client.Controls.IconButton();
            this.ibSubmit = new BatMes.Client.Controls.IconButton();
            this.labValMust = new System.Windows.Forms.Label();
            this.labNameMust = new System.Windows.Forms.Label();
            this.txtVal = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labVal = new System.Windows.Forms.Label();
            this.labName = new System.Windows.Forms.Label();
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
            this.panTop.TabIndex = 2;
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
            this.labTitle.Size = new System.Drawing.Size(74, 21);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "编辑工序";
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(108, 208);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRemark.Size = new System.Drawing.Size(567, 192);
            this.txtRemark.TabIndex = 20;
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRemark.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labRemark.Location = new System.Drawing.Point(50, 214);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(58, 21);
            this.labRemark.TabIndex = 14;
            this.labRemark.Text = "备注：";
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
            this.ibDelete.TabIndex = 21;
            this.ibDelete.TextValue = "删除";
            this.ibDelete.Click += new System.EventHandler(this.ibDelete_Click);
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
            this.ibSubmit.TabIndex = 15;
            this.ibSubmit.TextValue = "保存";
            this.ibSubmit.Click += new System.EventHandler(this.ibSubmit_Click);
            // 
            // labValMust
            // 
            this.labValMust.AutoSize = true;
            this.labValMust.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labValMust.ForeColor = System.Drawing.Color.Red;
            this.labValMust.Location = new System.Drawing.Point(681, 160);
            this.labValMust.Name = "labValMust";
            this.labValMust.Size = new System.Drawing.Size(21, 27);
            this.labValMust.TabIndex = 27;
            this.labValMust.Text = "*";
            // 
            // labNameMust
            // 
            this.labNameMust.AutoSize = true;
            this.labNameMust.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labNameMust.ForeColor = System.Drawing.Color.Red;
            this.labNameMust.Location = new System.Drawing.Point(681, 106);
            this.labNameMust.Name = "labNameMust";
            this.labNameMust.Size = new System.Drawing.Size(21, 27);
            this.labNameMust.TabIndex = 26;
            this.labNameMust.Text = "*";
            // 
            // txtVal
            // 
            this.txtVal.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVal.Location = new System.Drawing.Point(108, 154);
            this.txtVal.Name = "txtVal";
            this.txtVal.Size = new System.Drawing.Size(567, 34);
            this.txtVal.TabIndex = 25;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(108, 100);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(567, 34);
            this.txtName.TabIndex = 24;
            // 
            // labVal
            // 
            this.labVal.AutoSize = true;
            this.labVal.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labVal.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labVal.Location = new System.Drawing.Point(65, 160);
            this.labVal.Name = "labVal";
            this.labVal.Size = new System.Drawing.Size(42, 21);
            this.labVal.TabIndex = 23;
            this.labVal.Text = "值：";
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labName.Location = new System.Drawing.Point(50, 106);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(58, 21);
            this.labName.TabIndex = 22;
            this.labName.Text = "名称：";
            // 
            // OpsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labValMust);
            this.Controls.Add(this.labNameMust);
            this.Controls.Add(this.txtVal);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labVal);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.ibDelete);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.ibSubmit);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.panTop);
            this.Name = "OpsEdit";
            this.Size = new System.Drawing.Size(750, 520);
            this.Load += new System.EventHandler(this.OpsEdit_Load);
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
        private IconButton ibSubmit;
        private System.Windows.Forms.Label labRemark;
        private IconButton ibDelete;
        private System.Windows.Forms.Label labValMust;
        private System.Windows.Forms.Label labNameMust;
        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labVal;
        private System.Windows.Forms.Label labName;
    }
}
