using ClosedXML.Excel;
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
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
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
        private const string _InvalidOptimizationRunID = "Error_Invalid_OptimizationRunID";

        //Label localization variables
        private const string _reclocLabelJob = "locLabelJob";
        private const string _reclocLabelDevice = "locLabelDevice";
        private const string _reclocLabelMetric = "locLabelMetric";
        private const string _reclocLabelStandard = "locLabelStandard";
        private const string _reclocLabelTimestamp = "locLabelTimestamp";
        private const string _reclocLabelCustomer = "locLabelCustomer";
        private const string _reclocLabelPageOrSurface = "locLabelPageOrSurface";
        private const string _reclocLabelJDFJobID = "locLabelJDFJobID";
        private const string _reclocLabeldEType = "locLabeldEType";
        private const string _reclocLabelTol = "locLabelTol";
        private const string _reclocLabelAct = "locLabelAct";
        private const string _reclocLabelSignature = "locLabelSignature";
        private const string _reclocLabelDate = "locLabelDate";
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
                errprResponse.Error.UserMessage = ex.Message;
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
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }


        [HttpGet]
        public IHttpActionResult GetAverageMeasurementCompatibility(int? OptimizationRunID)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<int> httpResponse = new BaseResponse<int>();
            string validationMessage = "";
            try
            {
                if (OptimizationRunID == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidOptimizationRunID);

                OptimizeRunId = Convert.ToInt32(OptimizationRunID);
                int CompatibilityNo = _presenterAnalyze.ChkAvgMeasurementCompatibility();
                httpResponse.Data = CompatibilityNo;
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

        //[HttpPost]
        //public HttpResponseMessage GetSpotPatchDetailsReport(ExportViewModel model)
        //{
        //    HttpResponseMessage httpResponseMessage = null;
        //    try
        //    {
        //        DetermineCulture();
        //        MeasurementId = 0;

        //        OptimizeRunId = Convert.ToInt32(model.OptimizationRunID);
        //        _presenterAnalyze.GetSpotPatchDetailsofMeasurement();

        //        DataTable dt = new DataTable();
        //        dt = GridFilters.GetFilteredResult(dtPatchDetails, model.sort, model.filter);
        //        //logic for filter

        //        ReportPDF objReportPDF = new ReportPDF();
        //        objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

        //        if (!Directory.Exists(objReportPDF.PDFPath))
        //            Directory.CreateDirectory(objReportPDF.PDFPath);

        //        objReportPDF.dtSpotPatchDetails = dt;
        //        objReportPDF.PDF_NAME = model.OptimizationRunID.ToString() + "_ReportSpotPatchDetails.PDF";
        //        objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");
        //        objReportPDF.GenerateSpotPatchDetailsReport();

        //        string filePath = objReportPDF.PDFPath + objReportPDF.PDF_NAME;

        //        var dataBytes = File.ReadAllBytes(filePath);
        //        //adding bytes to memory stream   
        //        var dataStream = new MemoryStream(dataBytes);
        //        httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);
        //        httpResponseMessage.Content = new StreamContent(dataStream);
        //        httpResponseMessage.Content.Headers.Add("Access-Control-Expose-Headers", "Accept-Ranges, Content-Encoding, Content-Length, Content-Range");
        //        httpResponseMessage.Content.Headers.ContentRange = new ContentRangeHeaderValue(dataStream.Length);
        //        httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
        //        httpResponseMessage.Content.Headers.ContentDisposition.FileName = objReportPDF.PDF_NAME;
        //        httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseErrorResponse errprResponse = new BaseErrorResponse();
        //        errprResponse.Error.UserMessage = ex.Message;
        //        errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
        //        httpResponseMessage = Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse);//returns the 500 status code
        //    }
        //    return httpResponseMessage;
        //}

        [HttpPost]
        public IHttpActionResult GetSpotPatchDetailsReport(ExportViewModel model)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                DetermineCulture();
                MeasurementId = 0;

                OptimizeRunId = Convert.ToInt32(model.OptimizationRunID);
                _presenterAnalyze.GetSpotPatchDetailsofMeasurement();

                DataTable dt = new DataTable();
                dt = GridFilters.GetFilteredResult(dtPatchDetails, model.sort, model.filter);
                //logic for filter

                ReportPDF objReportPDF = new ReportPDF();
                objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");

                if (!Directory.Exists(objReportPDF.PDFPath))
                    Directory.CreateDirectory(objReportPDF.PDFPath);

                objReportPDF.dtSpotPatchDetails = dt;
                objReportPDF.PDF_NAME = model.OptimizationRunID.ToString() + "_ReportSpotPatchDetails.PDF";
                objReportPDF.BaseWebURL = Utilities.ReadConfigValue("BaseWebURL");
                objReportPDF.GenerateSpotPatchDetailsReport();

                string ReportFileName = "/UploadFiles/" + objReportPDF.PDF_NAME;                                                
                httpResponse.Data = ReportFileName;
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

        [HttpPost]
        public IHttpActionResult GetSpotPatchDetailsExcelReport(ExportViewModel model)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {
                DetermineCulture();
                MeasurementId = 0;
                int measurementID = 0;
                OptimizeRunId = Convert.ToInt32(model.OptimizationRunID);
                _presenterAnalyze.GetSpotPatchDetailsofMeasurement();
                if (dtPatchDetails.Rows.Count > 0)
                    measurementID = Convert.ToInt32(dtPatchDetails.Rows[0]["MeasurementID"]);

                DataTable dt = new DataTable();
                dt = GridFilters.GetFilteredResult(dtPatchDetails, model.sort, model.filter);

                //select specific columns 
                DataView view = new DataView(dt);
                DataTable cTable = new DataTable();
                if (measurementID > 0)
                    cTable = view.ToTable(false, "SpotID", "HTMLColor", "SpotName", "ΔE_T_P", "ΔE_M_T", "ΔE_M_P");
                else
                    cTable = view.ToTable(false, "SpotID", "HTMLColor", "SpotName", "ΔL", "Δa", "Δb", "ΔE");


                ReportPDF objReportPDF = new ReportPDF();
                objReportPDF.PDFPath = HostingEnvironment.MapPath("~/UploadFiles/");
                objReportPDF.PDF_NAME = OptimizeRunId.ToString() + "_DataGridViewExport.xlsx";
                if (!Directory.Exists(objReportPDF.PDFPath))
                    Directory.CreateDirectory(objReportPDF.PDFPath);
                string filePath = objReportPDF.PDFPath + objReportPDF.PDF_NAME;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("worksheet");
                    if (cTable.Rows.Count > 0)
                    {
                        // Adding HeaderRow.
                        ws.Cell("A" + 1).Value = cTable.Columns[0].ColumnName;
                        ws.Cell("B" + 1).Value = cTable.Columns[1].ColumnName;
                        ws.Cell("C" + 1).Value = cTable.Columns[2].ColumnName;
                        ws.Cell("D" + 1).Value = cTable.Columns[3].ColumnName;
                        ws.Cell("E" + 1).Value = cTable.Columns[4].ColumnName;
                        ws.Cell("F" + 1).Value = cTable.Columns[5].ColumnName;

                        //for spot run 0
                        if (measurementID == 0)
                        {
                            ws.Cell("D" + 1).Value = "pΔL";
                            ws.Cell("E" + 1).Value = "pΔa";
                            ws.Cell("F" + 1).Value = "pΔb";
                            ws.Cell("G" + 1).Value = "pΔE";
                        }



                        // Adding DataRows.
                        for (int i = 0; i < cTable.Rows.Count; i++)
                        {

                            ws.Cell("A" + (i + 2)).Value = cTable.Rows[i][0];
                            ws.Cell("B" + (i + 2)).Value = cTable.Rows[i][1];
                            ws.Cell("C" + (i + 2)).Value = cTable.Rows[i][2];
                            ws.Cell("D" + (i + 2)).Value = cTable.Rows[i][3];
                            ws.Cell("E" + (i + 2)).Value = cTable.Rows[i][4];
                            ws.Cell("F" + (i + 2)).Value = cTable.Rows[i][5];

                            //for ΔE in run 0 with
                            if (measurementID == 0)
                                ws.Cell("G" + (i + 2)).Value = cTable.Rows[i][6]; ;

                            // Changing color to to html color
                            ws.Cells("B" + (i + 2)).Style.Fill.BackgroundColor = XLColor.FromHtml(cTable.Rows[i]["HTMLColor"].ToString());
                        }
                    }
                    wb.SaveAs(filePath);

                }


                var dataBytes = File.ReadAllBytes(filePath);
                //adding bytes to memory stream   
                
                string ReportFileName = "/UploadFiles/" + objReportPDF.PDF_NAME;
                httpResponse.Data = ReportFileName;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, httpResponse));
                //delete the excel file
                //File.Delete(filePath);
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
        public IHttpActionResult GetCGATSPatchReport(ExportPatchViewModel model)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            string validationMessage = "";
            string ReportFileName = "";
            try
            {
                if (model.MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (model.IsSync1DTypeProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsSync1DTypeProject);

                if (model.IsVerificationProject == null)
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

                    this.MeasurementId = Convert.ToInt32(model.MeasurementId);
                    this.IsVerification = model.IsVerificationProject == null ? false : Convert.ToBoolean(model.IsVerificationProject);
                    this.IsSync1DType = model.IsSync1DTypeProject == null ? false : Convert.ToBoolean(model.IsSync1DTypeProject);
                    _presenterAnalyze.GetPatchDetailsofMeasurement();
                    //Now we are using DataTable as DataSource                    
                    DataTable dt = FilteredPatchDetailsTable(model.metricSumID, dtPatchDetails);
                    
                    dtFilteredPatchDetails = GridFilters.GetFilteredResult(dt, model.sort, model.filter);

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
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        [HttpPost]
        public IHttpActionResult GetCFXPatchReport(ExportPatchViewModel model)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<string> httpResponse = new BaseResponse<string>();
            string validationMessage = "";
            string ReportFileName = "";
            try
            {
                if (model.MeasurementId == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidMeasurementId);

                if (model.IsSync1DTypeProject == null)
                    validationMessage = GlobalResourceManager.Instance.GetGlobalResourceString(_InvalidIsSync1DTypeProject);

                if (model.IsVerificationProject == null)
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

                    this.MeasurementId = Convert.ToInt32(model.MeasurementId);
                    this.IsVerification = model.IsVerificationProject == null ? false : Convert.ToBoolean(model.IsVerificationProject);
                    this.IsSync1DType = model.IsSync1DTypeProject == null ? false : Convert.ToBoolean(model.IsSync1DTypeProject);
                    _presenterAnalyze.GetPatchDetailsofMeasurement();
                    //Now we are using DataTable as DataSource                    
                    DataTable dt = FilteredPatchDetailsTable(model.metricSumID, dtPatchDetails);

                    dtFilteredPatchDetails = GridFilters.GetFilteredResult(dt, model.sort, model.filter);

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
                errprResponse.Error.UserMessage = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }
            return actionResult;
        }

        [HttpPost]
        public IHttpActionResult GetDymoPrintData(DymoMeasurement model)
        {
            IHttpActionResult actionResult = null;
            BaseResponse<object> httpResponse = new BaseResponse<object>();
            try
            {

                var measurementSummary = model.MeasurementSummary;

                //Open label template from XML(.label) file            
                StreamReader streamReader = new StreamReader(HostingEnvironment.MapPath("~/Templates/CertifiedLabel.label"));
                string labelXML = streamReader.ReadToEnd();
                streamReader.Close();
                //string labelXML = "XML";

                //Set the localization values for label caption to placeholder
                labelXML = labelXML.Replace("locJob", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelJob));
                labelXML = labelXML.Replace("locDevice", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelDevice));
                labelXML = labelXML.Replace("locMetric", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelMetric));
                labelXML = labelXML.Replace("locStandard", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelStandard));
                labelXML = labelXML.Replace("locTimestamp", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelTimestamp));
                labelXML = labelXML.Replace("locCustomer", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelCustomer));
                if (model.LicenseType == ELicenseType.Sync1D.ToString())
                {
                    labelXML = labelXML.Replace("locPageOrSurface", "");
                    labelXML = labelXML.Replace("locJDFJobID", "");
                    labelXML = labelXML.Replace("locJob", GlobalResourceManager.Instance.GetGlobalResourceString("locMeasurementNameResource1.Text"));
                }
                else
                {
                    labelXML = labelXML.Replace("locPageOrSurface", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelPageOrSurface));
                    labelXML = labelXML.Replace("locJDFJobID", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelJDFJobID));
                    labelXML = labelXML.Replace("locJob", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelJob));
                }
                labelXML = labelXML.Replace("locdEType", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabeldEType));
                labelXML = labelXML.Replace("locTol", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelTol));
                labelXML = labelXML.Replace("locAct", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelAct));
                labelXML = labelXML.Replace("locSignature", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelSignature));
                labelXML = labelXML.Replace("locDate", GlobalResourceManager.Instance.GetGlobalResourceString(_reclocLabelDate));

                //Replace 5 sensitive characters of XML before passing them
                Regex badAmpersand = new Regex("&(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
                Regex badLessThan = new Regex("<(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
                Regex badGreaterThan = new Regex(">(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
                Regex badDoubleQuotation = new Regex("\"(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
                Regex badapostrophes = new Regex("'(?![a-zA-Z]{2,6};|#[0-9]{2,4};)");
                const string goodAmpersand = "&amp;";
                const string goodLessThan = "&lt;";
                const string goodGreaterThan = "&gt;";
                const string goodDoubleQuotation = "&quot;";
                const string goodapostrophes = "&apos;";

                //Set the values to Placeholder
                labelXML = labelXML.Replace("[Job]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurementSummary.MeasurementName, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if Measurement name contains "&" letter or rest of 4 letters
                labelXML = labelXML.Replace("[Device]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(model.DeviceName, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if Device name contains "&" letter or rest of 4 letters
                labelXML = labelXML.Replace("[Metric]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurementSummary.MetricTargetPresetName, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if MTP name contains "&" letter or rest of 4 letters
                labelXML = labelXML.Replace("[PrintStandard]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurementSummary.PrintStandardType, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if PrintStandardType name contains "&" letter or rest of 4 letters
                labelXML = labelXML.Replace("[Timestamp]", measurementSummary.MeasurementDate.ToString("g"));
                labelXML = labelXML.Replace("[Customer]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurementSummary.Customer, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if Customer name contains "&" letter or rest of 4 letters
                labelXML = labelXML.Replace("[dEType]", measurementSummary.dEMethod);
                labelXML = labelXML.Replace("[PageOrSurface]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurementSummary.PageOrSurface, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if PageOrSurface contains "&" letter or rest of 4 letters
                labelXML = labelXML.Replace("[JDFJobId]", badapostrophes.Replace(badDoubleQuotation.Replace(badGreaterThan.Replace(badLessThan.Replace(badAmpersand.Replace(measurementSummary.JDFJobId, goodAmpersand), goodLessThan), goodGreaterThan), goodDoubleQuotation), goodapostrophes)); //Here "&" is replaced by "&amp" and so on other 4 sesitive characters in XML in order to display correctly if JDFJobId contains "&" letter or rest of 4 letters
                                                                                                                                                                                                                                                                                                  //labelXML = labelXML.Replace("[Job]", measurmentSummary.MeasurementName);
                StringBuilder metricName = new StringBuilder();
                StringBuilder metricTol = new StringBuilder();
                StringBuilder metricActual = new StringBuilder();
                bool appendNewLine = true;
                foreach (var item in measurementSummary.ValidationMetrics)
                {
                    appendNewLine = true;
                    if (measurementSummary.ValidationMetrics.Count == measurementSummary.ValidationMetrics.IndexOf(item) + 1)
                        appendNewLine = false;
                    metricName.Append(item.Metric + (appendNewLine ? Environment.NewLine : ""));
                    metricTol.Append(item.Tolerance.ToString() + (appendNewLine ? Environment.NewLine : ""));
                    metricActual.Append(item.Actual.ToString() + (appendNewLine ? Environment.NewLine : ""));
                }
                labelXML = labelXML.Replace("[MetricCaption]", metricName.ToString());
                labelXML = labelXML.Replace("[dTol]", metricTol.ToString());
                labelXML = labelXML.Replace("[dActual]", metricActual.ToString());
                labelXML = labelXML.Replace("\r\n", @"\ \r\n");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "LabelXML", " function GetLabelXml() { return '" + labelXML.ToString() + "'; };", true);

                string imagePath = "";
                if (measurementSummary.MeasurementStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                    imagePath = HostingEnvironment.MapPath("~/Images/VStatusSuccessIcon.png");
                else
                    imagePath = HostingEnvironment.MapPath("~/Images/VStatusFailedIcon.png");


                byte[] binaryData = File.ReadAllBytes(imagePath);
                string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ImageString", " function GetStatusIconImageString() { return '" + base64String + "' ;};", true);

                httpResponse.Data = new
                {
                    labelPrintString= Regex.Unescape(labelXML.ToString()),
                    dymoStatusIcon = base64String
                };
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
        public HttpResponseMessage DownloadFile(string filePath)
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(filePath);
            var fileName = Path.GetFileName(fullPath);
            var stream = new FileStream(fullPath, FileMode.Open);

            // processing the stream.
            var result = new FileHttpResponseMessage(fullPath);            
            result.StatusCode = HttpStatusCode.OK;
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            //stream.Close();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //delete the file
            //File.Delete(fullPath);

            return result;
        }

        //[HttpGet]
        //public HttpResponseMessage DownloadFile(string filePath)
        //{
        //    var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(filePath);
        //    var fileName = Path.GetFileName(fullPath);
        //    var stream = new FileStream(fullPath, FileMode.Open);

        //    // processing the stream.
        //    var result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StreamContent(stream)
        //    };
        //    result.Content.Headers.ContentDisposition =
        //        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = fileName
        //        };
        //    result.Content.Headers.ContentType =
        //        new MediaTypeHeaderValue("application/octet-stream");

        //    //stream.Close();
        //    //GC.Collect();
        //    //GC.WaitForPendingFinalizers();
        //    //delete the file
        //    //File.Delete(fullPath);

        //    return result;
        //}


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
