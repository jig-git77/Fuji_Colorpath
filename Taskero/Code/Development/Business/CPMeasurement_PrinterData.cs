#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPMeasurement_PrinterData : CPMeasurement_Printer
    {
        #region uspGetDTLTrendInk
        /// <summary>
        /// Purpose : This retrieves the Ink.
        /// Instruction : @CPMeasurementGUID should be in below given format.
        /// 28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
        /// </summary>  
        public DataTable uspGetDTLTrendInk()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLTrendInk";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
