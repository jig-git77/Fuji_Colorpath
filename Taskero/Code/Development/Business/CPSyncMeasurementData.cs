#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPSyncMeasurementData : CPSyncMeasurement
    {
        #region uspUpdateIsUsedMeasurement
        /// <summary>
        /// Inserts new CPSync Press
        /// </summary>
        public void uspUpdateIsUsedMeasurement()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspUpdateIsUsedMeasurement";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetChartTrendData
        public DataTable uspGetChartTrendData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetChartTrendData";
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            //Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetChartSummaryData
        public DataSet uspGetChartSummaryData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetChartSummaryData";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetAnalyzeMeasurementG7ChartsData
        public DataSet uspGetAnalyzeMeasurementG7ChartsData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetAnalyzeG7ChartData";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetAnalyzeMeasurementISOChartsData
        public DataSet uspGetAnalyzeMeasurementISOChartsData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetAnalyzeISOChartData";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetChartGrayBalanceData
        public DataSet uspGetChartGrayBalanceData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetChartGrayBalanceData";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion


        #region uspGetChartMeasurementData
        public DataTable uspGetChartMeasurementData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetChartMeasurementData";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetMeasurementByPressRunID
        public DataSet uspGetMeasurementByPressRunID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetMeasurementByPressRunID";
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion


        #region uspGetChartsDataAvg
        public DataSet uspGetChartsDataAvg()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetChartsDataAvg";
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion


        #region uspDeleteMeasurement
        public void uspDeleteMeasurement()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspDeleteMeasurement";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion


        #region uspGetMeasurementGamut
        public DataSet uspGetMeasurementGamut()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetMeasurementGamut";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion


        #region uspGetMeasurementDotGain
        public DataSet uspGetMeasurementDotGain()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetMeasurementDotGain";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion


        #region uspGetMeasurementFile
        public DataTable uspGetMeasurementFile()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetMeasurementFile";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);

            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetChartLabData
        public DataSet uspGetChartLabData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetChartLabData";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);

            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetMeasurementData
        /// <summary>
        /// Get data for the measurement file (download if spectral is missing)
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetMeasurementData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetMeasurementData";
            Cmd.Parameters.AddWithValue("@MeasurementID", this.MeasurementID);

            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

    }
}
