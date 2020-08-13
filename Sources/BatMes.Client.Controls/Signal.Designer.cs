namespace BatMes.Client.Controls
{
    partial class Signal
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
            this.labSignalText = new System.Windows.Forms.Label();
            this.labSignalStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labSignalText
            // 
            this.labSignalText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.labSignalText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labSignalText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSignalText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.labSignalText.Location = new System.Drawing.Point(0, 0);
            this.labSignalText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labSignalText.Name = "labSignalText";
            this.labSignalText.Size = new System.Drawing.Size(76, 40);
            this.labSignalText.TabIndex = 1;
            this.labSignalText.Text = "名称";
            this.labSignalText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSignalStatus
            // 
            this.labSignalStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labSignalStatus.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSignalStatus.Location = new System.Drawing.Point(75, 0);
            this.labSignalStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labSignalStatus.Name = "labSignalStatus";
            this.labSignalStatus.Size = new System.Drawing.Size(75, 40);
            this.labSignalStatus.TabIndex = 2;
            this.labSignalStatus.Text = "状态";
            this.labSignalStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Signal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.labSignalStatus);
            this.Controls.Add(this.labSignalText);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Signal";
            this.Size = new System.Drawing.Size(150, 40);
            this.Load += new System.EventHandler(this.Signal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labSignalText;
        private System.Windows.Forms.Label labSignalStatus;
    }
}
