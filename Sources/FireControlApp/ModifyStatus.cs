using FireBusiness.Enums;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FireControlApp
{
    public partial class ModStatusForm : Form
    {
        /// <summary>
        /// 库位ID
        /// </summary>
        public int CellNo { get; set; }

        /// <summary>
        /// 库位状态
        /// </summary>
        public DeviceStatus CellStatus { get; set; }

        /// <summary>
        /// 行列层
        /// </summary>
        public string RCLVal { get; set; }

        /// <summary>
        /// 保存状态
        /// </summary>
        public DeviceStatus SaveStatus { get; set; }

        /// <summary>
        /// 点击保存OK
        /// </summary>
        public bool IsOk { get; set; } = false;

        public ModStatusForm(FireCell standingCell)
        {
            InitializeComponent();
            InitEditBox(this, standingCell);
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="statusForm"></param>
        /// <param name="standingCell"></param>
        public static void InitEditBox(ModStatusForm statusForm, FireCell standingCell)
        {
            statusForm.CellNo = standingCell.CellNo;
            statusForm.CellStatus = standingCell.Status;
            statusForm.RCLVal = $"{standingCell.RowVal}-{standingCell.ColVal}-{standingCell.LayVal}";
            statusForm.txtCellID.Text = statusForm.CellNo.ToString();
            statusForm.txtRcl.Text = statusForm.RCLVal;
            statusForm.txtCurrSt.Text =  ControlMap.Descriptions.ContainsKey(statusForm.CellStatus) ? ControlMap.Descriptions[statusForm.CellStatus] : "未知状态";
        }

        /// <summary>
        /// 点击保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            IsOk = true;
            SaveStatus = (DeviceStatus)cboxStatus.SelectedValue;
            this.Close();
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModStatusForm_Load(object sender, EventArgs e)
        {
            //绑定数据
            var showVal = new DeviceStatus[] { DeviceStatus.Normal };
            var loadData = ControlMap.Descriptions?.Where(t => showVal.Contains(t.Key));
            BindingSource bs = new BindingSource
            {
                DataSource = loadData
            };
            cboxStatus.DataSource = bs;
            cboxStatus.ValueMember = "Key";
            cboxStatus.DisplayMember = "Value";
            cboxStatus.SelectedValue = DeviceStatus.Normal;
        }
    }
}
