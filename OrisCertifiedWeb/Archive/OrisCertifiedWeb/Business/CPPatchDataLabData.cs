#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public class CPPatchDataLabData : CPPatchDataLab
    {        
        #region uspGetDTLTrendAveDeByMeasurementGUID
        /// <summary>
        /// Purpose : This retrieves the Average Delta E & Density for Trend Ave De Chart using @MeasurementGUID FOR Cyan, Magenta, Yellow & Black.
        /// Instruction : @MeasurementGUID should be in below given format.
        /// 28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
        /// </summary>  
        /// <returns>1 : Color</returns>
        /// <returns>2 : MeasurementGUID</returns>
        /// <returns>3 : AVE_DE</returns>
        /// <returns>4 : Density</returns>
        /// <returns>5 : Created_Time</returns>
        public DataSet uspGetDTLTrendAveDeByMeasurementGUID()
        {
            //========== Gets the DTL Trend Ave De ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLTrendAveDeByMeasurementGUID";            
            Cmd.Parameters.AddWithValue("@MeasurementGUID" , this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetDTLTrendAveDeByMeasurementGUID1
        /// <summary>
        /// Purpose : This retrieves the Average Delta E & Density for Trend Ave De Chart using @MeasurementGUID FOR Cyan, Magenta, Yellow & Black. Aslo this is an exted version for uspGetDTLTrendAveDeByMeasurementGUID.
        /// Instruction : @MeasurementGUID should be in below given format.
        /// 28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
        /// </summary>  
        public DataTable uspGetDTLTrendAveDeByMeasurementGUID1()
        {
            //========== Gets the DTL Trend Ave De ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLTrendAveDeByMeasurementGUID1";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAveMaxStdDevOnGUID
        /// <summary>
        /// Gets the Aver Density, Maximum Density, Standerd Deviation Density.
        /// </summary>
        /// <returns>returns the standard deviation data</returns>
        public DataTable uspGetAveMaxStdDevOnGUID()
        {
            //========== Gets the DTL Trend Ave De ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAveMaxStdDevOnGUID";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDTLChartsdeColorsOnGUID
        /// <summary>
        /// Gets the pathes on the basis of GUID.
        /// </summary>
        /// <returns>returns the charts colors data</returns>
        public DataSet uspGetDTLChartsdeColorsOnGUID()
        {
            //========== Gets the DTL Trend Ave De ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLChartsdeColorsOnGUID";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetDTLChartsPatchDetailsOnCPPatchDataLabID
        /// <summary>
        /// Retrieves data on CPPatchDataLabID for patch data.
        /// </summary>
        /// <returns>returns the charts patch details data</returns>
        public DataTable uspGetDTLChartsPatchDetailsOnCPPatchDataLabID()
        {
            //========== Gets the DTL Trend Ave De ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLChartsPatchDetailsOnCPPatchDataLabID";
            Cmd.Parameters.AddWithValue("@CPPatchDataLabID", this.CPPatchDataLabID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDTLTrendPrintContrast
        /// <summary>
        /// Purpose : This retrieves the print contrast using @MeasurementGUID FOR Cyan, Magenta, Yellow & Black.
        /// Instruction : @MeasurementGUID should be in below given format.
        /// 28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6,28849A78-113E-11DD-89C5-000A958F3FC6
        /// </summary> 
        /// <returns>returns the trend print contrast data</returns>
        public DataSet uspGetDTLTrendPrintContrast()
        {
            //========== Gets the DTL Trend Ave De ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLTrendPrintContrast";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetColorCPVMeasurement
        /// <summary>
        /// Get All the Report Types
        /// </summary>
        /// <returns>returns the color measurement data</returns>
        public DataSet uspGetColorCPVMeasurement()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetColorCPVMeasurement";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetPlateCPVMeasurement
        /// <summary>
        /// Get All the Report Types
        /// </summary>
        /// <returns>returns the plate measurement data</returns>
        public DataSet uspGetPlateCPVMeasurement()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetPlateCPVMeasurement";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetDTLDetailsOnGUID
        /// <summary>
        /// Gets the data for DTLDetails.aspx
        /// </summary>
        /// <returns>returns the DTL details data</returns>
        public DataSet uspGetDTLDetailsOnGUID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLDetailsOnGUID";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetDTLColorChartsByGUID
        /// <summary>
        /// Used to get the DTL Colours Charts by GUID
        /// </summary>
        /// <returns>returns the DTL Colours charts data</returns>
        public DataSet uspGetDTLColorChartsByGUID()
        {
            //========== Gets the DTL Color Charts  ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLColorChartsByGUID";            
            Cmd.Parameters.AddWithValue("@MeasurementGUID" , this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }

        #endregion

        #region uspGetColorCharDotGain
        /// <summary>
        /// Purpose : This retrieves the Dotgain.                
        /// </summary>  
        public DataSet uspGetColorCharDotGain()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetColorCharDotGain";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetDTLTrendTempRH
        /// <summary>
        /// Gets the temperature, RH data
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetDTLTrendTempRH()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetTempRHTrend";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

    }    
}
