using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Common
{
    public class ColorMath
    {
        //Status T Density Red 
        private static double[] _RefStatusT_R = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0,0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 
                                                  0.001, 0.06,  0.4498, 29.9916, 100.0, 84.918, 54.9541, 25.0035, 10.0, 5.0003, 1.4997, 0.5, 0.2999, 0.15, 0.05, 
                                                  0.01, 0.001, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };


        //Status T Density Green
        private static double[] _RefStatusT_G = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.001, 1.0, 5.0003, 27.9898, 68.0769, 92.045, 100.0, 87.9023, 66.0693, 41.9759, 21.9786, 8.995, 2.5003, 0.6998, 0.0899, 0.01, 0.001, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };



        //Status T Density Blue
        private static double[] _RefStatusT_B = { 0.0, 0.01, 0.02, 0.10, 0.30, 1.50, 6.0, 16.98, 39.99, 59.98, 82.04, 93.97, 100.0, 97.05, 84.92, 65.01, 39.99, 17.99, 5.0, 0.20, 0.04, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };


        //Status T Density Blue
        private static double[] _RefVisual = { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.01, 0.02, 0.08, 0.28, 0.65, 1.23, 2.22, 3.82, 6.58, 10.99, 18.88, 32.58, 50.35, 66.83, 80.35, 90.57, 97.50, 100.00, 97.50, 90.36, 79.80, 67.14, 53.83, 39.17, 27.10, 17.30, 10.30, 5.61, 3.09, 1.54, 0.80, 0.42, 0.22, 0.11, 0.05, 0.03, 0.01, 0.01, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };




        //Status E Density Red 
        private static double[] _RefStatusE_R = { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.01, 0.06, 0.45, 29.99, 100.00, 84.92, 54.95, 25.00, 10.00, 5.00, 1.50, 0.50, 0.30, 0.15, 0.05, 0.01, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };

        //Status E Density Green
        private static double[] _RefStatusE_G = { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.01, 1.00, 5.00, 27.99, 68.08, 92.04, 100.00, 87.90, 66.07, 41.98, 21.98, 8.99, 2.50, 0.70, 0.09, 0.01, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };


        //Status E Density Blue
        private static double[] _RefStatusE_B = { 0.00, 0.00, 0.00, 0.01, 0.27, 2.70, 13.00, 29.99, 59.98, 82.04, 100.00, 90.99, 76.03, 46.99, 17.99, 6.00, 0.80, 0.05, 0.01, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };


        private static double _RefStatusT_R_Sum = 312.8889;
        private static double _RefStatusT_G_Sum = 524.3353;
        private static double _RefStatusT_B_Sum = 711.097538;
        private static double _RefVisual_Sum = 1057.35;


        private static double _RefStatusE_R_Sum = 312.92;
        private static double _RefStatusE_G_Sum = 524.34;
        private static double _RefStatusE_B_Sum = 526.84;


        //CIE 1931 2° Standard Observer Color Matching Functions (340nm to 830 nm)
        private static double[] _x = { 0.000, 0.000, 0.000, 0.001, 0.003, 0.012, 0.06, 0.234, 0.775, 1.61, 2.453, 2.777, 2.5, 1.717, 0.861, 0.283, 0.04, 0.088, 0.593, 1.59, 2.799, 4.207, 5.657, 7.132, 8.54, 9.255, 9.835, 9.469, 8.009, 5.926, 4.171, 2.609, 1.541, 0.855, 0.434, 0.194, 0.097, 0.05, 0.022, 0.012, 0.006, 0.002, 0.001, 0.001, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };

        private static double[] _y = { 0, 0, 0, 0, 0, 0, 0.002, 0.006, 0.023, 0.066, 0.162, 0.313, 0.514, 0.798, 1.239, 1.839, 2.948, 4.632, 6.587, 8.308, 9.197, 9.65, 9.471, 8.902, 8.112, 6.829, 5.838, 4.753, 3.573, 2.443, 1.629, 0.984, 0.57, 0.313, 0.158, 0.07, 0.035, 0.018, 0.008, 0.004, 0.002, 0.001, 0, 0, 0, 0, 0, 0, 0, 0 };

        private static double[] _z = { 0, 0, 0.001, 0.005, 0.013, 0.057, 0.285, 1.113, 3.723, 7.862, 12.309, 14.647, 14.346, 11.299, 7.309, 4.128, 2.466, 1.447, 0.736, 0.401, 0.196, 0.085, 0.037, 0.02, 0.015, 0.01, 0.007, 0.004, 0.002, 0.001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        /// <summary>
        /// Perform Sum Product of the given two arrays
        /// </summary>
        /// <param name="SampleSpectrum">Sample Data Spectrum Values</param>
        /// <param name="ReferenceSpectrum">Reference StatusT Values</param>
        /// <returns>Returns SumProduct Result as Double; If error return -1 </returns>
        /// <remarks></remarks>
        private static double SumProduct(double[] SampleSpectrum, double[] ReferenceSpectrum)
        {
            double Result = 0;
            try
            {

                for (int i = 0; i < SampleSpectrum.Length; i++)
                {
                    Result += (SampleSpectrum[i] * ReferenceSpectrum[i]);
                }

            }
            catch (Exception)
            {
                Result = -1; //a number to check if error has occured
            }

            return Result;
        }


        ///// <summary>
        ///// Perform Sum Product of the given three arrays
        ///// </summary>
        ///// <param name="SampleSpectrum">Sample Data Spectrum Values</param>
        ///// <param name="Observer">Reference Observer Values (2° in this case)</param>
        ///// <param name="Illuminant">Reference Illuminant Values (D50 in this case)</param>
        ///// <returns>Returns SumProduct Result as Double; If error return -1 </returns>
        ///// <remarks></remarks>
        //private static double SumProduct(double[] SampleSpectrum, double[] Observer, double[] Illuminant)
        //{
        //    double Result = 0;
        //    try
        //    {
        //        for (int i = 0; i < SampleSpectrum.Length; i++)
        //        {
        //            Result += (SampleSpectrum[i] * Observer[i] * Illuminant[i]);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Result = -1; //a number to check if error has occured
        //    }
        //    return Result;
        //}


        /// <summary>
        /// Calculate Density From Spectral Data
        /// </summary>
        /// <param name="SampleSpectrum">Sample Spectral Data</param>
        /// <returns>Density V, C, M, Y Filter</returns>
        /// <remarks>Spectral Density Structure; if error returns null</remarks>
        public static SpectralDensity CalcStatusTDensity(double[] SampleSpectrum)
        {
            try
            {

                double DensRed = 0;
                double DensGreen = 0;
                double DensBlue = 0;
                double DensVisual = 0;

                DensRed = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefStatusT_R) / _RefStatusT_R_Sum);
                DensGreen = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefStatusT_G) / _RefStatusT_G_Sum);
                DensBlue = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefStatusT_B) / _RefStatusT_B_Sum);
                DensVisual = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefVisual) / _RefVisual_Sum);
                return new SpectralDensity(DensRed, DensGreen, DensBlue, DensVisual);

            }
            catch (Exception)
            {
                return null; //Error

            }

        }

        /// <summary>
        /// Calculate Density From Spectral Data
        /// </summary>
        /// <param name="SampleSpectrum">Sample Spectral Data</param>
        /// <returns>Density V, C, M, Y Filter</returns>
        /// <remarks>Spectral Density Structure; if error returns null</remarks>
        public static SpectralDensity CalcStatusEDensity(double[] SampleSpectrum)
        {
            try
            {

                double DensRed = 0;
                double DensGreen = 0;
                double DensBlue = 0;
                double DensVisual = 0;

                DensRed = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefStatusE_R) / _RefStatusE_R_Sum);
                DensGreen = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefStatusE_G) / _RefStatusE_G_Sum);
                DensBlue = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefStatusE_B) / _RefStatusE_B_Sum);
                DensVisual = -1.0 * Math.Log10(SumProduct(SampleSpectrum, _RefVisual) / _RefVisual_Sum);
                return new SpectralDensity(DensRed, DensGreen, DensBlue, DensVisual);

            }
            catch (Exception)
            {
                return null; //Error

            }

        }




        /// <summary>
        /// Calculate CIE XYZ from Spectral data
        /// </summary>
        /// <param name="SampleSpectrum">Sample Spectral Data</param>
        /// <returns>CIE XYZ Structure; in case of an error returns null</returns>
        public static CIEXYZ CalcCIEXYZ(double[] SampleSpectrum)
        {
            try
            {
                double X = 0;
                double Y = 0;
                double Z = 0;

                X = SumProduct(SampleSpectrum, _x);
                Y = SumProduct(SampleSpectrum, _y);
                Z = SumProduct(SampleSpectrum, _z);

                X = Math.Round(X, 4);
                Y = Math.Round(Y, 4);
                Z = Math.Round(Z, 4);
                return new CIEXYZ(X, Y, Z);
            }
            catch (Exception)
            {
                return null;
            }

        }


        /// XYZ to L*a*b* transformation function.
        private static double Fxyz(double t)
        {
            return ((t > 0.008856) ? Math.Pow(t, (1.0 / 3.0)) : (7.787 * t + 16.0 / 116.0));
        }


        /// <summary>
        /// Converts CIEXYZ to CIELab structure.
        /// </summary>
        public static CIELab XYZtoLab(double x, double y, double z)
        {
            CIELab lab = new CIELab(0, 0, 0);
            double d50X = 96.422;
            double d50Y = 100.00;
            double d50Z = 82.521;

            lab.L = 116.0 * (Fxyz(y / d50Y)) - 16;
            lab.A = 500.0 * (Fxyz(x / d50X) - Fxyz(y / d50Y));
            lab.B = 200.0 * (Fxyz(y / d50Y) - Fxyz(z / d50Z));

            return lab;
        }

        /// <summary>
        /// Converts CIEXYZ to CIELab structure.
        /// </summary>
        public static CIELab XYZtoLab(CIEXYZ xyz)
        {
            return XYZtoLab(xyz.X, xyz.Y, xyz.Z);
        }


        /// <summary>
        /// Convert Visual Density to CIE L*
        /// </summary>
        /// <returns>CIELAB_L* for gray patches</returns>
        public static double VisualDensToGrayL(double Dens)
        {
            Double CIEXYX_L = 0;
            Double CIEXYZ_Y = 0;

            CIEXYZ_Y = Math.Pow(0.1, Dens);

            CIEXYX_L = 116.0 * (Fxyz(CIEXYZ_Y)) - 16;

            return CIEXYX_L;

        }



        /// <summary>
        /// Calculate Hue Angle from CIELAB (a*, b* Values)
        /// </summary>
        /// <param name="CIE_a">CIELAB a* value</param>
        /// <param name="CIE_b">CIELAB b* value</param>
        /// <returns>Hue Angle from CIELAB</returns>
        /// <remarks>Reference: www.easyrgb.com Author:cshah@fujifilmgs.com</remarks>
        public static double CalcCIELAB2Hue(double CIE_a, double CIE_b)
        {

            double var_bias = 0;
            double HueAngle;

            try
            {

                if ((CIE_a >= 0 & CIE_b == 0))
                {
                    return 0;
                }
                else if ((CIE_a < 0 & CIE_b == 0))
                {
                    return 180;
                }
                else if ((CIE_a == 0 & CIE_b > 0))
                {
                    return 90;
                }
                else if ((CIE_a == 0 & CIE_b < 0))
                {
                    return 270;
                }

                if ((CIE_a > 0 & CIE_b > 0))
                {
                    var_bias = 0;
                }
                else if ((CIE_a < 0))
                {
                    var_bias = 180;
                }
                else if ((CIE_a > 0 & CIE_b < 0))
                {
                    var_bias = 360;
                }

                HueAngle = ((Math.Atan(CIE_b / CIE_a) * (180.0 / Math.PI)) + var_bias);

                return HueAngle;
            }

            catch (Exception)
            {
                HueAngle = 361;
                //Angle must be between 0 to 360, 361 indicates an error  
                return HueAngle;
            }

        }


        /// <summary>
        /// Calculate Chroma from CIEa* and CIEb*
        /// </summary>
        /// <param name="CIE_a"></param>
        /// <param name="CIE_b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double CalcChroma(double CIE_a, double CIE_b)
        {
            double Chroma;
            try
            {
                Chroma = Math.Sqrt((Math.Pow(CIE_a, 2)) + (Math.Pow(CIE_b, 2)));
                return Chroma;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        /// <summary>
        /// Calculate Hue Difference deltaH
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double CalcDeltaH(double Target_L, double Target_a, double Target_b, double Sample_L, double Sample_a, double Sample_b)
        {

            double xDE;
            double DeltaH;
            try
            {
                xDE = Math.Sqrt((Math.Pow(Sample_a, 2)) + (Math.Pow(Sample_b, 2))) - Math.Sqrt((Math.Pow(Target_a, 2)) + (Math.Pow(Target_b, 2)));
                DeltaH = Math.Sqrt(Math.Pow((Sample_a - Target_a), 2) + Math.Pow((Sample_b - Target_b), 2) - (Math.Pow(xDE, 2)));
                if (double.IsNaN(DeltaH) == true)
                {
                    return 0;
                }
                else
                {
                    return DeltaH;
                }
            }

            catch (Exception)
            {
                return -1;
            }

        }



        /// <summary>
        /// The color difference, or ΔE, between reference and lab color using CIE 1976
        /// </summary>
        /// <returns>Returns CIELAB ΔE 1976</returns>
        /// <remarks></remarks>
        public static double CIELAB_dE(double Target_L, double Target_a, double Target_b, double Sample_L, double Sample_a, double Sample_b)
        {
            double deltaE;
            try
            {
                deltaE = Math.Sqrt(Math.Pow((Target_L - Sample_L), 2) + Math.Pow((Target_a - Sample_a), 2) + Math.Pow((Target_b - Sample_b), 2));
                return deltaE;
            }
            catch (Exception)
            {
                return -1;
            }

        }


        /// <summary>
        /// Calculate ΔE 94 (CIE 1994)
        /// </summary>
        /// <returns>ΔE as double, in case of an error returns -1</returns>
        /// <remarks></remarks>
        public static double CIELAB_dE94(double Target_L, double Target_a, double Target_b, double Sample_L, double Sample_a, double Sample_b)
        {

            try
            {

                double ΔE94;
                double xC1;
                double xC2;
                double xDL;
                double xDC;
                double xDE;
                double xSC;
                double xSH;
                double xDH;

                xC1 = Math.Sqrt((Math.Pow(Target_a, 2)) + (Math.Pow(Target_b, 2)));
                xC2 = Math.Sqrt((Math.Pow(Sample_a, 2)) + (Math.Pow(Sample_b, 2)));

                xDL = Math.Abs((Target_L - Sample_L));
                xDC = Math.Round(Math.Abs(xC2 - xC1), 2);

                xDE = Math.Sqrt(((Target_L - Sample_L) * (Target_L - Sample_L)) + ((Target_a - Sample_a) * (Target_a - Sample_a)) + ((Target_b - Sample_b) * (Target_b - Sample_b)));


                xDH = Math.Round(Math.Sqrt(Math.Pow((Target_a - Sample_a), 2) + Math.Pow((Target_b - Sample_b), 2) - Math.Pow((xDC), 2)));
                if (double.IsNaN(xDH) == true)
                {
                    xDH = 0;


                }

                xSC = 1 + (0.045 * xC1);
                xSH = 1 + (0.015 * xC1);


                xDC /= (1 * xSC);
                xDH /= (1 * xSH);

                ΔE94 = Math.Sqrt(Math.Pow(xDL, 2) + Math.Pow(xDC, 2) + Math.Pow(xDH, 2));

                if (double.IsNaN(ΔE94) == true)
                {
                    return 0;
                }
                else
                {
                    return ΔE94;
                }
            }

            catch (Exception)
            {
                return -1;
            }

        }



        /// <summary>
        /// Calculate d
        /// </summary>
        /// <returns></returns>
        /// <remarks>Reference: www.easyrgb.com </remarks>
        public static double CIELAB_dECMC11(double Target_L, double Target_a, double Target_b, double Sample_L, double Sample_a, double Sample_b)
        {

            double ΔECMC;
            double xC1;
            double xC2;
            double xff;
            double xH1;
            double xTT;
            double xSL;
            double xSC;
            double xSH;
            double xDH;

            try
            {
                xC1 = Math.Sqrt((Math.Pow(Target_a, 2)) + (Math.Pow(Target_b, 2)));
                xC2 = Math.Sqrt((Math.Pow(Sample_a, 2)) + (Math.Pow(Sample_b, 2)));
                xff = Math.Sqrt((Math.Pow(xC1, 4)) / ((Math.Pow(xC1, 4)) + 1900));
                xH1 = ColorMath.CalcCIELAB2Hue(Target_a, Target_b);

                if ((xH1 < 164 | xH1 > 345))
                {
                    xTT = 0.36 + Math.Abs(0.4 * Math.Cos(DegreeToRadian(35 + xH1)));
                }
                else
                {
                    xTT = 0.56 + Math.Abs(0.2 * Math.Cos(DegreeToRadian(168 + xH1)));
                }

                if ((Target_L < 16))
                {
                    xSL = 0.511;
                }
                else
                {
                    xSL = (0.040975 * Target_L) / (1 + (0.01765 * Target_L));
                }

                xSC = ((0.0638 * xC1) / (1 + (0.0131 * xC1))) + 0.638;
                xSH = ((xff * xTT) + 1 - xff) * xSC;
                xDH = Math.Sqrt(Math.Pow((Sample_a - Target_a), 2) + Math.Pow((Sample_b - Target_b), 2) - Math.Pow((xC2 - xC1), 2));
                xSL = (Sample_L - Target_L) / (1 * xSL);
                xSC = (xC2 - xC1) / (1 * xSC);
                xSH = xDH / xSH;
                ΔECMC = Math.Sqrt(Math.Pow(xSL, 2) + Math.Pow(xSC, 2) + Math.Pow(xSH, 2));

                return ΔECMC;
            }

            catch (Exception)
            {
                return -1;
            }

        }



        public static double CIELAB_dECMC21(double Target_L, double Target_a, double Target_b, double Sample_L, double Sample_a, double Sample_b)
        {

            double ΔECMC;
            double xC1;
            double xC2;
            double xff;
            double xH1;
            double xTT;
            double xSL;
            double xSC;
            double xSH;
            double xDH;

            try
            {
                xC1 = Math.Sqrt((Math.Pow(Target_a, 2)) + (Math.Pow(Target_b, 2)));
                xC2 = Math.Sqrt((Math.Pow(Sample_a, 2)) + (Math.Pow(Sample_b, 2)));
                xff = Math.Sqrt((Math.Pow(xC1, 4)) / ((Math.Pow(xC1, 4)) + 1900));
                xH1 = ColorMath.CalcCIELAB2Hue(Target_a, Target_b);

                if ((xH1 < 164 | xH1 > 345))
                {
                    xTT = 0.36 + Math.Abs(0.4 * Math.Cos(DegreeToRadian(35 + xH1)));
                }
                else
                {
                    xTT = 0.56 + Math.Abs(0.2 * Math.Cos(DegreeToRadian(168 + xH1)));
                }

                if ((Target_L < 16))
                {
                    xSL = 0.511;
                }
                else
                {
                    xSL = (0.040975 * Target_L) / (1 + (0.01765 * Target_L));
                }

                xSC = ((0.0638 * xC1) / (1 + (0.0131 * xC1))) + 0.638;
                xSH = ((xff * xTT) + 1 - xff) * xSC;
                xDH = Math.Sqrt(Math.Pow((Sample_a - Target_a), 2) + Math.Pow((Sample_b - Target_b), 2) - Math.Pow((xC2 - xC1), 2));
                xSL = (Sample_L - Target_L) / (2 * xSL);
                xSC = (xC2 - xC1) / (1 * xSC);
                xSH = xDH / xSH;
                ΔECMC = Math.Sqrt(Math.Pow(xSL, 2) + Math.Pow(xSC, 2) + Math.Pow(xSH, 2));

                if (double.IsNaN(ΔECMC) == true)
                {
                    return 0;
                }
                else
                {
                    return ΔECMC;
                }
            }


            catch (Exception)
            {
                return -1;
            }

        }


        /// <summary>
        /// Calculate ΔE 2000 
        /// Reference: http://www.easyrgb.com/index.php?X=DELT
        /// </summary>
        /// <returns>ΔE 2000 Double</returns>
        /// <remarks></remarks>
        public static double CIELAB_dE2000(double Target_L, double Target_a, double Target_b, double Sample_L, double Sample_a, double Sample_b)
        {

            double xC1;
            double xC2;
            double xCX;
            double xGX;
            double xNN;
            double xH1;
            double xH2;
            double xDL;
            double xDC;
            double xDH;
            double xLX;
            double xCY;
            double xTX;
            double ΔE00;
            double xHX;
            double xPH;
            double xRC;
            double xSL;
            double xSC;
            double xSH;
            double xRT;

            try
            {
                xC1 = Math.Sqrt(Target_a * Target_a + Target_b * Target_b);
                xC2 = Math.Sqrt(Sample_a * Sample_a + Sample_b * Sample_b);
                xCX = (xC1 + xC2) / 2;
                xGX = 0.5 * (1 - Math.Sqrt((Math.Pow(xCX, 7)) / ((Math.Pow(xCX, 7)) + (Math.Pow(25, 7)))));
                xNN = (1 + xGX) * Target_a;
                xC1 = Math.Sqrt(xNN * xNN + Target_b * Target_b);
                xH1 = CalcCIELAB2Hue(xNN, Target_b);
                xNN = (1 + xGX) * Sample_a;
                xC2 = Math.Sqrt(xNN * xNN + Sample_b * Sample_b);
                xH2 = CalcCIELAB2Hue(xNN, Sample_b);
                xDL = Math.Abs(Sample_L - Target_L);
                xDC = Math.Abs(xC2 - xC1);
                if (((xC1 * xC2) == 0))
                {
                    xDH = 0;
                }
                else
                {
                    xNN = Math.Round(xH2 - xH1, 12);
                    if ((Math.Abs(xNN) <= 180))
                    {
                        xDH = xH2 - xH1;
                    }
                    else
                    {
                        if ((xNN > 180))
                        {
                            xDH = xH2 - xH1 - 360;
                        }
                        else
                        {
                            xDH = xH2 - xH1 + 360;
                        }
                    }
                }
                xDH = 2 * Math.Sqrt(xC1 * xC2) * Math.Sin(DegreeToRadian(xDH / 2));
                xLX = (Target_L + Sample_L) / 2;
                xCY = (xC1 + xC2) / 2;

                if (((xC1 * xC2) == 0))
                {
                    xHX = xH1 + xH2;
                }
                else
                {
                    xNN = Math.Abs(Math.Round(xH1 - xH2, 12));
                    if ((xNN > 180))
                    {
                        if (((xH2 + xH1) < 360))
                        {
                            xHX = xH1 + xH2 + 360;
                        }
                        else
                        {
                            xHX = xH1 + xH2 - 360;
                        }
                    }
                    else
                    {
                        xHX = xH1 + xH2;
                    }
                    xHX /= 2;
                }

                xTX = 1 - 0.17 * Math.Cos(DegreeToRadian(xHX - 30)) + 0.24 * Math.Cos(DegreeToRadian(2 * xHX)) + 0.32 * Math.Cos(DegreeToRadian(3 * xHX + 6)) - 0.2 * Math.Cos(DegreeToRadian(4 * xHX - 63));
                xPH = 30 * Math.Exp(-((xHX - 275) / 25) * ((xHX - 275) / 25));
                xRC = 2 * Math.Sqrt((Math.Pow(xCY, 7)) / ((Math.Pow(xCY, 7)) + (Math.Pow(25, 7))));
                xSL = 1 + ((0.015 * ((xLX - 50) * (xLX - 50))) / Math.Sqrt(20 + ((xLX - 50) * (xLX - 50))));
                xSC = 1 + 0.045 * xCY;
                xSH = 1 + 0.015 * xCY * xTX;
                xRT = -Math.Sin(DegreeToRadian(2 * xPH)) * xRC;
                xDL = xDL / (1 * xSL);
                xDC = xDC / (1 * xSC);
                xDH = xDH / (1 * xSH);

                ΔE00 = Math.Sqrt(Math.Pow(xDL, 2) + Math.Pow(xDC, 2) + Math.Pow(xDH, 2) + xRT * xDC * xDH);

                if (double.IsNaN(ΔE00) == true)
                {
                    return 0;
                }
                else
                {
                    return ΔE00;
                }
            }


            catch (Exception)
            {
                return -1;
            }
        }


        /// <summary>
        /// Converts Degree to Radians
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double DegreeToRadian(double degree)
        {
            return (Math.PI / 180) * degree;
        }


        //Convert Radian To Degree
        public static double RadianToDegree(double radian)
        {
            return (180 / Math.PI) * radian;
        }


        /// <summary>
        /// Converts CIELab to CIEXYZ structure.
        /// </summary>
        public static CIEXYZ LabtoXYZ(double l, double a, double b)
        {
            CIEXYZ xyz = new CIEXYZ(0, 0, 0);

            double theta = 6.0 / 29.0;

            //Observer= 2°, Illuminant= D50
            double d50X = 96.422;
            double d50Y = 100;
            double d50Z = 82.521;

            double fy = (l + 16) / 116.0;
            double fx = fy + (a / 500.0);
            double fz = fy - (b / 200.0);

            xyz.X = (fx > theta) ? d50X * (fx * fx * fx) : (fx - 16.0 / 116.0) * 3 * (theta * theta) * d50X;
            xyz.Y = (fy > theta) ? d50Y * (fy * fy * fy) : (fy - 16.0 / 116.0) * 3 * (theta * theta) * d50Y;
            xyz.Z = (fz > theta) ? d50Z * (fz * fz * fz) : (fz - 16.0 / 116.0) * 3 * (theta * theta) * d50Z;

            return xyz;
        }

        /// <summary>
        /// Converts CIELab to CIEXYZ structure.
        /// </summary>
        public static CIEXYZ LabtoXYZ(CIELab lab)
        {
            return LabtoXYZ(lab.L, lab.A, lab.B);
        }

        public static CIELab SubstrateRelCalc(double currentpaper_X, double currentpaper_Y, double currentpaper_Z, double newpaper_X, double newpaper_Y, double newpaper_Z, double patch_L, double patch_A, double patch_B)
        {
            CIELab lab = new CIELab(0, 0, 0);
            CIEXYZ xyz = new CIEXYZ(0, 0, 0);
          
            //convert current patch's LAB to XYZ
            xyz = LabtoXYZ(patch_L, patch_A, patch_B);

            ///set the CMYK 100,100,100,100 to zero for now
            var cmyk100_X = 0;
            var cmyk100_Y = 0;
            var cmyk100_Z = 0;

            var X_C1 = (newpaper_X -currentpaper_X) / (currentpaper_X -  cmyk100_X);
            var Y_C1 = (newpaper_Y - currentpaper_Y) / (currentpaper_Y - cmyk100_Y);
            var Z_C1 = (newpaper_Z - currentpaper_Z) / (currentpaper_Z - cmyk100_Z);

            var X_C2 = X_C1 * cmyk100_X;
            var Y_C2 = Y_C1 * cmyk100_Y;
            var Z_C2 = Z_C1 * cmyk100_Z;

            var NewX = (xyz.X * (1 + X_C1)) - X_C2;
            var NewY = (xyz.Y * (1 + Y_C1)) - Y_C2;
            var NewZ = (xyz.Z * (1 + Z_C1)) - Z_C2;

            //Convert NewXYZ to NewLAB
            lab = XYZtoLab(NewX, NewY, NewZ);

            return lab;
        }

    }
}
