#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion

#region Others
using Fuji.DataAccess;
#endregion


namespace Fuji.Business
{
    public abstract class CPSyncPressRunCurve
    {
        #region Variables
        private string _TableName = "PressRunCurve";
        private short _PressRunCurveDataID = 0;
        private short _PressRunID = 0;
        private int _ReferenceNo = 0;
        private decimal _CMYK_C = 0;
        private decimal _CMYK_M = 0;
        private decimal _CMYK_Y = 0;
        private decimal _CMYK_K = 0;


        

        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PressRunCurveDataID
        {
            get { return _PressRunCurveDataID; }
            set { _PressRunCurveDataID = value; }
        }
        public short PressRunID
        {
            get { return _PressRunID; }
            set { _PressRunID = value; }
        }

        public int ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
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

        #endregion
    }
}
