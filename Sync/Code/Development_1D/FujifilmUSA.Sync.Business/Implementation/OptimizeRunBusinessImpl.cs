using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Messages;
using FujifilmUSA.Sync.Business.ProfilerServiceClient;
using FujifilmUSA.Sync.Business.ProfilerServiceClient.Request;
using FujifilmUSA.Sync.Business.ProfilerServiceClient.Response;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSync1D;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;


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
                        "[CPSync4D].[CreateOptimizeRun]",
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
                       "[CPSync4D].[DeleteOptimizeRun]",
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

        public void Delete1DOptimizeRun(int OptimizeRunID, int ProjectID, int MasterRunID, EOptimizeRunStatus RunStatus)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                       "[CPSync].[DeleteOptimizeRun]",
                        new object[]
                            {
                                OptimizeRunID,
                                ProjectID,
                                MasterRunID,
                                (int)RunStatus
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteOptimizeRun"));
        }

        public void Delete1DOptimizeRunDLP(int OptimizeRunID, int ProjectID)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "[CPSync].[DeleteOptimizeRunDLP]",
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
                        ProfileOptions = new List<Parameter>()
                    };

                    var measurementDataResponse =
                        BusinessFactory<IMeasurementBusiness>.
                            CreateObject().GetMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

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

                    var profileOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
                    foreach (KeyValuePair<string, string> option in profileOptions)
                    {
                        request.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = dlpDescription });

                    request.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;

                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - DLP Options added to request object."), null, false);

                    /* WebClient Example */
                    var response = Utilities.GetProfilerServiceClientCall<CreateDestinationAndDeviceLinkProfileRequest, CreateDestinationAndDeviceLinkProfileResponse>(request, Utilities.ProfilerCallMethod.CreateDLP, Utilities.ProfilerRequestCall.HttpPost);
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


                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - End"), null, false);
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateInitialDestinationAndDeviceLinkProfiles"));
        }

        public void CreateInitialDestinationAndDeviceLinkProfilesPlus(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
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
                        ProfileOptions = new List<Parameter>()
                    };

                    var measurementDataResponse =
                        BusinessFactory<IMeasurementBusiness>.
                            CreateObject().GetMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

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

                    var profileOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
                    foreach (KeyValuePair<string, string> option in profileOptions)
                    {
                        request.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = dlpDescription });

                    request.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;

                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - DLP Options added to request object."), null, false);

                    /* WebClient Example */
                    var response = Utilities.GetProfilerServiceClientCall<CreateDestinationAndDeviceLinkProfileRequest, CreateDestinationAndDeviceLinkProfileResponse>(request, Utilities.ProfilerCallMethod.CreateDLP, Utilities.ProfilerRequestCall.HttpPost);
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


                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - End"), null, false);
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateInitialDestinationAndDeviceLinkProfilesPlus"));
        }

        public void CreateInitialDestinationAndDeviceLinkProfilesMultiColor(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
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
                        ProfileOptions = new List<Parameter>()
                    };

                    var measurementDataResponse =
                        BusinessFactory<IMeasurementBusiness>.
                            CreateObject().GetMultiColorMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

                    request.PcsData = measurementDataResponse.PcsData;
                    request.DcsData = measurementDataResponse.DcsData;
                    request.TotalNoColors = measurementDataResponse.TotalNoColors;

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

                    var profileOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
                    foreach (KeyValuePair<string, string> option in profileOptions)
                    {
                        request.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = dlpDescription });

                    request.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;



                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - DLP Options added to request object."), null, false);

                    /* WebClient Example */
                    var response = Utilities.GetProfilerServiceClientCall<CreateDestinationAndDeviceLinkProfileRequest, CreateDestinationAndDeviceLinkProfileResponse>(request, Utilities.ProfilerCallMethod.CreateDLPMultiColor, Utilities.ProfilerRequestCall.HttpPost);
                    if (response.DeviceLinkProfile.ProfileData == null || response.DestinationProfile.ProfileData == null)
                    {
                        //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - Destination and device link profile not generated."), null, false);
                        throw new Exception("Destination and Device link profile MultiColor has not been generated successfully from service for runId: " + optimizeRunId + " Error: " + response.Message);
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
                        ProfileOptions = new List<Parameter>()
                    };

                    var measurementDataResponse = BusinessFactory<IMeasurementBusiness>.CreateObject().GetMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);
                    request.PCSData = measurementDataResponse.PcsData;
                    request.DCSData = measurementDataResponse.DcsData;

                    var profileOptions = BusinessFactory<IProjectBusiness>.CreateObject().GetProjectOption(projectId, EProfileType.OptimizeDlp);
                    foreach (KeyValuePair<string, string> option in profileOptions)
                    {
                        request.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = description });

                    request.PCSDataType = (ProfilerServiceClient.Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;
                    var response = Utilities.GetProfilerServiceClientCall<OptimizeDeviceLinkProfileRequest, OptimizeDeviceLinkProfileResponse>(request, Utilities.ProfilerCallMethod.OptimizeDLP, Utilities.ProfilerRequestCall.HttpPost);
                    ///var response = client.OptimizeDeviceLinkProfile(request);
                    if (response.OptimizedProfile.ProfileData == null)
                    {
                        throw new Exception("Optimized device link profile has not been generated successfully from service for runId:" + optimizeRunId + " Error: " + response.Message);
                    }
                    DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.UpdateOptimizeDlp",
                                                                new object[] { optimizeRunId, response.OptimizedProfile.ProfileData, status, endDateTime });

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

        public void Certify1DOptimizeRun(int optimizeRunId, int ProjectId, int MasterRunId, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CPSync].[CertifyOptimizeRun]",
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
                            "[CPSync4D].[DeleteMeasurement]",
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

        public int Get1DLastOptimizeRunId(int projectId)
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
                                    "CPSync.GetLastOptimizeRunId",
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

        public EOptimizeRunStatus Get1DLastRunStatus(int OptimizeRunId)
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
                                    "[CPSync].[GetLastRunStatus]",
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

        public bool Delete1DMeasurement(int measurementId)
        {
            int affectedRow = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    affectedRow = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CPSync].[DeleteMeasurement]",
                            new object[]
                                    {
                                        measurementId
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteMeasurement"));
            return (affectedRow > 0);
        }

        public void Create1DOptimizeRun(int projectId, int status, DateTime startDateTime, short CurveTypeId)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "[CPSync].[CreateOptimizeRun]",
                        new object[]
                            {
                                projectId,
                                status,
                                startDateTime,
                                CurveTypeId
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateOptimizeRun"));
        }

        //to-do set return type
        public void CreateG7AlignmentCurve(int projectId, int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime, bool Is1DMeasurementSmoothing, string description)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    MeasurementBusinessImpl measurementBusiness = new MeasurementBusinessImpl();
                    MeasurementDataResponse measurementDataResponse = measurementBusiness.Get1DMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

                    //ProfilerClient syncClient = new ProfilerClient();

                    CreateG7AlignmentCurveRequest alignmentCurveRequest = new CreateG7AlignmentCurveRequest();


                    alignmentCurveRequest.DCSData = new List<double>();          //DSCData as CMYK Data           
                    for (int i = 0; i < measurementDataResponse.DcsData.Count; i++)
                    {
                        alignmentCurveRequest.DCSData.Add(measurementDataResponse.DcsData[i]);
                    }

                    alignmentCurveRequest.PCSData = new List<double>();         //PCSData as either Spectral Data or XYZ Data            
                    for (int i = 0; i < measurementDataResponse.PcsData.Count; i++)
                    {
                        alignmentCurveRequest.PCSData.Add(measurementDataResponse.PcsData[i]);
                    }

                    alignmentCurveRequest.UseDLP = useDLP;
                    if (useDLP)
                    {
                        var dlp = BusinessFactory<IProjectBusiness>.CreateObject().Get1DLastOptimizedDlp(projectId);
                        alignmentCurveRequest.DeviceLinkProfile = new IccProfile();
                        alignmentCurveRequest.DeviceLinkProfile.ProfileData = dlp;
                    }

                    var ProfOption = BusinessFactory<IMeasurementBusiness>.CreateObject().GetTargetTVIData(projectId, Is1DMeasurementSmoothing);
                    alignmentCurveRequest.ProfileOptions = new List<Parameter>();

                    foreach (KeyValuePair<string, string> option in ProfOption)
                    {
                        alignmentCurveRequest.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    alignmentCurveRequest.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = description });

                    //alignmentCurveRequest.PCSDataType = PCSDataType.XYZ;//(PCSDataType)Enum.Parse(typeof(PCSDataType), pcsDataType.ToString());                    
                    alignmentCurveRequest.PCSDataType = (ProfilerServiceClient.Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;

                    //alignmentCurveRequest.DensityType = (ProfilerService.DensityType)SessionManager.Instance.CurrentProject.DensityType;  

                    CreateG7AlignmentCurveResponse syncClientResponse = new CreateG7AlignmentCurveResponse();
                    //syncClientResponse = syncClient.CreateG7AlignmentCurve(alignmentCurveRequest);
                    syncClientResponse = Utilities.GetProfilerServiceClientCall<CreateG7AlignmentCurveRequest, CreateG7AlignmentCurveResponse>(alignmentCurveRequest, Utilities.ProfilerCallMethod.CreateG7Alignment, Utilities.ProfilerRequestCall.HttpPost);

                    int n = 101;

                    List<Entity.CPSync1D.OptimizeRunCurve> OptimizeRunCurveList = new List<Entity.CPSync1D.OptimizeRunCurve>();

                    for (int i = 0; i < n; i++)
                    {
                        Entity.CPSync1D.OptimizeRunCurve OptimizeRunCurveItem = new Entity.CPSync1D.OptimizeRunCurve();
                        OptimizeRunCurveItem.OptimizeRunID = optimizeRunId;
                        OptimizeRunCurveItem.ReferenceNo = Convert.ToInt16(i);
                        OptimizeRunCurveItem.CMYK_C = Convert.ToDecimal(syncClientResponse.Cyan[i]);
                        OptimizeRunCurveItem.CMYK_M = Convert.ToDecimal(syncClientResponse.Magenta[i]);
                        OptimizeRunCurveItem.CMYK_Y = Convert.ToDecimal(syncClientResponse.Yellow[i]);
                        OptimizeRunCurveItem.CMYK_K = Convert.ToDecimal(syncClientResponse.Key[i]);
                        OptimizeRunCurveList.Add(OptimizeRunCurveItem);
                    }

                    CreateOptimizeRunCurve(optimizeRunId, OptimizeRunCurveList);

                    DataAccessFactory.Instance.ExecuteProcedure("CPSync.UpdateOptimizeDlp",
                                                                            new object[] { optimizeRunId, syncClientResponse.DeviceLinkProfile.ProfileData, status, endDateTime });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateG7AlignmentCurve"));
        }

        public void CreateISOAlignmentCurve(int projectId, int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime, bool Is1DMeasurementSmoothing, string description)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    MeasurementBusinessImpl measurementBusiness = new MeasurementBusinessImpl();
                    MeasurementDataResponse measurementDataResponse = measurementBusiness.Get1DMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

                    //ProfilerClient syncClient = new ProfilerClient();

                    CreateISOAlignmentCurveRequest alignmentCurveRequest = new CreateISOAlignmentCurveRequest();

                    alignmentCurveRequest.DCSData = new List<double>();    //DSCData as CMYK Data
                    for (int i = 0; i < measurementDataResponse.DcsData.Count; i++)
                    {
                        alignmentCurveRequest.DCSData.Add(measurementDataResponse.DcsData[i]);
                    }

                    alignmentCurveRequest.PCSData = new List<double>();    //PCSData as either Spectral Data or Density Data                
                    for (int i = 0; i < measurementDataResponse.PcsData.Count; i++)
                    {
                        alignmentCurveRequest.PCSData.Add(measurementDataResponse.PcsData[i]);
                    }

                    alignmentCurveRequest.UseDLP = useDLP;
                    if (useDLP)
                    {
                        var dlp = BusinessFactory<IProjectBusiness>.CreateObject().Get1DLastOptimizedDlp(projectId);
                        alignmentCurveRequest.DeviceLinkProfile = new IccProfile();
                        alignmentCurveRequest.DeviceLinkProfile.ProfileData = dlp;
                    }

                    //---Pass TVI values into Profiler Options, so now we don't need to pass Linearization TVIs----//
                    var ProfOption = BusinessFactory<IMeasurementBusiness>.CreateObject().GetTargetTVIData(projectId, Is1DMeasurementSmoothing);
                    alignmentCurveRequest.ProfileOptions = new List<Parameter>();

                    foreach (KeyValuePair<string, string> option in ProfOption)
                    {
                        alignmentCurveRequest.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    alignmentCurveRequest.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = description });

                    //alignmentCurveRequest.PCSDataType = PCSDataType.Density; // (PCSDataType)Enum.Parse(typeof(PCSDataType), pcsDataType.ToString());
                    alignmentCurveRequest.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;

                    //alignmentCurveRequest.DensityType = ProfilerService.DensityType.StatusE;
                    alignmentCurveRequest.DensityType = (Enums.DensityType)SessionManager.Instance.CurrentProject.DensityType;

                    CreateISOAlignmentCurveResponse syncClientResponse = new CreateISOAlignmentCurveResponse();
                    //syncClientResponse = syncClient.CreateISOAlignmentCurve(alignmentCurveRequest);
                    syncClientResponse = Utilities.GetProfilerServiceClientCall<CreateISOAlignmentCurveRequest, CreateISOAlignmentCurveResponse>(alignmentCurveRequest, Utilities.ProfilerCallMethod.CreateISOAlignment, Utilities.ProfilerRequestCall.HttpPost);

                    int n = 101;

                    List<Entity.CPSync1D.OptimizeRunCurve> OptimizeRunCurveList = new List<Entity.CPSync1D.OptimizeRunCurve>();

                    for (int i = 0; i < n; i++)
                    {
                        Entity.CPSync1D.OptimizeRunCurve OptimizeRunCurveItem = new Entity.CPSync1D.OptimizeRunCurve();
                        OptimizeRunCurveItem.OptimizeRunID = optimizeRunId;
                        OptimizeRunCurveItem.ReferenceNo = Convert.ToInt16(i);
                        OptimizeRunCurveItem.CMYK_C = Convert.ToDecimal(syncClientResponse.Cyan[i]);
                        OptimizeRunCurveItem.CMYK_M = Convert.ToDecimal(syncClientResponse.Magenta[i]);
                        OptimizeRunCurveItem.CMYK_Y = Convert.ToDecimal(syncClientResponse.Yellow[i]);
                        OptimizeRunCurveItem.CMYK_K = Convert.ToDecimal(syncClientResponse.Key[i]);
                        OptimizeRunCurveList.Add(OptimizeRunCurveItem);
                    }

                    CreateOptimizeRunCurve(optimizeRunId, OptimizeRunCurveList);

                    DataAccessFactory.Instance.ExecuteProcedure("CPSync.UpdateOptimizeDlp",
                                                                            new object[] { optimizeRunId, syncClientResponse.DeviceLinkProfile.ProfileData, status, endDateTime });

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateISOAlignmentCurve"));
        }

        public void CreateOptimizeRunCurve(int optimizeRunId, List<Entity.CPSync1D.OptimizeRunCurve> OptimizeRunCurveList)
        {
            var OptimizeRunCurveData = new XElement("OptimizeRunCurveData",
                                                       OptimizeRunCurveList.
                                                           Select(x =>
                                                                  new XElement("OptimizeRunCurve",
                                                                               new XAttribute("OptimizeRunID", x.OptimizeRunID),
                                                                               new XAttribute("ReferenceNo", x.ReferenceNo),
                                                                               new XAttribute("CMYK_C", x.CMYK_C),
                                                                               new XAttribute("CMYK_M", x.CMYK_M),
                                                                               new XAttribute("CMYK_Y", x.CMYK_Y),
                                                                               new XAttribute("CMYK_K", x.CMYK_K)
                                                                      )).ToList());

            DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CPSync].[CreateOptimizeRunCurve]",
                                new object[]
                                    {
                                       optimizeRunId,
                                       OptimizeRunCurveData.ToString()
                                    });
        }

        public CurveDataResponse GetCurveData(int optimizeRunId, short curveTypeId)
        {
            CurveDataResponse curveDataResponse = new CurveDataResponse();
            List<Entity.CPSync1D.OptimizeRunCurve> curveExportList = new List<Entity.CPSync1D.OptimizeRunCurve>();
            List<Entity.CPSync1D.OptimizeRunCurve> curveDeltaRefList = new List<Entity.CPSync1D.OptimizeRunCurve>();
            List<Entity.CPSync1D.OptimizeRunCurve> curveDeltaPrevRunList = new List<Entity.CPSync1D.OptimizeRunCurve>();

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsOptimizeRunCurveList = DataAccessFactory.Instance.GetDsResult("[CPSync].[GetCurveData]",
                                                                            new object[] { optimizeRunId, curveTypeId });
                    if (dsOptimizeRunCurveList != null && dsOptimizeRunCurveList.Tables.Count > 0)
                    {
                        if (dsOptimizeRunCurveList.Tables.Count >= 1 && dsOptimizeRunCurveList.Tables[0].Rows.Count > 0)
                        {
                            curveExportList = dsOptimizeRunCurveList.Tables[0].AsEnumerable().
                                Select(x => new Entity.CPSync1D.OptimizeRunCurve()
                                {
                                    ReferenceNo = DataReader.ReadShort(x, "ReferenceNo"),
                                    CMYK_C = DataReader.ReadDecimal(x, "CMYK_C"),
                                    CMYK_M = DataReader.ReadDecimal(x, "CMYK_M"),
                                    CMYK_Y = DataReader.ReadDecimal(x, "CMYK_Y"),
                                    CMYK_K = DataReader.ReadDecimal(x, "CMYK_K")
                                }).ToList();
                        }

                        if (dsOptimizeRunCurveList.Tables.Count >= 2 && dsOptimizeRunCurveList.Tables[1].Rows.Count > 0)
                        {
                            curveDeltaRefList = dsOptimizeRunCurveList.Tables[1].AsEnumerable().
                                    Select(x => new Entity.CPSync1D.OptimizeRunCurve()
                                    {
                                        ReferenceNo = DataReader.ReadShort(x, "ReferenceNo"),
                                        CMYK_C = DataReader.ReadDecimal(x, "CMYK_C"),
                                        CMYK_M = DataReader.ReadDecimal(x, "CMYK_M"),
                                        CMYK_Y = DataReader.ReadDecimal(x, "CMYK_Y"),
                                        CMYK_K = DataReader.ReadDecimal(x, "CMYK_K")
                                    }).ToList();
                        }

                        if (dsOptimizeRunCurveList.Tables.Count >= 3 && dsOptimizeRunCurveList.Tables[2].Rows.Count > 0)
                        {
                            curveDeltaPrevRunList = dsOptimizeRunCurveList.Tables[2].AsEnumerable().
                                    Select(x => new Entity.CPSync1D.OptimizeRunCurve()
                                    {
                                        ReferenceNo = DataReader.ReadShort(x, "ReferenceNo"),
                                        CMYK_C = DataReader.ReadDecimal(x, "CMYK_C"),
                                        CMYK_M = DataReader.ReadDecimal(x, "CMYK_M"),
                                        CMYK_Y = DataReader.ReadDecimal(x, "CMYK_Y"),
                                        CMYK_K = DataReader.ReadDecimal(x, "CMYK_K")
                                    }).ToList();
                        }
                    }

                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetExportCurveData"));

            curveDataResponse.CurveExportView = curveExportList;
            curveDataResponse.CurveDeltaRefView = curveDeltaRefList;
            curveDataResponse.CurveDeltaPrevRunView = curveDeltaPrevRunList;
            return curveDataResponse;
        }

        public byte[] Get1DDeviceLinkProfile(int optimizeRunId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSync.GetDeviceLinkProfile",
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

        public void CreateSourceToSourceDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
        {
            //----This code is for Source to Source DLP generation-----------//
            Common.ImplementBusinessLogic(
                () =>
                {
                    var sourceProfile =
                        BusinessFactory<IProjectBusiness>.
                            CreateObject().
                            GetSourceProfile(projectId);

                    var request = new CreateSourceToSourceDeviceLinkProfileRequest()
                    {
                        SourceProfile = new IccProfile() { ProfileData = sourceProfile },
                        ProfileOptions = new List<Parameter>()
                    };

                    var profilerOptionBusiness = BusinessFactory<IProjectBusiness>.CreateObject();

                    var profileOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
                    foreach (KeyValuePair<string, string> option in profileOptions)
                    {
                        request.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = dlpDescription });
                    var response = Utilities.GetProfilerServiceClientCall<CreateSourceToSourceDeviceLinkProfileRequest, CreateSourceToSourceDeviceLinkProfileResponse>(request, Utilities.ProfilerCallMethod.CreateSourceDLP, Utilities.ProfilerRequestCall.HttpPost);
                    if (response.DeviceLinkProfile.ProfileData == null)
                    {
                        throw new Exception("Device link profile has not been generated successfully from service for runId: " + optimizeRunId);
                    }
                    DataAccessFactory.
                       Instance.
                       ExecuteProcedure(
                           "CPSync4D.UpdateInitialDlp",
                           new object[]
                                {
                                        optimizeRunId,
                                        response.DeviceLinkProfile.ProfileData,
                                        status,
                                        endDateTime
                                });


                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateInitialDestinationAndDeviceLinkProfiles"));
        }

        public void CreateCustomSourceProfile(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    var sourceProfile =
                       BusinessFactory<IProjectBusiness>.
                           CreateObject().
                           GetSourceProfile(projectId);


                    var sourcerequest = new CreateSourceProfileRequest()
                    {
                        PCSData = new List<double>(),
                        DCSData = new List<double>(),
                        SourceProfile = new ProfilerServiceClient.IccProfile() { ProfileData = sourceProfile },
                        ProfileOptions = new List<ProfilerServiceClient.Parameter>()
                    };

                    var SourcemeasurementDataResponse =
                        BusinessFactory<IMeasurementBusiness>.
                            CreateObject().GetMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

                    sourcerequest.PCSData = SourcemeasurementDataResponse.PcsData;
                    sourcerequest.DCSData = SourcemeasurementDataResponse.DcsData;

                    var sourceprofilerOptionBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                    var sourceprofileOptions =
                        sourceprofilerOptionBusiness.
                            GetProjectOption(projectId, EProfileType.SourceProfile);
                    foreach (KeyValuePair<string, string> option in sourceprofileOptions)
                    {
                        sourcerequest.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    sourcerequest.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = destinationDescription });

                    sourcerequest.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;
                    //var sourceresponse = client.CreateSourceProfile(sourcerequest);
                    var sourceresponse = Utilities.GetProfilerServiceClientCall<CreateSourceProfileRequest, CreateSourceProfileResponse>(sourcerequest, Utilities.ProfilerCallMethod.CreateSourceProfile, Utilities.ProfilerRequestCall.HttpPost);
                    if (sourceresponse.Profile.ProfileData == null || sourceresponse.Profile.ProfileData == null)
                    {

                        throw new Exception("Custom Source profile has not been generated successfully from service for runId: " + optimizeRunId + " Error: " + sourceresponse.Message);
                    }

                    DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "CP.InsertCustomSourceProfile",
                            new object[]
                                {
                                        projectId,
                                        destinationDescription,
                                        destinationDescription,
                                        sourceresponse.Profile.ProfileData,
                                        true
                                });

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateInitialDestinationAndDeviceLinkProfiles"));
        }


        public void CreateReTargetProject(int projectId, int optimizeRunId, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "[CPSync].[CreateReTargetProject]",
                        new object[]
                            {
                                projectId,
                                optimizeRunId,
                                status,
                                endDateTime
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateReTargetProject"));
        }

        public void CreateSpotProjectOptimizeRun(int spotProjectId, int status, DateTime startDateTime)
        {
            Common.ImplementBusinessLogic(
               () =>
               DataAccessFactory.
                   Instance.
                   ExecuteProcedure(
                       "[CPSpot].[CreateOptimizeRun]",
                       new object[]
                           {
                                spotProjectId,
                                status,
                                startDateTime
                           }),
               new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateOptimizeRun"));
        }

        public void UpdateSpotProjectOptimizeRun(int optimizeRunID, int status, DateTime startDateTime)
        {
            Common.ImplementBusinessLogic(
               () =>
               DataAccessFactory.
                   Instance.
                   ExecuteProcedure(
                       "[CPSpot].[UpdateOptimizeDlp]",
                       new object[]
                           {
                                optimizeRunID,
                                //ProfileData,
                                status,
                                startDateTime
                           }),
               new BusinessHandlerRequest(this, ErrorOccured, "Error_OptimizeNamedColorProfile"));
        }

        public void DeleteSpotOptimizeRun(int OptimizeRunID, int SpotProjectID, int MasterRunID, EOptimizeRunStatus RunStatus)
        {
            Common.ImplementBusinessLogic(
               () =>
               DataAccessFactory.
                   Instance.
                   ExecuteProcedure(
                      "[CPSpot].[DeleteOptimizeRun]",
                       new object[]
                           {
                                OptimizeRunID,
                                SpotProjectID,
                                MasterRunID,
                                (int)RunStatus
                           }),
               new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteOptimizeRun"));
        }

        public bool DeleteSpotMeasurement(int measurementId)
        {
            int affectedRow = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    affectedRow = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CPSpot].[DeleteMeasurement]",
                            new object[]
                                    {
                                        measurementId
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteSpotMeasurement"));
            return (affectedRow > 0);
        }

        public void DeleteSpotOptimizeRunDLP(int OptimizeRunID, int SpotProjectID)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "CPSpot.DeleteOptimizeRunDLP",
                        new object[]
                            {
                                OptimizeRunID,
                                SpotProjectID
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteOptimizeRunNCP"));
        }

        public EOptimizeRunStatus GetSpotLastRunStatus(int OptimizeRunId)
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
                                    "CPSpot.GetLastRunStatus",
                                    new object[]
                                            {
                                                OptimizeRunId
                                            }));
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLastRunStatus"));
            return (EOptimizeRunStatus)LastRunStatus;
        }

        public void CertifySpotOptimizeRun(int optimizeRunId, int SpotProjectId, int MasterRunId, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "CPSpot.CertifyOptimizeRun",
                              new object[]
                                  {
                                      optimizeRunId,
                                      SpotProjectId,
                                      MasterRunId,
                                      status,
                                      endDateTime
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CloseOptimizeRun"));
        }

        public void OptimizeNamedColoredProfile(int projectId, int optimizeRunId, List<int> measurementIds, string description, int status, DateTime endDateTime)
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
                        ProfileOptions = new List<Parameter>()
                    };

                    var measurementDataResponse = BusinessFactory<IMeasurementBusiness>.CreateObject().GetMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);
                    request.PCSData = measurementDataResponse.PcsData;
                    request.DCSData = measurementDataResponse.DcsData;

                    var profileOptions = BusinessFactory<IProjectBusiness>.CreateObject().GetProjectOption(projectId, EProfileType.OptimizeDlp);
                    foreach (KeyValuePair<string, string> option in profileOptions)
                    {
                        request.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = description });

                    request.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;
                    var response = Utilities.GetProfilerServiceClientCall<OptimizeDeviceLinkProfileRequest, OptimizeDeviceLinkProfileResponse>(request, Utilities.ProfilerCallMethod.OptimizeDLP, Utilities.ProfilerRequestCall.HttpPost);
                    if (response.OptimizedProfile.ProfileData == null)
                    {
                        throw new Exception("Optimized device link profile has not been generated successfully from service for runId:" + optimizeRunId + " Error: " + response.Message);
                    }
                    DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.UpdateOptimizeDlp",
                                                                new object[] { optimizeRunId, response.OptimizedProfile.ProfileData, status, endDateTime });

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_OptimizeNamedColorProfile"));
        }

        public void CreateSpotNamedColorProfile(int spotProjectId, int OptimizeRunID, int Status, DateTime EndDate)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    //ProfilerClient client = new ProfilerClient();

                    CreateSpotLibraryRequest req = new CreateSpotLibraryRequest();
                    SpotParameter[] paramList = new SpotParameter[10];
                    paramList[0] = new SpotParameter() { Name = "MaxChannels", Value = "0" };
                    paramList[1] = new SpotParameter() { Name = "MaxChannels", Value = "0" };
                    paramList[2] = new SpotParameter() { Name = "SaveProfilerInfo", Value = "1" };
                    paramList[3] = new SpotParameter() { Name = "SaveProfilerData", Value = "1" };
                    paramList[4] = new SpotParameter() { Name = "CalculateDCSMode", Value = "2" };
                    paramList[5] = new SpotParameter() { Name = "Rendering", Value = "3" };
                    paramList[6] = new SpotParameter() { Name = "OOGMethod", Value = "dE00" };
                    paramList[7] = new SpotParameter() { Name = "ClipAbsTolerance", Value = "0" };
                    paramList[8] = new SpotParameter() { Name = "ClipRelTolerance", Value = "0" };
                    paramList[9] = new SpotParameter() { Name = "MaxChannels", Value = "0" };

                    req.ProfileOptions = paramList.ToList();

                    var dsSpotTarget = DataAccessFactory.
                     Instance.
                     GetDsResult(
                         "CPSpot.GetSpotTargetLABByOptimizeRun",
                         new object[]
                                 {
                                        OptimizeRunID
                                 });

                    if (dsSpotTarget.Tables[0].Rows.Count > 0)
                    {
                        req.DestinationProfile = new SpotIccProfile();
                        req.DestinationProfile.ProfileData = (byte[])dsSpotTarget.Tables[0].Rows[0]["DestinationProfile"];

                        var ms = new MemoryStream();
                        StreamWriter writer = new StreamWriter(ms);

                        writer.WriteLine("BEGIN_DATA_FORMAT");
                        writer.WriteLine("SAMPLE_NAME\tLAB_L\tLAB_A\tLAB_B");
                        writer.WriteLine("END_DATA_FORMAT");
                        writer.WriteLine("BEGIN_DATA");

                        if (dsSpotTarget.Tables[1] != null && dsSpotTarget.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsSpotTarget.Tables[1].Rows)
                            {
                                writer.WriteLine("\"" + dr["SpotName"].ToString() + "\"\t" + dr["Target_L*"].ToString() + "\t" + dr["Target_a*"].ToString() + "\t" + dr["Target_b*"].ToString() + "");
                            }
                        }

                        writer.WriteLine("END_DATA");
                        writer.Flush();
                        req.TargetProfile = ms.ToArray();
                    }

                    //var syncClientResponse = client.CreateSpotLibraryValues(req);
                    var syncClientResponse = Utilities.GetProfilerServiceClientCall<CreateSpotLibraryRequest, CreateSpotLibraryResponse>(req, Utilities.ProfilerCallMethod.CreateSpotLibrary, Utilities.ProfilerRequestCall.HttpPost);


                    List<ProfilerServiceClient.Patch> list = syncClientResponse.spotPatchDetail;

                    var spotProjectDetails = new XElement("SpotColorDetails",
                                                       list.
                                                           Select(x =>
                                                                  new XElement("SpotColorData",
                                                                               new XAttribute("OptimizeRunID", OptimizeRunID),
                                                                               new XAttribute("SpotID", x.PatchID),
                                                                               new XAttribute("SpotName", x.ColorName),
                                                                               new XAttribute("CMYK_C", x.Cmyk_C),
                                                                               new XAttribute("CMYK_M", x.Cmyk_M),
                                                                               new XAttribute("CMYK_Y", x.Cmyk_Y),
                                                                               new XAttribute("CMYK_K", x.Cmyk_K),
                                                                               new XAttribute("Target_L", x.Cie_L),
                                                                               new XAttribute("Target_A", x.Cie_a),
                                                                               new XAttribute("Target_B", x.Cie_b),
                                                                               new XAttribute("Predicted_L", x.p_Cie_L),
                                                                               new XAttribute("Predicted_A", x.p_Cie_a),
                                                                               new XAttribute("Predicted_B", x.p_Cie_b)
                                                                      )).ToList());



                    DataAccessFactory.Instance.ExecuteProcedure("CPSpot.UpdateOptimizeDlp",
                                                                            new object[] { OptimizeRunID, syncClientResponse.namedColorProfile, Status, EndDate, spotProjectDetails.ToString(), true });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateSpotNamedColorProfile"));
        }

        public void OptimizeSpotNamedColorProfile(int OptimizeRunID, int Status, DateTime EndDate)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    //ProfilerClient client = new ProfilerClient();

                    OptimizeSpotLibraryRequest req = new OptimizeSpotLibraryRequest();
                    SpotParameter[] paramList = new SpotParameter[5];
                    paramList[0] = new SpotParameter() { Name = "ProfileSize", Value = "2" };
                    paramList[1] = new SpotParameter() { Name = "SaveProfilerInfo", Value = "1" };
                    paramList[2] = new SpotParameter() { Name = "SaveProfilerData", Value = "1" };
                    paramList[3] = new SpotParameter() { Name = "ProfileFormat", Value = "2" };
                    paramList[4] = new SpotParameter() { Name = "Rendering", Value = "3" };


                    req.ProfileOptions = paramList.ToList();

                    var dsSpotTarget = DataAccessFactory.
                     Instance.
                     GetDsResult(
                         "CPSpot.GetSpotTargetLABByMeasurementID",
                         new object[]
                                 {
                                        OptimizeRunID
                                 });

                    if (dsSpotTarget.Tables[0].Rows.Count > 0)
                    {
                        req.DestinationProfile = new SpotIccProfile();
                        req.previousNamedColorProfile = new SpotIccProfile();
                        req.DestinationProfile.ProfileData = (byte[])dsSpotTarget.Tables[0].Rows[0]["DestinationProfile"];
                        req.previousNamedColorProfile.ProfileData = (byte[])dsSpotTarget.Tables[0].Rows[0]["LastOptimizedNCP"];

                        var ms = new MemoryStream();
                        StreamWriter writer = new StreamWriter(ms);

                        writer.WriteLine("BEGIN_DATA_FORMAT");
                        writer.WriteLine("SAMPLE_NAME\tLAB_L\tLAB_A\tLAB_B");
                        writer.WriteLine("END_DATA_FORMAT");
                        writer.WriteLine("BEGIN_DATA");

                        if (dsSpotTarget.Tables[1] != null && dsSpotTarget.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsSpotTarget.Tables[1].Rows)
                            {
                                writer.WriteLine("\"" + dr["SpotName"].ToString() + "\"\t" + dr["Measured_L*"].ToString() + "\t" + dr["Measured_a*"].ToString() + "\t" + dr["Measured_b*"].ToString() + "");
                            }
                        }

                        writer.WriteLine("END_DATA");
                        writer.Flush();
                        req.TargetProfile = ms.ToArray();
                    }

                    //var syncClientResponse = client.OptimizeSpotLibraryValues(req);
                    var syncClientResponse = Utilities.GetProfilerServiceClientCall<OptimizeSpotLibraryRequest, OptimizeSpotLibraryResponse>(req, Utilities.ProfilerCallMethod.OptimizeSpotLibrary, Utilities.ProfilerRequestCall.HttpPost);


                    List<ProfilerServiceClient.Patch> list = syncClientResponse.spotPatchDetail;

                    var spotProjectDetails = new XElement("SpotColorDetails",
                                                       list.
                                                           Select(x =>
                                                                  new XElement("SpotColorData",
                                                                               new XAttribute("OptimizeRunID", OptimizeRunID),
                                                                               new XAttribute("SpotID", x.PatchID),
                                                                               new XAttribute("SpotName", x.ColorName),
                                                                               new XAttribute("CMYK_C", x.Cmyk_C),
                                                                               new XAttribute("CMYK_M", x.Cmyk_M),
                                                                               new XAttribute("CMYK_Y", x.Cmyk_Y),
                                                                               new XAttribute("CMYK_K", x.Cmyk_K)
                                                                      )).ToList());



                    DataAccessFactory.Instance.ExecuteProcedure("CPSpot.UpdateOptimizeDlp",
                                                                            new object[] { OptimizeRunID, syncClientResponse.namedColorProfile, Status, EndDate, spotProjectDetails.ToString(), false });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateSpotNamedColorProfile"));
        }

        public void CreateDestinationProfile(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
             () =>
             {
                 //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - Start"), null, false);
                 var request = new CreateDestinationProfileRequest()
                 {
                     PcsData = new List<double>(),
                     DcsData = new List<double>(),
                     DestinationProfileOptions = new List<Parameter>()
                 };

                 var measurementDataResponse =
                     BusinessFactory<IMeasurementBusiness>.
                         CreateObject().GetMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

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

                 request.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;

                 //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - DLP Options added to request object."), null, false);

                 /* WebClient Example */
                 var response = Utilities.GetProfilerServiceClientCall<CreateDestinationProfileRequest, CreateDestinationProfileResponse>(request, Utilities.ProfilerCallMethod.CreateDestinationProfile, Utilities.ProfilerRequestCall.HttpPost);
                 if (response.DestinationProfile.ProfileData == null)
                 {
                     //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - Destination and device link profile not generated."), null, false);
                     throw new Exception("Destination profile has not been generated successfully from service for runId: " + optimizeRunId + " Error: " + response.Message);
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
                                        null,
                                        status,
                                        endDateTime
                             }); ;
                 //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - CPSync4D.UpdateInitialDestinationAndDlp call finished."), null, false);


                 //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - End"), null, false);
             },
             new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateInitialDestinationProfile"));
        }

        public void CreateRevoria(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
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
                        ProfileOptions = new List<Parameter>()
                    };

                    var measurementDataResponse =
                        BusinessFactory<IMeasurementBusiness>.
                            CreateObject().GetMeasurementData(measurementIds, SessionManager.Instance.CurrentProject.PCSDataTypes);

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

                    var profileOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
                    foreach (KeyValuePair<string, string> option in profileOptions)
                    {
                        request.ProfileOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.ProfileOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = dlpDescription });

                    request.PCSDataType = (Enums.PCSDataType)SessionManager.Instance.CurrentProject.PCSDataTypes;

                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - DLP Options added to request object."), null, false);

                    /* WebClient Example */
                    var response = Utilities.GetProfilerServiceClientCall<CreateDestinationAndDeviceLinkProfileRequest, CreateDestinationAndDeviceLinkProfileResponse>(request, Utilities.ProfilerCallMethod.CreateDLP, Utilities.ProfilerRequestCall.HttpPost);
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


                    //LogManager.WriteLog(new Exception("Create DLP Logging - OptimizeRunBusinessImpl - CreateInitialDestinationAndDeviceLinkProfiles - End"), null, false);
                },
                new BusinessHandlerRequest(this, ErrorOccured, "CreateRevoria"));
        }

        public void UpdateTargetLabList(int ProjectId, List<ProjectTargetLAB> ProjectTargetLABList, bool EnabledPaperScaling, int MeasurementId, int LicenseTypeId, decimal NewPaper_White_L, decimal NewPaper_White_a, decimal NewPaper_White_b, bool IsUpdateNewPaper, bool IsUpdateDelta)
        {
            // Build XML like in reference code
            var projectTargetLAB = new XElement("ProjectTargetLAB", "");

            if (ProjectTargetLABList != null && ProjectTargetLABList.Count > 0)
            {
                projectTargetLAB = new XElement("ProjectTargetLAB",
                    ProjectTargetLABList.Select(x =>
                        new XElement("TargetLAB",
                            new XAttribute("PatchTypeID", x.PatchType.PatchTypeId),
                            new XAttribute("TargetL", x.TargetL),
                            new XAttribute("TargetA", x.TargetA),
                            new XAttribute("TargetB", x.TargetB)
                        )
                    )
                );
            }
            Common.ImplementBusinessLogic(
                 () =>
                 {
                     // Call SP
                     DataAccessFactory
                          .Instance
                          .ExecuteProcedure(
                              "CPSync.UpdateProjectTargetLAB",
                              new object[]
                              {
                                ProjectId,
                                projectTargetLAB.ToString(),
                                EnabledPaperScaling,
                                MeasurementId,
                                LicenseTypeId,
                                NewPaper_White_L,
                                NewPaper_White_a,
                                NewPaper_White_b,
                                IsUpdateNewPaper,
                                IsUpdateDelta
                              }
                          );
                 },
                 new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateTargetLAB")
             );
        }

        public Project GetProjectTargetLAB(int printStandardTypeId, int ProjectTypeId, int ProjectId, int LicenseTypeId)
        {
            Project project = new Project();

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectTargetLABList = DataAccessFactory.Instance
                    .GetDsResult("[CPSync].[GetTargetLabNewPaper]",
                      new object[] { printStandardTypeId, ProjectTypeId, ProjectId, LicenseTypeId });

                    if (dsProjectTargetLABList != null && dsProjectTargetLABList.Tables.Count > 0)
                    {
                        bool includeTargetLab = (LicenseTypeId == 1 || LicenseTypeId == 13);

                        // Load Target LAB only for specific license types
                        if (includeTargetLab && dsProjectTargetLABList.Tables[0].Rows.Count > 0)
                        {
                            project.ProjectTargetLAB = dsProjectTargetLABList.Tables[0]
                                .AsEnumerable()
                                .Select(x => new Entity.CPSync1D.ProjectTargetLAB()
                                {
                                    PatchType = new PatchType()
                                    {
                                        PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                        Type = DataReader.ReadString(x, "PatchType")
                                    },
                                    TargetL = DataReader.ReadDecimal(x, "TargetL"),
                                    TargetA = DataReader.ReadDecimal(x, "TargetA"),
                                    TargetB = DataReader.ReadDecimal(x, "TargetB")
                                }).ToList();
                        }

                        // Decide which table contains NewPaper values
                        int newPaperTableIndex = includeTargetLab ? 1 : 0;

                        if (dsProjectTargetLABList.Tables.Count > newPaperTableIndex &&
                            dsProjectTargetLABList.Tables[newPaperTableIndex].Rows.Count > 0)
                        {
                            var row = dsProjectTargetLABList.Tables[newPaperTableIndex].Rows[0];

                            project.NewPaper_White_L_Old = DataReader.ReadDecimal(row, "Old_NewPaper_White_L");
                            project.NewPaper_White_a_Old = DataReader.ReadDecimal(row, "Old_NewPaper_White_a");
                            project.NewPaper_White_b_Old = DataReader.ReadDecimal(row, "Old_NewPaper_White_b");
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetProjectTargetLAB")
            );

            return project;
        }

        public List<int> GetMeasurementIDs_RPS(int ProjectID, int deviceId, int LicenseTypeID)
        {
            var objMeasurementIDList = new List<int>();
            string storedProecdure = "CPSync.GetMeasurementIDsByProjectID_RPS";

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectList =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                               storedProecdure,
                                new object[]
                                        {
                                            ProjectID, deviceId, LicenseTypeID
                                        });
                    if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsProjectList.Tables[0].Rows.Count; i++)
                        {
                            objMeasurementIDList.Add(Convert.ToInt32(dsProjectList.Tables[0].Rows[i]["MeasurementID"].ToString()));
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementIDsbyProjectID"));
            return objMeasurementIDList;
        }

        public MeasurementPatchDetails GetActualValuesByMeasurementId(int MeasurementID)
        {
            MeasurementPatchDetails measurementPatchDetails = new MeasurementPatchDetails();

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsActualValues = DataAccessFactory.Instance
                        .GetDsResult(
                            "[CPSync].[GetActualValuesByMeasurementID]",
                            new object[] { MeasurementID });

                    if (dsActualValues != null &&
                        dsActualValues.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = dsActualValues.Tables[0].Rows[0];

                        measurementPatchDetails.CielabL = DataReader.ReadDecimal(row, "CIELAB_L");
                        measurementPatchDetails.CielabA = DataReader.ReadDecimal(row, "CIELAB_a");
                        measurementPatchDetails.CielabB = DataReader.ReadDecimal(row, "CIELAB_b");
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetActualValues")
            );

            return measurementPatchDetails;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}