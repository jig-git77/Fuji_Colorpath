using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ProjectBusinessImpl : IProjectBusiness
    {
        #region IProjectBusiness Members

        public Project GetProjectDetail(int projectId)
        {
            var project = new Project();
            var dsProject = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetProjectDetail", new object[] { projectId });

            if (dsProject.Tables[0].Rows.Count > 0)
            {
                project = dsProject.Tables[0].AsEnumerable().
                    Select(x => new Project()
                    {
                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                        ProjectName = DataReader.ReadString(x, "ProjectName"),
                        CreateDate = DataReader.ReadDate(x, "CreateDate"),
                        CurrDlpId = DataReader.ReadInt(x, "CurrDLPID"),
                        IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                        Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "ProjStatus"),
                        ReferenceChartTypeId = DataReader.ReadInt(x, "ReferenceChartTypeId"),
                    }).FirstOrDefault();

                project.OptimizeRuns = dsProject.Tables[1].AsEnumerable().
                    Select(x => new OptimizeRun()
                    {
                        OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                        RunName = DataReader.ReadString(x, "RunName"),
                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                        Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "Status"),
                        StartDateTime = DataReader.ReadDate(x, "StartDateTime"),
                        EndDateTime = DataReader.ReadDate(x, "EndDateTime"),
                        MasterRunId = DataReader.ReadInt(x, "MasterRunID"),
                        HasMeasurements = (DataReader.ReadInt(x, "MeasurementCount") > 0),
                        HasProfile = DataReader.ReadBoolean(x, "HasProfile")
                    }).ToList();
            }
            return project;
        }

        public List<Project> GetProjectsByDeviceId(int deviceId)
        {
            var objProjectList = new List<Project>();
            var dsProjectList = DataAccessFactory.Instance.GetDsResult("[uspProjectByDeviceID]", new object[] { deviceId });
            if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
            {
                objProjectList = dsProjectList.Tables[0].AsEnumerable().
                    Select(x => new Project()
                    {
                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                        ProjectName = DataReader.ReadString(x, "ProjectName"),
                        DeviceId = DataReader.ReadInt(x, "CpDeviceId"),
                        IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                        SourceProfileId = DataReader.ReadShort(x, "SourceProfileId"),
                        CreateDate = DataReader.ReadDate(x, "EndDateTime"),
                        PrintCondition = new PrintCondition() { Media = DataReader.ReadString(x, "Media"), Colorant = DataReader.ReadString(x, "Colorant"), RipSettings = DataReader.ReadString(x, "RipSettings") },
                        Status = (EOptimizeRunStatus)DataReader.ReadInt(x, "ProjStatus")
                    }).ToList();
            }
            return objProjectList;
        }

        public int DeleteProject(int projectId)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[uspDeleteProject]",
                                                     new object[] { projectId });
            return returnValue;
        }

        public Dictionary<string, string> GetProjectOption(int projectId, EProfileType type)
        {
            var dsProjectOption = DataAccessFactory.Instance.GetDsResult(
                "CPSync4D.GetProjectOptions",
                new object[] { projectId, Convert.ToInt32(type) });
            var projectOptions = dsProjectOption.Tables[0].AsEnumerable().
                Select(x =>
                       new KeyValuePair<string, string>(
                           DataReader.ReadString(x, "ProfilerOption"),
                           DataReader.ReadString(x, "ProfilerOptionValue"))).ToDictionary(x => x.Key, x => x.Value);
            return projectOptions;
        }

        public List<SourceProfile> GetSourceProfileList()
        {
            var dstSourceList = new List<SourceProfile>();
            var dsSourceList = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetSourceProfileList", new object[] { });
            if (dsSourceList != null && dsSourceList.Tables.Count > 0)
            {
                dstSourceList = (dsSourceList.Tables[0].AsEnumerable().Select(
                    Source => new SourceProfile()
                    {
                        SourceProfileId = DataReader.ReadShort(Source, "SourceProfileId"),
                        ProfileName = DataReader.ReadString(Source, "ProfileName"),
                        ProfileDesc = DataReader.ReadString(Source, "ProfileDesc"),
                    })).ToList();
            }
            return dstSourceList;
        }

        public List<DeviceFamilyProfilerOption> GetDeviceFamilyProfilerOptionPreset(short deviceFamilyId)
        {
            var presetList = new List<DeviceFamilyProfilerOption>();
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
            return presetList;
        }

        public byte[] GetChartPreviewOnProjectId(int projectId)
        {
            return (byte[])DataAccessFactory.Instance.GetScalar("CPSync4D.GetChartPreviewOnProjectId", new object[] { projectId });
        }

        public byte[] GetSourceProfile(int projectId)
        {
            return (byte[])DataAccessFactory.Instance.GetScalar("CPSync4D.GetSourceProfileData", new object[] { projectId });
        }

        public byte[] GetDestinationProfile(int projectId)
        {
            return (byte[])DataAccessFactory.Instance.GetScalar("CPSync4D.GetDestinationProfileData", new object[] { projectId });
        }

        public byte[] GetLastOptimizedDlp(int projectId)
        {
            return (byte[])DataAccessFactory.Instance.GetScalar("CPSync4D.GetLastOptimizedDlp", new object[] { projectId });
        }

        #endregion
    }
}
