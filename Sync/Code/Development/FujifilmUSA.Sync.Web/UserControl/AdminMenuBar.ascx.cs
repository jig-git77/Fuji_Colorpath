using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using System.Web.Security;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AdminMenuBar : BaseControl
    {
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();

            //role base permission
            lbtnCompany.Visible = ((BasePage)this.Page).HasManageCompanyPermission;
            lbtnBranch.Visible = ((BasePage)this.Page).HasManageBranchPermission;
            lbtnUser.Visible = ((BasePage)this.Page).HasManageUserPermission;
            lbtnSourceProfile.Visible = ((BasePage)this.Page).HasManageSourceProfilePermission;
            lbtnDevice.Visible = ((BasePage)this.Page).HasManageDevicePermission;
            ltnLicense.Visible = ((BasePage)this.Page).HasManageLicensePermission;
            lbtnMeasurementDevice.Visible = ((BasePage)this.Page).HasManageMeasurementDevicePermission;
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