using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatMes.Client.Entity.batmes_client;
using FireBusiness;
using FireBusiness.Enums;
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

            PublicDel.BackNormal += PublicDel_OnBackNormal;
            PublicDel.DoSpray += PublicDel_OnDoSpray;
            PublicDel.CancelSpray += PublicDel_OnCancelSpray;
            PublicDel.StopSpray += PublicDel_OnStopSpray;
        }

        /// <summary>
        /// 恢复正常状态
        /// </summary>
        /// <param name="cellNo"></param>
        private void PublicDel_OnBackNormal(int cellNo, string rclVal)
        {
            var isOk = MessageBox.Show($"库位编号{cellNo}，行列层{rclVal}，确定要恢复正常状态吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (isOk == DialogResult.OK)
                CoreBusiness.Hub.OnBackNormal(cellNo);
        }

        /// <summary>
        /// 手工喷淋
        /// </summary>
        /// <param name="cellNo"></param>
        private void PublicDel_OnDoSpray(int cellNo, string rclVal, FirePost firePost)
        {
            if (ControlMap.NeedPassWord)
            {
                InputPswForm inputPsw = new InputPswForm()
                {
                    StartPosition = FormStartPosition.CenterScreen
                };
                inputPsw.ShowDialog();
                if (!inputPsw.IsOk)
                {
                    return;
                }
            }
            
            var isOk = MessageBox.Show($"库位编号{cellNo}，行列层{rclVal}，确定要喷淋吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (isOk == DialogResult.OK)
                CoreBusiness.Hub.OnDoSpray(cellNo, firePost);
        }

        /// <summary>
        /// 手工取消喷淋
        /// </summary>
        /// <param name="cellNo"></param>
        private void PublicDel_OnCancelSpray(int cellNo, string rclVal)
        {
            var isOk = MessageBox.Show($"库位编号{cellNo}，行列层{rclVal}，确定要取消喷淋吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (isOk == DialogResult.OK)
                CoreBusiness.Hub.OnCancelSpray(cellNo);
        }

        /// <summary>
        /// 喷淋过程中停止喷淋
        /// </summary>
        /// <param name="cellNo"></param>
        private void PublicDel_OnStopSpray(int cellNo, string rclVal, FirePost firePost)
        {
            var isOk = MessageBox.Show($"库位编号{cellNo}，行列层{rclVal}，确定停止喷淋吗？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (isOk == DialogResult.OK)
                CoreBusiness.Hub.OnStopSpray(cellNo, firePost);
        }

        private void PublicDel_OnUpdateCellStatus(cell objCell)
        {
            var hasMsg = CoreBusiness.Hub.UptCellStatusByHand(objCell);
            if (!hasMsg.isOk)
            {
                var rclVal = string.Format(ControlMap.RclKey, objCell.type, objCell.row, objCell.col, objCell.lay);
                if (cellKeyValues.ContainsKey(rclVal))
                {
                    FireCell.ResetStatus(cellKeyValues[rclVal]);
                }
                MessageBox.Show(hasMsg.hasMsg, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void PublicDel_AutoUpdateCellStatus(cell objCell, string remarkCode)
        {
            var rclVal = string.Format(ControlMap.RclKey, objCell.type, objCell.row, objCell.col, objCell.lay);
            if (cellKeyValues.ContainsKey(rclVal))
            {
                if(!string.IsNullOrEmpty(remarkCode))
                    FireCell.OnlyUptRemark(cellKeyValues[rclVal], remarkCode);
                else
                    FireCell.UpdateCell(cellKeyValues[rclVal], objCell, remarkCode);
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
                BatMes.Client.Controls.Tools.ScreenWidth - 0 * 1,
                BatMes.Client.Controls.Tools.ScreenHeight - BatMes.Client.Controls.Tools.TOP_HEIGHT - BatMes.Client.Controls.Tools.BOTTOM_HEIGHT - 0 * 1);

            //常温静置
            this.gBox_FcS.Width = this.panMain.Width - 10;
            this.gBox_FcS.Location = new Point(statusIndicator.Location.X, statusIndicator.Height + 10);
            var sAverHight = (gBox_FcS.Height - 40) / 2;
            this.tabLine_FcS1.Width = this.panMain.Width - 20;
            this.tabLine_FcS1.Location = new Point(tabLine_FcS1.Location.X, gBox_FcS.Location.Y - 10);
            this.tabLine_FcS1.Height = sAverHight;
            this.tabLine_FcS2.Width = this.panMain.Width - 20;
            this.tabLine_FcS2.Location = new Point(tabLine_FcS1.Location.X, tabLine_FcS1.Location.Y + tabLine_FcS1.Height + 10);
            this.tabLine_FcS2.Height = sAverHight;

            //分容压床
            this.gBox_Fc.Width = this.panMain.Width - 10;
            //this.gBox_Fc.Location = new Point(gBox_FcS.Location.X, gBox_FcS.Location.Y + gBox_FcS.Height + 10);
            this.tabLine_Fc.Width = this.panMain.Width - 20;
            //this.tabLine_Fc.Location = new Point(tabLine_FcS1.Location.X, gBox_Fc.Location.Y - 10);

            ////高温静置
            this.gBox_Hot.Width = this.panMain.Width - 10;
            //this.gBox_Hot.Location = new Point(gBox_Fc.Location.X, gBox_Fc.Location.Y + gBox_Fc.Height + 10);
            var hAverHight = (gBox_FcS.Height - 40) / 2;
            this.tabLine_Hot1.Width = this.panMain.Width - 20;
            this.tabLine_Hot1.Height = hAverHight;
            //this.tabLine_Hot1.Location = new Point(tabLine_FcS1.Location.X, tabLine_Fc.Location.Y + tabLine_Fc.Height + 10);
            this.tabLine_Hot2.Width = this.panMain.Width - 20;
            this.tabLine_Hot2.Height = hAverHight;
            this.tabLine_Hot2.Location = new Point(tabLine_FcS1.Location.X, tabLine_Hot1.Location.Y + tabLine_Hot1.Height + 10);

            //日志
            this.realLog.Height = panMain.Height - (gBox_Hot.Location.Y + gBox_Hot.Height) - 10;
            this.realLog.Width = this.panMain.Width;
            this.realLog.Location = new Point(tabLine_FcS1.Location.X, gBox_Hot.Location.Y + gBox_Hot.Height + 10);
        }

        #region 绘制生产线
        /// <summary>
        /// 界面初始化时，模拟现实的生产线
        /// </summary>
        private void DrawLine()
        {
            LoadLine(1, tabLine_FcS1, FirePost.FcStandby);
            LoadLine(2, tabLine_FcS2, FirePost.FcStandby);
            LoadLine(1, tabLine_Fc, FirePost.Fc, false);
            LoadLine(1, tabLine_Hot1, FirePost.HotStandby);
            LoadLine(2, tabLine_Hot2, FirePost.HotStandby);

            //异步获取所有库位信息
            List<cell> initCell = CoreBusiness.GreatHub().GetCellsAsync();
            Task.Run(() =>
            {
                foreach (var hasCell in initCell)
                {
                    var rclVal = $"{hasCell.type}-{hasCell.row}-{hasCell.col}-{hasCell.lay}";
                    if (cellKeyValues.ContainsKey(rclVal))
                    {
                        var standCell = cellKeyValues[rclVal];
                        FireCell.UpdateCell(standCell, hasCell, null, true);
                    }
                }
            });
        }
        //库位控件集合
        private ConcurrentDictionary<string, FireCell> cellKeyValues = new ConcurrentDictionary<string, FireCell>();


        /// <summary>
        /// 加载生产线
        /// </summary>
        /// <param name="rowId">行</param>
        /// <param name="parent">控件</param>
        /// <param name="firePost">位置</param>
        /// <param name="isReverse">是否倒序显示</param>
        private void LoadLine(int rowId, MyTableLayoutPanel parent, FirePost firePost, bool isReverse = true)
        {
            var hasCol = ControlMap.PostCountMap.ContainsKey((firePost, rowId)) ? ControlMap.PostCountMap[(firePost, rowId)].colVal : 0;
            var hasRow = ControlMap.PostCountMap.ContainsKey((firePost, rowId)) ? ControlMap.PostCountMap[(firePost, rowId)].rowVal : 0;
            var filterCell = ControlMap.NoOpenVal.Split(',');
            for (int row = hasRow; row >= 1; row--)
            {
                var col = isReverse ? hasCol : 1;
                while(col >= 1 && col <= hasCol)
                {   
                    //过滤不显示的库位
                    var rclVal = string.Format(ControlMap.RclKey, (int)firePost, rowId, col, row);
                    if(filterCell.Any(t => rclVal.StartsWith(t)))
                    {
                        Label emptyLab = new Label();
                        parent.Controls.Add(emptyLab);
                        col = isReverse ? (col - 1) : (col + 1);
                        continue;
                    }
                    FireCell newCell = new FireCell(this, rclVal)
                    {
                        RowVal = rowId,
                        ColVal = col,
                        LayVal = row,
                        FirePost = firePost,
                        PostDesc = ControlMap.PostDescMap.ContainsKey(firePost) ? ControlMap.PostDescMap[firePost] : "未知",
                        BaseColor = ControlMap.baseColor,
                        IsDrawGlass = false,
                        InnerBorderColor = Color.Transparent,
                        BorderColor = ControlMap.borderColor,
                        ForeColor = ControlMap.foreColor,
                        Margin = ControlMap.padding,
                        Radius = ControlMap.radius,
                        RoundStyle = CCWin.SkinClass.RoundStyle.All,
                        MouseBaseColor = Color.Gainsboro
                    };
                    newCell.Text = firePost == FirePost.Fc ? col + "-" + row : rowId + "-" + col + "-" + row;
                    newCell.Font = new Font("微软雅黑", 5, FontStyle.Bold);
                    newCell.TipMsg = FireCell.GetTipMsg(newCell);
                    parent.Controls.Add(newCell);
                    cellKeyValues[rclVal] = newCell;
                    col = isReverse ? (col - 1) : (col + 1);
                }
            }
        }
        #endregion 绘制产线
    }
}
