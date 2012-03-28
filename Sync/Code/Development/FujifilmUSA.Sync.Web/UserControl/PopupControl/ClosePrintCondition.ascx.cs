using System;
using System.Web.UI;
using System.Text;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ClosePrintCondition : BaseControl
    {
        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            Common.Utilities.PopupWindowsRedirect(this,"Measurement.aspx");
        }
        protected void lbtnDownLoadCurrentDLP_Click(object sender, EventArgs e)
        {

        }
    }
}