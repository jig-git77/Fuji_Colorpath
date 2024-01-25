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
    public class CategoryUser
    {
        #region Variables
        private string _TableName = "CategoryUser";
        private int _CategoryUserID = 0;
        private int _TaskeroUserID = 0;
        private int _CategoryID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CategoryUserID
        {
            get { return _CategoryUserID; }
            set { _CategoryUserID = value; }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }
        #endregion    
    }
}
