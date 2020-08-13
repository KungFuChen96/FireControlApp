using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class Calendar : UserControl
    {
        /// <summary>
        /// 绑定的TextBox
        /// </summary>
        private TextBox bindTextBox { get; set; }

        //最终确定时间
        private int confirmYear;
        private int confirmMonth;
        private int confirmDay;
        //时分从控件实时获取

        public Calendar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_Load(object sender, EventArgs e)
        {
            this.labTopYearPrev.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 16);
            this.labTopYearPrev.Text = Tools.IconButtonTypeValue(IconButtonType.First);

            this.labTopMonthPrev.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 16);
            this.labTopMonthPrev.Text = Tools.IconButtonTypeValue(IconButtonType.Prev);

            this.labTopMonthNext.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 16);
            this.labTopMonthNext.Text = Tools.IconButtonTypeValue(IconButtonType.Next);

            this.labTopYearNext.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, 16);
            this.labTopYearNext.Text = Tools.IconButtonTypeValue(IconButtonType.Last);

            this.cbBottomHour.LostFocus += cbBottomHour_LostFocus;
            this.cbBottomMinute.LostFocus += cbBottomMinute_LostFocus;
        }

        private void cbBottomHour_LostFocus(object sender, EventArgs e)
        {
            if (!HourOrMinuteFocused)
                this.bindTextBox.Focus();
        }

        private void cbBottomMinute_LostFocus(object sender, EventArgs e)
        {
            if (!HourOrMinuteFocused)
                this.bindTextBox.Focus();
        }

        private void bind(int year, int month, int day, int hour, int minute)
        {
            //时
            int cbBottomHourIndex = 0;
            this.cbBottomHour.Items.Clear();
            this.cbBottomHour.ValueMember = "Key";
            this.cbBottomHour.DisplayMember = "Value";
            for (int i = 0; i <= 23; i++)
            {
                this.cbBottomHour.Items.Add(new KeyValuePair<int, string>(i, $"{i}时"));
                if (i == hour)
                    cbBottomHourIndex = i;
            }
            this.cbBottomHour.SelectedIndex = cbBottomHourIndex;

            //分
            int cbBottomMinuteIndex = 0;
            this.cbBottomMinute.Items.Clear();
            this.cbBottomMinute.ValueMember = "Key";
            this.cbBottomMinute.DisplayMember = "Value";
            for (int i = 0; i <= 59; i += 1)
            {
                this.cbBottomMinute.Items.Add(new KeyValuePair<int, string>(i, $"{i}分"));
                if (i == minute)
                    cbBottomMinuteIndex = i;
            }
            this.cbBottomMinute.SelectedIndex = cbBottomMinuteIndex;

            //年
            this.confirmYear = year;
            this.labTopYear.Text = $"{this.confirmYear}年";

            //月
            this.confirmMonth = month;
            this.labTopMonth.Text = $"{this.confirmMonth}月";

            //日
            int[] days = XY.Knowledge.Days(this.confirmYear, this.confirmMonth);
            if (day > days.Last())
                this.confirmDay = days.Last();
            else
                this.confirmDay = day;

            int dayFullIndex = 0;//全局序号
            int dayFirstIndex = (int)(new DateTime(this.confirmYear, this.confirmMonth, 1).DayOfWeek);//当月1号起始序号
            this.panBody.Controls.Clear();

            for (int i = 0; i < 6; i++)//遍历行
            {
                for (int j = 0; j < 7; j++)//遍历列
                {
                    //基础
                    Label lab = new Label();
                    lab.AutoSize = false;                    
                    lab.TextAlign = ContentAlignment.MiddleCenter;
                    lab.Size = new Size(40, 30);
                    lab.Font = new Font("微软雅黑", 12);                    
                    lab.Location = new Point(40 * j, 30 * i);
                    
                    //上一月的日期
                    if (dayFullIndex < dayFirstIndex)
                    {
                        DateTime dt = new DateTime(this.confirmYear, this.confirmMonth, 1, ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0)
                            .AddDays(dayFullIndex - dayFirstIndex);
                        lab.Text = dt.Day.ToString();
                        lab.ForeColor = Color.FromArgb(204, 204, 204);
                        lab.Cursor = Cursors.Hand;
                        lab.MouseHover += (object sender, EventArgs e) =>
                        {
                            lab.ForeColor = Color.FromArgb(51, 51, 51);
                            ((Label)sender).BackColor = Color.FromArgb(160, 215, 233);
                        };
                        lab.MouseLeave += (object sender, EventArgs e) =>
                        {
                            lab.ForeColor = Color.FromArgb(204, 204, 204);
                            ((Label)sender).BackColor = Color.Transparent;
                        };
                        lab.Click += (object sender, EventArgs e) =>
                        {
                            this.bind(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
                        };
                    }
                    //下一月的日期
                    else if(dayFullIndex >= days.Length + dayFirstIndex)
                    {
                        DateTime dt = new DateTime(this.confirmYear, this.confirmMonth, days.Last(), ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0)
                            .AddDays(dayFullIndex - (days.Length + dayFirstIndex) + 1);
                        lab.Text = dt.Day.ToString();
                        lab.ForeColor = Color.FromArgb(204, 204, 204);
                        lab.Cursor = Cursors.Hand;
                        lab.MouseHover += (object sender, EventArgs e) =>
                        {
                            lab.ForeColor = Color.FromArgb(51, 51, 51);
                            ((Label)sender).BackColor = Color.FromArgb(160, 215, 233);
                        };
                        lab.MouseLeave += (object sender, EventArgs e) =>
                        {
                            lab.ForeColor = Color.FromArgb(204, 204, 204);
                            ((Label)sender).BackColor = Color.Transparent;
                        };
                        lab.Click += (object sender, EventArgs e) =>
                        {
                            this.bind(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
                        };
                    }
                    //当月日期
                    else
                    {
                        int _day = days[dayFullIndex - dayFirstIndex];
                        lab.Text = _day.ToString();
                        //当前确认日期
                        if (days[dayFullIndex - dayFirstIndex] == this.confirmDay)
                        {
                            lab.ForeColor = Color.White;
                            lab.BackColor = Color.FromArgb(0, 125, 184);
                        }
                        else
                        {
                            lab.Cursor = Cursors.Hand;
                            lab.ForeColor = Color.FromArgb(51, 51, 51);
                            lab.MouseHover += (object sender, EventArgs e) =>
                            {
                                ((Label)sender).BackColor = Color.FromArgb(160, 215, 233);
                            };
                            lab.MouseLeave += (object sender, EventArgs e) =>
                            {
                                ((Label)sender).BackColor = Color.Transparent;
                            };
                            lab.Click += (object sender, EventArgs e) =>
                            {
                                DateTime dt = new DateTime(this.confirmYear, this.confirmMonth, _day, ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0);
                                this.bind(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
                            };
                        }
                    }

                    //添加
                    this.panBody.Controls.Add(lab);
                    dayFullIndex++;
                }
            }

            this.bindTextBox.Focus();
        }

        public void Bind(TextBox textBox)
        {
            if(this.Visible == false)
            {
                this.bindTextBox = textBox;
                this.Location = new Point(textBox.Location.X, textBox.Location.Y + textBox.Height + 5);

                //初始化时间，如果TextBox中不存在初始化时间，则使用当前时间作为初始化时间
                DateTime initTime = DateTime.Now;
                if (textBox.Text.Length > 0)
                    initTime = DateTime.Parse(textBox.Text);

                this.Visible = true;
                this.bind(initTime.Year, initTime.Month, initTime.Day, initTime.Hour, initTime.Minute);
            }
        }

        public void UnBind()
        {
            //this.bindTextBox = null;
            this.Visible = false;
        }

        /// <summary>
        /// 判断时、分控件是否拥有焦点
        /// </summary>
        public bool HourOrMinuteFocused
        {
            get { return this.cbBottomHour.Focused || this.cbBottomMinute.Focused; }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labBottomConfirm_Click(object sender, EventArgs e)
        {
            var temp = this.cbBottomHour.SelectedItem;
            this.bindTextBox.Text = new DateTime(this.confirmYear, this.confirmMonth, this.confirmDay, ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0).ToString("yyyy-MM-dd HH:mm");
            this.UnBind();
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labBottomClear_Click(object sender, EventArgs e)
        {
            this.bindTextBox.Text = string.Empty;
            this.UnBind();
        }

        /// <summary>
        /// 现在
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labBottomNow_Click(object sender, EventArgs e)
        {
            this.bindTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            this.UnBind();
        }

        /// <summary>
        /// 上一年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labTopYearPrev_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(this.confirmYear, this.confirmMonth, this.confirmDay, ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0);
            dt = dt.AddYears(-1);
            this.bind(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
        }

        /// <summary>
        /// 上一月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labTopMonthPrev_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(this.confirmYear, this.confirmMonth, this.confirmDay, ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0);
            dt = dt.AddMonths(-1);
            this.bind(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
        }

        /// <summary>
        /// 下一月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labTopMonthNext_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(this.confirmYear, this.confirmMonth, this.confirmDay, ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0);
            dt = dt.AddMonths(1);
            this.bind(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
        }

        /// <summary>
        /// 下一年
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labTopYearNext_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(this.confirmYear, this.confirmMonth, this.confirmDay, ((KeyValuePair<int, string>)this.cbBottomHour.SelectedItem).Key, ((KeyValuePair<int, string>)this.cbBottomMinute.SelectedItem).Key, 0);
            dt = dt.AddYears(1);
            this.bind(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute);
        }

    }//end class
}