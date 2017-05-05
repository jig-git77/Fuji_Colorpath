using FujiFilmSyncExt.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujiFilmSyncExt.Utill
{
    public class PDFConverter
    {
        public PDFConverter()
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




       // [DllImport("zlib1.dll")]
       //public static extern int SetForegroundWindow(IntPtr point);

        //public int SaveAsPDF(DirectoryInfo SaveInFolderItem, Bitmap PictureObject, Bitmap CMYKAlphaChannelPictureObject = null)
        //{
        //    PDFHelper pdf = new PDFHelper('P', "mm", "letter");
        //    pdf.outFileStream = new FileStream(SaveInFolderItem.FullName, FileMode.Create, FileAccess.Write);
        //    string str1 = "%PDF-1.4\r\n";
        //    pdf.writeString(str1);



        //    return 0;
        //}

        public int SaveAsPDF(DirectoryInfo SaveInFolderItem, Bitmap PictureObject, Bitmap CMYKAlphaChannelPictureObject = null)
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

            PDFHelper pdf = new PDFHelper('P', "mm", "letter");


            long streamstart = 0;
            long streamend = 0;
            long stream1 = 0;
            long stream2 = 0;

            returnCode = ValidateParametersAndProperties(SaveInFolderItem, PictureObject, CMYKAlphaChannelPictureObject);

            if ((returnCode != 0))
            {
                return returnCode;
            }
            else
            {
                //pdf.b = new BinaryDataReader(File.Open(SaveInFolderItem.FullName, FileMode.Open));
                //BinaryReader reader = new BinaryDataReader(File.Open(SaveInFolderItem.FullName, FileMode.Open));
                int pageObj = 0;
                FileStream fileStream = File.Open(SaveInFolderItem.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                pdf.b = new BinaryDataWriter(fileStream);

                if (pdf.b == null)
                {
                    return 1000;
                }
                else
                {
                    width = PictureObject.Width;
                    height = PictureObject.Height;
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
                    pdf.write("<<" + ((char)10) + "/OpenAction <<" + ((char)10) + "/D [ " + Convert.ToString(pageObj) + " 0 R /FitV -32768]" + ((char)10) + "/S /GoTo" + ((char)10) + "/Type /Action" + ((char)10) + ">>" + ((char)10) + "/Pages 3 0 R" + ((char)10) + "/Type /Catalog" + ((char)10) + ">>");
                    pdf.endObject();

                    pdf.newObject();
                    pdf.write("<<" + ((char)10) + "/Producer (CPS_PDFWriter)" + ((char)10) + ">>");
                    pdf.endObject();

                    string heightPT = Convert.ToDouble(height * 72 / 76.2).ToString("#.0000");
                    string widthPT = Convert.ToDouble(width * 72 / 76.2).ToString("#.0000");

                    pdf.newObject();
                    pdf.write("<<" + ((char)10) + "/Count 1" + ((char)10) + "/Kids [ " + Convert.ToString(pageObj) + " 0 R]" + ((char)10) + "/MediaBox [ 0 0 " + widthPT + " " + heightPT + "]" + ((char)10) + "/Resources <<" + ((char)10) + "/ColorSpace <<");

                    if (multicolor)
                    {
                        pdf.write("/CS0 4 0 R");
                    }

                    int imageObj = 0;
                    if (multicolor)
                        imageObj = 7;
                    else
                        imageObj = 4;

                    pdf.write("/Lab [ /Lab <<" + ((char)10) + "/Range [ -128 127 -128 127]" + ((char)10) + "/WhitePoint [ 0.9642 1 0.825]" + ((char)10) +
                        ">>]" + ((char)10) + ">>" + ((char)10) + "/XObject <<" + ((char)10) + "/Image0 " + Convert.ToString(imageObj) + " 0 R" + ((char)10) + ">>" + ((char)10) +
                        ">>" + ((char)10) + "/Type /Pages" + ((char)10) + ">>");
                    pdf.endObject();

                    if (multicolor)
                    {
                        pdf.newObject();
                        pdf.write("[ /DeviceN [ /Cyan /Magenta /Yellow /Black /Orange] /DeviceCMYK 5 0 R]");
                        pdf.endObject();

                        pdf.newObject();
                        pdf.write("<<" + ((char)10) + "/BitsPerSample 8" + ((char)10) + "/Domain [ 0 1 0 1 0 1 0 1 0 1]" + ((char)10) + "/Filter [ /FlateDecode]" + ((char)10) + "/FunctionType 0" + ((char)10) + "/Length 6 0 R" + ((char)10) + "/Range [ 0 1 0 1 0 1 0 1]" + ((char)10) + "/Size [ 2 2 2 2 2]" + ((char)10) + ">>" + ((char)10) + "stream");

                        MemoryStream outputZZ = new MemoryStream();
                        //pending
                    }

                    streamstart = pdf.b.BaseStream.Position;
                    bufferSize = IT_MemoryBufferSize - (IT_MemoryBufferSize % (numChannels + 1));

                    //RAMBuffer = new MemoryStream(bufferSize);



                    if (RAMBuffer == null)
                    {
                        pdf.b.Close();
                        SaveInFolderItem.Delete();
                        return 1000;
                    }

                    memOffset = 0;
                    //for (row = 0; row < heightMinusOne; row++)
                    //{
                    //    for (column = 0; column < widthMinusOne; column++)
                    //    {
                    //        Color clr = PictureObject.GetPixel(column, row);
                    //        string colorHex = "#" + clr.R.ToString("X2") + clr.G.ToString("X2") + clr.B.ToString("X2");
                    //        byte[] array = Encoding.ASCII.GetBytes(colorHex);
                    //        Array.Reverse(array);
                    //        RAMBuffer.Write(array, memOffset, 24);
                    //        memOffset = memOffset + 3;

                    //        //if(test.A != 255)
                    //        //{
                    //        //    var arb = test.A;
                    //        //}

                    //    }
                    //}

                    tempPosition = pdf.b.BaseStream.Position;
                    RAMBuffer.Position = 0;
                    var strm = new StreamReader(RAMBuffer);
                    var stringRAMBuffer = strm.ReadToEnd();
                    pdf.write(stringRAMBuffer);

                    pdf.write(((char)10) + ((char)10) + "endstream");
                    pdf.endObject();
                    //pdf.write("<<" + ((char)10) + "/Filter /FlateDecode" + ((char)10) + "/Length " 
                    //    + Convert.ToString(output1.Size) + ((char)10) + ">>" + ((char)10) + "stream");


                }
            }

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
            catch (Exception)
            {
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
