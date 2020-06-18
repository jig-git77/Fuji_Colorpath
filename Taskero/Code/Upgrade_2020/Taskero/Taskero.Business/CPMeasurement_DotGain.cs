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
    public abstract class CPMeasurement_DotGain
    {

        #region Variables
        private string _TableName = "CPMeasurement_DotGain";
        private int _CPMeasurement_DotGainID = 0;
        private string _CPMeasurementGUID = "";
        private decimal _DotGain_C50 = Convert.ToDecimal("0");
        private decimal _DotGain_M50 = Convert.ToDecimal("0");
        private decimal _DotGain_Y50 = Convert.ToDecimal("0");
        private decimal _DotGain_K50 = Convert.ToDecimal("0");
        private decimal _DotGain_C25 = Convert.ToDecimal("0");
        private decimal _DotGain_M25 = Convert.ToDecimal("0");
        private decimal _DotGain_Y25 = Convert.ToDecimal("0");
        private decimal _DotGain_K25 = Convert.ToDecimal("0");
        private decimal _DotGain_C75 = Convert.ToDecimal("0");
        private decimal _DotGain_M75 = Convert.ToDecimal("0");
        private decimal _DotGain_Y75 = Convert.ToDecimal("0");
        private decimal _DotGain_K75 = Convert.ToDecimal("0");
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPMeasurement_DotGainID
        {
            get { return _CPMeasurement_DotGainID; }
            set { _CPMeasurement_DotGainID = value; }
        }
        public string CPMeasurementGUID
        {
            get { return _CPMeasurementGUID; }
            set { _CPMeasurementGUID = value; }
        }
        public decimal DotGain_C50
        {
            get { return _DotGain_C50; }
            set { _DotGain_C50 = value; }
        }
        public decimal DotGain_M50
        {
            get { return _DotGain_M50; }
            set { _DotGain_M50 = value; }
        }
        public decimal DotGain_Y50
        {
            get { return _DotGain_Y50; }
            set { _DotGain_Y50 = value; }
        }
        public decimal DotGain_K50
        {
            get { return _DotGain_K50; }
            set { _DotGain_K50 = value; }
        }
        public decimal DotGain_C25
        {
            get { return _DotGain_C25; }
            set { _DotGain_C25 = value; }
        }
        public decimal DotGain_M25
        {
            get { return _DotGain_M25; }
            set { _DotGain_M25 = value; }
        }
        public decimal DotGain_Y25
        {
            get { return _DotGain_Y25; }
            set { _DotGain_Y25 = value; }
        }
        public decimal DotGain_K25
        {
            get { return _DotGain_K25; }
            set { _DotGain_K25 = value; }
        }
        public decimal DotGain_C75
        {
            get { return _DotGain_C75; }
            set { _DotGain_C75 = value; }
        }
        public decimal DotGain_M75
        {
            get { return _DotGain_M75; }
            set { _DotGain_M75 = value; }
        }
        public decimal DotGain_Y75
        {
            get { return _DotGain_Y75; }
            set { _DotGain_Y75 = value; }
        }
        public decimal DotGain_K75
        {
            get { return _DotGain_K75; }
            set { _DotGain_K75 = value; }
        }
        #endregion       

    }
}