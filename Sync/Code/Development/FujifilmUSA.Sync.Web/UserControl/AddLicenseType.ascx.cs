using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddLicenseType : System.Web.UI.UserControl, IViewAddLicenseTypeManagement
    {
        private PresenterLicenseManagement _presenterLicenseManagement;
        
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterLicenseManagement = new PresenterLicenseManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
        }
        #endregion

        #region Methods
        private void SaveLicenseType()
        {
            LicenseType = new LicenseType()
                               {
                                   Type =   txtLicenseType.Text
                               };
            _presenterLicenseManagement.InsertLicenseType();
            Response.Redirect("LicenseManagement.aspx");
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveLicenseType();
        }
        #endregion

        #region Implement Interface
        public LicenseType LicenseType { get; set; }
        #endregion




        
    }
}