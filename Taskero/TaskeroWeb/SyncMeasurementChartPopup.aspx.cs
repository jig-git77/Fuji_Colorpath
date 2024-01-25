using Dundas.Charting.WebControl;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI.HtmlControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;


namespace TaskeroWeb
{
    public partial class SyncMeasurementChartPopup : PageBase
    {
        #region Declaration
        #endregion

        #region Page Load and Events
        /// <summary>
        /// Page load event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Type"].ToString() == "Gamut")
                    {
                        TblVisibleTrueFalse(TblGamut);
                        fillABComperison(Convert.ToInt32(Request.QueryString["MID"]), Convert.ToInt32(Request.QueryString["PressRunID"]));

                    }
                    else
                    {
                        TblVisibleTrueFalse(TblDotGain);
                        fillDotgainChart(Convert.ToInt32(Request.QueryString["MID"]), Convert.ToInt32(Request.QueryString["PressRunID"]));

                    }
                }
            }
            catch (Exception exc)
            {
                LblError.Text = exc.Message;
            }
        }
        #endregion

        #region Methods
        private void fillABComperison(Int32 MeasurementID, Int32 PressRunID)
        {
            try
            {
                #region Fill data table from database
                // ===== Create Object of CPSyncMeasurementData  ======//
                CPSyncMeasurementData objCPSyncMeasurementData = new CPSyncMeasurementData();


                // ===== Get Data from data base  ==================//
                objCPSyncMeasurementData.PressRunID = PressRunID;
                objCPSyncMeasurementData.MeasurementID = MeasurementID;
                DataSet DS = objCPSyncMeasurementData.uspGetMeasurementGamut();
                #endregion


                DataView DV = new DataView();
                if (DS != null)
                {
                    #region Measurement LAB Value
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtGamut = new DataTable();
                        dtGamut = DS.Tables[0];
                        int i = 0;

                        #region Cyan
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '1')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 0;
                            CHART_ABComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[0].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[1].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[1].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[0].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[1].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[1].Points[i].BorderWidth = 2;
                        }
                        #endregion

                        #region Green
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '6')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 1;
                            CHART_ABComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[0].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[1].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[1].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[0].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[1].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[1].Points[i].BorderWidth = 2;
                        }
                        #endregion

                        #region Yellow
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '3')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 2;
                            CHART_ABComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[0].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[1].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[1].Points[i].BorderWidth = 2;


                            CHART_LComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[0].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[1].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[1].Points[i].BorderWidth = 2;
                        }
                        #endregion

                        #region Red
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '5')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 3;
                            CHART_ABComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[0].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[1].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[1].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[0].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[1].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[1].Points[i].BorderWidth = 2;
                        }
                        #endregion

                        #region Magenta
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '2')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 4;
                            CHART_ABComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[0].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[1].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[1].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[0].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[1].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[1].Points[i].BorderWidth = 2;
                        }
                        #endregion

                        #region Blue
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '7')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 5;
                            CHART_ABComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[0].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[1].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[1].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[0].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[1].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[1].Points[i].BorderWidth = 2;
                        }
                        #endregion

                        #region Other
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '1')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 6;
                            CHART_ABComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[0].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[1].Type = SeriesChartType.Line;
                            CHART_ABComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[1].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[0].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[0].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[0].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[1].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[1].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[1].Points[i].BorderWidth = 2;
                        }
                        #endregion

                        #region Meddile Point
                        //dtGamut.DefaultView.RowFilter = "(PatchTypeID = '9')";
                        dtGamut.DefaultView.RowFilter = "(PatchTypeID = " + Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() + ")";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            i = 0;
                            CHART_ABComparison.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["Targeta*"]), Convert.ToDouble(DV[0]["Targetb*"]));
                            CHART_ABComparison.Series[2].Type = SeriesChartType.Point;
                            CHART_ABComparison.Series[2].Points[i].BorderWidth = 2;

                            CHART_ABComparison.Series[3].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["CIELAB_b*"]));
                            CHART_ABComparison.Series[3].Type = SeriesChartType.Point;
                            CHART_ABComparison.Series[3].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[3].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_ABComparison.Series[3].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(0));
                            CHART_LComparison.Series[2].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[2].Points[i].BorderWidth = 2;

                            CHART_LComparison.Series[3].Points.AddXY(Convert.ToDouble(DV[0]["CIELAB_a*"]), Convert.ToDouble(DV[0]["L*Deviation"]));
                            CHART_LComparison.Series[3].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[3].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[3].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[0]["HTMLColor"]));
                            CHART_LComparison.Series[3].Points[i].BorderWidth = 2;
                        }
                        #endregion
                    }
                    #endregion

                    #region Other Measurement LAB Value
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        DataTable dtOtherGamut = new DataTable();
                        dtOtherGamut = DS.Tables[1];
                        for (int i = 0; i < dtOtherGamut.Rows.Count; i = i + 1)
                        {
                            CHART_ABComparison.Series[4].Points.AddXY(Convert.ToDouble(dtOtherGamut.Rows[i]["CIELAB_a*"]), Convert.ToDouble(dtOtherGamut.Rows[i]["CIELAB_b*"]));
                            CHART_ABComparison.Series[4].Type = SeriesChartType.Point;
                            CHART_ABComparison.Series[4].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_ABComparison.Series[4].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dtOtherGamut.Rows[i]["HTMLColor"]));
                            CHART_ABComparison.Series[4].Points[i].BorderWidth = 1;

                            CHART_LComparison.Series[4].Points.AddXY(Convert.ToDouble(dtOtherGamut.Rows[i]["CIELAB_a*"]), Convert.ToDouble(dtOtherGamut.Rows[i]["L*Deviation"]));
                            CHART_LComparison.Series[4].Type = SeriesChartType.Point;
                            CHART_LComparison.Series[4].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_LComparison.Series[4].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dtOtherGamut.Rows[i]["HTMLColor"]));
                            CHART_LComparison.Series[4].Points[i].BorderWidth = 1;
                        }

                    }
                    #endregion
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void fillDotgainChart(Int32 MeasurementID, Int32 PressRunID)
        {
            try
            {
                #region Fill data table from database
                // ===== Create Object of CPSyncMeasurementData  ======//
                CPSyncMeasurementData objCPSyncMeasurementData = new CPSyncMeasurementData();


                // ===== Get Data from data base  ==================//
                objCPSyncMeasurementData.PressRunID = PressRunID;
                objCPSyncMeasurementData.MeasurementID = MeasurementID;
                DataSet DS = objCPSyncMeasurementData.uspGetMeasurementDotGain();
                #endregion

                string _TitleValue = string.Empty;

                DataView DV = new DataView();
                if (DS != null)
                {
                    #region Measurement Dotgain Value
                    DataTable dtDotgain = new DataTable();
                    dtDotgain = DS.Tables[0];

                    #region Cyan
                    //dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (1,8,16,17,18))";
                    dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "))";
                    DV = dtDotgain.DefaultView;
                    DV.Sort = "CMYK_C";  //Changed By Chirag on 04/26
                    if (DV.Count > 0)
                    {
                        for (int i = 0; i < DV.Count; i++)
                        {
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString())
                                _TitleValue = "25%";
                            else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString())
                                _TitleValue = "50%";
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString())
                                _TitleValue = "75%";

                            AddPointChart(i, CHRT_DotgainCyan.Series["Series0"], CHRT_DotgainCyan.Series["Series0Point"], DV[i]["CMYK_C"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Cyan.Name.ToString(), true);
                            AddPointChart(i, CHRT_DotgainCyan.Series["Series1"], null, DV[i]["CMYK_C"].ToString(), DV[i]["TargetDotGain"].ToString(), "", "", Color.Green.Name.ToString(), "", false);
                        }

                    }
                    #endregion

                    #region Magenta
                    //dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (2,8,19,20,21))";                
                    dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "))";
                    DV = dtDotgain.DefaultView;
                    DV.Sort = "CMYK_M";  //Changed By Chirag on 04/26
                    if (DV.Count > 0)
                    {
                        for (int i = 0; i < DV.Count; i++)
                        {
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString())
                                _TitleValue = "25%";
                            else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString())
                                _TitleValue = "50%";
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString())
                                _TitleValue = "75%";

                            AddPointChart(i, CHRT_DotgainMeganta.Series["Series0"], CHRT_DotgainMeganta.Series["Series0Point"], DV[i]["CMYK_M"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Magenta.Name.ToString(), true);
                            AddPointChart(i, CHRT_DotgainMeganta.Series["Series1"], null, DV[i]["CMYK_M"].ToString(), DV[i]["TargetDotGain"].ToString(), "", "", Color.Green.Name.ToString(), "", false);
                        }

                    }
                    #endregion

                    #region Yellow
                    //dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (3,8,22,23,24))";                
                    dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "))";
                    DV = dtDotgain.DefaultView;
                    DV.Sort = "CMYK_Y";
                    if (DV.Count > 0)
                    {
                        for (int i = 0; i < DV.Count; i++)
                        {
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString())
                                _TitleValue = "25%";
                            else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString())
                                _TitleValue = "50%";
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString())
                                _TitleValue = "75%";

                            AddPointChart(i, CHRT_DotgainYellow.Series["Series0"], CHRT_DotgainYellow.Series["Series0Point"], DV[i]["CMYK_Y"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Yellow.Name.ToString(), true);
                            AddPointChart(i, CHRT_DotgainYellow.Series["Series1"], null, DV[i]["CMYK_Y"].ToString(), DV[i]["TargetDotGain"].ToString(), "", "", Color.Green.Name.ToString(), "", false);
                        }

                    }
                    #endregion

                    #region Black
                    //dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (4,8,25,26,27))";                
                    dtDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "))";
                    DV = dtDotgain.DefaultView;
                    DV.Sort = "CMYK_K";
                    if (DV.Count > 0)
                    {
                        for (int i = 0; i < DV.Count; i++)
                        {
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString())
                                _TitleValue = "25%";
                            else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString())
                                _TitleValue = "50%";
                            if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString())
                                _TitleValue = "75%";

                            AddPointChart(i, CHRT_DotgainBlack.Series["Series0"], CHRT_DotgainBlack.Series["Series0Point"], DV[i]["CMYK_K"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Black.Name.ToString(), true);
                            AddPointChart(i, CHRT_DotgainBlack.Series["Series1"], null, DV[i]["CMYK_K"].ToString(), DV[i]["TargetDotGain"].ToString(), "", "", Color.Green.Name.ToString(), "", false);
                        }

                    }
                    #endregion

                    #endregion


                    #region Other Measurement Dotgain Value
                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        DataTable dtOtherDotgain = new DataTable();
                        dtOtherDotgain = DS.Tables[1];

                        #region Cyan
                        //dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (16,17,18))";                    
                        dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "))";
                        DV = dtOtherDotgain.DefaultView;
                        if (DV.Count > 0)
                            for (int i = 0; i < DV.Count; i++)
                            {
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString())
                                    _TitleValue = "25%";
                                else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString())
                                    _TitleValue = "50%";
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString())
                                    _TitleValue = "75%";

                                AddOtherPointChart(i, CHRT_DotgainCyan.Series["Series2"], DV[i]["CMYK_C"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Cyan.Name.ToString());
                            }


                        #endregion

                        #region Magenta
                        //dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (19,20,21))";                    
                        dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "))";
                        DV = dtOtherDotgain.DefaultView;
                        if (DV.Count > 0)
                            for (int i = 0; i < DV.Count; i++)
                            {
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString())
                                    _TitleValue = "25%";
                                else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString())
                                    _TitleValue = "50%";
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString())
                                    _TitleValue = "75%";

                                AddOtherPointChart(i, CHRT_DotgainMeganta.Series["Series2"], DV[i]["CMYK_M"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Magenta.Name.ToString());
                            }


                        #endregion

                        #region Yellow
                        //dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (22,23,24))";                    
                        dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "))";
                        DV = dtOtherDotgain.DefaultView;
                        if (DV.Count > 0)
                            for (int i = 0; i < DV.Count; i++)
                            {
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString())
                                    _TitleValue = "25%";
                                else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString())
                                    _TitleValue = "50%";
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString())
                                    _TitleValue = "75%";

                                AddOtherPointChart(i, CHRT_DotgainYellow.Series["Series2"], DV[i]["CMYK_Y"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Yellow.Name.ToString());
                            }

                        #endregion

                        #region Black
                        //dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (25,26,27))";                    
                        dtOtherDotgain.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "))";
                        DV = dtOtherDotgain.DefaultView;
                        if (DV.Count > 0)
                            for (int i = 0; i < DV.Count; i++)
                            {
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString())
                                    _TitleValue = "25%";
                                else if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString())
                                    _TitleValue = "50%";
                                if (DV[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString())
                                    _TitleValue = "75%";

                                AddOtherPointChart(i, CHRT_DotgainBlack.Series["Series2"], DV[i]["CMYK_K"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["TargetDotgain"].ToString(), _TitleValue, DV[i]["HTMLColor"].ToString(), Color.Black.Name.ToString());
                            }

                        #endregion
                    }
                    #endregion
                }



            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }


        private void AddOtherPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, string _Target, string _TitleValue, string HTMLColor, string ColorName)
        {
            try
            {
                _objSeries.Points.AddXY(CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
                _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
                if (CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)) > 0)
                {
                    #region Add Tooltip HTML
                    StringBuilder toolTip = new StringBuilder();
                    toolTip.Append("<table>");
                    toolTip.Append("<tr><td><b>" + ColorName + " " + _TitleValue + " Patch Metrics</b></td></tr>");
                    toolTip.Append("<tr><td><table>");
                    toolTip.Append("<tr><td>Actual TVI</td><td>");
                    toolTip.Append(_YValue + "%");
                    toolTip.Append("</td></tr>");
                    toolTip.Append("<tr><td>Target TVI</td><td>");
                    toolTip.Append(_Target + "%");
                    toolTip.Append("</td></tr>");
                    toolTip.Append("</table></td></tr>");
                    toolTip.Append("</table>");
                    #endregion
                    _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void AddPointChart(int _Index, Series _objSeries, Series _objSeriesPoint, string _XValue, string _YValue, string _Target, string _TitleValue, string HTMLColor, string ColorName, bool isTooltip)
        {
            try
            {
                if (isTooltip)
                {
                    _objSeries.Type = SeriesChartType.Spline;
                    _objSeries.Points.AddXY(CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
                    _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
                    _objSeries.Points[_Index].BorderWidth = 1;


                    _objSeriesPoint.Type = SeriesChartType.Point;
                    _objSeriesPoint.Points.AddXY(CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
                    _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
                    _objSeriesPoint.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                    _objSeriesPoint.Points[_Index].MarkerSize = 8;
                    _objSeriesPoint.Points[_Index].BorderWidth = 1;
                    if (CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)) > 0)
                    {
                        #region Add Tooltip HTML
                        StringBuilder toolTip = new StringBuilder();
                        toolTip.Append("<table>");
                        toolTip.Append("<tr><td><b>" + ColorName + " " + _TitleValue + " Patch Metrics</b></td></tr>");
                        toolTip.Append("<tr><td><table>");
                        toolTip.Append("<tr><td>Actual TVI</td><td>");
                        toolTip.Append(_YValue + "%");
                        toolTip.Append("</td></tr>");
                        toolTip.Append("<tr><td>Target TVI</td><td>");
                        toolTip.Append(_Target + "%");
                        toolTip.Append("</td></tr>");
                        toolTip.Append("</table></td></tr>");
                        toolTip.Append("</table>");
                        #endregion
                        _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                    }
                }
                else
                {
                    _objSeries.Type = SeriesChartType.Spline;
                    _objSeries.Points.AddXY(CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
                    _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
                    _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                    _objSeries.Points[_Index].MarkerSize = 8;
                    _objSeries.Points[_Index].BorderWidth = 1;
                }
                //_objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
                //_objSeries.Points[_Index].ToolTip = "(" + CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + ")";

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void TblVisibleTrueFalse(HtmlTable HT)
        {
            try
            {
                TblGamut.Visible = false;
                TblDotGain.Visible = false;

                HT.Visible = true;

            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }
        #endregion
    }

}