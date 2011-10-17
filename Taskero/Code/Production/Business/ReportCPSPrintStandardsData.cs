using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Fuji.Business
{
    public class ReportCPSPrintStandardsData : ReportCPSPrintStandards
    {
        #region uspPrintStandardReport
        public DataSet GetPrintStandardReport()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspPrintStandardReport";
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunId);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion
    }
}
