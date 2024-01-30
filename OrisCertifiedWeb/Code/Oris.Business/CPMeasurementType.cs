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
    public abstract class CPMeasurementType
    {

        #region Variables
        private string _TableName = "CPMeasurementType";
        private short _CPMeasurementTypeID = 0;
        private string _CPMeasurementType = "";
        private string _CPMeasurementTypeDesc = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short CPMeasurementTypeID
        {
            get { return _CPMeasurementTypeID; }
            set { _CPMeasurementTypeID = value; }
        }
        public string CPMeasurementType1
        {
            get { return _CPMeasurementType; }
            set { _CPMeasurementType = value; }
        }
        public string CPMeasurementTypeDesc
        {
            get { return _CPMeasurementTypeDesc; }
            set { _CPMeasurementTypeDesc = value; }
        }
        #endregion

    }
}