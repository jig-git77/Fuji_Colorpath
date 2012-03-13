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
                case ELeftMenuItemLevel.Description:
                    lnkDescription.Enabled = true;
                    break;
                case ELeftMenuItemLevel.TargetColorSpace:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        true;
                    break;
                case ELeftMenuItemLevel.ProfilerOption:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        true;
                    break;
                case ELeftMenuItemLevel.MeasurementSetup:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        true;
                    break;
                case ELeftMenuItemLevel.Save:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        lnkSave.Enabled =
                        true;
                    break;
                case ELeftMenuItemLevel.Measure:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        lnkSave.Enabled =
                        lnkMeasure.Enabled =
                        true;
                    break;
                case ELeftMenuItemLevel.Analyze:
                    lnkDescription.Enabled =
                        lnkTargetColorSpace.Enabled =
                        lnkProfilerOption.Enabled =
                        lnkMeasurementSetup.Enabled =
                        lnkSave.Enabled =
                        lnkMeasure.Enabled =
                        lnkAnalyze.Enabled =
                        true;
                    break;
                case ELeftMenuItemLevel.Download:
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
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;
                            break;
                        case "lnkTargetColorSpace":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.TargetColorSpace;
                            break;
                        case "lnkProfilerOption":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
                            break;
                        case "lnkMeasurementSetup":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
                            break;
                        case "lnkSave":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
                            break;
                        case "lnkMeasure":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                            break;
                        case "lnkAnalyze":
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                            break;
                    }
                    Response.Redirect("~/AddProject.aspx");
                }
            }
        }
    }
}