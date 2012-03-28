using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProjectDescription : BaseControl
    {
        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDeviceInfoFromSession();
            }
        }
        #endregion

        #region Methods
        private void LoadDeviceInfoFromSession()
        {
            if (SessionManager.Instance.CurrentDevice.DeviceName!=null)
            {
                txtPress.Text = SessionManager.Instance.CurrentDevice.DeviceName;
                txtDeviceFamily.Text = SessionManager.Instance.CurrentDevice.DeviceFamily.DeviceFamilyDesc;
            }
        }
        #endregion

        #region Control Events
        protected void lnkbtnPrintDescription_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.PrintCondition = new FujifilmUSA.Sync.Entity.PrintCondition {   
                                                                                Media = txtMedia.Text.Trim(),
                                                                                Colorant=txtColorant.Text.Trim(),
                                                                                RipSettings=txtRipSetting.Text.Trim(),
                                                                                Notes=txtNotes.Text.Trim(),
                                                                                TechnicianName=txtTechnicianName.Text.Trim()
                                                                            };

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.TargetColorSpace;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }
        #endregion
    }
}
