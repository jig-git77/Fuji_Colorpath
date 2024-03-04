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
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class ChartController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> GenerateFiles([FromBody] IccFileParams param)
        {
            IHttpActionResult result = null;
            BaseResponse<FileResponse> response = new BaseResponse<FileResponse>();
            FileResponse responseUpload = new FileResponse();
            var resultFile = new FileHttpResponseMessage();
            bool isValidParameter = false;
            string validationMessage = "";
            try
            {
                //validation for parameters
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
                else if (param.Configurations.PatchWidth <= 0)
                {
                    validationMessage = "Provided patch width are insuffiecient.";
                    isValidParameter = false;
                }
                else if (param.Configurations.PatchHeight <= 0)
                {
                    validationMessage = "Provided patch height are insuffiecient.";
                    isValidParameter = false;
                }
                else if (param.Configurations.RowsPerChart <= 0)
                {
                    validationMessage = "Provided rows per chart are insuffiecient.";
                    isValidParameter = false;
                }
                else if (param.Configurations.ColumsPerChart <= 0)
                {
                    validationMessage = "Provided columns per chart are insuffiecient.";
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

                //checks parmeters valid or not
                if (!isValidParameter)
                {
                    BaseErrorResponse errprResponse = new BaseErrorResponse();
                    errprResponse.Error.Message = validationMessage;
                    errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
                    result = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
                }
                else//if valid parameters
                {
                    PdfConfigurations property = new PdfConfigurations();
                    property = param.Configurations;

                    //Chart utility object
                    ChartUtility chart = new ChartUtility(property.NumberOfPatches, property.ChartPages, property.PatchWidth,
                        property.PatchHeight, property.ChartBorder, property.ChartSizeW, property.ChartSizeH,
                        property.RowsPerChart, property.ColumsPerChart, property.Vgap, property.Hgap,
                        property.GapLeval, property.PreviousL, property.ChartName, property.ProfileName);

                    //Get Pdf filelocation from call
                    string strPdfPath = await chart.GenerateColorListChart(param.Patches);

                    //create the pdf
                    if (!string.IsNullOrWhiteSpace(strPdfPath) && File.Exists(strPdfPath))
                    {
                        var fileName = Path.GetFileName(strPdfPath);
                        FileStream stream = new FileStream(strPdfPath, FileMode.Open);
                        resultFile.filePath = strPdfPath;
                        resultFile.StatusCode = HttpStatusCode.OK;
                        resultFile.Content = new StreamContent(stream);
                        resultFile.Content.Headers.ContentDisposition =
                        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                        {
                            FileName = fileName
                        };
                        resultFile.Content.Headers.ContentType =
                            new MediaTypeHeaderValue("application/octet-stream");

                        ////check file directory is exists or not
                        //if (Directory.Exists(strPdfPath.Substring(0, strPdfPath.LastIndexOf("\\"))))
                        //{
                        //    //wait for pending processes
                        //    GC.Collect();
                        //    GC.WaitForPendingFinalizers();
                        //    Directory.Delete(strPdfPath.Substring(0, strPdfPath.LastIndexOf("\\")),true);
                        //}

                        return resultFile;
                    }
                    else
                    {
                        throw new Exception("There is some error while generating PDF files.");//if pdf not create then
                    }
                }
            }
            catch (Exception ex)
            {
                BaseErrorResponse errprResponse = new BaseErrorResponse();
                ErrorLog.WriteErrorLog("From Controller : " + ex.Message);
                errprResponse.Error.Message = ex.Message;
                errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
                result = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
            }

            return resultFile;
        }

        //[HttpPost]
        //public async Task<HttpResponseMessage> GenerateFiles_Old([FromBody]IccFileParams param)
        //{
        //    IHttpActionResult result = null;
        //    BaseResponse<FileResponse> response = new BaseResponse<FileResponse>();
        //    FileResponse responseUpload = new FileResponse();
        //    var resultFile = new FileHttpResponseMessage();
        //    bool isValidParameter = false;
        //    string validationMessage = "";
        //    try
        //    {
        //        //validation for parameters
        //        if (param == null || param.Patches == null || param.Configurations == null || param.Patches.Count == 0)
        //        {
        //            validationMessage = "Invalid patches have been provided.";
        //            isValidParameter = false;
        //        }
        //        else if (param.Configurations.NumberOfPatches <= 0)
        //        {
        //            validationMessage = "Number of patches provided are insuffiecient ";
        //            isValidParameter = false;
        //        }
        //        else if (param.Configurations.ChartPages <= 0)
        //        {
        //            validationMessage = "Provided chart pages are insuffiecient.";
        //            isValidParameter = false;
        //        }
        //        //else if (!(param.Configurations.PatchWidth >= 15 && param.Configurations.PatchWidth < 30))
        //        else if (param.Configurations.PatchWidth <= 0)
        //        {
        //            validationMessage = "Provided patch width are insuffiecient.";
        //            isValidParameter = false;
        //        }
        //        //else if (!(param.Configurations.PatchHeight >= 15 && param.Configurations.PatchHeight < 30))
        //        else if (param.Configurations.PatchHeight <= 0)
        //        {
        //            validationMessage = "Provided patch height are insuffiecient.";
        //            isValidParameter = false;
        //        }

        //        //else if (!(param.Configurations.RowsPerChart >= 20 && param.Configurations.RowsPerChart < 30))
        //        else if (param.Configurations.RowsPerChart <= 0)
        //        {
        //            validationMessage = "Provided rows per chart are insuffiecient.";
        //            isValidParameter = false;
        //        }
        //        //else if (!(param.Configurations.ColumsPerChart >= 20 && param.Configurations.ColumsPerChart < 30))
        //        else if (param.Configurations.ColumsPerChart <= 0)
        //        {
        //            validationMessage = "Provided columns per chart are insuffiecient.";
        //            isValidParameter = false;
        //        }
        //        else if (param.Configurations.Vgap <= 0)
        //        {
        //            validationMessage = "Vgap should be greater than 0.";
        //            isValidParameter = false;
        //        }
        //        else if (param.Configurations.GapLeval <= 0)
        //        {
        //            validationMessage = "GapLeval should be greater than 0.";
        //            isValidParameter = false;
        //        }
        //        else if (string.IsNullOrEmpty(param.Configurations.ChartName))
        //        {
        //            validationMessage = "Chart name is required.";
        //            isValidParameter = false;
        //        }
        //        else
        //        {
        //            isValidParameter = true;
        //        }

        //        //checks parmeters valid or not
        //        if (!isValidParameter)
        //        {
        //            BaseErrorResponse errprResponse = new BaseErrorResponse();
        //            errprResponse.Error.Message = validationMessage;
        //            errprResponse.Error.Code = (int)ResponseStatus.ValidationError;
        //            result = ResponseMessage(Request.CreateResponse(HttpStatusCode.PreconditionFailed, errprResponse));//returns 412 status code
        //        }
        //        else//if valid parameters
        //        {
        //            PdfConfigurations property = new PdfConfigurations();
        //            property = param.Configurations;

        //            //Chart utility object
        //            ChartUtility chart = new ChartUtility(property.NumberOfPatches, property.ChartPages, property.PatchWidth,
        //                property.PatchHeight, property.ChartBorder, property.ChartSizeW, property.ChartSizeH,
        //                property.RowsPerChart, property.ColumsPerChart, property.Vgap, property.Hgap,
        //                property.GapLeval, property.PreviousL, property.ChartName, property.ProfileName);

        //            //Generate images from the patches
        //            var image = await chart.GenerateColorListChart(param.Patches);

        //            string zipDirFolderName = Convert.ToString(ConfigurationManager.AppSettings["ZipDirectoryPath"]);
        //            var fileDirPath = System.Web.Hosting.HostingEnvironment.MapPath(zipDirFolderName);
        //            string fileDirPDFPath = fileDirPath + string.Format("{0}", DateTime.Now.ToString("ddMMyyyyhhmmssffftt"));
        //            //check file directory is exists or not
        //            if (!Directory.Exists(fileDirPDFPath))
        //            {
        //                //wait for pending processes
        //                GC.Collect();
        //                GC.WaitForPendingFinalizers();
        //                Directory.CreateDirectory(fileDirPDFPath);
        //            }

        //            //set the parameters which required to write the pdf
        //            DirectoryInfo dirFile = new DirectoryInfo(fileDirPDFPath);
        //            PdfUtility pdfUtil = new PdfUtility();
        //            pdfUtil.IT_MemoryBufferSize = 4800000;
        //            pdfUtil.IT_PhotometricInterpretation = "CMYK";
        //            pdfUtil.IT_ResolutionUnit = "cm";
        //            pdfUtil.IT_SaveAlphaChannel = false;
        //            pdfUtil.IT_XResolutionNumerator = 30;
        //            pdfUtil.IT_XResolutionDenominator = 1;
        //            pdfUtil.IT_YResolutionNumerator = 30;
        //            pdfUtil.IT_YResolutionDenominator = 1;

        //            //create the pdf
        //            if (await pdfUtil.SaveAsPDF(dirFile, image) == 0)
        //            {
        //                string sourcePath = fileDirPDFPath;
        //                string destinationPath = sourcePath + ".zip";

        //                GC.Collect();
        //                GC.WaitForPendingFinalizers();
        //                //create zip file of directory containing the pdf files
        //                ZipFile.CreateFromDirectory(sourcePath, destinationPath, CompressionLevel.Fastest, true);

        //                if (Directory.Exists(sourcePath))
        //                {
        //                    GC.Collect();
        //                    GC.WaitForPendingFinalizers();
        //                    Directory.Delete(sourcePath, true);//delete the directory
        //                }

        //                //createing the path
        //                string zipfolderName = Path.GetDirectoryName(destinationPath).Split(Path.DirectorySeparatorChar).Last();
        //                string fullVirtualPath = string.Format("{0}{1}/{2}", Convert.ToString(ConfigurationManager.AppSettings["ServiceURL"]), zipfolderName, Path.GetFileName(destinationPath));
        //                //responseUpload.FilePath = fullVirtualPath;
        //                //responseUpload.FileName = Path.GetFileName(destinationPath);

        //                var fileName = Path.GetFileName(destinationPath);
        //                var stream = new FileStream(destinationPath, FileMode.Open);
        //                resultFile.filePath = destinationPath;
        //                resultFile.StatusCode = HttpStatusCode.OK;
        //                resultFile.Content = new StreamContent(stream);
        //                resultFile.Content.Headers.ContentDisposition =
        //                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //                    {
        //                        FileName = fileName
        //                    };
        //                resultFile.Content.Headers.ContentType =
        //                    new MediaTypeHeaderValue("application/octet-stream");
        //                return resultFile;
        //            }
        //            else
        //            {
        //                throw new Exception("There is some error while generating PDF files.");//if pdf not create then
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BaseErrorResponse errprResponse = new BaseErrorResponse();
        //        ErrorLog.WriteErrorLog("From Controller : " + ex.Message);
        //        errprResponse.Error.Message = ex.Message;
        //        errprResponse.Error.Code = (int)ResponseStatus.InternalServer;
        //        result = ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errprResponse));//returns the 500 status code
        //    }
        //    return resultFile;
        //}

    }
}
