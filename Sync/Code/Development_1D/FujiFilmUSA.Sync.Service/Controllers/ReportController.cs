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
                        objReportPDF.ReportName = "Single Measurement Report";
                        objReportPDF.DLPName = "DLP_NAME";
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