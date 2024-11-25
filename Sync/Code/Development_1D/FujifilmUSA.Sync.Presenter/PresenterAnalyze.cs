using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAnalyze
    {
        private IViewAnalyze _viewAnalyze;
        public PresenterAnalyze(IViewAnalyze viewAnalyze)
        {
            _viewAnalyze = viewAnalyze;
        }

        public void CreateDestinationAndDlp()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateInitialDestinationAndDeviceLinkProfiles(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    _viewAnalyze.DlpDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }
        public void CreateDestinationAndDlpPlus()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateInitialDestinationAndDeviceLinkProfilesPlus(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    _viewAnalyze.DlpDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }
        public void CreateDestinationAndDlpMultiColor()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateInitialDestinationAndDeviceLinkProfilesMultiColor(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    _viewAnalyze.DlpDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }

        public void GetMeasurementsOnRunId()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementIds = measurementBusiness.GetMeasurementsOnRunId(_viewAnalyze.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
        }

        public void GetAnalyzeChartMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurements = measurementBusiness.GetAnalyzeChartMeasurement(_viewAnalyze.OptimizeRunId, _viewAnalyze.From_ΔE, _viewAnalyze.IsSync1DType);
        }

        public void GetAnalyzeChartVMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurements = measurementBusiness.GetAnalyzeChartVMeasurement(_viewAnalyze.ProjectId, _viewAnalyze.From_ΔE);
        }

        public void UpdateMeasurementByAnalyzeChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            measurementBusiness.UpdateMeasurementByAnalyzeChart(_viewAnalyze.Measurement, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType);
        }

        public void GetAvgDeltaMeasurement_Optimization()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementsDelta = measurementBusiness.GetAvgDeltaMeasurement(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE, _viewAnalyze.IsVerification);
        }

        public void GetAvgDeltaMeasurement_Validation()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementSummary = measurementBusiness.GetVMeasurementSummary(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements, _viewAnalyze.IsSyncDLPPlusType);
        }

        public void GetPatchDetailsofMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.dtPatchDetails = measurementBusiness.GetPatchDetailsofMeasurement(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsDlpMultiColor, _viewAnalyze.IsSync1DMultiColor, _viewAnalyze.CurrentRunname, _viewAnalyze.LicenseTypeId,_viewAnalyze.IsDestinationProfile);
        }

        public void GetSpotPatchDetailsofMeasurement()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.dtPatchDetails = measurementBusiness.GetSpotPatchDetailsofMeasurement(_viewAnalyze.OptimizeRunId);
        }

        public void GetInkPaperChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            var charData = measurementBusiness.GetChartData(_viewAnalyze.MeasurementId, _viewAnalyze.From_ΔE, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements, _viewAnalyze.LicenseTypeId);
            _viewAnalyze.DTDeltaEPrimaryPatches = charData[0];
            _viewAnalyze.DTPaperPatches = charData[1];
            _viewAnalyze.DTDensity = charData[2];
        }

        public void GetGrayChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            var GaumatChartData = measurementBusiness.GetGamutChartData(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements, _viewAnalyze.IsDlpMultiColor , _viewAnalyze.IsDestinationProfile);
            _viewAnalyze.DTGaumatChart1 = GaumatChartData[0];
            _viewAnalyze.DTGaumatChart2 = GaumatChartData[1];
            _viewAnalyze.DTLABChart = GaumatChartData[2];
        }

        public void GetG7Chart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            var G7ChardData = measurementBusiness.GetG7ChartData(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsAverageOfMeasurements, _viewAnalyze.IsSyncDLPPlusType);
            _viewAnalyze.GrayFChart = G7ChardData[0];
            _viewAnalyze.GrayFSummary = G7ChardData[1];
            _viewAnalyze.BlackLChart = G7ChardData[2];
            _viewAnalyze.BlackLSummary = G7ChardData[3];
            _viewAnalyze.GrayLChart = G7ChardData[4];
            _viewAnalyze.GrayLSummary = G7ChardData[5];
            _viewAnalyze.BlackNPDChart = G7ChardData[6];
            _viewAnalyze.GrayNPDChart = G7ChardData[7];
        }

        public void Get1DMeasurementsOnRunId()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.MeasurementIds = measurementBusiness.Get1DMeasurementsOnRunId(_viewAnalyze.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
        }

        public void CreateG7AlignmentCurve()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.CreateG7AlignmentCurve(
                _viewAnalyze.ProjectId,
                _viewAnalyze.OptimizeRunId,
                _viewAnalyze.MeasurementIds,
                _viewAnalyze.useDLP,
                Convert.ToInt32(EOptimizeRunStatus.Closed),
                DateTime.Now,
                _viewAnalyze.Is1DMeasurementSmoothing,
                _viewAnalyze.DlpDescription);

        }

        public void CreateISOAlignmentCurve()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.CreateISOAlignmentCurve(
                _viewAnalyze.ProjectId,
                _viewAnalyze.OptimizeRunId,
                _viewAnalyze.MeasurementIds,
                _viewAnalyze.useDLP,
                Convert.ToInt32(EOptimizeRunStatus.Closed),
                DateTime.Now,
                _viewAnalyze.Is1DMeasurementSmoothing,
                _viewAnalyze.DlpDescription);

        }

        public void GetTVIChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.DTTVIChart = measurementBusiness.GetTVIChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements);
        }
        public void GetISOTVIChart()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.DTISOTVIChart = measurementBusiness.GetISOTVIChart(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements);
            _viewAnalyze.DTSpreadChartData = measurementBusiness.GetSpreadChartData(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements);
            _viewAnalyze.DTSpreadChartData_CMYK = measurementBusiness.GetSpreadChartData_CMYK(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements);
            _viewAnalyze.DTSpreadSplineRangeChartData = measurementBusiness.GetSpreadSplineRangeChartData(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements);
            _viewAnalyze.DTMaxSpreadChartData = measurementBusiness.GetMaxSpreadChartData(_viewAnalyze.MeasurementId, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType, _viewAnalyze.IsAverageOfMeasurements);
        }

        public void CreateSourceToSourceDlp()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateSourceToSourceDeviceLinkProfile(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    _viewAnalyze.DlpDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }

        public void CreateCustomSourceProfileDlp()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateCustomSourceProfile(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    _viewAnalyze.DlpDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }

        public int ChkAvgMeasurementCompatibility()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            return measurementBusiness.ChkAvgMeasurementCompatibility(_viewAnalyze.OptimizeRunId);
        }

        public void CreateReTargetProject()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateReTargetProject(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }

        public void UpdateOptimizeRun_SpotProject()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.UpdateSpotProjectOptimizeRun(_viewAnalyze.OptimizeRunId, _viewAnalyze.ProjectStatus, DateTime.Now);
        }

        public void GetProjectDetail_SpotProject()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Project = projectBusiness.GetProjectDetail_SpotProject(_viewAnalyze.SpotProjectId);
        }

        public void UpdateMeasurementByAnalyzeChartFromService()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            measurementBusiness.UpdateMeasurementByAnalyzeChartFromService(_viewAnalyze.MeasurementId, _viewAnalyze.IsUsed, _viewAnalyze.IsVerification, _viewAnalyze.IsSync1DType);
        }

        public void OptmizeSpotLibrary()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.OptimizeSpotNamedColorProfile(
                    _viewAnalyze.OptimizeRunId,
                    (int)EOptimizeRunStatus.Closed,
                    DateTime.Now);
        }

        public void GetSpotPatchSummaryDetails()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.dtPatchDetails = measurementBusiness.GetSpotProjectSummary(_viewAnalyze.OptimizeRunId);
        }

        public bool UpdateSpotProjectLabValues()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            return measurementBusiness.SaveSpotProjectLABValues(_viewAnalyze.SpotProjectLabs);
        }

        public void GetPatchDetailsOfMeasurementExport()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurement = measurementBusiness.GetMeasurementPatchDetailsForExport(_viewAnalyze.MeasurementId);
        }
        public void GetSpotPatchDetailsOfMeasurementExport()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurement = measurementBusiness.GetSpotMeasurementPatchDetailsForExport(_viewAnalyze.OptimizeRunId);
        }
        public void Get1DPatchDetailsOfMeasurementExport()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurement = measurementBusiness.Get1DMeasurementPatchDetailsForExport(_viewAnalyze.MeasurementId);
        }
        public void GetValidationPatchDetailsOfMeasurementExport()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.Measurement = measurementBusiness.GetValidationMeasurementPatchDetailsForExport(_viewAnalyze.MeasurementId);
        }

        public void SpotProjectSummarySliderByDeltaE()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.dtMeasurement = measurementBusiness.GetSpotProjectSummaryByDeltaE(_viewAnalyze.OptimizeRunId, _viewAnalyze.Dec_E_Value);
        }

        public void GetPatchDetailsofMeasurement_DotGain()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            _viewAnalyze.dtPatchDetails = measurementBusiness.GetPatchDetailsofMeasurement_DotGain(_viewAnalyze.MeasurementId);
        }

        public int ChkCloudFlowSupport()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            return measurementBusiness.ChkCloudFlowSupport(_viewAnalyze.ProjectId,_viewAnalyze.CurrentRefChartTypeId);
        }

        public void CreateDestination()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateDestinationProfile(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }
        public void CreateRevoria()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewAnalyze.ErrorOccured;
            optimizeRunBusiness.
                CreateInitialDestinationAndDeviceLinkProfilesPlus(
                    _viewAnalyze.ProjectId,
                    _viewAnalyze.OptimizeRunId,
                    _viewAnalyze.MeasurementIds,
                    _viewAnalyze.DestinationProfileDescription,
                    _viewAnalyze.DlpDescription,
                    Convert.ToInt32(EOptimizeRunStatus.Closed),
                    DateTime.Now);
        }
    }
}