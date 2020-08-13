namespace BatMes.Client.Controls
{
    partial class Calendar
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
            this.panMain = new System.Windows.Forms.Panel();
            this.panBody = new System.Windows.Forms.Panel();
            this.panBottom = new System.Windows.Forms.Panel();
            this.cbBottomMinute = new System.Windows.Forms.ComboBox();
            this.cbBottomHour = new System.Windows.Forms.ComboBox();
            this.labBottomNow = new System.Windows.Forms.Label();
            this.labBottomConfirm = new System.Windows.Forms.Label();
            this.labBottomClear = new System.Windows.Forms.Label();
            this.panTop = new System.Windows.Forms.Panel();
            this.labTopMonth = new System.Windows.Forms.Label();
            this.labTopYearNext = new System.Windows.Forms.Label();
            this.labTopYear = new System.Windows.Forms.Label();
            this.labTopMonthNext = new System.Windows.Forms.Label();
            this.labTopMonthPrev = new System.Windows.Forms.Label();
            this.labTopYearPrev = new System.Windows.Forms.Label();
            this.labMon = new System.Windows.Forms.Label();
            this.labTue = new System.Windows.Forms.Label();
            this.labSat = new System.Windows.Forms.Label();
            this.labWed = new System.Windows.Forms.Label();
            this.labThur = new System.Windows.Forms.Label();
            this.labFri = new System.Windows.Forms.Label();
            this.labSun = new System.Windows.Forms.Label();
            this.panMain.SuspendLayout();
            this.panBottom.SuspendLayout();
            this.panTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panMain
            // 
            this.panMain.BackColor = System.Drawing.Color.White;
            this.panMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMain.Controls.Add(this.panBody);
            this.panMain.Controls.Add(this.panBottom);
            this.panMain.Controls.Add(this.panTop);
            this.panMain.Controls.Add(this.labMon);
            this.panMain.Controls.Add(this.labTue);
            this.panMain.Controls.Add(this.labSat);
            this.panMain.Controls.Add(this.labWed);
            this.panMain.Controls.Add(this.labThur);
            this.panMain.Controls.Add(this.labFri);
            this.panMain.Controls.Add(this.labSun);
            this.panMain.Location = new System.Drawing.Point(0, 0);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(300, 310);
            this.panMain.TabIndex = 1;
            // 
            // panBody
            // 
            this.panBody.Location = new System.Drawing.Point(10, 85);
            this.panBody.Name = "panBody";
            this.panBody.Size = new System.Drawing.Size(280, 180);
            this.panBody.TabIndex = 9;
            // 
            // panBottom
            // 
            this.panBottom.BackColor = System.Drawing.Color.Transparent;
            this.panBottom.Controls.Add(this.cbBottomMinute);
            this.panBottom.Controls.Add(this.cbBottomHour);
            this.panBottom.Controls.Add(this.labBottomNow);
            this.panBottom.Controls.Add(this.labBottomConfirm);
            this.panBottom.Controls.Add(this.labBottomClear);
            this.panBottom.Location = new System.Drawing.Point(0, 270);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(300, 40);
            this.panBottom.TabIndex = 8;
            // 
            // cbBottomMinute
            // 
            this.cbBottomMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBottomMinute.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBottomMinute.FormattingEnabled = true;
            this.cbBottomMinute.Location = new System.Drawing.Point(75, 0);
            this.cbBottomMinute.Name = "cbBottomMinute";
            this.cbBottomMinute.Size = new System.Drawing.Size(60, 29);
            this.cbBottomMinute.TabIndex = 13;
            // 
            // cbBottomHour
            // 
            this.cbBottomHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBottomHour.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBottomHour.FormattingEnabled = true;
            this.cbBottomHour.Location = new System.Drawing.Point(10, 0);
            this.cbBottomHour.Name = "cbBottomHour";
            this.cbBottomHour.Size = new System.Drawing.Size(60, 29);
            this.cbBottomHour.TabIndex = 12;
            // 
            // labBottomNow
            // 
            this.labBottomNow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labBottomNow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labBottomNow.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labBottomNow.Location = new System.Drawing.Point(191, 0);
            this.labBottomNow.Name = "labBottomNow";
            this.labBottomNow.Size = new System.Drawing.Size(50, 30);
            this.labBottomNow.TabIndex = 11;
            this.labBottomNow.Text = "现在";
            this.labBottomNow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labBottomNow.Click += new System.EventHandler(this.labBottomNow_Click);
            // 
            // labBottomConfirm
            // 
            this.labBottomConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.labBottomConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labBottomConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labBottomConfirm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labBottomConfirm.ForeColor = System.Drawing.Color.White;
            this.labBottomConfirm.Location = new System.Drawing.Point(240, 0);
            this.labBottomConfirm.Name = "labBottomConfirm";
            this.labBottomConfirm.Size = new System.Drawing.Size(50, 30);
            this.labBottomConfirm.TabIndex = 10;
            this.labBottomConfirm.Text = "确定";
            this.labBottomConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labBottomConfirm.Click += new System.EventHandler(this.labBottomConfirm_Click);
            // 
            // labBottomClear
            // 
            this.labBottomClear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labBottomClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labBottomClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labBottomClear.Location = new System.Drawing.Point(142, 0);
            this.labBottomClear.Name = "labBottomClear";
            this.labBottomClear.Size = new System.Drawing.Size(50, 30);
            this.labBottomClear.TabIndex = 9;
            this.labBottomClear.Text = "清空";
            this.labBottomClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labBottomClear.Click += new System.EventHandler(this.labBottomClear_Click);
            // 
            // panTop
            // 
            this.panTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.panTop.Controls.Add(this.labTopMonth);
            this.panTop.Controls.Add(this.labTopYearNext);
            this.panTop.Controls.Add(this.labTopYear);
            this.panTop.Controls.Add(this.labTopMonthNext);
            this.panTop.Controls.Add(this.labTopMonthPrev);
            this.panTop.Controls.Add(this.labTopYearPrev);
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(300, 40);
            this.panTop.TabIndex = 7;
            // 
            // labTopMonth
            // 
            this.labTopMonth.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopMonth.ForeColor = System.Drawing.Color.White;
            this.labTopMonth.Location = new System.Drawing.Point(160, 5);
            this.labTopMonth.Name = "labTopMonth";
            this.labTopMonth.Size = new System.Drawing.Size(50, 30);
            this.labTopMonth.TabIndex = 14;
            this.labTopMonth.Text = "12月";
            this.labTopMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labTopYearNext
            // 
            this.labTopYearNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopYearNext.ForeColor = System.Drawing.Color.White;
            this.labTopYearNext.Location = new System.Drawing.Point(260, 5);
            this.labTopYearNext.Name = "labTopYearNext";
            this.labTopYearNext.Size = new System.Drawing.Size(30, 30);
            this.labTopYearNext.TabIndex = 12;
            this.labTopYearNext.Text = "年+";
            this.labTopYearNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopYearNext.Click += new System.EventHandler(this.labTopYearNext_Click);
            // 
            // labTopYear
            // 
            this.labTopYear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTopYear.ForeColor = System.Drawing.Color.White;
            this.labTopYear.Location = new System.Drawing.Point(90, 5);
            this.labTopYear.Name = "labTopYear";
            this.labTopYear.Size = new System.Drawing.Size(70, 30);
            this.labTopYear.TabIndex = 13;
            this.labTopYear.Text = "2019年";
            this.labTopYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labTopMonthNext
            // 
            this.labTopMonthNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopMonthNext.ForeColor = System.Drawing.Color.White;
            this.labTopMonthNext.Location = new System.Drawing.Point(230, 5);
            this.labTopMonthNext.Name = "labTopMonthNext";
            this.labTopMonthNext.Size = new System.Drawing.Size(30, 30);
            this.labTopMonthNext.TabIndex = 11;
            this.labTopMonthNext.Text = "月+";
            this.labTopMonthNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopMonthNext.Click += new System.EventHandler(this.labTopMonthNext_Click);
            // 
            // labTopMonthPrev
            // 
            this.labTopMonthPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopMonthPrev.ForeColor = System.Drawing.Color.White;
            this.labTopMonthPrev.Location = new System.Drawing.Point(40, 5);
            this.labTopMonthPrev.Name = "labTopMonthPrev";
            this.labTopMonthPrev.Size = new System.Drawing.Size(30, 30);
            this.labTopMonthPrev.TabIndex = 10;
            this.labTopMonthPrev.Text = "月-";
            this.labTopMonthPrev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopMonthPrev.Click += new System.EventHandler(this.labTopMonthPrev_Click);
            // 
            // labTopYearPrev
            // 
            this.labTopYearPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labTopYearPrev.ForeColor = System.Drawing.Color.White;
            this.labTopYearPrev.Location = new System.Drawing.Point(10, 5);
            this.labTopYearPrev.Name = "labTopYearPrev";
            this.labTopYearPrev.Size = new System.Drawing.Size(30, 30);
            this.labTopYearPrev.TabIndex = 9;
            this.labTopYearPrev.Text = "年-";
            this.labTopYearPrev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labTopYearPrev.Click += new System.EventHandler(this.labTopYearPrev_Click);
            // 
            // labMon
            // 
            this.labMon.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labMon.Location = new System.Drawing.Point(50, 50);
            this.labMon.Name = "labMon";
            this.labMon.Size = new System.Drawing.Size(40, 30);
            this.labMon.TabIndex = 6;
            this.labMon.Text = "一";
            this.labMon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labTue
            // 
            this.labTue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTue.Location = new System.Drawing.Point(90, 50);
            this.labTue.Name = "labTue";
            this.labTue.Size = new System.Drawing.Size(40, 30);
            this.labTue.TabIndex = 5;
            this.labTue.Text = "二";
            this.labTue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSat
            // 
            this.labSat.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSat.Location = new System.Drawing.Point(250, 50);
            this.labSat.Name = "labSat";
            this.labSat.Size = new System.Drawing.Size(40, 30);
            this.labSat.TabIndex = 1;
            this.labSat.Text = "六";
            this.labSat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labWed
            // 
            this.labWed.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labWed.Location = new System.Drawing.Point(130, 50);
            this.labWed.Name = "labWed";
            this.labWed.Size = new System.Drawing.Size(40, 30);
            this.labWed.TabIndex = 4;
            this.labWed.Text = "三";
            this.labWed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labThur
            // 
            this.labThur.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labThur.Location = new System.Drawing.Point(170, 50);
            this.labThur.Name = "labThur";
            this.labThur.Size = new System.Drawing.Size(40, 30);
            this.labThur.TabIndex = 3;
            this.labThur.Text = "四";
            this.labThur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labFri
            // 
            this.labFri.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFri.Location = new System.Drawing.Point(210, 50);
            this.labFri.Name = "labFri";
            this.labFri.Size = new System.Drawing.Size(40, 30);
            this.labFri.TabIndex = 2;
            this.labFri.Text = "五";
            this.labFri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labSun
            // 
            this.labSun.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSun.Location = new System.Drawing.Point(10, 50);
            this.labSun.Name = "labSun";
            this.labSun.Size = new System.Drawing.Size(40, 30);
            this.labSun.TabIndex = 0;
            this.labSun.Text = "日";
            this.labSun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Calendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panMain);
            this.Name = "Calendar";
            this.Size = new System.Drawing.Size(300, 310);
            this.Load += new System.EventHandler(this.Calendar_Load);
            this.panMain.ResumeLayout(false);
            this.panBottom.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Label labMon;
        private System.Windows.Forms.Label labTue;
        private System.Windows.Forms.Label labSat;
        private System.Windows.Forms.Label labWed;
        private System.Windows.Forms.Label labThur;
        private System.Windows.Forms.Label labFri;
        private System.Windows.Forms.Label labSun;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Label labTopYear;
        private System.Windows.Forms.Label labTopYearPrev;
        private System.Windows.Forms.Label labTopMonthPrev;
        private System.Windows.Forms.Label labTopYearNext;
        private System.Windows.Forms.Label labTopMonthNext;
        private System.Windows.Forms.Label labTopMonth;
        private System.Windows.Forms.Label labBottomNow;
        private System.Windows.Forms.Label labBottomConfirm;
        private System.Windows.Forms.Label labBottomClear;
        private System.Windows.Forms.ComboBox cbBottomMinute;
        private System.Windows.Forms.ComboBox cbBottomHour;
        private System.Windows.Forms.Panel panBody;
    }
}
