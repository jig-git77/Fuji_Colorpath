using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSpot;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProjectBusiness : ITaskBase
    {
        Project GetProjectDetail(int projectId, bool isVarification = false);
        List<Project> GetProjectsByDeviceId(int deviceId, int LicenseTypeID);
        int DeleteProject(int projectId);
        Dictionary<string, string> GetProjectOption(int projectId, EProfileType type);
        List<Entity.CPSync1D.PrintStandardType> GetPrintStandardTypeList(int currentUserId, bool isSync1DType, int TargetPrintCategory = 0);
        List<DeviceFamilyProfilerOption> GetDeviceFamilyProfilerOptionPreset(short deviceFamilyId, short LicenseTypeId);
        List<Illuminant> GetIlluminantList();
        List<ObserverType> GetObserverTypeList();
        List<MeasurementDevice> GetMeasurementDeviceList(int loginId, int deviceId, bool isSpotTargetType = false);
        List<ReferenceChartType> GetReferenceChartTypeList(int measurementDeviceId, int ChartDisplayType, int ProjectTypeId, int MetricTargetPresetID = 0, int InstrumentTypeID = 0);
        Dictionary<string, string> GetPrintConditionPreviewData(short measurementDeviceId, short illuminantId, short observerTypeId, short referenceChartTypeId, int deviceFamilyProfilerOptionId, short V_measurementDeviceId, short Opt_ReferenceChartTypeId, short V_ReferenceChartTypeId, short metricTargetPresetId, int ReTargetProjectId);
        byte[] GetChartPreviewOnReferenceChartTypeId(short referenceChartTypeId, string source);
        byte[] GetSourceProfile(int projectId);
        byte[] GetDestinationProfile(int projectId);
        byte[] GetLastOptimizedDlp(int projectId);
        int SaveProject(Project project);
        int SaveSpotProject(Project project);
        ReferenceChartData GetReferenceChartData(int projectId, EReferenceChartDisType referenceChartDisType);
        Guid StoreTempararyProjectInformation(string projectName, string media, string colorant, string ripSettings, int measurementDeviceTypeId, int chartId, int projectId, Guid tempProjectId, string silverlightPostUrl);
        TempProjectInfo GetTempProjectInfo(Guid tempProjectId);
        List<Entity.CPSyncV.MetricTargetPreset> GetMetricTargetPresetList(int currentUserId, int ProjectTypeId, short PrintingTypeId, bool isVarification = false);
        List<Project> GetProjectsForImportMeasurement(int deviceId, int LicenseTypeID, int CurrentProjectID);
        List<int> GetMeasurementIDsbyProjectID(int deviceId, int LicenseTypeID, int CurrentProjectID);
        int ImportMeasurements(int MeasurementID, int OptimizeRunID);
        List<Entity.CPSyncV.MetricSum> GetMetricSumTargetValues(int metricTargetPresetId);
        List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVI(int printStandardTypeId);
        List<Entity.CPSync1D.ProjectTargetDensity> GetPrintStandardDensity(int printStandardTypeId);
        List<Entity.CPSync1D.ProjectTargetHueAngle> GetPrintStandardHueAngle(int printStandardTypeId);
        List<Entity.CPSync1D.ProjectTargetLAB> GetProjectTargetLAB(int printStandardTypeId, int ProjectTypeId);
        byte[] Get1DLastOptimizedDlp(int projectId);
        Guid StoreTemporaryValidationInformation(string jobName, string customer, string pageOrSurface, string jdfJobId, int projectId, Guid tempValidationInfoId, string silverlightPostUrl);
        Entity.CPSyncV.TempValidationInfo GetTempValidationInfo(Guid tempValidationId);
        Dictionary<int, string> GetReTargetProjectList(int currentUserId, int ProjectTypeId, int DeviceId);
        List<Entity.CPSpot.SpotProjectLibrary> GetSpotLibraryListByProjectId(int projectId);
        List<Entity.CPSpot.SpotTarget> GetSpotTargetList(int currentUserId, bool isSpotTargetType);
        Project GetProjectDetail_SpotProject(int projectId);
        int DeleteSpotProject(int spotprojectId);

        byte[] GetNamedColorProfile(int operationId);
        byte[] GetLastOptimizedNamedColorProfile(int spotProjectId);
        ReferenceChartData GetSpotReferenceChartData(int spotProjectId, EReferenceChartDisType referenceChartDisType);
        byte[] GetSpotDestinationProfile(int projectId);

        ReferenceChartPlugin GetPluginReferenceChartData(int projectId, EReferenceChartDisType referenceChartDisType);
        ReferenceChartPlugin GetPluginSpotReferenceChartData(int spotProjectId, EReferenceChartDisType referenceChartDisType);

        Dictionary<int, string> GetReTargetSpotProjectList(int CompanyID, int BranchID, int DeviceID);
        SpotProjectLibrary GetRetargetSpotProjectDetails(int RetargetSpotprojectID);
        DataTable GetRetargetProjectDetails(int RetargetProjectID);
        int GetProjectLicenseTypeLimit(int DeviceID, int LicenseTypeID);
    }
}
