using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuItem : System.Web.UI.UserControl
    {
        #region Declaration
        public LeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }
        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {

            switch (ItemLevel)
            {
                case LeftMenuItemLevel.Home:
                    imgItem.ImageUrl = "../images/home_icon.png";
                    imgItem.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Home;
                    break;
                case LeftMenuItemLevel.Device:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    imgItem.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Device;
                    break;
                case LeftMenuItemLevel.Operation:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    imgItem.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Operation;
                    break;
                case LeftMenuItemLevel.ProjectSetup:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    imgItem.Enabled = false;
                    break;
                case LeftMenuItemLevel.Project:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    imgItem.Enabled = false;
                    break;
            }
            lnkItem.Text = ItemName;
        }

        protected void lnkItem_Click(object sender, EventArgs e)
        {
            switch (this.ItemLevel)
            {
                case LeftMenuItemLevel.Home:
                    SessionManager.Instance.CurrentDevice = null;
                    SessionManager.Instance.CurrentOperation = Operation.None;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("Index.aspx");
                    break;
                case LeftMenuItemLevel.Device:
                    SessionManager.Instance.CurrentOperation = Operation.None;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("OperationSelection.aspx");
                    break;
                case LeftMenuItemLevel.Operation:
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("ProjectList.aspx");
                    break;
            }
        }
    }
}