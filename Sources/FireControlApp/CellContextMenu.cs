using FireBusiness.Model;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FireControlApp
{
    public class CellContextMenu
    {
        public ContextMenuStrip contextMenuStrip1;

        #region 分容菜单--暂时隐藏
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
        #endregion

        #region 消防菜单
        //右键恢复正常状态
        public ToolStripMenuItem BackNormalCell;
        //点击恢复正常状态
        public EventHandler BackNormalClick;

        //右键取消喷淋
        public ToolStripMenuItem CancelSprayCell;
        //右键取消喷淋
        public EventHandler CancelSprayClick;

        //右键喷淋
        public ToolStripMenuItem DoSprayCell;
        //点击喷淋
        public EventHandler DoSprayClick;

        //右键停止喷淋
        public ToolStripMenuItem StopSprayCell;
        //点击停止喷淋
        public EventHandler StopSprayClick;

        #endregion
        public CellContextMenu(CellEvent eventModle)
        {
            this.EditCellClick = eventModle.EditCellClick;
            this.EditReflushClick = eventModle.EditReflushClick;
            this.ResetCellClick = eventModle.ResetClick;
            this.OutChangeClick = eventModle.OutChangeClick;
            this.OutUnloadClick = eventModle.OutUnloadClick;
            this.OutFinishClick = eventModle.OutFinishClick;
            this.InFinishClick = eventModle.InFinishClick;

            //消防
            this.BackNormalClick = eventModle.BackNormalClick;
            this.CancelSprayClick = eventModle.CancelSprayClick;
            this.DoSprayClick = eventModle.DoSprayClick;
            this.StopSprayClick = eventModle.StopSprayClick;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.contextMenuStrip1 = new ContextMenuStrip(new Container())
            {
                Name = "richRightClick",
            };
            //临时挂起控件的布局逻辑
            this.contextMenuStrip1.SuspendLayout();

            #region 分容逻辑--暂时不使用
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
            #endregion

            #region 消防逻辑
            this.BackNormalCell = new ToolStripMenuItem
            {
                Name = "BackNormalCell",
                Text = "恢复正常状态",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.BackNormalCell.Click += BackNormalClick;

            this.DoSprayCell = new ToolStripMenuItem
            {
                Name = "DoSprayCell",
                Text = "执行喷淋",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.DoSprayCell.Click += DoSprayClick;

            this.CancelSprayCell = new ToolStripMenuItem
            {
                Name = "CancelSprayCell",
                Text = "取消喷淋",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.CancelSprayCell.Click += CancelSprayClick;

            this.StopSprayCell = new ToolStripMenuItem
            {
                Name = "StopSprayCell",
                Text = "停止喷淋",
                ForeColor = Color.FromArgb(30, 57, 91),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.StopSprayCell.Click += StopSprayClick;
            #endregion
            //将右键项目添加到右键菜单中
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.BackNormalCell, this.DoSprayCell, this.StopSprayCell });
            //恢复正常的布局逻辑
            this.contextMenuStrip1.ResumeLayout(false);
        }
    }
}