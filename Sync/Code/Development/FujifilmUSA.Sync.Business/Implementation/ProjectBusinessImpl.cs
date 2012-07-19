using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ProjectBusinessImpl : IProjectBusiness
    {
        #region IProjectBusiness Members

        public Project GetProjectDetail(int projectId)
        {
            var project = new Project();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProject =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetProjectDetail",
                                new object[]
                                        {
                                            projectId
                                        });
                    if (dsProject.Tables[0].Rows.Count > 0)
                    {
                        project = new Project()
                                      {
                                          ProjectId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "ProjectID"),
                                          ProjectName = DataReader.ReadString(dsProject.Tables[0].Rows[0], "ProjectName"),
                                          CreateDate = DataReader.ReadDate(dsProject.Tables[0].Rows[0], "CreateDate"),
                                          CurrDlpId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "CurrDLPID"),
                                          IsUsed = DataReader.ReadBoolean(dsProject.Tables[0].Rows[0], "IsUsed"),
                                          Status = (EOptimizeRunStatus)DataReader.ReadInt(dsProject.Tables[0].Rows[0], "ProjStatus"),
                                          SourceProfile = new SourceProfile()
                                                              {
                                                                  SourceProfileId = DataReader.ReadShort(dsProject.Tables[0].Rows[0], "SourceProfileID"),
                                                                  ProfileName = DataReader.ReadString(dsProject.Tables[0].Rows[0], "ProfileName")
                                                              },
                                          DeviceFamilyProfilerOptionId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "BaseDeviceFamilyProfilerOption")
                                      };
                        if (dsProject.Tables[1].Rows.Count > 0)
                        {
                            project.PrintCondition =
                                new PrintCondition()
                                    {
                                        Media = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Media"),
                                        Colorant = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Colorant"),
                                        RipSettings = DataReader.ReadString(dsProject.Tables[1].Rows[0], "RipSettings"),
                                        TechnicianName = DataReader.ReadString(dsProject.Tables[1].Rows[0], "TechnicianName"),
                                        Notes = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Notes"),
                                    };
                        }
                        if (dsProject.Tables[2].Rows.Count > 0)
                        {
                            project.MeasurementSetting =
                                new MeasurementSetting()
                                    {
                                        ObserverType = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "ObserverType"),
                                        Illuminant = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "Illuminant"),
                                        UvIncluded = DataReader.ReadBoolean(dsProject.Tables[2].Rows[0], "UVIncluded"),
                                        MeasurementDeviceId =
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "MeasurementDeviceID"),
                                        ReferenceChartType = new KeyValuePair<short, string>(
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "ReferenceChartTypeID"),
                                            DataReader.ReadString(dsProject.Tables[2].Rows[0], "ChartName"))
                                    };
                        }
                        if (dsProject.Tables[3].Rows.Count > 0)
                        {
                            project.ProjectOption = dsProject.Tables[3].AsEnumerable().
                                Select(x => new ProjectOption()
                                                {
                                                    ProfileTypeId = DataReader.ReadShort(x, "ProfileTypeID"),
                                                    ProfilerOptionId = DataReader.ReadShort(x, "ProfilerOptionID"),
                                                    ProfilerOptionValue = DataReader.ReadString(x, "ProfilerOptionValue"),
                                                }).ToList();
                        }
                        project.OptimizeRuns = dsProject.Tables[4].AsEnumerable().
                            Select(x => new OptimizeRun()
                            {
                                OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                               // RunName = DataReader.ReadString(x, "RunName"),
                                RunName = (DataReader.ReadString(x, "RunName").Contains("DLP")) ? DataReader.ReadString(x, "RunName") : ((EOptimizeRunStatus)DataReader.ReadInt(x, "RunName") == EOptimizeRunStatus.Open) ? "Open" : "Certification",
                                ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "Status"),
                                StartDateTime = DataReader.ReadDate(x, "StartDateTime"),
                                EndDateTime = DataReader.ReadDate(x, "EndDateTime"),
                                MasterRunId = DataReader.ReadInt(x, "MasterRunID"),
                                HasMeasurements = (DataReader.ReadInt(x, "MeasurementCount") > 0),
                                HasProfile = DataReader.ReadBoolean(x, "HasProfile"),
                                ReferenceChartType = new KeyValuePair<short, string>(DataReader.ReadShort(x, "ReferenceChartTypeId"), DataReader.ReadString(x, "ChartName"))
                            }).ToList();

                        int index = 0;
                        string LastDLPName = "";
                        project.OptimizeRuns.ForEach(x =>
                                                         {
                                                             if (index > 0)
                                                             {
                                                                 LastDLPName = project.ProjectName + " DLP" + project.OptimizeRuns[index - 1].EndDateTime.Value.Month.ToString("00") + project.OptimizeRuns[index - 1].EndDateTime.Value.Day.ToString("00") + project.OptimizeRuns[index - 1].EndDateTime.Value.Hour.ToString("00") + project.OptimizeRuns[index - 1].EndDateTime.Value.Minute.ToString("00") + ".icc";
                                                                 if (index > 1)
                                                                 {
                                                                     if (project.OptimizeRuns[index - 1].Status == EOptimizeRunStatus.Closed && !project.OptimizeRuns[index - 1].HasProfile)
                                                                     {
                                                                         LastDLPName = project.ProjectName + " DLP" + project.OptimizeRuns[index - 2].EndDateTime.Value.Month.ToString("00") + project.OptimizeRuns[index - 2].EndDateTime.Value.Day.ToString("00") + project.OptimizeRuns[index - 2].EndDateTime.Value.Hour.ToString("00") + project.OptimizeRuns[index - 2].EndDateTime.Value.Minute.ToString("00") + ".icc";  //----If last run is certify run, then get DLP of its previous run----//
                                                                     }
                                                                 }
                                                             }
                                                             x.LastDlpName = (index == 0) ? string.Empty : LastDLPName;
                                                             x.DlpName = string.Format("{0} DLP {1}.icc",
                                                                                       project.ProjectName,
                                                                                       index++.ToString("000"));

                                                         });
                        if (project.CurrDlpId > 0)
                        {
                            project.CurrentDlpName = project.OptimizeRuns.Single(x => x.OptimizeRunId == project.CurrDlpId).DlpName;
                            project.ProposedDlpName = project.OptimizeRuns.Last().DlpName;
                        }
                        project.CurrentDisplayStatus =
                            project.OptimizeRuns.Count() == 1
                                ? ECurrentProjectStatus.New
                                : ECurrentProjectStatus.Run;
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetProjectDetail"));
            return project;
        }

        public List<Project> GetProjectsByDeviceId(int deviceId, int LicenseTypeID)
        {
            var objProjectList = new List<Project>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectList =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetProjectList",
                                new object[]
                                        {
                                            deviceId, LicenseTypeID
                                        });
                    if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
                    {
                        objProjectList = dsProjectList.Tables[0].AsEnumerable().
                            Select(x => new Project()
                                            {
                                                ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                                ProjectName = DataReader.ReadString(x, "ProjectName"),
                                                DeviceId = DataReader.ReadInt(x, "CpDeviceId"),
                                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                                LicenseTypeID = DataReader.ReadInt(x, "LicenseTypeID"),
                                                TotalRuns = DataReader.ReadInt(x, "TotalRuns"),
                                                HasDLP = DataReader.ReadBoolean(x, "HasDLP"),
                                                SourceProfile = new SourceProfile()
                                                                    {
                                                                        SourceProfileId = DataReader.ReadShort(x, "SourceProfileId"),
                                                                        ProfileName = DataReader.ReadString(x, "ProfileName")
                                                                    },
                                                CreateDate = DataReader.ReadDate(x, "EndDateTime"),
                                                PrintCondition =
                                                    new PrintCondition()
                                                        {
                                                            Media = DataReader.ReadString(x, "Media"),
                                                            Colorant = DataReader.ReadString(x, "Colorant"),
                                                            RipSettings = DataReader.ReadString(x, "RipSettings")
                                                        },
                                                Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "ProjStatus"),
                                                CurrentDlpName = (DataReader.ReadInt(x, "CurrentDlpNo").ToString("000").Contains("-001")) ? String.Format("{0} DLP {1}.icc",
                                                                               DataReader.ReadString(x, "ProjectName"),
                                                                               DataReader.ReadInt(x, "CurrentDlpNo").ToString("000"))
                                                                               : String.Format("{0} DLP{1}.icc",
                                                                               DataReader.ReadString(x, "ProjectName"),
                                                                               DataReader.ReadDate(x, "EndDateTime").Month.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Day.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Hour.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Minute.ToString("00"))

                                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetProjectsByDeviceId"));
            return objProjectList;
        }

        public int DeleteProject(int projectId)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[DeleteProject]",
                            new object[]
                                    {
                                        projectId
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteProject"));
            return returnValue;
        }

        public Dictionary<string, string> GetProjectOption(int projectId, EProfileType type)
        {
            var projectOptions = new Dictionary<string, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectOption =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetProjectOptions",
                                new object[]
                                        {
                                            projectId,
                                            Convert.ToInt32(type)
                                        });
                    projectOptions = dsProjectOption.Tables[0].AsEnumerable().
                        Select(x =>
                               new KeyValuePair<string, string>(
                                   DataReader.ReadString(x, "ProfilerOption"),
                                   DataReader.ReadString(x, "ProfilerOptionValue"))).ToDictionary(x => x.Key, x => x.Value);
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetProjectOption"));
            return projectOptions;
        }

        public List<SourceProfile> GetSourceProfileList(int currentUserId)
        {
            var dstSourceList = new List<SourceProfile>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSourceList =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetSourceProfileList",
                                new object[] { currentUserId });
                    if (dsSourceList != null && dsSourceList.Tables.Count > 0)
                    {
                        dstSourceList = (dsSourceList.Tables[0].AsEnumerable().Select(
                            source => new SourceProfile()
                                          {
                                              SourceProfileId = DataReader.ReadShort(source, "SourceProfileId"),
                                              ProfileName = DataReader.ReadString(source, "ProfileName"),
                                              ProfileDesc = DataReader.ReadString(source, "ProfileDesc"),
                                          })).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfileList"));
            return dstSourceList;
        }

        public List<DeviceFamilyProfilerOption> GetDeviceFamilyProfilerOptionPreset(short deviceFamilyId)
        {
            var presetList = new List<DeviceFamilyProfilerOption>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPresetList = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetDeviceFamilyProfilerOption", new object[] { deviceFamilyId });
                    if (dsPresetList != null && dsPresetList.Tables.Count > 0)
                    {
                        presetList = dsPresetList.Tables[0].AsEnumerable().Select(
                            x => new DeviceFamilyProfilerOption()
                                     {
                                         DeviceFamilyProfilerOptionId = DataReader.ReadInt(x, "DeviceFamilyProfilerOptionID"),
                                         ProfilerOptionsName = DataReader.ReadString(x, "ProfilerOptionsName"),
                                         ProfilerOptionsDesc = DataReader.ReadString(x, "ProfilerOptionsDesc"),
                                     }).ToList();

                        var profilerOptionPresets = dsPresetList.Tables[1].
                            AsEnumerable().
                            Select(x =>
                                   new ProfilerOptionPreset()
                                       {
                                           DeviceFamilyProfilerOptionId = DataReader.ReadInt(x, "DeviceFamilyProfilerOptionID"),
                                           ProfileTypeId = DataReader.ReadShort(x, "ProfileTypeID"),
                                           ProfilerOption = new ProfilerOption()
                                                                {
                                                                    ProfilerOptionId = DataReader.ReadShort(x, "ProfilerOptionID"),
                                                                    Option = DataReader.ReadString(x, "ProfilerOption")
                                                                },
                                           ProfilerOptionValue = DataReader.ReadString(x, "ProfilerOptionValue")
                                       }).ToList();
                        presetList.
                            ForEach(x =>
                                    x.ProfilerOptionPresets = profilerOptionPresets.
                                                                  Where(y => y.DeviceFamilyProfilerOptionId == x.DeviceFamilyProfilerOptionId).
                                                                  ToList());
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDeviceFamilyProfilerOptionPreset"));
            return presetList;
        }

        public byte[] GetChartPreviewOnReferenceChartTypeId(short referenceChartTypeId, string source)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    if (source == "Preview")
                    {
                        data = DataAccessFactory.Instance.GetDsResult("CP.GetChartPreviewOnReferenceChartTypeId",
                                new object[]
                                    {
                                        referenceChartTypeId
                                    }).Tables[0].Rows[0][0];
                    }
                    else if (source == "Download")
                    {
                        data = DataAccessFactory.Instance.GetDsResult("CP.GetChartPreviewOnReferenceChartTypeId",
                                new object[]
                                    {
                                        referenceChartTypeId
                                    }).Tables[1].Rows[0][0];
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetChartPreviewOnOptimizeRunId"));
            return (data == null || data == DBNull.Value) ? null : (byte[])data;
        }

        public byte[] GetSourceProfile(int projectId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSync4D.GetSourceProfileData",
                            new object[]
                                    {
                                        projectId
                                    });
                    if (data == null || data == DBNull.Value)
                    {
                        throw new Exception("Source profile not found for projectId: " + projectId);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfile"));
            return data == DBNull.Value ? null : (byte[])data;
        }

        public byte[] GetDestinationProfile(int projectId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSync4D.GetDestinationProfileData",
                            new object[]
                                    {
                                        projectId
                                    });
                    if (data == null || data == DBNull.Value)
                    {
                        throw new Exception("destination profile not found for projectId: " + projectId);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetDestinationProfile"));
            return data == DBNull.Value ? null : (byte[])data;
        }

        public byte[] GetLastOptimizedDlp(int projectId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSync4D.GetLastOptimizedDlp",
                            new object[]
                                    {
                                        projectId
                                    });
                    if (data == null || data == DBNull.Value)
                    {
                        throw new Exception("Last optimized device link profile not found for projectId: " + projectId);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLastOptimizedDlp"));
            return data == DBNull.Value ? null : (byte[])data;
        }

        public Project SaveProject(Project project)
        {
            var savedProject = new Project();
            if (project.ProjectId > 0)
            {
                #region Update Project
                Common.ImplementBusinessLogic(
                    () =>
                    {
                        var projectOptions = new XElement("ProjectOptions",
                                                          project.ProjectOption.
                                                              Select(x =>
                                                                     new XElement("ProjectOption",
                                                                                  new XAttribute("ProfileTypeID", x.ProfileTypeId),
                                                                                  new XAttribute("ProfilerOptionID", x.ProfilerOptionId),
                                                                                  new XAttribute("ProfilerOptionValue", x.ProfilerOptionValue)
                                                                         )).ToList());
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSync4D.UpdateProject",
                                             new object[]
                                                 {
                                                     project.ProjectId,
                                                     project.SourceProfile.SourceProfileId,
                                                     project.ProjectName,
                                                     project.PrintCondition.Media,
                                                     project.PrintCondition.Colorant,
                                                     project.PrintCondition.RipSettings,
                                                     project.PrintCondition.TechnicianName,
                                                     project.PrintCondition.Notes,
                                                     project.MeasurementSetting.ObserverType,
                                                     project.MeasurementSetting.Illuminant,
                                                     project.MeasurementSetting.UvIncluded,
                                                     project.MeasurementSetting.MeasurementDeviceId,
                                                     project.MeasurementSetting.ReferenceChartType.Key,
                                                     project.DeviceFamilyProfilerOptionId,
                                                     projectOptions.ToString()
                                                 });
                        savedProject = GetProjectDetail(project.ProjectId);
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveProject"));
                #endregion
            }
            else
            {
                #region Insert Project
                var outParam = new Parameters() { ParameterName = "@ProjectID", Type = DbType.Int32 };
                Common.ImplementBusinessLogic(
                    () =>
                    {
                        var projectOptions = new XElement("ProjectOptions",
                                                          project.ProjectOption.
                                                              Select(x =>
                                                                     new XElement("ProjectOption",
                                                                                  new XAttribute("ProfileTypeID", x.ProfileTypeId),
                                                                                  new XAttribute("ProfilerOptionID", x.ProfilerOptionId),
                                                                                  new XAttribute("ProfilerOptionValue", x.ProfilerOptionValue)
                                                                         )).ToList());
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSync4D.CreateProject",
                                             new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@CPDeviceID", Type = DbType.Int32, Value = project.DeviceId},
                                                     new Parameters() {ParameterName = "@SourceProfileID", Type = DbType.Int32, Value = project.SourceProfile.SourceProfileId},
                                                     new Parameters() {ParameterName = "@ProjectName", Size = 30, Type = DbType.String, Value = project.ProjectName},
                                                     new Parameters()
                                                         {
                                                             ParameterName = "@CreateDate",
                                                             Type = DbType.DateTime,
                                                             Value = project.CreateDate.AddMilliseconds(-project.CreateDate.Millisecond)
                                                         },
                                                     new Parameters() {ParameterName = "@Media", Size = 50, Type = DbType.String, Value = project.PrintCondition.Media},
                                                     new Parameters() {ParameterName = "@Colorant", Size = 50, Type = DbType.String, Value = project.PrintCondition.Colorant},
                                                     new Parameters() {ParameterName = "@RipSettings", Size = 50, Type = DbType.String, Value = project.PrintCondition.RipSettings},
                                                     new Parameters()
                                                         {ParameterName = "@TechnicalName", Size = 30, Type = DbType.String, Value = project.PrintCondition.TechnicianName},
                                                     new Parameters() {ParameterName = "@Notes", Size = 500, Type = DbType.String, Value = project.PrintCondition.Notes},
                                                     new Parameters() {ParameterName = "@ObserverType", Type = DbType.Int16, Value = project.MeasurementSetting.ObserverType},
                                                     new Parameters() {ParameterName = "@Illuminant", Type = DbType.Int16, Value = project.MeasurementSetting.Illuminant},
                                                     new Parameters() {ParameterName = "@UVIncluded", Type = DbType.Boolean, Value = project.MeasurementSetting.UvIncluded},
                                                     new Parameters()
                                                         {ParameterName = "@MeasurementDeviceID", Type = DbType.Int16, Value = project.MeasurementSetting.MeasurementDeviceId},
                                                     new Parameters()
                                                         {ParameterName = "@ReferenceChartTypeID", Type = DbType.Int16, Value = project.MeasurementSetting.ReferenceChartType.Key},
                                                     new Parameters() {ParameterName = "@Status", Type = DbType.Int32, Value = Convert.ToInt32(EOptimizeRunStatus.Open)},
                                                     new Parameters() {ParameterName = "@StartDateTime", Type = DbType.DateTime, Value = DateTime.Now},
                                                     new Parameters() {ParameterName = "@DeviceFamilyProfilerOptionId", Type = DbType.Int32, Value = project.DeviceFamilyProfilerOptionId},
                                                     new Parameters() {ParameterName = "@ProjectOptions", Value = projectOptions.ToString()},
                                                     new Parameters() {ParameterName = "@LicenseTypeID", Type = DbType.Int32, Value = project.LicenseTypeID}
                                                 },
                                             new List<Parameters>()
                                                 {
                                                     outParam
                                                 });
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveProject"));
                if (outParam.Value != null)
                {
                    var projectId = Convert.ToInt32(outParam.Value);
                    savedProject = GetProjectDetail(projectId);
                }
                #endregion
            }
            return savedProject;
        }

        public ReferenceChartData GetReferenceChartData(int projectId)
        {
            var chartData = new ReferenceChartData();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsReferenceChart = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[GetReferenceChart]", new object[] { projectId });
                    if (dsReferenceChart.Tables[0].Rows.Count > 0)
                    {
                        chartData.ReferenceChartTypeId = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "ReferenceChartTypeID");
                        chartData.ChartName = DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ChartName");
                        chartData.NumOfCols = DataReader.ReadInt(dsReferenceChart.Tables[0].Rows[0], "NumOfCols");
                        chartData.NumOfRows = DataReader.ReadInt(dsReferenceChart.Tables[0].Rows[0], "NumOfRows");
                        chartData.TotalPatches = DataReader.ReadInt(dsReferenceChart.Tables[0].Rows[0], "TotalPatches");
                        chartData.InstrumentTypeId = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "InstrumentTypeID");
                        chartData.MeasureDeviceSrNo = DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "MeasureDeviceSrNo");
                        chartData.ActivationKey =
                            Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + chartData.MeasureDeviceSrNo);
                        var uvIncluded = DataReader.ReadBoolean(dsReferenceChart.Tables[0].Rows[0], "UVIncluded");
                        chartData.DeviceSetting = new MeasurementDevice
                                                      {
                                                          MeasurementSettings = new MeasurementSetting()
                                                                                    {
                                                                                        IlluminantName =
                                                                                            DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "IlluminantType"),
                                                                                        ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ObserverType")),
                                                                                        UvIncludedName =
                                                                                            uvIncluded ? EUVIncludedMode.UV.ToString() : EUVIncludedMode.UVCut.ToString(),
                                                                                        DensityType = EDensityType.ANSIT.ToString()
                                                                                    }
                                                      };
                        chartData.ReferenceCharts = dsReferenceChart.Tables[1].AsEnumerable().
                            Select(x =>
                                   new ReferenceChart()
                                       {
                                           ChartTypeId = DataReader.ReadShort(x, "ChartTypeID"),
                                           SampleName = DataReader.ReadString(x, "SampleName"),
                                           CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                                           CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                                           CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                                           CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                                           PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                           HtmlColor = DataReader.ReadString(x, "HTMLColor")
                                       }).ToList();

                        var reader = DataAccessFactory.Instance.GetXmlResult("CPSync4D.GetChartSettings", new object[] { chartData.ReferenceChartTypeId });
                        var dsChartSettings = new DataSet();
                        dsChartSettings.ReadXml(reader);
                        if (dsChartSettings.Tables.Count > 0)
                        {
                            chartData.ChartSetting = new ChartSetting();
                            if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeX"))
                                chartData.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeX");
                            if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeY"))
                                chartData.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeY");
                            if (dsChartSettings.Tables[0].Columns.Contains("BarSizeX"))
                                chartData.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeX");
                            if (dsChartSettings.Tables[0].Columns.Contains("BarSizeY"))
                                chartData.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeY");
                            if (dsChartSettings.Tables[0].Columns.Contains("MarkerDistance"))
                                chartData.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "MarkerDistance");
                            if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeX"))
                                chartData.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeX");
                            if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeY"))
                                chartData.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeY");
                            if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstLineY"))
                                chartData.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstLineY");
                            if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstField"))
                                chartData.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstField");
                            if (dsChartSettings.Tables[0].Columns.Contains("DistToBar"))
                                chartData.ChartSetting.DistToBar = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToBar");
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartData"));
            return chartData;
        }

        public Guid StoreTempararyProjectInformation(string projectName, string media, string colorant, string ripSettings, int measurementDeviceTypeId, int chartId, int projectId, Guid tempProjectId, string silverlightPostUrl)
        {
            var outParam = new Parameters() { ParameterName = "@TempProjectId", Size = 16, Type = DbType.Guid };
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure("CPSync4D.GetTempProjectInfoGuid",
                                           new List<Parameters>()
                                               {
                                                   new Parameters() {ParameterName = "@ProjectName", Type = DbType.String, Size = 30, Value = projectName},
                                                   new Parameters() {ParameterName = "@Media", Type = DbType.String, Size = 30, Value = media},
                                                   new Parameters() {ParameterName = "@Colorant", Type = DbType.String, Size = 30, Value = colorant},
                                                   new Parameters() {ParameterName = "@RipSettings", Type = DbType.String, Size = 30, Value = ripSettings},
                                                   new Parameters() {ParameterName = "@MeasurementDeviceTypeId", Type = DbType.Int32, Size = 4, Value = measurementDeviceTypeId},
                                                   new Parameters() {ParameterName = "@ChartId", Type = DbType.Int32, Size = 4, Value = chartId},
                                                   new Parameters() {ParameterName = "@ProjectId", Type = DbType.Int32, Size = 4, Value = projectId},
                                                   new Parameters() {ParameterName = "@SilverlightPostUrl", Type = DbType.String, Size = 200, Value = silverlightPostUrl},
                                                   new Parameters() {ParameterName = "@TempProjectIdIN", Type = DbType.Guid, Size = 16, Value = tempProjectId},
                                               },
                                           new List<Parameters>()
                                               {
                                                   outParam
                                               }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_StoreTempararyProjectInformation"));
            if (outParam.Value != null)
            {
                tempProjectId = Guid.Parse(outParam.Value.ToString());
            }
            return tempProjectId;
        }

        public TempProjectInfo GetTempProjectInfo(Guid tempProjectId)
        {
            var projectInfo = new TempProjectInfo();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectInfo = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetTempProjectInfo", new object[] { tempProjectId });
                    if (dsProjectInfo.Tables[0].Rows.Count > 0)
                    {
                        projectInfo.TempProjectId = tempProjectId;
                        projectInfo.ProjectName = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "ProjectName");
                        projectInfo.Media = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "Media");
                        projectInfo.Colorant = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "Colorant");
                        projectInfo.RipSettings = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "RipSettings");
                        projectInfo.MeasurementDeviceTypeId = DataReader.ReadInt(dsProjectInfo.Tables[0].Rows[0], "MeasurementDeviceTypeId");
                        projectInfo.ChartId = DataReader.ReadInt(dsProjectInfo.Tables[0].Rows[0], "ChartId");
                        projectInfo.ProjectId = DataReader.ReadInt(dsProjectInfo.Tables[0].Rows[0], "ProjectId");
                        projectInfo.SilverlightPostUrl = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "SilverlightPostUrl");
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetTempProjectInfo"));
            return projectInfo;
        }

        public List<Illuminant> GetIlluminantList()
        {
            var dstIlluminantList = new List<Illuminant>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsIlluminantList = DataAccessFactory.Instance.GetDsResult("CP.GetIlluminantList", new object[] { });
                    if (dsIlluminantList != null && dsIlluminantList.Tables.Count > 0)
                    {
                        dstIlluminantList = (dsIlluminantList.Tables[0].AsEnumerable().Select(
                            illumin => new Illuminant()
                                           {
                                               IlluminantId = DataReader.ReadShort(illumin, "IlluminantID"),
                                               IlluminantType = DataReader.ReadString(illumin, "IlluminantType"),
                                           })).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetIlluminantList"));
            return dstIlluminantList;
        }

        public List<ObserverType> GetObserverTypeList()
        {
            var dstObserverTypeList = new List<ObserverType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsObserverTypeList = DataAccessFactory.Instance.GetDsResult("CP.GetObserverTypeList", new object[] { });
                    if (dsObserverTypeList != null && dsObserverTypeList.Tables.Count > 0)
                    {
                        dstObserverTypeList = (dsObserverTypeList.Tables[0].AsEnumerable().Select(
                            observer => new ObserverType()
                                            {
                                                ObserverTypeId = DataReader.ReadShort(observer, "ObserverTypeID"),
                                                Type = DataReader.ReadString(observer, "ObserverType"),
                                            })).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetObserverTypeList"));
            return dstObserverTypeList;
        }

        public List<MeasurementDevice> GetMeasurementDeviceList(int deviceId)
        {
            var measurementDevice = new List<MeasurementDevice>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurementDevice = DataAccessFactory.Instance.GetDsResult("CP.GetMeasurementDeviceList",
                                                                                     new object[] { deviceId });
                    measurementDevice = dsMeasurementDevice.Tables[0].AsEnumerable().
                        Select(x =>
                               new MeasurementDevice()
                                   {
                                       MeasurementDeviceId = DataReader.ReadShort(x, "MeasurementDeviceID"),
                                       InstrumentTypeId = DataReader.ReadShort(x, "InstrumentTypeID"),
                                       InstrumentType = new InstrumentType()
                                                            {
                                                                InstrumentTypeId = DataReader.ReadShort(x, "InstrumentTypeID"),
                                                                InstrumentName = DataReader.ReadString(x, "InstrumentName")
                                                            },
                                       SerialNumber = DataReader.ReadString(x, "MeasurementDevi")
                                   }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementDeviceList"));
            return measurementDevice;
        }

        public List<ReferenceChartType> GetReferenceChartTypeList(int measurementDeviceId, int ChartDisplayType)
        {
            var presetList = new List<ReferenceChartType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPresetList = DataAccessFactory.Instance.GetDsResult("CP.GetReferenceChartTypeList", new object[] { measurementDeviceId, ChartDisplayType });
                    if (dsPresetList != null && dsPresetList.Tables.Count > 0)
                    {
                        presetList = dsPresetList.Tables[0].AsEnumerable().Select(
                            x => new ReferenceChartType()
                                     {
                                         ReferenceChartTypeId = DataReader.ReadShort(x, "ReferenceChartTypeID"),
                                         ChartName = DataReader.ReadString(x, "ChartName"),
                                         ChartPreview = DataReader.ReadByteArray(x, "ChartPreview"),
                                     }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartTypeList"));
            return presetList;
        }

        public Dictionary<string, string> GetPrintConditionPreviewData(short MeasurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId)
        {
            var data = new Dictionary<string, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPreviewData = DataAccessFactory.Instance.GetDsResult("CP.GetPreviewPrintDetail",
                                                                               new object[]
                                                                                       {
                                                                                           MeasurementDeviceId,
                                                                                           illuminantId,
                                                                                           observerTypeId,
                                                                                           referenceChartTypeId,
                                                                                           deviceFamilyProfilerOptionId
                                                                                       });

                    if (dsPreviewData.Tables[0].Rows.Count > 0)
                    {
                        data.Add("MeasurementDeviceId", dsPreviewData.Tables[0].Rows[0]["MeasurementDevice"].ToString());

                    }
                    if (dsPreviewData.Tables[1].Rows.Count > 0)
                    {
                        data.Add("illuminantId", dsPreviewData.Tables[1].Rows[0]["IlluminantType"].ToString());
                    }
                    if (dsPreviewData.Tables[2].Rows.Count > 0)
                    {
                        data.Add("observerTypeId", dsPreviewData.Tables[2].Rows[0]["ObserverType"].ToString());
                    }
                    if (dsPreviewData.Tables[3].Rows.Count > 0)
                    {
                        data.Add("referenceChartTypeId", dsPreviewData.Tables[3].Rows[0]["ChartName"].ToString());
                    }
                    if (dsPreviewData.Tables[4].Rows.Count > 0)
                    {
                        data.Add("deviceFamilyProfilerOptionId", dsPreviewData.Tables[4].Rows[0]["ProfilerOptionsName"].ToString());
                        data.Add("deviceFamilyProfilerOptionDesc", dsPreviewData.Tables[4].Rows[0]["ProfilerOptionsDesc"].ToString());
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPrintConditionPreviewData"));
            return data;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
