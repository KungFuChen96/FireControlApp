using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BatMes.Client.Controls
{
    /// <summary>
    /// 系统事件
    /// </summary>
    public partial class SysEvent : UserControl
    {
        private const int pageSize = 15;
        private int pageIndex = 1;
        private int pageTotal = 0;

        public SysEvent()
        {
            InitializeComponent();
        }

        private void SysEvent_Load(object sender, EventArgs e)
        {
            //宽
            int _width = Tools.ScreenWidth;
            if (_width < Tools.SCREEN_WIDTH_MIN)
                _width = Tools.SCREEN_WIDTH_MIN;
            //if (_width > Tools.SCREEN_WIDTH_MAX)
            //    _width = Tools.SCREEN_WIDTH_MAX;
            _width = _width - 20 * 2;

            //高
            int _height = Tools.ScreenHeight - Tools.TOP_HEIGHT - Tools.BOTTOM_HEIGHT - 20 * 2;

            //设置用户控件宽高
            this.Width = _width;
            this.Height = _height;
            //设置顶部操作区域宽
            this.panTopZone.Width = _width;
            //设置数据控件宽高与行高
            this.dgvEvent.Width = _width;
            decimal _target_height = _height - 70 - 50;
            int _row_height = (int)Math.Floor(_target_height / 16);
            this.dgvEvent.ColumnHeadersHeight = _row_height;
            this.dgvEvent.RowTemplate.Height = _row_height;
            this.dgvEvent.Height = _row_height * 16;
            //设置底部分页区域宽与位置
            this.panBottomZone.Width = _width;
            this.panBottomZone.Location = new Point(0, 70 + (int)_target_height);
            //设备底部文字区域宽
            this.labPager.Width = _width - 120 * 4 - 10 * 3;

            this.txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00");
            this.txtEndTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00");

            this.txtBeginTime.GotFocus += txtBeginTime_GotFocus;
            this.txtBeginTime.LostFocus += txtBeginTime_LostFocus;

            this.txtEndTime.GotFocus += txtEndTime_GotFocus;
            this.txtEndTime.LostFocus += txtEndTime_LostFocus;

            this.bindLevel();
            this.bindEvent();
        }

        private void txtBeginTime_Click(object sender, EventArgs e)
        {
            this.calendar.Bind((TextBox)sender);
        }

        private void txtBeginTime_GotFocus(object sender, EventArgs e)
        {
            this.calendar.Bind((TextBox)sender);
        }

        private void txtBeginTime_LostFocus(object sender, EventArgs e)
        {
            if (!this.calendar.HourOrMinuteFocused)
                this.calendar.UnBind();
        }

        private void txtEndTime_Click(object sender, EventArgs e)
        {
            this.calendar.Bind((TextBox)sender);
        }

        private void txtEndTime_GotFocus(object sender, EventArgs e)
        {
            this.calendar.Bind((TextBox)sender);
        }

        private void txtEndTime_LostFocus(object sender, EventArgs e)
        {
            if (!this.calendar.HourOrMinuteFocused)
                this.calendar.UnBind();
        }

        private void bindLevel()
        {
            this.cbLevel.ValueMember = "Key";
            this.cbLevel.DisplayMember = "Value";
            this.cbLevel.Items.Add(new KeyValuePair<int, string>(0, "全部"));
            this.cbLevel.Items.Add(new KeyValuePair<int, string>((int)Enums.SysEventLevel.Info, Enums.Tools.SysEventLevel((int)Enums.SysEventLevel.Info)));
            this.cbLevel.Items.Add(new KeyValuePair<int, string>((int)Enums.SysEventLevel.Warn, Enums.Tools.SysEventLevel((int)Enums.SysEventLevel.Warn)));
            this.cbLevel.Items.Add(new KeyValuePair<int, string>((int)Enums.SysEventLevel.Error, Enums.Tools.SysEventLevel((int)Enums.SysEventLevel.Error)));
            this.cbLevel.SelectedIndex = 0;
        }

        private void bindEvent()
        {
            if (this.txtBeginTime.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入起始时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBeginTime.Focus();
                return;
            }
            if (this.txtEndTime.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtEndTime.Focus();
                return;
            }

            DateTime beginTime = DateTime.Parse(this.txtBeginTime.Text);
            DateTime endTime = DateTime.Parse(this.txtEndTime.Text);
            if (endTime <= beginTime)
            {
                MessageBox.Show(this, "结束时间必须大于开始时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtEndTime.Focus();
                return;
            }

            if (this.pageIndex < 1)
                this.pageIndex = 1;

            var data = Business.Instance.SysEventList(pageSize, this.pageIndex, beginTime, endTime, ((KeyValuePair<int, string>)this.cbLevel.SelectedItem).Key);
            this.pageTotal = data.PageTotal;
            this.labPager.Text = $"共  {data.RecordTotal}  条记录  {this.pageIndex}/{this.pageTotal}  页";

            if (this.pageIndex > data.PageTotal)
                this.pageIndex = data.PageTotal;

            //没有记录或只有一页记录
            if (data.RecordTotal == 0 || data.PageTotal == 1)
            {
                this.ibFirst.BackColor = Color.FromArgb(136, 136, 136);
                this.ibFirst.Cursor = Cursors.No;

                this.ibPrev.BackColor = Color.FromArgb(136, 136, 136);
                this.ibPrev.Cursor = Cursors.No;

                this.ibNext.BackColor = Color.FromArgb(136, 136, 136);
                this.ibNext.Cursor = Cursors.No;

                this.ibLast.BackColor = Color.FromArgb(136, 136, 136);
                this.ibLast.Cursor = Cursors.No;
            }
            else if (this.pageIndex == 1)
            {
                this.ibFirst.BackColor = Color.FromArgb(136, 136, 136);
                this.ibFirst.Cursor = Cursors.No;

                this.ibPrev.BackColor = Color.FromArgb(136, 136, 136);
                this.ibPrev.Cursor = Cursors.No;

                this.ibNext.BackColor = Color.FromArgb(0, 125, 184);
                this.ibNext.Cursor = Cursors.Hand;

                this.ibLast.BackColor = Color.FromArgb(0, 125, 184);
                this.ibLast.Cursor = Cursors.Hand;
            }
            else if (this.pageIndex == data.PageTotal)
            {
                this.ibFirst.BackColor = Color.FromArgb(0, 125, 184);
                this.ibFirst.Cursor = Cursors.Hand;

                this.ibPrev.BackColor = Color.FromArgb(0, 125, 184);
                this.ibPrev.Cursor = Cursors.Hand;

                this.ibNext.BackColor = Color.FromArgb(136, 136, 136);
                this.ibNext.Cursor = Cursors.No;

                this.ibLast.BackColor = Color.FromArgb(136, 136, 136);
                this.ibLast.Cursor = Cursors.No;
            }
            else
            {
                this.ibFirst.BackColor = Color.FromArgb(0, 125, 184);
                this.ibFirst.Cursor = Cursors.Hand;

                this.ibPrev.BackColor = Color.FromArgb(0, 125, 184);
                this.ibPrev.Cursor = Cursors.Hand;

                this.ibNext.BackColor = Color.FromArgb(0, 125, 184);
                this.ibNext.Cursor = Cursors.Hand;

                this.ibLast.BackColor = Color.FromArgb(0, 125, 184);
                this.ibLast.Cursor = Cursors.Hand;
            }

            //绑定数据
            this.dgvEvent.Columns.Clear();
            this.dgvEvent.Rows.Clear();
            if (data != null && data.Count() > 0)
            {
                //添加列
                //ID
                DataGridViewTextBoxColumn col_event_id = new DataGridViewTextBoxColumn();
                col_event_id.Name = "event_id";
                col_event_id.HeaderText = "事件ID";
                col_event_id.SortMode = DataGridViewColumnSortMode.NotSortable;
                col_event_id.Width = 100;
                this.dgvEvent.Columns.Add(col_event_id);
                //标题
                DataGridViewTextBoxColumn col_title = new DataGridViewTextBoxColumn();
                col_title.Name = "title";
                col_title.HeaderText = "标题";
                col_title.SortMode = DataGridViewColumnSortMode.NotSortable;
                int _width_title = (this.dgvEvent.Width - 100 - 100 - 180) / 2;
                col_title.Width = _width_title;
                this.dgvEvent.Columns.Add(col_title);
                //内容
                DataGridViewTextBoxColumn col_cont = new DataGridViewTextBoxColumn();
                col_cont.Name = "cont";
                col_cont.HeaderText = "内容";
                col_cont.SortMode = DataGridViewColumnSortMode.NotSortable;
                col_cont.Width = this.dgvEvent.Width - 100 - 100 - 180 - _width_title;
                this.dgvEvent.Columns.Add(col_cont);
                //级别
                DataGridViewTextBoxColumn col_level = new DataGridViewTextBoxColumn();
                col_level.Name = "level";
                col_level.HeaderText = "级别";
                col_level.SortMode = DataGridViewColumnSortMode.NotSortable;
                col_level.Width = 100;
                this.dgvEvent.Columns.Add(col_level);
                //时间
                DataGridViewTextBoxColumn col_create_time = new DataGridViewTextBoxColumn();
                col_create_time.Name = "create_time";
                col_create_time.HeaderText = "时间";
                col_create_time.SortMode = DataGridViewColumnSortMode.NotSortable;
                col_create_time.Width = 180;
                this.dgvEvent.Columns.Add(col_create_time);

                foreach (var item in data)
                {
                    int i = this.dgvEvent.Rows.Add();
                    this.dgvEvent.Rows[i].Cells["event_id"].Value = item.event_id;
                    this.dgvEvent.Rows[i].Cells["title"].Value = item.title;
                    this.dgvEvent.Rows[i].Cells["cont"].Value = item.cont;
                    this.dgvEvent.Rows[i].Cells["level"].Value = Enums.Tools.SysEventLevel(item.level);
                    switch(item.level)
                    {
                        case (int)Enums.SysEventLevel.Warn:
                            this.dgvEvent.Rows[i].Cells["level"].Style.ForeColor = Color.Blue;
                            break;
                        case (int)Enums.SysEventLevel.Error:
                            this.dgvEvent.Rows[i].Cells["level"].Style.ForeColor = Color.Red;
                            break;
                    }
                    this.dgvEvent.Rows[i].Cells["create_time"].Value = item.create_time.ToString("yyyy-MM-dd HH:mm:ss");

                    if (i % 2 == 1)
                        this.dgvEvent.Rows[i].DefaultCellStyle.BackColor = CustomColor.YellowLight;
                }
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibSearch_Click(object sender, EventArgs e)
        {
            this.pageIndex = 1;
            this.bindEvent();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibExport_Click(object sender, EventArgs e)
        {
            if (this.txtBeginTime.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入起始时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtBeginTime.Focus();
                return;
            }
            if (this.txtEndTime.Text.Length == 0)
            {
                MessageBox.Show(this, "请输入结束时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtEndTime.Focus();
                return;
            }

            DateTime beginTime = DateTime.Parse(this.txtBeginTime.Text);
            DateTime endTime = DateTime.Parse(this.txtEndTime.Text);
            if (endTime <= beginTime)
            {
                MessageBox.Show(this, "结束时间必须大于开始时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtEndTime.Focus();
                return;
            }

            //级别
            int level = ((KeyValuePair<int, string>)this.cbLevel.SelectedItem).Key;

            SaveFileDialog sfd = new SaveFileDialog();
            //指定文件类型
            sfd.Filter = "CSV文件（*.csv）|";
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            //设置默认的文件名
            string fileName = $"事件-{beginTime.ToString("yyyyMMddHHmm")}至{endTime.ToString("yyyyMMddHHmm")}-";
            switch (level)
            {
                case 0:
                    fileName += "全部";
                    break;
                case (int)Enums.SysEventLevel.Info:
                    fileName += Enums.Tools.SysEventLevel((int)Enums.SysEventLevel.Info);
                    break;
                case (int)Enums.SysEventLevel.Warn:
                    fileName += Enums.Tools.SysEventLevel((int)Enums.SysEventLevel.Warn);
                    break;
                case (int)Enums.SysEventLevel.Error:
                    fileName += Enums.Tools.SysEventLevel((int)Enums.SysEventLevel.Error);
                    break;
            }
            sfd.FileName = fileName + ".csv";
            //保存文件
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder("事件ID,标题,内容,级别,时间\r\n");
                using (MySqlDataReader dr = Business.Instance.SysEventList(beginTime, endTime, level))
                {
                    while (dr.Read())
                    {
                        sb.Append(dr["event_id"].ToString() + ",");
                        sb.Append("\"" + dr["title"].ToString().Replace("\"", "\"\"") + "\",");
                        sb.Append("\"" + dr["cont"].ToString().Replace("\"", "\"\"") + "\",");
                        sb.Append("\"" + Enums.Tools.SysEventLevel((int)dr["level"]) + "\",");
                        sb.Append("\"" + dr["create_time"].ToString() + "\"");
                        sb.Append("\r\n");
                    }
                }

                try
                {
                    XY.IO.TextWrite(sfd.FileName, sb.ToString());
                    MessageBox.Show(this, "导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    MessageBox.Show(this, "文件保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 禁止选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEvent_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvEvent.ClearSelection();
        }

        /// <summary>
        /// 禁止Tooltips
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEvent_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvEvent.ShowCellToolTips = false;
        }

        private void ibFirst_Click(object sender, EventArgs e)
        {
            //首页
            if (this.pageIndex != 1)
            {
                this.pageIndex = 1;
                this.bindEvent();
            }
        }

        private void ibPrev_Click(object sender, EventArgs e)
        {
            //上一页
            if (this.pageIndex > 1)
            {
                this.pageIndex -= 1;
                this.bindEvent();
            }
        }

        private void ibNext_Click(object sender, EventArgs e)
        {
            //下一页
            if (this.pageTotal > 1 && this.pageIndex < this.pageTotal)
            {
                this.pageIndex += 1;
                this.bindEvent();
            }
        }

        private void ibLast_Click(object sender, EventArgs e)
        {
            //尾页
            if (this.pageTotal > 1 && this.pageIndex != this.pageTotal)
            {
                this.pageIndex = this.pageTotal;
                this.bindEvent();
            }
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvEvent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.eventDetail.LogID = this.dgvEvent.Rows[e.RowIndex].Cells["event_id"].Value.ToString();
            this.eventDetail.Type = this.dgvEvent.Rows[e.RowIndex].Cells["level"].Value.ToString();
            this.eventDetail.CreateTime = this.dgvEvent.Rows[e.RowIndex].Cells["create_time"].Value.ToString();
            this.eventDetail.Title = this.dgvEvent.Rows[e.RowIndex].Cells["title"].Value.ToString();
            this.eventDetail.Cont = this.dgvEvent.Rows[e.RowIndex].Cells["cont"].Value.ToString();
            this.eventDetail.Bind();
            this.eventDetail.Visible = true;
        }

    }//end class
}