using System;
using System.Data;
using System.Data.SqlClient;

namespace Taskero.Business
{
    public class ReportTargetMeasurementData
    {
        #region " === Constructor === "
            public ReportTargetMeasurementData()
            { }
        #endregion

        #region " === Property === "
            public string CPMeasurementGUID { get; set; }

            public string PromoID { get; set; }
            public string ItemNumber { get; set; }
            public string Brand { get; set; }
            public string OrderCode { get; set; }
        #endregion

        #region " === Methods === "
            public DataSet GetMeasurementData()
            {
                
                try
                {
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = "Report.usp_PS_GetMeasurementData";
                    Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
                    return (new DataAccess.Database()).GetDataSet(Cmd);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }

            public DataTable GetSearchJobs()
            {
                try
                {
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = "Report.usp_PS_SearchJobs";
                    Cmd.Parameters.AddWithValue("@PromoID", PromoID);
                    Cmd.Parameters.AddWithValue("@ItemNumber", ItemNumber);
                    Cmd.Parameters.AddWithValue("@Brand", Brand);
                    Cmd.Parameters.AddWithValue("@OrderCode", OrderCode);
                    return (new DataAccess.Database()).GetDataTable(Cmd);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public DataTable GetPatchData()
            {
                try
                {
                    SqlCommand Cmd = new SqlCommand();
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = "Report.usp_PS_GetPatchData";
                    Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
                    return (new DataAccess.Database()).GetDataTable(Cmd);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        #endregion
    }
}
