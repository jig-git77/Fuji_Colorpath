#region Using
using System;
using System.Data;
using System.Data.SqlClient;
using Taskero.DataAccess;
#endregion

namespace Taskero.CPSync
{
    public class MeasuredPatch
    {


        public enum Spectrum : int
        {
            SPECTRAL_340 = 0,
            SPECTRAL_350 = 1,
            SPECTRAL_360 = 2,
            SPECTRAL_370 = 3,
            SPECTRAL_380 = 4,
            SPECTRAL_390 = 5,
            SPECTRAL_400 = 6,
            SPECTRAL_410 = 7,
            SPECTRAL_420 = 8,
            SPECTRAL_430 = 9,
            SPECTRAL_440 = 10,
            SPECTRAL_450 = 11,
            SPECTRAL_460 = 12,
            SPECTRAL_470 = 13,
            SPECTRAL_480 = 14,
            SPECTRAL_490 = 15,
            SPECTRAL_500 = 16,
            SPECTRAL_510 = 17,
            SPECTRAL_520 = 18,
            SPECTRAL_530 = 19,
            SPECTRAL_540 = 20,
            SPECTRAL_550 = 21,
            SPECTRAL_560 = 22,
            SPECTRAL_570 = 23,
            SPECTRAL_580 = 24,
            SPECTRAL_590 = 25,
            SPECTRAL_600 = 26,
            SPECTRAL_610 = 27,
            SPECTRAL_620 = 28,
            SPECTRAL_630 = 29,
            SPECTRAL_640 = 30,
            SPECTRAL_650 = 31,
            SPECTRAL_660 = 32,
            SPECTRAL_670 = 33,
            SPECTRAL_680 = 34,
            SPECTRAL_690 = 35,
            SPECTRAL_700 = 36,
            SPECTRAL_710 = 37,
            SPECTRAL_720 = 38,
            SPECTRAL_730 = 39,
            SPECTRAL_740 = 40,
            SPECTRAL_750 = 41,
            SPECTRAL_760 = 42,
            SPECTRAL_770 = 43,
            SPECTRAL_780 = 44,
            SPECTRAL_790 = 45,
            SPECTRAL_800 = 46,
            SPECTRAL_810 = 47,
            SPECTRAL_820 = 48,
            SPECTRAL_830 = 49

        }

        public enum EnumPatchType
        {
            Cyan = 1,
            Magenta = 2,
            Yellow = 3,
            Black = 4,
            Paper = 8,
            Red = 5,
            Green = 6,
            Blue = 7,
            Triplet = 9,
            Gray = 10,
            Other = 11
        }


        private int Sample_ID;
        public int SampleID
        {
            set { Sample_ID = value; }
        }

        private string _SampleName;
        public string SampleName
        {
            get { return _SampleName; }
            set { _SampleName = value; }
        }


        //the measurmentid this patch belongs for db insert
        private int _MeasurementID = 0;
        public int MeasurementID
        {
            get { return _MeasurementID; }
            set { _MeasurementID = value; }
        }

        private SpectralDensity _Density;
        public SpectralDensity Density
        {
            get { return _Density; }
            set { _Density = value; }
        }

        //type of patch 
        private EnumPatchType _PatchType;
        public EnumPatchType PatchType
        {
            get { return _PatchType; }
            set { _PatchType = value; }
        }

        private CIEXYZ _XYZ;
        public CIEXYZ XYZ
        {
            get { return _XYZ; }
            set { _XYZ = value; }
        }


        private CIELab _LAB;
        public CIELab LAB
        {
            get { return _LAB; }
            set { _LAB = value; }
        }


        /// <summary>
        /// Array to hold Spectral Data from 340 to 830 nm
        /// </summary>
        /// <remarks></remarks>
        double[] _patchSpectrum = new double[50];

        public void SetPatchSpectrum(int index, double value)
        {
            _patchSpectrum[index] = value;
        }

        public double[] GetPatchSpectrum()
        {
            return _patchSpectrum;
        }


        //Percentage of Cyan
        private decimal _CMYK_C;
        public decimal CMYK_C
        {
            get { return _CMYK_C; }
            set { _CMYK_C = value; }
        }


        //Percentage of Magenta
        private decimal _CMYK_M;
        public decimal CMYK_M
        {
            get { return _CMYK_M; }
            set { _CMYK_M = value; }
        }

        //Percentage of Yellow
        private decimal _CMYK_Y;
        public decimal CMYK_Y
        {
            get { return _CMYK_Y; }
            set { _CMYK_Y = value; }
        }

        //Percentage of Key (Black)
        private decimal _CMYK_K;
        public decimal CMYK_K
        {
            get { return _CMYK_K; }
            set { _CMYK_K = value; } //SetPatchType();         
        }


        /// <summary>
        /// Spectrum Wavelegnth 340 nm
        /// </summary>
        private double _Spectral340 = 0;
        public double Spectral340
        {
            get { return _Spectral340; }
            set { _Spectral340 = value; }
        }

        /// <summary>
        /// Spectrum Wavelegnth 350 nm
        /// </summary>
        private double _Spectral350 = 0;
        public double Spectral350
        {
            get { return _Spectral350; }
            set { _Spectral350 = value; }
        }

        /// <summary>
        /// Spectrum Wavelegnth 360 nm
        /// </summary>
        private double _Spectral360 = 0;
        public double Spectral360
        {
            get { return _Spectral360; }
            set { _Spectral360 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 370 nm
        /// </summary>
        private double _Spectral370 = 0;
        public double Spectral370
        {
            get { return _Spectral370; }
            set { _Spectral370 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 380 nm
        /// </summary>
        private double _Spectral380 = 0;
        public double Spectral380
        {
            get { return _Spectral380; }
            set { _Spectral380 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 390 nm
        /// </summary>
        private double _Spectral390 = 0;
        public double Spectral390
        {
            get { return _Spectral390; }
            set { _Spectral390 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 400 nm
        /// </summary>
        private double _Spectral400 = 0;
        public double Spectral400
        {
            get { return _Spectral400; }
            set { _Spectral400 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 410 nm
        /// </summary>
        private double _Spectral410 = 0;
        public double Spectral410
        {
            get { return _Spectral410; }
            set { _Spectral410 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 420 nm
        /// </summary>
        private double _Spectral420 = 0;
        public double Spectral420
        {
            get { return _Spectral420; }
            set { _Spectral420 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 430 nm
        /// </summary>
        private double _Spectral430 = 0;
        public double Spectral430
        {
            get { return _Spectral430; }
            set { _Spectral430 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 440 nm
        /// </summary>
        private double _Spectral440 = 0;
        public double Spectral440
        {
            get { return _Spectral440; }
            set { _Spectral440 = value; }
        }

        /// <summary>
        /// Spectrum Wavelegnth 450 nm
        /// </summary>
        private double _Spectral450 = 0;
        public double Spectral450
        {
            get { return _Spectral450; }
            set { _Spectral450 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 460 nm
        /// </summary>
        private double _Spectral460 = 0;
        public double Spectral460
        {
            get { return _Spectral460; }
            set { _Spectral460 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 470 nm
        /// </summary>
        private double _Spectral470 = 0;
        public double Spectral470
        {
            get { return _Spectral470; }
            set { _Spectral470 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 480 nm
        /// </summary>
        private double _Spectral480 = 0;
        public double Spectral480
        {
            get { return _Spectral480; }
            set { _Spectral480 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 490 nm
        /// </summary>
        private double _Spectral490 = 0;
        public double Spectral490
        {
            get { return _Spectral490; }
            set { _Spectral490 = value; }
        }


        /// <summary>
        /// Spectrum Wavelegnth 500 nm
        /// </summary>
        private double _Spectral500 = 0;
        public double Spectral500
        {
            get { return _Spectral500; }
            set { _Spectral500 = value; }
        }


        /// <summary>
        /// Spectrum  510 nm
        /// </summary>
        private double _Spectral510 = 0;
        public double Spectral510
        {
            get { return _Spectral510; }
            set { _Spectral510 = value; }
        }


        /// <summary>
        /// Spectrum  520 nm
        /// </summary>
        private double _Spectral520 = 0;
        public double Spectral520
        {
            get { return _Spectral520; }
            set { _Spectral520 = value; }
        }


        /// <summary>
        /// Spectrum  530 nm
        /// </summary>
        private double _Spectral530 = 0;
        public double Spectral530
        {
            get { return _Spectral530; }
            set { _Spectral530 = value; }
        }


        /// <summary>
        /// Spectrum  540 nm
        /// </summary>
        private double _Spectral540 = 0;
        public double Spectral540
        {
            get { return _Spectral540; }
            set { _Spectral540 = value; }
        }

        /// <summary>
        /// Spectrum  550 nm
        /// </summary>
        private double _Spectral550 = 0;
        public double Spectral550
        {
            get { return _Spectral550; }
            set { _Spectral550 = value; }
        }


        /// <summary>
        /// Spectrum  560 nm
        /// </summary>
        private double _Spectral560 = 0;
        public double Spectral560
        {
            get { return _Spectral560; }
            set { _Spectral560 = value; }
        }


        /// <summary>
        /// Spectrum  570 nm
        /// </summary>
        private double _Spectral570 = 0;
        public double Spectral570
        {
            get { return _Spectral570; }
            set { _Spectral570 = value; }
        }


        /// <summary>
        /// Spectrum  560 nm
        /// </summary>
        private double _Spectral580 = 0;
        public double Spectral580
        {
            get { return _Spectral580; }
            set { _Spectral580 = value; }
        }


        /// <summary>
        /// Spectrum  590 nm
        /// </summary>
        private double _Spectral590 = 0;
        public double Spectral590
        {
            get { return _Spectral590; }
            set { _Spectral590 = value; }
        }


        /// <summary>
        /// Spectrum  600 nm
        /// </summary>
        private double _Spectral600 = 0;
        public double Spectral600
        {
            get { return _Spectral600; }
            set { _Spectral600 = value; }
        }


        /// <summary>
        /// Spectrum  600 nm
        /// </summary>
        private double _Spectral610 = 0;
        public double Spectral610
        {
            get { return _Spectral610; }
            set { _Spectral610 = value; }
        }


        /// <summary>
        /// Spectrum  600 nm
        /// </summary>
        private double _Spectral620 = 0;
        public double Spectral620
        {
            get { return _Spectral620; }
            set { _Spectral620 = value; }
        }


        /// <summary>
        /// Spectrum  630 nm
        /// </summary>
        private double _Spectral630 = 0;
        public double Spectral630
        {
            get { return _Spectral630; }
            set { _Spectral630 = value; }
        }



        /// <summary>
        /// Spectrum  640 nm
        /// </summary>
        private double _Spectral640 = 0;
        public double Spectral640
        {
            get { return _Spectral640; }
            set { _Spectral640 = value; }
        }


        /// <summary>
        /// Spectrum  650 nm
        /// </summary>
        private double _Spectral650 = 0;
        public double Spectral650
        {
            get { return _Spectral650; }
            set { _Spectral650 = value; }
        }

        /// <summary>
        /// Spectrum  660 nm
        /// </summary>
        private double _Spectral660 = 0;
        public double Spectral660
        {
            get { return _Spectral660; }
            set { _Spectral660 = value; }
        }


        /// <summary>
        /// Spectrum  660 nm
        /// </summary>
        private double _Spectral670 = 0;
        public double Spectral670
        {
            get { return _Spectral670; }
            set { _Spectral670 = value; }
        }


        /// <summary>
        /// Spectrum  680 nm
        /// </summary>
        private double _Spectral680 = 0;
        public double Spectral680
        {
            get { return _Spectral680; }
            set { _Spectral680 = value; }
        }


        /// <summary>
        /// Spectrum  680 nm
        /// </summary>
        private double _Spectral690 = 0;
        public double Spectral690
        {
            get { return _Spectral690; }
            set { _Spectral690 = value; }
        }


        /// <summary>
        /// Spectrum  700 nm
        /// </summary>
        private double _Spectral700 = 0;
        public double Spectral700
        {
            get { return _Spectral700; }
            set { _Spectral700 = value; }
        }

        /// <summary>
        /// Spectrum  710 nm
        /// </summary>
        private double _Spectral710 = 0;
        public double Spectral710
        {
            get { return _Spectral710; }
            set { _Spectral710 = value; }
        }

        /// <summary>
        /// Spectrum  720 nm
        /// </summary>
        private double _Spectral720 = 0;
        public double Spectral720
        {
            get { return _Spectral720; }
            set { _Spectral720 = value; }
        }


        /// <summary>
        /// Spectrum  730 nm
        /// </summary>
        private double _Spectral730 = 0;
        public double Spectral730
        {
            get { return _Spectral730; }
            set { _Spectral730 = value; }
        }


        /// <summary>
        /// Spectrum  740 nm
        /// </summary>
        private double _Spectral740 = 0;
        public double Spectral740
        {
            get { return _Spectral740; }
            set { _Spectral740 = value; }
        }


        /// <summary>
        /// Spectrum  750 nm
        /// </summary>
        private double _Spectral750 = 0;
        public double Spectral750
        {
            get { return _Spectral750; }
            set { _Spectral750 = value; }
        }

        /// <summary>
        /// Spectrum  760 nm
        /// </summary>
        private double _Spectral760 = 0;
        public double Spectral760
        {
            get { return _Spectral760; }
            set { _Spectral760 = value; }
        }


        /// <summary>
        /// Spectrum  770 nm
        /// </summary>
        private double _Spectral770 = 0;
        public double Spectral770
        {
            get { return _Spectral770; }
            set { _Spectral770 = value; }
        }

        /// <summary>
        /// Spectrum  780 nm
        /// </summary>
        private double _Spectral780 = 0;
        public double Spectral780
        {
            get { return _Spectral780; }
            set { _Spectral780 = value; }
        }


        /// <summary>
        /// Spectrum  790 nm
        /// </summary>
        private double _Spectral790 = 0;
        public double Spectral790
        {
            get { return _Spectral790; }
            set { _Spectral790 = value; }
        }

        /// <summary>
        /// Spectrum  800 nm
        /// </summary>
        private double _Spectral800 = 0;
        public double Spectral800
        {
            get { return _Spectral800; }
            set { _Spectral800 = value; }
        }

        /// <summary>
        /// Spectrum  810 nm
        /// </summary>
        private double _Spectral810 = 0;
        public double Spectral810
        {
            get { return _Spectral810; }
            set { _Spectral810 = value; }
        }

        /// <summary>
        /// Spectrum  820 nm
        /// </summary>
        private double _Spectral820 = 0;
        public double Spectral820
        {
            get { return _Spectral820; }
            set { _Spectral820 = value; }
        }

        /// <summary>
        /// Spectrum  830 nm
        /// </summary>
        private double _Spectral830 = 0;
        public double Spectral830
        {

            get { return _Spectral830; }
            set { _Spectral830 = value; }
        }




        private void SetPatchType()
        {
            if (this._CMYK_C == 0 & this._CMYK_M == 0 & this._CMYK_Y == 0 & this._CMYK_K == 0)
            {
                this._PatchType = EnumPatchType.Paper;
                return;
            }
            else if (this._CMYK_C == 100 & this.CMYK_M == 0 & this._CMYK_Y == 0 & this._CMYK_K == 0)
            {
                this._PatchType = EnumPatchType.Cyan;
                return;
            }
            else if (this._CMYK_C == 0 & this.CMYK_M == 100 & this._CMYK_Y == 0 & this._CMYK_K == 0)
            {
                this._PatchType = EnumPatchType.Magenta;
                return;
            }
            else if (this._CMYK_C == 0 & this.CMYK_M == 0 & this._CMYK_Y == 100 & this._CMYK_K == 0)
            {
                this._PatchType = EnumPatchType.Yellow;
                return;
            }
            else if (this._CMYK_C == 0 & this.CMYK_M == 0 & this._CMYK_Y == 0 & this._CMYK_K == 100)
            {
                this._PatchType = EnumPatchType.Black;
                return;
            }
            else if (this._CMYK_C == 0 & this.CMYK_M == 100 & this._CMYK_Y == 100 & this._CMYK_K == 0)
            {
                this._PatchType = EnumPatchType.Red;
                return;
            }
            else if (this._CMYK_C == 100 & this.CMYK_M == 0 & this._CMYK_Y == 100 & this._CMYK_K == 0)
            {
                this.PatchType = EnumPatchType.Green;
                return;
            }
            else if (this._CMYK_C == 100 & this.CMYK_M == 100 & this._CMYK_Y == 0 & this._CMYK_K == 0)
            {
                this._PatchType = EnumPatchType.Blue;
                return;
            }
        }



        /// <summary>
        /// save the measured patches in the db
        /// </summary>
        public void Save()
        {
            SqlCommand objCmd = new SqlCommand();
            try
            {
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "CPSync.uspInsertMeasuredPatch";


                objCmd.Parameters.AddWithValue("@MeasurementID", this._MeasurementID);

                if (this.Sample_ID != 0)
                {
                    objCmd.Parameters.AddWithValue("@SampleID", this.Sample_ID);
                }
                //colortport does not have sample_name
                objCmd.Parameters.AddWithValue("@SampleName", ((this._SampleName == null) ? (object)DBNull.Value : (object)this._SampleName));
                objCmd.Parameters.AddWithValue("@CMYK_C", this.CMYK_C);
                objCmd.Parameters.AddWithValue("@CMYK_M", this.CMYK_M);
                objCmd.Parameters.AddWithValue("@CMYK_Y", this._CMYK_Y);
                objCmd.Parameters.AddWithValue("@CMYK_K", this._CMYK_K);


                objCmd.Parameters.AddWithValue("@Density_C", this._Density.Red);
                objCmd.Parameters.AddWithValue("@Density_M", this._Density.Green);
                objCmd.Parameters.AddWithValue("@Density_Y", this._Density.Blue);
                objCmd.Parameters.AddWithValue("@Density_V", this.Density.Visual);


                objCmd.Parameters.AddWithValue("@CIEXYZ_X", this._XYZ.X);
                objCmd.Parameters.AddWithValue("@CIEXYZ_Y", this._XYZ.Y);
                objCmd.Parameters.AddWithValue("@CIEXYZ_Z", this._XYZ.Z);

                objCmd.Parameters.AddWithValue("@CIELAB_L", this._LAB.L);
                objCmd.Parameters.AddWithValue("@CIELAB_a", this._LAB.A);
                objCmd.Parameters.AddWithValue("@CIELAB_b", this._LAB.B);

                (new Database()).ExecuteNonQuery(objCmd);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }


    }
}
