namespace BatMes.Client.Controls
{
    partial class Bottom
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
            this.labStatus = new System.Windows.Forms.Label();
            this.labFixed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labStatus
            // 
            this.labStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labStatus.ForeColor = System.Drawing.Color.White;
            this.labStatus.Location = new System.Drawing.Point(10, 5);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(740, 20);
            this.labStatus.TabIndex = 0;
            this.labStatus.Text = "状态";
            // 
            // labFixed
            // 
            this.labFixed.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFixed.ForeColor = System.Drawing.Color.White;
            this.labFixed.Location = new System.Drawing.Point(750, 5);
            this.labFixed.Name = "labFixed";
            this.labFixed.Size = new System.Drawing.Size(740, 20);
            this.labFixed.TabIndex = 1;
            this.labFixed.Text = "技术支持 batmes.com        推荐分辨率 1600 x 900";
            this.labFixed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Bottom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.Controls.Add(this.labFixed);
            this.Controls.Add(this.labStatus);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Bottom";
            this.Size = new System.Drawing.Size(1500, 30);
            this.Load += new System.EventHandler(this.Bottom_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label labFixed;
    }
}
