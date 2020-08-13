using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BatMes.Client.Controls
{
    public static class Tools
    {
        /// <summary>
        /// 支持屏幕分辨率最小宽度
        /// </summary>
        public const int SCREEN_WIDTH_MIN = 1024;
        /// <summary>
        /// 支持屏幕分辨率最小高度
        /// </summary>
        public const int SCREEN_HEIGHT_MIN = 768;

        ///// <summary>
        ///// 支持屏幕分辨率最大宽度
        ///// </summary>
        //public const int SCREEN_WIDTH_MAX = 1920;
        ///// <summary>
        ///// 支持屏幕分辨率最大高度
        ///// </summary>
        //public const int SCREEN_HEIGHT_MAX = 1080;

        /// <summary>
        /// TOP用户控件高
        /// </summary>
        public const int TOP_HEIGHT = 100;
        /// <summary>
        /// BOTTOM用户控件高
        /// </summary>
        public const int BOTTOM_HEIGHT = 30;

        /// <summary>
        /// 当前屏幕分辨率宽
        /// </summary>
        public static int ScreenWidth
        {
            get { return Screen.PrimaryScreen.Bounds.Width; }
        }

        /// <summary>
        /// 当前屏幕分辨率高减去任务栏高后的工作区域高
        /// </summary>
        public static int ScreenHeight
        {
            get { return SystemInformation.WorkingArea.Height; }
        }

        /// <summary>
        /// 根据图标按钮类型获取fontawesome字体值
        /// </summary>
        /// <param name="iconButtonType"></param>
        /// <returns></returns>
        public static string IconButtonTypeValue(IconButtonType iconButtonType)
        {
            switch(iconButtonType)
            {
                case IconButtonType.Start:
                    return "\uF04B";
                case IconButtonType.Stop:
                    return "\uF04D";
                case IconButtonType.Search:
                    return "\uF002";
                case IconButtonType.File:
                    return "\uF15B";
                case IconButtonType.Save:
                    return "\uF0C7";
                case IconButtonType.First:
                    return "\uF100";
                case IconButtonType.Last:
                    return "\uF101";
                case IconButtonType.Prev:
                    return "\uF104";
                case IconButtonType.Next:
                    return "\uF105";
                case IconButtonType.Trash:
                    return "\uf2ed";
                case IconButtonType.Plus:
                    return "\uf067";
                case IconButtonType.Check:
                    return "\uf00c";
                case IconButtonType.Bug:
                    return "\uf188";
                case IconButtonType.ArrowUp:
                    return "\uf062";
                case IconButtonType.ArrowDown:
                    return "\uf063";
                case IconButtonType.Bus:
                    return "\uf207";
                case IconButtonType.Train:
                    return "\uf239";
                default:
                    return string.Empty;
            }
        }

        #region 内存字体（方法1）

        private static Dictionary<byte[], FontFamily> fontFamilies = new Dictionary<byte[], FontFamily>();

        public static Font FontFromMemorey(FontAwesome font, float size, FontStyle style = FontStyle.Regular)
            => new Font(fontFromResources(font), size, style, GraphicsUnit.Point);

        [DllImport("gdi32.dll")]
        static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);
        private static FontFamily fontFromResources(FontAwesome font)
        {
            bool isOTF = false;
            if (Environment.OSVersion.Version.CompareTo(new Version("6.2")) >= 0)
                isOTF = true;

            byte[] fontData = null;
            switch (font)
            {
                case FontAwesome.BrandsRegular400:
                    if (isOTF)
                        fontData = Properties.Resources.brands_regular_400_otf;
                    else
                        fontData = Properties.Resources.brands_regular_400_ttf;
                    break;
                case FontAwesome.FreeRegular400:
                    if (isOTF)
                        fontData = Properties.Resources.free_regular_400_otf;
                    else
                        fontData = Properties.Resources.free_regular_400_ttf;
                    break;
                case FontAwesome.FreeSolid900:
                default:
                    if (isOTF)
                        fontData = Properties.Resources.free_solid_900_otf;
                    else
                        fontData = Properties.Resources.free_solid_900_ttf;
                    break;
            }

            var pair = fontFamilies.FirstOrDefault(k => k.Key.SequenceEqual(fontData));
            if (pair.Value != null)
                return pair.Value;

            int dataLength = fontData.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontData, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref cFonts);
            PrivateFontCollection collection = new PrivateFontCollection();
            collection.AddMemoryFont(fontPtr, dataLength);
            FontFamily fontFamily = collection.Families.First();
            fontFamilies.Add(fontData, fontFamily);
            collection.Dispose();
            return fontFamily;
        }

        #endregion

        #region 内存字体（方法2）

        //private static List<PrivateFontCollection> _fontCollections;

        //public static Font FontFromMemorey(FontAwesome font, float size, FontStyle style = FontStyle.Regular)
        //{
        //    if (_fontCollections == null) 
        //        _fontCollections = new List<PrivateFontCollection>();
        //    PrivateFontCollection fontCol = new PrivateFontCollection();

        //    /*
        //        注：Windows 7及以下版本只支持TTF字体
        //        Windows 10	                                10.0*
        //        Windows Server 2016 Technical Preview	    10.0*
        //        Windows 8.1	                                6.3*
        //        Windows Server 2012 R2	                    6.3*
        //        Windows 8	                                6.2
        //        Windows Server 2012	                        6.2
        //        Windows 7	                                6.1
        //        Windows Server 2008 R2	                    6.1
        //        Windows Server 2008	                        6
        //        Windows Vista	                            6
        //        Windows Server 2003 R2	                    5.2
        //        Windows Server 2003	                        5.2
        //        Windows XP 64-Bit Edition	                5.2
        //        Windows XP	                                5.1
        //        Windows 2000                                5
        //    */
        //    bool isOTF = false;
        //    if (Environment.OSVersion.Version.CompareTo(new Version("6.2")) >= 0)
        //        isOTF = true;

        //    byte[] fontData = null;
        //    switch(font)
        //    {
        //        case FontAwesome.BrandsRegular400:
        //            if (isOTF)
        //                fontData = Properties.Resources.brands_regular_400_otf;
        //            else
        //                fontData = Properties.Resources.brands_regular_400_ttf;
        //            break;
        //        case FontAwesome.FreeRegular400:
        //            if (isOTF)
        //                fontData = Properties.Resources.free_regular_400_otf;
        //            else
        //                fontData = Properties.Resources.free_regular_400_ttf;
        //            break;
        //        case FontAwesome.FreeSolid900:
        //        default:
        //            if (isOTF)
        //                fontData = Properties.Resources.free_solid_900_otf;
        //            else
        //                fontData = Properties.Resources.free_solid_900_ttf;
        //            break;
        //    }

        //    IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
        //    Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
        //    fontCol.AddMemoryFont(fontPtr, fontData.Length);
        //    Marshal.FreeCoTaskMem(fontPtr);
        //    _fontCollections.Add(fontCol);
        //    return new Font(fontCol.Families[0], size, style);
        //}

        //public static Font FontFromMemorey(string fontFile, float size, FontStyle style = FontStyle.Regular)
        //{
        //    if (_fontCollections == null) _fontCollections = new List<PrivateFontCollection>();
        //    PrivateFontCollection fontCol = new PrivateFontCollection();
        //    fontCol.AddFontFile(fontFile);
        //    _fontCollections.Add(fontCol);
        //    return new Font(fontCol.Families[0], size, style);
        //}

        #endregion

    }//end class
}
