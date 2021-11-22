using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSync1D;
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
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class ReportController : BaseController, IViewReportSingleMeasurement, IViewReportGrading, IViewExport
    {
        #region Variable Declarations
        private PresenterReportSingleMeasurement _presenterReportSingleMeasurement;
        private PresenterExport _presenterExport;
        private PresenterReportGrading _presenterReportGrading;
        public decimal DtELimit = 0;
        private const string _InvalidMeasurementId = "Error_Invalid_MeasurementId";
        private const string _InvalidIsAverageOfMeasurements = "Error_Invalid_IsAverageOfMeasurements";
        private const string _InvalidIsVerificationProject = "Error_Invalid_IsVerificationProject";
        private const string _InvalidIsSync1DTypeProject = "Error_Invalid_IsSync1DTypeProject";
        private const string _InvalidProjectId = "Error_Invalid_ProjectId";
        private const string _InvalidDeltaEValue = "Error_Invalid_DeltaEValue";
        private const string _InvalidGradingDetails = "Error_Invalid_GradingDetails";
        private const string _InvalidValidationType = "Error_Invalid_ValidationType";
        private const string _InvalidOptimizationRunID = "Error_Invalid_OptimizationRunID";
        private const string _InvalidCurveType = "Error_Invalid_CurveType";
        private const string _InvalidLibraryType = "Error_Invalid_SpotExportLibraryType";

        #endregion

        #region Controller events
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterReportSingleMeasurement = new PresenterReportSingleMeasurement(this);
            _presenterReportGrading = new PresenterReportGrading(this);
            _presenterExport = new PresenterExport(this);
            base.Initialize(controllerContext);
        }
        #endregion

        #region RESTfull Services
        [HttpPost]
        [CustomAuthentication]
        public IHttpActionResult ExportAnalyzeChartReport(ChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                if (chart == null)
                {
                    validationMessage = "Invalid Charts, Please try again.";

                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    if (chart.MeasurementId == null)
                        validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                    if (chart.IsAverageOfMeasurements == null)
                        validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsAverageOfMeasurements);

                    if (chart.IsVerificationProject == null)
                        validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsVerificationProject);

                    if (chart.IsSync1DTypeProject == null)
                        validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsSync1DTypeProject);

                    if (string.IsNullOrEmpty(Convert.ToString(chart.fromDeltaE)))
                        validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidDeltaEValue);

                    if (string.IsNullOrEmpty(Convert.ToString(chart.validationType)))
                        validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidValidationType);

                    if (!string.IsNullOrEmpty(validationMessage))
                    {
                        BaseErrorResponse errprResponse = new BaseErrorResponse();
                        errprResponse.Error.UserMessage = validationMessage;
                        errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                    }
                    else
                    {
                        MeasurementId = Convert.ToInt32(chart.MeasurementId);
                        IsVerification = chart.IsVerificationProject == null ? false : Convert.ToBoolean(chart.IsVerificationProject);
                        IsSync1DType = chart.IsSync1DTypeProject == null ? false : Convert.ToBoolean(chart.IsSync1DTypeProject);
                        IsAverageOfMeasurements = chart.IsAverageOfMeasurements == null ? false : Convert.ToBoolean(chart.IsAverageOfMeasurements);
                        From_ΔE = Convert.ToString(chart.fromDeltaE);
                        ValidationType = Convert.ToInt32(chart.validationType);


                        ReportPDF objReportPDF = new ReportPDF();
                        objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                        if (!Directory.Exists(objReportPDF.PDFPath))
                            Directory.CreateDirectory(objReportPDF.PDFPath);

                        objReportPDF.chartContent = chart;
                        objReportPDF.PDF_NAME = chart.CurrentUserId.ToString() + "_ReportSingleMeasurement.PDF";
                        objReportPDF.From_ΔE = From_ΔE;
                        objReportPDF.IsValidation = IsVerification;
                        objReportPDF.DLPName = chart.ReportDLPName;
                        //objReportPDF.ReportName = "Single Measurement Report";
                        //objReportPDF.DLPName = "DLP_NAME";
                        objReportPDF.BaseServiceURL = Utilities.ReadConfigValue("BaseServiceURL");

                        if (chart.validationType == 1)
                        {
                            //code to set the report title
                            objReportPDF.ReportName = "Validation Report";
                            if (IsSync1DType)
                            {
                                objReportPDF.ReportName = "Press Alignment Report";
                            }


                            DSReport_G7_Vali = new DataSet();
                            _presenterReportSingleMeasurement.GetReportDataOnMeasurement_G7_Vali();
                            objReportPDF.DS_Common = DSReport_G7_Vali;
                            objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement_G7;
                        }
                        else
                        {
                            objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement_ISO;
                            if (ValidationType == 0) //For Default mode in Sync1D ISO, G7 or 4D validation project as choice of MTP
                            {
                                objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement_Default;
                            }
                            //code to set the report title
                            if (IsVerification)
                            {
                                objReportPDF.ReportName = "Validation Report";
                            }
                            else if (IsSync1DType)
                            {
                                objReportPDF.ReportName = "Alignment Curves Report";
                            }
                            else
                            {
                                objReportPDF.ReportName = "DLP Optimization Report";
                                if (chart.ProjectLicenseType == ELicenseType.InkOptimization.ToString())
                                {
                                    objReportPDF.ReportName = "Ink Optimization Report";
                                }
                                else if (chart.ProjectLicenseType == ELicenseType.Sync4DCertification.ToString())
                                {
                                    objReportPDF.ReportName = "Color Certification Report";
                                }
                                objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement; //Only in 4D Optimization part
                            }

                            DSReport = new DataSet();
                            _presenterReportSingleMeasurement.GetReportDataOnMeasurement();
                            objReportPDF.DS_Common = DSReport;
                            if (objReportPDF.DS_Common.Tables.Count > 0)
                            {
                                if (objReportPDF.DS_Common.Tables[0].Rows.Count > 0)
                                {
                                    string pdfName = string.Empty;
                                    string flName = string.Empty;
                                    flName = !IsAverageOfMeasurements ? objReportPDF.DS_Common.Tables[0].Rows[0]["MeasurementName"].ToString() : "Avg";
                                    pdfName = objReportPDF.DS_Common.Tables[0].Rows[0]["ProjectName"].ToString() + "-" + flName + ".PDF";
                                    objReportPDF.PDF_NAME = string.Empty;
                                    objReportPDF.PDF_NAME = pdfName;
                                }
                            }                            
                        }
                        objReportPDF.GenerateClientReportPDF();
                        string ReportPath = "/UploadFiles/" + objReportPDF.PDF_NAME;
                        ReportPath = HttpUtility.UrlEncode(ReportPath);
                        httpResponse.status = ResponseStatus.Success;
                        httpResponse.Data = ReportPath;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                    }
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

        [HttpPost]
        [CustomAuthentication]
        public IHttpActionResult ExportSpotAnalyzeChartReport(SpotChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                if (chart == null)
                {
                    validationMessage = "Invalid Charts, Please try again.";

                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    if (chart.OptimizeRunId == null)
                        validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                    if (!string.IsNullOrEmpty(validationMessage))
                    {
                        BaseErrorResponse errprResponse = new BaseErrorResponse();
                        errprResponse.Error.UserMessage = validationMessage;
                        errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                    }
                    else
                    {
                        OptimizeRunId = Convert.ToInt32(chart.OptimizeRunId);
                        ReportPDF objReportPDF = new ReportPDF();
                        objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                        if (!Directory.Exists(objReportPDF.PDFPath))
                            Directory.CreateDirectory(objReportPDF.PDFPath);

                        objReportPDF.spotChartContent = chart;
                        objReportPDF.PDF_NAME = chart.SpotProjectName + "-" + DateTime.Now.ToString("MMddyyHHmm") + ".PDF"; //chart.CurrentUserId.ToString() + "_ReportSpotProject.PDF";
                        objReportPDF.ReportName = "Brand Color Optimization";
                        objReportPDF.ReportType = (int)enumReportType.ReportSpotColorProject; //Only in spot color Optimization part
                        objReportPDF.BaseServiceURL = Utilities.ReadConfigValue("BaseServiceURL");
                        DSReport = new DataSet();
                        _presenterReportSingleMeasurement.GetReportDataOnSpotProject();
                        objReportPDF.DS_Common = DSReport;
                        objReportPDF.GenerateClientReportPDF();
                        string ReportPath = "/UploadFiles/" + objReportPDF.PDF_NAME;

                        httpResponse.status = ResponseStatus.Success;
                        httpResponse.Data = ReportPath;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                    }
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
        public HttpResponseMessage DownloadReport(string guid)
        {
            HttpResponseMessage httpResponseMessage = null;
            string validationMessage = "";
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            try
            {
                if (guid == null)
                {
                    validationMessage = "Invalid Charts, Please try again.";

                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse);//returns 412 status code
                }
                else
                {
                    var dataBytes = (byte[])HttpContext.Current.Session[guid];
                    string fileName = Convert.ToString(HttpContext.Current.Session[guid + "AttachementFileName"]);
                    //adding bytes to memory stream   
                    var dataStream = new MemoryStream(dataBytes);
                    httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StreamContent(dataStream);
                    httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Accept-Ranges, Content-Encoding, Content-Length, Content-Range");
                    httpResponseMessage.Content.Headers.ContentRange = new ContentRangeHeaderValue(dataStream.Length);
                    httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = fileName;
                    httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                }
            }
            catch (Exception ex)
            {

                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse);//returns the 500 status code

            }
            return httpResponseMessage;
        }

        //service to bind the summary delta E values for report
        [HttpGet]
        [CustomAuthentication]
        public IHttpActionResult GenerateSummaryDeltaCharts(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject,
                                                            bool? IsSync1DTypeProject, string fromDeltaE, int validationType)
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

                if (string.IsNullOrEmpty(Convert.ToString(validationType)))
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidValidationType);

                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    this.MeasurementId = Convert.ToInt32(MeasurementId);
                    this.IsVerification = IsVerificationProject == null ? false : Convert.ToBoolean(IsVerificationProject);
                    this.IsSync1DType = IsSync1DTypeProject == null ? false : Convert.ToBoolean(IsSync1DTypeProject);
                    this.IsAverageOfMeasurements = IsAverageOfMeasurements == null ? false : Convert.ToBoolean(IsAverageOfMeasurements);
                    this.From_ΔE = Convert.ToString(fromDeltaE);
                    this.ValidationType = Convert.ToInt32(validationType);

                    _presenterReportSingleMeasurement.GetReportDataOnMeasurement();
                    //List<Dictionary<string, decimal>> listDeltaSummary = new List<Dictionary<string, decimal>>();
                    List<string> listStatesNames = new List<string>();
                    List<decimal> listStatesValues = new List<decimal>();
                    List<decimal> listStatesLimits = new List<decimal>();
                    if (DSReport != null)
                        if (DSReport.Tables[2] != null)
                        {
                            //foreach (DataColumn dc in DSReport.Tables[2].Columns)
                            //{
                            //    listStatesNames.Add(dc.ColumnName);
                            //}

                            if (!IsVerification && !IsSync1DType)
                            {
                                for (int index = 0; index < DSReport.Tables[2].Columns.Count - 1; index++)
                                    listStatesNames.Add(DSReport.Tables[2].Columns[index].ColumnName);
                                if (DSReport.Tables[2].Rows.Count > 0)
                                    foreach (string columnName in listStatesNames)
                                        listStatesValues.Add(DataReader.ReadDecimal(DSReport.Tables[2].Rows[0], columnName));
                            }
                            else
                            {
                                for (int index = 0; index < DSReport.Tables[2].Rows.Count; index++)
                                {
                                    listStatesNames.Add(Convert.ToString(DSReport.Tables[2].Rows[index]["MetricSumName"]));
                                    listStatesValues.Add(Convert.ToDecimal(DSReport.Tables[2].Rows[index]["MeasuredValue"]));
                                    listStatesLimits.Add(Convert.ToDecimal(DSReport.Tables[2].Rows[index]["TargetValue"]));
                                }
                            }

                        }
                    httpResponse.Data = new
                    {
                        DTDeltaSummaryLables = listStatesNames,
                        DTDeltaSummaryValues = listStatesValues,
                        DTDeltaSummaryLimits = listStatesLimits
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


        [HttpPost]
        [CustomAuthentication]
        public IHttpActionResult ExportReportGrading(ChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            try
            {


                if (chart.MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (chart.IsAverageOfMeasurements == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsAverageOfMeasurements);


                if (chart.ProjectId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidProjectId);

                if (string.IsNullOrEmpty(Convert.ToString(chart.fromDeltaE)))
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidDeltaEValue);

                if (chart.grading == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidGradingDetails);


                if (!string.IsNullOrEmpty(validationMessage))
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.UserMessage = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else
                {
                    MeasurementId = Convert.ToInt32(chart.MeasurementId);
                    From_ΔE = Convert.ToString(chart.fromDeltaE);
                    IsAverageOfMeasurements = chart.grading.IsAverageOfMeasurements;
                    ProjectId = Convert.ToInt32(chart.ProjectId);
                    DSReport = new DataSet();
                    _presenterReportGrading.GetMeasurementDataForScoring();

                    if (DSReport != null && DSReport.Tables.Count > 0)
                    {
                        RevisedDataSet(DSReport, chart.grading); //-----Call the function to get two separate DataSets for Avg Scoring and Individual Scoring Measurements list

                        ReportPDF objReportPDF = new ReportPDF();
                        objReportPDF.ReportType = (int)enumReportType.ReportScoringMeasurement_Default;
                        objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                        if (!Directory.Exists(objReportPDF.PDFPath))
                            Directory.CreateDirectory(objReportPDF.PDFPath);

                        objReportPDF.chartContent = chart;
                        objReportPDF.PDF_NAME = chart.CurrentUserId.ToString() + "_ReportSingleMeasurement.PDF";
                        objReportPDF.DS_Common = DSReport;
                        objReportPDF.DS_MetricAvgScoring = DSReportTotalAvg;
                        objReportPDF.DS_ScoringMeasurementList = DSReportRevised;
                        objReportPDF.From_ΔE = From_ΔE;
                        objReportPDF.IsValidation = IsVerification;
                        objReportPDF.DLPName = chart.ReportDLPName;
                        objReportPDF.ReportName = "Scoring Report";
                        objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");
                        objReportPDF.BaseServiceURL = Utilities.ReadConfigValue("BaseServiceURL");
                        objReportPDF.GenerateClientReportPDF();
                        string ReportPath = "/UploadFiles/" + objReportPDF.PDF_NAME;
                        httpResponse.status = ResponseStatus.Success;
                        httpResponse.Data = ReportPath;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                    }
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
        [CustomAuthentication]
        public IHttpActionResult ExportGenerateCurveChart(int? OptimizationRunId, int? CurveType, string ExportFileName)
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
                    if (Convert.ToInt16(CurveType) == 100 || Convert.ToInt16(CurveType) == 101)
                        this.CurveTypeId = 5;
                    if (Convert.ToInt16(CurveType) == 102 || Convert.ToInt16(CurveType) == 104)
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
                        CurveFileName = ExportFileName + "_CurvesFile.txt";
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
                        CurveFileName = ExportFileName;
                        string zipCurveFileName = CurveFileName + "_CMYKFile.zip";
                        ReportFileName = "/UploadFiles/CurvesFile/" + zipCurveFileName;

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
                        CurveFileName = ExportFileName + "_CurvesFile.csv";
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

                        CurveFileName = ExportFileName;
                        string zipCurveFileName = CurveFileName + "_Harmony.zip";
                        ReportFileName = "/UploadFiles/CurvesFile/" + zipCurveFileName;


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

                        CurveFileName = ExportFileName;
                        string zipCurveFileName = CurveFileName + "_Nexus.zip";
                        ReportFileName = "/UploadFiles/CurvesFile/" + zipCurveFileName;


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
                        CurveFileName = ExportFileName + "_CurvesFile.ppc";

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
                        CurveFileName = ExportFileName + "_CurveDelta.ctvx";
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

                        CurveFileName = ExportFileName + "_CurveDelta.cgcx";
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
                        CurveFileName = ExportFileName + "_CurvesFile.txt";
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
                    else if (CurveType == 103)  // Hybrid
                    {
                        #region Create Zip file and download file for Hybrid

                        CurveFileName = ExportFileName;
                        string zipCurveFileName = CurveFileName + "_Hybrid.zip";
                        ReportFileName = "/UploadFiles/CurvesFile/" + zipCurveFileName;


                        //check if file exists and delete it
                        if (System.IO.File.Exists(CurveFilePath + zipCurveFileName))
                            System.IO.File.Delete(CurveFilePath + zipCurveFileName);

                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;
                        swC = System.IO.File.CreateText(CurveFilePath + "Cyan.csv");
                        swM = System.IO.File.CreateText(CurveFilePath + "Magenta.csv");
                        swY = System.IO.File.CreateText(CurveFilePath + "Yellow.csv");
                        swK = System.IO.File.CreateText(CurveFilePath + "Black.csv");


                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0"));
                            swC.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_C).ToString("0.0"));
                            swM.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0"));
                            swM.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_M).ToString("0.0"));
                            swY.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0"));
                            swY.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_Y).ToString("0.0"));
                            swK.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0"));
                            swK.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_K).ToString("0.0"));
                        }

                        swC.Close();
                        swM.Close();
                        swY.Close();
                        swK.Close();

                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + "Cyan.csv");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + "Magenta.csv");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + "Yellow.csv");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + "Black.csv");

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_Nexus.zip", CurveFileName + "_Nexus.zip");
                        #endregion
                    }
                    else if (CurveType == 104)  // Esko
                    {
                        #region Create Zip file and download file for Esko

                        CurveFileName = ExportFileName;
                        string zipCurveFileName = CurveFileName + "_Esko.zip";
                        ReportFileName = "/UploadFiles/CurvesFile/" + zipCurveFileName;


                        //check if file exists and delete it
                        if (System.IO.File.Exists(CurveFilePath + zipCurveFileName))
                            System.IO.File.Delete(CurveFilePath + zipCurveFileName);

                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;
                        swC = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Esko_Cya.txt");
                        swM = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Esko_Mag.txt");
                        swY = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Esko_Yel.txt");
                        swK = System.IO.File.CreateText(CurveFilePath + CurveFileName + "_Esko_Bla.txt");


                        for (int i = 0; i < CurveData.CurveExportView.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_C).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000"));
                            swM.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_M).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000"));
                            swY.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_Y).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000"));
                            swK.WriteLine(Convert.ToDecimal(CurveData.CurveExportView[i].CMYK_K).ToString("0.0000") + "    " + Convert.ToDecimal(CurveData.CurveExportView[i].ReferenceNo).ToString("0.0000"));
                        }

                        swC.Close();
                        swM.Close();
                        swY.Close();
                        swK.Close();

                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Esko_Cya.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Esko_Mag.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Esko_Yel.txt");
                        ZipManager.AddFileToZip(CurveFilePath + zipCurveFileName, CurveFilePath + CurveFileName + "_Esko_Bla.txt");

                        //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_Esko.zip", CurveFileName + "_Esko.zip");
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
        [CustomAuthentication]
        public IHttpActionResult ExportDeltaCurveChart(int? OptimizationRunId, int? CurveType, string ExportFileName, bool isCurrentRun)
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
                    if (Convert.ToInt16(CurveType) == 100 || Convert.ToInt16(CurveType) == 101)
                        this.CurveTypeId = 5;
                    if (Convert.ToInt16(CurveType) == 102 || Convert.ToInt16(CurveType) == 104)
                        this.CurveTypeId = 45;

                    _presenterExport.GetCurveData();

                    List<OptimizeRunCurve> DT_CurveDelta = new List<OptimizeRunCurve>();
                    if (isCurrentRun)
                        DT_CurveDelta = CurveData.CurveDeltaRefView;
                    else
                        DT_CurveDelta = CurveData.CurveDeltaPrevRunView;

                    string CurveFileName = ExportFileName + "_CurveDelta.txt";
                    string CurveFilePath = HostingEnvironment.MapPath("~/UploadFiles/CurvesFile/");
                    string ReportFileName = "/UploadFiles/CurvesFile/" + CurveFileName; ;
                    if (!File.Exists(CurveFilePath))
                        Directory.CreateDirectory(CurveFilePath);

                    #region Create file and download txt file for all curve types
                    //---------------For all Curve Types -------------------//
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(CurveFilePath + CurveFileName);
                    sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                    sw.WriteLine("BEGIN_DATA_FORMAT");
                    sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                    sw.WriteLine("END_DATA_FORMAT");
                    sw.WriteLine("NUMBER_OF_SETS\t" + DT_CurveDelta.Count);
                    sw.WriteLine("BEGIN_DATA");
                    for (int i = 0; i < DT_CurveDelta.Count; i++)
                    {
                        sw.WriteLine(DT_CurveDelta[i].ReferenceNo.ToString() + "\t" + DT_CurveDelta[i].CMYK_C.ToString() + "\t" + DT_CurveDelta[i].CMYK_M.ToString() + "\t" + DT_CurveDelta[i].CMYK_Y.ToString() + "\t" + DT_CurveDelta[i].CMYK_K.ToString());
                    }
                    sw.WriteLine("END_DATA");
                    sw.Close();
                    #endregion

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

        [HttpPost]
        [CustomAuthentication]
        public IHttpActionResult ExportSpotCMYKLibraray(SpotLibrayCMYKExport model)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                if (model == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidLibraryType);

                //if (LibraryType == null)
                //    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidLibraryType);

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

                    if (!string.IsNullOrEmpty(model.SpotProjectIDs))
                    {
                        SpotProjectIDs = model.SpotProjectIDs;
                        _presenterExport.GetSpotPatchDetailsForCMYKExportBySpotProject();
                    }
                    else
                    {
                        OptimizeRunId = model.OptimizationRunId;
                        _presenterExport.GetSpotPatchDetailsForCMYKExport();
                    }


                    List<Patch> listPatch = new List<Patch>();

                    if (dtPatchDetails != null && dtPatchDetails.Rows.Count > 0)
                    {
                        if (dtPatchDetails.Columns.Contains("DuplicatePatchError"))
                        {
                            if (Convert.ToInt32(dtPatchDetails.Rows[0]["DuplicatePatchError"]) == -1)
                            {
                                httpResponse.Data = "-1";
                                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                                return actionResult;
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in dtPatchDetails.Rows)
                            {
                                Patch patch = new Patch()
                                {
                                    PatchID = Convert.ToInt32(dr["SpotID"]),
                                    ColorName = Convert.ToString(dr["SpotName"]),
                                    Cmyk_C = Convert.ToDouble(dr["CMYK_C"]),
                                    Cmyk_M = Convert.ToDouble(dr["CMYK_M"]),
                                    Cmyk_Y = Convert.ToDouble(dr["CMYK_Y"]),
                                    Cmyk_K = Convert.ToDouble(dr["CMYK_K"]),
                                    Cie_L = Convert.ToDouble(dr["TargetL"]),
                                    Cie_a = Convert.ToDouble(dr["Targeta"]),
                                    Cie_b = Convert.ToDouble(dr["Targetb"]),
                                    Spot1 = 0
                                };
                                listPatch.Add(patch);
                            }
                        }
                    }

                    string fileName = "";
                    if (model.LibraryType == 1)
                        fileName = ExportColderaFile(listPatch, model);
                    else if (model.LibraryType == 2)
                        fileName = ExportColdera2File(listPatch, model);
                    else if (model.LibraryType == 3)
                        fileName = ExportColorgateFile(listPatch, model);
                    else
                        fileName = ExportCMYK_DBDFile(listPatch, model);


                    if (!string.IsNullOrEmpty(fileName))
                        fileName = "/UploadFiles/CMYKFile/" + fileName;
                    httpResponse.Data = fileName;
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
        #endregion

        #region Methods
        private void RevisedDataSet(DataSet DSReport, ReportGrading grading)
        {
            decimal CyanAvgScore = 0;
            decimal MagentaAvgScore = 0;
            decimal YellowAvgScore = 0;
            decimal BlackAvgScore = 0;
            decimal RedAvgScore = 0;
            decimal GreenAvgScore = 0;
            decimal BlueAvgScore = 0;
            decimal PaperAvgScore = 0;
            decimal CMY25AvgScore = 0;
            decimal CMY50AvgScore = 0;
            decimal CMY75AvgScore = 0;
            DSReportRevised = new DataSet();

            DataTable dt = new DataTable("MyTable");
            dt.Columns.Add(new DataColumn("MeasurementID", typeof(int)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Cyan", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Magenta", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Yellow", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Black", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CMYKPri", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Red", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Green", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Blue", typeof(decimal)));
            dt.Columns.Add(new DataColumn("RGBSec", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CMY25", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CMY50", typeof(decimal)));
            dt.Columns.Add(new DataColumn("CMY75", typeof(decimal)));
            dt.Columns.Add(new DataColumn("GrayBalance", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Paper", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Overall", typeof(decimal)));

            if (DSReport != null && DSReport.Tables.Count > 0)
            {
                for (int i = 0; i < DSReport.Tables[4].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["MeasurementID"] = DSReport.Tables[4].Rows[i]["MeasurementID"].ToString();
                    dr["Name"] = DSReport.Tables[4].Rows[i]["MeasurementName"].ToString();
                    dr["Date"] = DSReport.Tables[4].Rows[i]["MeasurementDate"].ToString();
                    dr["Cyan"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Cyan"].ToString(), "Cyan", grading);
                    dr["Magenta"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Magenta"].ToString(), "Magenta", grading);
                    dr["Yellow"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Yellow"].ToString(), "Yellow", grading);
                    dr["Black"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Black"].ToString(), "Black", grading);
                    dr["CMYKPri"] = (CalculateGrading(DSReport.Tables[4].Rows[i]["Cyan"].ToString(), "Cyan", grading) + CalculateGrading(DSReport.Tables[4].Rows[i]["Magenta"].ToString(), "Magenta", grading) + CalculateGrading(DSReport.Tables[4].Rows[i]["Yellow"].ToString(), "Yellow", grading) + CalculateGrading(DSReport.Tables[4].Rows[i]["Black"].ToString(), "Black", grading)) / 4;

                    dr["Red"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Red"].ToString(), "Red", grading);
                    dr["Green"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Green"].ToString(), "Green", grading);
                    dr["Blue"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Blue"].ToString(), "Blue", grading);
                    dr["RGBSec"] = (CalculateGrading(DSReport.Tables[4].Rows[i]["Red"].ToString(), "Red", grading) + CalculateGrading(DSReport.Tables[4].Rows[i]["Green"].ToString(), "Green", grading) + CalculateGrading(DSReport.Tables[4].Rows[i]["Blue"].ToString(), "Blue", grading)) / 3;

                    dr["CMY25"] = CalculateGrading(DSReport.Tables[4].Rows[i]["CMY25"].ToString(), "CMY25", grading);
                    dr["CMY50"] = CalculateGrading(DSReport.Tables[4].Rows[i]["CMY50"].ToString(), "CMY50", grading);
                    dr["CMY75"] = CalculateGrading(DSReport.Tables[4].Rows[i]["CMY75"].ToString(), "CMY75", grading);
                    dr["GrayBalance"] = (CalculateGrading(DSReport.Tables[4].Rows[i]["CMY25"].ToString(), "CMY25", grading) + CalculateGrading(DSReport.Tables[4].Rows[i]["CMY50"].ToString(), "CMY50", grading) + CalculateGrading(DSReport.Tables[4].Rows[i]["CMY75"].ToString(), "CMY75", grading)) / 3;

                    dr["Paper"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Paper"].ToString(), "Paper", grading);
                    dr["Overall"] = (Convert.ToDecimal(dr["CMYKPri"]) + Convert.ToDecimal(dr["RGBSec"]) + Convert.ToDecimal(dr["GrayBalance"]) + Convert.ToDecimal(dr["Paper"])) / 4;
                    dt.Rows.Add(dr);
                }
            }
            DSReportRevised.Tables.Add(dt);

            DSReportTotalAvg = new DataSet();
            if (DSReportRevised != null && DSReportRevised.Tables.Count > 0)
            {
                DataTable dtAvg = new DataTable("MyTableAvg");

                dtAvg.Columns.Add(new DataColumn("Color", typeof(string)));
                dtAvg.Columns.Add(new DataColumn("AvgScore", typeof(decimal)));

                for (int i = 0; i < DSReportRevised.Tables[0].Rows.Count; i++)
                {
                    CyanAvgScore = CyanAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Cyan"]);
                    MagentaAvgScore = MagentaAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Magenta"]);
                    YellowAvgScore = YellowAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Yellow"]);
                    BlackAvgScore = BlackAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Black"]);

                    RedAvgScore = RedAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Red"]);
                    GreenAvgScore = GreenAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Green"]);
                    BlueAvgScore = BlueAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Blue"]);

                    CMY25AvgScore = CMY25AvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["CMY25"]);
                    CMY50AvgScore = CMY50AvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["CMY50"]);
                    CMY75AvgScore = CMY75AvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["CMY75"]);

                    PaperAvgScore = PaperAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Paper"]);
                }

                DataRow dr1 = dtAvg.NewRow();
                dr1["Color"] = "Cyan";
                dr1["AvgScore"] = Math.Round((CyanAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr1);

                DataRow dr2 = dtAvg.NewRow();
                dr2["Color"] = "Magenta";
                dr2["AvgScore"] = Math.Round((MagentaAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr2);

                DataRow dr3 = dtAvg.NewRow();
                dr3["Color"] = "Yellow";
                dr3["AvgScore"] = Math.Round((YellowAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr3);

                DataRow dr4 = dtAvg.NewRow();
                dr4["Color"] = "Black";
                dr4["AvgScore"] = Math.Round((BlackAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr4);

                DataRow dr5 = dtAvg.NewRow();
                dr5["Color"] = "Red";
                dr5["AvgScore"] = Math.Round((RedAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr5);

                DataRow dr6 = dtAvg.NewRow();
                dr6["Color"] = "Green";
                dr6["AvgScore"] = Math.Round((GreenAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr6);

                DataRow dr7 = dtAvg.NewRow();
                dr7["Color"] = "Blue";
                dr7["AvgScore"] = Math.Round((BlueAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr7);

                DataRow dr8 = dtAvg.NewRow();
                dr8["Color"] = "CMY25";
                dr8["AvgScore"] = Math.Round((CMY25AvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr8);

                DataRow dr9 = dtAvg.NewRow();
                dr9["Color"] = "CMY50";
                dr9["AvgScore"] = Math.Round((CMY50AvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr9);

                DataRow dr10 = dtAvg.NewRow();
                dr10["Color"] = "CMY75";
                dr10["AvgScore"] = Math.Round((CMY75AvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr10);

                DataRow dr11 = dtAvg.NewRow();
                dr11["Color"] = "Paper";
                dr11["AvgScore"] = Math.Round((PaperAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
                dtAvg.Rows.Add(dr11);

                DSReportTotalAvg.Tables.Add(dtAvg);
            }
        }

        private decimal CalculateGrading(string Orig_ΔE, string OriginFrom, ReportGrading grading)
        {
            decimal ΔEGrade = 0;
            decimal OriginalValue = 0;
            OriginalValue = Convert.ToDecimal(Orig_ΔE);
            decimal CMYKSoc_C, CMYKMaxSoc_C, CMYKIndPer_C = 0;
            decimal CMYKSoc_M, CMYKMaxSoc_M, CMYKIndPer_M = 0;
            decimal CMYKSoc_Y, CMYKMaxSoc_Y, CMYKIndPer_Y = 0;
            decimal CMYKSoc_K, CMYKMaxSoc_K, CMYKIndPer_K = 0;

            decimal RGBSoc_R, RGBMaxSoc_R, RGBIndPer_R = 0;
            decimal RGBSoc_G, RGBMaxSoc_G, RGBIndPer_G = 0;
            decimal RGBSoc_B, RGBMaxSoc_B, RGBIndPer_B = 0;

            decimal CMYSoc_25, CMYMaxSoc_25, CMYIndPer_25 = 0;
            decimal CMYSoc_50, CMYMaxSoc_50, CMYIndPer_50 = 0;
            decimal CMYSoc_75, CMYMaxSoc_75, CMYIndPer_75 = 0;

            decimal PaperSoc, PaperMaxSoc, PaperIndPer = 0;

            //lblCMYKMaxSoc.Text = (txtCMYKSco1.Text == "") ? "0" : (Convert.ToDecimal(txtCMYKSco1.Text) * 4).ToString();
            //lblRGBMaxSoc.Text = (txtRGBSco1.Text == "") ? "0" : (Convert.ToDecimal(txtRGBSco1.Text) * 3).ToString();
            //lblCMYMaxSoc.Text = (txtCMYSco1.Text == "") ? "0" : (Convert.ToDecimal(txtCMYSco1.Text) * 3).ToString();
            //lblPaperMaxSoc.Text = (txtPaperSco1.Text == "") ? "0" : txtPaperSco1.Text;

            if (OriginFrom == "Cyan")
            {
                #region Cyan calculation
                //CMYKMaxSoc_C = (txtCMYKSco1.Text == "") ? "0" : txtCMYKSco1.Text;
                CMYKMaxSoc_C = grading.CmykScore1;
                if (OriginalValue <= grading.CmykTolerance1)
                {
                    CMYKSoc_C = grading.CmykScore1;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance2))
                {
                    CMYKSoc_C = grading.CmykScore2;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance3))
                {
                    CMYKSoc_C = grading.CmykScore3;
                }
                else
                {
                    CMYKSoc_C = 0;
                }

                if (CMYKMaxSoc_C != 0)
                {
                    CMYKIndPer_C = Math.Round(((CMYKSoc_C / Convert.ToDecimal(CMYKMaxSoc_C)) * 100), 2);
                }

                ΔEGrade = Convert.ToDecimal(CMYKIndPer_C);
                #endregion
            }
            else if (OriginFrom == "Magenta")
            {
                #region Magenta calculation
                CMYKMaxSoc_M = grading.CmykScore1;

                if (OriginalValue <= grading.CmykTolerance1)
                {
                    CMYKSoc_M = grading.CmykScore1;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance2))
                {
                    CMYKSoc_M = grading.CmykScore2;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance3))
                {
                    CMYKSoc_M = grading.CmykScore3;
                }
                else
                {
                    CMYKSoc_M = 0;
                }

                if (CMYKMaxSoc_M != 0)
                {
                    CMYKIndPer_M = Math.Round(((CMYKSoc_M / CMYKMaxSoc_M) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(CMYKIndPer_M);
                #endregion
            }
            else if (OriginFrom == "Yellow")
            {
                #region Yellow calculation
                CMYKMaxSoc_Y = grading.CmykScore1;

                if (OriginalValue <= grading.CmykTolerance1)
                {
                    CMYKSoc_Y = grading.CmykScore1;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance2))
                {
                    CMYKSoc_Y = grading.CmykScore2;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance3))
                {
                    CMYKSoc_Y = grading.CmykScore3;
                }
                else
                {
                    CMYKSoc_Y = 0;
                }

                if (CMYKMaxSoc_Y != 0)
                {
                    CMYKIndPer_Y = Math.Round(((CMYKSoc_Y / CMYKMaxSoc_Y) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(CMYKIndPer_Y);
                #endregion
            }
            else if (OriginFrom == "Black")
            {
                #region Black calculation
                CMYKMaxSoc_K = grading.CmykScore1;

                if (OriginalValue <= grading.CmykTolerance1)
                {
                    CMYKSoc_K = grading.CmykScore1;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance2))
                {
                    CMYKSoc_K = grading.CmykScore2;
                }
                else if (OriginalValue <= (grading.CmykTolerance1 + grading.CmykTolerance3))
                {
                    CMYKSoc_K = grading.CmykScore3;
                }
                else
                {
                    CMYKSoc_K = 0;
                }

                if (CMYKMaxSoc_K != 0)
                {
                    CMYKIndPer_K = Math.Round(((Convert.ToDecimal(CMYKSoc_K) / Convert.ToDecimal(CMYKMaxSoc_K)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(CMYKIndPer_K);
                #endregion
            }
            else if (OriginFrom == "Red")
            {
                #region Red calculation
                RGBMaxSoc_R = grading.RgbScore1;

                if (OriginalValue <= grading.RgbTolerance1)
                {
                    RGBSoc_R = grading.RgbScore1;
                }
                else if (OriginalValue <= (grading.RgbTolerance1 + grading.RgbTolerance2))
                {
                    RGBSoc_R = grading.RgbScore2;
                }
                else if (OriginalValue <= (grading.RgbTolerance1 + grading.RgbTolerance3))
                {
                    RGBSoc_R = grading.RgbScore3;
                }
                else
                {
                    RGBSoc_R = 0;
                }

                if (RGBMaxSoc_R != 0)
                {
                    RGBIndPer_R = Math.Round(((Convert.ToDecimal(RGBSoc_R) / Convert.ToDecimal(RGBMaxSoc_R)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(RGBIndPer_R);
                #endregion
            }
            else if (OriginFrom == "Green")
            {
                #region Green calculation
                RGBMaxSoc_G = grading.RgbScore1;

                if (OriginalValue <= grading.RgbTolerance1)
                {
                    RGBSoc_G = grading.RgbScore1;
                }
                else if (OriginalValue <= (grading.RgbTolerance1 + grading.RgbTolerance2))
                {
                    RGBSoc_G = grading.RgbScore2;
                }
                else if (OriginalValue <= (grading.RgbTolerance1 + grading.RgbTolerance3))
                {
                    RGBSoc_G = grading.RgbScore3;
                }
                else
                {
                    RGBSoc_G = 0;
                }

                if (RGBMaxSoc_G != 0)
                {
                    RGBIndPer_G = Math.Round(((Convert.ToDecimal(RGBSoc_G) / Convert.ToDecimal(RGBMaxSoc_G)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(RGBIndPer_G);
                #endregion
            }
            else if (OriginFrom == "Blue")
            {
                #region Blue calculation
                RGBMaxSoc_B = grading.RgbScore1;

                if (OriginalValue <= grading.RgbTolerance1)
                {
                    RGBSoc_B = grading.RgbScore1;
                }
                else if (OriginalValue <= (grading.RgbTolerance1 + grading.RgbTolerance2))
                {
                    RGBSoc_B = grading.RgbScore2;
                }
                else if (OriginalValue <= (grading.RgbTolerance1 + grading.RgbTolerance3))
                {
                    RGBSoc_B = grading.RgbScore3;
                }
                else
                {
                    RGBSoc_B = 0;
                }

                if (RGBMaxSoc_B != 0)
                {
                    RGBIndPer_B = Math.Round(((Convert.ToDecimal(RGBSoc_B) / Convert.ToDecimal(RGBMaxSoc_B)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(RGBIndPer_B);
                #endregion
            }
            else if (OriginFrom == "CMY25")
            {
                #region CMY25 calculation
                CMYMaxSoc_25 = grading.CmyScore1;

                if (OriginalValue <= grading.CmyTolerance1)
                {
                    CMYSoc_25 = grading.CmyScore1;
                }
                else if (OriginalValue <= (grading.CmyTolerance1 + grading.CmyTolerance2))
                {
                    CMYSoc_25 = grading.CmyScore2;
                }
                else if (OriginalValue <= (grading.CmyTolerance1 + grading.CmyTolerance3))
                {
                    CMYSoc_25 = grading.CmyScore3;
                }
                else
                {
                    CMYSoc_25 = 0;
                }

                if (CMYMaxSoc_25 != 0)
                {
                    CMYIndPer_25 = Math.Round(((Convert.ToDecimal(CMYSoc_25) / Convert.ToDecimal(CMYMaxSoc_25)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(CMYIndPer_25);
                #endregion
            }
            else if (OriginFrom == "CMY50")
            {
                #region CMY50 calculation
                CMYMaxSoc_50 = grading.CmyScore1;

                if (OriginalValue <= grading.CmyTolerance1)
                {
                    CMYSoc_50 = grading.CmyScore1;
                }
                else if (OriginalValue <= (grading.CmyTolerance1 + grading.CmyTolerance2))
                {
                    CMYSoc_50 = grading.CmyScore2;
                }
                else if (OriginalValue <= (grading.CmyTolerance1 + grading.CmyTolerance3))
                {
                    CMYSoc_50 = grading.CmyScore3;
                }
                else
                {
                    CMYSoc_50 = 0;
                }

                if (CMYMaxSoc_50 != 0)
                {
                    CMYIndPer_50 = Math.Round(((Convert.ToDecimal(CMYSoc_50) / Convert.ToDecimal(CMYMaxSoc_50)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(CMYIndPer_50);
                #endregion
            }
            else if (OriginFrom == "CMY75")
            {
                #region CMY75 calculation
                CMYMaxSoc_75 = grading.CmyScore1;

                if (OriginalValue <= grading.CmyTolerance1)
                {
                    CMYSoc_75 = grading.CmyScore1;
                }
                else if (OriginalValue <= (grading.CmyTolerance1 + grading.CmyTolerance2))
                {
                    CMYSoc_75 = grading.CmyScore2;
                }
                else if (OriginalValue <= (grading.CmyTolerance1 + grading.CmyTolerance3))
                {
                    CMYSoc_75 = grading.CmyScore3;
                }
                else
                {
                    CMYSoc_75 = 0;
                }

                if (CMYMaxSoc_75 != 0)
                {
                    CMYIndPer_75 = Math.Round(((Convert.ToDecimal(CMYSoc_75) / Convert.ToDecimal(CMYMaxSoc_75)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(CMYIndPer_75);
                #endregion
            }
            else if (OriginFrom == "Paper")
            {
                #region Paper calculation
                PaperMaxSoc = grading.PaperScore1;

                if (OriginalValue <= grading.PaperTolerance1)
                {
                    PaperSoc = grading.PaperScore1;
                }
                else if (OriginalValue <= (grading.PaperTolerance1 + grading.PaperTolerance2))
                {
                    PaperSoc = grading.PaperScore2;
                }
                else if (OriginalValue <= (grading.PaperTolerance1 + grading.PaperTolerance3))
                {
                    PaperSoc = grading.PaperScore3;
                }
                else
                {
                    PaperSoc = 0;
                }

                if (PaperMaxSoc != 0)
                {
                    PaperIndPer = Math.Round(((Convert.ToDecimal(PaperSoc) / Convert.ToDecimal(PaperMaxSoc)) * 100), 2);
                }
                ΔEGrade = Convert.ToDecimal(PaperIndPer);
                #endregion
            }
            return ΔEGrade;
        }

        #region Create file and download .xml file for Caldera CMYK type
        private string ExportColderaFile(List<Patch> listPatch, SpotLibrayCMYKExport detail)
        {

            string directoryFilePath = HostingEnvironment.MapPath("~/UploadFiles/CMYKFile/");
            string fileName = detail.FileName + ".xml";
            string SpotName_Parameter = "";
            if (!Directory.Exists(directoryFilePath))
                Directory.CreateDirectory(directoryFilePath);

            System.IO.StreamWriter sw;
            sw = System.IO.File.CreateText(HostingEnvironment.MapPath("~/UploadFiles/CMYKFile/") + fileName);

            //--------Start: Header------------//
            sw.WriteLine("<MediaDeviceColors>");
            sw.WriteLine("<ColorList server=" + "\"" + detail.Server + "\"" + " model=" + "\"" + detail.Model + "\"" + " media=" + "\"" + detail.Media + "\"" + " resolution=" + "\"" + detail.Resolution + "\"" + " mode=" + "\"" + "CMYK" + "\"" + ">");
            //--------End: Header--------------//

            for (int i = 0; i < listPatch.Count; i++)
            {
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");
                sw.WriteLine("<Color>" + SpotName_Parameter + " : " + "CMYK " + listPatch[i].Cmyk_C.ToString("N1") + " " + listPatch[i].Cmyk_M.ToString("N1") + " " + listPatch[i].Cmyk_Y.ToString("N1") + " " + listPatch[i].Cmyk_K.ToString("N1") + " </Color>");
            }
            //--------Start: Footer------------//
            sw.WriteLine("</ColorList>");
            sw.WriteLine("</MediaDeviceColors>");
            //--------End: Footer--------------//

            sw.Close();

            //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".xml", CMYKFileName + ".xml");
            return fileName;
        }
        #endregion

        #region Create file and download .cct file for Colorgate CMYK type
        private string ExportColorgateFile(List<Patch> listPatch, SpotLibrayCMYKExport detail)
        {
            string directoryFilePath = HostingEnvironment.MapPath("~/UploadFiles/CMYKFile/");
            string fileName = detail.FileName + ".cct";
            string SpotName_Parameter = "";
            if (!Directory.Exists(directoryFilePath))
                Directory.CreateDirectory(directoryFilePath);

            System.IO.StreamWriter sw;
            sw = System.IO.File.CreateText(HostingEnvironment.MapPath("~/UploadFiles/CMYKFile/") + fileName);

            //--------Start: Header------------//
            sw.Write("<?xml version=" + "\"1.0" + "\"" + " encoding=" + "\"UTF-8" + "\"?>");
            sw.Write("<ColorTable xmlns=" + "\"urn:schemas-colorgate-com:colortable" + "\"" + " Name=" + "\"" + detail.FileName + "\"" + " Version=" + "\"2" + "\">");
            //--------End: Header--------------//

            for (int i = 0; i < listPatch.Count; i++)
            {
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");
                sw.Write("<ColorEntry Name=" + "\"" + SpotName_Parameter + "\"" + " ReplacementEnabled=" + "\"true" + "\"" + " ReplacementType=" + "\"23" + "\">");
                sw.Write("<ColorIn ColorModel=" + "\"Spot" + "\"/>");
                sw.Write("<ColorOut ColorModel=" + "\"Custom" + "\"" + " CustomColorModelId=" + "\"1" + "\"" + " ColorAliasName=" + "\"" + "\">");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_C.ToString("N0") + "</ComponentValue>");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_M.ToString("N0") + "</ComponentValue>");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_Y.ToString("N0") + "</ComponentValue>");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_K.ToString("N0") + "</ComponentValue>");
                sw.Write("</ColorOut>");
                sw.Write("</ColorEntry>");
            }
            //--------Start: Footer------------//
            sw.Write("</ColorTable>");
            //--------End: Footer--------------//

            sw.Close();

            //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".cct", CMYKFileName + ".cct");
            return fileName;
        }


        #endregion

        #region Create file and download .xml file for Caldera2 CMYK type
        private string ExportColdera2File(List<Patch> listPatch, SpotLibrayCMYKExport detail)
        {
            string directoryFilePath = HostingEnvironment.MapPath("~/UploadFiles/CMYKFile/");
            string fileName = detail.FileName + ".xml";
            string SpotName_Parameter = "";
            if (!Directory.Exists(directoryFilePath))
                Directory.CreateDirectory(directoryFilePath);

            System.IO.StreamWriter sw;
            sw = System.IO.File.CreateText(HostingEnvironment.MapPath("~/UploadFiles/CMYKFile/") + fileName);

            //--------Start: Header------------//
            sw.WriteLine("<MediaDeviceColors>");
            sw.WriteLine("<ColorList server=" + "\"" + detail.Server + "\"" + " model=" + "\"" + detail.Model + "\"" + " media=" + "\"" + detail.Media + "\"" + " resolution=" + "\"" + detail.Resolution + "\"" + " quality=" + "\"" + detail.Quality + "\"" + " mode=" + "\"" + detail.Mode + "\"" + ">");
            //--------End: Header--------------//

            for (int i = 0; i < listPatch.Count; i++)
            {
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");
                sw.WriteLine("<Color>" + SpotName_Parameter + " : " + "CMYK " + listPatch[i].Cmyk_C.ToString("N1") + " " + listPatch[i].Cmyk_M.ToString("N1") + " " + listPatch[i].Cmyk_Y.ToString("N1") + " " + listPatch[i].Cmyk_K.ToString("N1") + " </Color>");
            }
            //--------Start: Footer------------//
            sw.WriteLine("</ColorList>");
            sw.WriteLine("</MediaDeviceColors>");
            //--------End: Footer--------------//

            sw.Close();

            //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".xml", CMYKFileName + ".xml");
            return fileName;
        }
        #endregion

        #region Create file and download .dbd file for XMF CMYK type
        private string ExportCMYK_DBDFile(List<Patch> listPatch, SpotLibrayCMYKExport detail)
        {
            string SpotName_Header, SpotName_Parameter;
            System.IO.StreamWriter sw = null;
            string directoryName = HostingEnvironment.MapPath("~/UploadFiles/CMYKFile/") + detail.FileName;
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            string fileName = "";
            for (int i = 0; i < listPatch.Count; i++)
            {
                fileName = directoryName + "/" + detail.FileName + i + ".dbi";
                sw = System.IO.File.CreateText(fileName);

                SpotName_Header = listPatch[i].ColorName.Replace("_", "#20");
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");

                sw.WriteLine("%%N:/Proforma/ColorPools/" + detail.InkPoolID + "/ColorPool/" + SpotName_Header);
                sw.WriteLine("Color_1:OBJECT Color");
                sw.WriteLine("{");
                sw.WriteLine("\tName:string = " + "\"" + SpotName_Parameter + "\"");
                sw.WriteLine("\tFUJI" + "\\" + "PrintMode:string = " + "\"LargeGumatMerge\";");
                sw.WriteLine("\tColorType:string = " + "\"Normal\";");
                sw.WriteLine("\tNeutralDensity:double = " + GetNdensity(listPatch[i].Cie_L).ToString("0.000000000000000e+0") + ";");
                sw.WriteLine("\tTempLab:double[3] = " + listPatch[i].Cie_L.ToString("0.000000000000000e+0") + "," + listPatch[i].Cie_a.ToString("0.000000000000000e+0") + "," + listPatch[i].Cie_b.ToString("0.000000000000000e+0") + ";");
                sw.WriteLine("\tCMYK:double[4] = " + String.Format("{0,12:e15}", (listPatch[i].Cmyk_C / 100)) + "," + (listPatch[i].Cmyk_M / 100).ToString("0.000000000000000e+0") + "," + (listPatch[i].Cmyk_Y / 100).ToString("0.000000000000000e+0") + "," + (listPatch[i].Cmyk_K / 100).ToString("0.000000000000000e+0") + ";");
                sw.WriteLine("\tHideCMYK:string = " + "\"ShowAll\";");
                sw.WriteLine("\tHideLab:string = " + "\"ShowAll\";");
                sw.WriteLine("\tRawName:string = " + "\"\";");
                sw.WriteLine("\tLibraryType:string = " + "\"ColorPath Optimized\";");
                sw.WriteLine("}");
                sw.WriteLine("");
                sw.WriteLine("%%E:");
                sw.Close();

                ZipManager.AddFileToZip(directoryName + ".zip", fileName);
            }

            ////-----Alias file-- START----------//
            fileName = directoryName + "/" + detail.FileName + "alias.dbi";
            sw = System.IO.File.CreateText(fileName);
            sw.WriteLine("%%N:/Proforma/ColorPools/" + detail.InkPoolID + "/ColorantAliasRules");
            sw.WriteLine("");
            sw.WriteLine("%%E:");
            sw.Close();
            ZipManager.AddFileToZip(directoryName + ".zip", fileName);
            ////-----Alias file-- END----------//

            ////-----Info file-- START----------//
            fileName = directoryName + "/" + detail.FileName + "info.dbi";
            sw = System.IO.File.CreateText(fileName);
            sw.WriteLine("%%N:/Proforma/ColorPools/" + detail.InkPoolID + "/Info");
            sw.WriteLine("FUJI" + "\\" + "ColorPoolInfo_1:OBJECT FUJI" + "\\" + "ColorPoolInfo");
            sw.WriteLine("{");
            sw.WriteLine("\tName:string = " + "\"" + detail.FileName + "\";");
            sw.WriteLine("\tComment:string = " + "\"" + detail.Comment + "\";");
            sw.WriteLine("}");
            sw.WriteLine("");
            sw.WriteLine("%%E:");
            sw.Close();
            ZipManager.AddFileToZip(directoryName + ".zip", fileName);
            ////-----Info file-- END----------//

            //Delete the directory
            if (Directory.Exists(directoryName))
                Directory.Delete(directoryName, true);

            return detail.FileName + ".zip";
            //Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".zip", CMYKFileName + ".zip");
        }
        #endregion


        //Get density of LabL to export CMYK values
        double GetNdensity(double Lab_L)
        {
            double var_Y;
            if ((Lab_L > 8))
            {
                var_Y = (100 * Math.Pow(((Lab_L + 16) / 116), 3));
            }
            else
            {
                var_Y = (100 * ((108 / 841) * (((Lab_L + 16) / 116) - (4 / 29))));
            }

            if ((var_Y > 0))
            {
                return (0.13353 + Math.Log10((100 / var_Y)));
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region IViewReportSingleMeasurement Members

        public int MeasurementId { get; set; }
        public DataSet DSReport { get; set; }
        public string From_ΔE { get; set; }
        public bool IsVerification { get; set; }
        public bool IsSync1DType { get; set; }
        public DataSet DSReport_G7_Vali { get; set; }
        public int ValidationType { get; set; }
        public bool IsAverageOfMeasurements { get; set; }
        #endregion

        #region IViewReportGrading Members
        public int ProjectId { get; set; }
        public DataSet DSReportRevised { get; set; }
        public DataSet DSReportTotalAvg { get; set; }
        #endregion

        #region IViewExport Members
        public int OptimizeRunId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public Project Project { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public short CurveTypeId { get; set; }
        public FujifilmUSA.Sync.Business.Messages.CurveDataResponse CurveData { get; set; }
        public int SpotProjectId { get; set; }
        public DataTable dtPatchDetails { get; set; }
        public SpotProjectChartSetting SpotProjectChartSetting { get; set; }
        public string SpotProjectIDs { get; set; }
        #endregion
    }
}