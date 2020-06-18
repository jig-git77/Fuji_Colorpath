using Dundas.Charting.WebControl;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;


namespace TaskeroWeb
{

    public partial class PressChartsNonProcess : PageBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                //========== Style Settings ==========//
                DL_Zone1.Style.Add("width", "100%");

                //========== Style Settings ==========//
                if (IsPostBack == false)
                {
                    //========== Clear Literal ==========//
                    LTRL_DIVs.Text = "";

                    //========== Fill Form ==========//
                    LTRL_JobID.Text = Request.QueryString["JobID"] == null ? "0" : Request.QueryString["JobID"].ToString();
                    LTRL_DongleID.Text = Request.QueryString["DongleID"] == null ? "0" : Request.QueryString["DongleID"].ToString();
                    LTRL_CPDeviceID.Text = Request.QueryString["CPDeviceID"] == null ? "0" : Request.QueryString["CPDeviceID"].ToString();

                    //========== Set Key ==========//
                    LTRL_KeyJobZoneSummary.Text = LTRL_JobID.Text + "-Press-DSet-ZoneSummary";

                    //========== Clear Session ==========//                
                    Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, null);
                }

                // to attach the annotation to the X and Y axis
                CHRT_ZoneDeltaE.Annotations[0].AxisX = CHRT_ZoneDeltaE.ChartAreas[0].AxisX;
                CHRT_ZoneDeltaE.Annotations[0].AxisY = CHRT_ZoneDeltaE.ChartAreas[0].AxisY;

                // set the height and width to be automatically
                CHRT_ZoneDeltaE.Annotations[0].Width = double.NaN;
                CHRT_ZoneDeltaE.Annotations[0].Height = double.NaN;
                CHRT_ZoneDeltaE.Annotations[0].X = double.NaN;
                CHRT_ZoneDeltaE.Annotations[0].Y = double.NaN;

                //========== FillForm ==========//
                if (!IsPostBack)
                    FillForm();

                //========== Click Event ==========//
                //this.CHRT_ZoneDeltaE.Click += new ImageClickEventHandler(CHRT_ZoneDeltaE_Click);
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }

        void CHRT_ZoneDeltaE_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                HitTestResult objHitTestResult = CHRT_ZoneDeltaE.HitTest(e.X, e.Y);
                if (objHitTestResult != null)
                {
                    // Update chart title with the name of the last clicked chart element
                    if (objHitTestResult.ChartElementType == ChartElementType.DataPoint)
                    {
                        // Set the point to anchor
                        CHRT_ZoneDeltaE.Annotations[0].AnchorDataPoint = CHRT_ZoneDeltaE.Series["Series1"].Points[objHitTestResult.PointIndex];

                        // Set annotation anchor
                        CHRT_ZoneDeltaE.Annotations[0].AnchorX = double.NaN;
                        CHRT_ZoneDeltaE.Annotations[0].AnchorY = double.NaN;
                    }
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void DL_Zone1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                //========== Gets The Data List ===========//
                DataList DL_Zone1_Colors = (DataList)e.Item.FindControl("DL_Zone1_Colors");
                HtmlGenericControl DIV1 = (HtmlGenericControl)e.Item.FindControl("DIV1");

                //========== Gets the ZoneNumber ===========//
                DataRowView DRV = (DataRowView)e.Item.DataItem;

                //========== Get Table[1] ===========//
                DataTable DT = ((DataSet)Sessions.GetSession(LTRL_KeyJobZoneSummary.Text)).Tables[1];

                LTRL_DIVs.Text += "<div id=\"DIV_ZoneNumber_" + DRV["ZoneNumber"].ToString() + "\" name=\"DIV_ZoneNumber_" + DRV["ZoneNumber"].ToString() + "\" class=\"CHRT_DIV_MOUSEOVER_1\" style=\"display:none;\">Zone : " + DRV["ZoneNumber"].ToString() + "<br />";

                //========== Bind Data List with Table[1] ===========//
                DT.DefaultView.RowFilter = " ZoneNumber = " + DRV["ZoneNumber"].ToString();
                DL_Zone1_Colors.RepeatColumns = DT.DefaultView.Count;
                DL_Zone1_Colors.Attributes.Add("align", "center");

                LTRL_Colors.Text = "";
                LTRL_IsEnabledImage.Text = "";

                DL_Zone1_Colors.DataSource = DT.DefaultView;
                DL_Zone1_Colors.DataBind();

                //========== Start HTML FOR A ZoneNumber ===========//
                DIV1.Attributes.Add("onmouseover", "ShowPopUpDiv( '" + DRV["ZoneNumber"].ToString() + "' , '" + LTRL_Colors.Text + "' , '" + LTRL_IsEnabledImage.Text + "' );");
                DIV1.Attributes.Add("onmouseout", "HidePopUpDiv();");
                LTRL_DIVs.Text += "</div>";

                //---------------------- Manish 14-02-2009-------------------
                if (HDChart.Value.Trim() != "" && CHRT_ZoneDeltaE.Series["Series1"].Points.Count > 0)
                {
                    string[] split = null;
                    split = HDChart.Value.Split(';');
                    if (split.Length > 0 && int.Parse(split[0]) >= 0)
                    {
                        if (CHRT_ZoneDeltaE.Series["Series1"].Points[int.Parse(split[0])].AxisLabel.Equals(DRV["ZoneNumber"].ToString()))
                        {
                            DIV1.Style.Add("border-color", "Blue");
                            DIV1.Style.Add("border-width", "5");
                            DIV1.Style.Add("border-style", "solid");
                        }
                    }
                }
                //----------------------End Manish -------------------
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void DL_Zone1_Colors_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                //========== Retrieve Data ==========//
                DataRowView DRV = (DataRowView)e.Item.DataItem;

                //========== Assign Values ==========//            
                e.Item.BackColor = Color.FromName(DRV["HTMLColor"].ToString());

                if (e.Item.ItemIndex == 0)
                { LTRL_Colors.Text = " " + DRV["HTMLColor"].ToString(); }
                else
                { LTRL_Colors.Text += "," + DRV["HTMLColor"].ToString(); }

                if (e.Item.ItemIndex == 0)
                { LTRL_IsEnabledImage.Text = DRV["IsDudOrIsEnabled"].ToString(); }
                else
                { LTRL_IsEnabledImage.Text += "," + DRV["IsDudOrIsEnabled"].ToString(); }

                Literal LTRL_Color = (Literal)e.Item.FindControl("LTRL_Color");
                LTRL_Color.Text = DRV["IsDudOrIsEnabled"].ToString();//<img src=\"images/spacer.gif\" style=\"height:10px;width:1px\" border=\"0\" alt=\"\" Title=\"\" />


                LTRL_DIVs.Text += "<div class=\"CHRT_DIV_MOUSEOVER_1_INNER_DIV\" style=\"background-color:" + DRV["HTMLColor"].ToString() + "\">" + DRV["IsDudOrIsEnabled"].ToString() + "</div>";
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

                if (DSet.Tables[0] != null)
                {
                    //========== Bind Data List with Table[0] ===========//
                    DL_Zone1.DataSource = DSet.Tables[0];
                    DL_Zone1.DataBind();

                    //========== Assign the Repeat to decrease the entries. ===========//
                    DL_Zone1.RepeatColumns = DSet.Tables[0].Rows.Count;
                }

                //========== Bind the Chart ==========//
                int _Width = (DL_Zone1.RepeatColumns * 68);
                CHRT_ZoneDeltaE.Width = Unit.Pixel(_Width);
                if (_Width < 900)
                {
                    CHRT_ZoneDeltaE.Width = Unit.Pixel(910);
                }
                CHRT_ZoneDeltaE.Series["Series1"]["PixelPointWidth"] = "25";

                if (DSet.Tables[0] != null)
                {
                    for (int i = 0; i < DSet.Tables[0].Rows.Count; i = i + 1)
                    {
                        AddPointToZoneDeltaE(i, CHRT_ZoneDeltaE.Series["Series1"], "Zone Number", DSet.Tables[0].Rows[i]["ZoneNumber"].ToString(), "Average Delta E", DSet.Tables[0].Rows[i]["AvgDeltaE"].ToString(), DSet.Tables[0].Rows[i]["ZoneColor"].ToString());

                        string[] split = null;
                        if (HDChart.Value.Trim() != "")
                        {
                            split = HDChart.Value.Split(';');
                            if (split[0].ToString() == i.ToString())
                            {
                                CHRT_ZoneDeltaE.Series["Series1"].Points[i].BorderWidth = 5;
                                CHRT_ZoneDeltaE.Series["Series1"].Points[i].BorderColor = Color.Blue;
                                CHRT_ZoneDeltaE.Series["Series1"].Points[i].BorderStyle = ChartDashStyle.Solid;
                            }
                        }

                    }
                }

                //========== Bind Data List with Table[0] ===========//
                DL_Zone1.DataSource = DSet.Tables[0];
                DL_Zone1.DataBind();

                if (DSet.Tables[0] != null)
                {
                    //========== Assign the Repeat to decrease the entries. ===========//
                    DL_Zone1.RepeatColumns = DSet.Tables[0].Rows.Count;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void AddPointToZoneDeltaE(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue, string ZoneColor)
        {
            try
            {
                _objSeries.Points.AddXY(_XValue, _YValue);
                //_objSeries.Points[_Index].MapAreaAttributes = "onmouseover = \"ShowColorDiv( '" + _XValue + "');\" onmouseout = \"ShowHideDiv( '" + _XValue + "');\" onclick = \"PageRedirect( '" + LTRL_JobID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + _XValue + "' );\"";

                //'**********Changed By Chirag on 1/23/2008
                string ColorName = null;
                switch (ZoneColor)
                {
                    case "1": //Red
                        ColorName = "Red";
                        break;
                    case "2": //Yellow
                        ColorName = "Yellow";
                        break;
                    case "3": //Green
                        ColorName = "Green";
                        break;
                }

                //_objSeries.Points[_Index].MapAreaAttributes = "onclick = \"PageRedirect( " + _Index + " , '" + LTRL_JobID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + _XValue + "' , '" + LTRL_DongleID.Text + "' );\"";
                _objSeries.Points[_Index].MapAreaAttributes = "onclick = \"SetValue( '" + _Index + ";" + LTRL_JobID.Text + ";" + LTRL_CPDeviceID.Text + ";" + _XValue + ";" + LTRL_DongleID.Text + "' );\"";
                _objSeries.Points[_Index].ToolTip = _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
                _objSeries.Points[_Index].Color = Color.FromName(ColorName);
                //_objSeries.Points[_Index].Href = "#";
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
                FillForm();

                string[] split = null;
                if (HDChart.Value.Trim() != "")
                {
                    split = HDChart.Value.Split(';');
                    FRM1.Style.Add("display", "block");
                    FRM1.Attributes["src"] = "PressChartsPatchDetails.aspx?JobID=" + split[1].ToString() + "&DongleID=" + split[4].ToString() + "&CPDeviceID=" + split[2].ToString() + "&ZoneNumber=" + split[3].ToString();
                }
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }

    }
}