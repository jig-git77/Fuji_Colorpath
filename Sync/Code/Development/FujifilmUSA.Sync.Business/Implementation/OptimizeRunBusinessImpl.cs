using System;
using System.Data;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Business.ProfilerService;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class OptimizeRunBusinessImpl : IOptimizeRunBusiness
    {
        #region IOptimizeRunBusiness Members

        public void CreateOptimizeRun(int projectId, int status, DateTime startDateTime)
        {
            DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.CreateOptimizeRun",
                                            new object[] { projectId, status, startDateTime });
        }

        public void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds)
        {
            var sourceProfile = BusinessFactory<IProjectBusiness>.CreateObject().GetSourceProfile(projectId);
            var request = new CreateDestinationAndDeviceLinkProfileRequest()
                              {
                                  PcsData = new List<double>(),
                                  DcsData = new List<double>(),
                                  SourceProfile = new IccProfile() { ProfileData = sourceProfile }
                              };

            var measurementDataResponse = BusinessFactory<IMeasurementBusiness>.CreateObject().GetMeasurementData(measurementIds);
            request.PcsData = measurementDataResponse.PcsData;
            request.DcsData = measurementDataResponse.DcsData;

            var profilerOptionBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            var destinationOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DestinationProfile);
            foreach (KeyValuePair<string, string> option in destinationOptions)
            {
                request.DestinationProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
            }

            var dlpOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
            foreach (KeyValuePair<string, string> option in dlpOptions)
            {
                request.DlpOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
            }

            using (var client = new ProfilerClient())
            {
                var response = client.CreateDestinationAndDeviceLinkProfile(request);
                DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.UpdateInitialDestinationAndDlp",
                                                            new object[] { optimizeRunId, response.DestinationProfile.ProfileData, response.DeviceLinkProfile.ProfileData });
            }
        }


        public byte[] GetDeviceLinkProfile(int optimizeRunId)
        {
            return (byte[])DataAccessFactory.Instance.GetScalar("CPSync4D.GetDeviceLinkProfile", new object[] { optimizeRunId });
        }

        public List<Measurement> GetMeasurements(int optimizeRunId)
        {
            var dsMeasurements = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetMeasurementsByOptimizeRunID", new object[] { optimizeRunId });
            var measurements = dsMeasurements.Tables[0].AsEnumerable().
                Select(x => new Measurement()
                                {
                                    MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                                    MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                                    MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                                    OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                                    IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                }).ToList();
            return measurements;
        }

        public void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds)
        {
            var sourceProfile = BusinessFactory<IProjectBusiness>.CreateObject().GetSourceProfile(projectId);
            var destinationProfile = BusinessFactory<IProjectBusiness>.CreateObject().GetDestinationProfile(projectId);
            var deviceLinkProfile = BusinessFactory<IProjectBusiness>.CreateObject().GetLastOptimizedDlp(projectId);
            var request = new OptimizeDeviceLinkProfileRequest()
            {
                PCSData = new List<double>(),
                DCSData = new List<double>(),
                SourceProfile = new IccProfile() { ProfileData = sourceProfile },
                DestinationProfile = new IccProfile() { ProfileData = destinationProfile },
                DeviceLinkProfile = new IccProfile() { ProfileData = deviceLinkProfile }
            };

            var measurementDataResponse = BusinessFactory<IMeasurementBusiness>.CreateObject().GetMeasurementData(measurementIds);
            request.PCSData = measurementDataResponse.PcsData;
            request.DCSData = measurementDataResponse.DcsData;

            var options = BusinessFactory<IProjectBusiness>.CreateObject().
                GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
            foreach (KeyValuePair<string, string> option in options)
            {
                request.Options.Add(new Parameter() { Name = option.Key, Value = option.Value });
            }

            using (var client = new ProfilerClient())
            {
                var response = client.OptimizeDeviceLinkProfile(request);
                DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.UpdateOptimizeDlp",
                                                            new object[] { optimizeRunId, response.OptimizedProfile.ProfileData });
            }
        }

        #endregion
    }
}