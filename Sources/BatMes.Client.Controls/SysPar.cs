using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class SysPar : UserControl
    {
        private const int pageSize = 15;
        private int pageIndex = 1;
        private int pageTotal = 0;

        /// <summary>
        /// 客户端开发规范
        /// </summary>
        public BatMes.Client.IClient Client { get; set; }

        public SysPar()
        {
            InitializeComponent();
        }

        private void SysPar_Load(object sender, EventArgs e)
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
            this.dgvPar.Width = _width;
            decimal _target_height = _height - 70 - 50;
            int _row_height = (int)Math.Floor(_target_height / 16);
            this.dgvPar.ColumnHeadersHeight = _row_height;
            this.dgvPar.RowTemplate.Height = _row_height;
            this.dgvPar.Height = _row_height * 16;
            //设置底部分页区域宽与位置
            this.panBottomZone.Width = _width;
            this.panBottomZone.Location = new Point(0, 70 + (int)_target_height);
            //设备底部文字区域宽
            this.labPager.Width = _width - 120 * 4 - 10 * 3;

            this.bindSort();
            this.BindPar();
        }

        private void bindSort()
        {
            this.cbSort.ValueMember = "Key";
            this.cbSort.DisplayMember = "Value";
            var sortList = Business.Instance.SysParaSortList();
            if(sortList != null && sortList.Count > 0)
            {
                foreach (var sort in sortList)
                {
                    this.cbSort.Items.Add(new KeyValuePair<int, string>(sort.sort_id, sort.name));
                }

                this.cbSort.SelectedIndex = 0;
            }
        }

        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindPar();
        }

        public void BindPar()
        {
            if (this.pageIndex < 1)
                this.pageIndex = 1;

            if(this.cbSort.SelectedItem != null)
            {
                var data = Business.Instance.SysParaList(pageSize, this.pageIndex, ((KeyValuePair<int, string>)this.cbSort.SelectedItem).Key);
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
                this.dgvPar.Columns.Clear();
                this.dgvPar.Rows.Clear();
                if (data != null && data.Count() > 0)
                {
                    //添加列（总宽1460）
                    //ID
                    DataGridViewTextBoxColumn col_para_id = new DataGridViewTextBoxColumn();
                    col_para_id.Visible = false;
                    col_para_id.Name = "para_id";
                    col_para_id.HeaderText = "ID";
                    col_para_id.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvPar.Columns.Add(col_para_id);
                    //名称
                    DataGridViewTextBoxColumn col_name = new DataGridViewTextBoxColumn();
                    col_name.Name = "name";
                    col_name.HeaderText = "名称";
                    col_name.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvPar.Columns.Add(col_name);
                    //说明
                    DataGridViewTextBoxColumn col_remark = new DataGridViewTextBoxColumn();
                    col_remark.Name = "remark";
                    col_remark.HeaderText = "说明";
                    col_remark.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvPar.Columns.Add(col_remark);
                    ////类型
                    //DataGridViewTextBoxColumn col_para_type = new DataGridViewTextBoxColumn();
                    //col_para_type.Name = "para_type";
                    //col_para_type.HeaderText = "类型";
                    //col_para_type.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //col_para_type.Width = 100;
                    //this.dgvPar.Columns.Add(col_para_type);
                    //参数值
                    DataGridViewTextBoxColumn col_para_val = new DataGridViewTextBoxColumn();
                    col_para_val.Name = "para_val";
                    col_para_val.HeaderText = "参数值";
                    col_para_val.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvPar.Columns.Add(col_para_val);

                    foreach (var item in data)
                    {
                        int i = this.dgvPar.Rows.Add();
                        this.dgvPar.Rows[i].Cells["para_id"].Value = item.para_id;
                        this.dgvPar.Rows[i].Cells["name"].Value = item.name;
                        this.dgvPar.Rows[i].Cells["remark"].Value = item.remark;
                        //this.dgvPar.Rows[i].Cells["remark"].Style.ForeColor = CustomColor.GrayLight;
                        //this.dgvPar.Rows[i].Cells["para_type"].Value = Enums.Tools.SysParaType(item.para_type);
                        this.dgvPar.Rows[i].Cells["para_val"].Value = item.para_val;

                        if (i % 2 == 1)
                            this.dgvPar.Rows[i].DefaultCellStyle.BackColor = CustomColor.YellowLight;
                    }
                }
            }
        }

        private void ibSearch_Click(object sender, EventArgs e)
        {
            this.pageIndex = 1;
            this.BindPar();
        }

        private void ibFirst_Click(object sender, EventArgs e)
        {
            //首页
            if (this.pageIndex != 1)
            {
                this.pageIndex = 1;
                this.BindPar();
            }
        }

        private void ibPrev_Click(object sender, EventArgs e)
        {
            //上一页
            if (this.pageIndex > 1)
            {
                this.pageIndex -= 1;
                this.BindPar();
            }
        }

        private void ibNext_Click(object sender, EventArgs e)
        {
            //下一页
            if (this.pageTotal > 1 && this.pageIndex < this.pageTotal)
            {
                this.pageIndex += 1;
                this.BindPar();
            }
        }

        private void ibLast_Click(object sender, EventArgs e)
        {
            //尾页
            if (this.pageTotal > 1 && this.pageIndex != this.pageTotal)
            {
                this.pageIndex = this.pageTotal;
                this.BindPar();
            }
        }

        /// <summary>
        /// 禁止选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPar_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvPar.ClearSelection();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.sysParEdit.Bind(this, this.dgvPar.Rows[e.RowIndex].Cells["para_id"].Value.ToString());
            this.sysParEdit.Visible = true;
        }

    }//end class
}