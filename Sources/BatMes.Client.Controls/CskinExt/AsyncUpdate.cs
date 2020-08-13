using System;

namespace CskinExt
{
    public static class AsyncUpdate
    {
        public static SkinButtonExt SetTip(this SkinButtonExt btn, string tipMsg)
        {
            if (!btn.InvokeRequired)
                btn.TipMsg = tipMsg;
            else
                btn.Invoke(new Action(() => { btn.TipMsg = tipMsg; }));
            return btn;
        }
    }
}