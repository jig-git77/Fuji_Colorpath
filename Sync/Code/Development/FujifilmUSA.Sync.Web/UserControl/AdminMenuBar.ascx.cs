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
            if (SessionManager.Instance.CurrentUser != null)
            {
                lbtnCompany.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageCompany);
                lbtnBranch.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageBranch);
                lbtnUser.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUser);
                lbtnSourceProfile.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile);
                lbtnDevice.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice);
                ltnLicense.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense);
                lbtnMeasurementDevice.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMeasurementDevice);
                lbtnRefChart.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageReferenceChart);
            }
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

        protected void lbtnRefChart_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ReferenceChartManagement.aspx");
        }
        protected void lbtProfileMgmt_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ProfileManagement.aspx");
        }
        protected void lbtDownload_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("DownloadSoftwares.aspx");
        }
        protected void lbtnActivityValidationRules_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ActivityValidationRules.aspx");
        }
    }
}