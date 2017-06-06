using FujiFilmUSA.Sync.Service.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Utility
{
    public class PDFUtility
    {
        public PDFUtility()
        {
            outputZ = new MemoryStream();
        }
        public int IT_MemoryBufferSize { get; set; }
        public string IT_PhotometricInterpretation { get; set; }
        public string IT_ResolutionUnit { get; set; }
        public bool IT_SaveAlphaChannel { get; set; }
        public int IT_totalColors { get; set; }
        public int IT_XResolutionDenominator { get; set; }
        public int IT_XResolutionNumerator { get; set; }
        public int IT_YResolutionDenominator { get; set; }
        public int IT_YResolutionNumerator { get; set; }
        public bool multicolor { get; set; }
        public MemoryStream outputZ { get; set; }
        public byte[] BitmapToByteArray { get; set; }
        private Object thisLock = new Object();

        public async Task<int> SaveAsPDF(DirectoryInfo SaveInFolder, List<Bitmap> PictureObjects, Bitmap CMYKAlphaChannelPictureObject = null)
        {
            int returnCode = 0;
            int row = 0;
            int column = 0;
            int width = 0;
            int height = 0;
            int widthMinusOne = 0;
            int heightMinusOne = 0;
            int numChannels = 0;
            int bufferSize = 0;
            int memOffset = 0;
            int totalBytes = 0;
            int i = 0;
            long tempPosition = 0;
            int IFDEntryCount = 0;
            int IFDOffset = 0;
            int IFDValueOffset = 0;
            int IFDWordBoundaryAdjustment = 0;
            bool writeBitsPerSample = false;
            MemoryStream RAMBuffer = new MemoryStream();
            //BinaryDataWriter RAMBuffer = new BinaryDataWriter();
            Graphics pictureMask;
            Graphics blackChannel;
            string line1 = null;
            long streamstart = 0;
            long streamend = 0;
            long stream1 = 0;
            long stream2 = 0;

            
            try
            {
                int currentFileName = 1;
                foreach (var pictureObject in PictureObjects)
                {
                    PDFHelper pdf = new PDFHelper('P', "mm", "letter");
                    DirectoryInfo SaveInFolderItem = new DirectoryInfo(SaveInFolder.FullName + "/PDFFile" + currentFileName.ToString() + ".pdf");
                    //validate the image
                    returnCode = ValidateParametersAndProperties(SaveInFolderItem, pictureObject, CMYKAlphaChannelPictureObject);
                    if ((returnCode != 0))
                    {
                        return returnCode;
                    }
                    else
                    {
                        lock (thisLock)
                        {
                            int pageObj = 0;
                            FileStream fileStream = File.Open(SaveInFolderItem.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            pdf.b = new BinaryDataWriter(fileStream);
                            if (pdf.b == null)
                            {
                                return 1000;
                            }
                            else
                            {
                                width = pictureObject.Width;
                                height = pictureObject.Height;
                                widthMinusOne = width - 1;
                                heightMinusOne = height - 1;
                                returnCode = GetNumChannels();

                                if (returnCode == 200)       // 200 = unknown error
                                    return returnCode;
                                else
                                    numChannels = returnCode;   // Set the number of channels to write

                                totalBytes = (width * height * numChannels);

                                if (multicolor)
                                    pageObj = 9;
                                else
                                    pageObj = 6;

                                pdf.putHeader();
                                pdf.newObject();
                                pdf.WriteString("<<" + ((char)10) + "/OpenAction <<" + ((char)10) + "/D [ " + Convert.ToString(pageObj) + " 0 R /FitV -32768]" + ((char)10) + "/S /GoTo" + ((char)10) + "/Type /Action" + ((char)10) + ">>" + ((char)10) + "/Pages 3 0 R" + ((char)10) + "/Type /Catalog" + ((char)10) + ">>\n");
                                pdf.endObject();

                                pdf.newObject();
                                pdf.WriteString("<<" + ((char)10) + "/Producer (CPS_PDFWriter)" + ((char)10) + ">>\n");
                                pdf.endObject();

                                string heightPT = Convert.ToDouble(height * 72 / 76.2).ToString("#.0000");
                                string widthPT = Convert.ToDouble(width * 72 / 76.2).ToString("#.0000");

                                pdf.newObject();
                                pdf.WriteString("<<" + ((char)10) + "/Count 1" + ((char)10) + "/Kids [ " + Convert.ToString(pageObj) + " 0 R]" + ((char)10) + "/MediaBox [ 0 0 " + widthPT + " " + heightPT + "]" + ((char)10) + "/Resources <<" + ((char)10) + "/ColorSpace <<\n");




                                pdf.WriteString("/Lab [ /Lab <<" + ((char)10) + "/Range [ -128 127 -128 127]" + ((char)10) + "/WhitePoint [ 0.9642 1 0.825]" + ((char)10) +
                                    ">>]" + ((char)10) + ">>" + ((char)10) + "/XObject <<" + ((char)10) + "/Image0 4 0 R" + ((char)10) + ">>" + ((char)10) +
                                    ">>" + ((char)10) + "/Type /Pages" + ((char)10) + ">>\n");
                                pdf.endObject();


                                bufferSize = (height * width) * 4;
                                var bmp = new LockBitmap(pictureObject);
                                pdf.newObject();
                                pdf.WriteString("<<" + ((char)10) + "/BitsPerComponent 8" + ((char)10) + "/ColorSpace /DeviceCMYK" + ((char)10) + "/Filter /FlateDecode" + ((char)10) +
                                 "/Height " + Convert.ToString(height) + ((char)10) + "/Length " + Convert.ToString(bufferSize) + ((char)10) +
                                "/Subtype /Image" + ((char)10) + "/Type /XObject" + ((char)10) + "/Width " + Convert.ToString(width) + ((char)10) +
                                ">>" + ((char)10) + "stream\n");

                                pdf.WriteByteArray(bmp.Pixels);
                                pdf.WriteString("\n");
                                pdf.WriteString("endstream\n");
                                pdf.endObject();



                                pdf.newObject();
                                String ImageStr = ("% BeginGraphic" + ((char)10) + "q" + ((char)10) + "1 0 0 1 0.00 0.00 cm" + ((char)10) +
                                        "q " + widthPT + " 0 0 " + heightPT + " 0.00 0.00 cm /Image0 Do" + ((char)10) + "Q" + ((char)10) +
                                           "Q" + ((char)10) + "% EndGraphic" + ((char)10) + "\n");
                                pdf.WriteString("<<" + ((char)10) + "/Length " + Convert.ToString(ImageStr.Length) + ((char)10) + ">>" + ((char)10) + "stream\n");
                                pdf.WriteString(ImageStr);

                                pdf.WriteString("endstream\n");
                                pdf.endObject();

                                pdf.newObject();
                                pdf.WriteString("<<" + ((char)10) + "/Contents 5 0 R" + ((char)10) + "/Parent 3 0 R" + ((char)10) + "/Type /Page" + ((char)10) + ">>\n");
                                pdf.endObject();

                                String lastref = pdf.currentPosition();


                                pdf.WriteString("xref" + ((char)10) + ((char)10) + "0 7\n");

                                pdf.WriteString("0000000000 65535 f\n");

                                //put in the xrefs stuff here
                                pdf.writeXrefs();

                                pdf.WriteString("trailer" + ((char)10) + "<<" + ((char)10) + "/Size 7" + ((char)10) +
                                        "/Root 1 0 R" + ((char)10) + "/Info 2 0 R" + ((char)10) +
                                        ">>" + ((char)10) + ((char)10) + "startxref" + ((char)10) + lastref + "%%EOF\n");

                                pdf.b.Close();
                                currentFileName++;
                            }
                        }
                    }
                }
                return 0;                
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog("From PDF Utility : " + ex.Message);
                throw ex;
            }
            //pdf.b.Close();
            return 1000;
        }

        public int ValidateParametersAndProperties(DirectoryInfo SaveInFolderItem, Bitmap PictureObject, Bitmap CMYKAlphaChannelPictureObject)
        {
            try
            {
                switch (IT_PhotometricInterpretation)
                {
                    case "RGB":
                        break;
                    case "CMYK":
                        break;
                    default:
                        return 1;
                }

                if (PictureObject == null)
                {
                    return 2;
                }

                if ((IT_PhotometricInterpretation == "CMYK") & (IT_SaveAlphaChannel == true))
                {
                    if ((CMYKAlphaChannelPictureObject == null))
                    {
                        return 3;
                    }
                    else
                    {
                        // Both picture objects are valid, so check that both have the same dimensions
                        if ((PictureObject.Width != CMYKAlphaChannelPictureObject.Width) | (PictureObject.Height != CMYKAlphaChannelPictureObject.Height))
                        {
                            return 4;
                        }
                    }
                }

                if (SaveInFolderItem == null)
                {
                    return 5;
                }

                if (IT_MemoryBufferSize < 1000)
                {
                    IT_MemoryBufferSize = 1000;
                }


                if (IT_XResolutionNumerator / IT_XResolutionDenominator <= 0)
                {
                    IT_XResolutionNumerator = 72;
                    IT_XResolutionDenominator = 1;
                }

                if (IT_YResolutionNumerator / IT_YResolutionDenominator <= 0)
                {
                    IT_YResolutionNumerator = 72;
                    IT_YResolutionDenominator = 1;
                }

                if (PictureObject.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                {
                    return 6;
                }

                if ((IT_PhotometricInterpretation == "CMYK") & (IT_SaveAlphaChannel == true))
                {
                    if ((CMYKAlphaChannelPictureObject.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                    {
                        return 7;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog("From ValidateParametersAndProperties : " + ex.Message);
                return 100;
            }
        }

        public int GetNumChannels()
        {
            try
            {
                int numChannels = 0;
                int alpha = 0;


                if (IT_SaveAlphaChannel == false)
                {
                    alpha = 0;
                }
                else
                {
                    alpha = 1;
                }

                switch (IT_PhotometricInterpretation)
                {
                    case "RGB":
                        numChannels = 3 + alpha;
                        break;
                    case "CMYK":
                        numChannels = 4 + alpha;
                        break;
                }

                return numChannels;
            }
            catch (Exception)
            {
                return 200;
            }
        }
    }
}