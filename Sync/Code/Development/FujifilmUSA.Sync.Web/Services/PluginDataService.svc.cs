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
        [WebInvoke(Method = "POST", UriTemplate = "SaveMeasurement")]
        [OperationContract]
        public void SaveMeasurement(Entity.Measurement measurement)
        {
            this.Measurement = measurement;
            var presenter = new PresenterPluginDataService(this);
            presenter.SaveMeasurement();
        }

        [WebGet(UriTemplate = "GetReferenceChart/{projectId}")]
        [OperationContract]
        public ReferenceChartData GetReferenceChart(string projectId)
        {
            this.ProjectId = Convert.ToInt32(projectId);
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

        //[WebInvoke(Method = "POST", UriTemplate = "LogActivityTime/{userId}")]
        //[OperationContract]
        //public void LogActivityTime(string userId)
        //{
        //    LoginRestriction.LogActivityTime(Convert.ToInt32(userId));
        //}

        #region IViewPluginDataService Members
        public Entity.Measurement Measurement { get; set; }
        public int ProjectId { get; set; }
        public ReferenceChartData ReferenceChartData { get; set; }
        public int OptimizeRunId { get; set; }
        public List<Entity.Measurement> Measurements { get; set; }
        public int MeasurementId { get; set; }
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
