using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProjectBusinessMock:IProjectBusiness
    {
        #region IProjectBusiness Members

        public Project GetProjectDetail(int projectId)
        {
            return new Project();
        }

        public List<Project> GetProjectsByDeviceId(int deviceId)
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

        public byte[] GetChartPreviewOnReferenceChartTypeId(int referenceChartTypeId, string source)
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

        public Project SaveProject(Project project)
        {
            return new Project();
        }

        public ReferenceChartData GetReferenceChartData(int projectId)
        {
            return new ReferenceChartData();
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

        public List<ReferenceChartType> GetReferenceChartTypeList(int MeasurementDeviceID)
        {
            return new List<ReferenceChartType>();
        }

        public Dictionary<string, string> GetPrintConditionPreviewData(short MeasurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId)
        {
            return new Dictionary<string, string>();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
