using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatMes.Client.Entity.batmes_client;
using FireBusiness;
using FireBusiness.Model;
using FireBusiness.WCF;
using MyControls;

namespace FireControlApp
{
    /// <summary>
    /// 1、在代码中设置软件名称与Top标题；对Top控件进行初始化；
    /// 2、panMain中的控件进行自适应分辨率设置；
    /// </summary>
    public partial class Default : BatMes.Client.Controls.MasterDefault
    {
        public Default()
        {
            InitializeComponent();
        }

        private void Default_Load(object sender, EventArgs e)
        {
            CORE.Instance.Init();
            this.Text = CORE.Instance.SysPara<string>("ComTitle");
            base.top.TopTitle = this.Text;

            //Top初始化
            #region 交互日志用户控件

            BatMes.Client.Controls.SysLog sysLog = new BatMes.Client.Controls.SysLog();
            this.top.SysLog = sysLog;

            #endregion

            #region 系统参数用户控件

            BatMes.Client.Controls.SysPar sysPar = new BatMes.Client.Controls.SysPar();
            //核心业务逻辑
            sysPar.Client = CORE.Instance;
            this.top.SysPar = sysPar;

            #endregion

            #region 系统事件用户控件

            BatMes.Client.Controls.SysEvent sysEvent = new BatMes.Client.Controls.SysEvent();
            this.top.SysEvent = sysEvent;

            #endregion

            #region 软件服务用户控件

            BatMes.Client.Controls.Service service = new BatMes.Client.Controls.Service();
            service.Client = CORE.Instance;
            this.top.Service = service;

            #endregion

            #region panMain中控件自适应分辨率
            this.panMain.Size = new Size(
                BatMes.Client.Controls.Tools.ScreenWidth - 20 * 2,
                BatMes.Client.Controls.Tools.ScreenHeight - BatMes.Client.Controls.Tools.TOP_HEIGHT - BatMes.Client.Controls.Tools.BOTTOM_HEIGHT - 20 * 2);


            this.realLog.Height = this.panMain.Height - 20;
            this.realLog.Width = this.panMain.Width - 20;
            this.realLog.Location = new Point(10, 10);
            #endregion

            BindNoticeEvent();
            AutoScreen();
            DrawLine();
            CORE.Instance.StartService();
        }

        #region 订阅事件
        /// <summary>
        /// 和日志、界面进行交互
        /// </summary>
        private void BindNoticeEvent()
        {
            //订阅通用消息事件
            CORE.Instance.NoticeLogEvent += Instance_NoticeEvent;

            //程序非正常停止
            CORE.Instance.AbnormalExitEvent += Instance_AbnormalExitEvent;
            PublicDel.OnUpdateCellStatus += PublicDel_OnUpdateCellStatus;
            PublicDel.AutoUpdateCellStatus += PublicDel_AutoUpdateCellStatus;
            PublicDel.OnReflushFile += PublicDel_OnReflushFileCell;
            PublicDel.OnResetCell += PublicDel_OnResetCell;
            PublicDel.OnOutChange += PublicDel_OutChange;
            PublicDel.OnOutUnload += PublicDel_OutUnload;
        }

        /// <summary>
        /// 手工更新工步文件
        /// </summary>
        /// <param name="cellNo"></param>
        private void PublicDel_OnReflushFileCell(int cellNo)
        {
            var isOk = MessageBox.Show("确定要更新工步文件吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (isOk == DialogResult.OK)
                CoreBusiness.Hub.ReflushFileCell(cellNo);
            //MessageBox.Show(opVal.strMsg);
        }

        /// <summary>
        /// 手工复位
        /// </summary>
        /// <param name="cellNo"></param>
        private void PublicDel_OnResetCell(int cellNo)
        {
            var isOk = MessageBox.Show("确定要复位库位吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if(isOk == DialogResult.OK)
                CoreBusiness.Hub.ResetCell(cellNo);
        }

        private void PublicDel_OutChange(int cellNo)
        {
            var isOk = MessageBox.Show("确定要换库位吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (isOk == DialogResult.OK)
                CoreBusiness.Hub.OutChangeByHand(cellNo);
        }

        private void PublicDel_OutUnload(int cellNo)
        {
            var isOk = MessageBox.Show("确定要出盘吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (isOk == DialogResult.OK)
                CoreBusiness.Hub.OutUploadByHand(cellNo);
        }

        private void PublicDel_OnUpdateCellStatus(cell objCell)
        {
            var hasMsg = CoreBusiness.Hub.UptCellStatusByHand(objCell);
            if (!hasMsg.isOk)
            {
                var rclVal = string.Format(ControlMap.RclKey, objCell.row, objCell.col, objCell.lay);
                if (cellKeyValues.ContainsKey(rclVal))
                {
                    FireCell.ResetStatus(cellKeyValues[rclVal]);
                }
                MessageBox.Show(hasMsg.hasMsg, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void PublicDel_AutoUpdateCellStatus(cell objCell, string trayCode)
        {
            var rclVal = string.Format(ControlMap.RclKey, objCell.row, objCell.col, objCell.lay);
            if (cellKeyValues.ContainsKey(rclVal))
            {
                FireCell.UpdateCell(cellKeyValues[rclVal], objCell, trayCode);
            }
        }

        private void Instance_AbnormalExitEvent(object sender, EventArgs e)
        {
            if(e is LogEventArgs eventArgs)
            {
                LogShow(eventArgs.Message, eventArgs.EventLevel);
            }
        }

        /// <summary>
        /// 消息订阅，输出到日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_NoticeEvent(object sender, EventArgs e)
        {
            if (e is LogEventArgs eventArgs)
            {
                LogShow(eventArgs.Message, eventArgs.EventLevel);
            }
        }
        #endregion

        /// <summary>
        /// 自适应屏幕
        /// </summary>
        public void AutoScreen()
        {
            this.panMain.Size = new Size(
                BatMes.Client.Controls.Tools.ScreenWidth - 20 * 2,
                BatMes.Client.Controls.Tools.ScreenHeight - BatMes.Client.Controls.Tools.TOP_HEIGHT - BatMes.Client.Controls.Tools.BOTTOM_HEIGHT - 20 * 2);


            this.tabLine_1.Width = this.panMain.Width - 20;
            this.tabLine_1.Location = new Point(tabLine_1.Location.X, statusIndicator.Height + 10);

            this.realLog.Height = panMain.Height - this.tabLine_1.Height - statusIndicator.Height - 20;
            this.realLog.Width = this.panMain.Width;
            this.realLog.Location = new Point(tabLine_1.Location.X, tabLine_1.Location.Y + tabLine_1.Height + 10);
        }

        #region 绘制生产线
        /// <summary>
        /// 界面初始化时，模拟现实的生产线
        /// </summary>
        private void DrawLine()
        {
            LoadLine(1, tabLine_1);

            //异步获取所有库位信息
            List<cell> initCell = CoreBusiness.GreatHub().GetCellsAsync();
            Task.Run(() =>
            {
                foreach (var hasCell in initCell)
                {
                    var rclVal = $"{hasCell.row}-{hasCell.col}-{hasCell.lay}";
                    if (cellKeyValues.ContainsKey(rclVal))
                    {
                        var standCell = cellKeyValues[rclVal];
                        FireCell.UpdateCell(standCell, hasCell);
                    }
                }
            });
        }
        //库位控件集合
        private Dictionary<string, FireCell> cellKeyValues = new Dictionary<string, FireCell>();

        /// <summary>
        /// 加载生产线
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="parent"></param>
        private void LoadLine(int lineId, MyTableLayoutPanel parent)
        {
            var hasCol = lineId == 1 ? ControlMap.ColCountLine_1 : ControlMap.ColCountLine_2;
            Label visable = new Label
            {
                Text = " ",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = ControlMap.indicatorFont
            };
            parent.Controls.Add(visable);
            for (int col = 1; col <= hasCol; col++)
            {
                Label titLab = new Label()
                {
                    Text = $"第{col}列",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = ControlMap.indicatorFont
                };
                parent.Controls.Add(titLab);
            }
            var hasRow = lineId == 1 ? ControlMap.RowCountLine_1 : ControlMap.RowCountLine_2;
            for (int row = hasRow; row >= 1; row--)
            {
                Label layerLab = new Label()
                {
                    Text = $"第{row}层",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = ControlMap.indicatorFont
                };
                parent.Controls.Add(layerLab);
                for (int col = 1; col <= hasCol; col++)
                {
                    var rclVal = string.Format(ControlMap.RclKey, lineId, col, row);
                    FireCell newCell = new FireCell(this, rclVal)
                    {
                        RowVal = lineId,
                        ColVal = col,
                        LayVal = row,
                        BaseColor = ControlMap.baseColor,
                        Text = col + "-" + row,
                        IsDrawGlass = false,
                        InnerBorderColor = Color.Transparent,
                        BorderColor = ControlMap.borderColor,
                        ForeColor = ControlMap.foreColor,
                        Margin = ControlMap.padding,
                        Radius = ControlMap.radius,
                        RoundStyle = CCWin.SkinClass.RoundStyle.All,
                        MouseBaseColor = Color.Gainsboro
                    };
                    newCell.TipMsg = FireCell.GetTipMsg(newCell);
                    parent.Controls.Add(newCell);
                    cellKeyValues[rclVal] = newCell;
                }
            }
        }
        #endregion 绘制产线
    }
}
