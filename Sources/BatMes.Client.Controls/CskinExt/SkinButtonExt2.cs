namespace CskinExt
{
    public class SkinButtonExt2 : SkinButtonExt
    {
        public SkinButtonExt2()
        {
            this.MouseDown += (obj, e) => this.BorderColor = this.DownBaseColor;
            this.MouseEnter += (obj, e) => this.BorderColor = this.MouseBaseColor;
            this.MouseLeave += (obj, e) => this.BorderColor = this.BaseColor;
            //禁用时取消IsDrawGlass，启用时开启IsDrawGlass
            //this.EnabledChanged += (obj, e) => this.IsDrawGlass = this.Enabled;
        }
    }
}