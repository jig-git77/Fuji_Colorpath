using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using System.Xml.Linq;
using FujifilmUSA.Sync.Entity.CPSpot;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ProjectBusinessImpl : IProjectBusiness
    {
        #region IProjectBusiness Members

        public Project GetProjectDetail(int projectId, bool isVerification = false)
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
                                            projectId,
                                            isVerification
                                        });
                    if (dsProject.Tables[0].Rows.Count > 0)
                    {
                        project = new Project()
                        {
                            ProjectId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "ProjectID"),
                            DeviceId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "CPDeviceID"),
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
                            PrintStandardType = new Entity.CPSync1D.PrintStandardType()
                            {
                                PrintStandard = DataReader.ReadString(dsProject.Tables[0].Rows[0], "PrintStandard"),
                                PrintStandardTypeID = DataReader.ReadShort(dsProject.Tables[0].Rows[0], "PrintStandardTypeId"),
                            },
                            DeviceFamilyProfilerOptionId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "BaseDeviceFamilyProfilerOption"),
                            EnabledValidation = DataReader.ReadBoolean(dsProject.Tables[0].Rows[0], "EnabledValidation"),
                            LicenseType = (ELicenseType)DataReader.ReadInt(dsProject.Tables[0].Rows[0], "LicenseTypeID"),
                            MetricTargetPresetId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "MetricTargetPresetID"),
                            ProjectTypeId = DataReader.ReadShort(dsProject.Tables[0].Rows[0], "ProjectTypeId"),
                            DensityType = (DensityTypes)(DataReader.ReadInt(dsProject.Tables[0].Rows[0], "DensityType") == -1 ? 0 : DataReader.ReadInt(dsProject.Tables[0].Rows[0], "DensityType")),
                            PCSDataTypes = (PCSDataTypes)(DataReader.ReadInt(dsProject.Tables[0].Rows[0], "PCSDataType") == -1 ? (DataReader.ReadShort(dsProject.Tables[0].Rows[0], "ProjectTypeId") != 2 ? 1 : 0) : DataReader.ReadInt(dsProject.Tables[0].Rows[0], "PCSDataType")),  //----If Sync1D G7 projectType then default is XYZ and if ISO type then default is Density and for all 4D projects, default is XYZ like G7
                            ReTargetProjectID = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "ReTargetProjectID"),
                            CurveTypeID = DataReader.ReadShort(dsProject.Tables[0].Rows[0], "CurveTypeID"),
                            EnabledPaperScaling = DataReader.ReadBoolean(dsProject.Tables[0].Rows[0], "EnabledPaperScaling"),
                            NewPaper_White_L = DataReader.ReadDecimal(dsProject.Tables[0].Rows[0], "NewPaper_White_L"),
                            NewPaper_White_a = DataReader.ReadDecimal(dsProject.Tables[0].Rows[0], "NewPaper_White_a"),
                            NewPaper_White_b = DataReader.ReadDecimal(dsProject.Tables[0].Rows[0], "NewPaper_White_b"),
                            NewPaper_Black_L = DataReader.ReadDecimal(dsProject.Tables[0].Rows[0], "NewPaper_Black_L"),
                            NewPaper_Black_a = DataReader.ReadDecimal(dsProject.Tables[0].Rows[0], "NewPaper_Black_a"),
                            NewPaper_Black_b = DataReader.ReadDecimal(dsProject.Tables[0].Rows[0], "NewPaper_Black_b")
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
                                    InkOrder = (EInkOrder)DataReader.ReadInt(dsProject.Tables[1].Rows[0], "InkOrder"),
                                    FountainSolution = DataReader.ReadString(dsProject.Tables[1].Rows[0], "FountainSolution"),
                                    Blanket = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Blanket"),
                                    BlanketAge = DataReader.ReadString(dsProject.Tables[1].Rows[0], "BlanketAge"),
                                    Screening = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Screening"),
                                    Coating = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Coating")
                                };
                        }
                        if (dsProject.Tables[2].Rows.Count > 0)
                        {
                            project.MeasurementSetting =
                                new MeasurementSetting()
                                {
                                    ObserverType = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "ObserverType"),
                                    Illuminant = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "Illuminant"),
                                    UVMode = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "UVMode"),
                                    MeasurementCount = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "MeasurementCount"),
                                    ReadMode = DataReader.ReadString(dsProject.Tables[2].Rows[0], "ReadMode"),
                                    MeasurementDeviceId =
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "MeasurementDeviceID"),
                                    InstrumentTypeId =
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "InstrumentTypeID"),
                                    ReferenceChartType = new KeyValuePair<short, string>(
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "ReferenceChartTypeID"),
                                            DataReader.ReadString(dsProject.Tables[2].Rows[0], "ChartName")),
                                    VMeasurementDeviceID = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "VMeasurementDeviceID"),
                                    OptReferenceChartType = new KeyValuePair<short, string>(
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "OPTReferenceChartTypeID"),
                                            DataReader.ReadString(dsProject.Tables[2].Rows[0], "OPTChartName")),
                                    VReferenceChartType = new KeyValuePair<short, string>(
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "VReferenceChartTypeID"),
                                        DataReader.ReadString(dsProject.Tables[2].Rows[0], "VChartName")),
                                    MetricTargetPreset = new KeyValuePair<int, string>(
                                                DataReader.ReadInt(dsProject.Tables[2].Rows[0], "MetricTargetPresetID"),
                                                DataReader.ReadString(dsProject.Tables[2].Rows[0], "MetricTargetPresetName")),
                                    dEMethod = DataReader.ReadString(dsProject.Tables[2].Rows[0], "dEMethod"),

                                    VUVMode = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "VUVMode"),
                                    VMeasurementCount = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "VMeasurementCount"),
                                    VReadMode = DataReader.ReadString(dsProject.Tables[2].Rows[0], "VReadMode"),
                                    PresetWeighted = DataReader.ReadBoolean(dsProject.Tables[2].Rows[0], "Weighted")
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
                                RunName =
                                                    isVerification
                                                        ? "Validation"
                                                        : ((EOptimizeRunStatus)DataReader.ReadInt(x, "Status")) == EOptimizeRunStatus.Closed
                                                              ? (GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("DLPText") + " " +
                                                                 Convert.ToDateTime(DataReader.ReadDate(x, "EndDateTime")).ToString("MMdd") + " " +
                                                                 Convert.ToDateTime(DataReader.ReadDate(x, "EndDateTime")).ToString("HH:mm"))
                                                              : ((EOptimizeRunStatus)DataReader.ReadInt(x, "Status")) == EOptimizeRunStatus.Open
                                                                    ? GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("OpenStatusText")
                                                                    : GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString(
                                                                        "CertificationStatusText"),
                                ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "Status"),
                                StartDateTime = DataReader.ReadDate(x, "StartDateTime"),
                                EndDateTime = DataReader.ReadDate(x, "EndDateTime"),
                                MasterRunId = DataReader.ReadInt(x, "MasterRunID"),
                                HasMeasurements = (DataReader.ReadInt(x, "MeasurementCount") > 0),
                                HasProfile = DataReader.ReadBoolean(x, "HasProfile"),
                                ReferenceChartType =
                                                    new KeyValuePair<short, string>(DataReader.ReadShort(x, "ReferenceChartTypeId"),
                                                                                    DataReader.ReadString(x, "ChartName")),
                                DeviceLinkProfile = DataReader.ReadByteArray(x, "DeviceLinkProfile")
                            }).ToList();

                        if (!isVerification)
                        {
                            int index = 0;
                            string LastDLPName = "";
                            project.OptimizeRuns.ForEach(x =>
                                                             {
                                                                 if (index > 0)
                                                                 {
                                                                     LastDLPName = project.ProjectName + " DLP" +
                                                                                   project.OptimizeRuns[index - 1].EndDateTime.Value.Month.ToString("00") +
                                                                                   project.OptimizeRuns[index - 1].EndDateTime.Value.Day.ToString("00") +
                                                                                   project.OptimizeRuns[index - 1].EndDateTime.Value.Hour.ToString("00") +
                                                                                   project.OptimizeRuns[index - 1].EndDateTime.Value.Minute.ToString("00") +
                                                                                   ".icc";
                                                                     if (index > 1)
                                                                     {
                                                                         if (project.OptimizeRuns[index - 1].Status == EOptimizeRunStatus.Certify)
                                                                         {
                                                                             LastDLPName = project.ProjectName + " DLP" +
                                                                                           project.OptimizeRuns[index - 2].EndDateTime.Value.Month.ToString("00") +
                                                                                           project.OptimizeRuns[index - 2].EndDateTime.Value.Day.ToString("00") +
                                                                                           project.OptimizeRuns[index - 2].EndDateTime.Value.Hour.ToString("00") +
                                                                                           project.OptimizeRuns[index - 2].EndDateTime.Value.Minute.ToString(
                                                                                               "00") + ".icc";
                                                                             //----If last run is certify run, then get DLP of its previous run----//
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
                        }
                        if (isVerification)
                        {
                            project.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        }
                        else
                        {
                            project.CurrentDisplayStatus =
                                project.OptimizeRuns.Count() == 1
                                    ? ECurrentProjectStatus.New
                                    : ECurrentProjectStatus.Run;
                        }
                        if (project.LicenseType == ELicenseType.Sync1D)
                        {
                            //----Select ProjectTargetLAB-----------//
                            if (dsProject.Tables[5].Rows.Count > 0)
                            {
                                project.ProjectTargetLAB = dsProject.Tables[5].AsEnumerable().
                                    Select(x => new Entity.CPSync1D.ProjectTargetLAB()
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
                            //----Select ProjectTargetTVI-----------//
                            if (dsProject.Tables[6].Rows.Count > 0)
                            {
                                project.PrintStandardTVI = dsProject.Tables[6].AsEnumerable().
                                    Select(x => new Entity.CPSync1D.PrintStandardTVI()
                                    {
                                        PatchType = new PatchType()
                                        {
                                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                            Type = DataReader.ReadString(x, "PatchType")
                                        },
                                        TVI = DataReader.ReadDecimal(x, "TVI")
                                    }).ToList();
                            }
                            //----Select ProjectTargetDensity-----------//
                            if (dsProject.Tables[7].Rows.Count > 0)
                            {
                                project.ProjectTargetDensity = dsProject.Tables[7].AsEnumerable().
                                    Select(x => new Entity.CPSync1D.ProjectTargetDensity()
                                    {
                                        PatchType = new PatchType()
                                        {
                                            PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                            Type = DataReader.ReadString(x, "PatchType")
                                        },
                                        Target = DataReader.ReadDecimal(x, "Target")
                                    }).ToList();
                            }
                        }
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
                                LicenseType = (ELicenseType)DataReader.ReadInt(x, "LicenseTypeID"),
                                TotalRuns = DataReader.ReadInt(x, "TotalRuns"),
                                //HasDLP = DataReader.ReadBoolean(x, "HasDLP"),
                                SourceProfile = new SourceProfile()
                                {
                                    SourceProfileId = DataReader.ReadShort(x, "SourceProfileId"),
                                    ProfileName = DataReader.ReadString(x, "ProfileName")
                                },
                                PrintStandardType = new Entity.CPSync1D.PrintStandardType()
                                {
                                    PrintStandardTypeID = DataReader.ReadShort(x, "PrintStandardTypeID"),
                                    PrintStandard = DataReader.ReadString(x, "PrintStandard"),
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
                                                                               DataReader.ReadDate(x, "EndDateTime").Month.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Day.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Hour.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Minute.ToString("00")),
                                EnabledValidation = DataReader.ReadBoolean(x, "EnabledValidation"),
                                ValidationStatus = (ValidationStatus)DataReader.ReadInt(x, "ValidationStatus"),
                                HasSpotProject = DataReader.ReadBoolean(x, "HasSpotProject")
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

        public List<Entity.CPSync1D.PrintStandardType> GetPrintStandardTypeList(int currentUserId, bool isSync1DType, int TargetPrintCategory = 0)
        {
            var dstSourceList = new List<Entity.CPSync1D.PrintStandardType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSourceList =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "[CP].[GetPrintStandardTypeList]",
                                new object[] { currentUserId, isSync1DType, TargetPrintCategory });
                    if (dsSourceList != null && dsSourceList.Tables.Count > 0)
                    {
                        dstSourceList = (dsSourceList.Tables[0].AsEnumerable().Select(
                            source => new Entity.CPSync1D.PrintStandardType()
                            {
                                SourceProfile = new SourceProfile()
                                {
                                    SourceProfileId = DataReader.ReadShort(source, "SourceProfileId"),
                                    ProfileName = DataReader.ReadString(source, "ProfileName"),
                                    ProfileDesc = DataReader.ReadString(source, "ProfileDesc"),
                                },
                                PrintStandard = DataReader.ReadString(source, "PrintStandard"),
                                PrintStandardTypeID = DataReader.ReadShort(source, "PrintStandardTypeID")
                            })).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfileList"));
            return dstSourceList;
        }

        public List<DeviceFamilyProfilerOption> GetDeviceFamilyProfilerOptionPreset(short deviceFamilyId, short LicenseTypeId)
        {
            var presetList = new List<DeviceFamilyProfilerOption>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPresetList = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetDeviceFamilyProfilerOption", new object[] { deviceFamilyId, LicenseTypeId });
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

        public int SaveProject(Project project)
        {
            int savedProjectId = 0;
            if (project.ProjectId > 0)
            {
                #region Update Project
                Common.ImplementBusinessLogic(
                    () =>
                    {
                        var projectOptions = new XElement("ProjectOptions", "");
                        if (project.ProjectOption != null && project.ProjectOption.Count > 0)
                        {
                            projectOptions = new XElement("ProjectOptions",
                                                          project.ProjectOption.
                                                              Select(x =>
                                                                     new XElement("ProjectOption",
                                                                                  new XAttribute("ProfileTypeID", x.ProfileTypeId),
                                                                                  new XAttribute("ProfilerOptionID", x.ProfilerOptionId),
                                                                                  new XAttribute("ProfilerOptionValue", x.ProfilerOptionValue)
                                                                         )).ToList());
                        }

                        var projectTargetTVI = new XElement("ProjectTargetTVI", "");
                        if (project.PrintStandardTVI != null && project.PrintStandardTVI.Count > 0)
                        {
                            projectTargetTVI = new XElement("ProjectTargetTVI",
                                                           project.PrintStandardTVI.
                                                               Select(x => new XElement("TargetTVI",
                                                                                   new XAttribute("PatchTypeID", x.PatchType.PatchTypeId),
                                                                                   new XAttribute("TVI", x.TVI))).ToList());
                        }

                        var projectTargetLAB = new XElement("ProjectTargetLAB", "");
                        if (project.ProjectTargetLAB != null && project.ProjectTargetLAB.Count > 0)
                        {
                            projectTargetLAB = new XElement("ProjectTargetLAB", project.ProjectTargetLAB.Select(x => new XElement("TargetLAB",
                                                                                    new XAttribute("PatchTypeID", x.PatchType.PatchTypeId),
                                                                                    new XAttribute("TargetL", x.TargetL),
                                                                                    new XAttribute("TargetA", x.TargetA),
                                                                                    new XAttribute("TargetB", x.TargetB))).ToList());
                        }

                        var projectTargetDensity = new XElement("ProjectTargetDensity", "");
                        if (project.ProjectTargetDensity != null && project.ProjectTargetDensity.Count > 0)
                        {
                            projectTargetDensity = new XElement("ProjectTargetDensity", project.ProjectTargetDensity.Select(x => new XElement("TargetDensity",
                                                                                    new XAttribute("PatchTypeID", x.PatchType.PatchTypeId),
                                                                                    new XAttribute("Target", x.Target))).ToList());
                        }

                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSync4D.UpdateProject",
                                             new object[]
                                                 {
                                                     project.ProjectId,
                                                     project.PrintStandardType.PrintStandardTypeID,
                                                     project.ProjectName,
                                                     project.PrintCondition.Media,
                                                     project.PrintCondition.Colorant,
                                                     project.PrintCondition.RipSettings,
                                                     project.PrintCondition.TechnicianName,
                                                     project.PrintCondition.Notes,
                                                     project.PrintCondition.InkOrder,
                                                     project.PrintCondition.FountainSolution,
                                                     project.PrintCondition.Blanket,
                                                     project.PrintCondition.BlanketAge,
                                                     project.PrintCondition.Screening,
                                                     project.PrintCondition.Coating,
                                                     project.MeasurementSetting.ObserverType,
                                                     project.MeasurementSetting.Illuminant,
                                                     (project.MeasurementSetting.UVMode == -1 ? 2 : project.MeasurementSetting.UVMode),  //---For older project who doen't have UVMode, so by default it will pass -1 because of NULL, so in that case make it 2 "UVCut"
                                                     (project.MeasurementSetting.MeasurementCount == -1 ? 0 : project.MeasurementSetting.MeasurementCount), //---For older project who doen't have MeasurementCount, so by default it will pass -1 because of NULL, so in that case make it 0
                                                     project.MeasurementSetting.ReadMode,
                                                     project.MeasurementSetting.MeasurementDeviceId,
                                                     project.MeasurementSetting.ReferenceChartType.Key,
                                                     project.DeviceFamilyProfilerOptionId,
                                                     projectOptions.ToString(),
                                                     project.MeasurementSetting.VMeasurementDeviceID,
                                                     project.MeasurementSetting.OptReferenceChartType.Key,
                                                     project.MeasurementSetting.VReferenceChartType.Key,
                                                     project.EnabledValidation,
                                                     project.MeasurementSetting.MetricTargetPreset.Key,
                                                     projectTargetTVI.ToString(),
                                                     project.MetricTargetPresetId,
                                                     projectTargetLAB.ToString(),
                                                     projectTargetDensity.ToString(),
                                                     project.ProjectTypeId,
                                                     (project.MeasurementSetting.VUVMode == -1 ? 2 : project.MeasurementSetting.VUVMode),  //---For older project who doen't have VUVMode, so by default it will pass -1 because of NULL, so in that case make it 2 "UVCut"
                                                     (project.MeasurementSetting.VMeasurementCount == -1 ? 0 : project.MeasurementSetting.VMeasurementCount), //---For older project who doen't have MeasurementCount, so by default it will pass -1 because of NULL, so in that case make it 0
                                                     project.MeasurementSetting.VReadMode,
                                                     Convert.ToInt16(project.DensityType),
                                                     Convert.ToInt16(project.PCSDataTypes),
                                                     project.ReTargetProjectID,
                                                     project.EnabledPaperScaling,
                                                     project.NewPaper_White_L,
                                                     project.NewPaper_White_a,
                                                     project.NewPaper_White_b,
                                                     project.NewPaper_Black_L,
                                                     project.NewPaper_Black_a,
                                                     project.NewPaper_Black_b
                                                 });
                        savedProjectId = project.ProjectId;
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
                        var projectOptions = new XElement("ProjectOptions", "");
                        if (project.ProjectOption != null && project.ProjectOption.Count > 0)
                        {
                            projectOptions = new XElement("ProjectOptions",
                                                              project.ProjectOption.
                                                                  Select(x =>
                                                                         new XElement("ProjectOption",
                                                                                      new XAttribute("ProfileTypeID", x.ProfileTypeId),
                                                                                      new XAttribute("ProfilerOptionID", x.ProfilerOptionId),
                                                                                      new XAttribute("ProfilerOptionValue", x.ProfilerOptionValue)
                                                                             )).ToList());
                        }

                        var projectTargetTVI = new XElement("ProjectTargetTVI", "");
                        if (project.PrintStandardTVI != null && project.PrintStandardTVI.Count > 0)
                        {
                            projectTargetTVI = new XElement("ProjectTargetTVI",
                                                                project.PrintStandardTVI.
                                                                    Select(x => new XElement("TargetTVI",
                                                                                        new XAttribute("PatchTypeID", x.PatchType.PatchTypeId),
                                                                                        new XAttribute("TVI", x.TVI))).ToList());
                        }

                        var projectTargetLAB = new XElement("ProjectTargetLAB", "");
                        if (project.ProjectTargetLAB != null && project.ProjectTargetLAB.Count > 0)
                        {
                            projectTargetLAB = new XElement("ProjectTargetLAB", project.ProjectTargetLAB.Select(x => new XElement("TargetLAB",
                                                                                    new XAttribute("PatchTypeID", x.PatchType.PatchTypeId),
                                                                                    new XAttribute("TargetL", x.TargetL),
                                                                                    new XAttribute("TargetA", x.TargetA),
                                                                                    new XAttribute("TargetB", x.TargetB))).ToList());

                        }

                        var projectTargetDensity = new XElement("ProjectTargetDensity", "");
                        if (project.ProjectTargetDensity != null && project.ProjectTargetDensity.Count > 0)
                        {
                            projectTargetDensity = new XElement("ProjectTargetDensity", project.ProjectTargetDensity.Select(x => new XElement("TargetDensity",
                                                                                    new XAttribute("PatchTypeID", x.PatchType.PatchTypeId),
                                                                                    new XAttribute("Target", x.Target))).ToList());
                        }

                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSync4D.CreateProject",
                                             new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@CPDeviceID", Type = DbType.Int32, Value = project.DeviceId},
                                                     new Parameters() {ParameterName = "@PrintStandardTypeID", Type = DbType.Int32, Value = project.PrintStandardType.PrintStandardTypeID},
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
                                                     new Parameters() {ParameterName = "@TechnicalName", Size = 30, Type = DbType.String, Value = project.PrintCondition.TechnicianName},
                                                     new Parameters() {ParameterName = "@Notes", Size = 500, Type = DbType.String, Value = project.PrintCondition.Notes},

                                                     new Parameters() {ParameterName = "@InkOrder", Type = DbType.Int32, Value = (int)project.PrintCondition.InkOrder},
                                                     new Parameters() {ParameterName = "@FountainSolution", Size = 100, Type = DbType.String, Value = project.PrintCondition.FountainSolution},
                                                     new Parameters() {ParameterName = "@Blanket", Size = 100, Type = DbType.String, Value = project.PrintCondition.Blanket},
                                                     new Parameters() {ParameterName = "@BlanketAge", Size = 100, Type = DbType.String, Value = project.PrintCondition.BlanketAge},
                                                     new Parameters() {ParameterName = "@Screening", Size = 100, Type = DbType.String, Value = project.PrintCondition.Screening},
                                                     new Parameters() {ParameterName = "@Coating", Size = 100, Type = DbType.String, Value = project.PrintCondition.Coating},

                                                     new Parameters() {ParameterName = "@ObserverType", Type = DbType.Int16, Value = project.MeasurementSetting.ObserverType},
                                                     new Parameters() {ParameterName = "@Illuminant", Type = DbType.Int16, Value = project.MeasurementSetting.Illuminant},
                                                     new Parameters() {ParameterName = "@UVMode", Type = DbType.Int16, Value = project.MeasurementSetting.UVMode},
                                                     new Parameters() {ParameterName = "@MeasurementCount", Type = DbType.Int16, Value = project.MeasurementSetting.MeasurementCount},
                                                     new Parameters() {ParameterName = "@ReadMode", Type = DbType.String, Value = project.MeasurementSetting.ReadMode},

                                                     new Parameters()
                                                         {ParameterName = "@MeasurementDeviceID", Type = DbType.Int16, Value = project.MeasurementSetting.MeasurementDeviceId},
                                                     new Parameters()
                                                         {ParameterName = "@ReferenceChartTypeID", Type = DbType.Int16, Value = project.MeasurementSetting.ReferenceChartType.Key},
                                                     new Parameters() {ParameterName = "@Status", Type = DbType.Int32, Value = Convert.ToInt32(EOptimizeRunStatus.Open)},
                                                     new Parameters() {ParameterName = "@StartDateTime", Type = DbType.DateTime, Value = DateTime.Now},
                                                     new Parameters() {ParameterName = "@DeviceFamilyProfilerOptionId", Type = DbType.Int32, Value = project.DeviceFamilyProfilerOptionId},
                                                     new Parameters() {ParameterName = "@ProjectOptions", Value = projectOptions.ToString()},
                                                     new Parameters() {ParameterName = "@LicenseTypeID", Type = DbType.Int32, Value = (int)project.LicenseType},
                                                     new Parameters() {ParameterName = "@VMeasurementDeviceID",Type=DbType.Int16,Value = project.MeasurementSetting.VMeasurementDeviceID},
                                                     new Parameters() {ParameterName = "@OPTReferenceChartTypeID", Type=DbType.Int16, Value = project.MeasurementSetting.OptReferenceChartType.Key},
                                                     new Parameters() {ParameterName ="@VReferenceChartTypeID", Type=DbType.Int16,Value = project.MeasurementSetting.VReferenceChartType.Key },
                                                     new Parameters(){ ParameterName = "@EnabledValidation", Type=DbType.Boolean,Value=project.EnabledValidation},
                                                     new Parameters() { ParameterName  = "@MetricTargetPresetID", Type = DbType.Int32, Value = project.MeasurementSetting.MetricTargetPreset.Key},
                                                     new Parameters() { ParameterName  = "@MetricTargetPresetOptionID", Type = DbType.Int16, Value = project.MetricTargetPresetId},
                                                     new Parameters(){ ParameterName="@ProjectTargetTVI", Value = projectTargetTVI.ToString() },
                                                     new Parameters() {ParameterName = "@ProjectTargetLAB", Value = projectTargetLAB.ToString() },
                                                     new Parameters() {ParameterName = "@ProjectTargetDensity", Value = projectTargetDensity.ToString() },
                                                     new Parameters() { ParameterName = "@ProjectTypeId" ,Value = project.ProjectTypeId },

                                                     new Parameters() {ParameterName = "@VUVMode", Type = DbType.Int16, Value = project.MeasurementSetting.VUVMode},
                                                     new Parameters() {ParameterName = "@VMeasurementCount", Type = DbType.Int16, Value = project.MeasurementSetting.VMeasurementCount},
                                                     new Parameters() {ParameterName = "@VReadMode", Type = DbType.String, Value = project.MeasurementSetting.VReadMode},
                                                     new Parameters() {ParameterName = "@DensityType", Type = DbType.Int16, Value = Convert.ToInt16(project.DensityType)},
                                                     new Parameters() {ParameterName = "@PCSDataType", Type = DbType.Int16, Value = Convert.ToInt16(project.PCSDataTypes)},
                                                     new Parameters() {ParameterName = "@ReTargetProjectID", Type = DbType.Int32, Value = project.ReTargetProjectID},

                                                     new Parameters() {ParameterName = "@EnabledPaperScaling", Type = DbType.Boolean, Value = project.EnabledPaperScaling},
                                                     new Parameters() {ParameterName = "@NewPaper_White_L", Type = DbType.Decimal, Value = project.NewPaper_White_L},
                                                     new Parameters() {ParameterName = "@NewPaper_White_a", Type = DbType.Decimal, Value = project.NewPaper_White_a},
                                                     new Parameters() {ParameterName = "@NewPaper_White_b", Type = DbType.Decimal, Value = project.NewPaper_White_b},
                                                     new Parameters() {ParameterName = "@NewPaper_Black_L", Type = DbType.Decimal, Value = project.NewPaper_Black_L},
                                                     new Parameters() {ParameterName = "@NewPaper_Black_a", Type = DbType.Decimal, Value = project.NewPaper_Black_a},
                                                     new Parameters() {ParameterName = "@NewPaper_Black_b", Type = DbType.Decimal, Value = project.NewPaper_Black_b}

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
                    savedProjectId = projectId;
                }
                #endregion
            }
            return savedProjectId;
        }

        public ReferenceChartData GetReferenceChartData(int projectId, EReferenceChartDisType referenceChartDisType)
        {
            var chartData = new ReferenceChartData();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsReferenceChart = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[GetReferenceChart]",
                                                                                  new object[] { projectId, referenceChartDisType });
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
                        var UVMode = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "UVMode");
                        var DensityType = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "DensityType");

                        chartData.DeviceSetting = new MeasurementDevice
                        {
                            SerialNumber = chartData.MeasureDeviceSrNo,
                            MeasurementSettings = new MeasurementSetting()
                            {
                                IlluminantName =
                                                                                            DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "IlluminantType"),
                                ObserverTypeName =
                                                                                            Utilities.GetValueFromDescription<EObserverType>(
                                                                                                DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ObserverType")),
                                UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                UVMode = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "UVMode"),
                                ReadMode = DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ReadMode"),
                                MeasurementCount = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "MeasurementCount"),
                                DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
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

                            //For IO devices...
                            if (dsChartSettings.Tables[0].Columns.Contains("FirstSample"))
                                chartData.ChartSetting.FirstSample = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "FirstSample");
                            if (dsChartSettings.Tables[0].Columns.Contains("ReadDirection"))
                                chartData.ChartSetting.ReadDirection = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadDirection");
                            if (dsChartSettings.Tables[0].Columns.Contains("ReadMode"))
                                chartData.ChartSetting.ReadMode = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadMode");
                            if (dsChartSettings.Tables[0].Columns.Contains("RowsPerPage"))
                                chartData.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "RowsPerPage");
                            if (dsChartSettings.Tables[0].Columns.Contains("Pages"))
                                chartData.ChartSetting.Pages = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "Pages");
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

        public Guid StoreTemporaryValidationInformation(string jobName, string customer, string pageOrSurface, string jdfJobId, int projectId, Guid tempValidationInfoId, string silverlightPostUrl)
        {
            var outParam = new Parameters() { ParameterName = "@TempValidationInfoId", Size = 16, Type = DbType.Guid };
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure("[CPSyncV].[GetTempValidationInfoGuid]",
                                           new List<Parameters>()
                                               {
                                                   new Parameters() {ParameterName = "@JobName", Type = DbType.String, Size = 50, Value = jobName},
                                                   new Parameters() {ParameterName = "@Customer", Type = DbType.String, Size = 50, Value = customer},
                                                   new Parameters() {ParameterName = "@PageOrSurface", Type = DbType.String, Size = 50, Value = pageOrSurface},
                                                   new Parameters() {ParameterName = "@JDFJobId", Type = DbType.String, Size = 50, Value = jdfJobId},
                                                   new Parameters() {ParameterName = "@ProjectId", Type = DbType.Int32, Size = 4, Value = projectId},
                                                   new Parameters() {ParameterName = "@SilverlightPostUrl", Type = DbType.String, Size = 200, Value = silverlightPostUrl},
                                                   new Parameters() {ParameterName = "@TempValidationInfoIdIN", Type = DbType.Guid, Size = 16, Value = tempValidationInfoId},
                                               },
                                           new List<Parameters>()
                                               {
                                                   outParam
                                               }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_StoreTemporaryValidationInformation"));
            if (outParam.Value != null)
            {
                tempValidationInfoId = Guid.Parse(outParam.Value.ToString());
            }
            return tempValidationInfoId;
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

        public Entity.CPSyncV.TempValidationInfo GetTempValidationInfo(Guid tempValidationId)
        {
            var validationInfo = new Entity.CPSyncV.TempValidationInfo();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectInfo = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetTempValidationInfo]", new object[] { tempValidationId });
                    if (dsProjectInfo.Tables[0].Rows.Count > 0)
                    {
                        validationInfo.TempValidationId = tempValidationId;
                        validationInfo.JobName = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "JobName");
                        validationInfo.Customer = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "Customer");
                        validationInfo.PageOrSurface = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "PageOrSurface");
                        validationInfo.JDFJobId = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "JDFJobId");
                        validationInfo.ProjectId = DataReader.ReadInt(dsProjectInfo.Tables[0].Rows[0], "ProjectId");
                        validationInfo.SilverlightPostUrl = DataReader.ReadString(dsProjectInfo.Tables[0].Rows[0], "SilverlightPostUrl");
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetTempValidationInfo"));
            return validationInfo;
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

        public List<MeasurementDevice> GetMeasurementDeviceList(int loginId, int deviceId, bool isSpotTargetType = false)
        {
            var measurementDevice = new List<MeasurementDevice>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurementDevice = DataAccessFactory.Instance.GetDsResult("CP.GetMeasurementDeviceList",
                                                                                     new object[] { loginId, deviceId, isSpotTargetType });
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

        public List<ReferenceChartType> GetReferenceChartTypeList(int measurementDeviceId, int ChartDisplayType, int ProjectTypeId, int MetricTargetPresetID = 0, int InstrumentTypeID = 0)
        {
            var presetList = new List<ReferenceChartType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPresetList = DataAccessFactory.Instance.GetDsResult("CP.GetReferenceChartTypeList", new object[] { measurementDeviceId, ChartDisplayType, ProjectTypeId, MetricTargetPresetID, InstrumentTypeID });
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

        public Dictionary<string, string> GetPrintConditionPreviewData(short MeasurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId, short V_measurementDeviceId, short Opt_ReferenceChartTypeId, short V_ReferenceChartTypeId, short metricTargetPresetId, int ReTargetProjectId)
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
                                                                                           deviceFamilyProfilerOptionId,
                                                                                           V_measurementDeviceId,
                                                                                           Opt_ReferenceChartTypeId,
                                                                                           V_ReferenceChartTypeId,
                                                                                           metricTargetPresetId,
                                                                                           ReTargetProjectId
                                                                                       });

                    if (dsPreviewData.Tables[0].Rows.Count > 0)
                    {
                        data.Add("MeasurementDeviceId", dsPreviewData.Tables[0].Rows[0]["MeasurementDevice"].ToString());
                    }
                    if (dsPreviewData.Tables[1].Rows.Count > 0)
                    {
                        data.Add("V_MeasurementDeviceId", dsPreviewData.Tables[1].Rows[0]["V_MeasurementDevice"].ToString());
                    }
                    if (dsPreviewData.Tables[2].Rows.Count > 0)
                    {
                        data.Add("illuminantId", dsPreviewData.Tables[2].Rows[0]["IlluminantType"].ToString());
                    }
                    if (dsPreviewData.Tables[3].Rows.Count > 0)
                    {
                        data.Add("observerTypeId", dsPreviewData.Tables[3].Rows[0]["ObserverType"].ToString());
                    }
                    if (dsPreviewData.Tables[4].Rows.Count > 0)
                    {
                        data.Add("referenceChartTypeId", dsPreviewData.Tables[4].Rows[0]["ChartName"].ToString());
                    }
                    if (dsPreviewData.Tables[5].Rows.Count > 0)
                    {
                        data.Add("deviceFamilyProfilerOptionId", dsPreviewData.Tables[5].Rows[0]["ProfilerOptionsName"].ToString());
                        data.Add("deviceFamilyProfilerOptionDesc", dsPreviewData.Tables[5].Rows[0]["ProfilerOptionsDesc"].ToString());
                    }
                    if (dsPreviewData.Tables[6].Rows.Count > 0)
                    {
                        data.Add("opt_referenceChartTypeId", dsPreviewData.Tables[6].Rows[0]["OptimizationChart"].ToString());
                    }
                    if (dsPreviewData.Tables[7].Rows.Count > 0)
                    {
                        data.Add("v_referenceChartTypeId", dsPreviewData.Tables[7].Rows[0]["ValidationChart"].ToString());
                    }
                    if (dsPreviewData.Tables[8].Rows.Count > 0)
                    {
                        data.Add("metricTargetPresetName", dsPreviewData.Tables[8].Rows[0]["MetricTargetPresetName"].ToString());
                        data.Add("metricTargetPresetDesc", dsPreviewData.Tables[8].Rows[0]["MetricTargetPresetDesc"].ToString());
                    }
                    if (dsPreviewData.Tables[9].Rows.Count > 0)
                    {
                        data.Add("ReTargetProject", dsPreviewData.Tables[9].Rows[0]["ProjectName"].ToString());
                    }
                    else
                    {
                        data.Add("ReTargetProject", "");
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPrintConditionPreviewData"));
            return data;
        }

        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPresetList(int currentUserId, int ProjectTypeId, bool isVerification = false)
        {
            List<Entity.CPSyncV.MetricTargetPreset> metricPresetList = new List<Entity.CPSyncV.MetricTargetPreset>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    string procedureName = "[CPSync].[GetMetricTargetPresetList]";

                    if (isVerification)
                        procedureName = "[CPSyncV].[GetMetricTargetPresetList]";

                    var dsMetricTargetPresetList = DataAccessFactory.Instance.GetDsResult(procedureName, new object[] { currentUserId, ProjectTypeId });

                    if (dsMetricTargetPresetList != null && dsMetricTargetPresetList.Tables.Count > 0)
                    {
                        metricPresetList = dsMetricTargetPresetList.Tables[0].AsEnumerable().Select(
                        x => new Entity.CPSyncV.MetricTargetPreset()
                        {
                            MetricTargetPresetID = DataReader.ReadInt(x, "MetricTargetPresetID"),
                            MetricTargetPresetName = DataReader.ReadString(x, "MetricTargetPresetName"),
                            MetricTargetPresetDesc = DataReader.ReadString(x, "MetricTargetPresetDesc")
                        }).ToList();
                    }

                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricTargetPresetList"));
            return metricPresetList;
        }

        public List<Project> GetProjectsForImportMeasurement(int deviceId, int LicenseTypeID, int CurrentProjectID)
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
                                LicenseType = (ELicenseType)DataReader.ReadInt(x, "LicenseTypeID"),
                                TotalRuns = DataReader.ReadInt(x, "TotalRuns"),
                                HasDLP = DataReader.ReadBoolean(x, "HasDLP"),
                                SourceProfile = new SourceProfile()
                                {
                                    SourceProfileId = DataReader.ReadShort(x, "SourceProfileId"),
                                    ProfileName = DataReader.ReadString(x, "ProfileName")
                                },
                                PrintStandardType = new Entity.CPSync1D.PrintStandardType()
                                {
                                    PrintStandardTypeID = DataReader.ReadShort(x, "PrintStandardTypeID"),
                                    PrintStandard = DataReader.ReadString(x, "PrintStandard"),
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
                                                               DataReader.ReadDate(x, "EndDateTime").Month.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Day.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Hour.ToString("00") + DataReader.ReadDate(x, "EndDateTime").Minute.ToString("00")),
                                MetricTargetPresetId = DataReader.ReadInt(x, "MetricTargetPresetId")

                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetProjectsByDeviceId"));
            return objProjectList;
        }

        public List<int> GetMeasurementIDsbyProjectID(int deviceId, int LicenseTypeID, int ProjectID)
        {
            var objMeasurementIDList = new List<int>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectList =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSync4D.GetMeasurementIDsByProjectID",
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

        public int ImportMeasurements(int MeasurementID, int OptimizeRunID)
        {
            var returnValue = 0;

            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CPSync4D].[ImportMeasurements]",
                            new object[]
                                    {
                                        MeasurementID, OptimizeRunID
                                    });

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_ImportMeasurements"));
            return returnValue;
        }

        public List<Entity.CPSyncV.MetricSum> GetMetricSumTargetValues(int metricTargetPresetId)
        {
            List<Entity.CPSyncV.MetricSum> metricSumList = new List<Entity.CPSyncV.MetricSum>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMetricTargetPresetList = DataAccessFactory.Instance.GetDsResult("[CPSyncV].[GetMetricSumTargetValues]", new object[] { metricTargetPresetId });

                    if (dsMetricTargetPresetList != null && dsMetricTargetPresetList.Tables.Count > 0)
                    {
                        metricSumList = dsMetricTargetPresetList.Tables[0].AsEnumerable().Select(
                        x => new Entity.CPSyncV.MetricSum()
                        {
                            MetricSumId = DataReader.ReadInt(x, "MetricSumId"),
                            MetricSumName = DataReader.ReadString(x, "MetricSumName"),
                            TargetValue = DataReader.ReadDecimal(x, "TargetValue")
                        }).ToList();
                    }

                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMetricSumTargetValues"));
            return metricSumList;
        }

        public List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVI(int printStandardTypeId)
        {
            List<Entity.CPSync1D.PrintStandardTVI> printStandardTVIList = new List<Entity.CPSync1D.PrintStandardTVI>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPrintStandardTVIList = DataAccessFactory.Instance.GetDsResult("[CPSync].[GetPrintStandardTVITargets]", new object[] { printStandardTypeId });
                    if (dsPrintStandardTVIList != null && dsPrintStandardTVIList.Tables.Count > 0)
                    {
                        printStandardTVIList = dsPrintStandardTVIList.Tables[0].AsEnumerable().Select(
                            x => new Entity.CPSync1D.PrintStandardTVI()
                            {
                                PatchType = new PatchType()
                                {
                                    PatchTypeId = DataReader.ReadShort(x, "PatchTypeId"),
                                    Type = DataReader.ReadString(x, "PatchType")
                                },
                                TVI = DataReader.ReadDecimal(x, "TVI")
                            }).ToList();
                    }
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPrintStandardTVI"));
            return printStandardTVIList;
        }

        public List<Entity.CPSync1D.ProjectTargetDensity> GetPrintStandardDensity(int printStandardTypeId)
        {
            List<Entity.CPSync1D.ProjectTargetDensity> projectTargetDensityList = new List<Entity.CPSync1D.ProjectTargetDensity>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPrintStandardDensityList = DataAccessFactory.Instance.GetDsResult("[CPSync].[GetDensityTargetsByPrintStandardTypeID]", new object[] { printStandardTypeId });
                    if (dsPrintStandardDensityList != null && dsPrintStandardDensityList.Tables.Count > 0)
                    {
                        projectTargetDensityList = dsPrintStandardDensityList.Tables[0].AsEnumerable().Select(
                            x => new Entity.CPSync1D.ProjectTargetDensity()
                            {
                                PatchType = new PatchType()
                                {
                                    PatchTypeId = DataReader.ReadShort(x, "PatchTypeId"),
                                    Type = DataReader.ReadString(x, "PatchType")
                                },
                                Target = DataReader.ReadDecimal(x, "Target")
                            }).ToList();
                    }
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPrintStandardDensity"));
            return projectTargetDensityList;
        }

        public List<Entity.CPSync1D.ProjectTargetLAB> GetProjectTargetLAB(int printStandardTypeId, int ProjectTypeId)
        {
            List<Entity.CPSync1D.ProjectTargetLAB> targetLABList = new List<Entity.CPSync1D.ProjectTargetLAB>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectTargetLABList = DataAccessFactory.Instance.GetDsResult("[CPSync].[GetLABTargetsByPrintStandardTypeID]", new object[] { printStandardTypeId, ProjectTypeId });
                    if (dsProjectTargetLABList != null && dsProjectTargetLABList.Tables.Count > 0)
                    {
                        targetLABList = dsProjectTargetLABList.Tables[0].AsEnumerable().Select(
                            x => new Entity.CPSync1D.ProjectTargetLAB()
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
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetProjectTargetLAB"));
            return targetLABList;
        }

        public byte[] Get1DLastOptimizedDlp(int projectId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSync.GetLastOptimizedDlp",
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

        public Dictionary<int, string> GetReTargetProjectList(int currentUserId, int ProjectTypeId, int DeviceId)
        {
            var dstReTargetProjectList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsReTargetProjectList = DataAccessFactory.Instance.GetDsResult("[CPSync].[GetReTargetProjectList]", new object[] { currentUserId, ProjectTypeId, DeviceId });
                    if (dsReTargetProjectList != null && dsReTargetProjectList.Tables.Count > 0)
                    {

                        dstReTargetProjectList = (dsReTargetProjectList.Tables[0].AsEnumerable().Select(
                            ReTargetProject => new
                            {
                                Value = DataReader.ReadInt(ReTargetProject, "ProjectID"),
                                Text = DataReader.ReadString(ReTargetProject, "ProjectName")
                            })).
                            ToDictionary(ReTargetProject => ReTargetProject.Value, ReTargetProject => ReTargetProject.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GeReTargetProjectList"));
            return dstReTargetProjectList;
        }

        public List<Entity.CPSpot.SpotProjectLibrary> GetSpotLibraryListByProjectId(int projectId)
        {
            var objProjectList = new List<Entity.CPSpot.SpotProjectLibrary>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProjectList =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSpot.GetSpotLibraryList",
                                new object[]
                                        {
                                            projectId
                                        });
                    if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
                    {
                        objProjectList = dsProjectList.Tables[0].AsEnumerable().
                            Select(x => new Entity.CPSpot.SpotProjectLibrary()
                            {
                                SpotProjectId = DataReader.ReadInt(x, "SpotProjectID"),
                                ProjectId = DataReader.ReadInt(x, "ProjectID"),
                                ProjectName = DataReader.ReadString(x, "ProjectName"),

                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),

                                // TotalRuns = DataReader.ReadInt(x, "TotalRuns"),

                                SpotTarget = new Entity.CPSpot.SpotTarget()
                                {
                                    SpotTargetID = DataReader.ReadShort(x, "SpotTargetID"),
                                    SpotTargetName = DataReader.ReadString(x, "SpotTargetName")
                                },

                                CreateDate = DataReader.ReadDate(x, "EndDateTime"),
                                TotalRuns = DataReader.ReadInt(x, "TotalRuns"),
                                HasColorProfile = DataReader.ReadBoolean(x, "HasColorProfile"),
                                LastOptmizeId = DataReader.ReadInt(x, "LastOptimizeID"),
                                Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "ProjStatus"),
                                isAllowSpotProject = DataReader.ReadBoolean(x, "isAllowSpotProject")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetProjectsByDeviceId"));
            return objProjectList;
        }

        public List<Entity.CPSpot.SpotTarget> GetSpotTargetList(int currentUserId, bool isSpotTargetType)
        {
            var dstSpotTargetList = new List<Entity.CPSpot.SpotTarget>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSpotTargetList =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "[CPSpot].[GetSpotTargetList]",
                                new object[] { currentUserId, isSpotTargetType });
                    if (dsSpotTargetList != null && dsSpotTargetList.Tables.Count > 0)
                    {
                        dstSpotTargetList = (dsSpotTargetList.Tables[0].AsEnumerable().Select(
                            spotTarget => new Entity.CPSpot.SpotTarget()
                            {
                                SpotTargetID = DataReader.ReadShort(spotTarget, "SpotTargetID"),
                                SpotTargetName = DataReader.ReadString(spotTarget, "SpotTargetName"),
                                MeasurementMode = DataReader.ReadShort(spotTarget, "MeasurementMode"),
                                Description = DataReader.ReadString(spotTarget, "Description")
                                //Company = new Company()
                                //{
                                //    CompanyId = DataReader.ReadShort(spotTarget, "CompanyID"),
                                //    CompanyName = DataReader.ReadString(spotTarget, "CompanyName")
                                //}
                            })).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotTargetList"));
            return dstSpotTargetList;
        }

        public int SaveSpotProject(Project project)
        {
            int savedProjectId = 0;
            if (project.ProjectId > 0 || project.SpotTargetProject.SpotProjectId > 0)
            {
                #region Update Project
                Common.ImplementBusinessLogic(
                    () =>
                    {
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSpot.UpdateSpotProject",
                                             new object[]
                                                 {
                                                     project.SpotTargetProject.SpotProjectId,
                                                     project.SpotTargetProject.SpotTarget.SpotTargetID,
                                                     project.SpotTargetProject.ProjectName,
                                                     project.SpotTargetProject.PrintCondition.TechnicianName,
                                                     project.SpotTargetProject.PrintCondition.Notes,
                                                     project.SpotTargetProject.PrintCondition.Media,
                                                     project.SpotTargetProject.PrintCondition.Colorant,
                                                     project.SpotTargetProject.PrintCondition.RipSettings,
                                                     project.MeasurementSetting.ObserverType,
                                                     project.MeasurementSetting.Illuminant,
                                                     (project.MeasurementSetting.UVMode == -1 ? 2 : project.MeasurementSetting.UVMode),  //---For older project who doen't have UVMode, so by default it will pass -1 because of NULL, so in that case make it 2 "UVCut"
                                                     (project.MeasurementSetting.MeasurementCount == -1 ? 0 : project.MeasurementSetting.MeasurementCount), //---For older project who doen't have MeasurementCount, so by default it will pass -1 because of NULL, so in that case make it 0
                                                     project.MeasurementSetting.ReadMode,
                                                     project.MeasurementSetting.MeasurementDeviceId

                                                 });
                        savedProjectId = project.SpotTargetProject.SpotProjectId;
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_SaveProject"));
                #endregion
            }
            else
            {
                #region Insert Project
                var outParam = new Parameters() { ParameterName = "@SpotProjectID", Type = DbType.Int32 };
                Common.ImplementBusinessLogic(
                    () =>
                    {
                        DataAccessFactory.
                            Instance.
                            ExecuteProcedure("CPSpot.CreateSpotProject",
                                             new List<Parameters>()
                                                 {
                                                     new Parameters() {ParameterName = "@CPDeviceID", Type = DbType.Int32, Value = project.DeviceId},
                                                     new Parameters() {ParameterName = "@SpotTargetID", Type = DbType.Int32, Value = project.SpotTargetProject.SpotTarget.SpotTargetID},
                                                     new Parameters() {ParameterName = "@ProjectName", Size = 30, Type = DbType.String, Value = project.SpotTargetProject.ProjectName},
                                                     new Parameters()
                                                         {
                                                             ParameterName = "@CreateDate",
                                                             Type = DbType.DateTime,
                                                             Value = project.CreateDate.AddMilliseconds(-project.CreateDate.Millisecond)
                                                         },
                                                     new Parameters() {ParameterName = "@TechnicalName", Size = 30, Type = DbType.String, Value = project.SpotTargetProject.PrintCondition.TechnicianName},
                                                     new Parameters() {ParameterName = "@Notes", Size = 500, Type = DbType.String, Value = project.SpotTargetProject.PrintCondition.Notes},
                                                     new Parameters() {ParameterName = "@Media", Size = 500, Type = DbType.String, Value = project.SpotTargetProject.PrintCondition.Media},
                                                     new Parameters() {ParameterName = "@Colorant", Size = 500, Type = DbType.String, Value = project.SpotTargetProject.PrintCondition.Colorant},
                                                     new Parameters() {ParameterName = "@RipSettings", Size = 500, Type = DbType.String, Value = project.SpotTargetProject.PrintCondition.RipSettings},


                                                     new Parameters() {ParameterName = "@ObserverType", Type = DbType.Int16, Value = project.MeasurementSetting.ObserverType},
                                                     new Parameters() {ParameterName = "@Illuminant", Type = DbType.Int16, Value = project.MeasurementSetting.Illuminant},
                                                     new Parameters() {ParameterName = "@UVMode", Type = DbType.Int16, Value = project.MeasurementSetting.UVMode},
                                                     new Parameters() {ParameterName = "@MeasurementCount", Type = DbType.Int16, Value = project.MeasurementSetting.MeasurementCount},
                                                     new Parameters() {ParameterName = "@ReadMode", Type = DbType.String, Value = project.MeasurementSetting.ReadMode},

                                                     new Parameters()
                                                         {ParameterName = "@MeasurementDeviceID", Type = DbType.Int16, Value = project.MeasurementSetting.MeasurementDeviceId},
                                                     new Parameters() {ParameterName = "@Status", Type = DbType.Int32, Value = Convert.ToInt32(EOptimizeRunStatus.Open)},
                                                     new Parameters() {ParameterName = "@StartDateTime", Type = DbType.DateTime, Value = DateTime.Now},
                                                     new Parameters() {ParameterName = "@LicenseTypeID", Type = DbType.Int32, Value = (int)project.LicenseType},
                                                     new Parameters() {ParameterName = "@MasterProjectID", Type = DbType.Int32, Value = project.SpotTargetProject.ProjectId}
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
                    savedProjectId = projectId;
                }
                #endregion
            }
            return savedProjectId;
        }

        public Project GetProjectDetail_SpotProject(int projectId)
        {
            var project = new Project();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsProject =
                        DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "CPSpot.GetSpotProjectDetail",
                                new object[]
                                        {
                                            projectId
                                        });
                    if (dsProject.Tables[0].Rows.Count > 0)
                    {
                        project = new Project()
                        {
                            DeviceId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "CPDeviceID"),
                            LicenseType = (ELicenseType)DataReader.ReadInt(dsProject.Tables[0].Rows[0], "LicenseTypeID"),
                        };

                        project.SpotTargetProject = new Entity.CPSpot.SpotProjectLibrary()
                        {
                            SpotProjectId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "SpotProjectID"),
                            ProjectId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "ProjectID"),
                            DeviceId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "CPDeviceID"),
                            ProjectName = DataReader.ReadString(dsProject.Tables[0].Rows[0], "ProjectName"),
                            CreateDate = DataReader.ReadDate(dsProject.Tables[0].Rows[0], "CreateDate"),
                            CurrDlpId = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "CurrDLPID"),
                            IsUsed = DataReader.ReadBoolean(dsProject.Tables[0].Rows[0], "IsUsed"),
                            Status = (EOptimizeRunStatus)DataReader.ReadInt(dsProject.Tables[0].Rows[0], "ProjStatus"),
                            SpotTarget = new Entity.CPSpot.SpotTarget()
                            {
                                SpotTargetID = DataReader.ReadShort(dsProject.Tables[0].Rows[0], "SpotTargetID"),
                                SpotTargetName = DataReader.ReadString(dsProject.Tables[0].Rows[0], "SpotTargetName"),
                                Description = DataReader.ReadString(dsProject.Tables[0].Rows[0], "Description"),
                                MeasurementMode = DataReader.ReadInt(dsProject.Tables[0].Rows[0], "MeasurementMode")
                            }
                        };
                        if (dsProject.Tables[1].Rows.Count > 0)
                        {
                            project.SpotTargetProject.PrintCondition =
                                new PrintCondition()
                                {
                                    TechnicianName = DataReader.ReadString(dsProject.Tables[1].Rows[0], "TechnicianName"),
                                    Notes = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Notes"),
                                    Media = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Media"),
                                    Colorant = DataReader.ReadString(dsProject.Tables[1].Rows[0], "Colorant"),
                                    RipSettings = DataReader.ReadString(dsProject.Tables[1].Rows[0], "RipSettings")
                                };
                        }
                        if (dsProject.Tables[2].Rows.Count > 0)
                        {
                            project.MeasurementSetting =
                                new MeasurementSetting()
                                {
                                    ObserverType = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "ObserverType"),
                                    Illuminant = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "Illuminant"),
                                    UVMode = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "UVMode"),
                                    MeasurementCount = DataReader.ReadShort(dsProject.Tables[2].Rows[0], "MeasurementCount"),
                                    ReadMode = DataReader.ReadString(dsProject.Tables[2].Rows[0], "ReadMode"),
                                    MeasurementDeviceId =
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "MeasurementDeviceID"),
                                    InstrumentTypeId =
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "InstrumentTypeID"),
                                    OptReferenceChartType = new KeyValuePair<short, string>(
                                            DataReader.ReadShort(dsProject.Tables[2].Rows[0], "OPTReferenceChartTypeID"),
                                            DataReader.ReadString(dsProject.Tables[2].Rows[0], "OPTChartName")),
                                };
                        }
                        project.SpotTargetProject.OptimizeRuns = dsProject.Tables[3].AsEnumerable().
                            Select(x => new OptimizeRun()
                            {
                                OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                                RunName = ((EOptimizeRunStatus)DataReader.ReadInt(x, "Status")) == EOptimizeRunStatus.Closed
                                                              ? (GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("NCPText") + " " +
                                                                 Convert.ToDateTime(DataReader.ReadDate(x, "EndDateTime")).ToString("MMdd") + " " +
                                                                 Convert.ToDateTime(DataReader.ReadDate(x, "EndDateTime")).ToString("HH:mm"))
                                                              : ((EOptimizeRunStatus)DataReader.ReadInt(x, "Status")) == EOptimizeRunStatus.Open
                                                                    ? GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("OpenStatusText")
                                                                    : GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString(
                                                                        "CertificationStatusText"),
                                ProjectId = DataReader.ReadInt(x, "SpotProjectID"),
                                Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "Status"),
                                StartDateTime = DataReader.ReadDate(x, "StartDateTime"),
                                EndDateTime = DataReader.ReadDate(x, "EndDateTime"),
                                MasterRunId = DataReader.ReadInt(x, "MasterRunID"),
                                HasMeasurements = (DataReader.ReadInt(x, "MeasurementCount") > 0),
                                HasProfile = DataReader.ReadBoolean(x, "HasProfile"),
                                NamedColorProfile = DataReader.ReadByteArray(x, "NamedColorProfile")
                                //ReferenceChartType =
                                //                    new KeyValuePair<short, string>(DataReader.ReadShort(x, "ReferenceChartTypeId"),
                                //                                                    DataReader.ReadString(x, "ChartName")),

                            }).ToList();


                        int index = 0;
                        string LastDLPName = "";
                        if (project.SpotTargetProject.OptimizeRuns.Count > 0)
                        {
                            project.SpotTargetProject.OptimizeRuns.ForEach(x =>
                            {
                                if (index > 0)
                                {
                                    LastDLPName = project.SpotTargetProject.ProjectName + " NCP" +
                                                  project.SpotTargetProject.OptimizeRuns[index - 1].EndDateTime.Value.Month.ToString("00") +
                                                  project.SpotTargetProject.OptimizeRuns[index - 1].EndDateTime.Value.Day.ToString("00") +
                                                  project.SpotTargetProject.OptimizeRuns[index - 1].EndDateTime.Value.Hour.ToString("00") +
                                                  project.SpotTargetProject.OptimizeRuns[index - 1].EndDateTime.Value.Minute.ToString("00") +
                                                  ".icc";
                                    if (index > 1)
                                    {
                                        if (project.SpotTargetProject.OptimizeRuns[index - 1].Status == EOptimizeRunStatus.Certify)
                                        {
                                            LastDLPName = project.SpotTargetProject.ProjectName + " NCP" +
                                                          project.SpotTargetProject.OptimizeRuns[index - 2].EndDateTime.Value.Month.ToString("00") +
                                                          project.SpotTargetProject.OptimizeRuns[index - 2].EndDateTime.Value.Day.ToString("00") +
                                                          project.SpotTargetProject.OptimizeRuns[index - 2].EndDateTime.Value.Hour.ToString("00") +
                                                          project.SpotTargetProject.OptimizeRuns[index - 2].EndDateTime.Value.Minute.ToString(
                                                              "00") + ".icc";
                                            //----If last run is certify run, then get DLP of its previous run----//
                                        }
                                    }
                                }
                                x.LastDlpName = (index == 0) ? string.Empty : LastDLPName;
                                x.DlpName = string.Format("{0} NCP {1}.icc",
                                                          project.SpotTargetProject.ProjectName,
                                                          index++.ToString("000"));

                            });
                        }
                        if (project.CurrDlpId > 0)
                        {
                            project.CurrentDlpName = project.SpotTargetProject.OptimizeRuns.Single(x => x.OptimizeRunId == project.CurrDlpId).DlpName;
                            project.ProposedDlpName = project.SpotTargetProject.OptimizeRuns.Last().DlpName;
                        }

                        project.CurrentDisplayStatus =
                            project.SpotTargetProject.OptimizeRuns.Count() == 1
                                ? ECurrentProjectStatus.New
                                : ECurrentProjectStatus.Run;


                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotProjectDetail"));
            return project;
        }

        public int DeleteSpotProject(int spotprojectId)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CPSpot].[DeleteSpotProject]",
                            new object[]
                                    {
                                        spotprojectId
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteSpotProject"));
            return returnValue;
        }


        public byte[] GetNamedColorProfile(int operationId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CpSpot.GetNamedColorProfileData",
                            new object[]
                                    {
                                        operationId
                                    });
                    if (data == null || data == DBNull.Value)
                    {
                        throw new Exception("named color profile not found for operationId: " + operationId);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetNamedColorProfile"));
            return data == DBNull.Value ? null : (byte[])data;
        }

        public byte[] GetLastOptimizedNamedColorProfile(int spotProjectId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSpot.GetLastOptimizedNamedColorProfile",
                            new object[]
                                    {
                                        spotProjectId
                                    });
                    if (data == null || data == DBNull.Value)
                    {
                        throw new Exception("Last optimized named color profile not found for spot projectId: " + spotProjectId);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLastOptimizedNamedColorProfile"));
            return data == DBNull.Value ? null : (byte[])data;
        }

        public ReferenceChartData GetSpotReferenceChartData(int spotProjectId, EReferenceChartDisType referenceChartDisType)
        {
            var chartData = new ReferenceChartData();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsReferenceChart = DataAccessFactory.Instance.GetDsResult("[CPSpot].[GetReferenceChart]",
                                                                                  new object[] { spotProjectId, referenceChartDisType });
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
                        var UVMode = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "UVMode");
                        var DensityType = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "DensityType");

                        chartData.DeviceSetting = new MeasurementDevice
                        {
                            SerialNumber = chartData.MeasureDeviceSrNo,
                            MeasurementSettings = new MeasurementSetting()
                            {
                                IlluminantName = DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "IlluminantType"),
                                ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ObserverType")),
                                UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                UVMode = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "UVMode"),
                                ReadMode = DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ReadMode"),
                                MeasurementCount = DataReader.ReadShort(dsReferenceChart.Tables[0].Rows[0], "MeasurementCount"),
                                DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                            }
                        };
                        chartData.ReferenceCharts = dsReferenceChart.Tables[1].AsEnumerable().
                            Select(x =>
                                   new ReferenceChart()
                                   {
                                       //ChartTypeId = DataReader.ReadShort(x, "ChartTypeID"),
                                       SampleName = DataReader.ReadString(x, "SampleName"),
                                       //CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                                       //CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                                       //CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                                       //CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                                       //PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                       HtmlColor = DataReader.ReadString(x, "HTMLColor")
                                   }).ToList();

                        var dsReferenceChartSetting = DataAccessFactory.Instance.GetDsResult("[CPSpot].[GetSpotReferenceChartSetting]",
                                                                                 new object[] { spotProjectId });
                        if (dsReferenceChartSetting.Tables[0].Rows.Count > 0)
                        {
                            chartData.ChartSetting = new ChartSetting();
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeX"))
                                chartData.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeX");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeY"))
                                chartData.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeY");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeX"))
                                chartData.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeX");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeY"))
                                chartData.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeY");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("MarkerDistance"))
                                chartData.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "MarkerDistance");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeX"))
                                chartData.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeX");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeY"))
                                chartData.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeY");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstLineY"))
                                chartData.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstLineY");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstField"))
                                chartData.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstField");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToBar"))
                                chartData.ChartSetting.DistToBar = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToBar");

                            //For IO devices...
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("FirstSample"))
                                chartData.ChartSetting.FirstSample = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "FirstSample");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadDirection"))
                                chartData.ChartSetting.ReadDirection = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadDirection");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadMode"))
                                chartData.ChartSetting.ReadMode = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadMode");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("RowsPerPage"))
                                chartData.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "RowsPerPage");
                            if (dsReferenceChartSetting.Tables[0].Columns.Contains("Pages"))
                                chartData.ChartSetting.Pages = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "Pages");
                        }

                        //var reader = DataAccessFactory.Instance.GetXmlResult("CPSync4D.GetChartSettings", new object[] { chartData.ReferenceChartTypeId });
                        //var dsChartSettings = new DataSet();
                        //dsChartSettings.ReadXml(reader);
                        //if (dsChartSettings.Tables.Count > 0)
                        //{
                        //    chartData.ChartSetting = new ChartSetting();
                        //    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeX"))
                        //        chartData.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeX");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeY"))
                        //        chartData.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeY");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeX"))
                        //        chartData.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeX");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeY"))
                        //        chartData.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeY");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("MarkerDistance"))
                        //        chartData.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "MarkerDistance");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeX"))
                        //        chartData.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeX");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeY"))
                        //        chartData.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeY");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstLineY"))
                        //        chartData.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstLineY");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstField"))
                        //        chartData.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstField");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("DistToBar"))
                        //        chartData.ChartSetting.DistToBar = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToBar");

                        //    //For IO devices...
                        //    if (dsChartSettings.Tables[0].Columns.Contains("FirstSample"))
                        //        chartData.ChartSetting.FirstSample = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "FirstSample");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("ReadDirection"))
                        //        chartData.ChartSetting.ReadDirection = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadDirection");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("ReadMode"))
                        //        chartData.ChartSetting.ReadMode = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadMode");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("RowsPerPage"))
                        //        chartData.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "RowsPerPage");
                        //    if (dsChartSettings.Tables[0].Columns.Contains("Pages"))
                        //        chartData.ChartSetting.Pages = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "Pages");
                        //}
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceChartData"));
            return chartData;
        }


        public byte[] GetSpotDestinationProfile(int projectId)
        {
            var data = new object();
            Common.ImplementBusinessLogic(
                () =>
                {
                    data = DataAccessFactory.
                        Instance.
                        GetScalar(
                            "CPSpot.GetSpotDestinationProfileData",
                            new object[]
                                    {
                                        projectId
                                    });
                    if (data == null || data == DBNull.Value)
                    {
                        throw new Exception("Destination profile not found for spot projectId: " + projectId);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotDestinationProfile"));
            return data == DBNull.Value ? null : (byte[])data;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
