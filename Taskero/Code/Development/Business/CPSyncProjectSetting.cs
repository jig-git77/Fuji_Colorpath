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
    public abstract class CPSyncProjectSetting
    {
        #region Variables
        private Int32 _ProjectID = 0;
        private Boolean _UsePlugin = false;
        private int _Observer = 0;
        private int _Illuminant = 0;
        private int _DensityType = 0;
        private int _IlluminationMode = 0;
        private int? _ChartTypeID = 0;
        private int? _InstrumentTypeID = 0;
        private string _SerialNumber = string.Empty;

        #endregion

        #region Properties
        public Int32  ProjectID { get { return _ProjectID; } set { _ProjectID = value; } }
        public Boolean UsePlugin { get { return _UsePlugin; } set { _UsePlugin = value; } }
        public int Observer { get { return _Observer; } set { _Observer = value; } }
        public int Illuminant { get { return _Illuminant; } set { _Illuminant = value; } }
        public int DensityType { get { return _DensityType; } set { _DensityType = value; } }
        public int IlluminationMode { get { return _IlluminationMode; } set { _IlluminationMode = value; } }
        public int? ChartTypeID { get { return _ChartTypeID; } set { _ChartTypeID = value; } }
        public int? InstrumentTypeID { get { return _InstrumentTypeID; } set { _InstrumentTypeID = value; } }
        public string SerialNumber { get { return _SerialNumber; } set { _SerialNumber = value; } }
        #endregion
    }
}
