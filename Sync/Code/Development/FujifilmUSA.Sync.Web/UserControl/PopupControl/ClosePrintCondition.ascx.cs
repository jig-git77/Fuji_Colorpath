using System;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ClosePrintCondition : System.Web.UI.UserControl
    {
        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            Response.Redirect("Measurement.aspx?ProjectName=''");
        }
        protected void lbtnDownLoadCurrentDLP_Click(object sender, EventArgs e)
        {

        }
    }
}