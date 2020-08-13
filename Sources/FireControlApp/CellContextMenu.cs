using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FireControlApp
{
    public class CellContextMenu
    {
        public ContextMenuStrip contextMenuStrip1;

        //右键修改库位信息
        public ToolStripMenuItem EditCell;

        //右键修改库位信息操作
        public EventHandler EditCellClick;

        //右键刷新
        public ToolStripMenuItem ReflushCell;

        //右键刷新
        public EventHandler EditReflushClick;

        //右键复位
        public ToolStripMenuItem ResetCell;
        //右键点击复位
        public EventHandler ResetCellClick;

        //右键请求换库位
        public ToolStripMenuItem OutChangeCell;
        //右键点击请求换库位
        public EventHandler OutChangeClick;

        //右键请求出盘
        public ToolStripMenuItem OutUnloadCell;
        //右键点击请求出盘
        public EventHandler OutUnloadClick;

        //右键出盘完成
        public ToolStripMenuItem OutFinishByHand;
        //点击出盘完成
        public EventHandler OutFinishClick;
        //右键出盘完成
        public ToolStripMenuItem InFinishByHand;
        //点击出盘完成
        public EventHandler InFinishClick;
        public CellContextMenu(EventHandler EditCellClick, EventHandler EditReflushClick, EventHandler ResetClick, EventHandler OutChangeClick, EventHandler OutUnloadClick,
                               EventHandler OutFinishClick, EventHandler InFinishClick)
        {
            this.EditCellClick = EditCellClick;
            this.EditReflushClick = EditReflushClick;
            this.ResetCellClick = ResetClick;
            this.OutChangeClick = OutChangeClick;
            this.OutUnloadClick = OutUnloadClick;
            this.OutFinishClick = OutFinishClick;
            this.InFinishClick = InFinishClick;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.contextMenuStrip1 = new ContextMenuStrip(new Container())
            {
                Name = "richRightClick",
                //Size = new Size(0, 0)
            };
            //临时挂起控件的布局逻辑
            this.contextMenuStrip1.SuspendLayout();

            this.EditCell = new ToolStripMenuItem
            {
                Name = "EditCell",
                Text = "修改状态",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.EditCell.Click += EditCellClick;

            this.ReflushCell = new ToolStripMenuItem
            {
                Name = "EditReflush",
                Text = "更新工步",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.ReflushCell.Click += EditReflushClick;

            this.ResetCell = new ToolStripMenuItem
            {
                Name = "EditReset",
                Text = "复位库位",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.ResetCell.Click += ResetCellClick;

            this.OutChangeCell = new ToolStripMenuItem
            {
                Name = "EditOutChange",
                Text = "请求换库位",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.OutChangeCell.Click += OutChangeClick;

            this.OutUnloadCell = new ToolStripMenuItem
            {
                Name = "EditOutUnload",
                Text = "请求出盘",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.OutUnloadCell.Click += OutUnloadClick;

            this.OutFinishByHand = new ToolStripMenuItem
            {
                Name = "EditOutFinish",
                Text = "取盘完成",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.OutFinishByHand.Click += OutFinishClick;

            this.InFinishByHand = new ToolStripMenuItem
            {
                Name = "EditInFinish",
                Text = "放盘完成",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.InFinishByHand.Click += InFinishClick;
            //将右键项目添加到右键菜单中
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.EditCell, this.ReflushCell, this.ResetCell, this.OutChangeCell, this.OutUnloadCell });
            //恢复正常的布局逻辑
            this.contextMenuStrip1.ResumeLayout(false);
        }
    }
}