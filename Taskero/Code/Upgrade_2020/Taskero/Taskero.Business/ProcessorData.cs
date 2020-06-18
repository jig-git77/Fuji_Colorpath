#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Taskero.Business
{
    public class ProcessorData : Processor
    {

        #region GetZProcList
        /// <summary>
        /// Get All Processors
        /// </summary>
        /// <returns>returns Processor data</returns>
        public DataTable GetZProcList()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "PlateProc.uspGetZProcList";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion


        #region GetTrendLine
        /// <summary>
        /// Get All Processors
        /// </summary>
        /// <returns>returns Processor data</returns>
        public DataTable GetTrendLine()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "PlateProc.uspGetTrendLine";
            Cmd.Parameters.AddWithValue("@CPDongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        public DataTable GetTrendLine1()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "PlateProc.uspGetTrendLine1";
            Cmd.Parameters.AddWithValue("@CPDongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@ZProcID", this.ZProcIDs);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        public DataSet GetStatusDetail()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "PlateProc.uspGetStatusDetail";
            Cmd.Parameters.AddWithValue("@ZProcID", this.ZProcID);
            Cmd.Parameters.AddWithValue("@CPDongleID", this.DongleID);

            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

    }
}
