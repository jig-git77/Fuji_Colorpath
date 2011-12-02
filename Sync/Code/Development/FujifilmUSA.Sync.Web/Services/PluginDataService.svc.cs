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
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [WebGet]
        [OperationContract]
        public ReferenceChartData GetReferenceChart(int projectId)
        {
            var refChartData = ((IReferenceChartBusiness)new Business.BusinessFactory<IReferenceChartBusiness>().CreateObject());
            return refChartData.GetReferenceChartData(projectId);
        }

        [WebGet]
        [OperationContract]
        public string GetData()
        {
            return "Hi Jay";
        }
    }
}
