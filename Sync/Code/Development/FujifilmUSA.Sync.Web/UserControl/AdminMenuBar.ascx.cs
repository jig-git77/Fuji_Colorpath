using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AdminMenuBar : BaseControl
    {
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            //role base permission
            //lbtnCompany.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageCompany);
            //lbtnBranch.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageBranch);
            //lbtnUser.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUser);
            //lbtnSourceProfile.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile);
            //lbtnDevice.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice);
            //ltnLicense.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense);
            //lbtnMeasurementDevice.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMeasurementDevice);
        }
        #endregion

        #region "Control Events"
        #endregion


        protected void lbtnCompany_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("CompanyManagement.aspx");
        }

        protected void lbtnBranch_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("BranchManagement.aspx");
        }

        protected void lbtnUser_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("UserManagement.aspx");
        }

        protected void lbtnSourceProfile_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SourceProfileManagement.aspx");
        }

        protected void lbtnDevice_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("DeviceManagement.aspx");
        }

        protected void ltnLicense_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("LicenseManagement.aspx");
        }

        protected void lbtnMeasurementDevice_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("MeasurementDeviceList.aspx");
        }
    }
}