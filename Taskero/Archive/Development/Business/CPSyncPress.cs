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
    public abstract class CPSyncPress
    {
        #region Variables
        private string _TableName = "Press";
        private short _PressID = 0;
        private short _CPDongleID = 0;
        private string _PressName = "";
        private string _PressManufacturer = "";
        private int _NumOfInkUnits = 0;
        private int _PressTypeID = 0;
        private bool useDLP = true;   //use the DLP for previous run for the iteration
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PressID
        {
            get { return _PressID; }
            set { _PressID = value; }
        }
        public short CPDongleID
        {
            get { return _CPDongleID; }
            set { _CPDongleID = value; }
        }
        public string PressName
        {
            get { return _PressName; }
            set { _PressName = value; }
        }
        public string PressManufacturer
        {
            get { return _PressManufacturer; }
            set { _PressManufacturer = value; }
        }
        public int NumOfInkUnits
        {
            get { return _NumOfInkUnits; }
            set { _NumOfInkUnits = value; }
        }
        public int PressTypeID
        {
            get { return _PressTypeID; }
            set { _PressTypeID = value; }
        }
               
        #endregion
    }
}
