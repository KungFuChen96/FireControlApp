using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    /// <summary>
    /// 图标按钮的图标类型
    /// </summary>
    public enum IconButtonType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        /// 启动
        /// </summary>
        Start,

        /// <summary>
        /// 停止
        /// </summary>
        Stop,

        /// <summary>
        /// 搜索
        /// </summary>
        Search,

        /// <summary>
        /// 文件
        /// </summary>
        File,

        /// <summary>
        /// 保存
        /// </summary>
        Save,

        /// <summary>
        /// 首页
        /// </summary>
        First,

        /// <summary>
        /// 尾页
        /// </summary>
        Last,

        /// <summary>
        /// 上一页
        /// </summary>
        Prev,

        /// <summary>
        /// 下一页
        /// </summary>
        Next,

        /// <summary>
        /// 垃圾箱
        /// </summary>
        Trash,

        /// <summary>
        /// 增加
        /// </summary>
        Plus,

        /// <summary>
        /// 检查
        /// </summary>
        Check,

        /// <summary>
        /// BUG
        /// </summary>
        Bug,

        /// <summary>
        /// 向上箭头
        /// </summary>
        ArrowUp,

        /// <summary>
        /// 向下箭头
        /// </summary>
        ArrowDown,

        /// <summary>
        /// 公交车
        /// </summary>
        Bus,

        /// <summary>
        /// 火车
        /// </summary>
        Train
    }

    /// <summary>
    /// 图标按钮
    /// </summary>
    public partial class IconButton : UserControl
    {
        public IconButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconButton_Load(object sender, EventArgs e)
        {
            this.labIcon.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, this.Font.Size, this.Font.Style);
            this.labIcon.Text = Tools.IconButtonTypeValue(this.iconType);

            this.labText.Font = new Font("微软雅黑", this.Font.Size, this.Font.Style);
        }

        /// <summary>
        /// 变更大小
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            this.labIcon.Height = this.Height;

            this.labText.Height = this.Height;
            this.labText.Width = this.Width - this.labIcon.Width;

            base.OnResize(e);
        }

        /// <summary>
        /// 变更字体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            this.labIcon.Font = Tools.FontFromMemorey(FontAwesome.FreeSolid900, this.Font.Size, this.Font.Style);
            this.labText.Font = new Font("微软雅黑", this.Font.Size, this.Font.Style);

            base.OnFontChanged(e);
        }

        private bool isActive = true;
        /// <summary>
        /// 控件是否可用
        /// </summary>
        public bool IsActive
        {
            get { return this.isActive; }
            set
            {
                this.isActive = value;
                if (this.isActive)
                {
                    this.BackColor = Color.FromArgb(0, 125, 184);
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    this.BackColor = Color.FromArgb(136, 136, 136);
                    this.Cursor = Cursors.No;
                }
            }
        }

        private IconButtonType iconType = IconButtonType.Start;
        /// <summary>
        /// 图标类型
        /// </summary>
        public IconButtonType IconType
        {
            get { return this.iconType; }
            set
            {
                this.iconType = value;
                this.labIcon.Text = Tools.IconButtonTypeValue(this.iconType);
            }
        }

        /// <summary>
        /// 图标区域宽度（像素）
        /// </summary>
        public int IconWidth
        {
            get { return this.labIcon.Width; }
            set
            {
                this.labIcon.Width = value;
                this.labText.Width = this.Width - this.labIcon.Width;
                this.labText.Left = this.labIcon.Width;
            }
        }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextValue
        {
            get { return this.labText.Text; }
            set { this.labText.Text = value; }
        }

        private void labIconButtonText_Click(object sender, EventArgs e)
        {
            if (this.isActive)
                base.OnClick(e);
        }

        private void labIconButtonIcon_Click(object sender, EventArgs e)
        {
            if (this.isActive)
                base.OnClick(e);
        }
    }
}
