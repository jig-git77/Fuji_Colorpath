using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProjectSettingsContainer : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            LoadProjectSettingControls();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadProjectSettingControls()
        {
            ph.Controls.Clear();
            var ctrl = new System.Web.UI.Control();
            switch (SessionManager.Instance.CurrentNavigation)
            {
                case LeftMenuItemLevel.Description:
                    ctrl = this.LoadControl("~/UserControl/AddProject/ProjectDescription.ascx");
                    ctrl.ID = "ctrl_ProjectDescription";
                    ph.Controls.Add(ctrl);
                    break;
                case LeftMenuItemLevel.TargetColorSpace:
                    ctrl = this.LoadControl("~/UserControl/AddProject/TargetColorSpace.ascx");
                    ctrl.ID = "ctrl_TargetColorSpace";
                    ph.Controls.Add(ctrl);
                    break;
                case LeftMenuItemLevel.ProfilerOption:
                    ctrl = this.LoadControl("~/UserControl/AddProject/ProfilerOptions.ascx");
                    ctrl.ID = "ctrl_ProfilerOptions";
                    ph.Controls.Add(ctrl);
                    break;
                case LeftMenuItemLevel.MeasurementSetup:
                    ctrl = this.LoadControl("~/UserControl/AddProject/MeasurementSetup.ascx");
                    ctrl.ID = "ctrl_MeasurementSetup";
                    ph.Controls.Add(ctrl);
                    break;
                case LeftMenuItemLevel.Save:
                    ctrl = this.LoadControl("~/UserControl/AddProject/SavePrintCondition.ascx");
                    ctrl.ID = "ctrl_SavePrintCondition";
                    ph.Controls.Add(ctrl);
                    break;
                case LeftMenuItemLevel.Measure:
                    break;
                case LeftMenuItemLevel.Analyze:
                    break;
                case LeftMenuItemLevel.Download:
                    break;
            }
        }

        //switch (Convert.ToString(Request.QueryString["Stage"]))
        //   {
        //       case "1":
        //           hTitle.InnerText = "New Print Condition - Description";
        //           lbtnCreateNewPrintCondition.ValidationGroup = "vgDescription";
        //           lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Traget ColorSpace<span>";
        //           break;
        //       case "2":
        //           hTitle.InnerText = "New Print Condition - Target Colorspace";
        //           lbtnCreateNewPrintCondition.ValidationGroup = "";
        //           lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Profiler Options<span>";
        //           break;
        //       case "3":
        //           hTitle.InnerText = "New Print Condition - Profiler Options";
        //           lbtnCreateNewPrintCondition.ValidationGroup = "";
        //           lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Measurement Setup<span>";
        //           break;
        //       case "4":
        //           hTitle.InnerText = "New Print Condition - Measurement Setup";
        //           lbtnCreateNewPrintCondition.ValidationGroup = "vgMeasurementSetup";
        //           lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Comfirm Save<span>";
        //           break;
        //       case "5":
        //           hTitle.InnerText = "New Print Condition - Save Print Condition";
        //           lbtnCreateNewPrintCondition.ValidationGroup = "";
        //           lbtnCreateNewPrintCondition.Text = "<span><img id='imgAdd' src='Images/new_project_icon.png'/>Save<span>";
        //           lbtnCreateNewPrintCondition.Attributes.Add("onclick", "javascript: return OpenInformationBox('" + ResolveUrl("~/PrintCondition.aspx?ModelControlId=3") + "')");
        //           break;

        //   }
    }
}