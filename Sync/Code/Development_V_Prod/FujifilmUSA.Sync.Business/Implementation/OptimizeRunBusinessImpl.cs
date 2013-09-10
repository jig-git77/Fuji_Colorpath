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
                                startDateTime
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateOptimizeRun"));
        }

        public void DeleteOptimizeRun(int OptimizeRunID, int ProjectID, int MasterRunID, EOptimizeRunStatus RunStatus)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "CPSync4D.DeleteOptimizeRun",
                        new object[]
                            {
                                OptimizeRunID,
                                ProjectID,
                                MasterRunID,
                                (int)RunStatus
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteOptimizeRun"));
        }

        public void DeleteOptimizeRunDLP(int OptimizeRunID, int ProjectID)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "CPSync4D.DeleteOptimizeRunDLP",
                        new object[]
                            {
                                OptimizeRunID,
                                ProjectID
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteOptimizeRunDLP"));
        }

        public void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - Start"), null, false);
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
                    request.DestinationProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = destinationDescription });

                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - Destination profile option added to request object."), null, false);

                    var dlpOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
                    foreach (KeyValuePair<string, string> option in dlpOptions)
                    {
                        request.DlpOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.DlpOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = dlpDescription });

                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - DLP Options added to request object."), null, false);

                    using (var client = new ProfilerClient())
                    {
                        var response = client.CreateDestinationAndDeviceLinkProfile(request);
                        if (response.DeviceLinkProfile.ProfileData == null || response.DestinationProfile.ProfileData == null)
                        {
                            //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - Destination and device link profile not generated."), null, false);
                            throw new Exception("Destination and Device link profile has not been generated successfully from service for runId: " + optimizeRunId + " Error: " + response.Message);
                        }
                        //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - CPSync4D.UpdateInitialDestinationAndDlp call invoked."), null, false);
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "CPSync4D.UpdateInitialDestinationAndDlp",
                                new object[]
                                    {
                                        optimizeRunId,
                                        response.DestinationProfile.ProfileData,
                                        response.DeviceLinkProfile.ProfileData,
                                        status,
                                        endDateTime
                                    });
                        //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - CPSync4D.UpdateInitialDestinationAndDlp call finished."), null, false);
                    }

                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - End"), null, false);
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
                    if (data == null || data == DBNull.Value)
                    {
                        throw new Exception("Device link profile not found for runId: " + optimizeRunId);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDeviceLinkProfile"));
            return data == DBNull.Value ? null : (byte[])data;
        }

        public void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string description, int status, DateTime endDateTime)
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
                        GetProjectOption(projectId, EProfileType.OptimizeDlp);
                    foreach (KeyValuePair<string, string> option in options)
                    {
                        request.Options.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.Options.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = description });

                    using (var client = new ProfilerClient())
                    {
                        var response = client.OptimizeDeviceLinkProfile(request);
                        if (response.OptimizedProfile.ProfileData == null)
                        {
                            throw new Exception("Optimized device link profile has not been generated successfully from service for runId:" + optimizeRunId + " Error: " + response.Message);
                        }
                        DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.UpdateOptimizeDlp",
                                                                    new object[] { optimizeRunId, response.OptimizedProfile.ProfileData, status, endDateTime });
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_OptimizeDeviceLinkProfile"));
        }

        public void CertifyOptimizeRun(int optimizeRunId, int ProjectId, int MasterRunId, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "CPSync4D.CertifyOptimizeRun",
                              new object[]
                                  {
                                      optimizeRunId,
                                      ProjectId,
                                      MasterRunId,
                                      status,
                                      endDateTime
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CloseOptimizeRun"));
        }

        public bool DeleteMeasurement(int measurementId)
        {
            int affectedRow = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    affectedRow = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "CPSync4D.DeleteMeasurement",
                            new object[]
                                    {
                                        measurementId
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteMeasurement"));
            return (affectedRow > 0);
        }

        public int GetLastOptimizeRunId(int projectId)
        {
            int optimizeRunId = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    optimizeRunId =
                        Convert.ToInt32(
                            DataAccessFactory.
                                Instance.
                                GetScalar(
                                    "CPSync4D.GetLastOptimizeRunId",
                                    new object[]
                                            {
                                                projectId
                                            }));
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLastOptimizeRunId"));
            return optimizeRunId;
        }


        public EOptimizeRunStatus GetLastRunStatus(int OptimizeRunId)
        {
            int LastRunStatus = 1;
            Common.ImplementBusinessLogic(
                () =>
                {
                    LastRunStatus =
                        Convert.ToInt32(
                            DataAccessFactory.
                                Instance.
                                GetScalar(
                                    "CPSync4D.GetLastRunStatus",
                                    new object[]
                                            {
                                                OptimizeRunId
                                            }));
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLastRunStatus"));
            return (EOptimizeRunStatus)LastRunStatus;
        }

        public bool DeleteVMeasurement(int measurementId)
        {
            int affectedRow = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    affectedRow = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "CPSyncV.DeleteVMeasurement",
                            new object[]
                                    {
                                        measurementId
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteMeasurement"));
            return (affectedRow > 0);
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}