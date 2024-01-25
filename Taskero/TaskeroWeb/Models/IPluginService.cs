using System.ServiceModel;
using System.ServiceModel.Web;
using TaskeroWeb.Models.Classes;

namespace TaskeroWeb.Models
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPluginService" in both code and config file together.
    [ServiceContract]
    public interface IPluginService
    {
        [OperationContract]
        [WebInvoke(
        Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveMeasurement")]
        void SaveMeasurement(Measurement objMeasurement);


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetStatus")]
        string GetStatus();


        [OperationContract]
        [WebInvoke(
        Method = "POST",
        BodyStyle = WebMessageBodyStyle.Wrapped,
        ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetReferenceChart")]
        ReferenceChart GetReferenceChart(int ProjectID);



    }
}
