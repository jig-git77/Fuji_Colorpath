using System;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ConfirmSave : System.Web.UI.UserControl
    {
        protected void lbtnYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectList.aspx");
        }
    }
}