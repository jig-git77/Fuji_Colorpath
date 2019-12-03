using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProjectSettingsContainer : BaseControl
    {
        protected override void OnInit(EventArgs e)
        {
            LoadProjectSettingControls();
        }

        public void LoadProjectSettingControls()
        {
            ph.Controls.Clear();
            var ctrl = new System.Web.UI.Control();
            switch (SessionManager.Instance.CurrentNavigation)
            {
                case ELeftMenuItemLevel.Description:
                    ctrl = this.LoadControl("~/UserControl/AddProject/ProjectDescription.ascx");
                    ctrl.ID = "ctrl_ProjectDescription";                    
                    ph.Controls.Add(ctrl);
                    break;
                case ELeftMenuItemLevel.TargetColorSpace:
                    ctrl = this.LoadControl("~/UserControl/AddProject/TargetColorSpace.ascx");
                    ctrl.ID = "ctrl_TargetColorSpace";
                    ph.Controls.Add(ctrl);
                    break;
                case ELeftMenuItemLevel.ProfilerOption:
                    ctrl = this.LoadControl("~/UserControl/AddProject/ProfilerOptions.ascx");                    
                    ctrl.ID = "ctrl_ProfilerOptions";
                    ph.Controls.Add(ctrl);
                    break;
                case ELeftMenuItemLevel.MeasurementSetup:
                    ctrl = this.LoadControl("~/UserControl/AddProject/MeasurementSetup.ascx");
                    ctrl.ID = "ctrl_MeasurementSetup";
                    ph.Controls.Add(ctrl);
                    break;
                case ELeftMenuItemLevel.ValidationSetup:
                    ctrl = this.LoadControl("~/UserControl/AddProject/ValidationSetup.ascx");
                    ctrl.ID = "ctrl_ValidationSetup";
                    ph.Controls.Add(ctrl);
                    break;
                case ELeftMenuItemLevel.Save:
                    //ctrl = this.LoadControl("~/UserControl/AddProject/PreviewPrintCondition.ascx");
                    ctrl = this.LoadControl("~/UserControl/AddProject/PreviewPrintConditionNew.ascx");
                    ctrl.ID = "ctrl_SavePrintCondition";
                    ph.Controls.Add(ctrl);
                    break;
                case ELeftMenuItemLevel.Measure:
                    break;
                case ELeftMenuItemLevel.Analyze:
                    break;
                case ELeftMenuItemLevel.Download:
                    break;
            }
        }
    }
}