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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Default));
            this.statusIndicator = new FireControlApp.StatusIndicator();
            this.gBox_FcS = new FireControlApp.MyGroupBox(this.components);
            this.tabLine_FcS1 = new MyControls.MyTableLayoutPanel();
            this.tabLine_FcS2 = new MyControls.MyTableLayoutPanel();
            this.gBox_Fc = new FireControlApp.MyGroupBox(this.components);
            this.tabLine_Fc = new MyControls.MyTableLayoutPanel();
            this.gBox_Hot = new FireControlApp.MyGroupBox(this.components);
            this.tabLine_Hot2 = new MyControls.MyTableLayoutPanel();
            this.tabLine_Hot1 = new MyControls.MyTableLayoutPanel();
            this.panMain.SuspendLayout();
            this.gBox_FcS.SuspendLayout();
            this.gBox_Fc.SuspendLayout();
            this.gBox_Hot.SuspendLayout();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.gBox_Hot);
            this.panMain.Controls.Add(this.gBox_Fc);
            this.panMain.Controls.Add(this.gBox_FcS);
            this.panMain.Controls.Add(this.statusIndicator);
            this.panMain.Location = new System.Drawing.Point(0, 79);
            this.panMain.Margin = new System.Windows.Forms.Padding(4);
            this.panMain.Size = new System.Drawing.Size(1593, 758);
            this.panMain.Controls.SetChildIndex(this.statusIndicator, 0);
            this.panMain.Controls.SetChildIndex(this.gBox_FcS, 0);
            this.panMain.Controls.SetChildIndex(this.gBox_Fc, 0);
            this.panMain.Controls.SetChildIndex(this.gBox_Hot, 0);
            this.panMain.Controls.SetChildIndex(this.realLog, 0);
            // 
            // realLog
            // 
            this.realLog.Location = new System.Drawing.Point(0, 713);
            this.realLog.Margin = new System.Windows.Forms.Padding(5);
            this.realLog.Size = new System.Drawing.Size(1560, 52);
            // 
            // top
            // 
            this.top.Font = new System.Drawing.Font("宋体", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.top.IsData = false;
            this.top.Size = new System.Drawing.Size(1920, 75);
            // 
            // statusIndicator
            // 
            this.statusIndicator.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusIndicator.Location = new System.Drawing.Point(8, 3);
            this.statusIndicator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.statusIndicator.Name = "statusIndicator";
            this.statusIndicator.Size = new System.Drawing.Size(309, 26);
            this.statusIndicator.TabIndex = 3;
            // 
            // gBox_FcS
            // 
            this.gBox_FcS.BorderColor = System.Drawing.Color.Black;
            this.gBox_FcS.Controls.Add(this.tabLine_FcS1);
            this.gBox_FcS.Controls.Add(this.tabLine_FcS2);
            this.gBox_FcS.Location = new System.Drawing.Point(8, 22);
            this.gBox_FcS.Name = "gBox_FcS";
            this.gBox_FcS.Size = new System.Drawing.Size(1440, 297);
            this.gBox_FcS.TabIndex = 26;
            this.gBox_FcS.TabStop = false;
            this.gBox_FcS.Text = "常温静置架";
            // 
            // tabLine_FcS1
            // 
            this.tabLine_FcS1.ColumnCount = 9;
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tabLine_FcS1.Location = new System.Drawing.Point(5, 19);
            this.tabLine_FcS1.M_BaseColor = System.Drawing.Color.White;
            this.tabLine_FcS1.M_BorderColor = System.Drawing.Color.Gainsboro;
            this.tabLine_FcS1.M_BorderWidth = 1;
            this.tabLine_FcS1.M_DrawBackground = true;
            this.tabLine_FcS1.M_DrawBorder = false;
            this.tabLine_FcS1.M_DrawInnerBorder = false;
            this.tabLine_FcS1.M_DrawLinear = false;
            this.tabLine_FcS1.M_InnerBorderColor = System.Drawing.Color.Empty;
            this.tabLine_FcS1.M_InnerBorderWidth = 1;
            this.tabLine_FcS1.M_Linear_EndColor = System.Drawing.Color.Red;
            this.tabLine_FcS1.M_Linear_StartColor = System.Drawing.Color.Orange;
            this.tabLine_FcS1.M_LinearModel = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabLine_FcS1.M_Radius_LeftBottom = 30;
            this.tabLine_FcS1.M_Radius_LeftTop = 30;
            this.tabLine_FcS1.M_Radius_RightBottom = 30;
            this.tabLine_FcS1.M_Radius_RightTop = 30;
            this.tabLine_FcS1.M_Title_BaseColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_FcS1.M_Title_BorderColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_FcS1.M_Title_Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabLine_FcS1.M_Title_Radius = 4;
            this.tabLine_FcS1.M_Title_Text = "";
            this.tabLine_FcS1.Margin = new System.Windows.Forms.Padding(2);
            this.tabLine_FcS1.Name = "tabLine_FcS1";
            this.tabLine_FcS1.RowCount = 7;
            this.tabLine_FcS1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28816F));
            this.tabLine_FcS1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tabLine_FcS1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tabLine_FcS1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tabLine_FcS1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tabLine_FcS1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tabLine_FcS1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28531F));
            this.tabLine_FcS1.Size = new System.Drawing.Size(1419, 140);
            this.tabLine_FcS1.TabIndex = 10;
            // 
            // tabLine_FcS2
            // 
            this.tabLine_FcS2.ColumnCount = 36;
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.777779F));
            this.tabLine_FcS2.Location = new System.Drawing.Point(5, 179);
            this.tabLine_FcS2.M_BaseColor = System.Drawing.Color.White;
            this.tabLine_FcS2.M_BorderColor = System.Drawing.Color.Gainsboro;
            this.tabLine_FcS2.M_BorderWidth = 1;
            this.tabLine_FcS2.M_DrawBackground = true;
            this.tabLine_FcS2.M_DrawBorder = false;
            this.tabLine_FcS2.M_DrawInnerBorder = false;
            this.tabLine_FcS2.M_DrawLinear = false;
            this.tabLine_FcS2.M_InnerBorderColor = System.Drawing.Color.Empty;
            this.tabLine_FcS2.M_InnerBorderWidth = 1;
            this.tabLine_FcS2.M_Linear_EndColor = System.Drawing.Color.Red;
            this.tabLine_FcS2.M_Linear_StartColor = System.Drawing.Color.Orange;
            this.tabLine_FcS2.M_LinearModel = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabLine_FcS2.M_Radius_LeftBottom = 30;
            this.tabLine_FcS2.M_Radius_LeftTop = 30;
            this.tabLine_FcS2.M_Radius_RightBottom = 30;
            this.tabLine_FcS2.M_Radius_RightTop = 30;
            this.tabLine_FcS2.M_Title_BaseColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_FcS2.M_Title_BorderColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_FcS2.M_Title_Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabLine_FcS2.M_Title_Radius = 4;
            this.tabLine_FcS2.M_Title_Text = "";
            this.tabLine_FcS2.Margin = new System.Windows.Forms.Padding(2);
            this.tabLine_FcS2.Name = "tabLine_FcS2";
            this.tabLine_FcS2.RowCount = 7;
            this.tabLine_FcS2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_FcS2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_FcS2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_FcS2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_FcS2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_FcS2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_FcS2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_FcS2.Size = new System.Drawing.Size(1419, 113);
            this.tabLine_FcS2.TabIndex = 9;
            // 
            // gBox_Fc
            // 
            this.gBox_Fc.BorderColor = System.Drawing.Color.Black;
            this.gBox_Fc.Controls.Add(this.tabLine_Fc);
            this.gBox_Fc.Location = new System.Drawing.Point(8, 334);
            this.gBox_Fc.Name = "gBox_Fc";
            this.gBox_Fc.Size = new System.Drawing.Size(1440, 143);
            this.gBox_Fc.TabIndex = 11;
            this.gBox_Fc.TabStop = false;
            this.gBox_Fc.Text = "分容压床";
            // 
            // tabLine_Fc
            // 
            this.tabLine_Fc.ColumnCount = 7;
            this.tabLine_Fc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_Fc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_Fc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_Fc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_Fc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_Fc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_Fc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tabLine_Fc.Location = new System.Drawing.Point(5, 19);
            this.tabLine_Fc.M_BaseColor = System.Drawing.Color.White;
            this.tabLine_Fc.M_BorderColor = System.Drawing.Color.Gainsboro;
            this.tabLine_Fc.M_BorderWidth = 1;
            this.tabLine_Fc.M_DrawBackground = true;
            this.tabLine_Fc.M_DrawBorder = false;
            this.tabLine_Fc.M_DrawInnerBorder = false;
            this.tabLine_Fc.M_DrawLinear = false;
            this.tabLine_Fc.M_InnerBorderColor = System.Drawing.Color.Empty;
            this.tabLine_Fc.M_InnerBorderWidth = 1;
            this.tabLine_Fc.M_Linear_EndColor = System.Drawing.Color.Red;
            this.tabLine_Fc.M_Linear_StartColor = System.Drawing.Color.Orange;
            this.tabLine_Fc.M_LinearModel = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabLine_Fc.M_Radius_LeftBottom = 30;
            this.tabLine_Fc.M_Radius_LeftTop = 30;
            this.tabLine_Fc.M_Radius_RightBottom = 30;
            this.tabLine_Fc.M_Radius_RightTop = 30;
            this.tabLine_Fc.M_Title_BaseColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_Fc.M_Title_BorderColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_Fc.M_Title_Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabLine_Fc.M_Title_Radius = 4;
            this.tabLine_Fc.M_Title_Text = "";
            this.tabLine_Fc.Margin = new System.Windows.Forms.Padding(2);
            this.tabLine_Fc.Name = "tabLine_Fc";
            this.tabLine_Fc.RowCount = 4;
            this.tabLine_Fc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tabLine_Fc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tabLine_Fc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tabLine_Fc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tabLine_Fc.Size = new System.Drawing.Size(1419, 112);
            this.tabLine_Fc.TabIndex = 4;
            // 
            // gBox_Hot
            // 
            this.gBox_Hot.BorderColor = System.Drawing.Color.Black;
            this.gBox_Hot.Controls.Add(this.tabLine_Hot2);
            this.gBox_Hot.Controls.Add(this.tabLine_Hot1);
            this.gBox_Hot.Location = new System.Drawing.Point(8, 483);
            this.gBox_Hot.Name = "gBox_Hot";
            this.gBox_Hot.Size = new System.Drawing.Size(1440, 294);
            this.gBox_Hot.TabIndex = 27;
            this.gBox_Hot.TabStop = false;
            this.gBox_Hot.Text = "高温静置架";
            // 
            // tabLine_Hot2
            // 
            this.tabLine_Hot2.ColumnCount = 21;
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot2.Location = new System.Drawing.Point(5, 146);
            this.tabLine_Hot2.M_BaseColor = System.Drawing.Color.White;
            this.tabLine_Hot2.M_BorderColor = System.Drawing.Color.Gainsboro;
            this.tabLine_Hot2.M_BorderWidth = 1;
            this.tabLine_Hot2.M_DrawBackground = true;
            this.tabLine_Hot2.M_DrawBorder = false;
            this.tabLine_Hot2.M_DrawInnerBorder = false;
            this.tabLine_Hot2.M_DrawLinear = false;
            this.tabLine_Hot2.M_InnerBorderColor = System.Drawing.Color.Empty;
            this.tabLine_Hot2.M_InnerBorderWidth = 1;
            this.tabLine_Hot2.M_Linear_EndColor = System.Drawing.Color.Red;
            this.tabLine_Hot2.M_Linear_StartColor = System.Drawing.Color.Orange;
            this.tabLine_Hot2.M_LinearModel = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabLine_Hot2.M_Radius_LeftBottom = 30;
            this.tabLine_Hot2.M_Radius_LeftTop = 30;
            this.tabLine_Hot2.M_Radius_RightBottom = 30;
            this.tabLine_Hot2.M_Radius_RightTop = 30;
            this.tabLine_Hot2.M_Title_BaseColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_Hot2.M_Title_BorderColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_Hot2.M_Title_Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabLine_Hot2.M_Title_Radius = 4;
            this.tabLine_Hot2.M_Title_Text = "";
            this.tabLine_Hot2.Margin = new System.Windows.Forms.Padding(2);
            this.tabLine_Hot2.Name = "tabLine_Hot2";
            this.tabLine_Hot2.RowCount = 6;
            this.tabLine_Hot2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot2.Size = new System.Drawing.Size(1419, 128);
            this.tabLine_Hot2.TabIndex = 11;
            // 
            // tabLine_Hot1
            // 
            this.tabLine_Hot1.ColumnCount = 21;
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.761906F));
            this.tabLine_Hot1.Location = new System.Drawing.Point(5, 19);
            this.tabLine_Hot1.M_BaseColor = System.Drawing.Color.White;
            this.tabLine_Hot1.M_BorderColor = System.Drawing.Color.Gainsboro;
            this.tabLine_Hot1.M_BorderWidth = 1;
            this.tabLine_Hot1.M_DrawBackground = true;
            this.tabLine_Hot1.M_DrawBorder = false;
            this.tabLine_Hot1.M_DrawInnerBorder = false;
            this.tabLine_Hot1.M_DrawLinear = false;
            this.tabLine_Hot1.M_InnerBorderColor = System.Drawing.Color.Empty;
            this.tabLine_Hot1.M_InnerBorderWidth = 1;
            this.tabLine_Hot1.M_Linear_EndColor = System.Drawing.Color.Red;
            this.tabLine_Hot1.M_Linear_StartColor = System.Drawing.Color.Orange;
            this.tabLine_Hot1.M_LinearModel = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.tabLine_Hot1.M_Radius_LeftBottom = 30;
            this.tabLine_Hot1.M_Radius_LeftTop = 30;
            this.tabLine_Hot1.M_Radius_RightBottom = 30;
            this.tabLine_Hot1.M_Radius_RightTop = 30;
            this.tabLine_Hot1.M_Title_BaseColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_Hot1.M_Title_BorderColor = System.Drawing.Color.WhiteSmoke;
            this.tabLine_Hot1.M_Title_Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tabLine_Hot1.M_Title_Radius = 4;
            this.tabLine_Hot1.M_Title_Text = "";
            this.tabLine_Hot1.Margin = new System.Windows.Forms.Padding(2);
            this.tabLine_Hot1.Name = "tabLine_Hot1";
            this.tabLine_Hot1.RowCount = 6;
            this.tabLine_Hot1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tabLine_Hot1.Size = new System.Drawing.Size(1419, 114);
            this.tabLine_Hot1.TabIndex = 10;
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
            this.gBox_FcS.ResumeLayout(false);
            this.gBox_Fc.ResumeLayout(false);
            this.gBox_Hot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private StatusIndicator statusIndicator;
        private MyGroupBox gBox_FcS;
        private MyControls.MyTableLayoutPanel tabLine_FcS1;
        private MyControls.MyTableLayoutPanel tabLine_FcS2;
        private MyGroupBox gBox_Fc;
        private MyControls.MyTableLayoutPanel tabLine_Fc;
        private MyGroupBox gBox_Hot;
        private MyControls.MyTableLayoutPanel tabLine_Hot2;
        private MyControls.MyTableLayoutPanel tabLine_Hot1;
    }
}