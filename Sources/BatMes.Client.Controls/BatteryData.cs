using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BatMes.Client.Controls
{
    public partial class BatteryData : UserControl
    {
        private const int pageSize = 15;
        private int pageIndex = 1;
        private int pageTotal = 0;

        public BatteryData()
        {
            InitializeComponent();
        }

        #region 字段呈现

        /// <summary>
        /// 电压单位
        /// </summary>
        public Enums.VolUnit VolUnit { get; set; } = Enums.VolUnit.MV;

        /// <summary>
        /// 电压精度（小数位数）
        /// </summary>
        public int VolDecimal { get; set; } = 0;

        /// <summary>
        /// 电阻单位
        /// </summary>
        public Enums.IrUnit IrUnit { get; set; } = Enums.IrUnit.MO;

        /// <summary>
        /// 电阻精度（小数位数）
        /// </summary>
        public int IrDecimal { get; set; } = 0;

        /// <summary>
        /// 是否呈现开路电压字段
        /// </summary>
        public bool IsOcv { get; set; }

        /// <summary>
        /// 自定义开路电压字段名称
        /// </summary>
        public string OcvName { get; set; } = "开路电压";

        /// <summary>
        /// 是否呈现壳体电压字段
        /// </summary>
        public bool IsShellV { get; set; }

        /// <summary>
        /// 自定义壳体电压字段名称
        /// </summary>
        public string ShellVName { get; set; } = "壳体电压";

        /// <summary>
        /// 是否呈现放电电压字段
        /// </summary>
        public bool IsDv { get; set; }

        /// <summary>
        /// 自定义放电电压字段名称
        /// </summary>
        public string DvName { get; set; } = "放电电压";

        /// <summary>
        /// 是否呈现交流内阻字段
        /// </summary>
        public bool IsAcir { get; set; }

        /// <summary>
        /// 自定义交流内阻字段名称
        /// </summary>
        public string AcirName { get; set; } = "交流内阻";

        /// <summary>
        /// 是否呈现直流内阻字段
        /// </summary>
        public bool IsDcir { get; set; }

        /// <summary>
        /// 自定义直流内阻字段名称
        /// </summary>
        public string DcirName { get; set; } = "直流内阻";

        /// <summary>
        /// 是否呈现环境温度字段
        /// </summary>
        public bool IsTemp { get; set; }

        /// <summary>
        /// 自定义环境温度字段名称
        /// </summary>
        public string TempName { get; set; } = "环境温度";

        /// <summary>
        /// 是否呈现K值字段
        /// </summary>
        public bool IsKval { get; set; }

        /// <summary>
        /// 自定义K值字段名称
        /// </summary>
        public string KvalName { get; set; } = "K值";

        /// <summary>
        /// K值精度（小数位数）
        /// </summary>
        public int KvalDecimal { get; set; } = 3;

        /// <summary>
        /// 是否呈现测试结果
        /// </summary>
        public bool IsResult { get; set; }

        #endregion

        private void BatteryData_Load(object sender, EventArgs e)
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
            this.dgvBattery.Width = _width;
            decimal _target_height = _height - 70 - 50;
            int _row_height = (int)Math.Floor(_target_height / 16);
            this.dgvBattery.ColumnHeadersHeight = _row_height;
            this.dgvBattery.RowTemplate.Height = _row_height;
            this.dgvBattery.Height = _row_height * 16;
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

            this.bindBattery();
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

        private void bindBattery()
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

            var data = Business.Instance.BatteryList(pageSize, this.pageIndex, beginTime, endTime, this.txtBatteryCode.Text.Trim());
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
            this.dgvBattery.Columns.Clear();
            this.dgvBattery.Rows.Clear();
            if (data != null && data.Count() > 0)
            {
                //添加列
                DataGridViewTextBoxColumn col_battery_code = new DataGridViewTextBoxColumn();
                col_battery_code.Name = "battery_code";
                col_battery_code.HeaderText = "电池条码";
                col_battery_code.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvBattery.Columns.Add(col_battery_code);
                //开路电压
                if (this.IsOcv)
                {
                    DataGridViewTextBoxColumn col_ocv = new DataGridViewTextBoxColumn();
                    col_ocv.Name = "ocv";
                    col_ocv.HeaderText = $"{this.OcvName}({Enums.Tools.VolUnit((int)this.VolUnit)})";
                    col_ocv.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_ocv);
                }
                //壳体电压
                if (this.IsShellV)
                {
                    DataGridViewTextBoxColumn col_shell_v = new DataGridViewTextBoxColumn();
                    col_shell_v.Name = "shell_v";
                    col_shell_v.HeaderText = $"{this.ShellVName}({Enums.Tools.VolUnit((int)this.VolUnit)})";
                    col_shell_v.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_shell_v);
                }
                //放电电压
                if (this.IsDv)
                {
                    DataGridViewTextBoxColumn col_dv = new DataGridViewTextBoxColumn();
                    col_dv.Name = "dv";
                    col_dv.HeaderText = $"{this.DvName}({Enums.Tools.VolUnit((int)this.VolUnit)})";
                    col_dv.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_dv);
                }
                //交流内阻
                if (this.IsAcir)
                {
                    DataGridViewTextBoxColumn col_acir = new DataGridViewTextBoxColumn();
                    col_acir.Name = "acir";
                    col_acir.HeaderText = $"{this.AcirName}({Enums.Tools.IrUnit((int)this.IrUnit)})";
                    col_acir.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_acir);
                }
                //直流内阻
                if (this.IsDcir)
                {
                    DataGridViewTextBoxColumn col_dcir = new DataGridViewTextBoxColumn();
                    col_dcir.Name = "dcir";
                    col_dcir.HeaderText = $"{this.DcirName}({Enums.Tools.IrUnit((int)this.IrUnit)})";
                    col_dcir.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_dcir);
                }                    
                //环境温度
                if(this.IsTemp)
                {
                    DataGridViewTextBoxColumn col_temp = new DataGridViewTextBoxColumn();
                    col_temp.Name = "temp";
                    col_temp.HeaderText = $"{this.TempName}({Enums.Tools.TempUnit((int)Enums.TempUnit.C)})";
                    col_temp.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_temp);
                }
                //K值
                if(this.IsKval)
                {
                    DataGridViewTextBoxColumn col_kval = new DataGridViewTextBoxColumn();
                    col_kval.Name = "kval";
                    col_kval.HeaderText = this.KvalName;
                    col_kval.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_kval);
                }                    
                //结果
                if(this.IsResult)
                {
                    DataGridViewTextBoxColumn col_result = new DataGridViewTextBoxColumn();
                    col_result.Name = "result";
                    col_result.HeaderText = "结果";
                    col_result.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvBattery.Columns.Add(col_result);
                }

                //时间
                DataGridViewTextBoxColumn col_create_time = new DataGridViewTextBoxColumn();
                col_create_time.Name = "create_time";
                col_create_time.HeaderText = "时间";
                col_create_time.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvBattery.Columns.Add(col_create_time);

                //添加行
                foreach (var item in data)
                {
                    int i = this.dgvBattery.Rows.Add();
                    //电池条码
                    this.dgvBattery.Rows[i].Cells["battery_code"].Value = item.battery_code;
                    //开路电压
                    if(this.IsOcv)
                        this.dgvBattery.Rows[i].Cells["ocv"].Value = (this.VolUnit == Enums.VolUnit.V ? Math.Round(item.ocv / 1000, this.VolDecimal) : Math.Round(item.ocv, this.VolDecimal)).ToString($"F{this.VolDecimal}");
                    //壳体电压
                    if(this.IsShellV)
                        this.dgvBattery.Rows[i].Cells["shell_v"].Value = (this.VolUnit == Enums.VolUnit.V ? Math.Round(item.shell_v / 1000, this.VolDecimal) : Math.Round(item.shell_v, this.VolDecimal)).ToString($"F{this.VolDecimal}");
                    //放电电压
                    if (this.IsDv)
                        this.dgvBattery.Rows[i].Cells["dv"].Value = (this.VolUnit == Enums.VolUnit.V ? Math.Round(item.dv / 1000, this.VolDecimal) : Math.Round(item.dv, this.VolDecimal)).ToString($"F{this.VolDecimal}");
                    //交流内阻
                    if (this.IsAcir)
                        this.dgvBattery.Rows[i].Cells["acir"].Value = (this.IrUnit == Enums.IrUnit.O ? Math.Round(item.acir / 1000, this.IrDecimal) : Math.Round(item.acir, this.IrDecimal)).ToString($"F{this.IrDecimal}");
                    //直流内阻
                    if (this.IsDcir)
                        this.dgvBattery.Rows[i].Cells["dcir"].Value = (this.IrUnit == Enums.IrUnit.O ? Math.Round(item.dcir / 1000, this.IrDecimal) : Math.Round(item.dcir, this.IrDecimal)).ToString($"F{this.IrDecimal}");
                    //环境温度
                    if (this.IsTemp)
                        this.dgvBattery.Rows[i].Cells["temp"].Value = Math.Round(item.temp, 1).ToString("F1");
                    //K值
                    if(this.IsKval)
                        this.dgvBattery.Rows[i].Cells["kval"].Value = Math.Round(item.kval, this.KvalDecimal).ToString($"F{this.KvalDecimal}");
                    //结果
                    if (this.IsResult)
                    {
                        this.dgvBattery.Rows[i].Cells["result"].Value = Enums.Tools.BatteryResult(item.result);
                        this.dgvBattery.Rows[i].Cells["result"].Style.ForeColor = (Enums.BatteryResult)item.result == Enums.BatteryResult.OK ? Color.Green : Color.Red;
                    }
                    //时间
                    this.dgvBattery.Rows[i].Cells["create_time"].Value = item.create_time.ToString("yyyy-MM-dd HH:mm:ss");

                    if (i % 2 == 1)
                        this.dgvBattery.Rows[i].DefaultCellStyle.BackColor = CustomColor.YellowLight;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibSearch_Click(object sender, EventArgs e)
        {
            this.pageIndex = 1;
            this.bindBattery();
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

            SaveFileDialog sfd = new SaveFileDialog();
            //指定文件类型
            sfd.Filter = "CSV文件（*.csv）|";
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            //设置默认的文件名
            string batteryCode = XY.Text.Trim(this.txtBatteryCode.Text);
            if(batteryCode.Length > 0)
                sfd.FileName = $"数据-电池条码{batteryCode}.csv";
            else
                sfd.FileName = $"数据-{beginTime.ToString("yyyyMMddHHmm")}至{endTime.ToString("yyyyMMddHHmm")}.csv";
            //保存文件
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //数据头
                StringBuilder sb = new StringBuilder("电池条码,");
                if (this.IsOcv)
                    sb.Append($"{this.OcvName}({Enums.Tools.VolUnit((int)this.VolUnit)}),");
                if (this.IsShellV)
                    sb.Append($"{this.ShellVName}({Enums.Tools.VolUnit((int)this.VolUnit)}),");
                if (this.IsDv)
                    sb.Append($"{this.DvName}({Enums.Tools.VolUnit((int)this.VolUnit)}),");
                if (this.IsAcir)
                    sb.Append($"{this.AcirName}({Enums.Tools.IrUnit((int)this.IrUnit)}),");
                if (this.IsDcir)
                    sb.Append($"{this.DcirName}({Enums.Tools.IrUnit((int)this.IrUnit)}),");
                if (this.IsTemp)
                    sb.Append($"{this.TempName}({Enums.Tools.TempUnit((int)Enums.TempUnit.C)}),");
                if (this.IsKval)
                    sb.Append($"{this.KvalName},");
                if (this.IsResult)
                    sb.Append("结果,");
                sb.Append("时间\r\n");

                //数据行
                using (MySqlDataReader dr = Business.Instance.BatteryList(beginTime, endTime, batteryCode))
                {
                    while (dr.Read())
                    {
                        //电池条码
                        sb.Append($"{dr["battery_code"].ToString()},");
                        //开路电压
                        if (this.IsOcv)
                            sb.Append($"{(this.VolUnit == Enums.VolUnit.V ? Math.Round((decimal)dr["ocv"] / 1000, this.VolDecimal) : Math.Round((decimal)dr["ocv"], this.VolDecimal)).ToString($"F{this.VolDecimal}")},");
                        //壳体电压
                        if (this.IsShellV)
                            sb.Append($"{(this.VolUnit == Enums.VolUnit.V ? Math.Round((decimal)dr["shell_v"] / 1000, this.VolDecimal) : Math.Round((decimal)dr["shell_v"], this.VolDecimal)).ToString($"F{this.VolDecimal}")},");
                        //放电电压
                        if (this.IsDv)
                            sb.Append($"{(this.VolUnit == Enums.VolUnit.V ? Math.Round((decimal)dr["dv"] / 1000, this.VolDecimal) : Math.Round((decimal)dr["dv"], this.VolDecimal)).ToString($"F{this.VolDecimal}")},");
                        //交流内阻
                        if (this.IsAcir)
                            sb.Append($"{(this.IrUnit == Enums.IrUnit.O ? Math.Round((decimal)dr["acir"] / 1000, this.IrDecimal) : Math.Round((decimal)dr["acir"], this.IrDecimal)).ToString($"F{this.IrDecimal}")},");
                        //直流内阻
                        if (this.IsDcir)
                            sb.Append($"{(this.IrUnit == Enums.IrUnit.O ? Math.Round((decimal)dr["dcir"] / 1000, this.IrDecimal) : Math.Round((decimal)dr["dcir"], this.IrDecimal)).ToString($"F{this.IrDecimal}")},");
                        //环境温度
                        if (this.IsTemp)
                            sb.Append($"{Math.Round((decimal)dr["temp"], 1).ToString("F1")},");
                        //K值
                        if (this.IsKval)
                            sb.Append($"{Math.Round((decimal)dr["kval"], this.KvalDecimal).ToString($"F{this.KvalDecimal}")},");
                        //结果
                        if (this.IsResult)
                            sb.Append($"{Enums.Tools.BatteryResult((int)dr["result"])},");
                        //时间
                        sb.Append($" {dr["create_time"].ToString()}\r\n");
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
        private void dgvBattery_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvBattery.ClearSelection();
        }

        private void ibFirst_Click(object sender, EventArgs e)
        {
            //首页
            if (this.pageIndex != 1)
            {
                this.pageIndex = 1;
                this.bindBattery();
            }
        }

        private void ibPrev_Click(object sender, EventArgs e)
        {
            //上一页
            if (this.pageIndex > 1)
            {
                this.pageIndex -= 1;
                this.bindBattery();
            }
        }

        private void ibNext_Click(object sender, EventArgs e)
        {
            //下一页
            if (this.pageTotal > 1 && this.pageIndex < this.pageTotal)
            {
                this.pageIndex += 1;
                this.bindBattery();
            }
        }

        private void ibLast_Click(object sender, EventArgs e)
        {
            //尾页
            if (this.pageTotal > 1 && this.pageIndex != this.pageTotal)
            {
                this.pageIndex = this.pageTotal;
                this.bindBattery();
            }
        }

    }//end class
}