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
    public abstract class TempRHData
    {

        #region Variables
        private string _TableName = "TempRHData";
        private int _TempRHDataID = 0;
        private short _TempRHDeviceID = 0;
        private string _CreatedDT = "";
        private string _CreatedGMT = "";
        private decimal _TempC = Convert.ToDecimal("0");
        private decimal _TempF = 0;
        private decimal _RH = Convert.ToDecimal("0");
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int TempRHDataID
        {
            get { return _TempRHDataID; }
            set { _TempRHDataID = value; }
        }
        public short TempRHDeviceID
        {
            get { return _TempRHDeviceID; }
            set { _TempRHDeviceID = value; }
        }
        public string CreatedDT
        {
            get { return _CreatedDT; }
            set { _CreatedDT = value; }
        }
        public string CreatedGMT
        {
            get { return _CreatedGMT; }
            set { _CreatedGMT = value; }
        }
        public decimal TempC
        {
            get { return _TempC; }
            set { _TempC = value; }
        }
        public decimal TempF
        {
            get { return _TempF; }
            set { _TempF = value; }
        }
        public decimal RH
        {
            get { return _RH; }
            set { _RH = value; }
        }
        #endregion       
    }
}