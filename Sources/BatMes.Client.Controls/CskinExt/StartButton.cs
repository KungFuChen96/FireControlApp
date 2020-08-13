using System;

namespace CskinExt
{
    public class StartButton : SkinButtonExt2
    {
        private readonly Action<bool> UpdateControl;

        //static event
        public StartButton()
        {
            SetStyle();
            if (UpdateControl == null)
                UpdateControl = isEnabled => base.Enabled = isEnabled;
        }

        private void SetStyle()
        {
            this.BackColor = System.Drawing.Color.Snow;
            this.BaseColor = System.Drawing.Color.LimeGreen;
            this.BorderColor = System.Drawing.Color.LimeGreen;
            this.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.DownBack = null;
            this.DownBaseColor = System.Drawing.Color.FromArgb(0, 101, 0);
            this.FadeGlow = false;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.InnerBorderColor = System.Drawing.Color.Transparent;
            this.IsDrawGlass = false;
            this.MouseBack = null;
            this.MouseBaseColor = System.Drawing.Color.FromArgb(0, 150, 0);
            this.NormlBack = null;
            this.Radius = 15;
            this.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.Size = new System.Drawing.Size(63, 63);
            this.TabIndex = 218;
            this.UseVisualStyleBackColor = false;
            //this.IsDrawBorder = false;
        }

        private bool _enabled = true;

        public new bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (InvokeRequired) this.BeginInvoke(UpdateControl, value);
                else base.Enabled = value;
                _enabled = value;
            }
        }
    }
}