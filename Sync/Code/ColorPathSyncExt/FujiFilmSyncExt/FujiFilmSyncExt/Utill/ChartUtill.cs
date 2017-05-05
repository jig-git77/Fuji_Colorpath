using FujiFilmSyncExt.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujiFilmSyncExt.Utill
{
    public class ChartUtill
    {
        public Bitmap GenerateColorListChart(Wedge wedge)
        {
            int result = 0;
            //Picture p = default(Picture);
            //FolderItem fi = default(FolderItem);           

            int numberofpatches = 1872;
            int chartpages = 1;
            int patchsize = 27;
            int chartborder = 40;
            int chartsizeW = 0;
            int chartsizeH = 0;
            int rowsperchart = 24;
            int columsperchart = 26;
            int vgap = 2;
            int hgap = 2;
            int gapLval = 75;
            double previousL = 0;
            string chartname = "Pantone Test Chart";

            //calculate height and width of image
            int totalvgap = vgap * (columsperchart + 2);
            chartsizeW = columsperchart * patchsize + (chartborder * 2) + totalvgap;
            chartsizeH = rowsperchart * patchsize + (chartborder * 2);

            int patchid = 0;

            //create image
            for (int thispage = 1; thispage <= chartpages; thispage++)
            {

                Bitmap image = new Bitmap(chartsizeW, chartsizeH);
                Graphics graphics = System.Drawing.Graphics.FromImage(image);

                //**Temp Code **//
                //chartsizeW = 500;
                //chartsizeH = 500;
                //**Temp Code **//

                //draw main outside rectangle
                Color color = CMYK(1, 1, 1, 0);
                SolidBrush mainBrush = new SolidBrush(color);
                Rectangle mainRect = new Rectangle(0, 0, chartsizeW, chartsizeH);
                graphics.FillRectangle(mainBrush, mainRect);

                //set the attributes of image
                Color lineColor = CMYK(0, 0, 0, 0);
                Pen pen = new Pen(lineColor, 1);
                graphics.DrawRectangle(pen, 5, 5, chartsizeW - 10, chartsizeH - 10);


                //Get the patches list
                List<Patch> listPatch = new List<Patch>();
                if (wedge.listPatch != null)
                    listPatch = wedge.listPatch;


                for (int i = 0; i < rowsperchart; i++)
                {
                    for (int j = 0; j < columsperchart; j++)
                    {
                        Patch patch = listPatch[patchid];
                        if (i == 0 && patch.cie_L >= gapLval)
                        {
                            Color patchColor = CMYK(1, 1, 1, 0);
                            SolidBrush patchBrush = new SolidBrush(color);
                            int patchX = chartborder + (i * patchsize) + ((i + 1) * vgap) - vgap;
                            int patchY = chartborder + (j * patchsize);
                            Rectangle patchRect = new Rectangle(patchX, patchY, vgap, patchsize);
                            graphics.FillRectangle(patchBrush, patchRect);

                            Color patchMaskColor = CMYK(0, 0, 0, 0);
                            Pen patchPen = new Pen(patchMaskColor, 1);
                            //Rectangle r = new Rectangle(5, 5, chartsizeW - 10, chartsizeH - 10);
                            graphics.DrawRectangle(patchPen, patchX, patchY, vgap, patchsize);
                        }

                        if (i == columsperchart - 1 && patch.cie_L >= gapLval)
                        {
                            Color patchColor = CMYK(1, 1, 1, 0);
                            SolidBrush patchBrush = new SolidBrush(patchColor);
                            int patchX = chartborder + (i * patchsize) + ((i + 1) * vgap) + patchsize;
                            int patchY = chartborder + (j * patchsize);
                            Rectangle patchRect = new Rectangle(patchX, patchY, vgap, patchsize);
                            graphics.FillRectangle(patchBrush, patchRect);

                            Color patchMaskColor = CMYK(0, 0, 0, 0);
                            Pen patchPen = new Pen(patchMaskColor, 1);
                            //Rectangle r = new Rectangle(5, 5, chartsizeW - 10, chartsizeH - 10);
                            graphics.DrawRectangle(patchPen, patchX, patchY, vgap, patchsize);
                        }

                        decimal colorC = Convert.ToDecimal(1 - (patch.cmyk_C / 100));
                        decimal colorM = Convert.ToDecimal(1 - (patch.cmyk_M / 100));
                        decimal colorY = Convert.ToDecimal(1 - (patch.cmyk_Y / 100));
                        Color cmyColor = CMYK(colorC, colorM, colorY, 0);
                        SolidBrush cmyBrush = new SolidBrush(cmyColor);
                        int cmyX = chartborder + (i * patchsize) + ((i + 1) * vgap);
                        int cmyY = chartborder + (j * patchsize);
                        Rectangle cmyRect = new Rectangle(cmyX, cmyY, patchsize, patchsize);
                        graphics.FillRectangle(cmyBrush, cmyRect);

                        Color cmyMaskColor = CMYK(colorC, colorM, colorY, 0);
                        Pen cmyMaskPen = new Pen(cmyMaskColor, 1);
                        graphics.DrawRectangle(cmyMaskPen, cmyX, cmyY, patchsize, patchsize);

                        patchid = patchid + 1;
                        if (i > 0)
                        {
                            if (previousL >= gapLval && patch.cie_L >= gapLval)
                            {
                                Color prevColor = CMYK(1, 1, 1, 0);
                                SolidBrush prevBrush = new SolidBrush(prevColor);
                                int prevX = chartborder + (i * patchsize) + ((i + 1) * vgap) - vgap;
                                int prevY = chartborder + (j * patchsize);
                                Rectangle prevRect = new Rectangle(prevX, prevY, vgap, patchsize);
                                graphics.FillRectangle(prevBrush, prevRect);

                                Color prevMaskColor = CMYK(0, 0, 0, 0);
                                Pen prevMaskPen = new Pen(prevMaskColor, 1);
                                graphics.DrawRectangle(prevMaskPen, prevX, prevY, vgap, patchsize);
                            }
                        }

                        previousL = patch.cie_L;
                    }
                }

                string chartinfo = "Profile Name: ";

                string pagedchartname = chartname + " page# " + Convert.ToString(thispage) + " of " + Convert.ToString(chartpages);

                //Color txtMaskColor = CMYK(1, 0, 1, 0);
                //SolidBrush txtMaskBrush = new SolidBrush(txtMaskColor);
                //Font font = new Font("Arial", 14);
                ////chartsizeW, chartsizeH
                //// Measure string.
                //SizeF stringSize = new SizeF();
                //stringSize = graphics.MeasureString(pagedchartname, font);

                //float txtX = (image.Width - stringSize.Width) / 2;
                //float txtY = stringSize.Width / 2;
                //graphics.DrawString("", font, txtMaskBrush, new PointF(0, 0));
                //int txtW = pagedchartname.Length) / 2;
                //graphics.DrawString("",)

                return image;
            }
            return null;
        }

        public Color CMYK(decimal c, decimal m, decimal y, decimal k)
        {
            decimal[] cmyk = new decimal[4];
            cmyk[0] = c;
            cmyk[1] = m;
            cmyk[2] = y;
            cmyk[3] = k;

            var r = (int)(255 * (1 - cmyk[0]) * (1 - cmyk[3]));
            var g = (int)(255 * (1 - cmyk[1]) * (1 - cmyk[3]));
            var b = (int)(255 * (1 - cmyk[2]) * (1 - cmyk[3]));

            var hex = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");

            System.Windows.Media.Color colorConvert = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hex);
            Color color = Color.FromArgb(colorConvert.A, colorConvert.R, colorConvert.G, colorConvert.B);
            return color;
        }
    }
}
