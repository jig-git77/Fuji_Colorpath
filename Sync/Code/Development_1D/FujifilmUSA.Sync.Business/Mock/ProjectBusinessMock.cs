using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProjectBusinessMock : IProjectBusiness
    {
        #region IProjectBusiness Members

        public Project GetProjectDetail(int projectId, bool isVarification = false)
        {
            return new Project();
        }

        public List<Project> GetProjectsByDeviceId(int deviceId, int LicenseTypeID)
        {
            return new List<Project>();
        }

        public int DeleteProject(int projectId)
        {
            return 0;
        }
        
        public Dictionary<string, string> GetProjectOption(int projectId, EProfileType type)
        {
            return new Dictionary<string, string>();
        }

        public List<Entity.CPSync1D.PrintStandardType> GetPrintStandardTypeList(int currentUserId, bool isSync1DType)
        {
            return new List<Entity.CPSync1D.PrintStandardType>();
        }

        public byte[] GetChartPreviewOnReferenceChartTypeId(short referenceChartTypeId, string source)
        {
            return new byte[1];
        }

        public byte[] GetSourceProfile(int projectId)
        {
            return new byte[1];
        }

        public byte[] GetDestinationProfile(int projectId)
        {
            return new byte[1];
        }

        public byte[] GetLastOptimizedDlp(int projectId)
        {
            return new byte[1];
        }

        public int SaveProject(Project project)
        {
            return 0;
        }

        public ReferenceChartData GetReferenceChartData(int projectId, EReferenceChartDisType referenceChartDisType)
        {
            return new ReferenceChartData();
        }

        public Guid StoreTempararyProjectInformation(string projectName, string media, string colorant, string ripSettings, int measurementDeviceTypeId, int chartId, int projectId, Guid tempProjectId, string silverlightPostUrl)
        {
            return new Guid();
        }

        public TempProjectInfo GetTempProjectInfo(Guid tempProjectId)
        {
            return new TempProjectInfo();
        }

        public List<DeviceFamilyProfilerOption> GetDeviceFamilyProfilerOptionPreset(short deviceFamilyId, short LicenseTypeId)
        {
            return new List<DeviceFamilyProfilerOption>();
        }

        public List<Illuminant> GetIlluminantList()
        {
            return new List<Illuminant>();
        }

        public List<ObserverType> GetObserverTypeList()
        {
            return new List<ObserverType>();
        }

        public List<MeasurementDevice> GetMeasurementDeviceList(int loginId, int deviceId)
        {
            return new List<MeasurementDevice>();
        }

        public List<ReferenceChartType> GetReferenceChartTypeList(int MeasurementDeviceID, int ChartDisplayType, int ProjectTypeId, int MetricTargetPresetID = 0, int InstrumentTypeID = 0)
        {
            return new List<ReferenceChartType>();
        }

        public Dictionary<string, string> GetPrintConditionPreviewData(short MeasurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId, short V_measurementDeviceId, short Opt_ReferenceChartTypeId, short V_ReferenceChartTypeId, short metricTargetPresetId, int ReTargetProjectId)
        {
            return new Dictionary<string, string>();
        }

        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPresetList(int currentUserId, int ProjectTypeId, bool isVarification = false)
        {
            return new List<Entity.CPSyncV.MetricTargetPreset>();
        }

        public List<Project> GetProjectsForImportMeasurement(int deviceId, int LicenseTypeID, int CurrentProjectID)
        {
            return new List<Project>();
        }
        public List<int> GetMeasurementIDsbyProjectID(int deviceId, int LicenseTypeID, int CurrentProjectID)
        {
            return new List<int>();
        }
        public int ImportMeasurements(int MeasurementID, int OptimizeRunID)
        {
            return 0;
        }

        public List<Entity.CPSyncV.MetricSum> GetMetricSumTargetValues(int metricTargetPresetId)
        {            
            return new List<Entity.CPSyncV.MetricSum>();
        }

        public List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVI(int printStandardTypeId)
        {
            return new List<Entity.CPSync1D.PrintStandardTVI>();
        }

        public List<Entity.CPSync1D.ProjectTargetDensity> GetPrintStandardDensity(int printStandardTypeId)
        {
            return new List<Entity.CPSync1D.ProjectTargetDensity>();
        }

        public List<Entity.CPSync1D.ProjectTargetLAB> GetProjectTargetLAB(int printStandardTypeId, int ProjectTypeId)
        {
            return new List<Entity.CPSync1D.ProjectTargetLAB>();
        }

        public byte[] Get1DLastOptimizedDlp(int projectId)
        {
            return new byte[1];        
        }
        public Guid StoreTemporaryValidationInformation(string jobName, string customer, string pageOrSurface, string jdfJobId, int projectId, Guid tempValidationInfoId, string silverlightPostUrl)
        {
            return new Guid();
        }

        public Entity.CPSyncV.TempValidationInfo GetTempValidationInfo(Guid tempValidationId)
        {
            return new Entity.CPSyncV.TempValidationInfo();
        }

        public Dictionary<int, string> GetReTargetProjectList(int currentUserId, int ProjectTypeId, int DeviceId)
        {
            return new Dictionary<int, string>();
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


        
    }
}
