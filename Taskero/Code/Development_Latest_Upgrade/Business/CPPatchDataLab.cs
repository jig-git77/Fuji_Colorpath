#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public abstract class CPPatchDataLab
    {

        #region Variables
        private string _TableName = "CPPatchDataLab";
        private int _CPPatchDataLabID = 0;
        private string _MeasurementGUID = "";
        private short _ID_N = 0;
        private short _Sample_ID = 0;
        private string _Sample_Name = "";
        private decimal _CMYK_C = Convert.ToDecimal("0");
        private decimal _CMYK_M = Convert.ToDecimal("0");
        private decimal _CMYK_Y = Convert.ToDecimal("0");
        private decimal _CMYK_K = Convert.ToDecimal("0");
        private decimal _LAB_L1 = Convert.ToDecimal("0");
        private decimal _LAB_A1 = Convert.ToDecimal("0");
        private decimal _LAB_B1 = Convert.ToDecimal("0");
        private decimal _LAB_L2 = Convert.ToDecimal("0");
        private decimal _LAB_A2 = Convert.ToDecimal("0");
        private decimal _LAB_B2 = Convert.ToDecimal("0");
        private decimal _DE = Convert.ToDecimal("0");
        private decimal _DL = Convert.ToDecimal("0");
        private decimal _DA = Convert.ToDecimal("0");
        private decimal _DB = Convert.ToDecimal("0");
        private decimal _DH = Convert.ToDecimal("0");
        private decimal _DC = Convert.ToDecimal("0");
        private decimal _DENSITY = Convert.ToDecimal("0");
        private decimal _DENS_TARGET = Convert.ToDecimal("0");
        private decimal _DENS_ACTUAL = Convert.ToDecimal("0");
        private decimal _TOL_STRICT = Convert.ToDecimal("0");
        private decimal _TOL_STAND = Convert.ToDecimal("0");
        private string _CREATED_TIME = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPPatchDataLabID
        {
            get { return _CPPatchDataLabID; }
            set { _CPPatchDataLabID = value; }
        }
        public string MeasurementGUID
        {
            get { return _MeasurementGUID; }
            set { _MeasurementGUID = value; }
        }
        public short ID_N
        {
            get { return _ID_N; }
            set { _ID_N = value; }
        }
        public short Sample_ID
        {
            get { return _Sample_ID; }
            set { _Sample_ID = value; }
        }
        public string Sample_Name
        {
            get { return _Sample_Name; }
            set { _Sample_Name = value; }
        }
        public decimal CMYK_C
        {
            get { return _CMYK_C; }
            set { _CMYK_C = value; }
        }
        public decimal CMYK_M
        {
            get { return _CMYK_M; }
            set { _CMYK_M = value; }
        }
        public decimal CMYK_Y
        {
            get { return _CMYK_Y; }
            set { _CMYK_Y = value; }
        }
        public decimal CMYK_K
        {
            get { return _CMYK_K; }
            set { _CMYK_K = value; }
        }
        public decimal LAB_L1
        {
            get { return _LAB_L1; }
            set { _LAB_L1 = value; }
        }
        public decimal LAB_A1
        {
            get { return _LAB_A1; }
            set { _LAB_A1 = value; }
        }
        public decimal LAB_B1
        {
            get { return _LAB_B1; }
            set { _LAB_B1 = value; }
        }
        public decimal LAB_L2
        {
            get { return _LAB_L2; }
            set { _LAB_L2 = value; }
        }
        public decimal LAB_A2
        {
            get { return _LAB_A2; }
            set { _LAB_A2 = value; }
        }
        public decimal LAB_B2
        {
            get { return _LAB_B2; }
            set { _LAB_B2 = value; }
        }
        public decimal DE
        {
            get { return _DE; }
            set { _DE = value; }
        }
        public decimal DL
        {
            get { return _DL; }
            set { _DL = value; }
        }
        public decimal DA
        {
            get { return _DA; }
            set { _DA = value; }
        }
        public decimal DB
        {
            get { return _DB; }
            set { _DB = value; }
        }
        public decimal DH
        {
            get { return _DH; }
            set { _DH = value; }
        }
        public decimal DC
        {
            get { return _DC; }
            set { _DC = value; }
        }
        public decimal DENSITY
        {
            get { return _DENSITY; }
            set { _DENSITY = value; }
        }
        public decimal DENS_TARGET
        {
            get { return _DENS_TARGET; }
            set { _DENS_TARGET = value; }
        }
        public decimal DENS_ACTUAL
        {
            get { return _DENS_ACTUAL; }
            set { _DENS_ACTUAL = value; }
        }
        public decimal TOL_STRICT
        {
            get { return _TOL_STRICT; }
            set { _TOL_STRICT = value; }
        }
        public decimal TOL_STAND
        {
            get { return _TOL_STAND; }
            set { _TOL_STAND = value; }
        }
        public string CREATED_TIME
        {
            get { return _CREATED_TIME; }
            set { _CREATED_TIME = value; }
        }
        #endregion      

    }
}