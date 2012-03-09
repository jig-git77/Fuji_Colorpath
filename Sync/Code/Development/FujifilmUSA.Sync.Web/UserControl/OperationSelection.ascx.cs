using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class OperationSelection : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Operation;
                lblDeviceName.Text = SessionManager.Instance.CurrentDevice.DeviceName;
            }
        }
        protected void lbtnDLPOptimization_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentOperation = Operation.DlpOptimization;
            Response.Redirect("~/ProjectList.aspx");
        }
    }
}

