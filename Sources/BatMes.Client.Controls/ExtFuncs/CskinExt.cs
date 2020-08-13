using CCWin.SkinControl;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExtFuncs
{
    public static class CskinExt
    {
        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="skinButton"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Control BaseColor(this SkinButton skinButton, Color color)
        {
            if (!skinButton.InvokeRequired)
                skinButton.BaseColor = color;
            else
                skinButton.Invoke(new Action(() => { skinButton.BaseColor = color; }));
            return skinButton;
        }

        public static void BorderColor(this SkinButton skinButton, Color color)
        {
            if (!skinButton.InvokeRequired)
                skinButton.BaseColor = color;
            else
                skinButton.Invoke(new Action(() => { skinButton.BorderColor = color; }));
        }

        /// <summary>
        /// 获取背景色
        /// </summary>
        /// <param name="skinButton"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color BaseColor(this SkinButton skinButton)
        {
            if (!skinButton.InvokeRequired)
                return skinButton.BaseColor;
            else
                skinButton.Invoke(new Func<Color>(() => { return skinButton.BaseColor; }));
            return default(Color);
        }

        public static Control RectBackColor(this SkinGroupBox groupbox, Color color)
        {
            if (!groupbox.InvokeRequired)
                groupbox.RectBackColor = color;
            else
                groupbox.Invoke(new Action(() => { groupbox.RectBackColor = color; }));
            return groupbox;
        }

        public static Control TitleRectBackColor(this SkinGroupBox groupbox, Color color)
        {
            if (!groupbox.InvokeRequired)
                groupbox.TitleRectBackColor = color;
            else
                groupbox.Invoke(new Action(() => { groupbox.TitleRectBackColor = color; }));
            return groupbox;
        }

        public static string Text(this SkinControlBase control)
        {
            if (!control.InvokeRequired)
                return control.Text;
            return control.Invoke(new Func<string>(() => { return control.Text; })).ToString();
        }

        public static SkinButton SkinButtonText(this SkinButton button, string text)
        {
            if (!button.InvokeRequired)
                button.Text = text;
            else
                button.Invoke(new Action(() => { button.Text = text; }));
            return button;
        }
    }
}