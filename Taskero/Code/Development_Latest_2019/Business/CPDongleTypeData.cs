#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPDongleTypeData : CPDongleType
    {
        #region uspGetDongleType
        /// <summary>
        /// Get DongleType
        /// </summary>
        /// <returns>returns the Dongle Type data</returns>
        public DataTable uspGetDongleType()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "CPV.uspGetDongleType";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

       
    }
}
