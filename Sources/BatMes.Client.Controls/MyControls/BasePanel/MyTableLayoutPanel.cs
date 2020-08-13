using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Tools;

namespace MyControls
{
    public class MyTableLayoutPanel : TableLayoutPanel
    {
        public MyTableLayoutPanel()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            BorderStyle = BorderStyle.None;
        }

        #region Field

        private bool _M_DrawBackground = true;

        [Category("0Mine")]
        [Description("是否填充颜色")]
        public bool M_DrawBackground
        {
            get { return _M_DrawBackground; }
            set
            {
                if (value == _M_DrawBackground) return;
                _M_DrawBackground = value;
                Invalidate();
            }
        }

        private bool _M_DrawBorder = true;

        [Category("0Mine")]
        [Description("是否画边框")]
        public bool M_DrawBorder
        {
            get { return _M_DrawBorder; }
            set
            {
                if (value == _M_DrawBorder) return;
                _M_DrawBorder = value;
                Invalidate();
            }
        }

        private Color _M_BorderColor = Color.Red;

        [Category("0Mine")]
        [Description("边框颜色")]
        public Color M_BorderColor
        {
            get { return _M_BorderColor; }
            set
            {
                if (value == _M_BorderColor) return;
                _M_BorderColor = value;
                Invalidate();
            }
        }

        private int _M_BorderWidth = 1;

        [Category("0Mine")]
        [Description("边框宽度")]
        public int M_BorderWidth
        {
            get { return _M_BorderWidth; }
            set
            {
                if (_M_BorderWidth == value) return;
                _M_BorderWidth = value;
                Invalidate();
            }
        }

        private bool _M_DrawInnerBorder = false;

        [Category("0Mine")]
        [Description("是否画内边框")]
        public bool M_DrawInnerBorder
        {
            get { return _M_DrawInnerBorder; }
            set
            {
                if (_M_DrawInnerBorder == value) return;
                _M_DrawInnerBorder = value;
                Invalidate();
            }
        }

        private Color _M_InnerBorderColor = Color.Empty;

        [Category("0Mine")]
        [Description("内边框颜色")]
        public Color M_InnerBorderColor
        {
            get { return _M_InnerBorderColor; }
            set
            {
                if (_M_InnerBorderColor == value) return;
                _M_InnerBorderColor = value;
                Invalidate();
            }
        }

        private int _M_InnerBorderWidth = 1;

        [Category("0Mine")]
        [Description("内边框宽度")]
        public int M_InnerBorderWidth
        {
            get { return _M_InnerBorderWidth; }
            set
            {
                if (_M_InnerBorderWidth == value) return;
                _M_InnerBorderWidth = value;
                Invalidate();
            }
        }

        private bool _M_DrawLinear = false;

        [Category("0Mine")]
        [Description("是否启用渐变色")]
        public bool M_DrawLinear
        {
            get { return _M_DrawLinear; }
            set
            {
                if (_M_DrawLinear == value) return;
                _M_DrawLinear = value;
                Invalidate();
            }
        }

        private LinearGradientMode _M_LinearModel = LinearGradientMode.ForwardDiagonal;

        [Category("0Mine")]
        [Description("渐变模式")]
        public LinearGradientMode M_LinearModel
        {
            get { return _M_LinearModel; }
            set
            {
                if (_M_LinearModel == value) return;
                _M_LinearModel = value;
                Invalidate();
            }
        }

        private Color _M_Linear_StartColor = Color.Orange;

        [Category("0Mine")]
        [Description("渐变起始颜色")]
        public Color M_Linear_StartColor
        {
            get { return _M_Linear_StartColor; }
            set
            {
                if (_M_Linear_StartColor == value) return;
                _M_Linear_StartColor = value;
                Invalidate();
            }
        }

        private Color _M_Linear_EndColor = Color.Red;

        [Category("0Mine")]
        [Description("渐变终止色")]
        public Color M_Linear_EndColor
        {
            get { return _M_Linear_EndColor; }
            set
            {
                if (_M_Linear_EndColor == value) return;
                _M_Linear_EndColor = value;
                Invalidate();
            }
        }

        private int _M_Radius_LeftTop = 30;

        [Category("0Mine")]
        [Description("左上角圆角弧度")]
        public int M_Radius_LeftTop
        {
            get { return _M_Radius_LeftTop; }
            set
            {
                if (_M_Radius_LeftTop == value) return;
                _M_Radius_LeftTop = value;
                Invalidate();
            }
        }

        private int _M_Radius_LeftBottom = 30;

        [Category("0Mine")]
        [Description("左下角圆角弧度")]
        public int M_Radius_LeftBottom
        {
            get { return _M_Radius_LeftBottom; }
            set
            {
                if (_M_Radius_LeftBottom == value) return;
                _M_Radius_LeftBottom = value;
                Invalidate();
            }
        }

        private int _M_Radius_RightTop = 30;

        [Category("0Mine")]
        [Description("右上角圆角弧度")]
        public int M_Radius_RightTop
        {
            get { return _M_Radius_RightTop; }
            set
            {
                if (_M_Radius_RightTop == value) return;
                _M_Radius_RightTop = value;
                Invalidate();
            }
        }

        private int _M_Radius_RightBottom = 30;

        [Category("0Mine")]
        [Description("右下角圆角弧度")]
        public int M_Radius_RightBottom
        {
            get { return _M_Radius_RightBottom; }
            set
            {
                if (_M_Radius_RightBottom == value) return;
                _M_Radius_RightBottom = value;
                Invalidate();
            }
        }

        private Color _backColor = Color.SteelBlue;

        [Category("0Mine")]
        [Description("背景基色")]
        public Color M_BaseColor
        {
            get { return _backColor; }
            set
            {
                if (value != _backColor)
                {
                    //_brush.Color = value;
                    _backColor = value;
                }
            }
        }

        #endregion Field

        #region Title Field

        private int _M_Title_Radius = 10;

        [Category("0Mine")]
        [Description("标题圆角")]
        public int M_Title_Radius
        {
            get { return _M_Title_Radius; }
            set
            {
                if (_M_Title_Radius == value) return;
                _M_Title_Radius = value;
                Invalidate();
            }
        }

        private string _M_Title_Text = string.Empty;

        [Category("0Mine")]
        [Description("标题")]
        public string M_Title_Text
        {
            get { return _M_Title_Text; }
            set
            {
                if (_M_Title_Text == value) return;
                _M_Title_Text = value;
                Invalidate();
            }
        }

        private Color _M_Title_BaseColor = Color.Purple;

        [Category("0Mine")]
        [Description("标题基色")]
        public Color M_Title_BaseColor
        {
            get { return _M_Title_BaseColor; }
            set
            {
                if (_M_Title_BaseColor == value) return;
                _M_Title_BaseColor = value;
                Invalidate();
            }
        }

        private Color _M_Title_BorderColor = Color.Red;

        [Category("0Mine")]
        [Description("标题边框色")]
        public Color M_Title_BorderColor
        {
            get { return _M_Title_BorderColor; }
            set
            {
                if (_M_Title_BorderColor == value) return;
                _M_Title_BorderColor = value;
                Invalidate();
            }
        }

        private Font _M_Title_Font = new Font("微软雅黑", 9f, FontStyle.Regular, GraphicsUnit.Point);

        [Category("0Mine")]
        [Description("标题字体")]
        public Font M_Title_Font
        {
            get { return _M_Title_Font; }
            set
            {
                if (_M_Title_Font == value) return;
                _M_Title_Font = value;
                Invalidate();
            }
        }

        #endregion Title Field

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            //if (M_Radius.All > 0)
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //计算主区域上边框边距（给title预留空间）
            int padTop = 0;
            SizeF titleSize = new SizeF();
            if (!string.IsNullOrEmpty(M_Title_Text))
            {
                titleSize = g.MeasureString(M_Title_Text, M_Title_Font);
                padTop = (int)titleSize.Height / 2;
            }

            //主区域矩形
            Rectangle rec = new Rectangle(ClientRectangle.X, ClientRectangle.Y + padTop, ClientRectangle.Width - 1, ClientRectangle.Height - 1 - padTop);

            #region 主区域边框，填充色

            using (var path = DrawHelper.CreateRoundPath(rec,
                this.M_Radius_LeftTop, this.M_Radius_LeftBottom, this.M_Radius_RightTop, this.M_Radius_RightBottom))
            {
                if (M_DrawBackground)
                {
                    Brush fillBrush = null;
                    if (M_DrawLinear)/*渐变*/
                    {
                        fillBrush = new LinearGradientBrush(rec, M_Linear_StartColor, M_Linear_EndColor, M_LinearModel);
                        using (fillBrush)
                        {
                            var blend = new ColorBlend(2);
                            blend.Positions[0] = 0.0f;
                            blend.Positions[1] = 1.0f;
                            blend.Colors[0] = M_Linear_StartColor;
                            blend.Colors[1] = M_Linear_EndColor;
                            ((LinearGradientBrush)fillBrush).InterpolationColors = blend;
                            g.FillPath(fillBrush, path);
                        }
                    }
                    else/*单色*/
                        using (fillBrush = new SolidBrush(M_BaseColor))
                            g.FillPath(fillBrush, path);
                }

                if (M_DrawBorder && M_BorderWidth > 0)
                    using (var pen = new Pen(new SolidBrush(M_BorderColor), M_BorderWidth))
                        g.DrawPath(pen, path);

                if (M_DrawInnerBorder && M_InnerBorderWidth > 0)
                {
                    Rectangle rectangle = rec;
                    rectangle.Inflate(-2, -2);
                    var innerPath = DrawHelper.CreateRoundPath(rectangle,
                        this.M_Radius_LeftTop, this.M_Radius_LeftBottom, this.M_Radius_RightTop, this.M_Radius_RightBottom);

                    using (var pen = new Pen(new SolidBrush(M_InnerBorderColor), M_InnerBorderWidth))
                        g.DrawPath(pen, innerPath);
                }
            }

            #endregion 主区域边框，填充色

            #region 画标题

            if (!string.IsNullOrEmpty(M_Title_Text))
            {
                Rectangle titleRec = new Rectangle(rec.X + M_Title_Radius + 5, 0, (int)titleSize.Width + 2, (int)titleSize.Height + 2);
                using (var path2 = DrawHelper.CreateRoundRect(titleRec.X, titleRec.Y, titleRec.Width, titleRec.Height, M_Title_Radius, RoundModel.All))
                using (var fillBrush = new SolidBrush(M_Title_BaseColor))
                using (var pen = new Pen(new SolidBrush(M_Title_BorderColor), 1))
                {
                    g.FillPath(fillBrush, path2);
                    g.DrawPath(pen, path2);
                    using (var brush = new SolidBrush(ForeColor))
                    using (var fomat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center })
                        g.DrawString(M_Title_Text, M_Title_Font, brush, titleRec, fomat);
                }
            }

            #endregion 画标题
        }
    }
}