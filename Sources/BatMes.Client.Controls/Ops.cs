using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public partial class Ops : UserControl
    {
        private const int pageSize = 15;
        private int pageIndex = 1;
        private int pageTotal = 0;

        public Ops()
        {
            InitializeComponent();
        }

        private void Ops_Load(object sender, EventArgs e)
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
            this.dgvOps.Width = _width;
            decimal _target_height = _height - 70 - 50;
            int _row_height = (int)Math.Floor(_target_height / 16);
            this.dgvOps.ColumnHeadersHeight = _row_height;
            this.dgvOps.RowTemplate.Height = _row_height;
            this.dgvOps.Height = _row_height * 16;
            //设置底部分页区域宽与位置
            this.panBottomZone.Width = _width;
            this.panBottomZone.Location = new Point(0, 70 + (int)_target_height);
            //设备底部文字区域宽
            this.labPager.Width = _width - 120 * 4 - 10 * 3;

            this.BindOps();
        }

        public void BindOps()
        {
            if (this.pageIndex < 1)
                this.pageIndex = 1;

            var data = Business.Instance.OpsList(pageSize, this.pageIndex);
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
            this.dgvOps.Columns.Clear();
            this.dgvOps.Rows.Clear();
            if (data != null && data.Count() > 0)
            {
                //添加列
                //ID
                DataGridViewTextBoxColumn col_ops_id = new DataGridViewTextBoxColumn();
                col_ops_id.Name = "ops_id";
                col_ops_id.Visible = false;
                this.dgvOps.Columns.Add(col_ops_id);
                //名称
                DataGridViewTextBoxColumn col_name = new DataGridViewTextBoxColumn();
                col_name.Name = "ops_name";
                col_name.HeaderText = "名称";
                col_name.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvOps.Columns.Add(col_name);
                //值
                DataGridViewTextBoxColumn col_val = new DataGridViewTextBoxColumn();
                col_val.Name = "ops_val";
                col_val.HeaderText = "值";
                col_val.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvOps.Columns.Add(col_val);
                //备注
                DataGridViewTextBoxColumn col_remark = new DataGridViewTextBoxColumn();
                col_remark.Name = "remark";
                col_remark.HeaderText = "备注";
                col_remark.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvOps.Columns.Add(col_remark);

                foreach (var item in data)
                {
                    int i = this.dgvOps.Rows.Add();
                    this.dgvOps.Rows[i].Cells["ops_id"].Value = item.ops_id;
                    this.dgvOps.Rows[i].Cells["ops_name"].Value = item.ops_name;
                    this.dgvOps.Rows[i].Cells["ops_val"].Value = item.ops_val;
                    this.dgvOps.Rows[i].Cells["remark"].Value = item.remark;

                    if (i % 2 == 1)
                        this.dgvOps.Rows[i].DefaultCellStyle.BackColor = CustomColor.YellowLight;
                }
            }
        }

        private void ibFirst_Click(object sender, EventArgs e)
        {
            //首页
            if (this.pageIndex != 1)
            {
                this.pageIndex = 1;
                this.BindOps();
            }
        }

        private void ibPrev_Click(object sender, EventArgs e)
        {
            //上一页
            if (this.pageIndex > 1)
            {
                this.pageIndex -= 1;
                this.BindOps();
            }
        }

        private void ibNext_Click(object sender, EventArgs e)
        {
            //下一页
            if (this.pageTotal > 1 && this.pageIndex < this.pageTotal)
            {
                this.pageIndex += 1;
                this.BindOps();
            }
        }

        private void ibLast_Click(object sender, EventArgs e)
        {
            //尾页
            if (this.pageTotal > 1 && this.pageIndex != this.pageTotal)
            {
                this.pageIndex = this.pageTotal;
                this.BindOps();
            }
        }

        private void ibAdd_Click(object sender, EventArgs e)
        {
            //新增
            this.opsAdd.Bind(this);
            this.opsAdd.Visible = true;
        }

        private void dgvOps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //编辑与删除
            this.opsEdit.Bind(this, (int)this.dgvOps.Rows[e.RowIndex].Cells["ops_id"].Value);
            this.opsEdit.Visible = true;
        }

        /// <summary>
        /// 禁止选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOps_SelectionChanged(object sender, EventArgs e)
        {
            this.dgvOps.ClearSelection();
        }

    }//end class
}
