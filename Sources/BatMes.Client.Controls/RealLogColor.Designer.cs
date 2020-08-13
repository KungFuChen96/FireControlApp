namespace BatMes.Client.Controls
{
    partial class RealLogColor
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
            this.panLog = new System.Windows.Forms.Panel();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.panLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // panLog
            // 
            this.panLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panLog.Controls.Add(this.rtbLog);
            this.panLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panLog.Location = new System.Drawing.Point(0, 0);
            this.panLog.Name = "panLog";
            this.panLog.Size = new System.Drawing.Size(1460, 150);
            this.panLog.TabIndex = 13;
            // 
            // rtbLog
            // 
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbLog.Size = new System.Drawing.Size(1460, 150);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            this.rtbLog.TextChanged += new System.EventHandler(this.rtbLog_TextChanged);
            // 
            // RealLogColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panLog);
            this.Name = "RealLogColor";
            this.Size = new System.Drawing.Size(1460, 150);
            this.Load += new System.EventHandler(this.RealLog_Load);
            this.panLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panLog;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}
