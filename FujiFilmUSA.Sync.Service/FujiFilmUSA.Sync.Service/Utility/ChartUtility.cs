using FujiFilmUSA.Sync.Service.Helper;
using FujiFilmUSA.Sync.Service.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using DynaPDF;
using System.Configuration;

namespace FujiFilmUSA.Sync.Service.Utility
{
    public class ChartUtility
    {
        int numberOfPatches = 1872;
        int chartPages = 1;
        int patchWidth = 27;
        int patchHeight = 27;
        int chartBorder = 40;
        int chartSizeW = 0;
        int chartSizeH = 0;
        int rowsPerChart = 24;
        int columsPerChart = 26;
        int vGap = 2;
        int hGap = 0;
        int gapLeval = 75;
        double previousL = 0;
        string chartName = "Pantone Test Chart";
        string profileName = "";

        //counstructor
        public ChartUtility() { }
        public ChartUtility(int numberOfPatchs, int chartpages, int patchwidth, int patchHeight,
            int chartborder, int chartsizeW, int chartsizeH, int rowsperchart, int columsperchart,
            int vgap, int hgap, int gapLval, double previousL, string chartname,string profileName)
        {
            //check value passed or not otherwise default
            if (numberOfPatchs != 0)
                this.numberOfPatches = numberOfPatchs;

            if (chartpages != 0)
                this.chartPages = chartpages;

            if (patchwidth != 0)
                this.patchWidth = patchwidth;

            if (patchHeight != 0)
                this.patchHeight = patchHeight;

            if (chartborder != 0)
                this.chartBorder = chartborder;

            if (chartsizeW != 0)
                this.chartSizeW = chartsizeW;

            if (chartsizeH != 0)
                this.chartSizeH = chartsizeH;

            if (rowsperchart != 0)
                this.rowsPerChart = rowsperchart;

            if (columsperchart != 0)
                this.columsPerChart = columsperchart;

            if (vgap != 0)
                this.vGap = vgap;

            if (hgap != 0)
                this.hGap = hgap;

            if (gapLval != 0)
                this.gapLeval = gapLval;

            if (previousL != 0)
                this.previousL = previousL;

            if (!string.IsNullOrEmpty(chartname))
                this.chartName = chartname;

            if (!string.IsNullOrEmpty(profileName))
                this.profileName = profileName;            
        }

        public async Task<string> GenerateColorListChart(List<Patch> patchs)
        {
            try
            {
                //We will use this array for column ID in PDF
                string[] columnID = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "E", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP" };
                //calculate height and width of image
                int totalVgap = vGap * (columsPerChart + 2);
                int totalHgap = hGap * (rowsPerChart + 2);
                chartSizeW = columsPerChart * patchWidth + (chartBorder * 2) + totalVgap;
                chartSizeH = rowsPerChart * patchHeight + (chartBorder * 2) + totalHgap;
                int TotalRows = numberOfPatches / columsPerChart;
                int NewrowsPerChart = 0;
                CPDF pdf = new CPDF();
                pdf.SetLicenseKey(Convert.ToString(ConfigurationManager.AppSettings["Dynaform_license_key"]));
                // Error messages and warnings are passed to the callback function.
                //pdf.SetOnErrorProc(IntPtr.Zero, new DynaPDF.TErrorProc(PDFError));
                pdf.CreateNewPDF(null); // We open the output file later if no error occurrs.

                pdf.SetDocInfo(DynaPDF.TDocumentInfo.diCreator, "Fujifim Colorpath Sync");
                pdf.SetDocInfo(DynaPDF.TDocumentInfo.diTitle, "Test Chart");
                pdf.SetPageWidth(chartSizeW);
                pdf.SetPageHeight(chartSizeH);

                int patchid = 0;
                for (int thispage = 1; thispage <= chartPages; thispage++)
                {
                    pdf.SetPageCoords(DynaPDF.TPageCoord.pcTopDown); // We use top down coordinates in this example
                    pdf.Append(); // An an empty page to the file
                    pdf.SetColorSpace(TPDFColorSpace.csDeviceCMYK);

                    //Start: -----Code to support unmatched rows for multiple page chart-----//
                    NewrowsPerChart = rowsPerChart * thispage;
                    if (TotalRows < NewrowsPerChart)
                    {
                        rowsPerChart = TotalRows - (rowsPerChart * (thispage - 1));
                    }
                    //End: --------------------//

                    //Get the patches list
                    List<Patch> listPatch = new List<Patch>();
                    if (patchs != null)
                        listPatch = patchs;

                    for (int i = 0; i < rowsPerChart; i++)
                    {
                        for (int j = 0; j < columsPerChart; j++)
                        {
                            Patch patch = listPatch[patchid];
                            if (j == 0 && patch.Cie_L >= gapLeval)
                            {
                                int patchX = chartBorder + (j * patchWidth) + ((j + 1) * vGap) - vGap;
                                int patchY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);

                                pdf.SetFillColor(pdf.CMYK(0, 0, 0, 255));
                                pdf.Rectangle(patchX, patchY, vGap, patchHeight, TPathFillMode.fmFill);
                            }

                            if (j == (columsPerChart - 1) && patch.Cie_L >= gapLeval)
                            {
                                int patchX = chartBorder + (j * patchWidth) + ((j + 1) * vGap) + patchWidth;
                                int patchY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);

                                pdf.SetFillColor(pdf.CMYK(0, 0, 0, 255));
                                pdf.Rectangle(patchX, patchY, vGap, patchHeight, TPathFillMode.fmFill);

                            }

                            byte CMYK_C = Convert.ToByte((int)(255 * (patch.Cmyk_C / 100)));
                            byte CMYK_M = Convert.ToByte((int)(255 * (patch.Cmyk_M / 100)));
                            byte CMYK_Y = Convert.ToByte((int)(255 * (patch.Cmyk_Y / 100)));
                            byte CMYK_K = Convert.ToByte((int)(255 * (patch.Cmyk_K / 100)));
                            decimal colorC = Convert.ToDecimal((patch.Cmyk_C));
                            decimal colorM = Convert.ToDecimal((patch.Cmyk_M));
                            decimal colorY = Convert.ToDecimal((patch.Cmyk_Y));
                            decimal colorK = Convert.ToDecimal((patch.Cmyk_K));

                            Color color_CMY = Color.FromArgb(255, (int)(255 * (colorY / 100)), (int)(255 * (colorM / 100)), (int)(255 * (colorC / 100)));
                            Color color_K = Color.FromArgb(255, (int)(255 * (colorK / 100)), 0, 0);

                            SolidBrush cmyBrush = new SolidBrush(color_CMY);
                            SolidBrush kBrush = new SolidBrush(color_K);

                            int cmyX = chartBorder + (j * patchWidth) + ((j + 1) * vGap);
                            int cmyY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);

                            pdf.SetFillColor(pdf.CMYK(CMYK_C, CMYK_M, CMYK_Y, CMYK_K));
                            pdf.Rectangle(cmyX, cmyY, patchWidth, patchHeight, TPathFillMode.fmFill);

                            patchid = patchid + 1;
                            if (j > 0)
                            {
                                if (previousL >= gapLeval && patch.Cie_L >= gapLeval)
                                {
                                    int patchX = chartBorder + (j * patchWidth) + ((j + 1) * vGap) - vGap;
                                    int patchY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);

                                    pdf.SetFillColor(pdf.CMYK(0, 0, 0, 255));
                                    pdf.Rectangle(patchX, patchY, vGap, patchHeight, TPathFillMode.fmFill);
                                }
                            }
                            previousL = patch.Cie_L;
                        }
                    }

                    string pagedchartname = chartName + " page# " + Convert.ToString(thispage) + " of " + Convert.ToString(chartPages);

                    string dateTimeString = "Created " + DateTime.Now.ToString();

                    pdf.SetFillColor(pdf.CMYK(0, 0, 0, 255));
                    pdf.SetFont("Arial", DynaPDF.TFStyle.fsNormal, 10.0, true, DynaPDF.TCodepage.cp1252);  // Before printing text you must set a font
                    pdf.WriteText(10, 20.0, dateTimeString); // Write some text

                    //For Profile details                    
                    string profileString = profileName;

                    pdf.EndPage(); // Close the open page
                }
                String filePath = "";
                // No fatal error occurred?
                if (pdf.HaveOpenDoc())
                {
                    // We write the output file into the current directory.
                    string zipDirFolderName = Convert.ToString(ConfigurationManager.AppSettings["ZipDirectoryPath"]);
                    var fileDirPath = System.Web.Hosting.HostingEnvironment.MapPath(zipDirFolderName);
                    string fileDirPDFPath = fileDirPath + string.Format("{0}", DateTime.Now.ToString("ddMMyyyyhhmmssffftt"));

                    //check file directory is exists or not
                    if (!Directory.Exists(fileDirPDFPath))
                    {
                        //wait for pending processes
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Directory.CreateDirectory(fileDirPDFPath);
                    }

                    filePath = fileDirPDFPath + "\\out.pdf";

                    // OK, now we can open the output file.
                    pdf.OpenOutputFile(filePath);
                    if (pdf.CloseFile())
                    {
                        Console.Write("PDF file \"{0}\" successfully created!\n", filePath);
                    }
                }
                pdf = null;
                return filePath;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteErrorLog("From Chart Utility : " + ex.Message);
                throw ex;
            }
            return null;
        }

    
        public Color CMYK(decimal c, decimal m, decimal y, decimal k)
        {
            Color color = new Color();
            try
            {
                decimal[] cmyk = new decimal[4];
                cmyk[0] = c;
                cmyk[1] = m;
                cmyk[2] = y;
                cmyk[3] = k;

                var A = (int)(255 * (cmyk[0] / 100));
                var r = (int)(255 * (cmyk[1] / 100));
                var g = (int)(255 * (cmyk[2] / 100));
                var b = (int)(255 * (cmyk[3] / 100));


                var hex = "#" + b.ToString("X2") + g.ToString("X2") + r.ToString("X2") + A.ToString("X2");

                System.Windows.Media.Color colorConvert = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hex);

                color = Color.FromArgb(colorConvert.A, colorConvert.R, colorConvert.G, colorConvert.B);
                //Color colorCMY = Color.FromArgb(255, (int)(255 * (cmyk[0] / 100)), (int)(255 * (cmyk[1] / 100)), (int)(255 * (cmyk[2] / 100)));

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return color;
        }

        #region Old_PDF zip creation code before DynaPDF
        //public async Task<List<Bitmap>> GenerateColorListChart_Old(List<Patch> patchs)
        //{
        //    try
        //    {          
        //        //calculate height and width of image
        //        int totalVgap = vGap * (columsPerChart + 2);
        //        int totalHgap = hGap * (rowsPerChart + 2);
        //        chartSizeW = columsPerChart * patchWidth + (chartBorder * 2) + totalVgap;
        //        chartSizeH = rowsPerChart * patchHeight + (chartBorder * 2) + totalHgap;
        //        int TotalRows = numberOfPatches / columsPerChart;
        //        int NewrowsPerChart = 0;

        //        List<Bitmap> listBitMapImage = new List<Bitmap>();
        //        int patchid = 0;
        //        for (int thispage = 1; thispage <= chartPages; thispage++)
        //        {
        //            //Start: -----Code to support unmatched rows for multiple page chart-----//
        //            NewrowsPerChart = rowsPerChart * thispage;
        //            if (TotalRows < NewrowsPerChart)
        //            {
        //                rowsPerChart = TotalRows - (rowsPerChart * (thispage - 1));
        //            }
        //            //End: --------------------//

        //            //create image
        //            Bitmap image = new Bitmap(chartSizeW, chartSizeH, PixelFormat.Format32bppArgb);
        //            Graphics graphics = System.Drawing.Graphics.FromImage(image);

        //            //create a second bitmap to store Black
        //            Bitmap image2 = new Bitmap(chartSizeW, chartSizeH, PixelFormat.Format32bppArgb);
        //            Graphics graphics2 = System.Drawing.Graphics.FromImage(image2);

        //            //draw main outside rectangle
        //            Color color = CMYK(0, 0, 0, 0);
        //            SolidBrush mainBrush = new SolidBrush(color);


        //            //set the attributes of image
        //            Color lineColor = CMYK(100, 100, 0, 0);
        //            Pen pen = new Pen(lineColor, 1);
        //            var k_Brush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));

        //            //temp
        //            Pen p = new Pen(Color.FromArgb(255, 255, 255, 255));
        //            Rectangle r = new Rectangle(5, 5, chartSizeW - 10, chartSizeH - 10);
        //            graphics.DrawRectangle(p, r);
        //            //temp

        //            //Get the patches list
        //            List<Patch> listPatch = new List<Patch>();
        //            if (patchs != null)
        //                listPatch = patchs;

        //            for (int i = 0; i < rowsPerChart; i++)
        //            {
        //                for (int j = 0; j < columsPerChart; j++)
        //                {
        //                    Patch patch = listPatch[patchid];
        //                    if (j == 0 && patch.Cie_L >= gapLeval)
        //                    {
        //                        int patchX = chartBorder + (j * patchWidth) + ((j + 1) * vGap) - vGap;
        //                        int patchY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);
        //                        graphics2.FillRectangle(k_Brush, patchX, patchY, vGap, patchHeight);
        //                    }

        //                    if (j == (columsPerChart - 1) && patch.Cie_L >= gapLeval)
        //                    {
        //                        int patchX = chartBorder + (j * patchWidth) + ((j + 1) * vGap) + patchWidth;
        //                        int patchY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);
        //                        graphics2.FillRectangle(k_Brush, patchX, patchY, vGap, patchHeight);

        //                    }

        //                    decimal colorC = Convert.ToDecimal((patch.Cmyk_C));
        //                    decimal colorM = Convert.ToDecimal((patch.Cmyk_M));
        //                    decimal colorY = Convert.ToDecimal((patch.Cmyk_Y));
        //                    decimal colorK = Convert.ToDecimal((patch.Cmyk_K));

        //                    Color color_CMY = Color.FromArgb(255, (int)(255 * (colorY / 100)), (int)(255 * (colorM / 100)), (int)(255 * (colorC / 100)));
        //                    Color color_K = Color.FromArgb(255, (int)(255 * (colorK / 100)), 0, 0);


        //                    SolidBrush cmyBrush = new SolidBrush(color_CMY);
        //                    SolidBrush kBrush = new SolidBrush(color_K);


        //                    int cmyX = chartBorder + (j * patchWidth) + ((j + 1) * vGap);
        //                    int cmyY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);
        //                    Rectangle cmykRect = new Rectangle(cmyX, cmyY, patchWidth, patchHeight);
        //                    graphics.FillRectangle(cmyBrush, cmykRect);
        //                    graphics2.FillRectangle(kBrush, cmykRect);


        //                    patchid = patchid + 1;
        //                    if (j > 0)
        //                    {
        //                        if (previousL >= gapLeval && patch.Cie_L >= gapLeval)
        //                        {
        //                            int patchX = chartBorder + (j * patchWidth) + ((j + 1) * vGap) - vGap;
        //                            int patchY = chartBorder + (i * patchHeight) + ((i + 1) * hGap);
        //                            graphics2.FillRectangle(k_Brush, patchX, patchY, vGap, patchHeight);
        //                        }
        //                    }
        //                    previousL = patch.Cie_L;
        //                }
        //            }

        //            string pagedchartname = chartName + " page# " + Convert.ToString(thispage) + " of " + Convert.ToString(chartPages);

        //            //For Main Text
        //            Font font = new Font("Arial", 14);
        //            ////chartsizeW, chartsizeH
        //            //// Measure string.
        //            SizeF stringSize = new SizeF();
        //            stringSize = graphics.MeasureString(pagedchartname, font);

        //            float txtX = (image.Width - stringSize.Width) / 2;
        //            float txtY = stringSize.Height / 2;
        //            graphics2.DrawString(pagedchartname, font, k_Brush, new PointF(txtX, txtY));

        //            //For DataTime details
        //            Font fontSub = new Font("Arial", 7);
        //            string dateTimeString = "Created " + DateTime.Now.ToString();
        //            graphics2.DrawString(dateTimeString, fontSub, k_Brush, new PointF(10, 20));


        //            //For Profile details                    
        //            string profileString = profileName;
        //            graphics2.DrawString(profileString, fontSub, k_Brush, new PointF(10, 10));

        //            await ProcessUsingLockbits(image, image2);
        //            listBitMapImage.Add(image);
        //            //return image;
        //        }
        //        return listBitMapImage;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.WriteErrorLog("From Chart Utility : " + ex.Message);
        //        throw ex;
        //    }
        //    return null;
        //}

        //private async Task<byte[]> ProcessUsingLockbits(Bitmap processedBitmap, Bitmap processedBitmapK)
        //{
        //    byte[] pixels;
        //    try
        //    {
        //        BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);
        //        BitmapData bitmapDataK = processedBitmapK.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

        //        int bytesPerPixel = Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
        //        int byteCount = bitmapData.Stride * processedBitmap.Height;
        //        pixels = new byte[byteCount];
        //        IntPtr ptrFirstPixel = bitmapData.Scan0;
        //        byte[] pixelsK = new byte[byteCount];
        //        IntPtr ptrFirstPixelK = bitmapDataK.Scan0;
        //        Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
        //        Marshal.Copy(ptrFirstPixelK, pixelsK, 0, pixelsK.Length);

        //        int heightInPixels = bitmapData.Height;
        //        int widthInBytes = bitmapData.Width * bytesPerPixel;

        //        for (int y = 0; y < heightInPixels; y++)
        //        {
        //            int currentLine = y * bitmapData.Stride;
        //            // int currentLineK = y * bitmapDataK.Stride;
        //            for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
        //            {
        //                //int oldBlue = pixels[currentLine + x];
        //                //int oldGreen = pixels[currentLine + x + 1];
        //                //int oldRed = pixels[currentLine + x + 2];
        //                int oldK = pixelsK[currentLine + x + 2];

        //                // calculate new pixel value
        //                //pixels[currentLine + x] = (byte)oldBlue;
        //                //pixels[currentLine + x + 1] = (byte)oldGreen;
        //                //pixels[currentLine + x + 2] = (byte)oldRed;
        //                pixels[currentLine + x + 3] = (byte)oldK;
        //            }
        //        }

        //        // copy modified bytes back
        //        Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
        //        processedBitmap.UnlockBits(bitmapData);
        //        processedBitmapK.UnlockBits(bitmapDataK);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.WriteErrorLog("From ProcessUsingLockbits : " + ex.Message);
        //        throw ex;
        //    }
        //    return pixels;
        //}
        #endregion
    }
}