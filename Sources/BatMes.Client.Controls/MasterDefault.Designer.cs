namespace BatMes.Client.Controls
{
    partial class MasterDefault
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
            this.bottom = new BatMes.Client.Controls.Bottom();
            this.panMain = new System.Windows.Forms.Panel();
            this.realLog = new BatMes.Client.Controls.RealLogColor();
            this.top = new BatMes.Client.Controls.Top();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // bottom
            // 
            this.bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.bottom.Location = new System.Drawing.Point(0, 720);
            this.bottom.Margin = new System.Windows.Forms.Padding(2);
            this.bottom.Name = "bottom";
            this.bottom.Size = new System.Drawing.Size(1600, 30);
            this.bottom.TabIndex = 16;
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.realLog);
            this.panMain.Location = new System.Drawing.Point(20, 120);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1460, 580);
            this.panMain.TabIndex = 17;
            // 
            // realLog
            // 
            this.realLog.Location = new System.Drawing.Point(0, 400);
            this.realLog.Name = "realLog";
            this.realLog.Size = new System.Drawing.Size(1460, 180);
            this.realLog.TabIndex = 0;
            // 
            // top
            // 
            this.top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.top.BatteryData = null;
            this.top.BRCode = null;
            this.top.CodeText = "结果代码";
            this.top.DataText = "数据查询";
            this.top.EventText = "系统事件";
            this.top.IsCode = false;
            this.top.IsData = true;
            this.top.IsEvent = true;
            this.top.IsLog = true;
            this.top.IsOps = false;
            this.top.IsPar = true;
            this.top.IsService = true;
            this.top.Location = new System.Drawing.Point(0, 0);
            this.top.LogText = "交互日志";
            this.top.MainText = "主界面";
            this.top.Margin = new System.Windows.Forms.Padding(0);
            this.top.Name = "top";
            this.top.Ops = null;
            this.top.OpsText = "工序管理";
            this.top.ParText = "系统参数";
            this.top.Service = null;
            this.top.ServiceText = "软件服务";
            this.top.Size = new System.Drawing.Size(1600, 100);
            this.top.SysEvent = null;
            this.top.SysLog = null;
            this.top.SysPar = null;
            this.top.TabIndex = 0;
            this.top.TopTitle = "";
            // 
            // MasterDefault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1500, 750);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.bottom);
            this.Controls.Add(this.top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MasterDefault";
            this.Text = "MasterDefault";
            this.Load += new System.EventHandler(this.MasterDefault_Load);
            this.panMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Bottom bottom;
        public System.Windows.Forms.Panel panMain;
        protected RealLogColor realLog;
        protected Top top;
    }
}