using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AdminMenuBar : BaseControl
    {
        #region Constructor
        public AdminMenuBar()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

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

                lbtnActivityValidationRules.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageActivityValidationRules);
                lbtnProfilerSettings.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageDeviceProfileSettings);
                lbtnApplicationConfiguration.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageApplicationConfiguration);
                lbtnMetricTargetPreset.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMetricTargetPreset);
                lbtnPatchPreset.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManagePatchTolerance);
                lbtnPrintStandardType.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManagePrintStandardType);
                lbtnLookupTVILABDensity.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSync1DTVILABDensity);
                lbtnSpotTarget.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageSpotTarget);
                lbtnMetricTarget.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMetricTargetPreset);
                lbtnMToolVersion.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageMToolVersion);
                lbtnUpdateChangeLog.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManageUpdateChangeLog);
                lbtAssignChartwithMTP.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.AssignChartWithMTP);
            }
        }
        #endregion

        #region "Control Events"

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

        protected void lbtnProfilerSettings_Click(object sender, EventArgs e)
        {
            //Response.RedirectPermanent("AddDeviceProfilerSettings.aspx");
            Response.RedirectPermanent("ProfilerOptionPresetsManagement.aspx");
        }

        protected void lbtnApplicationConfiguration_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("ApplicationConfiguration.aspx");
        }

        protected void lbtnMetricTargetPreset_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("MetricTargetPresetManagement.aspx");
        }

        protected void lbtnPatchPreset_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("PatchPresetManagement.aspx");
        }

        protected void lbtnPrintStandardType_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("PrintStandardTypeManagement.aspx");
        }

        protected void lbtnLookupTVILABDensity_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddLookupTVILABDensity.aspx");
        }

        protected void lbtnSpotTarget_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SpotTargetManagement.aspx");
        }

        protected void lbtnMetricTarget_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("MetricTargetManagement.aspx");
        }

        protected void lbtnMToolVersion_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("MToolVersionManagement.aspx");
        }

        protected void lbtnUpdateChangeLog_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("UpdateChangeLogManagement.aspx");
        }

        protected void lbtAssignChartwithMTP_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AssignRefChartWMetricTargetPreset.aspx");
        }

        protected void lbtCustomBCOTargetLibrary_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("CustomBcoManagement.aspx");
        }

        #endregion
    }
}