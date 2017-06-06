using FujiFilmUSA.Sync.Service.Helper;
using FujiFilmUSA.Sync.Service.Models;
using FujiFilmUSA.Sync.Service.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class ChartPDFController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> GetGraphicsChartFiles(ICCFileParams param)
        {
            //ICCFileParams param
            //string content = string.Empty;
            //var param = new JavaScriptSerializer().Deserialize<ICCFileParams>(content);

            //ICCFileParams param = new ICCFileParams();
            string output = JsonConvert.SerializeObject(param);
            IHttpActionResult result = null;

            BaseResponse<FileResponse> response = new BaseResponse<FileResponse>();
            FileResponse responseUpload = new FileResponse();
            try
            {
                if (param == null || param.wedge == null || param.property == null)
                {
                    result = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, response));
                }
                else
                {
                    PDFProperty property = new PDFProperty();
                    property = param.property;

                    ChartUtility chart = new ChartUtility(property.numberOfPatches, property.chartPages, property.patchWidth,
                        property.patchHeight, property.chartBorder, property.chartSizeW, property.chartSizeH,
                        property.rowsPerChart, property.columsPerChart, property.vGap, property.hGap,
                        property.gapLeval, property.previousL, property.chartName);

                    var image = await chart.GenerateColorListChart(param.wedge);

                    var fileDirPath = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ZipDirectoryPath"]);
                    string fileDirPDFPath = fileDirPath + string.Format("{0}", DateTime.Now.ToString("ddMMyyyyhhmmssffftt"));
                    if (!Directory.Exists(fileDirPDFPath))
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Directory.CreateDirectory(fileDirPDFPath);
                    }

                    DirectoryInfo dirFile = new DirectoryInfo(fileDirPDFPath);
                    PDFUtility pdfUtil = new PDFUtility();
                    pdfUtil.IT_MemoryBufferSize = 4800000;
                    pdfUtil.IT_PhotometricInterpretation = "CMYK";
                    pdfUtil.IT_ResolutionUnit = "cm";
                    pdfUtil.IT_SaveAlphaChannel = false;
                    pdfUtil.IT_XResolutionNumerator = 30;
                    pdfUtil.IT_XResolutionDenominator = 1;
                    pdfUtil.IT_YResolutionNumerator = 30;
                    pdfUtil.IT_YResolutionDenominator = 1;
                    if (await pdfUtil.SaveAsPDF(dirFile, image) == 0)
                    {
                        string sourcePath = fileDirPDFPath;
                        string destinationPath = sourcePath + ".zip";

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        ZipFile.CreateFromDirectory(sourcePath, destinationPath, CompressionLevel.Fastest, true);


                        if (Directory.Exists(sourcePath))
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            Directory.Delete(sourcePath, true);
                        }


                        responseUpload.isSuccess = true;
                        responseUpload.FilePath = destinationPath;
                        responseUpload.FileName = Path.GetFileName(destinationPath);
                        responseUpload.Message = "success";
                    }
                    else
                    {
                        responseUpload.isSuccess = false;
                        responseUpload.FilePath = "";
                        responseUpload.FileName = "";
                        responseUpload.Message = "Pdf not created";
                    }
                    response.Data = responseUpload;
                    response.status = ResponseStatus.Success;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog("From Controller : " + ex.Message);

                responseUpload.isSuccess = false;
                responseUpload.FilePath = "";
                responseUpload.Message = ex.Message;
                response.Data = responseUpload;
                response.status = ResponseStatus.Business_CustomError;
                //result = ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, ex));
            }

            result = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, response));
            return result;
        }
    }
}
