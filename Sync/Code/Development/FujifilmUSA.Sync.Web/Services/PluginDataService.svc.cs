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

        #region IViewPluginDataService Members
        public Entity.Measurement Measurement { get; set; }
        public int ProjectId { get; set; }
        public ReferenceChartData ReferenceChartData { get; set; }
        public int OptimizeRunId { get; set; }
        public List<Entity.Measurement> Measurements { get; set; }
        #endregion

        #region IViewError Members

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
