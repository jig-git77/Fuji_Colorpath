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
    public class CPMeasurement_DotGainData : CPMeasurement_DotGain
    {
        #region uspGetDTLTrendDotgain
        /// <summary>
        /// Purpose : This retrieves the Dotgain.
        /// Instruction : @CPMeasurementGUID should be in below given format.
        /// 28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
        /// </summary>  
        public DataTable uspGetDTLTrendDotgain()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLTrendDotgain";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
