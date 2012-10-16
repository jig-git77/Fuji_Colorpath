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

            lnkDescription.ForeColor =
                lnkTargetColorSpace.ForeColor =
                lnkProfilerOption.ForeColor =
                lnkMeasurementSetup.ForeColor =
                lnkValidationSetup.ForeColor =
                lnkSave.ForeColor =
                lnkMeasure.ForeColor =
                lnkAnalyze.ForeColor =
                lnkDownload.ForeColor = System.Drawing.Color.Silver;
            switch (SessionManager.Instance.CurrentProject.CurrentDisplayStatus)
            {
                case ECurrentProjectStatus.New:
                    if (SessionManager.Instance.CurrentProject.PrintCondition != null && !string.IsNullOrEmpty(SessionManager.Instance.CurrentProject.PrintCondition.Media))
                    {
                        lnkDescription.ForeColor = System.Drawing.Color.Black;
                        lnkDescription.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.SourceProfile != null && SessionManager.Instance.CurrentProject.SourceProfile.SourceProfileId > 0)
                    {
                        lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                        lnkTargetColorSpace.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.ProjectOption != null && SessionManager.Instance.CurrentProject.ProjectOption.Count > 0)
                    {
                        lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                        lnkProfilerOption.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0)
                    {
                        lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                        lnkMeasurementSetup.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key > 0)
                    {
                        lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                        lnkValidationSetup.Enabled = true;
                    }
                    break;
                case ECurrentProjectStatus.Edit:
                    //If verification, then don't enable Measurementsetup, analyze and download links...
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.None)
                    {
                        if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0)
                        {
                            lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                            lnkMeasurementSetup.Enabled = true;
                        }

                        if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0)
                        {
                            if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements)
                            {
                                lnkAnalyze.ForeColor = System.Drawing.Color.Black;
                                lnkAnalyze.Enabled = true;
                            }
                            if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile)
                            {
                                lnkDownload.ForeColor = System.Drawing.Color.Black;
                                lnkDownload.Enabled = true;
                            }
                        }
                    }
                    if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key > 0)
                    {
                        lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                        lnkValidationSetup.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.ProjectId > 0)
                    {
                        lnkSave.ForeColor = System.Drawing.Color.Black;
                        lnkSave.Enabled = true;
                    }
                    break;
                case ECurrentProjectStatus.Run:
                    //This control won't load...
                    break;
            }
            switch (SessionManager.Instance.CurrentNavigation)
            {
                case ELeftMenuItemLevel.Description:
                    //lnkDescription.CssClass = "Black_font";
                    lnkDescription.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.TargetColorSpace:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass = "Black_font";
                    lnkTargetColorSpace.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.ProfilerOption:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass =
                    //lnkProfilerOption.CssClass = "Black_font";
                    lnkProfilerOption.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.MeasurementSetup:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass =
                    //lnkProfilerOption.CssClass =
                    //lnkMeasurementSetup.CssClass = "Black_font";
                    lnkMeasurementSetup.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.ValidationSetup:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass =
                    //lnkProfilerOption.CssClass =
                    //lnkMeasurementSetup.CssClass =
                    //lnkValidationSetup.CssClass = "Black_font";
                    lnkValidationSetup.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Save:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass =
                    //lnkProfilerOption.CssClass =
                    //lnkMeasurementSetup.CssClass =
                    //lnkValidationSetup.CssClass =
                    //lnkSave.CssClass = "Black_font";
                    lnkSave.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Measure:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass =
                    //lnkProfilerOption.CssClass =
                    //lnkMeasurementSetup.CssClass =
                    //lnkValidationSetup.CssClass =
                    //lnkSave.CssClass =
                    //lnkMeasure.CssClass = "Black_font";
                    lnkMeasure.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Analyze:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass =
                    //lnkProfilerOption.CssClass =
                    //lnkMeasurementSetup.CssClass =
                    //lnkValidationSetup.CssClass =
                    //lnkSave.CssClass =
                    //lnkMeasure.CssClass =
                    //lnkAnalyze.CssClass = "Black_font";
                    lnkAnalyze.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Download:
                    //lnkDescription.CssClass =
                    //lnkTargetColorSpace.CssClass =
                    //lnkProfilerOption.CssClass =
                    //lnkMeasurementSetup.CssClass =
                    //lnkValidationSetup.CssClass =
                    //lnkSave.CssClass =
                    //lnkMeasure.CssClass =
                    //lnkAnalyze.CssClass =
                    //lnkDownload.CssClass = "Black_font";
                    lnkDownload.Font.Bold = true;
                    break;
            }
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
