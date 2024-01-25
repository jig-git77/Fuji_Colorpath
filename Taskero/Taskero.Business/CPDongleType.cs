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
    public abstract class CPDongleType
    {

        #region Variables
        //private string _TableName = "CPDeviceType";
        private short _CPDongleTypeID = 0;
        private string _CPDongleType = "";
        #endregion

        #region Properties
        //public string TableName
        //{
        //    get { return _TableName; }
        //    set { }
        //}
        public short CPDongleTypeID
        {
            get { return _CPDongleTypeID; }
            set { _CPDongleTypeID = value; }
        }
        public string CPDongleTypeDesc
        {
            get { return _CPDongleType; }
            set { _CPDongleType = value; }
        }
        #endregion
    }
}
