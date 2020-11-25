using BatMes.Client.Entity.batmes_client;
using ExtFuncs;
using FireBusiness.Enums;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FireBusiness;
using FireBusiness.Model;

namespace FireControlApp
{
    public partial class FireCell : CskinExt.SkinBtnPlus
    {
        new Default Parent = null;

        CellContextMenu myMenu = null;

        public FireCell(Default parent, string SortNo)
        {
            Parent = parent;
            BackColor = Color.White;
            Dock = DockStyle.Fill;
            FadeGlow = false;
            IsDrawBorder = true;
            this.SortNo = SortNo;

            var evModel = new CellEvent
            {
                EditCellClick = new EventHandler(EditeItemClick),//编辑库位状态
                EditReflushClick = new EventHandler(ReflushClick),
                ResetClick = new EventHandler(ResetClick),
                OutChangeClick = new EventHandler(OutChangeClick),
                OutUnloadClick = new EventHandler(OutUnloadClick),
                OutFinishClick = new EventHandler(OutFinishClick),
                InFinishClick = new EventHandler(InFinishClick),
                BackNormalClick = new EventHandler(BackNormalClick),
                CancelSprayClick = new EventHandler(CancelSprayClick),
                DoSprayClick = new EventHandler(DoSprayClick),
                StopSprayClick = new EventHandler(StopSprayClick)
            };
            myMenu = new CellContextMenu(evModel);
            this.ContextMenuStrip = myMenu.contextMenuStrip1;
            this.MouseUp += StandbyCell_MouseUp;
        }

        private void StandbyCell_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //myMenu.EditCell.Enabled = this.CellNo >= 0;
                //myMenu.ReflushCell.Enabled = this.CellNo >= 0 && Status == DeviceStatus.Normal;
                //myMenu.ResetCell.Enabled = this.CellNo >= 0 && ControlMap.HasOneExecStatuses.Contains(Status);
                //myMenu.OutFinishByHand.Enabled = this.CellNo >= 0 && ControlMap.HasOneExecStatuses.Contains(Status);
                //myMenu.InFinishByHand.Enabled = this.CellNo >= 0 && Status == DeviceStatus.Normal;
                //myMenu.OutChangeCell.Enabled = this.CellNo >= 0 && ControlMap.HasOneExecStatuses.Contains(Status);
                //myMenu.OutUnloadCell.Enabled = this.CellNo >= 0 && ControlMap.HasOneExecStatuses.Contains(Status);

                myMenu.BackNormalCell.Enabled = this.CellNo >= 0 && Status != DeviceStatus.Normal;
                myMenu.CancelSprayCell.Enabled = this.CellNo >= 0 && Status != DeviceStatus.Normal;
                myMenu.DoSprayCell.Enabled = this.CellNo >= 0;
                myMenu.StopSprayCell.Enabled = this.CellNo >= 0;
                myMenu.contextMenuStrip1.Show(this, new Point(e.X, e.Y));
            }
        }

        #region Fields
        /// <summary>
        /// 本地排序编号
        /// </summary>
        public string SortNo { get; set; }

        /// <summary>
        /// 库位编号
        /// </summary>
        public int CellNo { get; set; } = -1;

        /// <summary>
        /// 库位位置
        /// </summary>
        public FirePost FirePost { get; set; }

        /// <summary>
        /// 库位属性描述
        /// </summary>
        public string PostDesc { get; set; }

        /// <summary>
        /// 库位状态
        /// </summary>
        private DeviceStatus _status = DeviceStatus.Normal;

        /// <summary>
        /// 设置库位状态，同时设置库位背景色
        /// </summary>
        public DeviceStatus Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    //喷淋的优先级是最高的，喷淋颜色只能恢复正常颜色
                    if (_status == DeviceStatus.SprayByHand && value != DeviceStatus.Normal)
                        return;
                    this.BaseColor(ControlMap.ColorMap[value]);
                    _status = value;
                }
            }
        }

        public string ActionDes { get; set; }

        /// <summary>
        /// 托盘编码
        /// </summary>
        public string TrayCode { get; set; }

        /// <summary>
        /// 最短静置时间
        /// </summary>
        public int StandbyTime { get; set; }

        /// <summary>
        /// 异常代码
        /// </summary>
        public string WarningCode { get; set; }

        /// <summary>
        /// 行
        /// </summary>
        public int RowVal { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public int ColVal { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        public int LayVal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string RemarkMsg { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LastUptDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 上一次修改的状态
        /// </summary>
        public DeviceStatus PreStatus { get; set; }

        #endregion Fields

        #region 获取TipMsg

        //private StringBuilder sbVal = new StringBuilder();

        /// <summary>
        /// 获取提示框内容
        /// </summary>
        /// <param name="thisCell"></param>
        /// <returns></returns>
        public static string GetTipMsg(FireCell thisCell)
        {
            //if (thisCell.CellNo < 0) return string.Empty;
            StringBuilder sbVal = new StringBuilder();
            if (thisCell.CellNo >= 0)
                sbVal.Append($"MES编码：{thisCell.CellNo}");
            sbVal.Append($"\r\n库位类型：{thisCell.PostDesc}");
            sbVal.Append($"\r\n行列层：{thisCell.RowVal}-{thisCell.ColVal}-{thisCell.LayVal}");
            sbVal.Append($"\r\n库位状态：{ControlMap.Descriptions[thisCell.Status]}");
            if(!string.IsNullOrEmpty(thisCell.ActionDes))
                sbVal.Append($"\r\n消防动作：{thisCell.ActionDes}");
            if (!string.IsNullOrEmpty(thisCell.TrayCode))
                sbVal.Append($"\r\n托盘条码：{thisCell.TrayCode}");
            if (!string.IsNullOrEmpty(thisCell.RemarkMsg))
                sbVal.Append($"\r\n备注信息：{thisCell.RemarkMsg}");
            sbVal.Append($"\r\n变更时间：{thisCell.LastUptDate}");
            return sbVal.ToString();
        }

        /// <summary>
        /// 更新库位控件
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="cell"></param>
        public static void UpdateCell(FireCell oldVal, cell newVal, string trayCode = null, bool isInit = false)
        {
            var postType = (FirePost)newVal.type;
            oldVal.PostDesc = ControlMap.PostDescMap.ContainsKey(postType) ? ControlMap.PostDescMap[postType] : "未知";
            oldVal.TrayCode = trayCode;
            oldVal.CellNo = newVal.cell_id;
            oldVal.LastUptDate = newVal.last_update_time;
            oldVal.RemarkMsg = newVal.remark;
            oldVal.ActionDes = newVal.extend_field2;
            //if (!isInit && oldVal.Status == (DeviceStatus)newVal.cell_status)
            //    return;
            
            oldVal.PreStatus = oldVal.Status;
            oldVal.Status = (DeviceStatus)newVal.cell_status;
            oldVal.TipMsg = GetTipMsg(oldVal);
        }
        
        /// <summary>
        /// 仅更新备注
        /// </summary>
        /// <param name="oldVal"></param>
        /// <param name="remarkMsg"></param>
        public static void OnlyUptRemark(FireCell oldVal, string remarkMsg)
        {
            oldVal.RemarkMsg = remarkMsg;
            oldVal.TipMsg = GetTipMsg(oldVal);
        }

        /// <summary>
        /// 恢复上一次状态
        /// </summary>
        /// <param name="oldVal"></param>
        public static void ResetStatus(FireCell oldVal)
        {
            oldVal.Status = oldVal.PreStatus;
            oldVal.TipMsg = GetTipMsg(oldVal);
        }
        #endregion 获取TipMsg

        #region 右键修改库位
        /// <summary>
        /// 右键点击修改状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditeItemClick(object sender, EventArgs e)
        {
            ModStatusForm modifyStatus = new ModStatusForm(this);
            modifyStatus.StartPosition = FormStartPosition.CenterScreen;
            modifyStatus.ShowDialog();
            if (modifyStatus.IsOk)
            {
                this.PreStatus = this.Status;
                this.Status = modifyStatus.SaveStatus;
                var uptCell = new cell
                {
                    cell_id = CellNo,
                    cell_status = (int)Status,
                    last_update_time = DateTime.Now,
                    row = RowVal,
                    col = ColVal,
                    lay = LayVal
                };
                this.TipMsg = GetTipMsg(this);
                PublicDel.OnUpdateCellStatus?.Invoke(uptCell);
            }
        }
        #endregion 右键修改库位

        #region 刷新工步
        private void ReflushClick(object sender, EventArgs e)
        {
            PublicDel.OnReflushFile?.Invoke(this.CellNo);
        }
        #endregion

        #region 复位库位
        private void ResetClick(object sender, EventArgs e)
        {
            PublicDel.OnResetCell?.Invoke(this.CellNo);
        }
        #endregion

        #region 请求切换库位
        private void OutChangeClick(object sender, EventArgs e)
        {
            PublicDel.OnOutChange?.Invoke(this.CellNo);
        }
        #endregion

        #region 请求出盘
        private void OutUnloadClick(object sender, EventArgs e)
        {
            PublicDel.OnOutUnload?.Invoke(this.CellNo);
        }
        #endregion

        #region 手工取走托盘
        private void OutFinishClick(object sender, EventArgs e)
        {
            PublicDel.OnOutFinish?.Invoke(this.CellNo);
        }
        #endregion

        #region 手工下发压合
        private void InFinishClick(object sender, EventArgs e)
        {
            PublicDel.OnInFinish?.Invoke(this.CellNo);
        }
        #endregion

        #region 恢复正常状态
        private void BackNormalClick(object sender, EventArgs e)
        {
            PublicDel.BackNormal?.Invoke(this.CellNo, $"{RowVal}-{ColVal}-{LayVal}");
        }
        #endregion

        #region 取消喷淋，永不会喷淋
        private void CancelSprayClick(object sender, EventArgs e)
        {
            PublicDel.CancelSpray?.Invoke(this.CellNo, $"{RowVal}-{ColVal}-{LayVal}");
        }
        #endregion

        #region 喷淋
        private void DoSprayClick(object sender, EventArgs e)
        {
            PublicDel.DoSpray?.Invoke(this.CellNo, $"{RowVal}-{ColVal}-{LayVal}", this.FirePost);
        }
        #endregion

        #region 停止喷淋过程
        private void StopSprayClick(object sender, EventArgs e)
        {
            PublicDel.StopSpray?.Invoke(this.CellNo, $"{RowVal}-{ColVal}-{LayVal}", this.FirePost);
        }
        #endregion

        #region 非主线程弹出消息框
        public DialogResult ShowMessage(string title, string msg, MessageBoxButtons button = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            Func<DialogResult> fuc = () => MessageBox.Show(Parent, msg, title, button, icon);
            return (DialogResult)Parent.Invoke(fuc);
        }
        #endregion
    }
}