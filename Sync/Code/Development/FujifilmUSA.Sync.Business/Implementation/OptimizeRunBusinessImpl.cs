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

        public void CreateOptimizeRun(int projectId, int status, int closeStatus, DateTime startDateTime)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "CPSync4D.CreateOptimizeRun",
                        new object[]
                            {
                                projectId,
                                status,
                                closeStatus,
                                startDateTime
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateOptimizeRun"));
        }

        public void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    var sourceProfile =
                        BusinessFactory<IProjectBusiness>.
                            CreateObject().
                            GetSourceProfile(projectId);
                    var request = new CreateDestinationAndDeviceLinkProfileRequest()
                                      {
                                          PcsData = new List<double>(),
                                          DcsData = new List<double>(),
                                          SourceProfile = new IccProfile() { ProfileData = sourceProfile },
                                          DestinationProfileOptions = new List<Parameter>(),
                                          DlpOptions = new List<Parameter>()
                                      };

                    var measurementDataResponse =
                        BusinessFactory<IMeasurementBusiness>.
                            CreateObject().
                            GetMeasurementData(measurementIds);
                    request.PcsData = measurementDataResponse.PcsData;
                    request.DcsData = measurementDataResponse.DcsData;

                    var profilerOptionBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                    var destinationOptions =
                        profilerOptionBusiness.
                            GetProjectOption(projectId, EProfileType.DestinationProfile);
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
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "CPSync4D.UpdateInitialDestinationAndDlp",
                                new object[]
                                        {
                                            optimizeRunId,
                                            response.DestinationProfile.ProfileData,
                                            response.DeviceLinkProfile.ProfileData
                                        });
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateInitialDestinationAndDeviceLinkProfiles"));
        }

        public byte[] GetDeviceLinkProfile(int optimizeRunId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSync4D.GetDeviceLinkProfile",
                            new object[]
                                    {
                                        optimizeRunId
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDeviceLinkProfile"));
            return (data == null || data == DBNull.Value) ? null : (byte[])data;
        }

        public List<Measurement> GetMeasurements(int optimizeRunId)
        {
            var measurements = new List<Measurement>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurements =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetMeasurementsByOptimizeRunID",
                                new object[]
                                        {
                                            optimizeRunId
                                        });
                    measurements = dsMeasurements.Tables[0].AsEnumerable().
                        Select(x => new Measurement()
                                        {
                                            MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                                            MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                                            MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                                            OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                                            IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                        }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurements"));
            return measurements;
        }

        public void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds)
        {
            Common.ImplementBusinessLogic(
                () =>
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
                                          DeviceLinkProfile = new IccProfile() { ProfileData = deviceLinkProfile },
                                          Options = new List<Parameter>()
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
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_OptimizeDeviceLinkProfile"));
        }

        public void CloseOptimizeRun(int optimizeRunId, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "CPSync4D.CloseOptimizeRun",
                              new object[]
                                  {
                                      optimizeRunId,
                                      status,
                                      endDateTime
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CloseOptimizeRun"));
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}