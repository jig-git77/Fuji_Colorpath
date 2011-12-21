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
        public int SaveDestinationMeasurement(DestinationProfile objDestinationProfile,MeasurementLink measurementLink)
        {
            var destId = 0;
            if(objDestinationProfile.DestinationProfileId == 0)
            {
                var insertDest = ((IDestinationProfileBusiness)new Business.BusinessFactory<IDestinationProfileBusiness>().CreateObject());
                measurementLink.ProfileId = insertDest.InsertDestinationProfile(objDestinationProfile);
            }
            else
            {
                measurementLink.ProfileId = objDestinationProfile.DestinationProfileId;
            }
            measurementLink.TypeId = ProfileType.DestinationProfile;
            var insertMst = ((ISyncRunBusiness)new Business.BusinessFactory<ISyncRunBusiness>().CreateObject());
            return insertMst.SaveMeasurement(measurementLink);
        }

        [WebInvoke(Method = "POST", UriTemplate = "InsertDeviceLinkProfile")]
        [OperationContract]
        public int SaveDeviceLinkMeasurement(DeviceLinkProfile objDeviceLinkProfile, MeasurementLink measurementLink)
        {
            var destId = 0;
            if (objDeviceLinkProfile.DeviceLinkProfileId == 0)
            {
                var insertDlp = ((IDeviceLinkProfileBusiness)new Business.BusinessFactory<IDeviceLinkProfileBusiness>().CreateObject());
                measurementLink.ProfileId = insertDlp.InsertDeviceLinkProfile(objDeviceLinkProfile);
            }
            else
            {
                measurementLink.ProfileId = objDeviceLinkProfile.DeviceLinkProfileId;
            }

            measurementLink.TypeId = ProfileType.Dlp;
            var insertMst = ((ISyncRunBusiness)new Business.BusinessFactory<ISyncRunBusiness>().CreateObject());
            return insertMst.SaveMeasurement(measurementLink);
        }
    }
}
