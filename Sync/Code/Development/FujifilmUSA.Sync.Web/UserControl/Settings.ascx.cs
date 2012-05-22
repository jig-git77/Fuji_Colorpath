using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Settings : BaseControl
    {
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            //role base permission
            tdcompany.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageCompany);
            tdBranch.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageBranch);
            tdUser.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUser);
            tdSourceProfile.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile);
            tdDevice.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDevice);
            tdLicense.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageLicense);
            tdMeasurementDevice.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMeasurementDevice);
            //tdReferenceChart.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSourceProfile);

            ////seperator image 
            divcompanySeprator.Visible = tdcompany.Visible;
            divBranchSeprator.Visible = tdBranch.Visible;
            divUserSeprator.Visible = tdUser.Visible;
           
            divDeviceSeprator.Visible = tdDevice.Visible;
            divLicenseSeprator.Visible = tdLicense.Visible;
            divMeasurementSeprator.Visible = tdMeasurementDevice.Visible;           
        }
        #endregion


        protected void lbtnCompanyManagement_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("CompanyManagement.aspx");
        }
        protected void lbtnBranchManagement_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("BranchManagement.aspx");
        }
        protected void lbtnUserManagement_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("UserManagement.aspx");
        }
        protected void lbtnSourceProfile_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SourceProfileManagement.aspx");
        }
        protected void lbtnDeviceManagement_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("DeviceManagement.aspx");
        }
        protected void lbtnLicenseManagement_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("LicenseManagement.aspx");
        }
        protected void lnkMeasurementDevice_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("MeasurementDeviceList.aspx");
        }
        protected void lnkRefChart_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ReferenceChartManagement.aspx");
        }
        protected void lnkProfileMgmt_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ProfileManagement.aspx");
        }
    }
}