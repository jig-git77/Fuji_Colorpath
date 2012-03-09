using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuProjectSetting : System.Web.UI.UserControl
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            lnkDescription.Enabled =
                lnkTargetColorSpace.Enabled =
                lnkProfilerOption.Enabled =
                lnkMeasurementSetup.Enabled =
                lnkSave.Enabled =
                lnkMeasure.Enabled =
                lnkAnalyze.Enabled =
                lnkDownload.Enabled = false;

            switch (SessionManager.Instance.CurrentNavigation)
            {
                case LeftMenuItemLevel.Description:
                    lnkDescription.Enabled = true;
                    break;
                case LeftMenuItemLevel.TargetColorSpace:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        true;
                    break;
                case LeftMenuItemLevel.ProfilerOption:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        true;
                    break;
                case LeftMenuItemLevel.MeasurementSetup:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        true;
                    break;
                case LeftMenuItemLevel.Save:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        lnkSave.Enabled =
                        true;
                    break;
                case LeftMenuItemLevel.Measure:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        lnkSave.Enabled =
                        lnkMeasure.Enabled =
                        true;
                    break;
                case LeftMenuItemLevel.Analyze:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        lnkSave.Enabled =
                        lnkMeasure.Enabled =
                        lnkAnalyze.Enabled =
                        true;
                    break;
                case LeftMenuItemLevel.Download:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        lnkSave.Enabled =
                        lnkMeasure.Enabled =
                        lnkAnalyze.Enabled =
                        lnkDownload.Enabled = true;
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
                            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Description;
                            break;
                        case "lnkTargetColorSpace":
                            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.TargetColorSpace;
                            break;
                        case "lnkProfilerOption":
                            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.ProfilerOption;
                            break;
                        case "lnkMeasurementSetup":
                            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.MeasurementSetup;
                            break;
                        case "lnkSave":
                            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Save;
                            break;
                        case "lnkMeasure":
                            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Measure;
                            break;
                        case "lnkAnalyze":
                            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Analyze;
                            break;
                    }
                    Response.Redirect("~/AddProject.aspx");
                }
            }
        }
    }
}