#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class PressTypeData : PressType
    {
        #region uspGetAllPressType
        /// <summary>
        /// Get All PressType
        /// </summary>
        /// <returns>returns the All Press Type data</returns>
        public DataTable uspGetAllPressType()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "Press.uspGetAllPressType";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
