using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.GlobalResources;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujiFilmUSA.Sync.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class AnalyzeController : BaseController, IViewAnalyze
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        private PresenterReportCMYKDensity _presenterReportCMYKDensity;
        private Dictionary<string, string> _resourceValues { get; set; }
        private Dictionary<string, string> _globalResourceValues { get; set; }
        private const string _InvalidMeasurementId = "Error_Invalid_MeasurementId";
        private const string _InvalidIsAverageOfMeasurements = "Error_Invalid_IsAverageOfMeasurements";
        private const string _InvalidIsVerificationProject = "Error_Invalid_IsVerificationProject";
        private const string _InvalidIsSync1DTypeProject = "Error_Invalid_IsSync1DTypeProject";
        private const string _InvalidDeltaEValue = "Error_Invalid_DeltaEValue";
        private const string _InvalidValidationType = "Error_Invalid_ValidationType";
        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
            _globalResourceValues = new Dictionary<string, string>();
            _resourceValues = new Dictionary<string, string>();
            _resourceValues.Add("_recgbPatchMetrics", "gbPatchMetricsText");
            _resourceValues.Add("_recActual", "actualText");
            _resourceValues.Add("_recL", "lText");
            _resourceValues.Add("_recA", "aText");
            _resourceValues.Add("_recB", "bText");
            _resourceValues.Add("_recDeltaE", "deltaEText");
            _resourceValues.Add("_recTarget", "targetText");
            _resourceValues.Add("_recPrevious", "PreviousText");
            _resourceValues.Add("_recDeltaL", "deltaLText");
            _resourceValues.Add("_recDeltaA", "deltaAText");
            _resourceValues.Add("_recDeltaB", "deltaBText");
            _resourceValues.Add("_recPreDeltaE", "PreDeltaE");

            _resourceValues.Add("_recCyanPathMetrics", "cyanPathMetrics");
            _resourceValues.Add("_recMagentaPathMetrics", "magentaPathMetrics");
            _resourceValues.Add("_recYellowPathMetrics", "yellowPathMetrics");
            _resourceValues.Add("_recRedPathMetrics", "redPathMetrics");
            _resourceValues.Add("_recGreenPathMetrics", "greenPathMetrics");
            _resourceValues.Add("_recBluePathMetrics", "bluePathMetrics");
            _resourceValues.Add("_recBlackPathMetrics", "blackPathMetrics");
            _resourceValues.Add("_recPaperPathMetrics", "paperPathMetrics");

            _resourceValues.Add("_recCyan", "cyanText");
            _resourceValues.Add("_recMagenta", "magentaText");
            _resourceValues.Add("_recYellow", "yellowText");
            _resourceValues.Add("_recBlack", "blackText");
            _resourceValues.Add("_recRed", "redText");
            _resourceValues.Add("_recGreen", "greenText");
            _resourceValues.Add("_recBlue", "blueText");
            _resourceValues.Add("_recPaper", "paperText");
            _resourceValues.Add("_recTriplet", "tripletText");
            _resourceValues.Add("_recTripletPathMetrics", "tripletPathMetrics");

            _resourceValues.Add("_recLBalanceChartTitle", "lBalanceChartTitle");
            _resourceValues.Add("_recABalanceChartTitle", "aBalanceChartTitle");
            _resourceValues.Add("_recBBalanceChartTitle", "bBalanceChartTitle");

            //for patch details grid in spot analyze page
            _resourceValues.Add("gridAnalyzeHeaderIDResource", "gridAnalyzeHeaderIDResource");
            _resourceValues.Add("gridAnalyzeHeaderPatchResource", "gridAnalyzeHeaderPatchResource");
            _resourceValues.Add("gridAnalyzeHeaderViewResource", "gridAnalyzeHeaderViewResource");
            _resourceValues.Add("gridAnalyzeHeaderSpotNameResource", "gridAnalyzeHeaderSpotNameResource");
            _resourceValues.Add("gridAnalyzeHeaderETPResource", "gridAnalyzeHeaderETPResource");
            _resourceValues.Add("gridAnalyzeHeaderEMTResource", "gridAnalyzeHeaderEMTResource");
            _resourceValues.Add("gridAnalyzeHeaderEMPResource", "gridAnalyzeHeaderEMPResource");
            _resourceValues.Add("gridAnalyzeHeaderDeltaLResource", "gridAnalyzeHeaderDeltaLResource");
            _resourceValues.Add("gridAnalyzeHeaderDeltaAResource", "gridAnalyzeHeaderDeltaAResource");
            _resourceValues.Add("gridAnalyzeHeaderDeltaBesource", "gridAnalyzeHeaderDeltaBesource");
            _resourceValues.Add("gridAnalyzeHeaderDeltaEResource", "gridAnalyzeHeaderDeltaEResource");
            _resourceValues.Add("gridAnalyzeToolTipViewResource", "gridAnalyzeToolTipViewResource");
            _resourceValues.Add("gridAnalyzeHeader_C_Resource", "gridAnalyzeHeader_C_Resource");
            _resourceValues.Add("gridAnalyzeHeader_M_Resource", "gridAnalyzeHeader_M_Resource");
            _resourceValues.Add("gridAnalyzeHeader_Y_Resource", "gridAnalyzeHeader_Y_Resource");
            _resourceValues.Add("gridAnalyzeHeader_K_Resource", "gridAnalyzeHeader_K_Resource");
            _resourceValues.Add("gridAnalyzeHeader_E_Resource", "gridAnalyzeHeader_E_Resource");
            _resourceValues.Add("gridAnalyzeHeaderE94Resource", "gridAnalyzeHeaderE94Resource");
            _resourceValues.Add("gridAnalyzeHeaderE2000Resource", "gridAnalyzeHeaderE2000Resource");
            _resourceValues.Add("gridAnalyzeHeaderTargetLResource", "gridAnalyzeHeaderTargetLResource");
            _resourceValues.Add("gridAnalyzeHeaderCIELAB_LResource", "gridAnalyzeHeaderCIELAB_LResource");
            _resourceValues.Add("gridAnalyzeHeaderTargetAResource", "gridAnalyzeHeaderTargetAResource");
            _resourceValues.Add("gridAnalyzeHeaderCIELAB_AResource", "gridAnalyzeHeaderCIELAB_AResource");
            _resourceValues.Add("gridAnalyzeHeaderTargetBResource", "gridAnalyzeHeaderTargetBResource");
            _resourceValues.Add("gridAnalyzeHeaderCIELAB_BResource", "gridAnalyzeHeaderCIELAB_BResource");
            _resourceValues.Add("gridAnalyzeHeaderDensity_VResource", "gridAnalyzeHeaderDensity_VResource");
            _resourceValues.Add("gridAnalyzeHeaderDensity_CResource", "gridAnalyzeHeaderDensity_CResource");
            _resourceValues.Add("gridAnalyzeHeaderDensity_MResource", "gridAnalyzeHeaderDensity_MResource");
            _resourceValues.Add("gridAnalyzeHeaderDensity_YResource", "gridAnalyzeHeaderDensity_YResource");
            _resourceValues.Add("gridAnalyzeHeaderDelta_HResource", "gridAnalyzeHeaderDelta_HResource");
            _resourceValues.Add("gridAnalyzeHeaderDelta_CHResource", "gridAnalyzeHeaderDelta_CHResource");
            _resourceValues.Add("gridAnalyzeHeaderDelta_LResource", "gridAnalyzeHeaderDelta_LResource");
            _resourceValues.Add("selectMeasurementMsg", "selectMeasurementMsg");


            base.Initialize(controllerContext);
        }

        [HttpGet]
        public IHttpActionResult GetMeasurementSummaryDetails(int? MeasurementId, string fromDeltaE, bool? IsVerificationProject)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<List<MeasurementPatch_AvgDelta>> httpResponse = new BaseResponse<List<MeasurementPatch_AvgDelta>>();
            string validationMessage = "";
            try
            {
                if (MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (IsVerificationProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsVerificationProject);

                if (string.IsNullOrEmpty(Convert.ToString(fromDeltaE)))
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidDeltaEValue);

                this.MeasurementId = Convert.ToInt32(MeasurementId);
                this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                this.From_ΔE = fromDeltaE;

                MeasurementsDelta = new List<MeasurementPatch_AvgDelta>();
                _presenterAnalyze.GetAvgDeltaMeasurement_Optimization();
                httpResponse.Data = MeasurementsDelta;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }



        [HttpGet]
        public IHttpActionResult GetValidationMeasurementSummaryDetails(int? MeasurementId, string fromDeltaE, bool? IsVerificationProject, bool? IsSync1DTypeProject, bool? IsAverageOfMeasurements)
        {
            string validationMessage = "";
            IHttpActionResult actionResult = null;
            BaseResponse<List<Measurement>> httpResponse = new BaseResponse<List<Measurement>>();
            try
            {
                if (MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (IsAverageOfMeasurements == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsAverageOfMeasurements);

                if (IsVerificationProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsVerificationProject);

                if (IsSync1DTypeProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsSync1DTypeProject);

                if (string.IsNullOrEmpty(Convert.ToString(fromDeltaE)))
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidDeltaEValue);

                this.MeasurementId = Convert.ToInt32(MeasurementId);
                this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                this.IsAverageOfMeasurements = IsAverageOfMeasurements == null ? false : Convert.ToBoolean(IsAverageOfMeasurements);
                this.From_ΔE = fromDeltaE;

                _presenterAnalyze.GetAvgDeltaMeasurement_Validation();
                httpResponse.Data = MeasurementSummary;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        #region IViewAnalyze Members
        public int ProjectId { get; set; }

        public int OptimizeRunId { get; set; }
        public List<int> MeasurementIds { get; set; }
        public List<Measurement> Measurements { get; set; }
        public Measurement Measurement { get; set; }
        public string DestinationProfileDescription { get; set; }
        public string DlpDescription { get; set; }
        public List<MeasurementPatch_AvgDelta> MeasurementsDelta { get; set; }
        public List<MeasurementPatchDetails> PatchDetails { get; set; }
        public int MeasurementId { get; set; }
        public string From_ΔE { get; set; }
        public List<AnalyzeChart> DTDeltaEPrimaryPatches { get; set; }
        public List<AnalyzeChart> DTPaperPatches { get; set; }
        public List<AnalyzeChart> DTDensity { get; set; }
        public List<AnalyzeChart_Gray> DTLABChart { get; set; }
        public List<AnalyzeChart_Gray> DTGaumatChart1 { get; set; }
        public List<AnalyzeChart_Gray> DTGaumatChart2 { get; set; }
        public bool IsVerification { get; set; }


        public List<Measurement> MeasurementSummary { get; set; }

        public List<AnalyzeChart_G7> GrayFChart { get; set; }
        public List<AnalyzeChart_G7> GrayFSummary { get; set; }
        public List<AnalyzeChart_G7> BlackLChart { get; set; }
        public List<AnalyzeChart_G7> BlackLSummary { get; set; }
        public List<AnalyzeChart_G7> GrayLChart { get; set; }
        public List<AnalyzeChart_G7> GrayLSummary { get; set; }
        public List<AnalyzeChart_G7> BlackNPDChart { get; set; }
        public List<AnalyzeChart_G7> GrayNPDChart { get; set; }

        public DataTable dtPatchDetails { get; set; }
        public DataTable dtFilteredPatchDetails { get; set; }
        public bool useDLP { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public bool IsSync1DType { get; set; }
        public PCSDataTypes PCSDataTypes { get; set; }
        public DensityTypes DensityType { get; set; }

        public List<AnalyzeChart_DotGain> DTTVIChart { get; set; }

        public List<AnalyzeChart_DotGain> DTISOTVIChart { get; set; }
        public List<AnalyzeChart_DotGain> DTSpreadChartData { get; set; }
        public List<AnalyzeChart_DotGain> DTSpreadSplineRangeChartData { get; set; }

        public List<AnalyzeChart_DotGain> DTSpreadChartData_CMYK { get; set; }

        public DataTable DTMaxSpreadChartData { get; set; }

        public bool IsAverageOfMeasurements { get; set; }
        public bool Is1DMeasurementSmoothing { get; set; }
        public int ProjectStatus { get; set; }
        public Project Project { get; set; }
        public int SpotProjectId { get; set; }
        public bool IsUsed { get; set; }
        #endregion
    }
}
