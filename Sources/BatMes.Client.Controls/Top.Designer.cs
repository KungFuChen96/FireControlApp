namespace BatMes.Client.Controls
{
    partial class Top
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
            this.labTitle = new System.Windows.Forms.Label();
            this.labTopClose = new System.Windows.Forms.Label();
            this.labTopMinimize = new System.Windows.Forms.Label();
            this.labTopMain = new System.Windows.Forms.Label();
            this.labTopData = new System.Windows.Forms.Label();
            this.labTopLog = new System.Windows.Forms.Label();
            this.labTopEvent = new System.Windows.Forms.Label();
            this.labTopPar = new System.Windows.Forms.Label();
            this.labTopService = new System.Windows.Forms.Label();
            this.panNav = new System.Windows.Forms.FlowLayoutPanel();
            this.labTopOps = new System.Windows.Forms.Label();
            this.labTopCode = new System.Windows.Forms.Label();
            this.panTop = new System.Windows.Forms.Panel();
            this.panNav.SuspendLayout();
            this.panTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.ForeColor = System.Drawing.Color.White;
            this.labTitle.Location = new System.Drawing.Point(10, 10);
            this.labTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(62, 31);
            this.labTitle.TabIndex = 0;
            this.labTitle.Text = "标题";
            this.labTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.labTitle_MouseDoubleClick);
            this.labTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labTitle_MouseMove);
            // 
            // labTopClose
            // 
            this.labTopClose.AutoSize = true;
            this.labTopClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopClose.ForeColor = System.Drawing.Color.White;
            this.labTopClose.Location = new System.Drawing.Point(1460, 10);
            this.labTopClose.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labTopClose.Name = "labTopClose";
            this.labTopClose.Size = new System.Drawing.Size(29, 12);
            this.labTopClose.TabIndex = 1;
            this.labTopClose.Text = "关闭";
            this.labTopClose.Click += new System.EventHandler(this.labTopClose_Click);
            this.labTopClose.MouseLeave += new System.EventHandler(this.labTopClose_MouseLeave);
            this.labTopClose.MouseHover += new System.EventHandler(this.labTopClose_MouseHover);
            // 
            // labTopMinimize
            // 
            this.labTopMinimize.AutoSize = true;
            this.labTopMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopMinimize.ForeColor = System.Drawing.Color.White;
            this.labTopMinimize.Location = new System.Drawing.Point(1402, 10);
            this.labTopMinimize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labTopMinimize.Name = "labTopMinimize";
            this.labTopMinimize.Size = new System.Drawing.Size(41, 12);
            this.labTopMinimize.TabIndex = 2;
            this.labTopMinimize.Tag = "";
            this.labTopMinimize.Text = "最小化";
            this.labTopMinimize.Click += new System.EventHandler(this.labTopMinimize_Click);
            this.labTopMinimize.MouseLeave += new System.EventHandler(this.labTopMinimize_MouseLeave);
            this.labTopMinimize.MouseHover += new System.EventHandler(this.labTopMinimize_MouseHover);
            // 
            // labTopMain
            // 
            this.labTopMain.BackColor = System.Drawing.Color.White;
            this.labTopMain.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.labTopMain.Location = new System.Drawing.Point(20, 0);
            this.labTopMain.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.labTopMain.Name = "labTopMain";
            this.labTopMain.Size = new System.Drawing.Size(120, 40);
            this.labTopMain.TabIndex = 3;
            this.labTopMain.Text = "主界面";
            this.labTopMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopMain.Click += new System.EventHandler(this.labTopMain_Click);
            this.labTopMain.MouseLeave += new System.EventHandler(this.labTopMain_MouseLeave);
            this.labTopMain.MouseHover += new System.EventHandler(this.labTopMain_MouseHover);
            // 
            // labTopData
            // 
            this.labTopData.BackColor = System.Drawing.Color.Transparent;
            this.labTopData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopData.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.labTopData.Location = new System.Drawing.Point(140, 0);
            this.labTopData.Margin = new System.Windows.Forms.Padding(0);
            this.labTopData.Name = "labTopData";
            this.labTopData.Size = new System.Drawing.Size(120, 40);
            this.labTopData.TabIndex = 4;
            this.labTopData.Text = "数据查询";
            this.labTopData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopData.Visible = false;
            this.labTopData.Click += new System.EventHandler(this.labTopData_Click);
            this.labTopData.MouseLeave += new System.EventHandler(this.labTopData_MouseLeave);
            this.labTopData.MouseHover += new System.EventHandler(this.labTopData_MouseHover);
            // 
            // labTopLog
            // 
            this.labTopLog.BackColor = System.Drawing.Color.Transparent;
            this.labTopLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopLog.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.labTopLog.Location = new System.Drawing.Point(260, 0);
            this.labTopLog.Margin = new System.Windows.Forms.Padding(0);
            this.labTopLog.Name = "labTopLog";
            this.labTopLog.Size = new System.Drawing.Size(120, 40);
            this.labTopLog.TabIndex = 5;
            this.labTopLog.Text = "交互日志";
            this.labTopLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopLog.Visible = false;
            this.labTopLog.Click += new System.EventHandler(this.labTopLog_Click);
            this.labTopLog.MouseLeave += new System.EventHandler(this.labTopLog_MouseLeave);
            this.labTopLog.MouseHover += new System.EventHandler(this.labTopLog_MouseHover);
            // 
            // labTopEvent
            // 
            this.labTopEvent.BackColor = System.Drawing.Color.Transparent;
            this.labTopEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopEvent.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopEvent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.labTopEvent.Location = new System.Drawing.Point(380, 0);
            this.labTopEvent.Margin = new System.Windows.Forms.Padding(0);
            this.labTopEvent.Name = "labTopEvent";
            this.labTopEvent.Size = new System.Drawing.Size(120, 40);
            this.labTopEvent.TabIndex = 6;
            this.labTopEvent.Text = "系统事件";
            this.labTopEvent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopEvent.Visible = false;
            this.labTopEvent.Click += new System.EventHandler(this.labTopEvent_Click);
            this.labTopEvent.MouseLeave += new System.EventHandler(this.labTopEvent_MouseLeave);
            this.labTopEvent.MouseHover += new System.EventHandler(this.labTopEvent_MouseHover);
            // 
            // labTopPar
            // 
            this.labTopPar.BackColor = System.Drawing.Color.Transparent;
            this.labTopPar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopPar.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopPar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.labTopPar.Location = new System.Drawing.Point(500, 0);
            this.labTopPar.Margin = new System.Windows.Forms.Padding(0);
            this.labTopPar.Name = "labTopPar";
            this.labTopPar.Size = new System.Drawing.Size(120, 40);
            this.labTopPar.TabIndex = 7;
            this.labTopPar.Text = "系统参数";
            this.labTopPar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopPar.Visible = false;
            this.labTopPar.Click += new System.EventHandler(this.labTopPar_Click);
            this.labTopPar.MouseLeave += new System.EventHandler(this.labTopPar_MouseLeave);
            this.labTopPar.MouseHover += new System.EventHandler(this.labTopPar_MouseHover);
            // 
            // labTopService
            // 
            this.labTopService.BackColor = System.Drawing.Color.Transparent;
            this.labTopService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopService.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopService.ForeColor = System.Drawing.Color.White;
            this.labTopService.Location = new System.Drawing.Point(860, 0);
            this.labTopService.Margin = new System.Windows.Forms.Padding(0);
            this.labTopService.Name = "labTopService";
            this.labTopService.Size = new System.Drawing.Size(120, 40);
            this.labTopService.TabIndex = 9;
            this.labTopService.Text = "软件服务";
            this.labTopService.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopService.Visible = false;
            this.labTopService.Click += new System.EventHandler(this.labTopService_Click);
            this.labTopService.MouseLeave += new System.EventHandler(this.labTopService_MouseLeave);
            this.labTopService.MouseHover += new System.EventHandler(this.labTopService_MouseHover);
            // 
            // panNav
            // 
            this.panNav.Controls.Add(this.labTopMain);
            this.panNav.Controls.Add(this.labTopData);
            this.panNav.Controls.Add(this.labTopLog);
            this.panNav.Controls.Add(this.labTopEvent);
            this.panNav.Controls.Add(this.labTopPar);
            this.panNav.Controls.Add(this.labTopOps);
            this.panNav.Controls.Add(this.labTopCode);
            this.panNav.Controls.Add(this.labTopService);
            this.panNav.Location = new System.Drawing.Point(0, 60);
            this.panNav.Name = "panNav";
            this.panNav.Size = new System.Drawing.Size(1500, 40);
            this.panNav.TabIndex = 10;
            // 
            // labTopOps
            // 
            this.labTopOps.BackColor = System.Drawing.Color.Transparent;
            this.labTopOps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopOps.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopOps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.labTopOps.Location = new System.Drawing.Point(620, 0);
            this.labTopOps.Margin = new System.Windows.Forms.Padding(0);
            this.labTopOps.Name = "labTopOps";
            this.labTopOps.Size = new System.Drawing.Size(120, 40);
            this.labTopOps.TabIndex = 10;
            this.labTopOps.Text = "工序管理";
            this.labTopOps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopOps.Visible = false;
            this.labTopOps.Click += new System.EventHandler(this.labTopOps_Click);
            this.labTopOps.MouseLeave += new System.EventHandler(this.labTopOps_MouseLeave);
            this.labTopOps.MouseHover += new System.EventHandler(this.labTopOps_MouseHover);
            // 
            // labTopCode
            // 
            this.labTopCode.BackColor = System.Drawing.Color.Transparent;
            this.labTopCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopCode.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            this.labTopCode.Location = new System.Drawing.Point(740, 0);
            this.labTopCode.Margin = new System.Windows.Forms.Padding(0);
            this.labTopCode.Name = "labTopCode";
            this.labTopCode.Size = new System.Drawing.Size(120, 40);
            this.labTopCode.TabIndex = 11;
            this.labTopCode.Text = "结果代码";
            this.labTopCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopCode.Visible = false;
            this.labTopCode.Click += new System.EventHandler(this.labTopCode_Click);
            this.labTopCode.MouseLeave += new System.EventHandler(this.labTopCode_MouseLeave);
            this.labTopCode.MouseHover += new System.EventHandler(this.labTopCode_MouseHover);
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.labTitle);
            this.panTop.Controls.Add(this.panNav);
            this.panTop.Controls.Add(this.labTopMinimize);
            this.panTop.Controls.Add(this.labTopClose);
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1500, 100);
            this.panTop.TabIndex = 12;
            this.panTop.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panTop_MouseDoubleClick);
            this.panTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panTop_MouseMove);
            // 
            // Top
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.Controls.Add(this.panTop);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Top";
            this.Size = new System.Drawing.Size(1500, 100);
            this.Load += new System.EventHandler(this.Top_Load);
            this.panNav.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.Label labTopClose;
        private System.Windows.Forms.Label labTopMinimize;
        private System.Windows.Forms.Label labTopMain;
        private System.Windows.Forms.Label labTopData;
        private System.Windows.Forms.Label labTopLog;
        private System.Windows.Forms.Label labTopEvent;
        private System.Windows.Forms.Label labTopPar;
        private System.Windows.Forms.Label labTopService;
        private System.Windows.Forms.FlowLayoutPanel panNav;
        private System.Windows.Forms.Label labTopOps;
        private System.Windows.Forms.Label labTopCode;
        private System.Windows.Forms.Panel panTop;
    }
}
