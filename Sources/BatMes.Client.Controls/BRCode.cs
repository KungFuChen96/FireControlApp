using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class BRCode : UserControl
    {
        private const int pageSize = 15;
        private int pageIndex = 1;
        private int pageTotal = 0;

        public BRCode()
        {
            InitializeComponent();
        }

        private void BRCode_Load(object sender, EventArgs e)
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
            this.dgvBRCode.Width = _width;
            decimal _target_height = _height - 70 - 50;
            int _row_height = (int)Math.Floor(_target_height / 16);
            this.dgvBRCode.ColumnHeadersHeight = _row_height;
            this.dgvBRCode.RowTemplate.Height = _row_height;
            this.dgvBRCode.Height = _row_height * 16;
            //设置底部分页区域宽与位置
            this.panBottomZone.Width = _width;
            this.panBottomZone.Location = new Point(0, 70 + (int)_target_height);
            //设备底部文字区域宽
            this.labPager.Width = _width - 120 * 4 - 10 * 3;

            this.BindBRCode();
        }

        public void BindBRCode()
        {
            if (this.pageIndex < 1)
                this.pageIndex = 1;

            var data = Business.Instance.BatteryResultCodeList(pageSize, this.pageIndex);
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
            this.dgvBRCode.Columns.Clear();
            this.dgvBRCode.Rows.Clear();
            if (data != null && data.Count() > 0)
            {
                //添加列（总宽1460）
                //结果代码
                DataGridViewTextBoxColumn col_result_code = new DataGridViewTextBoxColumn();
                col_result_code.Name = "result_code";
                col_result_code.HeaderText = "结果代码";
                col_result_code.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvBRCode.Columns.Add(col_result_code);
                //转换显示
                DataGridViewTextBoxColumn col_custom_code = new DataGridViewTextBoxColumn();
                col_custom_code.Name = "custom_code";
                col_custom_code.HeaderText = "转换显示";
                col_custom_code.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvBRCode.Columns.Add(col_custom_code);
                //备注
                DataGridViewTextBoxColumn col_remark = new DataGridViewTextBoxColumn();
                col_remark.Name = "remark";
                col_remark.HeaderText = "备注";
                col_remark.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvBRCode.Columns.Add(col_remark);

                foreach (var item in data)
                {
                    int i = this.dgvBRCode.Rows.Add();
                    this.dgvBRCode.Rows[i].Cells["result_code"].Value = item.result_code;
                    this.dgvBRCode.Rows[i].Cells["custom_code"].Value = item.custom_code;
                    this.dgvBRCode.Rows[i].Cells["remark"].Value = item.remark;

                    if (i % 2 == 1)
                        this.dgvBRCode.Rows[i].DefaultCellStyle.BackColor = CustomColor.YellowLight;
                }
            }
        }

        private void ibFirst_Click(object sender, EventArgs e)
        {
            //首页
            if (this.pageIndex != 1)
            {
                this.pageIndex = 1;
                this.BindBRCode();
            }
        }

        private void ibPrev_Click(object sender, EventArgs e)
        {
            //上一页
            if (this.pageIndex > 1)
            {
                this.pageIndex -= 1;
                this.BindBRCode();
            }
        }

        private void ibNext_Click(object sender, EventArgs e)
        {
            //下一页
            if (this.pageTotal > 1 && this.pageIndex < this.pageTotal)
            {
                this.pageIndex += 1;
                this.BindBRCode();
            }
        }

        private void ibLast_Click(object sender, EventArgs e)
        {
            //尾页
            if (this.pageTotal > 1 && this.pageIndex != this.pageTotal)
            {
                this.pageIndex = this.pageTotal;
                this.BindBRCode();
            }
        }

        /// <summary>
        /// 禁止选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBRCode_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvBRCode.ClearSelection();
        }

        private void ibAdd_Click(object sender, EventArgs e)
        {
            //新增
            this.brCodeAdd.Bind(this);
            this.brCodeAdd.Visible = true;
        }

        private void dgvBRCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //编辑与删除
            this.brCodeEdit.Bind(this, this.dgvBRCode.Rows[e.RowIndex].Cells["result_code"].Value.ToString());
            this.brCodeEdit.Visible = true;
        }

    }//end class
}