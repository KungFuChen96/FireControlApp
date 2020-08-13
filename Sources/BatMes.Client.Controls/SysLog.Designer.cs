namespace BatMes.Client.Controls
{
    partial class SysLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labLogType = new System.Windows.Forms.Label();
            this.cbLogType = new System.Windows.Forms.ComboBox();
            this.labTime = new System.Windows.Forms.Label();
            this.labTimeBetween = new System.Windows.Forms.Label();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.txtBeginTime = new System.Windows.Forms.TextBox();
            this.dgvLog = new System.Windows.Forms.DataGridView();
            this.labPager = new System.Windows.Forms.Label();
            this.panTopZone = new System.Windows.Forms.FlowLayoutPanel();
            this.ibSearch = new BatMes.Client.Controls.IconButton();
            this.ibExport = new BatMes.Client.Controls.IconButton();
            this.panBottomZone = new System.Windows.Forms.FlowLayoutPanel();
            this.ibFirst = new BatMes.Client.Controls.IconButton();
            this.ibPrev = new BatMes.Client.Controls.IconButton();
            this.ibNext = new BatMes.Client.Controls.IconButton();
            this.ibLast = new BatMes.Client.Controls.IconButton();
            this.logDetail = new BatMes.Client.Controls.SysLogDetail();
            this.calendar = new BatMes.Client.Controls.Calendar();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).BeginInit();
            this.panTopZone.SuspendLayout();
            this.panBottomZone.SuspendLayout();
            this.SuspendLayout();
            // 
            // labLogType
            // 
            this.labLogType.AutoSize = true;
            this.labLogType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLogType.Location = new System.Drawing.Point(20, 24);
            this.labLogType.Margin = new System.Windows.Forms.Padding(20, 24, 0, 0);
            this.labLogType.Name = "labLogType";
            this.labLogType.Size = new System.Drawing.Size(74, 21);
            this.labLogType.TabIndex = 0;
            this.labLogType.Text = "日志类型";
            // 
            // cbLogType
            // 
            this.cbLogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLogType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLogType.FormattingEnabled = true;
            this.cbLogType.Location = new System.Drawing.Point(99, 20);
            this.cbLogType.Margin = new System.Windows.Forms.Padding(5, 20, 0, 0);
            this.cbLogType.Name = "cbLogType";
            this.cbLogType.Size = new System.Drawing.Size(120, 29);
            this.cbLogType.TabIndex = 1;
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTime.Location = new System.Drawing.Point(239, 24);
            this.labTime.Margin = new System.Windows.Forms.Padding(20, 24, 0, 0);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(74, 21);
            this.labTime.TabIndex = 2;
            this.labTime.Text = "时间范围";
            // 
            // labTimeBetween
            // 
            this.labTimeBetween.AutoSize = true;
            this.labTimeBetween.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTimeBetween.Location = new System.Drawing.Point(471, 24);
            this.labTimeBetween.Margin = new System.Windows.Forms.Padding(3, 24, 0, 0);
            this.labTimeBetween.Name = "labTimeBetween";
            this.labTimeBetween.Size = new System.Drawing.Size(26, 21);
            this.labTimeBetween.TabIndex = 6;
            this.labTimeBetween.Text = "至";
            // 
            // txtEndTime
            // 
            this.txtEndTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtEndTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEndTime.Location = new System.Drawing.Point(500, 20);
            this.txtEndTime.Margin = new System.Windows.Forms.Padding(3, 20, 0, 0);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size(150, 29);
            this.txtEndTime.TabIndex = 20;
            this.txtEndTime.Click += new System.EventHandler(this.txtEndTime_Click);
            // 
            // txtBeginTime
            // 
            this.txtBeginTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtBeginTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBeginTime.Location = new System.Drawing.Point(318, 20);
            this.txtBeginTime.Margin = new System.Windows.Forms.Padding(5, 20, 0, 0);
            this.txtBeginTime.Name = "txtBeginTime";
            this.txtBeginTime.ReadOnly = true;
            this.txtBeginTime.Size = new System.Drawing.Size(150, 29);
            this.txtBeginTime.TabIndex = 19;
            this.txtBeginTime.Click += new System.EventHandler(this.txtBeginTime_Click);
            // 
            // dgvLog
            // 
            this.dgvLog.AllowUserToAddRows = false;
            this.dgvLog.AllowUserToDeleteRows = false;
            this.dgvLog.AllowUserToResizeColumns = false;
            this.dgvLog.AllowUserToResizeRows = false;
            this.dgvLog.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLog.ColumnHeadersHeight = 34;
            this.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLog.EnableHeadersVisualStyles = false;
            this.dgvLog.Location = new System.Drawing.Point(0, 70);
            this.dgvLog.MultiSelect = false;
            this.dgvLog.Name = "dgvLog";
            this.dgvLog.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLog.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLog.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvLog.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLog.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvLog.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvLog.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvLog.RowTemplate.Height = 34;
            this.dgvLog.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLog.Size = new System.Drawing.Size(1460, 510);
            this.dgvLog.TabIndex = 12;
            this.dgvLog.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLog_CellDoubleClick);
            this.dgvLog.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLog_CellMouseEnter);
            this.dgvLog.SelectionChanged += new System.EventHandler(this.dgvLog_SelectionChanged);
            // 
            // labPager
            // 
            this.labPager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labPager.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPager.Location = new System.Drawing.Point(510, 25);
            this.labPager.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.labPager.Name = "labPager";
            this.labPager.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labPager.Size = new System.Drawing.Size(950, 21);
            this.labPager.TabIndex = 21;
            this.labPager.Text = "共  42  条记录  2/3  页";
            // 
            // panTopZone
            // 
            this.panTopZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panTopZone.Controls.Add(this.labLogType);
            this.panTopZone.Controls.Add(this.cbLogType);
            this.panTopZone.Controls.Add(this.labTime);
            this.panTopZone.Controls.Add(this.txtBeginTime);
            this.panTopZone.Controls.Add(this.labTimeBetween);
            this.panTopZone.Controls.Add(this.txtEndTime);
            this.panTopZone.Controls.Add(this.ibSearch);
            this.panTopZone.Controls.Add(this.ibExport);
            this.panTopZone.Location = new System.Drawing.Point(0, 0);
            this.panTopZone.Name = "panTopZone";
            this.panTopZone.Size = new System.Drawing.Size(1460, 70);
            this.panTopZone.TabIndex = 23;
            // 
            // ibSearch
            // 
            this.ibSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibSearch.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibSearch.IconType = BatMes.Client.Controls.IconButtonType.Search;
            this.ibSearch.IconWidth = 50;
            this.ibSearch.IsActive = true;
            this.ibSearch.Location = new System.Drawing.Point(670, 20);
            this.ibSearch.Margin = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.ibSearch.Name = "ibSearch";
            this.ibSearch.Size = new System.Drawing.Size(120, 30);
            this.ibSearch.TabIndex = 18;
            this.ibSearch.TextValue = "查询";
            this.ibSearch.Click += new System.EventHandler(this.ibSearch_Click);
            // 
            // ibExport
            // 
            this.ibExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibExport.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibExport.IconType = BatMes.Client.Controls.IconButtonType.File;
            this.ibExport.IconWidth = 50;
            this.ibExport.IsActive = true;
            this.ibExport.Location = new System.Drawing.Point(800, 20);
            this.ibExport.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.ibExport.Name = "ibExport";
            this.ibExport.Size = new System.Drawing.Size(120, 30);
            this.ibExport.TabIndex = 17;
            this.ibExport.TextValue = "导出";
            this.ibExport.Click += new System.EventHandler(this.ibExport_Click);
            // 
            // panBottomZone
            // 
            this.panBottomZone.BackColor = System.Drawing.Color.Transparent;
            this.panBottomZone.Controls.Add(this.ibFirst);
            this.panBottomZone.Controls.Add(this.ibPrev);
            this.panBottomZone.Controls.Add(this.ibNext);
            this.panBottomZone.Controls.Add(this.ibLast);
            this.panBottomZone.Controls.Add(this.labPager);
            this.panBottomZone.Location = new System.Drawing.Point(0, 580);
            this.panBottomZone.Name = "panBottomZone";
            this.panBottomZone.Size = new System.Drawing.Size(1460, 50);
            this.panBottomZone.TabIndex = 24;
            // 
            // ibFirst
            // 
            this.ibFirst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibFirst.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibFirst.IconType = BatMes.Client.Controls.IconButtonType.First;
            this.ibFirst.IconWidth = 50;
            this.ibFirst.IsActive = true;
            this.ibFirst.Location = new System.Drawing.Point(0, 20);
            this.ibFirst.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.ibFirst.Name = "ibFirst";
            this.ibFirst.Size = new System.Drawing.Size(120, 30);
            this.ibFirst.TabIndex = 13;
            this.ibFirst.TextValue = "首页";
            this.ibFirst.Click += new System.EventHandler(this.ibFirst_Click);
            // 
            // ibPrev
            // 
            this.ibPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibPrev.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibPrev.IconType = BatMes.Client.Controls.IconButtonType.Prev;
            this.ibPrev.IconWidth = 40;
            this.ibPrev.IsActive = true;
            this.ibPrev.Location = new System.Drawing.Point(130, 20);
            this.ibPrev.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.ibPrev.Name = "ibPrev";
            this.ibPrev.Size = new System.Drawing.Size(120, 30);
            this.ibPrev.TabIndex = 14;
            this.ibPrev.TextValue = "上一页";
            this.ibPrev.Click += new System.EventHandler(this.ibPrev_Click);
            // 
            // ibNext
            // 
            this.ibNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibNext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibNext.IconType = BatMes.Client.Controls.IconButtonType.Next;
            this.ibNext.IconWidth = 40;
            this.ibNext.IsActive = true;
            this.ibNext.Location = new System.Drawing.Point(260, 20);
            this.ibNext.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.ibNext.Name = "ibNext";
            this.ibNext.Size = new System.Drawing.Size(120, 30);
            this.ibNext.TabIndex = 15;
            this.ibNext.TextValue = "下一页";
            this.ibNext.Click += new System.EventHandler(this.ibNext_Click);
            // 
            // ibLast
            // 
            this.ibLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibLast.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibLast.IconType = BatMes.Client.Controls.IconButtonType.Last;
            this.ibLast.IconWidth = 50;
            this.ibLast.IsActive = true;
            this.ibLast.Location = new System.Drawing.Point(390, 20);
            this.ibLast.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.ibLast.Name = "ibLast";
            this.ibLast.Size = new System.Drawing.Size(120, 30);
            this.ibLast.TabIndex = 16;
            this.ibLast.TextValue = "尾页";
            this.ibLast.Click += new System.EventHandler(this.ibLast_Click);
            // 
            // logDetail
            // 
            this.logDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logDetail.Cont = null;
            this.logDetail.CreateTime = null;
            this.logDetail.IsLog = true;
            this.logDetail.Location = new System.Drawing.Point(318, 100);
            this.logDetail.LogID = null;
            this.logDetail.Name = "logDetail";
            this.logDetail.Size = new System.Drawing.Size(1000, 630);
            this.logDetail.TabIndex = 25;
            this.logDetail.Title = null;
            this.logDetail.Type = null;
            this.logDetail.Visible = false;
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(0, 100);
            this.calendar.Name = "calendar";
            this.calendar.Size = new System.Drawing.Size(300, 310);
            this.calendar.TabIndex = 22;
            this.calendar.Visible = false;
            // 
            // SysLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logDetail);
            this.Controls.Add(this.panBottomZone);
            this.Controls.Add(this.calendar);
            this.Controls.Add(this.panTopZone);
            this.Controls.Add(this.dgvLog);
            this.Name = "SysLog";
            this.Size = new System.Drawing.Size(1460, 630);
            this.Load += new System.EventHandler(this.SysLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLog)).EndInit();
            this.panTopZone.ResumeLayout(false);
            this.panTopZone.PerformLayout();
            this.panBottomZone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labLogType;
        private System.Windows.Forms.ComboBox cbLogType;
        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label labTimeBetween;
        private System.Windows.Forms.DataGridView dgvLog;
        private IconButton ibExport;
        private IconButton ibSearch;
        private IconButton ibFirst;
        private IconButton ibPrev;
        private IconButton ibNext;
        private IconButton ibLast;
        private System.Windows.Forms.Label labPager;
        private Calendar calendar;
        private System.Windows.Forms.TextBox txtBeginTime;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.FlowLayoutPanel panTopZone;
        private System.Windows.Forms.FlowLayoutPanel panBottomZone;
        private SysLogDetail logDetail;
    }
}
