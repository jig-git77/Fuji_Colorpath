using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Settings : BaseControl
    {
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();

            //role base permission
            tdCompnay.Visible = ((BasePage)this.Page).HasManageCompanyPermission;
            tdBranch.Visible = ((BasePage)this.Page).HasManageBranchPermission;
            tdUser.Visible = ((BasePage)this.Page).HasManageUserPermission;
            tdSourceProfile.Visible = ((BasePage)this.Page).HasManageSourceProfilePermission;
            tdDevice.Visible = ((BasePage)this.Page).HasManageDevicePermission;
            tdLicense.Visible = ((BasePage)this.Page).HasManageLicensePermission;
            tdMeasurementDevice.Visible = ((BasePage)this.Page).HasManageMeasurementDevicePermission;

            ////seperator image 
            divCompnaySeprator.Visible = (tdCompnay.Visible && tdBranch.Visible);
            divBranchSeprator.Visible = (tdBranch.Visible && tdUser.Visible);
            divUserSeprator.Visible = (tdUser.Visible && tdSourceProfile.Visible);

            divDeviceSeprator.Visible = (tdDevice.Visible && tdLicense.Visible);
            divLicenseSeprator.Visible = (tdLicense.Visible && tdMeasurementDevice.Visible);
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



    }
}