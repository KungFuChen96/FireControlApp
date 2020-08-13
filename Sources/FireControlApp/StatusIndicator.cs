using CCWin.SkinControl;
using System.Windows.Forms;

namespace FireControlApp
{
    public partial class StatusIndicator : UserControl
    {
        public StatusIndicator()
        {
            InitializeComponent();
            var btns = this.tableLayoutPanel1.Controls;
            foreach (SkinButton item in btns)
            {
                item.Margin = new Padding(1, 1, 1, 1);
                item.Enabled = false;
                item.IsEnabledDraw = false;
            }
        }
    }
}