#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    /// <summary>
    /// Created to use only Press Sps and Views.
    /// </summary>
    public class PressData : Press
    {
        #region uspGetPressChartZoneSelectionColors
        /// <summary>
        /// Gets the color on the basis of the Job and DongleID.
        /// </summary>
        /// <returns></returns>
        public DataSet uspGetPressChartZoneSelectionColors()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Press.uspGetChartZoneSelectionColors";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion
    }
}
