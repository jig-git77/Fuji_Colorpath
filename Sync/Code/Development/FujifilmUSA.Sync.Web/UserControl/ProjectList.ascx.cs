using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProductList : System.Web.UI.UserControl, IViewProjectList
    {
        #region "Declaration"
        private PresenterProjectList _presenterprojectlist;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenterprojectlist = new PresenterProjectList(this); 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
        }

        protected void grdProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindProject();
        }
        #endregion

        #region Method
        private void BindProject()
        {
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
                _presenterprojectlist.GetProjectListBySyncDeviceId();
                grdProjectList.DataSource = GetProjectListBySyncDeviceId;
                //grdProjectList.DataBind();
            }
        }
        #endregion

        #region IViewProjectList Members
        public int SyncDeviceId { get; set; }
        public List<Project> GetProjectListBySyncDeviceId { get; set; }
        public int ProjectId { get; set; }
        public int DeleteProject { get; set; }
        #endregion
    }
}