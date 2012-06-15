#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
#endregion

#region Others
using Fuji.Business;
using System.Drawing;
using Formulas;
using Dundas.Charting.WebControl;
#endregion


public partial class ColorCharts : PageBase
{
    #region Declarations
    int _DundasPageSize = 24;
    #endregion

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Call Script ==========//
            RBTN_ListChart.Attributes.Add("onclick", "ShowHideGridChart(this.id , '" + TR_CHRT.ClientID + "' , '" + TR_DL.ClientID + "' , '" + TR_LargeChart.ClientID + "' , '" + TR_Frame.ClientID + "');");

            //========== Grid V ==========//
            Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_deColors);
            GV_deColors.AllowPaging = false;
            //GV_deColors.AllowSorting = false;

            //========== Check And Set Labels ==========//
            if (IsPostBack == false)
            {
                ViewState["PatchOrder"] = "";
                //Error = Std
                //Caution = Strak
                //DE = Front

                //========== Set Labels ==========//        
                FillLabels();
                LTRL_KEY.Text = LTRL_CPMeasurementGUID.Text + "-Color-DSet-DTLChart"; //DS_Charts_deColors

                //========== Default Settings ==========//
                if(Request.QueryString["ListChart"] != null && Request.QueryString["ListChart"].ToString() != "")
                { RBTN_ListChart.SelectedValue = Convert.ToString(Request.QueryString["ListChart"]); }
                

                //========== Clear Session ==========//
                Fuji.Common.Sessions.SetSessions(LTRL_KEY.Text, null);

                //========== Fills Both Chart and DataList ==========//
                FillForm();
                RBTN_ListChart_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GV_deColors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //========== Default Settings for Sample ==========//
                ViewState["SampleName"] = "";
                ViewState["SampleNameCount"] = "0";
                ViewState["SampleNameFinalCount"] = "0";

                ViewState["TotalLines"] = "1";

                //========== IF there is specs for any Num of patches ==========//
                if (ViewState["NumOfPatchLines"] != null && ViewState["NumOfPatches"] != null)
                { ViewState["SampleNameFinalCount"] = Convert.ToString(Convert.ToInt32(ViewState["NumOfPatches"]) / Convert.ToInt32(ViewState["NumOfPatchLines"])); }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView DRV = (DataRowView)e.Row.DataItem;

                System.Web.UI.WebControls.Label LBL_Color = (System.Web.UI.WebControls.Label)e.Row.FindControl("LBL_Color");
                LBL_Color.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                LBL_Color.Style.Add("background-color", Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt16(DRV["LAB_L1"]), Convert.ToInt16(DRV["LAB_A1"]), Convert.ToInt16(DRV["LAB_B1"])));

                System.Web.UI.WebControls.Literal LTRL_PatchName = (System.Web.UI.WebControls.Literal)e.Row.FindControl("LTRL_PatchName");

                LTRL_PatchName.Text = DRV["Sample_Name"].ToString();

                #region Breaking Lines
                if (DDL_Sort.SelectedIndex == 0)
                {
                    //========== Convert To Variables ==========//
                    string _SampleName = ViewState["SampleName"].ToString();
                    int _SampleNameCount = Convert.ToInt32(ViewState["SampleNameCount"].ToString());
                    int _SampleNameFinalCount = Convert.ToInt32(ViewState["SampleNameFinalCount"].ToString());
                    int _TotalLines = Convert.ToInt32(ViewState["TotalLines"].ToString());

                    //========== Increase the counter ==========//
                    _SampleNameCount = _SampleNameCount + 1;

                    //========== Gets the Break Point ==========//
                    if (_SampleName == "") { _SampleName = LTRL_PatchName.Text.Substring(0, 1); }
                    if (_SampleNameFinalCount == 0)
                    {
                        if (_SampleName != LTRL_PatchName.Text.Substring(0, 1))
                        {
                            _SampleName = LTRL_PatchName.Text.Substring(0, 1);
                            _SampleNameFinalCount = _SampleNameCount;
                        }
                    }

                    //========== Gets the Break Point ==========//
                    if (ViewState["NumOfPatchLines"] != null && ViewState["NumOfPatches"] != null)
                    {
                        if (_SampleNameFinalCount < _SampleNameCount)
                        {
                            _SampleNameCount = 1;
                            _SampleName = "";
                            LTRL_LargeChart.Text += "<div style=\"clear:both\"></div>";
                            _TotalLines = _TotalLines + 1;

                        }
                    }
                    else
                    {
                        if (_SampleNameFinalCount == _SampleNameCount)
                        {
                            _SampleNameCount = 1;
                            _SampleName = "";
                            LTRL_LargeChart.Text += "<div style=\"clear:both\"></div>";
                            _TotalLines = _TotalLines + 1;
                        }
                    }

                    //========== Assign Variables to the viewstate again ==========//
                    ViewState["SampleName"] = _SampleName;
                    ViewState["SampleNameCount"] = _SampleNameCount.ToString();
                    ViewState["SampleNameFinalCount"] = _SampleNameFinalCount.ToString();
                    ViewState["TotalLines"] = _TotalLines.ToString();
                }
                #endregion

                string BGColor = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt16(DRV["LAB_L1"]), Convert.ToInt16(DRV["LAB_A1"]), Convert.ToInt16(DRV["LAB_B1"]));
                LTRL_LargeChart.Text += "<DIV style=\"background-color:" + BGColor + "\" class=\"CHRT_LARGE_DIV\" onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + DRV["CPPatchDataLabID"].ToString() + "' , 'C' , 1 , '" + DRV["DE"].ToString() + "' );\" title=\"Sample : " + DRV["Sample_Name"].ToString() + ", DE : " + DRV["DE"].ToString() + ", C : " + DRV["CMYK_C"].ToString() + ", M : " + DRV["CMYK_M"].ToString() + ", Y : " + DRV["CMYK_Y"].ToString() + ", K : " + DRV["CMYK_K"].ToString() + "\">&nbsp;</div>";
            }
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    //========== Default Settings for Sample ==========//
            //    //ViewState["SampleName"] = null;
            //    //ViewState["SampleNameCount"] = null;
            //    //ViewState["SampleNameFinalCount"] = null;
            //}
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }

    }
    protected void RBTN_ListChart_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Fill ==========//
            FillGridView();
            FilldeColorCharts();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GV_deColors_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //============ Arrange by order ==========//
            DataSet DS = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KEY.Text);

            //========== Change Sort Expression ==========//
            DDL_Sort.SelectedValue = e.SortExpression;
            if (ViewState["PatchOrder"].ToString() == "")
            { ViewState["PatchOrder"] = "Desc"; DDL_Order.SelectedIndex = 1; }
            else
            { ViewState["PatchOrder"] = ""; DDL_Order.SelectedIndex = 0; }
            DS.Tables[0].DefaultView.Sort = e.SortExpression + " " + ViewState["PatchOrder"].ToString();

            //=========== Clear Before Refilling ==========//
            LTRL_LargeChart.Text = "";

            //=========== Bind Grid ==========//
            GV_deColors.DataSource = DS.Tables[0].DefaultView;
            GV_deColors.DataBind();

            //========== Fills De Colors Charts ==========//
            FilldeColorCharts();

            //========== Show/Hide ==========//
            TR_DL.Style.Add("display", "");
            TR_CHRT.Style.Add("display", "none");
            TR_Frame.Style.Add("display", "none");
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_Sort_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Fills De Colors Charts ==========//
            FilldeColorCharts();

            //========== Sort the Data Set ==========//
            DataSet DS = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KEY.Text);

            DS.Tables[0].DefaultView.Sort = DDL_Sort.SelectedValue + " " + DDL_Order.SelectedValue;

            //=========== Clear Before Refilling ==========//
            LTRL_LargeChart.Text = "";

            //=========== Bind Grid ==========//
            GV_deColors.DataSource = DS.Tables[0].DefaultView;
            GV_deColors.DataBind();

            //========== Set the state back to normal ==========//
            ViewState["PatchOrder"] = DDL_Order.SelectedValue;

            //========== Show/Hide ==========//
            TR_CHRT.Style.Add("display", "none");
            TR_DL.Style.Add("display", "none");
            TR_LargeChart.Style.Add("display", "");
            TR_Frame.Style.Add("display", "");
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Methods and Functions
    private void FillLabels()
    {
        try
        {
            LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
            LTRL_CPDevicePrintTargetID.Text = Convert.ToString(Request.QueryString["CPDevicePrintTargetID"]);
            LTRL_CPMeasurementGUID.Text = Convert.ToString(Request.QueryString["CPMeasurementGUID"]);
            LTRL_CPPatchDataLabID.Text = Convert.ToString(Request.QueryString["CPPatchDataLabID"]); 
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillForm()
    {
        try
        {
            //========== Get The Measurements ==========//        
            if (Fuji.Common.Sessions.GetSession(LTRL_KEY.Text) == null)
            {
                CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
                objCPPatchDataLab.MeasurementGUID = LTRL_CPMeasurementGUID.Text;
                Fuji.Common.Sessions.SetSessions(LTRL_KEY.Text, objCPPatchDataLab.uspGetDTLChartsdeColorsOnGUID());
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FilldeColorCharts()
    {
        try
        {
            //=========== Fill DataSet in Session ==========//
            DataSet DS = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KEY.Text);

            //========== Convert To Variables ==========//
            int _SampleNameCount = ((DS.Tables[0] == null) ? 0 : (DS.Tables[0].Rows.Count));
            int _SampleNameFinalCount = Convert.ToInt32(ViewState["SampleNameFinalCount"].ToString());
            if (_SampleNameFinalCount == 0) { _SampleNameFinalCount = _SampleNameCount; }

            int _TotalLines = Convert.ToInt32(ViewState["TotalLines"].ToString());

            //=========== Plot Default Chart Values ==========//
            //-----------
            ChartTmp.Series[0].Points.AddXY("1", "1");
            ChartTmp.Visible = false;
            //-----------
            CHRT_deColors.Series[0].Points.AddXY("1", "1");
            CHRT_deColors.Visible = false;

            CHRT_deColors_2.Series[0].Points.AddXY("1", "1");
            CHRT_deColors_2.Visible = false;

            CHRT_deColors_3.Series[0].Points.AddXY("1", "1");
            CHRT_deColors_3.Visible = false;

            CHRT_deColors_4.Series[0].Points.AddXY("1", "1");
            CHRT_deColors_4.Visible = false;

            //=========== Bind Chart ==========//
            if (DS != null && DS.Tables[0] != null && DS.Tables[0].Rows.Count > 0 && DS.Tables[0].Rows.Count < 201 && _TotalLines <= 4)
            {
                //=========== Variable to get the no of points per chart ==========//
                if (_SampleNameCount != _SampleNameFinalCount)
                { _SampleNameFinalCount = _SampleNameFinalCount; }
                int _CountsPerChartMod = _SampleNameCount % _TotalLines;

                //=========== Chart1 ==========//
                int _StartIndex = 0;
                int _EndIndex = 0;
                if (_CountsPerChartMod == 0)
                {
                    _EndIndex = (_StartIndex + _SampleNameFinalCount);
                }
                else
                {
                    _EndIndex = (_StartIndex + _CountsPerChartMod);
                }
                if (_TotalLines >= 1)
                {
                    BindChart(CHRT_deColors, DS.Tables[0], _StartIndex, _EndIndex);
                }

                //=========== ChartTmp ==========// Testing for vertical chart display
                if (_TotalLines >= 1)
                {
                    BindBarChart(ChartTmp, DS.Tables[0], _StartIndex, _EndIndex);
                }
                //=============================//

                //=========== Chart2 ==========//
                _StartIndex = _EndIndex;
                if (_CountsPerChartMod == 0) { _EndIndex = (_StartIndex + _SampleNameFinalCount); } else { _EndIndex = (_StartIndex + _CountsPerChartMod); }
                if (_TotalLines >= 2) { BindChart(CHRT_deColors_2, DS.Tables[0], _StartIndex, _EndIndex); }

                //=========== Chart3 ==========//
                _StartIndex = _EndIndex;
                if (_CountsPerChartMod == 0) { _EndIndex = (_StartIndex + _SampleNameFinalCount); } else { _EndIndex = (_StartIndex + _CountsPerChartMod); }
                if (_TotalLines >= 3) { BindChart(CHRT_deColors_3, DS.Tables[0], _StartIndex, _EndIndex); }

                //=========== Chart4 ==========//
                _StartIndex = _EndIndex;
                if (_CountsPerChartMod == 0) { _EndIndex = (_StartIndex + _SampleNameFinalCount); } else { _EndIndex = (_StartIndex + _CountsPerChartMod); }
                if (_TotalLines >= 4) { BindChart(CHRT_deColors_4, DS.Tables[0], _StartIndex, _EndIndex); }
            }
            else
            {
                if (IsPostBack == false)
                {
                    RBTN_ListChart.Items.RemoveAt(0);
                    RBTN_ListChart.SelectedIndex = 0;
                }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void BindBarChart(Chart CHRT, DataTable DT, int _StartIndex, int _EndIndex)
    {
        try
        {
            //========== Clear Points and Show Chart ==========//
            CHRT.Visible = true;
            CHRT.Series[0].Points.Clear();
            CHRT.Series[1].Points.Clear();
            //CHRT.Series[2].Points.Clear();
            //CHRT.Series[3].Points.Clear();

            #region //========== Declarations ==========//
            string Sample_Name;
            double DE;
            double Caution;
            double Error;
            string HexColor1;
            int R1 = 0;
            int G1 = 0;
            int B1 = 0;
            int CPPatchDataLabID = 0;
            int PointIndex = 0;
            string ChartData = "";


            Series objSeries0 = (Series)CHRT.Series[0];
            objSeries0.XValueType = ChartValueTypes.Double;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Bar;

            objSeries0.MarkerStyle = MarkerStyle.Circle;
            objSeries0.MarkerColor = Color.Red;
            objSeries0.MarkerSize = 10;            
                        

            Series objSeries1 = (Series)CHRT.Series[1];
            objSeries1.XValueType = ChartValueTypes.Double;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;            
            objSeries1.Type = SeriesChartType.StackedBar;

            //Series objSeries2 = (Series)CHRT.Series[2];
            //objSeries2.XValueType = ChartValueTypes.Double;
            //objSeries2.YValueType = ChartValueTypes.String;
            //objSeries2.ShowInLegend = false;
            //objSeries2.Type = SeriesChartType.Bar;

            //objSeries2.MarkerStyle = MarkerStyle.Circle;
            //objSeries2.MarkerColor = Color.Red;
            //objSeries2.MarkerSize = 10;


            //Series objSeries3 = (Series)CHRT.Series[3];
            //objSeries3.XValueType = ChartValueTypes.Double;
            //objSeries3.YValueType = ChartValueTypes.String;
            //objSeries3.ShowInLegend = false;
            //objSeries3.Type = SeriesChartType.Bar;
            #endregion

            CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

            CHRT.ChartAreas["ChartArea1"].Position.Auto = true;
            CHRT.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            CHRT.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
            CHRT.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            CHRT.ChartAreas["ChartArea1"].Position.Height = float.Parse("91");

            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("1");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("91");
            

            #region " === Fill Series === "
            //========== Loop and add values ==========//

            if (_EndIndex > 7)
                _EndIndex = 7;

           // ChartData = "<table cellpadding='0' cellspacing='0' border='1' width='100%><tr style='height: 44px'><td>&nbsp;</td><td valign='middle' align='center'>Actual</td><td valign='middle' align='center'>Threshold</td></tr>";

            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {

                //========== Get the series ==========//
                Sample_Name = DT.Rows[i]["Sample_Name"].ToString();
                DE = Convert.ToDouble(DT.Rows[i]["DE"].ToString());
                Caution = Convert.ToDouble(DT.Rows[i]["Strict"].ToString());
                Error = Convert.ToDouble(DT.Rows[i]["limit"].ToString());
                CPPatchDataLabID = Convert.ToInt32(DT.Rows[i]["CPPatchDataLabID"].ToString());

                //Caution = 10 ;
                //Error = 1.8;

                if (Caution > Error)
                {
                    double Temp = Caution;
                    Caution = Error;
                    Error = Temp;
                }

                HexColor1 = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt32(DT.Rows[i]["LAB_L1"]), Convert.ToInt32(DT.Rows[i]["LAB_A1"]), Convert.ToInt32(DT.Rows[i]["LAB_B1"])).Replace("#", "");
                R1 = Convert.ToInt16(HexColor1.Substring(0, 2), 16);
                G1 = Convert.ToInt16(HexColor1.Substring(2, 2), 16);
                B1 = Convert.ToInt16(HexColor1.Substring(4, 2), 16);

                objSeries0.Points.AddXY(Sample_Name, Error); //DE
                objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                objSeries0.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;


                //CustomLabel c = new CustomLabel((PointIndex - 1), (PointIndex  + 1), "hello", 1, LabelMark.None);
                CustomLabel c = new CustomLabel();
               
                c.To = ((PointIndex + 1));
                c.From = ((PointIndex + 1) + 0.5);

                
                string strText = "";
                //strText += setString(Error.ToString(), 6);
                //strText += "  ";
                //strText += setString(Sample_Name, 12);
                //strText += "  ";
                //strText += setString(Error.ToString(), 6);
                c.Text = strText;
                
                //c.Image = "~/Images/MarkerRed_Circle.gif";
                CHRT.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(c);


                //objSeries0.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + DT.Rows[Start]["CPPatchDataLabID"].ToString() + "' , 'C' , 1 , '" + DE + "' );\"";
                //objSeries0.Points[PointIndex].Href = "#";

                objSeries1.Points.AddXY(Sample_Name, DE);
                objSeries1.Points[PointIndex].Color = Color.FromArgb(255, R1, G1, B1);
                objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries1.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;
                objSeries1.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + CPPatchDataLabID + "' , 'C' , 1 , '" + DE + "'  );\"";
                objSeries1.Points[PointIndex].Href = "#";

                //objSeries2.Points.AddXY(Sample_Name, Error);//Caution
                //objSeries2.Points[PointIndex].Color = Color.Transparent;
                //objSeries2.Points[PointIndex]["PointWidth"] = "0.4";
                //objSeries2.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;
                //objSeries2.Points[0].Label
                //objSeries2.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + DT.Rows[Start]["CPPatchDataLabID"].ToString() + "' , 'C' , 1 , '" + DE + "'  );\"";
                //objSeries2.Points[PointIndex].Href = "#";

                if (Caution == Error && Caution > 1)
                {
                    //objSeries3.Points.AddXY(Sample_Name, Caution - 1);
                }
                else
                {
                    //objSeries3.Points.AddXY(Sample_Name, Caution);
                }
                //objSeries3.Points[PointIndex].Color = Color.Transparent;
                //objSeries3.Points[PointIndex]["PointWidth"] = "0.4";
                //objSeries3.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;
                ////objSeries3.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + DT.Rows[Start]["CPPatchDataLabID"].ToString() + "' , 'C' , 1 , '" + DE + "'  );\"";
                ////objSeries3.Points[PointIndex].Href = "#";

                if (Error <= 0.0)
                {
                    objSeries0.Points[PointIndex].Empty = true;
//                    objSeries2.Points[PointIndex].Empty = true;
                }

                if (Caution <= 0.0)
                {
                    //objSeries3.Points[PointIndex].Empty = true;
                }
                //========== Increase Point Index ==========//
                PointIndex = PointIndex + 1;

                ChartData += "<tr style='height: 33px'><td valign='middle'>data</td><td valign='middle'>data</td><td valign='middle'>data</td></tr>";

            }
            //ChartData += "</table>";
            lblReportData.Text = ChartData;
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private string setString(string strActual,int len)
    {
        try
        {
            string strRet = "";
            int strLen = strActual.Length;
            int diff = len - strLen;
            int leftSpace = diff / 2;
            int rightSpace = diff - leftSpace;

            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                    strRet = "  " + strRet;

                strRet = strRet + strActual;

                //for (int i = 0; i < rightSpace; i++)
                //    strRet = strRet + "    ";
            }
            else
            {
                strRet = strActual;
            }
            return strRet;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void BindChart(Chart CHRT, DataTable DT, int _StartIndex, int _EndIndex)
    {
        try
        {
            //========== Clear Points and Show Chart ==========//
            CHRT.Visible = true;
            CHRT.Series[0].Points.Clear();
            CHRT.Series[1].Points.Clear();
            CHRT.Series[2].Points.Clear();
            CHRT.Series[3].Points.Clear();


            #region //========== Declarations ==========//
            string Sample_Name;
            double DE;
            double Caution;
            double Error;
            string HexColor1;
            int R1 = 0;
            int G1 = 0;
            int B1 = 0;
            int CPPatchDataLabID = 0;
            int PointIndex = 0;

            Series objSeries0 = (Series)CHRT.Series[0];
            objSeries0.XValueType = ChartValueTypes.Double;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Column;

            Series objSeries1 = (Series)CHRT.Series[1];
            objSeries1.XValueType = ChartValueTypes.Double;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.StackedColumn;

            Series objSeries2 = (Series)CHRT.Series[2];
            objSeries2.XValueType = ChartValueTypes.Double;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Point;

            Series objSeries3 = (Series)CHRT.Series[3];
            objSeries3.XValueType = ChartValueTypes.Double;
            objSeries3.YValueType = ChartValueTypes.String;
            objSeries3.ShowInLegend = false;
            objSeries3.Type = SeriesChartType.Point;

            CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
            #endregion


            #region " === Fill Series === "
            //========== Loop and add values ==========//
            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {

                //========== Get the series ==========//
                Sample_Name = DT.Rows[i]["Sample_Name"].ToString();
                DE = Convert.ToDouble(DT.Rows[i]["DE"].ToString());
                Caution = Convert.ToDouble(DT.Rows[i]["Strict"].ToString());
                Error = Convert.ToDouble(DT.Rows[i]["limit"].ToString());
                CPPatchDataLabID = Convert.ToInt32(DT.Rows[i]["CPPatchDataLabID"].ToString());

                //Caution = 10 ;
                //Error = 1.8;

                if (Caution > Error)
                {
                    double Temp = Caution;
                    Caution = Error;
                    Error = Temp;
                }

                HexColor1 = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt32(DT.Rows[i]["LAB_L1"]), Convert.ToInt32(DT.Rows[i]["LAB_A1"]), Convert.ToInt32(DT.Rows[i]["LAB_B1"])).Replace("#", "");
                R1 = Convert.ToInt16(HexColor1.Substring(0, 2), 16);
                G1 = Convert.ToInt16(HexColor1.Substring(2, 2), 16);
                B1 = Convert.ToInt16(HexColor1.Substring(4, 2), 16);

                objSeries0.Points.AddXY(Sample_Name, Error); //DE
                objSeries0.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries0.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;
                //objSeries0.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + DT.Rows[Start]["CPPatchDataLabID"].ToString() + "' , 'C' , 1 , '" + DE + "' );\"";
                //objSeries0.Points[PointIndex].Href = "#";

                objSeries1.Points.AddXY(Sample_Name, DE);
                objSeries1.Points[PointIndex].Color = Color.FromArgb(255, R1, G1, B1);
                objSeries1.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries1.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;
                objSeries1.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + CPPatchDataLabID + "' , 'C' , 1 , '" + DE + "'  );\"";
                objSeries1.Points[PointIndex].Href = "#";

                objSeries2.Points.AddXY(Sample_Name, Error);//Caution
                objSeries2.Points[PointIndex].Color = Color.Transparent;
                objSeries2.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries2.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;
                //objSeries2.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + DT.Rows[Start]["CPPatchDataLabID"].ToString() + "' , 'C' , 1 , '" + DE + "'  );\"";
                //objSeries2.Points[PointIndex].Href = "#";

                if (Caution == Error && Caution > 1)
                {
                    objSeries3.Points.AddXY(Sample_Name, Caution - 1);
                }
                else
                {
                    objSeries3.Points.AddXY(Sample_Name, Caution);
                }
                objSeries3.Points[PointIndex].Color = Color.Transparent;
                objSeries3.Points[PointIndex]["PointWidth"] = "0.4";
                objSeries3.Points[PointIndex].ToolTip = " Sample : " + Sample_Name + ", DE : " + DE + ", Caution : " + Caution + ", Error : " + Error;
                //objSeries3.Points[PointIndex].MapAreaAttributes = "onclick = \"javascript:PageRedirect( '" + LTRL_CPMeasurementGUID.Text + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' , '" + DT.Rows[Start]["CPPatchDataLabID"].ToString() + "' , 'C' , 1 , '" + DE + "'  );\"";
                //objSeries3.Points[PointIndex].Href = "#";

                if (Error <= 0.0)
                {
                    objSeries0.Points[PointIndex].Empty = true;
                    objSeries2.Points[PointIndex].Empty = true;
                }

                if (Caution <= 0.0)
                {
                    objSeries3.Points[PointIndex].Empty = true;
                }

                //========== Increase Point Index ==========//
                PointIndex = PointIndex + 1;
            }
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillGridView()
    {
        try
        {
            //=========== Fill Datatable in Session ==========//
            DataSet DS = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KEY.Text);

            //=========== Clear Before Refilling ==========//
            LTRL_LargeChart.Text = "";

            //=========== Bind DG ==========//
            if (DS != null && DS.Tables[1] != null && DS.Tables[1].Rows.Count > 0)
            {
                if (DS.Tables[1].Rows[0]["NumOfPatchLines"].ToString() != "1")
                {
                    ViewState["NumOfPatchLines"] = DS.Tables[1].Rows[0]["NumOfPatchLines"].ToString();
                    ViewState["NumOfPatches"] = DS.Tables[1].Rows[0]["NumOfPatches"].ToString();
                }
            }

            //=========== Bind DG ==========//
            GV_deColors.DataSource = DS.Tables[0];
            GV_deColors.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}