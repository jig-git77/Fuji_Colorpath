using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Taskero.DataAccess;

namespace TaskeroWeb.Models.Classes
{
    [DataContract]
    public class ReferenceChart
    {
        #region Properties
        private int _ChartTypeID = 0;
        [DataMember(Order = 1)]
        public int ChartTypeID
        {
            get { return _ChartTypeID; }
            set { _ChartTypeID = value; }
        }

        private string _ChartName;
        [DataMember(Order = 2)]
        public string ChartName
        {
            get { return _ChartName; }
            set { _ChartName = value; }
        }

        private int _TotalPatch = 0;
        [DataMember(Order = 3)]
        public int TotalPatch
        {
            get { return _TotalPatch; }
            set { _TotalPatch = value; }
        }

        //Number of Rows in colorbar (wedge) i.e. also called LGOROWLENGTH in printing industry
        private int _NumOfRows = 0;
        [DataMember(Order = 4)]
        public int PatchRows
        {
            get { return _NumOfRows; }
            set { _NumOfRows = value; }
        }

        private int _NumOfColumns = 0;
        [DataMember(Order = 5)]
        public int NumOfColumns
        {
            get { return _NumOfColumns; }
            set { _NumOfColumns = value; }
        }

        /// <summary>
        /// List of Reference Patch
        /// </summary>
        /// <remarks></remarks>
        private IList<ReferencePatch> _ReferencePatch;
        [DataMember(Order = 6)]
        public IList<ReferencePatch> ReferencePatch
        {
            get { return _ReferencePatch; }
            set { _ReferencePatch = value; }
        }

        private DeviceSetting _DeviceSetting;
        [DataMember(Order = 7)]
        public DeviceSetting DeviceSetting
        {
            get { return _DeviceSetting; }
            set { _DeviceSetting = value; }
        }

        private string _RowNames = String.Empty;
        [DataMember(Order = 8)]
        public string RowNames
        {
            get { return _RowNames; }
            set { _RowNames = value; }
        }

        private ChartSetting _ChartSettings;
        [DataMember(EmitDefaultValue = false, Order = 9)]
        public ChartSetting ChartSettings
        {
            get { return _ChartSettings; }
            set { _ChartSettings = value; }
        }

        private Int16 _InstrumentTypeID;
        [DataMember(Order = 10)]
        public Int16 InstrumentTypeID
        {
            get { return _InstrumentTypeID; }
            set { _InstrumentTypeID = value; }
        }

        private int _ProjectID = 0;
        [DataMember(Order = 11)]
        public int ProjectID
        {
            get { return _ProjectID; }
            set { _ProjectID = value; }
        }

        private string _activationKey = string.Empty;
        [DataMember(Order = 12)]
        public string ActivationKey
        {
            get { return _activationKey; }
            set { _activationKey = value; }
        }
        #endregion

        public void GetChartData()
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "CPSync.uspGetReferenceChart"
                };
                cmd.Parameters.AddWithValue("@ProjectID", this._ProjectID);

                var ds = (new Database()).GetDataSet(cmd);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this._ChartTypeID = Convert.ToInt32(ds.Tables[0].Rows[0]["ChartTypeID"]);
                    this._ChartName = Convert.ToString(ds.Tables[0].Rows[0]["ChartName"]);
                    this._TotalPatch = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalPatches"]);
                    this._NumOfRows = Convert.ToInt32(ds.Tables[0].Rows[0]["NumOfRows"]);
                    this.NumOfColumns = Convert.ToInt32(ds.Tables[0].Rows[0]["NumOfCols"]);
                    this._InstrumentTypeID = Convert.ToInt16(ds.Tables[0].Rows[0]["InstrumentTypeID"]);
                    var actKey = Convert.ToString(ds.Tables[0].Rows[0]["MeasureDeviceSrNo"]);
                    //Apply security on device serial no.
                    using (var md5Hash = MD5.Create())
                    {
                        this._activationKey = GetMd5Hash(md5Hash, DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + actKey);
                    }

                    this._DeviceSetting = new DeviceSetting();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            var samplePatch = new ReferencePatch
                            {
                                SampleName = Convert.ToString(ds.Tables[1].Rows[i]["SampleName"]),
                                CMYK_C = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_C"]),
                                CMYK_M = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_M"]),
                                CMYK_Y = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_Y"]),
                                CMYK_K = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_K"]),
                                PatchTypeID = Convert.ToInt32(ds.Tables[1].Rows[i]["PatchTypeID"]),
                                HTMLColor = Convert.ToString(ds.Tables[1].Rows[i]["HTMLColor"])
                            };
                            if (this._ReferencePatch == null) { this._ReferencePatch = new List<ReferencePatch> { }; };
                            this._ReferencePatch.Add(samplePatch);
                        }
                    }

                    //If Isis or Techkon then load chart settings
                    if (this._InstrumentTypeID > 1)
                    {
                        var objCmd = new SqlCommand
                        {
                            CommandType = CommandType.Text,
                            CommandText = "SELECT ChartSetting FROM CPSync.ChartSetting WHERE referencecharttypeid = @ChartTypeID"
                        };

                        objCmd.Parameters.AddWithValue("@ChartTypeID", this._ChartTypeID);

                        var reader = (new Database()).GetXmlReader(objCmd);
                        var objDS = new DataSet();
                        var objChartSetting = new ChartSetting();
                        objDS.ReadXml(reader);

                        if (objDS.Tables.Count > 0)
                        {
                            if (objDS.Tables[0].Columns.Contains("ChartSizeX") == true) objChartSetting.ChartSizeX = Convert.ToDecimal(objDS.Tables[0].Rows[0]["ChartSizeX"]);
                            if (objDS.Tables[0].Columns.Contains("ChartSizeY") == true) objChartSetting.ChartSizeY = Convert.ToDecimal(objDS.Tables[0].Rows[0]["ChartSizeY"]);
                            if (objDS.Tables[0].Columns.Contains("BarSizeX") == true) objChartSetting.BarSizeX = Convert.ToDecimal(objDS.Tables[0].Rows[0]["BarSizeX"]);
                            if (objDS.Tables[0].Columns.Contains("BarSizeY") == true) objChartSetting.BarSizeY = Convert.ToDecimal(objDS.Tables[0].Rows[0]["BarSizeY"]);
                            if (objDS.Tables[0].Columns.Contains("MarkerDistance") == true) objChartSetting.MarkerDistance = Convert.ToDecimal(objDS.Tables[0].Rows[0]["MarkerDistance"]);
                            if (objDS.Tables[0].Columns.Contains("PatchSizeX") == true) objChartSetting.PatchSizeX = Convert.ToDecimal(objDS.Tables[0].Rows[0]["PatchSizeX"]);
                            if (objDS.Tables[0].Columns.Contains("PatchSizeY") == true) objChartSetting.PatchSizeY = Convert.ToDecimal(objDS.Tables[0].Rows[0]["PatchSizeY"]);
                            if (objDS.Tables[0].Columns.Contains("DistToFirstLineY") == true) objChartSetting.DistToFirstLineY = Convert.ToDecimal(objDS.Tables[0].Rows[0]["DistToFirstLineY"]);
                            if (objDS.Tables[0].Columns.Contains("DistToFirstField") == true) objChartSetting.DistToFirstField = Convert.ToDecimal(objDS.Tables[0].Rows[0]["DistToFirstField"]);
                            if (objDS.Tables[0].Columns.Contains("DistToBar") == true) objChartSetting.DistToBar = Convert.ToDecimal(objDS.Tables[0].Rows[0]["DistToBar"]);

                        }
                        this._ChartSettings = objChartSetting;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}