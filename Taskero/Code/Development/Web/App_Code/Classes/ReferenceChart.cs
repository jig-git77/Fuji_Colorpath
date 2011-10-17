using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel.Web;
using Fuji.DataAccess;


namespace MeasurementPlugInSvc
{
    [DataContract]
    public class ReferenceChart
    {

        //database id of the chart being measured
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

        private string _MeasureDeviceSrNo = string.Empty;
        [DataMember(Order = 12)]
        public string MeasureDeviceSrNo
        {
            get { return _MeasureDeviceSrNo; }
            set { _MeasureDeviceSrNo = value; }
        }



        public void GetChartData()
        {

            //ReferenceChart SampleChart = new ReferenceChart();

            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "CPSync.uspGetReferenceChart";

                //Cmd.Parameters.AddWithValue("@ChartTypeID", this._ChartTypeID);
                Cmd.Parameters.AddWithValue("@ProjectID", this._ProjectID);

                DataSet ds = (new Fuji.DataAccess.Database()).GetDataSet(Cmd);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    this._ChartTypeID = Convert.ToInt32(ds.Tables[0].Rows[0]["ChartTypeID"]);
                    this._ChartName = Convert.ToString(ds.Tables[0].Rows[0]["ChartName"]);
                    this._TotalPatch = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalPatches"]);
                    this._NumOfRows = Convert.ToInt32(ds.Tables[0].Rows[0]["NumOfRows"]);
                    this.NumOfColumns = Convert.ToInt32(ds.Tables[0].Rows[0]["NumOfCols"]);
                    this._InstrumentTypeID = Convert.ToInt16(ds.Tables[0].Rows[0]["InstrumentTypeID"]);
                    this._MeasureDeviceSrNo = Convert.ToString(ds.Tables[0].Rows[0]["MeasureDeviceSrNo"]);


                    this._DeviceSetting = new DeviceSetting();


                    //if (ds.Tables[0].Rows[0]["RowNames"] != DBNull.Value) this._RowNames = ds.Tables[0].Rows[0]["RowNames"].ToString();


                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            ReferencePatch SamplePatch = new ReferencePatch();
                            SamplePatch.SampleName = Convert.ToString(ds.Tables[1].Rows[i]["SampleName"]);
                            SamplePatch.CMYK_C = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_C"]);
                            SamplePatch.CMYK_M = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_M"]);
                            SamplePatch.CMYK_Y = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_Y"]);
                            SamplePatch.CMYK_K = Convert.ToDecimal(ds.Tables[1].Rows[i]["CMYK_K"]);
                            SamplePatch.PatchTypeID = Convert.ToInt32(ds.Tables[1].Rows[i]["PatchTypeID"]);
                            SamplePatch.HTMLColor = Convert.ToString(ds.Tables[1].Rows[i]["HTMLColor"]);
                            // SamplePatch.TargetLAB = new CIELab(Convert.ToDouble(ds.Tables[1].Rows[i]["Target_LAB_L"]), Convert.ToDouble(ds.Tables[1].Rows[i]["Target_LAB_a"]), Convert.ToDouble(ds.Tables[1].Rows[i]["Target_LAB_b"]));

                            if (this._ReferencePatch == null) { this._ReferencePatch = new List<ReferencePatch> { }; };
                            this._ReferencePatch.Add(SamplePatch);



                        }
                    }

                    //If Isis or Techkon then load chart settings
                    if (this._InstrumentTypeID > 2)
                    {
                        SqlCommand objCmd = new SqlCommand();
                        objCmd.CommandType = CommandType.Text;
                        objCmd.CommandText = "SELECT ChartSetting FROM CPSync.ChartSetting WHERE referencecharttypeid = @ChartTypeID";

                        objCmd.Parameters.AddWithValue("@ChartTypeID", this._ChartTypeID);

                        System.Xml.XmlReader reader = (new Fuji.DataAccess.Database()).GetXmlReader(objCmd);
                        DataSet objDS = new DataSet();
                        ChartSetting objChartSetting = new ChartSetting();
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

                        if (objChartSetting != null) this._ChartSettings = objChartSetting;


                    }

                    // if (ds.Tables.Count >2 && ds.Tables[2].Rows.Count > 0 )
                    // {
                    //     /* load chart setting xml from SQL server and setup properties */ 
                    //      System.Xml.XmlReader reader  (ds.Tables[2].Rows[0]["ChartSetting"]


                    //}  

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

    }

}