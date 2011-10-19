#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPSyncProjectSettingData : CPSyncProjectSetting
    {
        #region uspGetChartByInstrument
        /// <summary>
        /// Get Chart By Instrument Type ID
        /// </summary>        
        /// <returns></returns>
        public DataTable uspGetChartByInstrument()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetChartByInstrument";
            Cmd.Parameters.AddWithValue("@InstrumentTypeID", InstrumentTypeID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion


        #region uspGetProjectSetting
        /// <summary>
        /// Get Project Setting By Project ID
        /// </summary>        
        /// <returns></returns>
        public DataTable uspGetProjectSetting()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetProjectSetting";
            Cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion


        #region uspInsertProjectSetting
        /// <summary>
        /// Insert And Update new CPSync Project Setting
        /// </summary>
        public void uspInsertProjectSetting()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertProjectSetting";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@ProjectID", this.ProjectID);
            Cmd.Parameters.AddWithValue("@UsePlugin", this.UsePlugin);
            Cmd.Parameters.AddWithValue("@Observer", this.Observer);
            Cmd.Parameters.AddWithValue("@Illuminant", this.Illuminant);
            Cmd.Parameters.AddWithValue("@DensityType", this.DensityType);
            Cmd.Parameters.AddWithValue("@IlluminationMode", this.IlluminationMode);
            Cmd.Parameters.AddWithValue("@ChartTypeID", this.ChartTypeID);
            Cmd.Parameters.AddWithValue("@InstrumentTypeID", this.InstrumentTypeID);
            Cmd.Parameters.AddWithValue("@SerialNumber", this.SerialNumber);

            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion
    }
}
