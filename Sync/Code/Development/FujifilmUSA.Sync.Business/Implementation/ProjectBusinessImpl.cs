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
            var dsProjectList = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetProjectList", new object[] { deviceId });
            if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
            {
                objProjectList = dsProjectList.Tables[0].AsEnumerable().
                    Select(x => new Project()
                    {
                        ProjectId = DataReader.ReadInt(x, "ProjectID"),
                        ProjectName = DataReader.ReadString(x, "ProjectName"),
                        DeviceId = DataReader.ReadInt(x, "CpDeviceId"),
                        IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                        SourceProfile = new SourceProfile() { SourceProfileId = DataReader.ReadShort(x, "SourceProfileId") },
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
            var dsProjectOption = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetProjectOptions",
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

        public Project SaveProject(Project project)
        {
            var projectOptions = new XElement("ProjectOptions",
                                              project.ProjectOption.
                                                  Select(x =>
                                                         new XElement("ProjectOption",
                                                                      new XAttribute("ProfileTypeID", x.ProfileTypeId),
                                                                      new XAttribute("ProfilerOptionID", x.ProfilerOptionId),
                                                                      new XAttribute("ProfilerOptionValue", x.ProfilerOptionValue)
                                                             )).ToList());
            var outParam = new Parameters() { ParameterName = "@ProjectID", Type = DbType.Int32 };
            DataAccessFactory.
                Instance.
                ExecuteProcedure("CPSync4D.CreateProject",
                                 new List<Parameters>()
                                     {
                                         new Parameters() {ParameterName = "@CPDeviceID", Type = DbType.Int32, Value = project.DeviceId},
                                         new Parameters() {ParameterName = "@SourceProfileID", Type = DbType.Int32, Value = project.SourceProfile.SourceProfileId},
                                         new Parameters() {ParameterName = "@ProjectName", Size = 30, Type = DbType.String, Value = project.ProjectName},
                                         new Parameters()
                                             {ParameterName = "@CreateDate", Type = DbType.DateTime, Value = project.CreateDate.AddMilliseconds(-project.CreateDate.Millisecond)},
                                         new Parameters() {ParameterName = "@Media", Size = 50, Type = DbType.String, Value = project.PrintCondition.Media},
                                         new Parameters() {ParameterName = "@Colorant", Size = 50, Type = DbType.String, Value = project.PrintCondition.Colorant},
                                         new Parameters() {ParameterName = "@RipSettings", Size = 50, Type = DbType.String, Value = project.PrintCondition.RipSettings},
                                         new Parameters() {ParameterName = "@TechnicalName", Size = 30, Type = DbType.String, Value = project.PrintCondition.TechnicianName},
                                         new Parameters() {ParameterName = "@Notes", Size = 500, Type = DbType.String, Value = project.PrintCondition.Notes},
                                         new Parameters() {ParameterName = "@ObserverType", Type = DbType.Int16, Value = project.MeasurementSetting.ObserverType},
                                         new Parameters() {ParameterName = "@Illuminant", Type = DbType.Int16, Value = project.MeasurementSetting.Illuminant},
                                         new Parameters() {ParameterName = "@UVIncluded", Type = DbType.Boolean, Value = project.MeasurementSetting.UvIncluded},
                                         new Parameters() {ParameterName = "@MeasurementDeviceID", Type = DbType.Int16, Value = project.MeasurementSetting.MeasurementDeviceId},
                                         new Parameters() {ParameterName = "@ReferenceChartTypeID", Type = DbType.Int16, Value = project.MeasurementSetting.ReferenceChartTypeId},
                                         new Parameters() {ParameterName = "@Status", Type = DbType.Int32, Value = Convert.ToInt32(EOptimizeRunStatus.Open)},
                                         new Parameters() {ParameterName = "@StartDateTime", Type = DbType.DateTime, Value = DateTime.Now},
                                         new Parameters() {ParameterName = "@ProjectOptions", Value = projectOptions.ToString()}
                                     },
                                 new List<Parameters>()
                                     {
                                         outParam
                                     });
            if (outParam.Value != null)
            {
                var projectId = Convert.ToInt32(outParam.Value);
                return GetProjectDetail(projectId);
            }
            else
            {
                return new Project();
            }
        }

        public ReferenceChartData GetReferenceChartData(int projectId)
        {
            var chartData = new ReferenceChartData();
            var dsReferenceChart = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[GetReferenceChart]", new object[] { projectId });
            if (dsReferenceChart.Tables[0].Rows.Count == 0) return chartData;

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
                                                                            IlluminantName = DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "IlluminantType"),
                                                                            ObserverTypeName = DataReader.ReadString(dsReferenceChart.Tables[0].Rows[0], "ObserverType"),
                                                                            UvIncludedName = uvIncluded ? EIlluminationMode.UV.ToString() : EIlluminationMode.UVCut.ToString(),
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
            return chartData;
        }

        public List<Illuminant> GetIlluminantList()
        {
            var dstIlluminantList = new List<Illuminant>();
            var dsIlluminantList = DataAccessFactory.Instance.GetDsResult("CP.GetIlluminantList", new object[] { });
            if (dsIlluminantList != null && dsIlluminantList.Tables.Count > 0)
            {
                dstIlluminantList = (dsIlluminantList.Tables[0].AsEnumerable().Select(
                    Illumin => new Illuminant()
                    {
                        IlluminantId = DataReader.ReadShort(Illumin, "IlluminantID"),
                        IlluminantType = DataReader.ReadString(Illumin, "IlluminantType"),
                    })).ToList();
            }
            return dstIlluminantList;
        }

        public List<ObserverType> GetObserverTypeList()
        {
            var dstObserverTypeList = new List<ObserverType>();
            var dsObserverTypeList = DataAccessFactory.Instance.GetDsResult("CP.GetObserverTypeList", new object[] { });
            if (dsObserverTypeList != null && dsObserverTypeList.Tables.Count > 0)
            {
                dstObserverTypeList = (dsObserverTypeList.Tables[0].AsEnumerable().Select(
                    Observer => new ObserverType()
                    {
                        ObserverTypeId = DataReader.ReadShort(Observer, "ObserverTypeID"),
                        Type = DataReader.ReadString(Observer, "ObserverType"),
                    })).ToList();
            }
            return dstObserverTypeList;
        }

        public Dictionary<int, string> GetMeasurementDeviceList(int deviceId)
        {
            var dsMeasurementDevice = DataAccessFactory.Instance.GetDsResult("CP.GetMeasurementDeviceList",
                                        new object[] { deviceId });
            var measurementDevice = dsMeasurementDevice.Tables[0].AsEnumerable().
                Select(x =>
                        new KeyValuePair<int, string>(
                            DataReader.ReadInt(x, "MeasurementDeviceID"),
                            DataReader.ReadString(x, "MeasurementDevi"))).ToDictionary(x => x.Key, x => x.Value);

            return measurementDevice;
        }

        public List<ReferenceChartType> GetReferenceChartTypeList(int measurementDeviceId)
        {
            var presetList = new List<ReferenceChartType>();
            var dsPresetList = DataAccessFactory.Instance.GetDsResult("CP.GetReferenceChartTypeList", new object[] { measurementDeviceId });
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
            return presetList;
        }

        public Dictionary<string, string> GetPrintConditionPreviewData(short MeasurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId)
        {
            var data = new Dictionary<string, string>();
            var dsPreviewData = DataAccessFactory.Instance.GetDsResult("CP.GetPreviewPrintDetail",
                    new object[] { 
                        MeasurementDeviceId, 
                        illuminantId, 
                        observerTypeId, 
                        referenceChartTypeId, 
                        deviceFamilyProfilerOptionId });

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
            return data;
        }

        public byte[] GetReferenceChartTypeImage(int ReferenceChartTypeID)
        {
            byte[] imageByte = null;
            var dsPresetList = DataAccessFactory.Instance.GetDsResult("CP.GetReferenceChartImage", new object[] { ReferenceChartTypeID });
            if (dsPresetList != null && dsPresetList.Tables.Count > 0)
            {
                imageByte = dsPresetList.Tables[0].AsEnumerable().Select(
                    x => new ReferenceChartType()
                    {
                        ChartPreview = DataReader.ReadByteArray(x, "ChartPreview")

                    }).SingleOrDefault().ChartPreview;
            }
            return imageByte;
        }



        #endregion
    }
}
