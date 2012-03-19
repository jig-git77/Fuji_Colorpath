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
    public class PluginDataService:IViewPluginDataService
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

        #region IViewPluginDataService Members

        public Entity.Measurement Measurement { get; set; }
        public int ProjectId{ get; set; }
        public ReferenceChartData ReferenceChartData{ get; set; }
        
        #endregion
    }
}
