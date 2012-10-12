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
            if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartTypeID.Key > 0)
            {
                lnkValidationSetup.Enabled = true;
            }
            if (SessionManager.Instance.CurrentProject.ProjectId > 0)
            {
                lnkSave.Enabled = true;
            }
            if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0)
            {
                lnkMeasure.Enabled = true;
                if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements)
                {
                    lnkAnalyze.Enabled = true;
                }
                if (SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile)
                {
                    lnkDownload.Enabled = true;
                }
            }

            lnkDescription.Font.Bold =
            lnkTargetColorSpace.Font.Bold =
            lnkProfilerOption.Font.Bold =
            lnkMeasurementSetup.Font.Bold =
            lnkValidationSetup.Font.Bold =
            lnkSave.Font.Bold =
            lnkMeasure.Font.Bold =
            lnkAnalyze.Font.Bold =
            lnkDownload.Font.Bold = false;

            switch (SessionManager.Instance.CurrentNavigation)
            {
                case ELeftMenuItemLevel.Description:
                    lnkDescription.Font.Bold = true;
                    lnkDescription.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasMeasurements))
                    {
                        lnkTargetColorSpace.ForeColor = System.Drawing.Color.Silver;
                        lnkProfilerOption.ForeColor = System.Drawing.Color.Silver;
                        lnkMeasurementSetup.ForeColor = System.Drawing.Color.Silver;
                        lnkValidationSetup.ForeColor = System.Drawing.Color.Silver;
                        lnkSave.ForeColor = System.Drawing.Color.Silver;
                        lnkMeasure.ForeColor = System.Drawing.Color.Silver;
                        lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                            lnkDownload.ForeColor = System.Drawing.Color.Silver;
                        }
                    }
                    break;
                case ELeftMenuItemLevel.TargetColorSpace:
                    lnkTargetColorSpace.Font.Bold = true;
                    lnkTargetColorSpace.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasMeasurements))
                    {
                        lnkProfilerOption.ForeColor = System.Drawing.Color.Silver;
                        lnkMeasurementSetup.ForeColor = System.Drawing.Color.Silver;
                        lnkValidationSetup.ForeColor = System.Drawing.Color.Silver;
                        lnkSave.ForeColor = System.Drawing.Color.Silver;
                        lnkMeasure.ForeColor = System.Drawing.Color.Silver;
                        lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                            lnkDownload.ForeColor = System.Drawing.Color.Silver;
                        }
                    }
                    break;
                case ELeftMenuItemLevel.ProfilerOption:
                    lnkProfilerOption.Font.Bold = true;
                    lnkProfilerOption.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasMeasurements))
                    {
                        lnkMeasurementSetup.ForeColor = System.Drawing.Color.Silver;
                        lnkValidationSetup.ForeColor = System.Drawing.Color.Silver;
                        lnkSave.ForeColor = System.Drawing.Color.Silver;
                        lnkMeasure.ForeColor = System.Drawing.Color.Silver;
                        lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                            lnkDownload.ForeColor = System.Drawing.Color.Silver;
                        }
                    }
                    break;
                case ELeftMenuItemLevel.MeasurementSetup:
                    lnkMeasurementSetup.Font.Bold = true;
                    lnkMeasurementSetup.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasMeasurements))
                    {
                        lnkSave.ForeColor = System.Drawing.Color.Silver;
                        lnkMeasure.ForeColor = System.Drawing.Color.Silver;
                        lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                            lnkDownload.ForeColor = System.Drawing.Color.Silver;
                        }
                    }
                    break;
                case ELeftMenuItemLevel.ValidationSetup:
                    lnkValidationSetup.Font.Bold = true;
                    lnkValidationSetup.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasMeasurements))
                    {
                        lnkSave.ForeColor = System.Drawing.Color.Silver;
                        lnkMeasure.ForeColor = System.Drawing.Color.Silver;
                        lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                            lnkDownload.ForeColor = System.Drawing.Color.Silver;
                        }
                    }
                    break;

                case ELeftMenuItemLevel.Save:
                    lnkSave.Font.Bold = true;
                    lnkSave.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasMeasurements))
                    {
                        lnkMeasure.ForeColor = System.Drawing.Color.Silver;
                        lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                            lnkDownload.ForeColor = System.Drawing.Color.Silver;
                        }
                    }
                    break;
                case ELeftMenuItemLevel.Measure:
                    lnkMeasure.Font.Bold = true;
                    lnkMeasure.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasMeasurements))
                    {
                        lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            lnkAnalyze.ForeColor = System.Drawing.Color.Silver;
                            lnkDownload.ForeColor = System.Drawing.Color.Silver;
                        }
                    }
                    break;
                case ELeftMenuItemLevel.Analyze:
                    lnkAnalyze.Font.Bold = true;
                    lnkAnalyze.ForeColor = System.Drawing.Color.Black;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                    {
                        lnkDownload.ForeColor = System.Drawing.Color.Silver;
                    }
                    break;
                case ELeftMenuItemLevel.Download:
                    lnkDownload.Font.Bold = true;
                    lnkDownload.ForeColor = System.Drawing.Color.Black;
                    break;
            }

            if(SessionManager.Instance.CurrentProject.CurrentDisplayStatus == ECurrentProjectStatus.Edit)
            {
                //----------When Editing Project Details, below can not be edited----------------//
                lnkMeasure.Enabled = false;
                //lnkAnalyze.Enabled = false;
                lnkDescription.Enabled = false;
                lnkTargetColorSpace.Enabled = false;
                lnkProfilerOption.Enabled = false;
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