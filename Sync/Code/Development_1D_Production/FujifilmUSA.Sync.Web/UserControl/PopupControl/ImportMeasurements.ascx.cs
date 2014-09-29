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

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterImportMeasurements = new PresenterImportMeasurements(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            rgProjectList.PageSize = Common.Utilities.PageSize;
        }
        #endregion

        #region Control Events
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
            Measurements = new List<int>();

            int parentProjectMeasurementCount = 0;
            #region Check for current project existing measurement
            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            LicenseTypeID = (int)SessionManager.Instance.CurrentProject.LicenseType;
            _presenterImportMeasurements.GetMeasurementIDsbyProjectID();
            if (!IsErrorOccured)
            {
                parentProjectMeasurementCount = Measurements.Count;
            }

            #endregion

            Measurements = new List<int>();
        
            //---------------Get ProjectID from selected checkbox-----------------//
            foreach (GridDataItem grdItem in rgProjectList.Items)
            {
                CheckBox chkbtnSelect = (CheckBox)grdItem.FindControl("chkbtnSelect");

                if (chkbtnSelect.Checked)
                {   
                    ProjectId = Convert.ToInt32(grdItem.GetDataKeyValue("ProjectId").ToString());
                }
            }
         
            //---------Get list of Measurement IDs with above selected Project-----------------//
            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            LicenseTypeID = (int)SessionManager.Instance.CurrentProject.LicenseType;
            _presenterImportMeasurements.GetMeasurementIDsbyProjectID();

            if (!IsErrorOccured)
            {
                if (parentProjectMeasurementCount + Measurements.Count > (FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentProject.LicenseType == FujifilmUSA.Sync.Entity.ELicenseType.Sync1D ? Business.ApplicationConfiguration.Instance.MeasurementLimitSync1D : Business.ApplicationConfiguration.Instance.MeasurementsLimit))
                {
                    fujiStatusbar.DisplayMessage("Measurements cannot be imported as total allowed measurement limit exceeded.", false);
                    return;
                }

                //-------------Import all Measurements one by one using above Measurement IDs list----------------//
                for (int i = 0; i < Measurements.Count; i++)
                {
                    MeasurementID = Convert.ToInt32(Measurements[i].ToString());
                    OptimizeRunID = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                    _presenterImportMeasurements.ImportMeasurements();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", "<script type=text/javascript> CloseImportWindow(); </script>");  
            }
        }
        #endregion

        #region Methods
        private void GetProjectList()
        {
            Projects = new List<Project>();
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            LicenseTypeID =(int)SessionManager.Instance.CurrentProject.LicenseType;
            _presenterImportMeasurements.GetProjectList();

            if (!IsErrorOccured)
            {
                Projects.RemoveAll(item => item.ProjectId == SessionManager.Instance.CurrentProject.ProjectId); //-----Remove the current project from project list------//
                rgProjectList.DataSource = Projects;
            }           
        }
        #endregion

        #region IViewImportMeasurements Members
        public List<Project> Projects { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public int LicenseTypeID { get; set; }
        public List<int> Measurements { get; set; }
        public int MeasurementID { get; set; }
        public int OptimizeRunID { get; set; }
        #endregion
    }
}