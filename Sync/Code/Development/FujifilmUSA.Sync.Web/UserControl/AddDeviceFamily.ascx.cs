using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDeviceFamily : BaseControl, IViewAddDeviceFamilyManagement
    {
        private PresenterDeviceManagement _presenterDeviceManagement;
        
        
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceManagement = new PresenterDeviceManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
        }
        #endregion

        #region Methods
        private void SaveDeviceFamily()
        {
            DeviceFamily = new DeviceFamily()
                               {
                                   DeviceFamilyDesc = txtDeviceFamilyName.Text,
                                   IsActive = chkIsActive.Checked
                               };
            _presenterDeviceManagement.InsertDeviceFamily();
            Response.Redirect("DeviceManagement.aspx");
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveDeviceFamily();
        }
        #endregion

        #region Implement Interface
        public DeviceFamily DeviceFamily { get; set; }
        #endregion
    }
}