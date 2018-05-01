using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.GlobalResources;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujiFilmUSA.Sync.Service.Models;
//using FujiFilmUSA.Sync.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class ChartController : BaseController, IViewAnalyze, IViewReportCMYKDensity, IViewExport
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        private PresenterReportCMYKDensity _presenterReportCMYKDensity;
        private PresenterExport _presenterExport;
        private Dictionary<string, string> _resourceValues { get; set; }
        private Dictionary<string, string> _globalResourceValues { get; set; }
        private const string _InvalidMeasurementId = "Error_Invalid_MeasurementId";
        private const string _InvalidIsAverageOfMeasurements = "Error_Invalid_IsAverageOfMeasurements";
        private const string _InvalidIsVerificationProject = "Error_Invalid_IsVerificationProject";
        private const string _InvalidIsSync1DTypeProject = "Error_Invalid_IsSync1DTypeProject";
        private const string _InvalidDeltaEValue = "Error_Invalid_DeltaEValue";
        private const string _InvalidValidationType = "Error_Invalid_ValidationType";
        private const string _InvalidOptimizationRunID = "Error_Invalid_OptimizationRunID";
        private const string _InvalidCurveType = "Error_Invalid_CurveType";

        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
            _presenterReportCMYKDensity = new PresenterReportCMYKDensity(this);
            _presenterExport = new PresenterExport(this);
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
            _resourceValues.Add("_recPatchMetrics", "patchMetricsText");
            _resourceValues.Add("_recactualTVI", "actualTVI");
            _resourceValues.Add("_rectargetTVI", "targetTVI");
            _resourceValues.Add("_recTVIThr", "TVIThr");
            _resourceValues.Add("_recpercentPatchMetrics", "percentPatchMetrics");
            _resourceValues.Add("selectMeasurementMsg", "selectMeasurementMsg");


            base.Initialize(controllerContext);
        }

        [HttpGet]
        public IHttpActionResult GenerateAnalyzeChart(int? ProjectId, int? OptimizeRunId, bool? IsSync1DTypeProject, string fromDeltaE, int ProjectType)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                //if (MeasurementId == null)
                //    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (string.IsNullOrEmpty(Convert.ToString(fromDeltaE)))
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidDeltaEValue);

                if (IsSync1DTypeProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsSync1DTypeProject);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    this.From_ΔE = Convert.ToString(fromDeltaE);
                    //this.MeasurementId = Convert.ToInt32(MeasurementId);
                    if (ProjectType == (int)ELicenseType.Validation)
                    {
                        //This will be called when Verification process is going on
                        this.ProjectId = Convert.ToInt32(ProjectId);
                        _presenterAnalyze.GetAnalyzeChartVMeasurement();
                    }
                    else
                    {
                        //In case of None (i.e. Optimization or Sync1D)
                        this.OptimizeRunId = Convert.ToInt32(OptimizeRunId);
                        _presenterAnalyze.GetAnalyzeChartMeasurement();
                    }
                    GenerateResources();

                    httpResponse.Data = new
                    {
                        Measurements = Measurements,
                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }


        [HttpGet]
        public IHttpActionResult GenerateGrayChart(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject, bool? IsSync1DTypeProject)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
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

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    //temp code start
                    //SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.Validation;
                    //temp code end
                    //IsVerification = true;
                    DetermineCulture();
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsAverageOfMeasurements = Convert.ToBoolean(IsAverageOfMeasurements);
                    _presenterAnalyze.GetGrayChart();
                    GenerateResources();

                    httpResponse.Data = new
                    {
                        GamutChartActual = DTGaumatChart1,
                        GamutChart = DTGaumatChart2,
                        LABChart = DTLABChart,
                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        public IHttpActionResult GetSpotPatchDetails(int id)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                DetermineCulture();
                MeasurementId = 0;
                OptimizeRunId = Convert.ToInt32(id);
                _presenterAnalyze.GetSpotPatchDetailsofMeasurement();

                //Now we are using DataTable as DataSource                    
                SpotAnalyze spotAnalyzeDetails = new SpotAnalyze();
                decimal totalDeltaE = 0;
                decimal avgDeltaE = 0;
                int measurementID = 0;
                string measurementName = "";
                //count Avg, Max and Min ΔE
                decimal minDtE = int.MaxValue;
                decimal maxDtE = int.MinValue;

                if (dtPatchDetails.Rows.Count > 0)
                {
                    //get the measurement id 
                    measurementID = Convert.ToInt32(dtPatchDetails.Rows[0]["MeasurementID"]);
                    //if (measurementID > 0)
                    //    measurementName = Convert.ToString(dtPatchDetails.Rows[0]["MeasurementName"]);

                    Dictionary<string, object> childRow;
                    foreach (DataRow row in dtPatchDetails.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in dtPatchDetails.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }

                        //decimal DtE = 0;
                        //if (measurementID > 0)
                        //    DtE = string.IsNullOrEmpty(Convert.ToString(row.Field<decimal?>("ΔE_M_T"))) ? 0 : row.Field<decimal>("ΔE_M_T");//for run-1,2,.....
                        //else
                        //    DtE = row.Field<decimal?>("ΔE") == null ? 0 : row.Field<decimal>("ΔE");//only for run-0

                        //totalDeltaE += DtE;
                        //minDtE = Math.Min(minDtE, DtE);
                        //maxDtE = Math.Max(maxDtE, DtE);

                        spotAnalyzeDetails.patchDetails.Add(childRow);
                    }
                    //avgDeltaE = totalDeltaE / dtPatchDetails.Rows.Count;
                    spotAnalyzeDetails.measurementID = Convert.ToInt32(dtPatchDetails.Rows[0]["MeasurementID"]);
                    //spotAnalyzeDetails.measurementName = measurementName;
                    //if (spotAnalyzeDetails.measurementID > 0)
                    //{

                    //    spotAnalyzeDetails.maxDeltaETP = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxETP"]));
                    //    spotAnalyzeDetails.minDeltaETP = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinETP"]));
                    //    spotAnalyzeDetails.avgDeltaETP = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgETP"]));

                    //    spotAnalyzeDetails.maxDeltaEMT = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxEMT"]));
                    //    spotAnalyzeDetails.minDeltaEMT = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinEMT"]));
                    //    spotAnalyzeDetails.avgDeltaEMT = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgEMT"]));

                    //    spotAnalyzeDetails.maxDeltaEMP = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxEMP"]));
                    //    spotAnalyzeDetails.minDeltaEMP = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinEMP"]));
                    //    spotAnalyzeDetails.avgDeltaEMP = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgEMP"]));

                    //}
                    //else
                    //{
                    //    spotAnalyzeDetails.minDeltaE = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinE"]));
                    //    spotAnalyzeDetails.maxDeltaE = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxE"]));
                    //    spotAnalyzeDetails.avgDeltaE = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgE"]));
                    //}
                }


                //spotAnalyzeDetails.minDeltaE = minDtE.ToString();
                //spotAnalyzeDetails.maxDeltaE = maxDtE.ToString();
                //spotAnalyzeDetails.avgDeltaE = String.Format("{0:0.###}", avgDeltaE);
                GenerateResources();
                var objGroup = new
                {
                    spotAnalyzeDetails = spotAnalyzeDetails,
                    GlobalResources = _globalResourceValues
                };
                httpResponse.Data = objGroup;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        [HttpGet]
        public IHttpActionResult GenerateInkPaperChart(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject, bool? IsSync1DTypeProject, string fromDeltaE)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
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


                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();
                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    this.IsAverageOfMeasurements = IsAverageOfMeasurements == null ? false : Convert.ToBoolean(IsAverageOfMeasurements);
                    this.From_ΔE = fromDeltaE;

                    _presenterAnalyze.GetInkPaperChart();
                    GenerateResources();
                    httpResponse.Data = new
                    {
                        DeltaEPrimaryPatchList = DTDeltaEPrimaryPatches,
                        PaperPatchList = DTPaperPatches,
                        DensityList = DTDensity,
                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }


        [HttpGet]
        public IHttpActionResult GenerateG7Chart(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                if (MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (IsAverageOfMeasurements == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsAverageOfMeasurements);

                if (IsVerificationProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsVerificationProject);

                //if (string.IsNullOrEmpty(Convert.ToString(validationType)))
                //    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidValidationType);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsAverageOfMeasurements = Convert.ToBoolean(IsAverageOfMeasurements);

                    //-------------Only call G7 tab data if it is G7 type of Validation / Sync 1D / Sync 4D Project
                    _presenterAnalyze.GetG7Chart();

                    //Get resource values
                    GenerateResources();

                    httpResponse.Data = new
                    {
                        GrayFChartList = GrayFChart,
                        GrayFSummaryList = GrayFSummary,
                        BlackLChartList = BlackLChart,
                        BlackLSummaryList = BlackLSummary,
                        GrayLChartList = GrayLChart,
                        GrayLSummaryList = GrayLSummary,
                        BlackNPDChartList = BlackNPDChart,
                        GrayNPDChartList = GrayNPDChart,

                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        [HttpGet]
        public IHttpActionResult GenerateTVIChart(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject, bool? IsSync1DTypeProject)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
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

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();

                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    this.IsAverageOfMeasurements = IsAverageOfMeasurements == null ? false : Convert.ToBoolean(IsAverageOfMeasurements);

                    //-------------G7 type for validation type 1
                    _presenterAnalyze.GetTVIChart();

                    //Get resource values
                    GenerateResources();

                    httpResponse.Data = new
                    {
                        DTTVIChartList = DTTVIChart,
                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        [HttpGet]
        public IHttpActionResult GenerateISOTVIChart(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject, bool? IsSync1DTypeProject)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
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

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();

                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    this.IsAverageOfMeasurements = IsAverageOfMeasurements == null ? false : Convert.ToBoolean(IsAverageOfMeasurements);

                    //-------------G7 type for validation type 1
                    _presenterAnalyze.GetISOTVIChart();

                    //Get resource values
                    GenerateResources();

                    httpResponse.Data = new
                    {
                        DTISOTVIChartList = DTISOTVIChart,
                        DTSpreadChartDataList = DTSpreadChartData,
                        DTSpreadChartData_CMYKList = DTSpreadChartData_CMYK,
                        DTSpreadSplineRangeChartDataList = DTSpreadSplineRangeChartData,
                        DTMaxSpreadChartDataList = DTMaxSpreadChartData,
                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }


        [HttpGet]
        public IHttpActionResult GetMeasurementPatchDetails(int? MeasurementId, bool? IsVerificationProject, bool? IsSync1DTypeProject, string metricSumID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            string validationMessage = "";
            List<Dictionary<string, object>> patchDetails = new List<Dictionary<string, object>>();
            try
            {
                if (MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (IsSync1DTypeProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsSync1DTypeProject);

                if (IsVerificationProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsVerificationProject);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();
                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    _presenterAnalyze.GetPatchDetailsofMeasurement();

                    if (!string.IsNullOrEmpty(metricSumID) && Convert.ToInt32(metricSumID) != -1)
                        dtFilteredPatchDetails = FilteredPatchDetailsTable(metricSumID, dtPatchDetails);
                    else
                        dtFilteredPatchDetails = dtPatchDetails;

                    if (dtFilteredPatchDetails.Rows.Count > 0)
                    {
                        Dictionary<string, object> childRow;
                        foreach (DataRow row in dtFilteredPatchDetails.Rows)
                        {
                            childRow = new Dictionary<string, object>();
                            foreach (DataColumn col in dtFilteredPatchDetails.Columns)
                            {
                                childRow.Add(col.ColumnName, row[col]);
                            }
                            patchDetails.Add(childRow);
                        }
                    }
                    GenerateResources();
                    var objGroup = new
                    {
                        measurementPatchDetails = patchDetails,
                        GlobalResources = _globalResourceValues
                    };
                    httpResponse.Data = objGroup;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }


        [HttpGet]
        public ValidationMetrics[] GetValidationMetrics(int? MeasurementId, string fromDeltaE, bool? IsAverageOfMeasurements, bool? IsVerificationProject,
                                                           bool? IsSync1DTypeProject)
        {
            ValidationMetrics[] matricsArray = null;
            try
            {
                this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                this.MeasurementId = Convert.ToInt32(MeasurementId);
                this.From_ΔE = fromDeltaE;
                this.IsAverageOfMeasurements = Convert.ToBoolean(IsAverageOfMeasurements);

                //Get Ink gray chart details
                _presenterAnalyze.GetAvgDeltaMeasurement_Validation();
                matricsArray = MeasurementSummary[0].ValidationMetrics.ToArray();

                return matricsArray;

            }
            catch (Exception ex)
            {
            }
            return matricsArray;
        }

        [HttpGet]
        public IHttpActionResult UpdateMeasurementFromAnalyzeChart(int? MeasurementId, bool? isMeasurementUsed, bool? IsVerificationProject, bool? IsSync1DTypeProject)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<bool> httpResponse = new BaseResponse<bool>();
            string validationMessage = "";
            bool isSuccess = false;
            try
            {
                if (MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (IsSync1DTypeProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsSync1DTypeProject);

                if (IsVerificationProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsVerificationProject);

                this.MeasurementId = Convert.ToInt32(MeasurementId);
                this.IsUsed = isMeasurementUsed == null ? false : Convert.ToBoolean(isMeasurementUsed); ;
                this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                _presenterAnalyze.UpdateMeasurementByAnalyzeChartFromService();
                isSuccess = true;

                httpResponse.Data = isSuccess;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        [HttpGet]
        public IHttpActionResult GetColorDensityChart(int? OptimizationRunId)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            CMYKDensity colorDensity = new CMYKDensity();
            try
            {
                if (OptimizationRunId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidOptimizationRunID);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();
                    DSDensity_C = new DataTable();
                    DSDensity_M = new DataTable();
                    DSDensity_Y = new DataTable();
                    DSDensity_K = new DataTable();

                    this.OptimizeRunId = Convert.ToInt32(OptimizationRunId);
                    _presenterReportCMYKDensity.GetDensityData_C();

                    colorDensity.CyanList = GetColorDensityDetails(DSDensity_C);
                    colorDensity.MagentaList = GetColorDensityDetails(DSDensity_M);
                    colorDensity.YellowList = GetColorDensityDetails(DSDensity_Y);
                    colorDensity.BlackList = GetColorDensityDetails(DSDensity_K);

                    //Get resource values
                    GenerateResources();
                    httpResponse.Data = new
                    {
                        CMYKDensity = colorDensity,
                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        [HttpGet]
        public IHttpActionResult GenerateCurveChart(int? OptimizationRunId, int? CurveType)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                if (OptimizationRunId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidOptimizationRunID);

                if (CurveType == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidCurveType);


                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();
                    this.OptimizeRunId = Convert.ToInt32(OptimizationRunId);
                    this.CurveTypeId = Convert.ToInt16(CurveType);
                    _presenterExport.GetCurveData();
                    GenerateResources();
                    httpResponse.Data = new
                    {
                        CurveExportView = CurveData.CurveExportView,
                        CurveDeltaRefView = CurveData.CurveDeltaRefView,
                        CurveDeltaPrevRunView = CurveData.CurveDeltaPrevRunView,
                        GlobalResources = _globalResourceValues
                    };

                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }


        [HttpGet]
        public IHttpActionResult ExportGenerateCurveChart(int? OptimizationRunId, int? CurveType)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                if (OptimizationRunId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidOptimizationRunID);

                if (CurveType == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidCurveType);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    DetermineCulture();
                    this.OptimizeRunId = Convert.ToInt32(OptimizationRunId);
                    this.CurveTypeId = Convert.ToInt16(CurveType);
                    if(Convert.ToInt16(CurveType)==100 || Convert.ToInt16(CurveType) == 101)
                        this.CurveTypeId = 5;
                    if (Convert.ToInt16(CurveType) == 102)
                        this.CurveTypeId = 45;

                    _presenterExport.GetCurveData();

                    string CurveFileName = "";
                    string CurveFilePath = HostingEnvironment.MapPath("~/UploadFiles/CurvesFile/");
                    string ReportFileName = "";
                    if (!File.Exists(CurveFilePath))
                        Directory.CreateDirectory(CurveFilePath);

                    if (CurveType == 1 || CurveType == 5 || CurveType == 10)
                    {
                        #region Create file and download txt file for 1%,5% and 10%
                        CurveFileName = OptimizationRunId.ToString() + "_CurvesFile.txt";
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(CurveFilePath + CurveFileName);
                        //const string quote = "\"";
                        sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                        sw.WriteLine("BEGIN_DATA_FORMAT");
                        sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                        //sw.WriteLine("SampleID\tSampleName\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                        sw.WriteLine("END_DATA_FORMAT");
                        sw.WriteLine("NUMBER_OF_SETS\t" + CurveData.CurveExportView.Count);
                        sw.WriteLine("BEGIN_DATA");
                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            sw.WriteLine(CurveData.CurveExportView[i].ReferenceNo.ToString() + "\t" + CurveData.CurveExportView[i].CMYK_C.ToString() + "\t" + CurveData.CurveExportView[i].CMYK_M.ToString() + "\t" + CurveData.CurveExportView[i].CMYK_Y.ToString() + "\t" + CurveData.CurveExportView[i].CMYK_K.ToString());
                        }
                        sw.WriteLine("END_DATA");
                        sw.Close();
                        #endregion
                    }
                    else if (CurveType == 15)  // Rampage
                    {
                        #region Create Zip file and download file for rampage
                        CurveFileName = OptimizationRunId.ToString() + "_Curves_CMYKFile";
                        ReportFileName = "/UploadFiles/CurvesFile/" + CurveFileName + ".zip";
                        string zipCurveFileName = CurveFileName + ".zip";
                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;
                        swC = System.IO.File.CreateText(HostingEnvironment.MapPath("~/UploadFiles/CurvesFile/") + CurveFileName + "_C.oli");
                        swC.WriteLine("2");
                        swC.WriteLine("0.0000000000");
                        swC.WriteLine("0.0000000000");
                        swC.WriteLine("25");

                        swM = System.IO.File.CreateText(HostingEnvironment.MapPath("~/UploadFiles/CurvesFile/") + CurveFileName + "_M.oli");
                        swM.WriteLine("2");
                        swM.WriteLine("0.0000000000");
                        swM.WriteLine("0.0000000000");
                        swM.WriteLine("25");

                        swY = System.IO.File.CreateText(HostingEnvironment.MapPath("~/UploadFiles/CurvesFile/") + CurveFileName + "_Y.oli");
                        swY.WriteLine("2");
                        swY.WriteLine("0.0000000000");
                        swY.WriteLine("0.0000000000");
                        swY.WriteLine("25");

                        swK = System.IO.File.CreateText(HostingEnvironment.MapPath("~/UploadFiles/CurvesFile/") + CurveFileName + "_K.oli");
                        swK.WriteLine("2");
                        swK.WriteLine("0.0000000000");
                        swK.WriteLine("0.0000000000");
                        swK.WriteLine("25");

                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_C).ToString("0.0"));
                            swM.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_M).ToString("0.0"));
                            swY.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_Y).ToString("0.0"));
                            swK.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_K).ToString("0.0"));
                        }
                        swC.WriteLine("Version: 2.0");
                        swC.Close();

                        swM.WriteLine("Version: 2.0");
                        swM.Close();

                        swY.WriteLine("Version: 2.0");
                        swY.Close();

                        swK.WriteLine("Version: 2.0");
                        swK.Close();

                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_C.oli");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_M.oli");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Y.oli");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_K.oli");

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CMYKFile.zip", CurveFileName + "_CMYKFile.zip");
                        #endregion
                    }
                    else if (CurveType == 25)  // for XMF
                    {
                        #region Create file and download txt file for XMF
                        CurveFileName = OptimizationRunId.ToString() + "_CurvesFile.csv";
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(CurveFilePath + CurveFileName);
                        sw.WriteLine("Cyan;Magenta;Yellow;Black");
                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            sw.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_C).ToString("0.0000") + ";" + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_M).ToString("0.0000") + ";" + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_Y).ToString("0.0000") + ";" + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_K).ToString("0.0000"));
                        }
                        sw.Close();

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurvesFile.csv", CurveFileName + "_CurvesFile.csv");
                        #endregion

                    }
                    else if (CurveType == 35)  // Harmony (Prinergy)
                    {
                        #region Create Zip file and download file for Harmony

                        CurveFileName = OptimizationRunId.ToString() + "_Curves_CMYKFile";
                        ReportFileName = "/UploadFiles/CurvesFile/" + CurveFileName + "_Harmony.zip";
                        string zipCurveFileName = CurveFileName + "_Harmony.zip";

                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;

                        //cyan
                        swC = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_C.txt");

                        //magenta
                        swM = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_M.txt");

                        //yellow    
                        swY = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Y.txt");

                        //black
                        swK = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_K.txt");

                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_C).ToString("0.00"));
                            swM.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_M).ToString("0.00"));
                            swY.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_Y).ToString("0.00"));
                            swK.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_K).ToString("0.00"));
                        }

                        swC.Close();
                        swM.Close();
                        swY.Close();
                        swK.Close();

                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_C.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_M.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Y.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_K.txt");

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_Harmony.zip", CurveFileName + "_Harmony.zip");
                        #endregion
                    }
                    else if (CurveType == 45)  // nexus
                    {
                        #region Create Zip file and download file for nexus

                        CurveFileName = OptimizationRunId.ToString() + "_Curves_CMYKFile";
                        ReportFileName = "/UploadFiles/CurvesFile/" + CurveFileName + "_Nexus.zip";
                        string zipCurveFileName = CurveFileName + "_Nexus.zip";

                        //check if file exists and delete it
                        if (System.IO.File.Exists(CurveFilePath + zipCurveFileName))
                            System.IO.File.Delete(CurveFilePath + zipCurveFileName);

                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;
                        swC = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Nexus_Cya.txt");
                        swM = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Nexus_Mag.txt");
                        swY = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Nexus_Yel.txt");
                        swK = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Nexus_Bla.txt");


                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_C).ToString("0.0000"));
                            swM.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_M).ToString("0.0000"));
                            swY.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_Y).ToString("0.0000"));
                            swK.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_K).ToString("0.0000"));
                        }

                        swC.Close();
                        swM.Close();
                        swY.Close();
                        swK.Close();

                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Nexus_Cya.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Nexus_Mag.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Nexus_Yel.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Nexus_Bla.txt");

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_Nexus.zip", CurveFileName + "_Nexus.zip");
                        #endregion
                    }
                    else if (CurveType == 55)  // for sericol UK Caldera
                    {
                        #region Create file and download txt file for sericol UK
                        CurveFileName = OptimizationRunId.ToString() + "_CurvesFile.ppc";

                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(CurveFilePath + CurveFileName);
                        sw.WriteLine("Device = CMYK");
                        sw.WriteLine("Max ink = 400");
                        sw.WriteLine("Max ink reduction = preserve black");

                        string strCyan = "";
                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {

                            strCyan = strCyan + "(" + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_C).ToString("0.0") + ") , ";

                        }
                        strCyan = strCyan.Remove(strCyan.Length - 3, 3);
                        sw.WriteLine("Cyan curve = " + strCyan);

                        string strMagenta = "";
                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {

                            strMagenta = strMagenta + "(" + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_M).ToString("0.0") + ") , ";

                        }
                        strMagenta = strMagenta.Remove(strMagenta.Length - 3, 3);
                        sw.WriteLine("Magenta curve = " + strMagenta);

                        string strYellow = "";
                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {

                            strYellow = strYellow + "(" + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_Y).ToString("0.0") + ") , ";

                        }
                        strYellow = strYellow.Remove(strYellow.Length - 3, 3);
                        sw.WriteLine("Yellow curve = " + strYellow);

                        string strBlack = "";
                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {

                            strBlack = strBlack + "(" + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_K).ToString("0.0") + ") , ";

                        }
                        strBlack = strBlack.Remove(strBlack.Length - 3, 3);
                        sw.WriteLine("Black curve = " + strBlack);


                        sw.Close();

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurvesFile.ppc", CurveFileName + "_CurvesFile.ppc");
                        #endregion

                    }
                    else if (CurveType == 100)  // for ColorGate-TVI
                    {
                        #region Create file and download .ctvx file for ColorGate-TVI

                        //-------For curvetype=100-------Make .ctvx XML file for ColorGate-TVI (Dhruti's Code)-------------------//
                        CurveFileName = OptimizationRunId.ToString() + "_CurveDelta.ctvx";
                        //-----------------Pass few parameters in "" marks to .ctvx xml file------------------//
                        string str0 = "\"" + "1.0" + "\"";
                        string str1 = "\"" + "UTF-8" + "\"";
                        string str2 = "\"" + "urn:schemas-colorgate-com:tonalvalues" + "\"";
                        string str3 = "\"" + "1" + "\"";

                        //-----------------Make .ctvx xml file------------------//
                        System.IO.StreamWriter sw3;
                        sw3 = System.IO.File.CreateText(CurveFilePath + CurveFileName);
                        sw3.WriteLine("<?xml version=" + str0 + " encoding=" + str1 + "?>");
                        sw3.WriteLine("<TonalValues xmlns=" + str2 + ">");

                        if (CurveData.CurveExportView.Count > 19)
                        {
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>0</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_K.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_K.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_K.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_K.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_K.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>1</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_C.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_C.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_C.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_C.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_C.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>2</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_M.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_M.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_M.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_M.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_M.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>3</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_Y.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_Y.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_Y.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_Y.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_Y.ToString()) - Convert.ToDecimal(CurveData.CurveExportView[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        }
                        sw3.WriteLine("</TonalValues>");
                        sw3.Close();

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurveDelta.ctvx", CurveFileName + "_CurveDelta.ctvx");

                        #endregion
                    }
                    else if (CurveType == 101)  // for ColorGate-CGCX
                    {
                        #region Create file and download .cgcx file for ColorGate-CGCX

                        CurveFileName = OptimizationRunId.ToString() + "_CurveDelta.cgcx";
                        //-------For curvetype=101-------Make .cgcx XML file for ColorGate-CGCX (Dhruti's Code)-------------------//

                        //-----------------Pass few parameters in "" marks to .cgcx xml file------------------//
                        string str0 = "\"" + "1.0" + "\"";
                        string str1 = "\"" + "UTF-8" + "\"";
                        string str2 = "\"" + "urn:schemas-colorgate-com:gradation" + "\"";
                        string str3 = "\"" + "1" + "\"";

                        //-----------------Make .cgcx xml file------------------//
                        System.IO.StreamWriter sw3;
                        sw3 = System.IO.File.CreateText(CurveFilePath + CurveFileName);
                        sw3.WriteLine("<?xml version=" + str0 + " encoding=" + str1 + "?>");
                        sw3.WriteLine("<GradationCurves xmlns=" + str2 + ">");

                        if (CurveData.CurveExportView.Count > 19)
                        {
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>0</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>1</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>2</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>3</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[2].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[5].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[10].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[15].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(CurveData.CurveExportView[18].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>4</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>5</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>6</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>7</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>8</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        }
                        sw3.WriteLine("</GradationCurves>");
                        sw3.Close();

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurveDelta.cgcx", CurveFileName + "_CurveDelta.cgcx");

                        #endregion
                    }
                    else if (CurveType == 102)  // for Onyx G7 Import
                    {
                        #region Create file and download txt file for weighted highlight shadow and then 5% increment
                        CurveFileName = OptimizationRunId.ToString() + "_CurvesFile.txt";
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(CurveFilePath + CurveFileName);
                        sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                        sw.WriteLine("BEGIN_DATA_FORMAT");
                        sw.WriteLine("SampleID\tSampleName\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                        sw.WriteLine("END_DATA_FORMAT");
                        sw.WriteLine("NUMBER_OF_SETS\t" + CurveData.CurveExportView.Count.ToString());
                        sw.WriteLine("BEGIN_DATA");
                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            sw.WriteLine("A" + i.ToString() + "\t\"" + CurveData.CurveExportView[i].ReferenceNo.ToString() + ".00" + "\"\t" + CurveData.CurveExportView[i].CMYK_C.ToString() + "\t" + CurveData.CurveExportView[i].CMYK_M.ToString() + "\t" + CurveData.CurveExportView[i].CMYK_Y.ToString() + "\t" + CurveData.CurveExportView[i].CMYK_K.ToString());
                        }
                        sw.WriteLine("END_DATA");
                        sw.Close();

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurvesFile.txt", CurveFileName + "_CurvesFile.txt");
                        #endregion
                    }
                    if (string.IsNullOrEmpty(ReportFileName))
                        ReportFileName = "/UploadFiles/CurvesFile/" + CurveFileName;
                    httpResponse.Data = ReportFileName;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }

            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }


        [HttpGet]
        public IHttpActionResult GetSpotPatchDetailsReport(int OptimizationRunID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                DetermineCulture();
                MeasurementId = 0;
                OptimizeRunId = Convert.ToInt32(OptimizationRunID);
                _presenterAnalyze.GetSpotPatchDetailsofMeasurement();

                ReportPDF objReportPDF = new ReportPDF();
                objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                if (!Directory.Exists(objReportPDF.PDFPath))
                    Directory.CreateDirectory(objReportPDF.PDFPath);

                objReportPDF.dtSpotPatchDetails = dtPatchDetails;
                objReportPDF.PDF_NAME = OptimizationRunID.ToString() + "_ReportSpotPatchDetails.PDF";
                objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");

                objReportPDF.GenerateSpotPatchDetailsReport();
                string ReportPath = "UploadFiles/" + objReportPDF.PDF_NAME;
                httpResponse.status = ResponseStatus.Success;
                httpResponse.Data = ReportPath;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        [HttpGet]
        public HttpResponseMessage GetSpotPatchDetailsReportTest(int OptimizationRunID)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                DetermineCulture();
                MeasurementId = 0;
                OptimizeRunId = Convert.ToInt32(OptimizationRunID);
                _presenterAnalyze.GetSpotPatchDetailsofMeasurement();

                ReportPDF objReportPDF = new ReportPDF();
                objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                if (!Directory.Exists(objReportPDF.PDFPath))
                    Directory.CreateDirectory(objReportPDF.PDFPath);

                objReportPDF.dtSpotPatchDetails = dtPatchDetails;
                objReportPDF.PDF_NAME = OptimizationRunID.ToString() + "_ReportSpotPatchDetails.PDF";
                objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");

                objReportPDF.GenerateSpotPatchDetailsReport();
                string ReportPath = "UploadFiles/" + objReportPDF.PDF_NAME;

                //httpResponse.status = ResponseStatus.Success;
                //httpResponse.Data = ReportPath;
                //actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));

                var dataBytes = File.ReadAllBytes(HostingEnvironment.MapPath("~/" + ReportPath));
                //adding bytes to memory stream   
                var dataStream = new MemoryStream(dataBytes);
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                httpResponseMessage.Content = new StreamContent(dataStream);
                httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Accept-Ranges, Content-Encoding, Content-Length, Content-Range");
                httpResponseMessage.Content.Headers.ContentRange = new ContentRangeHeaderValue(dataStream.Length);
                httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = objReportPDF.PDF_NAME;
                httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            }
            catch (Exception ex)
            {
                //BaseErrorResponse errprResponse = new BaseErrorResponse();
                //errprResponse.Error.UserMessage = ex.Message;
                //errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                //actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return httpResponseMessage;
        }

        #region Method for generate resources
        public void GenerateResources()
        {
            foreach (var item in _resourceValues)
            {
                _globalResourceValues.Add(item.Key, GlobalResourceManager.Instance.GetGlobalResourceString(item.Value));
            }
        }
        public List<ColorDensity> GetColorDensityDetails(DataTable dt)
        {
            List<ColorDensity> list = new List<ColorDensity>();
            if (dt != null && dt.Rows.Count > 0)
                for (int index = 0; index < dt.Rows.Count; index++)
                    list.Add(new ColorDensity()
                    {
                        MeasurementID = Convert.ToInt32(dt.Rows[index]["MeasurementID"]),
                        Color = Convert.ToString(dt.Rows[index]["Color"]),
                        Density = Convert.ToDecimal(dt.Rows[index]["Density"]),
                        ΔE = Convert.ToDecimal(dt.Rows[index]["ΔE"]),
                        ΔElimit = Convert.ToDecimal(dt.Rows[index]["ΔElimit"])
                    });

            return list;
        }
        #endregion

        #region Method for Patch Details tab
        private DataTable FilteredPatchDetailsTable(string MetricSumID, DataTable dtPatchDetails)
        {
            DataTable DT = new DataTable();

            if (MetricSumID == "3")     //----For 'Paper ΔE Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 8)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "4")    //----For 'CMYK ΔE Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 1 || r.Field<byte>("PatchTypeID") == 2 || r.Field<byte>("PatchTypeID") == 3 || r.Field<byte>("PatchTypeID") == 4)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "5")    //----For 'CMYK ΔH Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 1 || r.Field<byte>("PatchTypeID") == 2 || r.Field<byte>("PatchTypeID") == 3 || r.Field<byte>("PatchTypeID") == 4)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "6")    //----For 'FOGRA Gray ΔH Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 52 || r.Field<byte>("PatchTypeID") == 53 || r.Field<byte>("PatchTypeID") == 54 || r.Field<byte>("PatchTypeID") == 55 || r.Field<byte>("PatchTypeID") == 56 || r.Field<byte>("PatchTypeID") == 57)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "7")    //----For 'CMYKRGB ΔH Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 1 || r.Field<byte>("PatchTypeID") == 2 || r.Field<byte>("PatchTypeID") == 3 || r.Field<byte>("PatchTypeID") == 4 || r.Field<byte>("PatchTypeID") == 5 || r.Field<byte>("PatchTypeID") == 6 || r.Field<byte>("PatchTypeID") == 7)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "8" || MetricSumID == "9" || MetricSumID == "36" || MetricSumID == "37")    //----For 'G7 Gray ΔCh Avg' or ----For 'G7 Gray ΔCh Max' or ----For 'wG7 Gray ΔCh Avg'  or ---_For 'wG7 Gray ΔCh Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 10 || r.Field<byte>("PatchTypeID") == 28 || r.Field<byte>("PatchTypeID") == 29 || r.Field<byte>("PatchTypeID") == 30 || r.Field<byte>("PatchTypeID") == 31 || r.Field<byte>("PatchTypeID") == 32 || r.Field<byte>("PatchTypeID") == 33 || r.Field<byte>("PatchTypeID") == 34 || r.Field<byte>("PatchTypeID") == 35 || r.Field<byte>("PatchTypeID") == 36 || r.Field<byte>("PatchTypeID") == 37 || r.Field<byte>("PatchTypeID") == 38 || r.Field<byte>("PatchTypeID") == 39 || r.Field<byte>("PatchTypeID") == 40 || r.Field<byte>("PatchTypeID") == 41 || r.Field<byte>("PatchTypeID") == 42 || r.Field<byte>("PatchTypeID") == 43 || r.Field<byte>("PatchTypeID") == 44 || r.Field<byte>("PatchTypeID") == 45 || r.Field<byte>("PatchTypeID") == 46 || r.Field<byte>("PatchTypeID") == 47 || r.Field<byte>("PatchTypeID") == 48 || r.Field<byte>("PatchTypeID") == 49 || r.Field<byte>("PatchTypeID") == 50)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "10" || MetricSumID == "11" || MetricSumID == "38" || MetricSumID == "39")   //----For 'G7 Black ΔL Avg' or ----For 'G7 Black ΔL Max' or ----For 'wG7 Black ΔL Avg' or ----For 'wG7 Black ΔL Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 4 || r.Field<byte>("PatchTypeID") == 15 || r.Field<byte>("PatchTypeID") == 25 || r.Field<byte>("PatchTypeID") == 26 || r.Field<byte>("PatchTypeID") == 27)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "12" || MetricSumID == "13" || MetricSumID == "40" || MetricSumID == "41")    //----For 'G7 CMY / Gray ΔL Avg' or ----For 'G7 CMY / Gray ΔL Max' or ----For 'wG7 CMY / Gray ΔL Avg' or ----For 'wG7 CMY / Gray ΔL Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 28 || r.Field<byte>("PatchTypeID") == 29 || r.Field<byte>("PatchTypeID") == 30 || r.Field<byte>("PatchTypeID") == 31 || r.Field<byte>("PatchTypeID") == 32 || r.Field<byte>("PatchTypeID") == 33 || r.Field<byte>("PatchTypeID") == 34 || r.Field<byte>("PatchTypeID") == 35 || r.Field<byte>("PatchTypeID") == 36 || r.Field<byte>("PatchTypeID") == 37 || r.Field<byte>("PatchTypeID") == 38 || r.Field<byte>("PatchTypeID") == 39 || r.Field<byte>("PatchTypeID") == 40 || r.Field<byte>("PatchTypeID") == 41 || r.Field<byte>("PatchTypeID") == 42 || r.Field<byte>("PatchTypeID") == 43 || r.Field<byte>("PatchTypeID") == 44 || r.Field<byte>("PatchTypeID") == 45 || r.Field<byte>("PatchTypeID") == 46 || r.Field<byte>("PatchTypeID") == 47 || r.Field<byte>("PatchTypeID") == 48 || r.Field<byte>("PatchTypeID") == 49 || r.Field<byte>("PatchTypeID") == 50)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "14")    //----For 'RGB ΔE Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 5 || r.Field<byte>("PatchTypeID") == 6 || r.Field<byte>("PatchTypeID") == 7)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "15" || MetricSumID == "18")    //----For 'ΔTVI25', 'ΔTVI25Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 16 || r.Field<byte>("PatchTypeID") == 19 || r.Field<byte>("PatchTypeID") == 22 || r.Field<byte>("PatchTypeID") == 25)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "16" || MetricSumID == "19")    //----For 'ΔTVI50', 'ΔTVI50Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 17 || r.Field<byte>("PatchTypeID") == 20 || r.Field<byte>("PatchTypeID") == 23 || r.Field<byte>("PatchTypeID") == 26)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "17" || MetricSumID == "20")    //----For 'ΔTVI75', 'ΔTVI75Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 18 || r.Field<byte>("PatchTypeID") == 21 || r.Field<byte>("PatchTypeID") == 24 || r.Field<byte>("PatchTypeID") == 27)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "21" || MetricSumID == "22" || MetricSumID == "23")    //----For 'Paper ΔL', 'Paper Δa', 'Paper Δb'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 8)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "24")    //----For 'CMY100 ΔE Max'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "25")    //----For 'Japan Gray ΔH Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 61 || r.Field<byte>("PatchTypeID") == 62 || r.Field<byte>("PatchTypeID") == 63 || r.Field<byte>("PatchTypeID") == 64 || r.Field<byte>("PatchTypeID") == 65)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "26")    //----For 'G7 Gray ΔH Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 8 || r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 28 || r.Field<byte>("PatchTypeID") == 29 || r.Field<byte>("PatchTypeID") == 30 || r.Field<byte>("PatchTypeID") == 31 || r.Field<byte>("PatchTypeID") == 32 || r.Field<byte>("PatchTypeID") == 33 || r.Field<byte>("PatchTypeID") == 34 || r.Field<byte>("PatchTypeID") == 35 || r.Field<byte>("PatchTypeID") == 36 || r.Field<byte>("PatchTypeID") == 37 || r.Field<byte>("PatchTypeID") == 38 || r.Field<byte>("PatchTypeID") == 39 || r.Field<byte>("PatchTypeID") == 40 || r.Field<byte>("PatchTypeID") == 41 || r.Field<byte>("PatchTypeID") == 42 || r.Field<byte>("PatchTypeID") == 43 || r.Field<byte>("PatchTypeID") == 44 || r.Field<byte>("PatchTypeID") == 45 || r.Field<byte>("PatchTypeID") == 46 || r.Field<byte>("PatchTypeID") == 47 || r.Field<byte>("PatchTypeID") == 48 || r.Field<byte>("PatchTypeID") == 49 || r.Field<byte>("PatchTypeID") == 50)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "27")    //----For 'ΔE Max No Paper'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") != 8)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "28" || MetricSumID == "32")    //----For 'ΔTVI20', 'ΔTVI20Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 113 || r.Field<byte>("PatchTypeID") == 124 || r.Field<byte>("PatchTypeID") == 135 || r.Field<byte>("PatchTypeID") == 146)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "29" || MetricSumID == "33")    //----For 'ΔTVI40', 'ΔTVI40Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 116 || r.Field<byte>("PatchTypeID") == 127 || r.Field<byte>("PatchTypeID") == 138 || r.Field<byte>("PatchTypeID") == 149)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "30" || MetricSumID == "34")    //----For 'ΔTVI60', 'ΔTVI60Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 118 || r.Field<byte>("PatchTypeID") == 129 || r.Field<byte>("PatchTypeID") == 140 || r.Field<byte>("PatchTypeID") == 151)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "31" || MetricSumID == "35")    //----For 'ΔTVI80', 'ΔTVI80Spread'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 121 || r.Field<byte>("PatchTypeID") == 132 || r.Field<byte>("PatchTypeID") == 143 || r.Field<byte>("PatchTypeID") == 154)
                        .CopyToDataTable();
            }
            else if (MetricSumID == "48")    //----For 'Fogra Gray ΔCh Avg'
            {
                DT = dtPatchDetails.AsEnumerable()
                        .Where(r => r.Field<byte>("PatchTypeID") == 9 || r.Field<byte>("PatchTypeID") == 52 || r.Field<byte>("PatchTypeID") == 53 || r.Field<byte>("PatchTypeID") == 54 || r.Field<byte>("PatchTypeID") == 55 || r.Field<byte>("PatchTypeID") == 56 || r.Field<byte>("PatchTypeID") == 57 || r.Field<byte>("PatchTypeID") == 58 || r.Field<byte>("PatchTypeID") == 59 || r.Field<byte>("PatchTypeID") == 60)
                        .CopyToDataTable();
            }
            else
            {
                DT = dtPatchDetails;
            }

            return DT;
        }

        #endregion

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
        //public bool IsVerification
        //{
        //    get
        //    {
        //        return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    }
        //    set
        //    {

        //    }
        //}
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

        #region IViewReportCMYKDensity Members
        public DataTable DSDensity_C { get; set; }
        public DataTable DSDensity_M { get; set; }
        public DataTable DSDensity_Y { get; set; }
        public DataTable DSDensity_K { get; set; }
        #endregion

        #region IViewExport Members
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public short CurveTypeId { get; set; }
        public FujifilmUSA.Sync.Business.Messages.CurveDataResponse CurveData { get; set; }
        #endregion
    }
}

