using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PluginDataService : IViewPluginDataService
    {
        [WebInvoke(Method = "GET", UriTemplate = "Ping")]
        [OperationContract]
        public bool Ping()
        {
            return true;
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveMeasurement")]
        [OperationContract]
        public void SaveMeasurement(Entity.Measurement measurement)
        {
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.SaveMeasurement();
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveVMeasurement")]
        [OperationContract]
        public void SaveVMeasurement(Entity.Measurement measurement)
        {
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.SaveVMeasurement();
        }

        [WebGet(UriTemplate = "GetReferenceChart/{projectId}/{referencechartdisplaytype}")]
        [OperationContract]
        public ReferenceChartData GetReferenceChart(string projectId, string referenceChartDisplayType)
        {
            this.ProjectId = Convert.ToInt32(projectId);
            this.ReferenceChartDisType = (EReferenceChartDisType) Enum.Parse(typeof (EReferenceChartDisType), referenceChartDisplayType);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetReferenceChartData();
            return ReferenceChartData;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetMeasurementsByOptimizeId/{optimizeRunId}")]        
        [OperationContract]
        public List<Entity.Measurement> GetMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetMeasurementsOnRunId();
            if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            {
                FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            }
            return Measurements;
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetVMeasurementsByProjectId/{projectId}")]
        [OperationContract]
        public List<Entity.Measurement> GetVMeasurementsByProjectId(string projectId)
        {
            this.ProjectId = Convert.ToInt32(projectId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetVMeasurementsOnProjectId();
            if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            {
                FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            }
            return Measurements;
        }

        [WebInvoke(Method = "POST", UriTemplate = "DeleteMeasurement/{measurementId}")]
        [OperationContract]
        public bool DeleteMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);           
            return presenter.DeleteMeasurement();            
        }

        [WebInvoke(Method = "POST", UriTemplate = "DeleteVMeasurement/{measurementId}")]
        [OperationContract]
        public bool DeleteVMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteVMeasurement();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetDensityChartsData/{optimizeRunId}")]
        [OperationContract]
        public DensityCharts GetDensityChartsData(string optimizeRunId)
        {
            OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetDensityChartsData();
            return DensityChartsData;
        }

        [WebInvoke(Method = "POST", UriTemplate = "KeepUserAlive/{username}")]
        [OperationContract]
        public bool KeepUserAlive(string username)
        {

            List<User> users = FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers;
            User user = users.FirstOrDefault(c => c.LoginId == username);
            if (user != null)
                user.LastPingTime = DateTime.Now;
            FujifilmUSA.Sync.Common.CacheManager.Instance.ActiveUsers = users;            
            return true;
        }

        #region IViewPluginDataService Members
        public Entity.Measurement Measurement { get; set; }
        public int ProjectId { get; set; }
        public ReferenceChartData ReferenceChartData { get; set; }
        public int OptimizeRunId { get; set; }
        public List<Entity.Measurement> Measurements { get; set; }
        public int MeasurementId { get; set; }
        public EReferenceChartDisType ReferenceChartDisType { get; set; }
        public DensityCharts DensityChartsData { get; set; }
        #endregion

        #region IViewBase Members

        public int CurrentUserId { get; set; }

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            //Do nothing in the service...
        }

        #endregion
    }
}
