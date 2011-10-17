#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class ReportPrepressSingleMeasurementData : ReportPrepressSingleMeasurement
    {
        #region usp_CPV_SingleMeasurement
        public DataSet GetSingleMeasurement()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_CPV_SingleMeasurement";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.CPMeasurementGUID);            
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

    }
}
