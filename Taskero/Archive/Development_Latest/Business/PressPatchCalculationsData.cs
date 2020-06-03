#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class PressPatchCalculationsData : PressPatchCalculations
    {
        #region uspGetPressChartZoneSelection
        /// <summary>
        /// The query will give 2 results 
        /// </summary>       
        public DataSet uspGetPressChartZoneSelection()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetPatchChartZoneSelection";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetPressChartZoneSelection
        /// <summary>
        /// The query will give 2 results 
        /// </summary>       
        public DataSet uspGetPressChartZoneSelection(int _SheetNumber)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetPatchChartZoneSelection";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetSheetChartZoneSelection
        /// <summary>
        /// The query will give 2 results 
        /// </summary>       
        public DataSet uspGetSheetChartZoneSelection(int _SheetNumber)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetSheetChartZoneSelection";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetPressChartZoneSelectionColorsSheet
        /// <summary>
        /// The query will give 2 results 
        /// </summary>       
        public DataSet uspGetSheetChartZoneSelectionColors(int _SheetNumber)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetSheetChartZoneSelectionColors";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetPressPatchTrendChartPerZoneNumber
        /// <summary>
        /// Patch Trend Chart (Trend of the Patch across Sheets in the same Zone number) 
        /// </summary>
        public DataTable uspGetPressPatchTrendChartPerZoneNumber()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetPatchDeltaETrendPerZone";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@ZoneNumber", this.ZoneNumber);
            Cmd.Parameters.AddWithValue("@ColorID", this.ColorID);
            return (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetPressPatchTrendChartPerZoneNumberSheet
        /// <summary>
        /// Patch Trend Chart (Trend of the Patch across Sheets in the same Zone number) 
        /// </summary>
        public DataTable uspGetSheetPatchTrendChartPerZoneNumber(int _SheetNumber)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetSheetPatchTrendChartPerZoneNumber";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@ZoneNumber", this.ZoneNumber);
            Cmd.Parameters.AddWithValue("@ColorID", this.ColorID);
            Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
            return (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetPressTrend
        /// <summary>
        /// The is will get all the information for the Trend chart of Press.
        /// Table 1 : Avg Density and Deltae
        /// Table 2 : Returns Print Target And Dot gain
        /// </summary>
        public DataSet uspGetJobTrendView()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetJobTrendView";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetPressTrendSheet
        /// <summary>
        /// The is will get all the information for the Trend chart of Press.
        /// Table 1 : Avg Density and Deltae
        /// Table 2 : Returns Print Target And Dot gain
        /// </summary>
        public DataSet uspGetSheetTrendView(int _SheetNumber)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetSheetTrendView";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion
        
        #region uspGetPressColorChars
        /// <summary>
        /// The is will get all the information for the Trend chart of Press.
        /// </summary>
        public DataSet uspGetPressColorChars()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetJobColorChars";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetSheetColorChars
        /// <summary>
        /// The is will get all the information for the Trend chart of Press.
        /// </summary>
        public DataSet uspGetSheetColorChars(int _SheetNumber)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Press.uspGetSheetColorChars";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion
    }
}