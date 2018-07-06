using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FujiFilmUSA.Sync.PluginService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPluginData" in both code and config file together.
    [ServiceContract]
    public interface IPluginData : IViewPluginDataService
    {
        [OperationContract]
        void DoWork();

        [WebInvoke(UriTemplate = "cache", Method = "POST", BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        object UpdateCache(Stream messages);


        [WebInvoke(Method = "POST", UriTemplate = "SaveMeasurement", RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        void SaveMeasurement(Measurement measurement);

        [WebInvoke(Method = "POST", UriTemplate = "SaveVMeasurement")]
        [OperationContract]
        void SaveVMeasurement(Measurement measurement);

        [WebInvoke(Method = "POST", UriTemplate = "Save1DMeasurement")]
        [OperationContract]
        void Save1DMeasurement(Measurement measurement);

        [WebGet(UriTemplate = "GetReferenceChart/{projectId}/{referencechartdisplaytype}/{IsSpotProjectType}")]
        [OperationContract]
        ReferenceChartData GetReferenceChart(string projectId, string referenceChartDisplayType, string IsSpotProjectType);

        [WebInvoke(Method = "GET", UriTemplate = "GetMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        List<Measurement> GetMeasurementsByOptimizeId(string optimizeRunId);

        [WebInvoke(Method = "GET", UriTemplate = "GetVMeasurementsByProjectId/{projectId}")]
        [OperationContract]
        List<Measurement> GetVMeasurementsByProjectId(string projectId);

        [WebInvoke(Method = "GET", UriTemplate = "Get1DMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        List<Measurement> Get1DMeasurementsByOptimizeId(string optimizeRunId);

        [WebInvoke(Method = "POST", UriTemplate = "DeleteMeasurement/{measurementId}")]
        [OperationContract]
        bool DeleteMeasurement(string measurementId);

        [WebInvoke(Method = "POST", UriTemplate = "DeleteVMeasurement/{measurementId}")]
        [OperationContract]
        bool DeleteVMeasurement(string measurementId);

        [WebInvoke(Method = "POST", UriTemplate = "Delete1DMeasurement/{measurementId}")]
        [OperationContract]
        bool Delete1DMeasurement(string measurementId);

        [WebInvoke(Method = "POST", UriTemplate = "KeepUserAlive/{username}")]
        [OperationContract]
        bool KeepUserAlive(string username);

        [WebInvoke(Method = "POST", UriTemplate = "KeepUserSessionAlive/")]
        [OperationContract]
        bool KeepUserSessionAlive();

        [WebInvoke(Method = "POST", UriTemplate = "SaveSpotMeasurement")]
        [OperationContract]
        void SaveSpotMeasurement(Measurement measurement);

        [WebInvoke(Method = "GET", UriTemplate = "GetSpotMeasurementsByOptimizeId/{optimizeRunId}")]
        [OperationContract]
        List<Measurement> GetSpotMeasurementsByOptimizeId(string optimizeRunId);

        [WebInvoke(Method = "POST", UriTemplate = "DeleteSpotMeasurement/{measurementId}")]
        [OperationContract]
        bool DeleteSpotMeasurement(string measurementId);

        //[WebGet(UriTemplate = "stats?key={key}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //Response<object> GetCacheStats(string key);

        //[WebGet(UriTemplate = "config?key={key}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //Response<object> GetCacheConfigData(string key);

        #region IViewPluginDataService Members
        new Measurement Measurement { get; set; }
        new int ProjectId { get; set; }
        new ReferenceChartData ReferenceChartData { get; set; }
        new int OptimizeRunId { get; set; }
        new List<Measurement> Measurements { get; set; }
        new int MeasurementId { get; set; }
        new EReferenceChartDisType ReferenceChartDisType { get; set; }
        new int SpotProjectId { get; set; }
        new ReferenceChartPlugin ReferenceChartPlugin { get; set; }
        #endregion

        #region IViewBase Members

        new int CurrentUserId { get; set; }

        new void ErrorOccured(object sender, ErrorArgs e);


        #endregion
    }
}
