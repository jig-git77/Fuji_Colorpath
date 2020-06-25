#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public abstract class CPMeasurement_Printer
    {

        #region Variables
        private string _TableName = "CPMeasurement_Printer";
        private int _CPMeasurement_PrinterID = 0;
        private string _CPMeasurementGUID = "";
        private string _Printer_Model = "";
        private string _Printer_SerialNum = "";
        private string _Printer_State = "";
        private string _Printer_User = "";
        private string _Printer_Host = "";
        private string _Printer_Time = "";
        private string _Printer_InkName_0 = "";
        private decimal _Printer_InkValue_0 = Convert.ToDecimal("0");
        private string _Printer_InkName_1 = "";
        private decimal _Printer_InkValue_1 = Convert.ToDecimal("0");
        private string _Printer_InkName_2 = "";
        private decimal _Printer_InkValue_2 = Convert.ToDecimal("0");
        private string _Printer_InkName_3 = "";
        private decimal _Printer_InkValue_3 = Convert.ToDecimal("0");
        private string _Printer_InkName_4 = "";
        private decimal _Printer_InkValue_4 = Convert.ToDecimal("0");
        private string _Printer_InkName_5 = "";
        private decimal _Printer_InkValue_5 = Convert.ToDecimal("0");
        private string _Printer_InkName_6 = "";
        private decimal _Printer_InkValue_6 = Convert.ToDecimal("0");
        private string _Printer_InkName_7 = "";
        private decimal _Printer_InkValue_7 = Convert.ToDecimal("0");
        private string _Printer_InkName_8 = "";
        private decimal _Printer_InkValue_8 = Convert.ToDecimal("0");
        private string _Printer_Finisher = "";
        private decimal _Printer_FinisherValue = Convert.ToDecimal("0");
        private string _Printer_Paper = "";
        private string _Printer_CC_Table = "";
        private string _Printer_ICC_Target = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPMeasurement_PrinterID
        {
            get { return _CPMeasurement_PrinterID; }
            set { _CPMeasurement_PrinterID = value; }
        }
        public string CPMeasurementGUID
        {
            get { return _CPMeasurementGUID; }
            set { _CPMeasurementGUID = value; }
        }
        public string Printer_Model
        {
            get { return _Printer_Model; }
            set { _Printer_Model = value; }
        }
        public string Printer_SerialNum
        {
            get { return _Printer_SerialNum; }
            set { _Printer_SerialNum = value; }
        }
        public string Printer_State
        {
            get { return _Printer_State; }
            set { _Printer_State = value; }
        }
        public string Printer_User
        {
            get { return _Printer_User; }
            set { _Printer_User = value; }
        }
        public string Printer_Host
        {
            get { return _Printer_Host; }
            set { _Printer_Host = value; }
        }
        public string Printer_Time
        {
            get { return _Printer_Time; }
            set { _Printer_Time = value; }
        }
        public string Printer_InkName_0
        {
            get { return _Printer_InkName_0; }
            set { _Printer_InkName_0 = value; }
        }
        public decimal Printer_InkValue_0
        {
            get { return _Printer_InkValue_0; }
            set { _Printer_InkValue_0 = value; }
        }
        public string Printer_InkName_1
        {
            get { return _Printer_InkName_1; }
            set { _Printer_InkName_1 = value; }
        }
        public decimal Printer_InkValue_1
        {
            get { return _Printer_InkValue_1; }
            set { _Printer_InkValue_1 = value; }
        }
        public string Printer_InkName_2
        {
            get { return _Printer_InkName_2; }
            set { _Printer_InkName_2 = value; }
        }
        public decimal Printer_InkValue_2
        {
            get { return _Printer_InkValue_2; }
            set { _Printer_InkValue_2 = value; }
        }
        public string Printer_InkName_3
        {
            get { return _Printer_InkName_3; }
            set { _Printer_InkName_3 = value; }
        }
        public decimal Printer_InkValue_3
        {
            get { return _Printer_InkValue_3; }
            set { _Printer_InkValue_3 = value; }
        }
        public string Printer_InkName_4
        {
            get { return _Printer_InkName_4; }
            set { _Printer_InkName_4 = value; }
        }
        public decimal Printer_InkValue_4
        {
            get { return _Printer_InkValue_4; }
            set { _Printer_InkValue_4 = value; }
        }
        public string Printer_InkName_5
        {
            get { return _Printer_InkName_5; }
            set { _Printer_InkName_5 = value; }
        }
        public decimal Printer_InkValue_5
        {
            get { return _Printer_InkValue_5; }
            set { _Printer_InkValue_5 = value; }
        }
        public string Printer_InkName_6
        {
            get { return _Printer_InkName_6; }
            set { _Printer_InkName_6 = value; }
        }
        public decimal Printer_InkValue_6
        {
            get { return _Printer_InkValue_6; }
            set { _Printer_InkValue_6 = value; }
        }
        public string Printer_InkName_7
        {
            get { return _Printer_InkName_7; }
            set { _Printer_InkName_7 = value; }
        }
        public decimal Printer_InkValue_7
        {
            get { return _Printer_InkValue_7; }
            set { _Printer_InkValue_7 = value; }
        }
        public string Printer_InkName_8
        {
            get { return _Printer_InkName_8; }
            set { _Printer_InkName_8 = value; }
        }
        public decimal Printer_InkValue_8
        {
            get { return _Printer_InkValue_8; }
            set { _Printer_InkValue_8 = value; }
        }
        public string Printer_Finisher
        {
            get { return _Printer_Finisher; }
            set { _Printer_Finisher = value; }
        }
        public decimal Printer_FinisherValue
        {
            get { return _Printer_FinisherValue; }
            set { _Printer_FinisherValue = value; }
        }
        public string Printer_Paper
        {
            get { return _Printer_Paper; }
            set { _Printer_Paper = value; }
        }
        public string Printer_CC_Table
        {
            get { return _Printer_CC_Table; }
            set { _Printer_CC_Table = value; }
        }
        public string Printer_ICC_Target
        {
            get { return _Printer_ICC_Target; }
            set { _Printer_ICC_Target = value; }
        }
        #endregion
    }
}