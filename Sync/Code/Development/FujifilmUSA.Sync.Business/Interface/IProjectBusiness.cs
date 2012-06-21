using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProjectBusiness : ITaskBase
    {
        Project GetProjectDetail(int projectId);
        List<Project> GetProjectsByDeviceId(int deviceId, int LicenseTypeID);
        int DeleteProject(int projectId);
        Dictionary<string, string> GetProjectOption(int projectId, EProfileType type);
        List<SourceProfile> GetSourceProfileList(int currentUserId);
        List<DeviceFamilyProfilerOption> GetDeviceFamilyProfilerOptionPreset(short deviceFamilyId);
        List<Illuminant> GetIlluminantList();
        List<ObserverType> GetObserverTypeList();
        List<MeasurementDevice> GetMeasurementDeviceList(int deviceId);
        List<ReferenceChartType> GetReferenceChartTypeList(int measurementDeviceId, int ChartDisplayType);
        Dictionary<string, string> GetPrintConditionPreviewData(short measurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId);
        byte[] GetChartPreviewOnReferenceChartTypeId(short referenceChartTypeId, string source);
        byte[] GetSourceProfile(int projectId);
        byte[] GetDestinationProfile(int projectId);
        byte[] GetLastOptimizedDlp(int projectId);
        Project SaveProject(Project project);
        ReferenceChartData GetReferenceChartData(int projectId);
        Guid StoreTempararyProjectInformation(string projectName, string media, string colorant, string ripSettings, int measurementDeviceTypeId, int chartId, int projectId, Guid tempProjectId);
        TempProjectInfo GetTempProjectInfo(Guid tempProjectId);
    }
}
