using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.GlobalResources;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujiFilmUSA.Sync.Service.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class ReportController : BaseController, IViewReportSingleMeasurement, IViewReportGrading
    {
        #region Variable Declarations
        private PresenterReportSingleMeasurement _presenterReportSingleMeasurement;
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
        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterReportSingleMeasurement = new PresenterReportSingleMeasurement(this);
            _presenterReportGrading = new PresenterReportGrading(this);
            base.Initialize(controllerContext);
        }

        [HttpPost]
        public IHttpActionResult ExportChartReport(ChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            Models.BaseResponse<string> httpResponse = new Models.BaseResponse<string>();
            try
            {
                if (chart == null)
                {
                    validationMessage = "Invalid Charts, Please try again.";

                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)Models.ResponseStatus.ValidationError;
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
                    httpResponse.status = Models.ResponseStatus.Success;
                    httpResponse.Data = ReportPath;
                    actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                }
            }
            catch (Exception ex)
            {

                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)Models.ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        [HttpPost]
        //public IHttpActionResult ExportPrepressSingleMeasurementReportTest(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject, bool? IsSync1DTypeProject, string fromDeltaE, int validationType, ChartContent chart)
        //public IHttpActionResult ExportPrepressSingleMeasurementReportTest(ChartContent chart, int? MeasurementId, bool? IsAverageOfMeasurements, string IsVerificationProject, string IsSync1DTypeProject, string fromDeltaE, int validationType)
        public IHttpActionResult ExportPrepressSingleMeasurementReportTest(ChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            //ChartContent chart = new ChartContent();
            Models.BaseResponse<string> httpResponse = new Models.BaseResponse<string>();
            //int? MeasurementId= 25204;
            //bool? IsAverageOfMeasurements= false;
            //string IsVerificationProject= "false", IsSync1DTypeProject= "true", fromDeltaE= "E2000";
            //int validationType=-1;
            try
            {
                if (chart == null)
                {
                    validationMessage = "Invalid Charts, Please try again.";

                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)Models.ResponseStatus.ValidationError;
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
                        errprResponse.Error.Message = validationMessage;
                        errprResponse.Error.Code = (int)Models.ResponseStatus.ValidationError;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                    }
                    else
                    {
                        MeasurementId = Convert.ToInt32(chart.MeasurementId);
                        IsVerification = chart.IsVerificationProject == null ? false : Convert.ToBoolean(chart.IsVerificationProject);
                        IsSync1DType = chart.IsSync1DTypeProject == null ? false : Convert.ToBoolean(chart.IsSync1DTypeProject);
                        IsAverageOfMeasurements = chart.IsAverageOfMeasurements == null ? false : Convert.ToBoolean(IsAverageOfMeasurements);
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
                        objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");

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
                                objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement; //Only in 4D Optimization part
                            }

                            DSReport = new DataSet();
                            _presenterReportSingleMeasurement.GetReportDataOnMeasurement();
                            objReportPDF.DS_Common = DSReport;
                        }

                        objReportPDF.GenerateClientReportPDF();
                        string ReportPath = "UploadFiles/" + objReportPDF.PDF_NAME;
                        httpResponse.status = Models.ResponseStatus.Success;
                        httpResponse.Data = ReportPath;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                    }
                }
            }
            catch (Exception ex)
            {

                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)Models.ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        //service to bind the summary delta E values for report
        [HttpGet]
        public IHttpActionResult GenerateSummaryDeltaCharts(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject,
                                                            bool? IsSync1DTypeProject, string fromDeltaE, int validationType)
        {
            //int? MeasurementId = 15081;
            //bool? IsAverageOfMeasurements = true;   
            IHttpActionResult actionResult = null;
            string validationMessage = "";

            Models.BaseResponse<object> httpResponse = new Models.BaseResponse<object>();
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
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)Models.ResponseStatus.ValidationError;
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
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)Models.ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }



        public IHttpActionResult ExportReportGrading(ChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            Models.BaseResponse<string> httpResponse = new Models.BaseResponse<string>();
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
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)Models.ResponseStatus.ValidationError;
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
                        RevisedDataSet(DSReport,chart.grading); //-----Call the function to get two separate DataSets for Avg Scoring and Individual Scoring Measurements list

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
                        objReportPDF.GenerateClientReportPDF();
                        string ReportPath = "UploadFiles/" + objReportPDF.PDF_NAME;
                        httpResponse.status = Models.ResponseStatus.Success;
                        httpResponse.Data = ReportPath;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                    }
                }
            }
            catch (Exception ex)
            {

                BaseErrorResponse errprResponse = new BaseErrorResponse();
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)Models.ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code

            }
            return actionResult;
        }

        private void RevisedDataSet(DataSet DSReport,ReportGrading grading)
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
    }
}