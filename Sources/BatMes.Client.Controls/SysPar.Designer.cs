namespace BatMes.Client.Controls
{
    partial class SysPar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panTopZone = new System.Windows.Forms.FlowLayoutPanel();
            this.labSort = new System.Windows.Forms.Label();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.dgvPar = new System.Windows.Forms.DataGridView();
            this.panBottomZone = new System.Windows.Forms.FlowLayoutPanel();
            this.ibFirst = new BatMes.Client.Controls.IconButton();
            this.ibPrev = new BatMes.Client.Controls.IconButton();
            this.ibNext = new BatMes.Client.Controls.IconButton();
            this.ibLast = new BatMes.Client.Controls.IconButton();
            this.labPager = new System.Windows.Forms.Label();
            this.sysParEdit = new BatMes.Client.Controls.SysParEdit();
            this.panTopZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPar)).BeginInit();
            this.panBottomZone.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTopZone
            // 
            this.panTopZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panTopZone.Controls.Add(this.labSort);
            this.panTopZone.Controls.Add(this.cbSort);
            this.panTopZone.Location = new System.Drawing.Point(0, 0);
            this.panTopZone.Name = "panTopZone";
            this.panTopZone.Size = new System.Drawing.Size(1460, 70);
            this.panTopZone.TabIndex = 25;
            // 
            // labSort
            // 
            this.labSort.AutoSize = true;
            this.labSort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSort.Location = new System.Drawing.Point(20, 24);
            this.labSort.Margin = new System.Windows.Forms.Padding(20, 24, 0, 0);
            this.labSort.Name = "labSort";
            this.labSort.Size = new System.Drawing.Size(42, 21);
            this.labSort.TabIndex = 2;
            this.labSort.Text = "分类";
            // 
            // cbSort
            // 
            this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSort.FormattingEnabled = true;
            this.cbSort.Location = new System.Drawing.Point(67, 20);
            this.cbSort.Margin = new System.Windows.Forms.Padding(5, 20, 0, 0);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(200, 29);
            this.cbSort.TabIndex = 23;
            this.cbSort.SelectedIndexChanged += new System.EventHandler(this.cbSort_SelectedIndexChanged);
            // 
            // dgvPar
            // 
            this.dgvPar.AllowUserToAddRows = false;
            this.dgvPar.AllowUserToDeleteRows = false;
            this.dgvPar.AllowUserToResizeColumns = false;
            this.dgvPar.AllowUserToResizeRows = false;
            this.dgvPar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(215)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPar.ColumnHeadersHeight = 34;
            this.dgvPar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPar.EnableHeadersVisualStyles = false;
            this.dgvPar.Location = new System.Drawing.Point(0, 70);
            this.dgvPar.MultiSelect = false;
            this.dgvPar.Name = "dgvPar";
            this.dgvPar.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPar.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPar.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvPar.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPar.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvPar.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgvPar.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Transparent;
            this.dgvPar.RowTemplate.Height = 34;
            this.dgvPar.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvPar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPar.Size = new System.Drawing.Size(1460, 510);
            this.dgvPar.TabIndex = 26;
            this.dgvPar.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPar_CellDoubleClick);
            this.dgvPar.SelectionChanged += new System.EventHandler(this.dgvPar_SelectionChanged);
            // 
            // panBottomZone
            // 
            this.panBottomZone.BackColor = System.Drawing.Color.Transparent;
            this.panBottomZone.Controls.Add(this.ibFirst);
            this.panBottomZone.Controls.Add(this.ibPrev);
            this.panBottomZone.Controls.Add(this.ibNext);
            this.panBottomZone.Controls.Add(this.ibLast);
            this.panBottomZone.Controls.Add(this.labPager);
            this.panBottomZone.Location = new System.Drawing.Point(0, 580);
            this.panBottomZone.Name = "panBottomZone";
            this.panBottomZone.Size = new System.Drawing.Size(1460, 50);
            this.panBottomZone.TabIndex = 27;
            // 
            // ibFirst
            // 
            this.ibFirst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibFirst.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibFirst.IconType = BatMes.Client.Controls.IconButtonType.First;
            this.ibFirst.IconWidth = 50;
            this.ibFirst.IsActive = true;
            this.ibFirst.Location = new System.Drawing.Point(0, 20);
            this.ibFirst.Margin = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.ibFirst.Name = "ibFirst";
            this.ibFirst.Size = new System.Drawing.Size(120, 30);
            this.ibFirst.TabIndex = 13;
            this.ibFirst.TextValue = "首页";
            this.ibFirst.Click += new System.EventHandler(this.ibFirst_Click);
            // 
            // ibPrev
            // 
            this.ibPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibPrev.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibPrev.IconType = BatMes.Client.Controls.IconButtonType.Prev;
            this.ibPrev.IconWidth = 40;
            this.ibPrev.IsActive = true;
            this.ibPrev.Location = new System.Drawing.Point(130, 20);
            this.ibPrev.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.ibPrev.Name = "ibPrev";
            this.ibPrev.Size = new System.Drawing.Size(120, 30);
            this.ibPrev.TabIndex = 14;
            this.ibPrev.TextValue = "上一页";
            this.ibPrev.Click += new System.EventHandler(this.ibPrev_Click);
            // 
            // ibNext
            // 
            this.ibNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibNext.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibNext.IconType = BatMes.Client.Controls.IconButtonType.Next;
            this.ibNext.IconWidth = 40;
            this.ibNext.IsActive = true;
            this.ibNext.Location = new System.Drawing.Point(260, 20);
            this.ibNext.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.ibNext.Name = "ibNext";
            this.ibNext.Size = new System.Drawing.Size(120, 30);
            this.ibNext.TabIndex = 15;
            this.ibNext.TextValue = "下一页";
            this.ibNext.Click += new System.EventHandler(this.ibNext_Click);
            // 
            // ibLast
            // 
            this.ibLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(184)))));
            this.ibLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibLast.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibLast.IconType = BatMes.Client.Controls.IconButtonType.Last;
            this.ibLast.IconWidth = 50;
            this.ibLast.IsActive = true;
            this.ibLast.Location = new System.Drawing.Point(390, 20);
            this.ibLast.Margin = new System.Windows.Forms.Padding(10, 20, 0, 0);
            this.ibLast.Name = "ibLast";
            this.ibLast.Size = new System.Drawing.Size(120, 30);
            this.ibLast.TabIndex = 16;
            this.ibLast.TextValue = "尾页";
            this.ibLast.Click += new System.EventHandler(this.ibLast_Click);
            // 
            // labPager
            // 
            this.labPager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labPager.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPager.Location = new System.Drawing.Point(510, 25);
            this.labPager.Margin = new System.Windows.Forms.Padding(0, 25, 0, 0);
            this.labPager.Name = "labPager";
            this.labPager.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labPager.Size = new System.Drawing.Size(950, 21);
            this.labPager.TabIndex = 21;
            this.labPager.Text = "共  42  条记录  2/3  页";
            // 
            // sysParEdit
            // 
            this.sysParEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sysParEdit.Location = new System.Drawing.Point(20, 90);
            this.sysParEdit.Name = "sysParEdit";
            this.sysParEdit.Size = new System.Drawing.Size(1000, 630);
            this.sysParEdit.SysPar = null;
            this.sysParEdit.TabIndex = 28;
            this.sysParEdit.Visible = false;
            // 
            // SysPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sysParEdit);
            this.Controls.Add(this.panBottomZone);
            this.Controls.Add(this.dgvPar);
            this.Controls.Add(this.panTopZone);
            this.Name = "SysPar";
            this.Size = new System.Drawing.Size(1460, 630);
            this.Load += new System.EventHandler(this.SysPar_Load);
            this.panTopZone.ResumeLayout(false);
            this.panTopZone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPar)).EndInit();
            this.panBottomZone.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panTopZone;
        private System.Windows.Forms.Label labSort;
        private System.Windows.Forms.DataGridView dgvPar;
        private System.Windows.Forms.FlowLayoutPanel panBottomZone;
        private IconButton ibFirst;
        private IconButton ibPrev;
        private IconButton ibNext;
        private IconButton ibLast;
        private System.Windows.Forms.Label labPager;
        private System.Windows.Forms.ComboBox cbSort;
        private SysParEdit sysParEdit;
    }
}
