#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Taskero.DataAccess;
#endregion

namespace Taskero.Business
{
    public abstract class CPMeasurement_PT_Prop
    {

        #region Variables
        private string _TableName = "CPMeasurement_PT_Prop";
        private int _CPMeasurement_PT_PropID = 0;
        private string _CPMeasurementGUID = "";
        private decimal _Density_C = Convert.ToDecimal("0");
        private decimal _Density_M = Convert.ToDecimal("0");
        private decimal _Density_Y = Convert.ToDecimal("0");
        private decimal _Density_K = Convert.ToDecimal("0");
        private decimal _Density_TH = Convert.ToDecimal("0");
        private decimal _Dotgain_C = Convert.ToDecimal("0");
        private decimal _Dotgain_M = Convert.ToDecimal("0");
        private decimal _Dotgain_Y = Convert.ToDecimal("0");
        private decimal _Dotgain_K = Convert.ToDecimal("0");
        private decimal _Dotgain_TH = Convert.ToDecimal("0");
        private decimal _Print_Contrast_C = Convert.ToDecimal("0");
        private decimal _Print_Contrast_M = Convert.ToDecimal("0");
        private decimal _Print_Contrast_Y = Convert.ToDecimal("0");
        private decimal _Print_Contrast_K = Convert.ToDecimal("0");
        private decimal _Print_Contrast_TH = Convert.ToDecimal("0");
        private decimal _Trap_R = Convert.ToDecimal("0");
        private decimal _Trap_G = Convert.ToDecimal("0");
        private decimal _Trap_B = Convert.ToDecimal("0");
        private decimal _Trap_TH = Convert.ToDecimal("0");
        private string _1ST_INK = "";
        private string _2ND_INK = "";
        private string _3RD_INK = "";
        private string _4TH_INK = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPMeasurement_PT_PropID
        {
            get { return _CPMeasurement_PT_PropID; }
            set { _CPMeasurement_PT_PropID = value; }
        }
        public string CPMeasurementGUID
        {
            get { return _CPMeasurementGUID; }
            set { _CPMeasurementGUID = value; }
        }
        public decimal Density_C
        {
            get { return _Density_C; }
            set { _Density_C = value; }
        }
        public decimal Density_M
        {
            get { return _Density_M; }
            set { _Density_M = value; }
        }
        public decimal Density_Y
        {
            get { return _Density_Y; }
            set { _Density_Y = value; }
        }
        public decimal Density_K
        {
            get { return _Density_K; }
            set { _Density_K = value; }
        }
        public decimal Density_TH
        {
            get { return _Density_TH; }
            set { _Density_TH = value; }
        }
        public decimal Dotgain_C
        {
            get { return _Dotgain_C; }
            set { _Dotgain_C = value; }
        }
        public decimal Dotgain_M
        {
            get { return _Dotgain_M; }
            set { _Dotgain_M = value; }
        }
        public decimal Dotgain_Y
        {
            get { return _Dotgain_Y; }
            set { _Dotgain_Y = value; }
        }
        public decimal Dotgain_K
        {
            get { return _Dotgain_K; }
            set { _Dotgain_K = value; }
        }
        public decimal Dotgain_TH
        {
            get { return _Dotgain_TH; }
            set { _Dotgain_TH = value; }
        }
        public decimal Print_Contrast_C
        {
            get { return _Print_Contrast_C; }
            set { _Print_Contrast_C = value; }
        }
        public decimal Print_Contrast_M
        {
            get { return _Print_Contrast_M; }
            set { _Print_Contrast_M = value; }
        }
        public decimal Print_Contrast_Y
        {
            get { return _Print_Contrast_Y; }
            set { _Print_Contrast_Y = value; }
        }
        public decimal Print_Contrast_K
        {
            get { return _Print_Contrast_K; }
            set { _Print_Contrast_K = value; }
        }
        public decimal Print_Contrast_TH
        {
            get { return _Print_Contrast_TH; }
            set { _Print_Contrast_TH = value; }
        }
        public decimal Trap_R
        {
            get { return _Trap_R; }
            set { _Trap_R = value; }
        }
        public decimal Trap_G
        {
            get { return _Trap_G; }
            set { _Trap_G = value; }
        }
        public decimal Trap_B
        {
            get { return _Trap_B; }
            set { _Trap_B = value; }
        }
        public decimal Trap_TH
        {
            get { return _Trap_TH; }
            set { _Trap_TH = value; }
        }
        public string X1ST_INK
        {
            get { return _1ST_INK; }
            set { _1ST_INK = value; }
        }
        public string X2ND_INK
        {
            get { return _2ND_INK; }
            set { _2ND_INK = value; }
        }
        public string X3RD_INK
        {
            get { return _3RD_INK; }
            set { _3RD_INK = value; }
        }
        public string X4TH_INK
        {
            get { return _4TH_INK; }
            set { _4TH_INK = value; }
        }
        #endregion     
    }
}