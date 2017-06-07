using FujiFilmUSA.Sync.Service.Helper;
using FujiFilmUSA.Sync.Service.Models;
using FujiFilmUSA.Sync.Service.Utility;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
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
    public class ChartController : ApiController
    {
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(BaseResponse<FileResponse>))]
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, Type = typeof(BaseResponse<FileResponse>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(BaseResponse<FileResponse>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(BaseResponse<FileResponse>))]
        public async Task<IHttpActionResult> GenerateFiles(IccFileParams param)
        {      
            string output = JsonConvert.SerializeObject(param);
            IHttpActionResult result = null;

            BaseResponse<FileResponse> response = new BaseResponse<FileResponse>();
            FileResponse responseUpload = new FileResponse();
            try
            {
                if (param == null || param.Patches == null || param.Configurations == null || param.Patches.Count == 0)
                {
                    response.Error.UserMessage = "Insufficient parameters have been provided.";
                    response.Error.InternalMessage = "Insufficient parameters have been provided.";
                    result = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, response));
                }
                else
                {
                    PdfConfigurations property = new PdfConfigurations();
                    property = param.Configurations;

                    ChartUtility chart = new ChartUtility(property.NumberOfPatches, property.ChartPages, property.PatchWidth,
                        property.PatchHeight, property.ChartBorder, property.ChartSizeW, property.ChartSizeH,
                        property.RowsPerChart, property.ColumsPerChart, property.Vgap, property.Hgap,
                        property.GapLeval, property.PreviousL, property.ChartName);

                    var image = await chart.GenerateColorListChart(param.Patches);

                    var fileDirPath = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationManager.AppSettings["ZipDirectoryPath"]);
                    string fileDirPDFPath = fileDirPath + string.Format("{0}", DateTime.Now.ToString("ddMMyyyyhhmmssffftt"));
                    if (!Directory.Exists(fileDirPDFPath))
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Directory.CreateDirectory(fileDirPDFPath);
                    }

                    DirectoryInfo dirFile = new DirectoryInfo(fileDirPDFPath);
                    PdfUtility pdfUtil = new PdfUtility();
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
                        responseUpload.FilePath = destinationPath;
                        responseUpload.FileName = Path.GetFileName(destinationPath);                        
                    }
                    else
                    {                        
                        throw new Exception("There is some error while generating PDF files.");
                    }
                    response.Data = responseUpload;
                    result = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, response));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog("From Controller : " + ex.Message);

                responseUpload.FilePath = "";
                response.Data = responseUpload;
                response.Error.InternalMessage = ex.Message;
                response.Error.UserMessage = "There is some problem.";
                result = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, response));
            }            
            return result;
        }
    }
}
