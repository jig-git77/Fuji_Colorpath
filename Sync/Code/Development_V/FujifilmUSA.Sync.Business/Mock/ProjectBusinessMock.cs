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
        
        public List<SourceProfile> GetSourceProfileList(int currentUserId)
        {
            return new List<SourceProfile>();
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

        public List<DeviceFamilyProfilerOption> GetDeviceFamilyProfilerOptionPreset(short deviceFamilyId)
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

        public List<MeasurementDevice> GetMeasurementDeviceList(int deviceId)
        {
            return new List<MeasurementDevice>();
        }

        public List<ReferenceChartType> GetReferenceChartTypeList(int MeasurementDeviceID, int ChartDisplayType)
        {
            return new List<ReferenceChartType>();
        }

        public Dictionary<string, string> GetPrintConditionPreviewData(short MeasurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId)
        {
            return new Dictionary<string, string>();
        }

        public List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPresetList(int currentUserId)
        {
            return new List<Entity.CPSyncV.MetricTargetPreset>();
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion


        
    }
}
