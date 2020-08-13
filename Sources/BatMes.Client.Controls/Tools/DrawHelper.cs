using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tools
{
    public enum RoundModel
    {
        All,
        LeftTop,
        LeftBottom,
        RightTop,
        RightBottom,
        Left,
        Top,
        Right,
        Bottom
    }

    public static class DrawHelper
    {
        //计算圆角区域路径  radius可以理解为正方形内接圆半径  x，y为正方形左上角坐标
        //AddArc后面两个参数表示取哪一段弧线段
        //gp.AddArc(x, y, radius * 2, radius * 2, 0, 90)，表示从左上角坐标为x,y，宽为radius*2，高为radius*2的内接椭圆中，以椭圆圆心为原点，从0°出发，顺时针截取90°的弧线段
        //gp.AddLine(x + width - radius, y + height, x + radius, y + height);线段的起点和终点坐标
        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            //左上角圆弧
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.AddLine(x + radius, y, x + width - radius, y);
            //右上角圆弧
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(x + width, y + radius, x + width, y + height - radius);
            //右下角圆弧
            gp.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
            gp.AddLine(x + width - radius, y + height, x + radius, y + height);
            //左下角圆弧
            gp.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
            gp.AddLine(x, y + height - radius, x, y + radius);
            gp.CloseFigure();//闭合所画弧线段和线段
            return gp;
        }

        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius, RoundModel roundModel)
        {
            GraphicsPath gp = new GraphicsPath();
            switch (roundModel)
            {
                case RoundModel.All:
                    //左上角圆弧
                    gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
                    gp.AddLine(x + radius, y, x + width - radius, y);
                    //右上角圆弧
                    gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
                    gp.AddLine(x + width, y + radius, x + width, y + height - radius);
                    //右下角圆弧
                    gp.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
                    gp.AddLine(x + width - radius, y + height, x + radius, y + height);
                    //左下角圆弧
                    gp.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
                    gp.AddLine(x, y + height - radius, x, y + radius);
                    break;

                case RoundModel.RightTop:
                    gp.AddLine(x, y, x + width - radius, y);
                    gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
                    gp.AddLine(x + width, y + radius, x + width, y + height);
                    gp.AddLine(x + width, y + height, x, y + height);
                    gp.AddLine(x, y + height, x, y);
                    break;

                case RoundModel.RightBottom:
                    gp.AddLine(x, y, x + width, y);
                    gp.AddLine(x + width, y, x + width, y + height - radius);
                    gp.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
                    gp.AddLine(x + width - radius, y + height, x, y + height);
                    gp.AddLine(x, y + height, x, y);
                    break;

                case RoundModel.LeftBottom:
                    gp.AddLine(x, y, x + width, y);
                    gp.AddLine(x + width, y, x + width, y + height);
                    gp.AddLine(x + width, y + height, x - radius, y + height);
                    gp.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
                    gp.AddLine(x, y + height - radius, x, y);
                    break;

                case RoundModel.LeftTop:
                    gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
                    gp.AddLine(x + radius, y, x + width, y);
                    gp.AddLine(x + width, y, x + width, y + height);
                    gp.AddLine(x + width, y + height, x, y + height);
                    gp.AddLine(x, y + height, x, y);
                    break;

                case RoundModel.Left:
                    gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
                    gp.AddLine(x + radius, y, x + width, y);
                    gp.AddLine(x + width, y, x + width, y + height);
                    gp.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
                    gp.AddLine(x, y + height - radius, x, y - radius);
                    break;

                case RoundModel.Top:
                    gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
                    gp.AddLine(x + radius, y, x + width - radius, y);
                    gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
                    gp.AddLine(x + width, y + radius, x + width, y + height);
                    gp.AddLine(x + width, y + height, x, y + height);
                    gp.AddLine(x, y + height, x, y);
                    break;

                case RoundModel.Right:
                    gp.AddLine(x, y, x + width - radius, y);
                    gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
                    gp.AddLine(x + width, y + radius, x + width, y + height - radius);
                    gp.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
                    gp.AddLine(x + width - radius, y + height, x, y + height);
                    gp.AddLine(x, y + height, x, y);
                    break;

                case RoundModel.Bottom:
                    gp.AddLine(x, y, x + width, y);
                    gp.AddLine(x + width, y, x + width, y + height - radius);
                    gp.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
                    gp.AddLine(x + width - radius, y + height, x + radius, y + height);
                    gp.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
                    gp.AddLine(x, y + height - radius, x, y);
                    break;
            }

            gp.CloseFigure();//闭合所画弧线段和线段
            return gp;
        }

        public static GraphicsPath CreateRoundPath(RectangleF rect, int LeftTop, int LeftBottom, int RightTop, int RightBottom)
        {
            var path = new GraphicsPath();

            if (rect.Width == 0 || rect.Height == 0)
                return path;

            if (LeftTop > 0)
                path.AddArc(rect.Left, rect.Top, LeftTop * 2, LeftTop * 2, 180, 90);//左上角圆弧

            path.AddLine(rect.Left + LeftTop, rect.Top, rect.Right - RightTop, rect.Top);//上边框

            if (RightTop > 0)
                path.AddArc(rect.Right - (RightTop * 2), rect.Top, RightTop * 2, RightTop * 2, 270, 90);//右上角圆弧

            path.AddLine(rect.Right, rect.Top + RightTop, rect.Right, rect.Bottom - RightBottom);//右边框

            if (RightBottom > 0)
                path.AddArc(rect.Right - RightBottom * 2, rect.Bottom - RightBottom * 2, RightBottom * 2, RightBottom * 2, 0, 90);//右下角圆弧

            path.AddLine(rect.Right - RightBottom, rect.Bottom, rect.Left + LeftBottom, rect.Bottom);//下边框

            if (LeftBottom > 0)
                path.AddArc(rect.Left, rect.Bottom - LeftBottom * 2, LeftBottom * 2, LeftBottom * 2, 90, 90);//左下角圆弧

            path.AddLine(rect.Left, rect.Bottom - LeftBottom, rect.Left, rect.Top + LeftTop);

            path.CloseFigure();

            return path;
        }

        //计算圆角区域路径
        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius, RoundModel model = RoundModel.All)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width - 1, rect.Height - 1, radius, model);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            double ratio = blend / 255d;
            double invRatio = 1d - ratio;
            int r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            int g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            int b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }
    }
}