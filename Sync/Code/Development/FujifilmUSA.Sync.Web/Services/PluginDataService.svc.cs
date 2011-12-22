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

        [WebInvoke(Method = "POST", UriTemplate = "SaveDestinationMeasurement")]
        [OperationContract]
        public int SaveDestinationMeasurement(SaveDestinationMeasurementRequest request)
        {
            var destId = 0;
            if (request.DestinationProfile.DestinationProfileId == 0)
            {
                var insertDest = ((IDestinationProfileBusiness)new Business.BusinessFactory<IDestinationProfileBusiness>().CreateObject());
                request.MeasurementLink.ProfileId = insertDest.InsertDestinationProfile(request.DestinationProfile);
            }
            else
            {
                request.MeasurementLink.ProfileId = request.DestinationProfile.DestinationProfileId;
            }
            request.MeasurementLink.TypeId = ProfileType.DestinationProfile;
            var insertMst = ((ISyncRunBusiness)new Business.BusinessFactory<ISyncRunBusiness>().CreateObject());
            insertMst.SaveMeasurement(request.MeasurementLink);
            return request.MeasurementLink.ProfileId;
        }

        [WebInvoke(Method = "POST", UriTemplate = "SaveDeviceLinkMeasurement")]
        [OperationContract]
        public int SaveDeviceLinkMeasurement(SaveDeviceLinkMeasurementRequest request)
        {
            var destId = 0;
            if (request.DeviceLinkProfile.DeviceLinkProfileId == 0)
            {
                var insertDlp = ((IDeviceLinkProfileBusiness)new Business.BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject());
                request.MeasurementLink.ProfileId = insertDlp.InsertDeviceLinkProfile(request.DeviceLinkProfile);
            }
            else
            {
                request.MeasurementLink.ProfileId = request.DeviceLinkProfile.DeviceLinkProfileId;
            }

            request.MeasurementLink.TypeId = ProfileType.Dlp;
            var insertMst = ((ISyncRunBusiness)new Business.BusinessFactory<ISyncRunBusiness>().CreateObject());
            insertMst.SaveMeasurement(request.MeasurementLink);
            return request.MeasurementLink.ProfileId;
        }


        [WebInvoke(Method = "GET", UriTemplate = "GetReferenceChartTypeListByMeasurementDeviceId/{measurementDeviceId}")]
        [OperationContract]
        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(string measurementDeviceId)
        {
            var refChartData = ((IMiscDataBusiness)new Business.BusinessFactory<IMiscDataBusiness>().CreateObject());
            return refChartData.GetReferenceChartTypeListByMeasurementDeviceId(Convert.ToInt32(measurementDeviceId));
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetMeasurementsBySyncId/{syncRunId}")]
        [OperationContract]
        public List<Measurement> GetMeasurementsBySyncId(int syncRunId)
        {
            var getMeasurement = (ISyncRunBusiness)new Business.BusinessFactory<ISyncRunBusiness>().CreateObject();
            return getMeasurement.GetMeasurement(syncRunId);
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetMeasurementsByProfileType/{profileId}/{typeId}")]
        [OperationContract]
        public List<Measurement> GetMeasurementsByProfileType(int profileId, ProfileType typeId)
        {
            var getMeasurement = (IProfileBusiness)new Business.BusinessFactory<IProfileBusiness>().CreateObject();
            return getMeasurement.GetMeasurement(profileId, typeId);
        }

    }
}
