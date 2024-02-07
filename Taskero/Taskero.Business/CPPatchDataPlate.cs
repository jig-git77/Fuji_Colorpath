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
    public abstract class CPPatchDataPlate
    {

        #region Variables
        private string _TableName = "CPPatchDataPlate";
        private int _CPPatchDataPlateID = 0;
        private string _MeasurementGUID = "";
        private short _ID_N = 0;
        private short _Sample_ID = 0;
        private string _Sample_Name = "";
        private decimal _DOT_AREA_TARGET = Convert.ToDecimal("0");
        private decimal _DOT_AREA_ACTUAL = Convert.ToDecimal("0");
        private decimal _DEVIATION = Convert.ToDecimal("0");
        private decimal _TOL_STRICT = Convert.ToDecimal("0");
        private decimal _DOT_AREA_REFERENCE = Convert.ToDecimal("0");
        private string _Plate_Color = "";
        private string _CREATED_TIME = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPPatchDataPlateID
        {
            get { return _CPPatchDataPlateID; }
            set { _CPPatchDataPlateID = value; }
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
        public decimal DOT_AREA_TARGET
        {
            get { return _DOT_AREA_TARGET; }
            set { _DOT_AREA_TARGET = value; }
        }
        public decimal DOT_AREA_ACTUAL
        {
            get { return _DOT_AREA_ACTUAL; }
            set { _DOT_AREA_ACTUAL = value; }
        }
        public decimal DEVIATION
        {
            get { return _DEVIATION; }
            set { _DEVIATION = value; }
        }
        public decimal TOL_STRICT
        {
            get { return _TOL_STRICT; }
            set { _TOL_STRICT = value; }
        }
        public decimal DOT_AREA_REFERENCE
        {
            get { return _DOT_AREA_REFERENCE; }
            set { _DOT_AREA_REFERENCE = value; }
        }
        public string Plate_Color
        {
            get { return _Plate_Color; }
            set { _Plate_Color = value; }
        }
        public string CREATED_TIME
        {
            get { return _CREATED_TIME; }
            set { _CREATED_TIME = value; }
        }
        #endregion       
    }
}