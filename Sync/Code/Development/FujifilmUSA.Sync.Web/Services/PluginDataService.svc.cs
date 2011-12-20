using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PluginDataService
    {
        [WebGet(UriTemplate = "GetReferenceChart/{projectId}")]
        [OperationContract]
        public ReferenceChartData GetReferenceChart(string projectId)
        {
            var refChartData = ((IReferenceChartBusiness)new Business.BusinessFactory<IReferenceChartBusiness>().CreateObject());
            return refChartData.GetReferenceChartData(Convert.ToInt32(projectId));
        }

        [WebGet(UriTemplate = "GetReferenceChartByTypeId/{ReferenceChartTypeId}/{SerialNo}")]
        [OperationContract]
        public ReferenceChartData GetReferenceChartByTypeId(string referenceChartTypeId, string serialNo)
        {
            var refChartData = ((IReferenceChartBusiness)new Business.BusinessFactory<IReferenceChartBusiness>().CreateObject());
            return refChartData.GetReferenceChartDataByTypeId(Convert.ToInt32(referenceChartTypeId), serialNo);
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveMeasurement")]
        [OperationContract]
        public int SaveMeasurement(Measurement measurement)
        {
            var refChartData = ((ISyncRunBusiness)new Business.BusinessFactory<ISyncRunBusiness>().CreateObject());
            return refChartData.SaveMeasurement(measurement);
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveMeasurementLink")]
        [OperationContract]
        public int SaveMeasurementLink(MeasurementLink measurementLink)
        {
            var refChartData = ((ISyncRunBusiness)new Business.BusinessFactory<ISyncRunBusiness>().CreateObject());
            return refChartData.SaveMeasurement(measurementLink);
        }
    }
}
