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
    public abstract class RecentlyVideos
    {
        #region Variables
        private string _TableName = "RecentlyVideos";
        private int _VideoID = 0;
        private int _TaskeroUserID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int VideoID
        {
            get { return _VideoID; }
            set { _VideoID = value; }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        #endregion    
    }
}
