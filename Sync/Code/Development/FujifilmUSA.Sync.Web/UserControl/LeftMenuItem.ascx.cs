using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuItem : BaseControl
    {
        #region Declaration
        public ELeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }
        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {

            switch (ItemLevel)
            {
                case ELeftMenuItemLevel.Home:
                    imgItem.ImageUrl = "../images/home_icon.png";
                    lnkItem.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Home;
                    break;
                case ELeftMenuItemLevel.Device:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    lnkItem.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Device;
                    break;
                case ELeftMenuItemLevel.Operation:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    lnkItem.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Operation;
                    break;
                case ELeftMenuItemLevel.ProjectSetup:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    lnkItem.Enabled = false;
                    break;
                case ELeftMenuItemLevel.Project:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    lnkItem.Enabled = false;
                    break;
            }
            lnkItem.Text = ItemName;
        }

        protected void lnkItem_Click(object sender, EventArgs e)
        {
            switch (this.ItemLevel)
            {
                case ELeftMenuItemLevel.Home:
                    SessionManager.Instance.CurrentDevice = null;
                    SessionManager.Instance.CurrentOperation = EOperation.None;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("Index.aspx");
                    break;
                case ELeftMenuItemLevel.Device:
                    SessionManager.Instance.CurrentOperation = EOperation.None;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("OperationSelection.aspx");
                    break;
                case ELeftMenuItemLevel.Operation:
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("ProjectList.aspx");
                    break;
            }
        }
    }
}