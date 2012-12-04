using System;
using System.Linq;
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

        #region Constructor
        public LeftMenuItem()
        {
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        } 
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
                    imgItem.ImageUrl = "../images/dlp_optimization.png";
                    lnkItem.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Operation;
                    break;
                case ELeftMenuItemLevel.ProjectSetup:
                    imgItem.ImageUrl = "../images/project.png";
                    lnkItem.Enabled = false;
                    break;
                case ELeftMenuItemLevel.Project:
                    imgItem.ImageUrl = "../images/project.png";
                    break;
            }
            lnkItem.Text = ItemName;
        }

        protected void lnkItem_Click(object sender, EventArgs e)
        {
            switch (this.ItemLevel)
            {
                case ELeftMenuItemLevel.Home:
                    SessionManager.Instance.CurrentBranchName = null;
                    SessionManager.Instance.CurrentDevice = null;                    
                    SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("Index.aspx");
                    break;
                case ELeftMenuItemLevel.Device:
                case ELeftMenuItemLevel.Operation: //Here operation level will work as device level                    
                    SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Response.Redirect("ProjectList.aspx");
                    break;
                                    
                case ELeftMenuItemLevel.Project:
                    SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.First();
                    SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Edit;
                    SessionManager.Instance.CurrentNavigation =
                        SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation
                            ? ELeftMenuItemLevel.ValidationSetup
                            : ELeftMenuItemLevel.MeasurementSetup;
                    Response.Redirect("AddProject.aspx");
                    break;
            }
        }
    }
}