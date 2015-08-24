using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuProjectSetting : BaseControl
    {
        public LeftMenuProjectSetting()
        {
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            lnkDescription.Enabled =
                lnkTargetColorSpace.Enabled =
                lnkProfilerOption.Enabled =
                lnkMeasurementSetup.Enabled =
                lnkValidationSetup.Enabled =
                lnkSave.Enabled =
                lnkMeasure.Enabled =
                lnkAnalyze.Enabled =
                lnkDownload.Enabled = false;

            lnkDescription.Font.Bold =
               lnkTargetColorSpace.Font.Bold =
               lnkProfilerOption.Font.Bold =
               lnkMeasurementSetup.Font.Bold =
               lnkValidationSetup.Font.Bold =
               lnkSave.Font.Bold =
               lnkMeasure.Font.Bold =
               lnkAnalyze.Font.Bold =
               lnkDownload.Font.Bold = false;

            switch (SessionManager.Instance.CurrentProject.CurrentDisplayStatus)
            {
                case ECurrentProjectStatus.New:
                    if (SessionManager.Instance.CurrentProject.PrintCondition != null && !string.IsNullOrEmpty(SessionManager.Instance.CurrentProject.PrintCondition.Media))
                    {
                        lnkDescription.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.SourceProfile != null && SessionManager.Instance.CurrentProject.SourceProfile.SourceProfileId > 0 && SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.CustomSourceProfile) //No Target ColorSpace for Custom Source Profile project type
                    {
                        lnkTargetColorSpace.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.ProjectOption != null && SessionManager.Instance.CurrentProject.ProjectOption.Count > 0)
                    {
                        lnkProfilerOption.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0)
                    {
                        lnkMeasurementSetup.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key > 0)
                    {
                        lnkValidationSetup.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.ProjectId > 0)
                    {
                        lnkSave.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0)
                    {
                        lnkMeasurementSetup.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0)
                    {
                        lnkMeasure.Enabled = true;
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements && SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.CustomSourceProfile)
                        {
                            lnkAnalyze.Enabled = true;
                        }
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile)
                        {
                            lnkDownload.Enabled = true;
                        }


                        if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && SessionManager.Instance.CurrentProject.ReTargetProjectID > 0)
                        {
                            lnkMeasure.Enabled = false;
                        }

                    }
                    break;
                case ECurrentProjectStatus.Edit:
                    //If verification, then don't enable Measurementsetup, analyze and download links...
                    if (SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation)
                    {
                        if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0)
                        {
                            lnkMeasurementSetup.Enabled = true;
                        }

                        if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0)
                        {
                            if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements && SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.CustomSourceProfile)
                            {
                                lnkAnalyze.Enabled = true;
                            }
                            if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile)
                            {
                                lnkDownload.Enabled = true;
                            }
                        }
                    }
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key > 0)
                    {
                        lnkValidationSetup.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.ProjectId > 0)
                    {
                        lnkSave.Enabled = true;
                        lnkDescription.Enabled = true;
                    }
                    break;
                case ECurrentProjectStatus.Run:
                    //This control won't load...
                    break;
            }
            switch (SessionManager.Instance.CurrentNavigation)
            {
                case ELeftMenuItemLevel.Description:
                    lnkDescription.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.TargetColorSpace:
                    lnkTargetColorSpace.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.ProfilerOption:
                    lnkProfilerOption.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.MeasurementSetup:
                    lnkMeasurementSetup.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.ValidationSetup:
                    lnkValidationSetup.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Save:
                    lnkSave.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Measure:
                    lnkMeasure.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Analyze:
                    lnkAnalyze.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Download:
                    lnkDownload.Font.Bold = true;
                    break;
            }

            lnkDescription.ForeColor = lnkDescription.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkTargetColorSpace.ForeColor = lnkTargetColorSpace.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkProfilerOption.ForeColor = lnkProfilerOption.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkMeasurementSetup.ForeColor = lnkMeasurementSetup.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkValidationSetup.ForeColor = lnkValidationSetup.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkSave.ForeColor = lnkSave.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkMeasure.ForeColor = lnkMeasure.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkAnalyze.ForeColor = lnkAnalyze.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkDownload.ForeColor = lnkDownload.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
        }
        protected void lnkItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                var lnkItem = sender as LinkButton;
                if (lnkItem != null)
                {
                    switch (lnkItem.ID)
                    {
                        case "lnkDescription":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;
                            Response.Redirect("~/AddProject.aspx");
                            break;
                        case "lnkTargetColorSpace":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.TargetColorSpace;
                            Response.Redirect("~/AddProject.aspx");
                            break;
                        case "lnkProfilerOption":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
                            Response.Redirect("~/AddProject.aspx");
                            break;
                        case "lnkMeasurementSetup":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
                            Response.Redirect("~/AddProject.aspx");
                            break;
                        case "lnkValidationSetup":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ValidationSetup;
                            Response.Redirect("~/AddProject.aspx");
                            break;
                        case "lnkSave":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
                            Response.Redirect("~/AddProject.aspx");
                            break;
                        case "lnkMeasure":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                            Response.Redirect("~/Measurement.aspx");
                            break;
                        case "lnkAnalyze":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                            Response.Redirect("~/Analyze.aspx");
                            break;
                        case "lnkDownload":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                            Response.Redirect("~/Export.aspx");
                            break;
                    }
                }
            }
        }
    }
}
