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

        [WebInvoke(Method = "POST", UriTemplate = "Save1DMeasurement")]
        [OperationContract]
        public void Save1DMeasurement(Entity.Measurement measurement)
        {
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.Save1DMeasurement();
        }

        [WebGet(UriTemplate = "GetReferenceChart/{projectId}/{referencechartdisplaytype}/{IsSpotProjectType}")]
        [OperationContract]
        public ReferenceChartData GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType)
        {
            this.ReferenceChartDisType = (EReferenceChartDisType) Enum.Parse(typeof (EReferenceChartDisType), referenceChartDisplayType);
            var presenter = new PresenterPluginDataService(this);
            if (Convert.ToBoolean(IsSpotProjectType) == false)
            {
                this.ProjectId = Convert.ToInt32(projectId);
                presenter.GetReferenceChartData();
            }
            else
            {
                this.SpotProjectId = Convert.ToInt32(projectId);
                presenter.GetSpotReferenceChartData();
            }
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

        [WebInvoke(Method = "GET", UriTemplate = "Get1DMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        public List<Entity.Measurement> Get1DMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.Get1DMeasurementsOnRunId();
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

        [WebInvoke(Method = "POST", UriTemplate = "Delete1DMeasurement/{measurementId}")]
        [OperationContract]
        public bool Delete1DMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.Delete1DMeasurement();
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

        [WebInvoke(Method = "POST", UriTemplate = "KeepUserSessionAlive/")]
        [OperationContract]
        public bool KeepUserSessionAlive()
        {
            return true;
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveSpotMeasurement")]
        [OperationContract]
        public void SaveSpotMeasurement(Entity.Measurement measurement)
        {
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.SaveSpotMeasurement();
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetSpotMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        public List<Entity.Measurement> GetSpotMeasurementsByOptimizeId(string optimizeRunId)
        {
            this.OptimizeRunId = Convert.ToInt32(optimizeRunId);
            var presenter = new PresenterPluginDataService(this);
            presenter.GetSpotMeasurementsOnRunId();
            if (Measurements.Count == 0) //------If someone delete all measurements, and still able to go to Analyze page, then set this to false
            {
                FujifilmUSA.Sync.Common.SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = false;
            }
            return Measurements;
        }

        [WebInvoke(Method = "POST", UriTemplate = "DeleteSpotMeasurement/{measurementId}")]
        [OperationContract]
        public bool DeleteSpotMeasurement(string measurementId)
        {
            MeasurementId = Convert.ToInt32(measurementId);
            var presenter = new PresenterPluginDataService(this);
            return presenter.DeleteSpotMeasurement();
        }

        #region IViewPluginDataService Members
        public Entity.Measurement Measurement { get; set; }
        public int ProjectId { get; set; }
        public ReferenceChartData ReferenceChartData { get; set; }
        public int OptimizeRunId { get; set; }
        public List<Entity.Measurement> Measurements { get; set; }
        public int MeasurementId { get; set; }
        public EReferenceChartDisType ReferenceChartDisType { get; set; }
        public int SpotProjectId { get; set; }
        public ReferenceChartPlugin ReferenceChartPlugin { get; set; }
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
