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
    public class ReportController : BaseController, IViewReportSingleMeasurement
    {
        #region Variable Declarations
        private PresenterReportSingleMeasurement _presenterReportSingleMeasurement;
        public decimal DtELimit = 0;
        private const string _InvalidMeasurementId = "Error_Invalid_MeasurementId";
        private const string _InvalidIsAverageOfMeasurements = "Error_Invalid_IsAverageOfMeasurements";
        private const string _InvalidIsVerificationProject = "Error_Invalid_IsVerificationProject";
        private const string _InvalidIsSync1DTypeProject = "Error_Invalid_IsSync1DTypeProject";
        private const string _InvalidDeltaEValue = "Error_Invalid_DeltaEValue";
        private const string _InvalidValidationType = "Error_Invalid_ValidationType";
        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            _presenterReportSingleMeasurement = new PresenterReportSingleMeasurement(this);
            base.Initialize(controllerContext);
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

        [HttpPost]
        //public IHttpActionResult ExportPrepressSingleMeasurementReportTest(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject, bool? IsSync1DTypeProject, string fromDeltaE, int validationType, ChartContent chart)
        //public IHttpActionResult ExportPrepressSingleMeasurementReportTest(ChartContent chart, int? MeasurementId, bool? IsAverageOfMeasurements, string IsVerificationProject, string IsSync1DTypeProject, string fromDeltaE, int validationType)
        public IHttpActionResult ExportPrepressSingleMeasurementReportTest(ChartContent chart)
        {
            IHttpActionResult actionResult = null;
            string validationMessage = "";
            //ChartContent chart = new ChartContent();
            BaseResponse<string> httpResponse = new BaseResponse<string>();
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
                        errprResponse.Error.Message = validationMessage;
                        errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
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



                        _presenterReportSingleMeasurement.GetReportDataOnMeasurement();

                        ReportPDF objReportPDF = new ReportPDF();
                        objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                        if (!Directory.Exists(objReportPDF.PDFPath))
                            Directory.CreateDirectory(objReportPDF.PDFPath);

                        objReportPDF.chartContent = chart;
                        objReportPDF.PDF_NAME = chart.CurrentUserId.ToString() + "_ReportSingleMeasurement.PDF";
                        objReportPDF.From_ΔE = From_ΔE;
                        objReportPDF.IsValidation = IsVerification;
                        objReportPDF.DLPName = chart.ReportDLPName;
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

                        //objReportPDF.ReportName = "Single Measurement Report";
                        //objReportPDF.DLPName = "DLP_NAME";
                        objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");
                        objReportPDF.DS_Common = DSReport;
                        objReportPDF.GenerateClientReportPDF();
                        string ReportPath = "UploadFiles/" + objReportPDF.PDF_NAME;
                        httpResponse.status = ResponseStatus.Success;
                        httpResponse.Data = ReportPath;
                        actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                    }
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

        //service to bind the summary delta E values for report
        [HttpGet]
        public IHttpActionResult GenerateSummaryDeltaCharts(int? MeasurementId, bool? IsAverageOfMeasurements, bool? IsVerificationProject,
                                                            bool? IsSync1DTypeProject, string fromDeltaE, int validationType)
        {
            //int? MeasurementId = 15081;
            //bool? IsAverageOfMeasurements = true;   
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
                    errprResponse.Error.Message = validationMessage;
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
                    if (DSReport != null)
                        if (DSReport.Tables[2] != null)
                        {
                            //foreach (DataColumn dc in DSReport.Tables[2].Columns)
                            //{
                            //    listStatesNames.Add(dc.ColumnName);
                            //}

                            for (int index = 0; index < DSReport.Tables[2].Columns.Count - 1; index++)
                            {
                                listStatesNames.Add(DSReport.Tables[2].Columns[index].ColumnName);
                            }


                            if (DSReport.Tables[2].Rows.Count > 0)

                                foreach (string columnName in listStatesNames)
                                {
                                    listStatesValues.Add(DataReader.ReadDecimal(DSReport.Tables[2].Rows[0], columnName));
                                }
                        }


                    //listDeltaSummary.Add(new Dictionary<string, decimal>();
                    //listDeltaSummary =  new Dictionary<string, decimal>
                    //            {
                    //              {"Avg_ΔE", 10},
                    //              {"Max_ΔE", 20},
                    //              {"PriΔE", 20},
                    //              {"PriΔH", 20},
                    //              {"GrayΔE", 20},
                    //              {"NinetyFiveΔE", 20}
                    //            };


                    httpResponse.Data = new
                    {
                        DTDeltaSummaryLables = listStatesNames,
                        DTDeltaSummaryValues = listStatesValues
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
    }
}