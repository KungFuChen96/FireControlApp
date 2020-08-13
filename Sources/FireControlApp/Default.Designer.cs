namespace FireControlApp
{
    partial class Default
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Default));
            this.tabLine_1 = new MyControls.MyTableLayoutPanel();
            this.statusIndicator = new FireControlApp.StatusIndicator();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.statusIndicator);
            this.panMain.Controls.Add(this.tabLine_1);
            this.panMain.Margin = new System.Windows.Forms.Padding(4);
            this.panMain.Size = new System.Drawing.Size(1560, 690);
            this.panMain.Controls.SetChildIndex(this.realLog, 0);
            this.panMain.Controls.SetChildIndex(this.tabLine_1, 0);
            this.panMain.Controls.SetChildIndex(this.statusIndicator, 0);
            // 
            // realLog
            // 
            this.realLog.Location = new System.Drawing.Point(0, 380);
            this.realLog.Margin = new System.Windows.Forms.Padding(5);
            this.realLog.Size = new System.Drawing.Size(1560, 296);
            // 
            // top
            // 
            this.top.IsData = false;
            this.top.Size = new System.Drawing.Size(1920, 100);
            // 
            // tabLine_1
            // 
            this.tabLine_1.ColumnCount = 8;
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tabLine_1.Location = new System.Drawing.Point(2, 52);
            this.tabLine_1.M_BaseColor = System.Drawing.Color.White;
            this.tabLine_1.M_BorderColor = System.Drawing.Color.Gainsboro;
            this.tabLine_1.M_BorderWidth = 1;
            this.tabLine_1.M_DrawBackground = true;
            this.tabLine_1.M_DrawBorder = false;
            this.tabLine_1.M_DrawInnerBorder = false;
            this.tabLine_1.M_DrawLinear = false;
            this.tabLine_1.M_InnerBorderColor = System.Drawing.Color.Empty;
            this.tabLine_1.M_InnerBorderWidth = 1;
            this.tabLine_1.M_Linear_EndColor = System.Drawing.Color.Red;
            this.tabLine_1.M_Linear_StartColor = System.Drawing.Color.Orange;
            this.tabLine_1.M_LinearModel = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabLine_1.M_Radius_LeftBottom = 30;
            this.tabLine_1.M_Radius_LeftTop = 30;
            this.tabLine_1.M_Radius_RightBottom = 30;
            this.tabLine_1.M_Radius_RightTop = 30;
            this.tabLine_1.M_Title_BaseColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_1.M_Title_BorderColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_1.M_Title_Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabLine_1.M_Title_Radius = 4;
            this.tabLine_1.M_Title_Text = "";
            this.tabLine_1.Margin = new System.Windows.Forms.Padding(2);
            this.tabLine_1.Name = "tabLine_1";
            this.tabLine_1.RowCount = 5;
            this.tabLine_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabLine_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabLine_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabLine_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabLine_1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tabLine_1.Size = new System.Drawing.Size(1425, 310);
            this.tabLine_1.TabIndex = 2;
            // 
            // statusIndicator
            // 
            this.statusIndicator.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusIndicator.Location = new System.Drawing.Point(2, 3);
            this.statusIndicator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.statusIndicator.Name = "statusIndicator";
            this.statusIndicator.Size = new System.Drawing.Size(313, 35);
            this.statusIndicator.TabIndex = 3;
            // 
            // Default
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1455, 800);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Default";
            this.Text = "BatMes TrayOcv";
            this.Load += new System.EventHandler(this.Default_Load);
            this.Controls.SetChildIndex(this.top, 0);
            this.Controls.SetChildIndex(this.panMain, 0);
            this.panMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MyControls.MyTableLayoutPanel tabLine_1;
        private StatusIndicator statusIndicator;
    }
}