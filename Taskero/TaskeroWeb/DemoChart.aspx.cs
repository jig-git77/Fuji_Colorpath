#region
using System;
using System.Data;
using Taskero.Business;
using System.Drawing;
using Dundas.Charting.WebControl;
using TaskeroWeb.Models;
using Taskero.Common;
#endregion

namespace TaskeroWeb
{
    public partial class DemoChart : PageBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                CHRT_ZoneDeltaE.AJAXZoomEnabled = true;
                CHRT_ZoneDeltaE.CallbackStateContent = CallbackStateContent.All;

                #region ChartArea0
                // scrolling and zooming will force keeping of series data between callbacks.
                CHRT_ZoneDeltaE.ChartAreas["ChartArea0"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                CHRT_ZoneDeltaE.ChartAreas["ChartArea0"].AxisY.View.MinSize = 10.0;
                CHRT_ZoneDeltaE.ChartAreas["ChartArea0"].AxisX.View.Position = 0.0;
                CHRT_ZoneDeltaE.ChartAreas["ChartArea0"].AxisX.View.Size = 50.0;
                #endregion

                #region ChartArea1
                // scrolling and zooming will force keeping of series data between callbacks.
                CHRT_ZoneDeltaE.ChartAreas["ChartArea1"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                CHRT_ZoneDeltaE.ChartAreas["ChartArea1"].AxisY.View.MinSize = 10.0;
                CHRT_ZoneDeltaE.ChartAreas["ChartArea1"].AxisX.View.Position = 0.0;
                CHRT_ZoneDeltaE.ChartAreas["ChartArea1"].AxisX.View.Size = 50.0;
                #endregion

                if (IsPostBack == false)
                {
                    //========== Fill Form ==========//
                    LTRL_JobID.Text = "63";
                    LTRL_DongleID.Text = "P0008";
                    LTRL_CPDeviceID.Text = "";

                    //========== Set Key ==========//
                    LTRL_KeyJobZoneSummary.Text = LTRL_JobID.Text + "-Press-DSet-ZoneSummary";

                    //========== Clear Session ==========//                
                    Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, null);

                    //========== FillForm ==========//
                    FillForm();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        #endregion

        #region Methods and Functions
        private void FillForm()
        {
            try
            {
                if (Sessions.GetSession(LTRL_KeyJobZoneSummary.Text) == null)
                {
                    //========== Declaring Objects ==========//
                    PressData objPress = new PressData();
                    objPress.JobID = Convert.ToInt32(LTRL_JobID.Text);
                    objPress.DongleID = LTRL_DongleID.Text;
                    Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, objPress.uspGetPressChartZoneSelectionColors());
                }
                DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyJobZoneSummary.Text);

                //========== Bind the Chart ==========//        
                for (int i = 0; i < DSet.Tables[0].Rows.Count; i = i + 1)
                {
                    DSet.Tables[1].DefaultView.RowFilter = "ZoneNumber = " + DSet.Tables[0].Rows[i]["ZoneNumber"].ToString();

                    AddPointToZoneDeltaE(i, CHRT_ZoneDeltaE.Series["Series0"], "Zone Number", DSet.Tables[0].Rows[i]["ZoneNumber"].ToString(), "Average Delta E", DSet.Tables[0].Rows[i]["AvgDeltaE"].ToString(), DSet.Tables[0].Rows[i]["Color"].ToString(), DSet.Tables[1].DefaultView.Count);

                    AddColorPoint(i + 1, Convert.ToInt32(DSet.Tables[0].Rows[i]["ZoneNumber"]), DSet.Tables[1].DefaultView);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void AddPointToZoneDeltaE(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue, string _Color, int _ColorCount)
        {
            try
            {
                _objSeries.Points.AddXY(_XValue, _YValue);
                //_objSeries.Points[_Index].MapAreaAttributes = "onmouseover = \"ShowColorDiv( '" + _XValue + "');\" onmouseout = \"ShowHideDiv( '" + _XValue + "');\" onclick = \"PageRedirect( '" + LTRL_JobID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + _XValue + "' );\"";
                _objSeries.Points[_Index].MapAreaAttributes = "onclick = \"PageRedirect( " + _Index + " , '" + LTRL_JobID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + _XValue + "' , '" + LTRL_DongleID.Text + "' );\"";
                _objSeries.Points[_Index].ToolTip = _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
                _objSeries.Points[_Index].Color = Color.FromName(_Color);
                _objSeries.Points[_Index].Href = "#";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void AddColorPoint(int _Index, int _ZoneNumber, DataView _Dv)
        {
            try
            {
                //for (int i = 0; i < _Dv.Count; i = i + 1)
                for (int i = 0; i < _Dv.Count; i = i + 1)
                {
                    string _SeriesName = "Series-" + _Index + "-" + _ZoneNumber + "-" + i;
                    string _HTMLColor = _Dv[i]["HTMLColor"].ToString();

                    Series _objSeries = new Series();
                    _objSeries.Name = _SeriesName;
                    _objSeries.BorderColor = Color.FromArgb(100, 0, 0, 0);
                    _objSeries.ChartArea = "ChartArea1";
                    _objSeries.Type = SeriesChartType.Column;
                    _objSeries["DrawingStyle"] = "Cylinder";
                    _objSeries.BorderWidth = 1;
                    _objSeries.ShowInLegend = false;
                    CHRT_ZoneDeltaE.Series.Add(_objSeries);

                    CHRT_ZoneDeltaE.Series[_SeriesName].Points.AddXY(_ZoneNumber, 1);
                    CHRT_ZoneDeltaE.Series[_SeriesName].Points[0].ToolTip = "Zone Number : " + _ZoneNumber;
                    CHRT_ZoneDeltaE.Series[_SeriesName].Points[0].MarkerStyle = MarkerStyle.Square;
                    CHRT_ZoneDeltaE.Series[_SeriesName].Points[0].MarkerSize = 15;
                    //CHRT_ZoneDeltaE.Series[_SeriesName].Points[0].MarkerColor = Color.FromName(_HTMLColor);
                    //CHRT_ZoneDeltaE.Series[_SeriesName].Points[0].Color = Color.FromName(_HTMLColor);
                    CHRT_ZoneDeltaE.Series[_SeriesName].Points[0].BackGradientType = GradientType.None;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

    }

}