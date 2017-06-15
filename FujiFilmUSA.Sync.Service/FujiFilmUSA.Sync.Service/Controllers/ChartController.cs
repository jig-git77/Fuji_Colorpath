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
        [SwaggerResponse(HttpStatusCode.PreconditionFailed, Type = typeof(BaseErrorResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(BaseErrorResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(BaseErrorResponse))]
        public async Task<IHttpActionResult> GenerateFiles([FromBody]IccFileParams param)
        {
            IHttpActionResult result = null;
            BaseResponse<FileResponse> response = new BaseResponse<FileResponse>();
            FileResponse responseUpload = new FileResponse();

            bool isValidParameter = false;
            string validationMessage = "";
            try
            {

                if (param == null || param.Patches == null || param.Configurations == null || param.Patches.Count == 0)
                {
                    validationMessage = "Invalid patches have been provided.";
                    isValidParameter = false;
                }
                else if (param.Configurations.NumberOfPatches <= 0)
                {
                    validationMessage = "Number of patches provided are insuffiecient ";
                    isValidParameter = false;
                }
                else if (param.Configurations.ChartPages <= 0)
                {
                    validationMessage = "Provided chart pages are insuffiecient.";
                    isValidParameter = false;
                }
                else if (!(param.Configurations.PatchWidth >= 15 && param.Configurations.PatchWidth < 30))
                {
                    validationMessage = "Patch width should be between 15 and 30.";
                    isValidParameter = false;
                }
                else if (!(param.Configurations.PatchHeight >= 15 && param.Configurations.PatchHeight < 30))
                {
                    validationMessage = "Patch height should be between 15 and 30.";
                    isValidParameter = false;
                }
                else if (!(param.Configurations.RowsPerChart >= 20 && param.Configurations.RowsPerChart < 30))
                {
                    validationMessage = "Rows per chart should be between 15 and 30.";
                    isValidParameter = false;
                }
                else if (!(param.Configurations.ColumsPerChart >= 20 && param.Configurations.ColumsPerChart < 30))
                {
                    validationMessage = "Columns per chart should be between 15 and 30.";
                    isValidParameter = false;
                }
                else if (param.Configurations.Vgap <= 0)
                {
                    validationMessage = "Vgap should be greater than 0.";
                    isValidParameter = false;
                }
                else if (param.Configurations.GapLeval <= 0)
                {
                    validationMessage = "GapLeval should be greater than 0.";
                    isValidParameter = false;
                }
                else if (string.IsNullOrEmpty(param.Configurations.ChartName))
                {
                    validationMessage = "Chart name is required.";
                    isValidParameter = false;
                }
                else
                {
                    isValidParameter = true;
                }

                if (!isValidParameter)
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    result = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));
                }
                else
                {
                    PdfConfigurations property = new PdfConfigurations();
                    property = param.Configurations;

                    //Chart utility object
                    ChartUtility chart = new ChartUtility(property.NumberOfPatches, property.ChartPages, property.PatchWidth,
                        property.PatchHeight, property.ChartBorder, property.ChartSizeW, property.ChartSizeH,
                        property.RowsPerChart, property.ColumsPerChart, property.Vgap, property.Hgap,
                        property.GapLeval, property.PreviousL, property.ChartName);

                    //Generate images from the patches
                    var image = await chart.GenerateColorListChart(param.Patches);

                    string zipDirFolderName = Convert.ToString(ConfigurationManager.AppSettings["ZipDirectoryPath"]);
                    var fileDirPath = System.Web.Hosting.HostingEnvironment.MapPath(zipDirFolderName);
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

                        //createing the path
                        string zipfolderName = Path.GetDirectoryName(destinationPath).Split(Path.DirectorySeparatorChar).Last();
                        string fullVirtualPath = string.Format("{0}{1}/{2}", Convert.ToString(ConfigurationManager.AppSettings["ServiceURL"]), zipfolderName, Path.GetFileName(destinationPath));
                        responseUpload.FilePath = fullVirtualPath;
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
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                ErrorLog.WriteErrorLog("From Controller : " + ex.Message);
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                result = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));
            }
            return result;
        }
    }
}
