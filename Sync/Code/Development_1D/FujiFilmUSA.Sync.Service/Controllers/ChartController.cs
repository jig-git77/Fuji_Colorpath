using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.GlobalResources;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujiFilmUSA.Sync.Service.Models;
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
    public class ChartController : BaseController, IViewAnalyze, IViewReportCMYKDensity
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
            _presenterReportCMYKDensity = new PresenterReportCMYKDensity(this);
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
                    errprResponse.Error.Message = validationMessage;
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
                errprResponse.Error.Message = ex.Message;
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
                    errprResponse.Error.Message = validationMessage;
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
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }


        [HttpPost]
        public IHttpActionResult ExportChartReport(ChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<string> httpResponse = new BaseResponse<string>();
            try
            {
                if (chart == null)
                {
                    validationMessage = "Invalid Charts, Please try again.";

                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {

                    ReportPDF objReportPDF = new ReportPDF();
                    objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                    if (!Directory.Exists(objReportPDF.PDFPath))
                        Directory.CreateDirectory(objReportPDF.PDFPath);

                    objReportPDF.chartContent = chart;
                    objReportPDF.PDF_NAME = chart.CurrentUserId.ToString() + "_ReportSingleMeasurement.PDF";
                    objReportPDF.ReportName = "Single Measurement Report";
                    objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");

                    //objReportPDF.GenerateClientReportPDF();

                    string ReportPath = "UploadFiles/" + objReportPDF.PDF_NAME;
                    httpResponse.status = ResponseStatus.Success;
                    httpResponse.Data = ReportPath;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }
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
                //count Avg, Max and Min ΔE
                decimal minDtE = int.MaxValue;
                decimal maxDtE = int.MinValue;

                if (dtPatchDetails.Rows.Count > 0)
                {
                    //get the measurement id 
                    measurementID = Convert.ToInt32(dtPatchDetails.Rows[0]["MeasurementID"]);

                    Dictionary<string, object> childRow;
                    foreach (DataRow row in dtPatchDetails.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in dtPatchDetails.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }

                        decimal DtE = 0;
                        if (measurementID > 0)
                            DtE = string.IsNullOrEmpty(Convert.ToString(row.Field<decimal?>("ΔE_M_T"))) ? 0 : row.Field<decimal>("ΔE_M_T");//for run-1,2,.....
                        else
                            DtE = row.Field<decimal?>("ΔE") == null ? 0 : row.Field<decimal>("ΔE");//only for run-0

                        totalDeltaE += DtE;
                        minDtE = Math.Min(minDtE, DtE);
                        maxDtE = Math.Max(maxDtE, DtE);

                        spotAnalyzeDetails.patchDetails.Add(childRow);
                    }
                    avgDeltaE = totalDeltaE / dtPatchDetails.Rows.Count;
                }


                spotAnalyzeDetails.minDeltaE = minDtE.ToString();
                spotAnalyzeDetails.maxDeltaE = maxDtE.ToString();
                spotAnalyzeDetails.avgDeltaE = String.Format("{0:0.###}", avgDeltaE);
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
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        //public IHttpActionResult GenerateInkPaperChart(int? MeasurementId, string fromDeltaE, bool? IsVerificationProject, bool? IsSync1DTypeProject, bool? IsAverageOfMeasurements)
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
                    errprResponse.Error.Message = validationMessage;
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
                    var str = JsonConvert.SerializeObject(DTDeltaEPrimaryPatches);
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
                errprResponse.Error.Message = ex.Message;
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
                    errprResponse.Error.Message = validationMessage;
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
                        //GrayFSummaryList = GrayFSummary,
                        BlackLChartList = BlackLChart,
                        //BlackLSummaryList = BlackLSummary,
                        GrayLChartList = GrayLChart,
                        //GrayLSummaryList = GrayLSummary,
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
                errprResponse.Error.Message = ex.Message;
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
                    errprResponse.Error.Message = validationMessage;
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
                errprResponse.Error.Message = ex.Message;
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
                    errprResponse.Error.Message = validationMessage;
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
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }


        [HttpGet]
        public IHttpActionResult GetMeasurementPatchDetails(int? MeasurementId, bool? IsVerificationProject, bool? IsSync1DTypeProject)
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
                    errprResponse.Error.Message = validationMessage;
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
                    if (dtPatchDetails.Rows.Count > 0)
                    {
                        Dictionary<string, object> childRow;
                        foreach (DataRow row in dtPatchDetails.Rows)
                        {
                            childRow = new Dictionary<string, object>();
                            foreach (DataColumn col in dtPatchDetails.Columns)
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


                //Now we are using DataTable as DataSource                    
                //SpotAnalyze spotAnalyzeDetails = new SpotAnalyze();
                //decimal totalDeltaE = 0;
                //decimal avgDeltaE = 0;
                //int measurementID = 0;
                ////count Avg, Max and Min ΔE
                //decimal minDtE = int.MaxValue;
                //decimal maxDtE = int.MinValue;

                //if (dtPatchDetails.Rows.Count > 0)
                //{
                //    //get the measurement id 
                //    measurementID = Convert.ToInt32(dtPatchDetails.Rows[0]["MeasurementID"]);

                //    Dictionary<string, object> childRow;
                //    foreach (DataRow row in dtPatchDetails.Rows)
                //    {
                //        childRow = new Dictionary<string, object>();
                //        foreach (DataColumn col in dtPatchDetails.Columns)
                //        {
                //            childRow.Add(col.ColumnName, row[col]);
                //        }

                //        decimal DtE = 0;
                //        if (measurementID > 0)
                //            DtE = string.IsNullOrEmpty(Convert.ToString(row.Field<decimal?>("ΔE_M_T"))) ? 0 : row.Field<decimal>("ΔE_M_T");//for run-1,2,.....
                //        else
                //            DtE = row.Field<decimal?>("ΔE") == null ? 0 : row.Field<decimal>("ΔE");//only for run-0

                //        totalDeltaE += DtE;
                //        minDtE = Math.Min(minDtE, DtE);
                //        maxDtE = Math.Max(maxDtE, DtE);

                //        spotAnalyzeDetails.patchDetails.Add(childRow);
                //    }
                //    avgDeltaE = totalDeltaE / dtPatchDetails.Rows.Count;
                //}


                //spotAnalyzeDetails.minDeltaE = minDtE.ToString();
                //spotAnalyzeDetails.maxDeltaE = maxDtE.ToString();
                //spotAnalyzeDetails.avgDeltaE = String.Format("{0:0.###}", avgDeltaE);
                //GenerateResources();
                //var objGroup = new
                //{
                //    spotAnalyzeDetails = spotAnalyzeDetails,
                //    GlobalResources = _globalResourceValues
                //};
                //httpResponse.Data = objGroup;
                //actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
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
        public IHttpActionResult GetCGATSPatchReport(int? MeasurementId, bool? IsVerificationProject, bool? IsSync1DTypeProject, string metricSumID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            string validationMessage = "";
            string ReportFileName = "";
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
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {

                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    _presenterAnalyze.GetPatchDetailsofMeasurement();
                    //Now we are using DataTable as DataSource                    
                    dtFilteredPatchDetails = FilteredPatchDetailsTable(metricSumID, dtPatchDetails);

                    #region Generate CGATS .txt file
                    System.IO.StreamWriter sw;
                    ReportFileName = "/UploadFiles/" + MeasurementId.ToString() + "_MeasurementFile.txt";
                    string ReportFilePath = HostingEnvironment.MapPath("~/UploadFiles/") + MeasurementId.ToString() + "_MeasurementFile.txt";
                    sw = System.IO.File.CreateText(ReportFilePath);
                    sw.WriteLine(@"ORIGINATOR	""FujiFilm Colorpath Sync""");
                    sw.WriteLine(@"MEASUREMENT_SOURCE	""WhiteBase=Absolute  Filter=No""");
                    sw.WriteLine(@"ILLUMINATION_NAME	""D50""");
                    sw.WriteLine(@"OBSERVER_ANGLE	""2""");
                    sw.WriteLine(@"KEYWORD	""SampleID""");
                    sw.WriteLine(@"KEYWORD	""SAMPLE_NAME""");

                    if (IsSync1DType)
                    {
                        sw.WriteLine("NUMBER_OF_FIELDS" + "\t" + "55"); //-----Only for Sync1D projects, add Spectral data into CGATES file
                    }
                    else
                    {
                        sw.WriteLine("NUMBER_OF_FIELDS" + "\t" + "19");
                    }
                    sw.WriteLine("BEGIN_DATA_FORMAT");

                    if (IsSync1DType)
                    {
                        sw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tDE\tDE94\tDE2000\tLAB_L_TARGET\tLAB_A_TARGET\tLAB_B_TARGET\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS\tnm380\tnm390\tnm400\tnm410\tnm420\tnm430\tnm440\tnm450\tnm460\tnm470\tnm480\tnm490\tnm500\tnm510\tnm520\tnm530\tnm540\tnm550\tnm560\tnm570\tnm580\tnm590\tnm600\tnm610\tnm620\tnm630\tnm640\tnm650\tnm660\tnm670\tnm680\tnm690\tnm700\tnm710\tnm720\tnm730");
                    }
                    else
                    {
                        sw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tDE\tDE94\tDE2000\tLAB_L_TARGET\tLAB_A_TARGET\tLAB_B_TARGET\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS");
                    }
                    sw.WriteLine("END_DATA_FORMAT");
                    sw.WriteLine("NUMBER_OF_SETS" + "\t" + dtFilteredPatchDetails.Rows.Count.ToString());
                    sw.WriteLine("BEGIN_DATA");

                    for (int i = 0; i < dtFilteredPatchDetails.Rows.Count; i++)
                    {
                        if (IsSync1DType)
                        {
                            sw.WriteLine(dtFilteredPatchDetails.Rows[i]["SampleID"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_K"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE94"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE2000"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["TargetL"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targeta"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targetb"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_V"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm380"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm390"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm400"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm410"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm420"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm430"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm440"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm450"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm460"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm470"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm480"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm490"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm500"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm510"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm520"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm530"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm540"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm550"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm560"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm570"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm580"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm590"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm600"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm610"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm620"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm630"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm640"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm650"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm660"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm670"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm680"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm690"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm700"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm710"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm720"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm730"].ToString());
                        }
                        else
                        {
                            sw.WriteLine(dtFilteredPatchDetails.Rows[i]["SampleID"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_K"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE94"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE2000"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["TargetL"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targeta"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targetb"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_V"].ToString());
                        }
                    }
                    sw.WriteLine("END_DATA");
                    sw.Close();
                    #endregion

                    //var dataBytes = File.ReadAllBytes(ReportFilePath);
                    //var dataStream = new MemoryStream(dataBytes);
                    httpResponse.Data = ReportFileName;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }
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
        public HttpResponseMessage GetCGATSPatchReport2(int? MeasurementId, bool? IsVerificationProject, bool? IsSync1DTypeProject, string metricSumID)
        {
            HttpResponseMessage httpResponseMessage = null;
            string validationMessage = "";
            string ReportFileName = "";
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
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    httpResponseMessage = Request.CreateResponse<BaseErrorResponse>(HttpStatusCode.PreconditionFailed, errprResponse);
                    //httpResponseMessage.Content = new HttpContent()
                    //httpResponseMessage = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, ));//returns 412 status code
                }
                else
                {

                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    _presenterAnalyze.GetPatchDetailsofMeasurement();
                    //Now we are using DataTable as DataSource                    
                    dtFilteredPatchDetails = FilteredPatchDetailsTable(metricSumID, dtPatchDetails);

                    #region Generate CGATS .txt file
                    System.IO.StreamWriter sw;
                    //ReportFileName = "/UploadFiles/" + MeasurementId.ToString() + "_MeasurementFile.txt";
                    string ReportFilePath = HostingEnvironment.MapPath("~/UploadFiles/") + MeasurementId.ToString() + "_MeasurementFile.txt";
                    ReportFileName = MeasurementId.ToString() + "_MeasurementFile.txt";
                    sw = System.IO.File.CreateText(ReportFilePath);
                    sw.WriteLine(@"ORIGINATOR	""FujiFilm Colorpath Sync""");
                    sw.WriteLine(@"MEASUREMENT_SOURCE	""WhiteBase=Absolute  Filter=No""");
                    sw.WriteLine(@"ILLUMINATION_NAME	""D50""");
                    sw.WriteLine(@"OBSERVER_ANGLE	""2""");
                    sw.WriteLine(@"KEYWORD	""SampleID""");
                    sw.WriteLine(@"KEYWORD	""SAMPLE_NAME""");

                    if (IsSync1DType)
                    {
                        sw.WriteLine("NUMBER_OF_FIELDS" + "\t" + "55"); //-----Only for Sync1D projects, add Spectral data into CGATES file
                    }
                    else
                    {
                        sw.WriteLine("NUMBER_OF_FIELDS" + "\t" + "19");
                    }
                    sw.WriteLine("BEGIN_DATA_FORMAT");

                    if (IsSync1DType)
                    {
                        sw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tDE\tDE94\tDE2000\tLAB_L_TARGET\tLAB_A_TARGET\tLAB_B_TARGET\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS\tnm380\tnm390\tnm400\tnm410\tnm420\tnm430\tnm440\tnm450\tnm460\tnm470\tnm480\tnm490\tnm500\tnm510\tnm520\tnm530\tnm540\tnm550\tnm560\tnm570\tnm580\tnm590\tnm600\tnm610\tnm620\tnm630\tnm640\tnm650\tnm660\tnm670\tnm680\tnm690\tnm700\tnm710\tnm720\tnm730");
                    }
                    else
                    {
                        sw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tDE\tDE94\tDE2000\tLAB_L_TARGET\tLAB_A_TARGET\tLAB_B_TARGET\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS");
                    }
                    sw.WriteLine("END_DATA_FORMAT");
                    sw.WriteLine("NUMBER_OF_SETS" + "\t" + dtFilteredPatchDetails.Rows.Count.ToString());
                    sw.WriteLine("BEGIN_DATA");

                    for (int i = 0; i < dtFilteredPatchDetails.Rows.Count; i++)
                    {
                        if (IsSync1DType)
                        {
                            sw.WriteLine(dtFilteredPatchDetails.Rows[i]["SampleID"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_K"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE94"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE2000"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["TargetL"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targeta"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targetb"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_V"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm380"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm390"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm400"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm410"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm420"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm430"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm440"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm450"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm460"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm470"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm480"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm490"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm500"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm510"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm520"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm530"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm540"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm550"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm560"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm570"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm580"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm590"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm600"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm610"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm620"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm630"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm640"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm650"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm660"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm670"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm680"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm690"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm700"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm710"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm720"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["nm730"].ToString());
                        }
                        else
                        {
                            sw.WriteLine(dtFilteredPatchDetails.Rows[i]["SampleID"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CMYK_K"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE94"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["ΔE2000"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["TargetL"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targeta"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Targetb"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_C"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_M"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_Y"].ToString() + "\t" + dtFilteredPatchDetails.Rows[i]["Density_V"].ToString());
                        }
                    }
                    sw.WriteLine("END_DATA");
                    sw.Close();
                    #endregion

                    var dataBytes = File.ReadAllBytes(ReportFilePath);
                    var dataStream = new MemoryStream(dataBytes);
                    //httpResponse.Data = ReportFileName;
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StreamContent(dataStream);
                    httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = ReportFileName;
                    httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    //httpResponseMessage = Request.CreateResponse<BaseErrorResponse>(HttpStatusCode.InternalServerError, errprResponse);
                    //actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                //actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
                httpResponseMessage = Request.CreateResponse<BaseErrorResponse>(HttpStatusCode.InternalServerError, errprResponse);
            }
            return httpResponseMessage;
        }


        [HttpGet]
        public IHttpActionResult GetCFXPatchReport(int? MeasurementId, bool? IsVerificationProject, bool? IsSync1DTypeProject, string metricSumID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            string validationMessage = "";
            string ReportFileName = "";
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
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {

                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    _presenterAnalyze.GetPatchDetailsofMeasurement();
                    //Now we are using DataTable as DataSource                    
                    dtFilteredPatchDetails = FilteredPatchDetailsTable(metricSumID, dtPatchDetails);
                    char qt = '\u0022'; //---Pass " as a char into string

                    ReportFileName = "/UploadFiles/" + MeasurementId.ToString() + "_MeasurementFile.cxf";
                    string ReportFilePath = HostingEnvironment.MapPath("~/UploadFiles/") + MeasurementId.ToString() + "_MeasurementFile.cxf";

                    #region Generate .Cxf file
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(ReportFilePath);
                    sw.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>");
                    sw.WriteLine(@"<cc:CxF xmlns:xsi=""http:///www.w3.org/2001/XMLSchema-instance"" xmlns:cc=""http://colorexchangeformat.com/CxF3-core"">");
                    sw.WriteLine(@"    <cc:FileInformation>");
                    sw.WriteLine(@"        <cc:Creator>Fujifilm</cc:Creator>");
                    sw.WriteLine(@"        <cc:CreationDate>2016-12-19T16:31:27</cc:CreationDate>");
                    sw.WriteLine(@"        <cc:Description>Unknown</cc:Description>");
                    sw.WriteLine(@"    </cc:FileInformation>");
                    sw.WriteLine(@"    <cc:Resources>");
                    sw.WriteLine(@"        <cc:ObjectCollection>");


                    for (int i = 0; i < dtFilteredPatchDetails.Rows.Count; i++)
                    {
                        sw.WriteLine(@"            <cc:Object ObjectType=""Standard"" Name=" + qt + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + qt + " Id=" + qt + dtFilteredPatchDetails.Rows[i]["SampleName"].ToString() + qt + ">");
                        sw.WriteLine(@"                <cc:CreationDate>" + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss") + "</cc:CreationDate>");
                        sw.WriteLine(@"                <cc:ColorValues>");
                        sw.WriteLine(@"                    <cc:ColorCIELab ColorSpecification=""M0"">");
                        sw.WriteLine(@"                        <cc:L>" + dtFilteredPatchDetails.Rows[i]["CIELAB_L"].ToString() + "</cc:L>");
                        sw.WriteLine(@"                        <cc:a>" + dtFilteredPatchDetails.Rows[i]["CIELAB_A"].ToString() + "</cc:a>");
                        sw.WriteLine(@"                        <cc:b>" + dtFilteredPatchDetails.Rows[i]["CIELAB_B"].ToString() + "</cc:b>");
                        sw.WriteLine(@"                    </cc:ColorCIELab>");
                        sw.WriteLine(@"                </cc:ColorValues>");
                        sw.WriteLine(@"            </cc:Object>");
                    }

                    sw.WriteLine(@"        </cc:ObjectCollection>");
                    sw.WriteLine(@"        <cc:ColorSpecificationCollection>");
                    sw.WriteLine(@"            <cc:ColorSpecification Id=""M0"">");
                    sw.WriteLine(@"                <cc:MeasurementSpec>");
                    sw.WriteLine(@"                    <cc:MeasurementType>Colorimetric Reflectance</cc:MeasurementType>");
                    sw.WriteLine(@"                    <cc:GeometryChoice>");
                    sw.WriteLine(@"                        <cc:SingleAngle>");
                    sw.WriteLine(@"                            <cc:SingleAngleConfiguration>Uniplanar</cc:SingleAngleConfiguration>");
                    sw.WriteLine(@"                            <cc:IlluminationAngle>45</cc:IlluminationAngle>");
                    sw.WriteLine(@"                            <cc:MeasurementAngle>0</cc:MeasurementAngle>");
                    sw.WriteLine(@"                        </cc:SingleAngle>");
                    sw.WriteLine(@"                    </cc:GeometryChoice>");
                    sw.WriteLine(@"                    <cc:Device>");
                    sw.WriteLine(@"                        <cc:DeviceIllumination>M0 Incandescent</cc:DeviceIllumination>");
                    sw.WriteLine(@"                    </cc:Device>");
                    sw.WriteLine(@"                </cc:MeasurementSpec>");
                    sw.WriteLine(@"            </cc:ColorSpecification>");
                    sw.WriteLine(@"        </cc:ColorSpecificationCollection>");
                    sw.WriteLine(@"    </cc:Resources>");
                    sw.WriteLine(@"</cc:CxF>");
                    sw.Close();
                    #endregion

                    httpResponse.Data = ReportFileName;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }
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
                errprResponse.Error.Message = ex.Message;
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
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.Message = validationMessage;
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
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        [HttpGet]
        public HttpResponseMessage DownloadFile(string filePath)
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(filePath);
            var fileName = Path.GetFileName(fullPath);
            var stream = new FileStream(fullPath, FileMode.Open);
            //  byte[] dataBytes = new System.Net.WebClient().DownloadData(filePath);
            //var dataBytes = File.ReadAllBytes(System.Web.Hosting.HostingEnvironment.MapPath(filePath));
            //  var dataStream = new MemoryStream(dataBytes);

            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            //result.Content.Headers.ContentDisposition.FileName = "test.txt";

            return result;
        }

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

        #region Methods for Patch Details tab
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
    }
}

