using Dundas.Charting.WebControl;
using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;
using System.Data;
using System.Drawing;

namespace OrisWeb
{
    public partial class PressChartsPatchDetails : PageBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                //========== Assign Css ==========//            
                LtrlCSS.Text = @"<link href='CSS/" + Sessions.Css + "' rel='stylesheet' type='text/css' />";

                //========== Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========== Fill Form ==========//
                    LTRL_JobID.Text = Convert.ToString(Request.QueryString["JobID"]);
                    LTRL_DongleID.Text = Convert.ToString(Request.QueryString["DongleID"]);
                    LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
                    LTRL_ZoneNumber.Text = Convert.ToString(Request.QueryString["ZoneNumber"]);
                    LTRL_ZoneNumber1.Text = Convert.ToString(Request.QueryString["ZoneNumber"]);
                    if (Request.QueryString["SheetNumber"] != null)
                    {
                        LTRL_SheetNumber.Text = Convert.ToString(Request.QueryString["SheetNumber"]);
                        LTRL_ZoneNumber1.Text += " and Sheet Number " + Convert.ToString(Request.QueryString["SheetNumber"]);
                    }

                    //========== Set Key ==========//
                    LTRL_KeyJobZoneSummary.Text = LTRL_JobID.Text + "-Press-DSet-ZoneSummary1";
                    LTRL_KeyJobZoneSummaryPerSheet.Text = LTRL_JobID.Text + LTRL_SheetNumber.Text + "-Press-DSet-ZoneSummaryPerSheet";

                    //========== Clear Session ==========//
                    Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, null);
                    Sessions.SetSessions(LTRL_KeyJobZoneSummaryPerSheet.Text, null);

                    //========== FillForm ==========//
                    FillChart();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        #endregion

        #region Methods and Events
        private void FillChart()
        {
            try
            {
                //========== Gets Data If Empty ==========//
                DataSet DSet;
                if (LTRL_SheetNumber.Text == "")
                {
                    if (Sessions.GetSession(LTRL_KeyJobZoneSummary.Text) == null)
                    {
                        //========== Declaring Objects ==========//
                        PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();
                        objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                        objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                        Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, objPressPatchCalculations.uspGetPressChartZoneSelection());
                    }
                    DSet = (DataSet)Sessions.GetSession(LTRL_KeyJobZoneSummary.Text);
                }
                else
                {
                    if (Sessions.GetSession(LTRL_KeyJobZoneSummaryPerSheet.Text) == null)
                    {
                        //========== Declaring Objects ==========//
                        PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();
                        objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                        objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                        Sessions.SetSessions(LTRL_KeyJobZoneSummaryPerSheet.Text, objPressPatchCalculations.uspGetSheetChartZoneSelection(Convert.ToInt32(LTRL_SheetNumber.Text)));
                    }
                    DSet = (DataSet)Sessions.GetSession(LTRL_KeyJobZoneSummaryPerSheet.Text);
                }

                //========== Gets the Delta E ==========//
                //DSet.Tables[0].DefaultView.RowFilter = " ZoneNumber = " + LTRL_ZoneNumber.Text;
                //string _AvgDeltaE = DSet.Tables[0].DefaultView[0]["AvgDeltaE"].ToString();

                //========== Filter With Zone Number ==========//
                DSet.Tables[1].DefaultView.RowFilter = " ZoneNumber = " + LTRL_ZoneNumber.Text;
                DataView DV = DSet.Tables[1].DefaultView;

                //========== Bind The Chart ==========//
                for (int i = 0; i < DV.Count; i = i + 1)
                {
                    string _ColorID = DV[i]["ColorID"].ToString();
                    string _HTMLColor = DV[i]["HtmlColor"].ToString();
                    string _HTMLColorName = DV[i]["ColorName"].ToString();
                    string _HTMLColorNameValue = DV[i]["ColorName1"].ToString();
                    string _DeltaE = DV[i]["DeltaE"].ToString();


                    CHRT_ZoneDetails.Series[1].Points.AddXY(_HTMLColorNameValue, _DeltaE);



                    CHRT_ZoneDetails.Series[1].Points[i].Color = ColorTranslator.FromHtml(_HTMLColor);
                    CHRT_ZoneDetails.Series[1].Points[i].MapAreaAttributes = "onclick = \"SetValue( '" + i.ToString() + ";" + LTRL_JobID.Text + ";" + LTRL_CPDeviceID.Text + ";" + LTRL_ZoneNumber.Text + ";" + _ColorID + ";" + LTRL_DongleID.Text + "' );\"";
                    CHRT_ZoneDetails.Series[1].Points[i].Href = "#";
                    CHRT_ZoneDetails.Series[1].Points[i].ToolTip = "Zone Number: " + LTRL_ZoneNumber.Text + ", Color: " + _HTMLColorName + ", Delta E: " + _DeltaE;

                    string[] split = null;
                    if (HDChart.Value.Trim() != "")
                    {
                        split = HDChart.Value.Split(';');
                        if (split[0].ToString() == i.ToString())
                        {
                            CHRT_ZoneDetails.Series[1].Points[i].BorderWidth = 5;
                            CHRT_ZoneDetails.Series[1].Points[i].BorderColor = Color.Blue;
                            CHRT_ZoneDetails.Series[1].Points[i].BorderStyle = ChartDashStyle.Solid;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        protected void BtnChart_Click(object sender, EventArgs e)
        {
            try
            {
                FillChart();

                string[] split = null;
                if (HDChart.Value.Trim() != "")
                {
                    split = HDChart.Value.Split(';');
                    FRM1.Style.Add("display", "block");
                    if (Request.QueryString["SheetNumber"] == null)
                        FRM1.Attributes["src"] = "PressChartsPatchDetails1.aspx?JobID=" + split[1].ToString() + "&DongleID=" + split[5].ToString() + "&CPDeviceID=" + split[2].ToString() + "&ZoneNumber=" + split[3].ToString() + "&ColorID=" + split[4].ToString();
                    else
                        FRM1.Attributes["src"] = "PressChartsPatchDetails1.aspx?JobID=" + split[1].ToString() + "&DongleID=" + split[5].ToString() + "&CPDeviceID=" + split[2].ToString() + "&ZoneNumber=" + split[3].ToString() + "&ColorID=" + split[4].ToString() + "&SheetNumber=" + Convert.ToString(Request.QueryString["SheetNumber"]);
                    //FRM1.Attributes["src"] = "PressChartsPatchDetails.aspx?JobID=" + split[1].ToString() + "&DongleID=" + split[4].ToString() + "&CPDeviceID=" + split[2].ToString() + "&ZoneNumber=" + split[3].ToString();
                }
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
    }
}