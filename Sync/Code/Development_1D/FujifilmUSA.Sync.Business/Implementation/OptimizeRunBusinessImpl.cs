using System;
using System.Data;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Business.ProfilerService;
using FujifilmUSA.Sync.Business.CPSyncService;
using FujifilmUSA.Sync.Business.Messages;
using System.Xml.Linq;
using FujifilmUSA.Sync.Business.CustomExceptions;

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

        public void Create1DOptimizeRun(int projectId, int status, DateTime startDateTime)
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
                                startDateTime
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateOptimizeRun"));
        }

        //to-do set return type
        public void CreateG7AlignmentCurve(int projectId,int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    MeasurementBusinessImpl measurementBusiness = new MeasurementBusinessImpl();
                    MeasurementDataResponse measurementDataResponse = measurementBusiness.Get1DMeasurementData(measurementIds);

                    CPSyncClient syncClient = new CPSyncClient();
                    Curve alignmentCurveRequest = new Curve();
                    alignmentCurveRequest.arrDCS = new double[measurementDataResponse.DcsData.Count];
                    for (int i = 0; i < measurementDataResponse.DcsData.Count; i++)
                    {
                        alignmentCurveRequest.arrDCS[i] = measurementDataResponse.DcsData[i];
                    }

                    alignmentCurveRequest.arrPCS = new double[measurementDataResponse.PcsData.Count];
                    for (int i = 0; i < measurementDataResponse.PcsData.Count; i++)
                    {
                        alignmentCurveRequest.arrPCS[i] = measurementDataResponse.PcsData[i];
                    }
                    alignmentCurveRequest.arrDensity = new double[measurementDataResponse.DensityData.Count];
                    for (int i = 0; i < measurementDataResponse.DensityData.Count; i++)
                    {
                        alignmentCurveRequest.arrDensity[i] = measurementDataResponse.DensityData[i];
                    }

                    alignmentCurveRequest.UseDLP = useDLP;
                    if (useDLP)
                    {
                        var dlp = BusinessFactory<IProjectBusiness>.CreateObject().Get1DLastOptimizedDlp(projectId);
                        alignmentCurveRequest.ICCProfile = dlp;
                    }

                    Profile syncClientResponse = new Profile();
                    syncClientResponse = syncClient.CreateCurve(alignmentCurveRequest);

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
                                                                            new object[] { optimizeRunId, syncClientResponse.ICCProfile, status, endDateTime });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateG7AlignmentCurve"));
        }

        public void CreateISOAlignmentCurve(int projectId, int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    MeasurementBusinessImpl measurementBusiness = new MeasurementBusinessImpl();
                    MeasurementDataResponse measurementDataResponse = measurementBusiness.Get1DMeasurementData(measurementIds);

                    CPSyncClient syncClient = new CPSyncClient();
                    Curve alignmentCurveRequest = new Curve();
                    alignmentCurveRequest.arrDCS = new double[measurementDataResponse.DcsData.Count];
                    for (int i = 0; i < measurementDataResponse.DcsData.Count; i++)
                    {
                        alignmentCurveRequest.arrDCS[i] = measurementDataResponse.DcsData[i];
                    }

                    alignmentCurveRequest.arrPCS = new double[measurementDataResponse.PcsData.Count];
                    for (int i = 0; i < measurementDataResponse.PcsData.Count; i++)
                    {
                        alignmentCurveRequest.arrPCS[i] = measurementDataResponse.PcsData[i];
                    }
                    alignmentCurveRequest.arrDensity = new double[measurementDataResponse.DensityData.Count];
                    for (int i = 0; i < measurementDataResponse.DensityData.Count; i++)
                    {
                        alignmentCurveRequest.arrDensity[i] = measurementDataResponse.DensityData[i];
                    }

                    alignmentCurveRequest.UseDLP = useDLP;
                    if (useDLP)
                    {
                        var dlp = BusinessFactory<IProjectBusiness>.CreateObject().Get1DLastOptimizedDlp(projectId);
                        alignmentCurveRequest.ICCProfile = dlp;
                    }

                    TargetTVIDataResponse targetTVIResponse = measurementBusiness.GetTargetTVIData(projectId);
                    alignmentCurveRequest.LinearizationTVI25 = targetTVIResponse.TVI25;
                    alignmentCurveRequest.LinearizationTVI50 = targetTVIResponse.TVI50;
                    alignmentCurveRequest.LinearizationTVI75 = targetTVIResponse.TVI75;

                    Profile syncClientResponse = new Profile();
                    syncClientResponse = syncClient.CreateISOCurve(alignmentCurveRequest);

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
                                                                            new object[] { optimizeRunId, syncClientResponse.ICCProfile, status, endDateTime });

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

        public void CreateDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
        {
            //----This code is for Source to Source DLP generation-----------//
            Common.ImplementBusinessLogic(
                () =>
                {
                    var sourceProfile =
                        BusinessFactory<IProjectBusiness>.
                            CreateObject().
                            GetSourceProfile(projectId);                  

                    var request = new CreateDeviceLinkProfileRequest()
                    {
                        SourceProfile = new IccProfile() { ProfileData = sourceProfile },                       
                        DlpOptions = new List<Parameter>()
                    };
              
                    var profilerOptionBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                 
                    var dlpOptions = profilerOptionBusiness.GetProjectOption(projectId, EProfileType.DeviceLinkProfile);
                    foreach (KeyValuePair<string, string> option in dlpOptions)
                    {
                        request.DlpOptions.Add(new Parameter() { Name = option.Key, Value = option.Value });
                    }
                    request.DlpOptions.Add(new Parameter() { Name = Common.ProfileDescriptionKey, Value = dlpDescription });
                  
                    using (var client = new ProfilerClient())
                    {
                        var response = client.CreateDeviceLinkProfile(request);
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
                    }

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateInitialDestinationAndDeviceLinkProfiles"));
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}