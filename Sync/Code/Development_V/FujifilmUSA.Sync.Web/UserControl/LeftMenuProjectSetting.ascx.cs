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
                        lnkDescription.Enabled = true;
                    }
                    if (SessionManager.Instance.CurrentProject.SourceProfile != null && SessionManager.Instance.CurrentProject.SourceProfile.SourceProfileId > 0)
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
                    break;
                case ECurrentProjectStatus.Edit:
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
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0)
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements)
                        {
                            lnkAnalyze.Enabled = true;
                        }
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile)
                        {
                            lnkDownload.Enabled = true;
                        }
                    }
                    break;
                case ECurrentProjectStatus.Run:
                    //This control won't load...
                    break;
            }

           

            switch (SessionManager.Instance.CurrentNavigation)
            {
                case ELeftMenuItemLevel.Description:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkDescription.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.TargetColorSpace:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.ProfilerOption:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.MeasurementSetup:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                    lnkMeasurementSetup.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.ValidationSetup:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                    lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                    lnkValidationSetup.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Save:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                    lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                    lnkSave.ForeColor = System.Drawing.Color.Black;
                    lnkSave.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Measure:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                    lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                    lnkSave.ForeColor = System.Drawing.Color.Black;
                    lnkMeasure.ForeColor = System.Drawing.Color.Black;
                    lnkMeasure.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Analyze:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                    lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                    lnkSave.ForeColor = System.Drawing.Color.Black;
                    lnkMeasure.ForeColor = System.Drawing.Color.Black;
                    lnkAnalyze.ForeColor = System.Drawing.Color.Black;
                    lnkAnalyze.Font.Bold = true;
                    break;
                case ELeftMenuItemLevel.Download:
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                    lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                    lnkSave.ForeColor = System.Drawing.Color.Black;
                    lnkMeasure.ForeColor = System.Drawing.Color.Black;
                    lnkAnalyze.ForeColor = System.Drawing.Color.Black;
                    lnkDownload.ForeColor = System.Drawing.Color.Black;
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