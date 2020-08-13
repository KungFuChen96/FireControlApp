using CCWin;
using System.Drawing;

namespace CskinExt
{
    public static class SkinToolTipFactory
    {
        public static SkinToolTip Tip
        {
            get
            {
                return new SkinToolTip()
                {
                    UseAnimation = true,
                    UseFading = true,
                    ShowAlways = true,
                    ReshowDelay = 800,
                    InitialDelay = 600,
                    StripAmpersands = true,
                    AutoPopDelay = 5000,
                    OwnerDraw = true,
                    Opacity = 0.9,
                    TipFore = Color.White,
                    BackColor2 = Color.FromArgb(68, 121, 161),//68, 121, 161
                    BackColor = Color.FromArgb(0, 172, 194),//0, 172, 194
                    Border = Color.FromArgb(0, 172, 194),
                    //ToolTipIcon = ToolTipIcon.Warning
                };
            }
        }
    }
}