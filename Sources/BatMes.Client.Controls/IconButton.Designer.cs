namespace BatMes.Client.Controls
{
    partial class IconButton
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
            this.labIcon = new System.Windows.Forms.Label();
            this.labText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labIcon
            // 
            this.labIcon.ForeColor = System.Drawing.Color.White;
            this.labIcon.Location = new System.Drawing.Point(0, 0);
            this.labIcon.Name = "labIcon";
            this.labIcon.Size = new System.Drawing.Size(60, 50);
            this.labIcon.TabIndex = 0;
            this.labIcon.Text = "图标";
            this.labIcon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labIcon.Click += new System.EventHandler(this.labIconButtonIcon_Click);
            // 
            // labText
            // 
            this.labText.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labText.ForeColor = System.Drawing.Color.White;
            this.labText.Location = new System.Drawing.Point(60, 0);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(90, 50);
            this.labText.TabIndex = 1;
            this.labText.Text = "文本";
            this.labText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labText.Click += new System.EventHandler(this.labIconButtonText_Click);
            // 
            // IconButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.Controls.Add(this.labText);
            this.Controls.Add(this.labIcon);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "IconButton";
            this.Size = new System.Drawing.Size(150, 50);
            this.Load += new System.EventHandler(this.IconButton_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labIcon;
        private System.Windows.Forms.Label labText;
    }
}
