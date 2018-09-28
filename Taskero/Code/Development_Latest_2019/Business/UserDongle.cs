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
    public abstract class UserDongle
    {

        #region Variables
        private string _TableName = "UserDongle";
        private int _UserDongleID = 0;
        private int _TaskeroUserID = 0;
        private int _CPDongleID = 0;
        private string _DongleDesc = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int UserDongleID
        {
            get { return _UserDongleID; }
            set { _UserDongleID = value; }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        public int CPDongleID
        {
            get { return _CPDongleID; }
            set { _CPDongleID = value; }
        }
        public string DongleDesc
        {
            get { return _DongleDesc; }
            set { _DongleDesc = value; }
        }
        #endregion      

    }
}