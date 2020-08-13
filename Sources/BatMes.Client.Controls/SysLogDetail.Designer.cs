namespace BatMes.Client.Controls
{
    partial class SysLogDetail
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
            this.dataCreateTime = new System.Windows.Forms.Label();
            this.labTime = new System.Windows.Forms.Label();
            this.dataLogType = new System.Windows.Forms.Label();
            this.labType = new System.Windows.Forms.Label();
            this.dataLogID = new System.Windows.Forms.Label();
            this.labID = new System.Windows.Forms.Label();
            this.dataCont = new System.Windows.Forms.TextBox();
            this.dataTitle = new System.Windows.Forms.TextBox();
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
            this.labTitle.Size = new System.Drawing.Size(42, 21);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "标题";
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panHeader.Controls.Add(this.dataCreateTime);
            this.panHeader.Controls.Add(this.labTime);
            this.panHeader.Controls.Add(this.dataLogType);
            this.panHeader.Controls.Add(this.labType);
            this.panHeader.Controls.Add(this.dataLogID);
            this.panHeader.Controls.Add(this.labID);
            this.panHeader.Location = new System.Drawing.Point(20, 70);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(960, 50);
            this.panHeader.TabIndex = 1;
            // 
            // dataCreateTime
            // 
            this.dataCreateTime.AutoSize = true;
            this.dataCreateTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataCreateTime.Location = new System.Drawing.Point(781, 14);
            this.dataCreateTime.Name = "dataCreateTime";
            this.dataCreateTime.Size = new System.Drawing.Size(163, 21);
            this.dataCreateTime.TabIndex = 9;
            this.dataCreateTime.Text = "2020-12-31 12:12:21";
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labTime.Location = new System.Drawing.Point(732, 14);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(58, 21);
            this.labTime.TabIndex = 8;
            this.labTime.Text = "时间：";
            // 
            // dataLogType
            // 
            this.dataLogType.AutoSize = true;
            this.dataLogType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLogType.Location = new System.Drawing.Point(447, 14);
            this.dataLogType.Name = "dataLogType";
            this.dataLogType.Size = new System.Drawing.Size(42, 21);
            this.dataLogType.TabIndex = 5;
            this.dataLogType.Text = "本地";
            // 
            // labType
            // 
            this.labType.AutoSize = true;
            this.labType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labType.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labType.Location = new System.Drawing.Point(400, 14);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(58, 21);
            this.labType.TabIndex = 4;
            this.labType.Text = "类型：";
            // 
            // dataLogID
            // 
            this.dataLogID.AutoSize = true;
            this.dataLogID.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataLogID.Location = new System.Drawing.Point(79, 14);
            this.dataLogID.Name = "dataLogID";
            this.dataLogID.Size = new System.Drawing.Size(64, 21);
            this.dataLogID.TabIndex = 3;
            this.dataLogID.Text = "999999";
            // 
            // labID
            // 
            this.labID.AutoSize = true;
            this.labID.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labID.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labID.Location = new System.Drawing.Point(14, 14);
            this.labID.Name = "labID";
            this.labID.Size = new System.Drawing.Size(75, 21);
            this.labID.TabIndex = 2;
            this.labID.Text = "日志ID：";
            // 
            // dataCont
            // 
            this.dataCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataCont.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataCont.Location = new System.Drawing.Point(20, 190);
            this.dataCont.Multiline = true;
            this.dataCont.Name = "dataCont";
            this.dataCont.ReadOnly = true;
            this.dataCont.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataCont.Size = new System.Drawing.Size(960, 420);
            this.dataCont.TabIndex = 4;
            this.dataCont.Text = "内容";
            // 
            // dataTitle
            // 
            this.dataTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataTitle.Location = new System.Drawing.Point(18, 130);
            this.dataTitle.Multiline = true;
            this.dataTitle.Name = "dataTitle";
            this.dataTitle.ReadOnly = true;
            this.dataTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataTitle.Size = new System.Drawing.Size(960, 50);
            this.dataTitle.TabIndex = 5;
            this.dataTitle.Text = "标题";
            // 
            // SysLogDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.dataTitle);
            this.Controls.Add(this.dataCont);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.panTop);
            this.Name = "SysLogDetail";
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
        private System.Windows.Forms.Label dataLogType;
        private System.Windows.Forms.Label dataLogID;
        private System.Windows.Forms.Label labID;
        private System.Windows.Forms.TextBox dataCont;
        private System.Windows.Forms.Label labType;
        private System.Windows.Forms.Label dataCreateTime;
        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.TextBox dataTitle;
    }
}
