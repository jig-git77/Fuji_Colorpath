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
        #region Constructor
        public Settings()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

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
            tdReferenceChart.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageReferenceChart);

            dvActivityValidationRules.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageActivityValidationRules);
            dvApplicationConfiguration.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageApplicationConfiguration);
            dvProfilerSettings.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDeviceProfileSettings);

            divMetricTargetPreset.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMetricTargetPreset); 
            divPrintStandardType.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManagePrintStandardType);
            divLookupTVILABDensity.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSync1DTVILABDensity);
            divPatchPreset.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManagePatchTolerance); 
            
            ////seperator image 
            divBranchSeprator.Visible = tdBranch.Visible && tdcompany.Visible;
            divUserSeprator.Visible = tdUser.Visible && tdBranch.Visible;
            divProfileManagementSeprator.Visible = tdProfileManagement.Visible && tdUser.Visible;
            divLicenseSeprator.Visible = tdLicense.Visible && tdProfileManagement.Visible;

            divMeasurementSeprator.Visible = tdMeasurementDevice.Visible && tdDevice.Visible;
            divDownloadSeprator.Visible = tdDownload.Visible && tdMeasurementDevice.Visible;
            divRefChartSeparator.Visible = tdReferenceChart.Visible && tdDownload.Visible;
            divSourceProfileSeprator.Visible = tdSourceProfile.Visible && tdReferenceChart.Visible;

            divProfilerSettingSeparator.Visible = dvProfilerSettings.Visible && dvActivityValidationRules.Visible;
            divApplicationConfigurationSeparator.Visible = dvApplicationConfiguration.Visible && dvProfilerSettings.Visible;

            divMetricTargetPresetSeparator.Visible = divMetricTargetPreset.Visible && dvApplicationConfiguration.Visible;
            divPatchPresetSeparator.Visible = divPatchPreset.Visible && dvApplicationConfiguration.Visible;
            divLookupTVILABDensitySeparator.Visible = divLookupTVILABDensity.Visible && divPrintStandardType.Visible;
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
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("DownloadSoftwares.aspx");
        }
        protected void lnkbtnMetricTargetPreset_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("MetricTargetPresetManagement.aspx");
        }
        protected void lnkbtnPrintStandardType_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("PrintStandardTypeManagement.aspx");
        }

        protected void lnkbtnPatchPreset_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("PatchPresetManagement.aspx");
        }

        protected void lnkSpotTarget_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SpotTargetManagement.aspx");
        }
    }
}