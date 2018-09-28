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
using System.Drawing;
using Dundas.Charting.WebControl;
using Fuji.Common;
using Fuji.Business;
#endregion

public partial class PlateTrend : PageBase
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Fill From Request Querystring ==========//
                FillLabels();

                //========== Set Keys ==========//
                LTRL_KeyDeviation.Text = LTRL_CPMeasurementGUID.Text + "-Plate-Deviation";

                //========== Clear the Sessions ==========//
                Fuji.Common.Sessions.SetSessions(LTRL_KeyDeviation.Text, null);

                //========== Check the form is for device or Job ==========//
                if (Request.QueryString["ReportType"] == null)
                { DDL_PrintTarget.Visible = false; LTRL_PrintTargetTitle.Visible = false; }
                else
                { DDL_PrintTarget.Visible = true; LTRL_PrintTargetTitle.Visible = true; FillPrintTarget(); }


                //========== Fill Charts ==========//
                FillDT();
                FillChartDeviation();

                //========== Plot Legend At Bottom ==========//            
                CHRT_Deviation.Legends[0].Docking = LegendDocking.Bottom;
                CHRT_Deviation.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }

    }
    protected void CHK_Deviation_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Fills All Charts ==========//
            FillChartDeviation();
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void DDL_PrintTarget_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LTRL_CPDevicePrintTargetID.Text = DDL_PrintTarget.SelectedValue;
            Fuji.Common.Sessions.SetSessions(LTRL_KeyDeviation.Text, null);
            FillDT();
            //FillChartDeviation();

            //========== Gets the querystring and Redirects to that page ==========//
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyDeviation.Text);
            if (DT.Rows.Count > 0)
            {
                string _QueryString = "CPMeasurementGUID=" + DT.Rows[DT.Rows.Count - 1]["MeasurementGUID"].ToString();
                _QueryString += "&CPDevicePrintTargetID=" + LTRL_CPDevicePrintTargetID.Text;
                _QueryString += "&CPDeviceID=" + LTRL_CPDeviceID.Text;
                if (Request.QueryString["ModalDialog"] != null) { _QueryString += "&ModalDialog=Y"; }
                if (Request.QueryString["ReportType"] != null) { _QueryString += "&ReportType=D"; }
                Fuji.Common.CommonFunctions.PageRedirect("PlateTrend", _QueryString);
            }
            else
            {
                LblError.Text = "No Measurement found for Print Target : " + DDL_PrintTarget.SelectedItem.Text + ".";
                DDL_PrintTarget.SelectedIndex = 0;
                DDL_PrintTarget_OnSelectedIndexChanged(null, null);
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    #endregion

    #region FillCharts & FillForms
    private void FillDT()
    {
        try
        {
            //========== Get The Delta E Data ==========//
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyDeviation.Text) == null)
            {
                CPPatchDataPlateData objPatchDataPlate = new CPPatchDataPlateData();
                if (DDL_PrintTarget.Visible == false || DDL_PrintTarget.SelectedIndex == 0)
                {
                    if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                    { Fuji.Common.Sessions.SetSessions(LTRL_KeyDeviation.Text, objPatchDataPlate.uspGetDeviationByCPDeviceID_Admin(Convert.ToInt32(LTRL_CPDeviceID.Text))); }
                    else
                    { Fuji.Common.Sessions.SetSessions(LTRL_KeyDeviation.Text, objPatchDataPlate.uspGetDeviationByCPDeviceID(Convert.ToInt32(LTRL_CPDeviceID.Text), Fuji.Common.Sessions.UserID)); }
                }
                else
                {
                    if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                    { Fuji.Common.Sessions.SetSessions(LTRL_KeyDeviation.Text, objPatchDataPlate.uspGetDeviationByCPDevicePrintTargetID_Admin(Convert.ToInt32(LTRL_CPDevicePrintTargetID.Text))); }
                    else
                    { Fuji.Common.Sessions.SetSessions(LTRL_KeyDeviation.Text, objPatchDataPlate.uspGetDeviationByCPDevicePrintTargetID(Convert.ToInt32(LTRL_CPDevicePrintTargetID.Text), Fuji.Common.Sessions.UserID)); }
                }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillPrintTarget()
    {
        try
        {
            //========== Fill Print Target ==========//
            CPDevicePrintTargetData objCPDevicePrintTarget = new CPDevicePrintTargetData();
            objCPDevicePrintTarget.DeviceID = Convert.ToInt32(LTRL_CPDeviceID.Text);
            DataTable DT = objCPDevicePrintTarget.uspGetPrintTargetByDeviceID();

            //========== Fill DDL ==========//
            DDL_PrintTarget.DataSource = DT;
            DDL_PrintTarget.DataTextField = "PrintTarget";
            DDL_PrintTarget.DataValueField = "CPDevicePrintTargetID";
            DDL_PrintTarget.DataBind();

            //========== Default ==========//
            DDL_PrintTarget.Items.Insert(0, (new ListItem("All", "0")));
            DDL_PrintTarget.SelectedIndex = 0;

            //========== Select From the List ==========//
            DDL_PrintTarget.SelectedValue = LTRL_CPDevicePrintTargetID.Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillLabels()
    {
        try
        {
            LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
            LTRL_CPDevicePrintTargetID.Text = Convert.ToString(Request.QueryString["CPDevicePrintTargetID"]);
            LTRL_CPMeasurementGUID.Text = Convert.ToString(Request.QueryString["CPMeasurementGUID"]);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillChartDeviation()
    {
        try
        {
            //========== Get The DataTable ==========//
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyDeviation.Text);

            #region Paging
            #region Paging Variables
            //========== Settings for paging ==========//
            int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
            int _CurrentPageIndex = Convert.ToInt32(LTRL_CurrentPage.Text);
            int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }
            int _StartIndex = 1;
            int _EndIndex = 0;
            int _CPMOnPage = 0;
            #endregion

            #region Fill DDL only On Page Load
            if (IsPostBack == false)
            {
                //========== Filling the DropDown ==========//
                FillDDLGoToPage(_PageCount);

                //========== Setting On the Page ==========//
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    if (LTRL_CPMeasurementGUID.Text == DT.Rows[i]["MeasurementGUID"].ToString())
                    {
                        _CPMOnPage = i / _PageSize;
                        break;
                    }
                }
                DDL_GoToPage.SelectedIndex = _CPMOnPage;

            }
            _CurrentPageIndex = Convert.ToInt32(DDL_GoToPage.SelectedValue);
            #endregion

            #region Settings for Paging
            _CurrentPageIndex = Convert.ToInt32(DDL_GoToPage.SelectedValue);

            if (_CurrentPageIndex == 0) { _StartIndex = 0; } else { _StartIndex = (_CurrentPageIndex * _PageSize); }

            _EndIndex = _StartIndex + _PageSize;

            if (DT.Rows.Count < _EndIndex) { _EndIndex = DT.Rows.Count; }

            //========== Show Hide Settings and Setting Label Values ==========//        
            IBTN_FirstPage.Enabled = true; IBTN_FirstPage.ImageUrl = "Images/Nav_FirstPage.gif";
            IBTN_PreviousPage.Enabled = true; IBTN_PreviousPage.ImageUrl = "Images/Nav_PreviousPage.gif";

            IBTN_NextPage.Enabled = true; IBTN_NextPage.ImageUrl = "Images/Nav_NextPage.gif";
            IBTN_LastPage.Enabled = true; IBTN_LastPage.ImageUrl = "Images/Nav_LastPage.gif";

            //=========== Don't Show if Current Page = 0 =========//
            if (_CurrentPageIndex == 0)
            {
                IBTN_FirstPage.Enabled = false;
                IBTN_PreviousPage.Enabled = false;

                IBTN_FirstPage.ImageUrl = "Images/Nav_FirstPage_off.gif";
                IBTN_PreviousPage.ImageUrl = "Images/Nav_PreviousPage_off.gif";
            }
            //=========== Don't Show if Current Page = Last Page =========//
            if (_CurrentPageIndex == (_PageCount - 1))
            {
                IBTN_NextPage.Enabled = false;
                IBTN_LastPage.Enabled = false;

                IBTN_NextPage.ImageUrl = "Images/Nav_NextPage_Off.gif";
                IBTN_LastPage.ImageUrl = "Images/Nav_LastPage_Off.gif";
            }

            //=========== Don't Show if PageCount = 0 =========//
            if (_PageCount == 0)
            {

                IBTN_FirstPage.Enabled = false;
                IBTN_PreviousPage.Enabled = false;

                IBTN_NextPage.Enabled = false;
                IBTN_LastPage.Enabled = false;

                IBTN_FirstPage.ImageUrl = "Images/Nav_FirstPage_off.gif";
                IBTN_PreviousPage.ImageUrl = "Images/Nav_PreviousPage_off.gif";

                IBTN_NextPage.ImageUrl = "Images/Nav_NextPage_Off.gif";
                IBTN_LastPage.ImageUrl = "Images/Nav_LastPage_Off.gif";

            }

            //========== Set Values Back To Label ==========//
            LTRL_StartIndex.Text = _StartIndex.ToString();
            LTRL_CurrentPage.Text = _CurrentPageIndex.ToString();
            LTRL_StartRecord.Text = Convert.ToString(_StartIndex + 1);
            LTRL_EndRecord.Text = _EndIndex.ToString();
            LTRL_TotalRec.Text = DT.Rows.Count.ToString();
            #endregion
            #endregion

            LTRL_CPMeasurementGUIDs.Text = "";
            LTRL_StatuSs.Text = "";

            #region Deviation of 25,50,75,95

            //========== Sets The Series properties ==========//
            Series objSeries1 = (Series)CHRT_Deviation.Series["Series1"];// 25%
            Series objSeries2 = (Series)CHRT_Deviation.Series["Series2"];// 50%
            Series objSeries3 = (Series)CHRT_Deviation.Series["Series3"];// 75%
            Series objSeries4 = (Series)CHRT_Deviation.Series["Series4"];// 95%

            AssignSeriesSettings(objSeries1);// 25%
            AssignSeriesSettings(objSeries2);// 50%
            AssignSeriesSettings(objSeries3);// 75%
            AssignSeriesSettings(objSeries4);// 95%

            if (CHK_Deviation_25.Checked == false) { objSeries1.Enabled = false; }
            if (CHK_Deviation_50.Checked == false) { objSeries2.Enabled = false; }
            if (CHK_Deviation_75.Checked == false) { objSeries3.Enabled = false; }
            if (CHK_Deviation_95.Checked == false) { objSeries4.Enabled = false; }

            //========== For showing the index on the page ==========//
            int j = 0;
            for (int i = _StartIndex; i < _EndIndex; i = i + 1)// Loops to get all the rows for a table
            {
                //========== 25 ==========//
                PlotADataPoint(objSeries1, j, "25", DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Deviation_25"].ToString(), DT.Rows[i]["CREATED_TIME"].ToString(), Convert.ToInt32(DT.Rows[i]["Status"]));
                //========== 50 ==========//
                PlotADataPoint(objSeries2, j, "50", DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Deviation_50"].ToString(), DT.Rows[i]["CREATED_TIME"].ToString(), Convert.ToInt32(DT.Rows[i]["Status"]));
                //========== 75 ==========//
                PlotADataPoint(objSeries3, j, "75", DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Deviation_75"].ToString(), DT.Rows[i]["CREATED_TIME"].ToString(), Convert.ToInt32(DT.Rows[i]["Status"]));
                //========== 95 ==========//
                PlotADataPoint(objSeries4, j, "95", DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Deviation_95"].ToString(), DT.Rows[i]["CREATED_TIME"].ToString(), Convert.ToInt32(DT.Rows[i]["Status"]));

                //========== Incriment the Index ==========//
                j = j + 1;
            }

            #endregion

            #region Add A Blank Entry
            if (DT.Rows.Count == 0)
            { AddBlankRow(CHRT_Deviation); }
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Change Marker Color
    /// <summary>
    /// Give the green or red marker to the selected meaurement
    /// </summary>
    /// <param name="_Index"></param>
    /// <param name="_GUID"></param>
    /// <param name="_objS"></param>
    private void ChangeMarkerColor(int _Index, string _GUID, Series _objS, int _Status)
    {
        try
        {
            _objS.Points[_Index].Href = "#";

            //========== Default ==========//
            {
                _objS.Points[_Index].MarkerSize = 4;
                _objS.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                _objS.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            }

            //========== Show The Red Marker for search criteria ==========//
            if (_Status == 0)
            {
                _objS.Points[_Index].MarkerSize = 4;
                _objS.Points[_Index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                _objS.Points[_Index].MarkerImage = "Images/MarkerRed.gif";
            }
            else if (_Status == 1) //changed by chiarg on 06/05/2009
            {
                _objS.Points[_Index].MarkerSize = 4;
                _objS.Points[_Index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                _objS.Points[_Index].MarkerImage = "Images/MarkerYellow.gif";


            }


            //========== Show The Greed Marker for the one clicked ==========//
            if (_GUID == LTRL_CPMeasurementGUID.Text)
            {
                if (_Status == 0)
                { _objS.Points[_Index].MarkerImage = "Images/MarkerRed_Circle.gif"; }
                else if (_Status == 1)
                { _objS.Points[_Index].MarkerImage = "Images/MarkerYellow_Circle.gif"; }
                else
                { _objS.Points[_Index].MarkerImage = "Images/MarkerGreen.gif"; }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Add Blank Entry
    private void AddBlankRow(Chart objChart)
    {
        try
        {
            //========== Add Blank Entry ==========//
            Series objSeries0 = (Series)objChart.Series["Series0"];
            objSeries0.Type = SeriesChartType.Spline;
            objSeries0.XValueType = ChartValueTypes.DateTime;
            objSeries0.YValueType = ChartValueTypes.Double;

            objSeries0.Points.AddXY(System.DateTime.Now.ToString(), "1");
            objSeries0.Points.AddXY(System.DateTime.Now.AddDays(1).ToString(), "1");
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Assign Series Settings
    private void AssignSeriesSettings(Series objSeries)
    {
        try
        {
            objSeries.Type = SeriesChartType.Spline;
            objSeries.XValueType = ChartValueTypes.DateTime;
            objSeries.YValueType = ChartValueTypes.Double;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region PlotADataPoint
    /// <summary>
    /// Receives and plots the datapoint
    /// </summary>
    private void PlotADataPoint(Series objSeries, int _Index, string _Title, string _GUID, string _Deviation, string _CreatedTime, int _Status)
    {
        try
        {
            //========== Plots a point ==========//
            objSeries.Points.AddXY(_CreatedTime, _Deviation);

            //========== Add The Tooltip ==========//
            objSeries.Points[_Index].ToolTip = "Deviation " + _Title + "%=" + _Deviation + ", Created Date : " + _CreatedTime + ", GUID : " + _GUID;

            //========== Add The Click Event ==========//
            objSeries.Points[_Index].MapAreaAttributes = "style=\"cursor:pointer;\" OnClick=\"javascript:PageRedirect( '" + _GUID + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\"";

            //========== Change the color ==========//
            ChangeMarkerColor(_Index, _GUID, objSeries, _Status);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Search Bar Events and Methods
    protected void DDL_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Alter the page size ==========//
            LTRL_PageSize.Text = DDL_PageSize.SelectedValue;

            //========== Gets The Data Table ==========//
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyDeviation.Text);
            int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
            int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }

            //========== Filling the DropDown ==========//
            DDL_GoToPage.Items.Clear();
            FillDDLGoToPage(_PageCount);

            //========== Fill Chart Deviation ==========//
            FillChartDeviation();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DDL_GoToPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Directly to any page ==========//
            FillChartDeviation();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void IBTN_FirstPage_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Go To Page 1 ==========//
            DDL_GoToPage.SelectedIndex = 0;
            FillChartDeviation();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void IBTN_PreviousPage_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Go To previous page ==========//
            DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) - 1);
            FillChartDeviation();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void IBTN_NextPage_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Go To next page ==========//
            DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) + 1);
            FillChartDeviation();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void IBTN_LastPage_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Go To last page ==========//
            DDL_GoToPage.SelectedIndex = DDL_GoToPage.Items.Count - 1;
            FillChartDeviation();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    private void FillDDLGoToPage(int _Count)
    {
        try
        {
            //========== Fills the Paging =========//
            for (int i = 0; i < _Count; i = i + 1)
            {
                DDL_GoToPage.Items.Insert(i, (new ListItem((i + 1).ToString(), i.ToString())));
            }

            //========== Add A Def =========//
            if (_Count == 0) { DDL_GoToPage.Items.Insert(0, (new ListItem("--", "0"))); }

            //========== Sets Default =========//        
            DDL_GoToPage.SelectedIndex = 0;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
