namespace BatMes.Client.Controls
{
    partial class SysParEdit
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
            this.panHeader = new System.Windows.Forms.Panel();
            this.dataRemark = new System.Windows.Forms.Label();
            this.labRemark = new System.Windows.Forms.Label();
            this.dataParaType = new System.Windows.Forms.Label();
            this.labType = new System.Windows.Forms.Label();
            this.dataParaID = new System.Windows.Forms.Label();
            this.labID = new System.Windows.Forms.Label();
            this.dataName = new System.Windows.Forms.Label();
            this.labName = new System.Windows.Forms.Label();
            this.dataParaVal = new System.Windows.Forms.TextBox();
            this.ibSave = new BatMes.Client.Controls.IconButton();
            this.panTop.SuspendLayout();
            this.panHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.panTop.Controls.Add(this.labClose);
            this.panTop.Controls.Add(this.labTitle);
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1000, 50);
            this.panTop.TabIndex = 0;
            // 
            // labClose
            // 
            this.labClose.AutoSize = true;
            this.labClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(23)))), ((int)(((byte)(44)))));
            this.labClose.Location = new System.Drawing.Point(959, 12);
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
            this.labTitle.Text = "编辑系统参数";
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panHeader.Controls.Add(this.dataRemark);
            this.panHeader.Controls.Add(this.labRemark);
            this.panHeader.Controls.Add(this.dataParaType);
            this.panHeader.Controls.Add(this.labType);
            this.panHeader.Controls.Add(this.dataParaID);
            this.panHeader.Controls.Add(this.labID);
            this.panHeader.Controls.Add(this.dataName);
            this.panHeader.Controls.Add(this.labName);
            this.panHeader.Location = new System.Drawing.Point(20, 70);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(960, 220);
            this.panHeader.TabIndex = 1;
            // 
            // dataRemark
            // 
            this.dataRemark.AutoSize = true;
            this.dataRemark.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataRemark.Location = new System.Drawing.Point(100, 123);
            this.dataRemark.Name = "dataRemark";
            this.dataRemark.Size = new System.Drawing.Size(89, 21);
            this.dataRemark.TabIndex = 9;
            this.dataRemark.Text = "1:毫伏 2:伏";
            // 
            // labRemark
            // 
            this.labRemark.AutoSize = true;
            this.labRemark.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labRemark.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labRemark.Location = new System.Drawing.Point(15, 123);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(90, 21);
            this.labRemark.TabIndex = 8;
            this.labRemark.Text = "参数描述：";
            // 
            // dataParaType
            // 
            this.dataParaType.AutoSize = true;
            this.dataParaType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataParaType.Location = new System.Drawing.Point(100, 87);
            this.dataParaType.Name = "dataParaType";
            this.dataParaType.Size = new System.Drawing.Size(106, 21);
            this.dataParaType.TabIndex = 7;
            this.dataParaType.Text = "电压显示单位";
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labType.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labType.Location = new System.Drawing.Point(15, 87);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(90, 21);
            this.labType.TabIndex = 6;
            this.labType.Text = "参数类型：";
            // 
            // dataParaID
            // 
            this.dataParaID.AutoSize = true;
            this.dataParaID.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataParaID.Location = new System.Drawing.Point(100, 51);
            this.dataParaID.Name = "dataParaID";
            this.dataParaID.Size = new System.Drawing.Size(59, 21);
            this.dataParaID.TabIndex = 5;
            this.dataParaID.Text = "参数ID";
            // 
            // labID
            // 
            this.labID.AutoSize = true;
            this.labID.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labID.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labID.Location = new System.Drawing.Point(15, 51);
            this.labID.Name = "labID";
            this.labID.Size = new System.Drawing.Size(75, 21);
            this.labID.TabIndex = 4;
            this.labID.Text = "参数ID：";
            // 
            // dataName
            // 
            this.dataName.AutoSize = true;
            this.dataName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataName.Location = new System.Drawing.Point(100, 15);
            this.dataName.Name = "dataName";
            this.dataName.Size = new System.Drawing.Size(74, 21);
            this.dataName.TabIndex = 3;
            this.dataName.Text = "参数名称";
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labName.Location = new System.Drawing.Point(15, 15);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(90, 21);
            this.labName.TabIndex = 2;
            this.labName.Text = "参数名称：";
            // 
            // dataParaVal
            // 
            this.dataParaVal.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataParaVal.Location = new System.Drawing.Point(20, 310);
            this.dataParaVal.Multiline = true;
            this.dataParaVal.Name = "dataParaVal";
            this.dataParaVal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataParaVal.Size = new System.Drawing.Size(960, 230);
            this.dataParaVal.TabIndex = 4;
            this.dataParaVal.Text = "参数值";
            // 
            // ibSave
            // 
            this.ibSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibSave.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibSave.IconType = BatMes.Client.Controls.IconButtonType.Save;
            this.ibSave.IconWidth = 90;
            this.ibSave.IsActive = true;
            this.ibSave.Location = new System.Drawing.Point(400, 560);
            this.ibSave.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ibSave.Name = "ibSave";
            this.ibSave.Size = new System.Drawing.Size(200, 50);
            this.ibSave.TabIndex = 5;
            this.ibSave.TextValue = "保存";
            this.ibSave.Click += new System.EventHandler(this.ibSave_Click);
            // 
            // SysParEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.ibSave);
            this.Controls.Add(this.dataParaVal);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.panTop);
            this.Name = "SysParEdit";
            this.Size = new System.Drawing.Size(1000, 630);
            this.Load += new System.EventHandler(this.SysLogDetail_Load);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.Label labClose;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label dataParaID;
        private System.Windows.Forms.Label labID;
        private System.Windows.Forms.Label dataName;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label dataParaType;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.TextBox dataParaVal;
        private System.Windows.Forms.Label dataRemark;
        private System.Windows.Forms.Label labRemark;
        private IconButton ibSave;
    }
}
