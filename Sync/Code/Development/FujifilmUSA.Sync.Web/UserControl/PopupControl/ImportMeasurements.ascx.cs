using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using System.Collections;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class ImportMeasurements : BaseControl, IViewImportMeasurements
    {
        #region Variable Declaration
        private PresenterImportMeasurements _presenterImportMeasurements;       
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterImportMeasurements = new PresenterImportMeasurements(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rgProjectList.PageSize = Common.Utilities.PageSize;

        }

        protected void rgProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetProjectList();
        }

        protected void rgProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {               
                case "ClearFilter":
                    foreach (GridColumn column in rgProjectList.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgProjectList.MasterTableView.FilterExpression = string.Empty;
                    rgProjectList.MasterTableView.Rebind();
                    break;
            }
        }

        protected void lbtnImport_Click(object sender, EventArgs e)
        {

        }

        private void GetProjectList()
        {
            Projects = new List<Project>();
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            LicenseTypeID = Convert.ToInt32(SessionManager.Instance.CurrentOperation);
            _presenterImportMeasurements.GetProjectList();
            if (!IsErrorOccured)
            {
                rgProjectList.DataSource = Projects;
            }
           
        }

        #region IViewImportMeasurements Members
        public List<Project> Projects { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public int LicenseTypeID { get; set; }
        public KeyValuePair<short, string> Measurements { get; set; }
        #endregion
    }
}