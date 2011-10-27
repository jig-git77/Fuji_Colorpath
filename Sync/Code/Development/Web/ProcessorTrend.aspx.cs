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
using Fuji.Business;
using Dundas.Charting.WebControl;
using System.Drawing;
#endregion

public partial class ProcessorTrend : PageBase
{
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Fill Charts ==========//
                GetAllGuids();
                FillCharts();

            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }

    }
    #endregion

    #region Page Navigation Event
    protected void IBTN_NextPage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //========== Go To next page ==========//
            DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) + 1);
            GetAllGuids();
            FillCharts();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }

    }
    protected void IBTN_FirstPage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //========== Go To Page 1 ==========//
            DDL_GoToPage.SelectedIndex = 0;
            GetAllGuids();
            FillCharts();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }

    }
    protected void IBTN_PreviousPage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //========== Go To previous page ==========//
            DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) - 1);
            GetAllGuids();
            FillCharts();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void IBTN_LastPage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //========== Go To last page ==========//
            DDL_GoToPage.SelectedIndex = DDL_GoToPage.Items.Count - 1;
            GetAllGuids();
            FillCharts();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DDL_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Alter the page size ==========//
            LTRL_PageSize.Text = DDL_PageSize.SelectedValue;

            //========== Gets The Data Table ==========//            
            ProcessorData objProcessor = new ProcessorData();
            objProcessor.DongleID = int.Parse(Convert.ToString(Request.QueryString["DongleID"]));//LTRL_DongleID.Text);
            DataTable DT = objProcessor.GetTrendLine();

            
            int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
            int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }

            //========== Filling the DropDown ==========//
            DDL_GoToPage.Items.Clear();
            FillDDLGoToPage(_PageCount);
            GetAllGuids();
            FillCharts();
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
            GetAllGuids();
            FillCharts();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Methods
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
    private void FillCharts()
    {
        //========== Get The Delta E Data ==========//        
        ProcessorData objProcessor = new ProcessorData();
        objProcessor.DongleID = int.Parse(Convert.ToString(Request.QueryString["DongleID"]));//LTRL_DongleID.Text);
        objProcessor.ZProcIDs = LTRL_ZProcIDs.Text;
        DataTable DT = objProcessor.GetTrendLine1();

        #region Conductivity

        //========== Get all the series ==========//
        Series objConductivity = (Series)CHRT_TrendDeltaE.Series["Series1"];
        Series objTemperature = (Series)CHRT_TrendDeltaE.Series["Series2"];
        Series objAVG = (Series)CHRT_TrendDeltaE.Series["Series3"];

        //========== Assign Series Settings ==========//

        objConductivity.Type = SeriesChartType.Line;
        objConductivity.Color = Color.Blue;
        objConductivity.XValueType = ChartValueTypes.DateTime;
        objConductivity.YValueType = ChartValueTypes.Double;


        objTemperature.Type = SeriesChartType.Line;
        objTemperature.Color = Color.BlueViolet;
        objTemperature.XValueType = ChartValueTypes.DateTime;
        objTemperature.YValueType = ChartValueTypes.Double;

        objAVG.Type = SeriesChartType.Line;
        objAVG.Color = Color.Gray;
        objAVG.XValueType = ChartValueTypes.DateTime;
        objAVG.YValueType = ChartValueTypes.Double;


        object ConductivitySum = DT.Compute("Sum(Conductivity)", "");
        Decimal Sum = Convert.ToDecimal(ConductivitySum);
        Decimal AVG = Sum / DT.Rows.Count;


        for (int i = 0; i < DT.Rows.Count; i = i + 1)
        {
            #region CHRT_Date
            CHRT_Date.Series[0].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), "0");
            #endregion

            #region TrendLine
            objConductivity.Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Conductivity"].ToString());
            //objConductivity.Points[i].MarkerSize = 4;
            //objConductivity.Points[i].MarkerStyle = MarkerStyle.Circle;
            objConductivity.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( 'ProcessorTrend.aspx?DongleID=" + Convert.ToString(Request.QueryString["DongleID"]) + "&ZProcID=" + DT.Rows[i]["ZProcID"].ToString() + "&Location=" + Convert.ToString(Request.QueryString["Location"]) + "&Device=" + Convert.ToString(Request.QueryString["Device"]) + "','" + DT.Rows[i]["ZProcID"].ToString() + "');\"";
            //objConductivity.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect('" + DT.Rows[i]["ZProcID"].ToString() + "');\"";
            objConductivity.Points[i].Href = "#";
            objConductivity.Points[i].ToolTip = "Created Date : " + DT.Rows[i]["CreatedDT"].ToString() + ", Value : " + DT.Rows[i]["Conductivity"].ToString();


            objTemperature.Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Current_Dev_Temp"].ToString());
            objTemperature.YAxisType = AxisType.Secondary;
            //objTemperature.Points[i].MarkerSize = 4;
            //objTemperature.Points[i].MarkerStyle = MarkerStyle.Circle;
            objTemperature.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( 'ProcessorTrend.aspx?DongleID=" + Convert.ToString(Request.QueryString["DongleID"]) + "&ZProcID=" + DT.Rows[i]["ZProcID"].ToString() + "&Location=" + Convert.ToString(Request.QueryString["Location"]) + "&Device=" + Convert.ToString(Request.QueryString["Device"]) + "','" + DT.Rows[i]["ZProcID"].ToString() + "');\"";
            objTemperature.Points[i].Href = "#";
            objTemperature.Points[i].ToolTip = "Created Date : " + DT.Rows[i]["CreatedDT"].ToString() + ", Value : " + DT.Rows[i]["Current_Dev_Temp"].ToString();

            objAVG.Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), AVG);

            if (Convert.ToBoolean(DT.Rows[i]["IsDevChanged"]))
            {
                objConductivity.Points[i].MarkerSize = 10;
                objConductivity.Points[i].MarkerStyle = MarkerStyle.Diamond;
                objConductivity.Points[i].MarkerColor = Color.Orange;

                objTemperature.Points[i].MarkerSize = 10;
                objTemperature.Points[i].MarkerStyle = MarkerStyle.Diamond;
                objTemperature.Points[i].MarkerColor = Color.Orange;
            }
            else
            {
                objConductivity.Points[i].MarkerSize = 4;
                objConductivity.Points[i].MarkerStyle = MarkerStyle.Circle;


                objTemperature.Points[i].MarkerSize = 4;
                objTemperature.Points[i].MarkerStyle = MarkerStyle.Circle;
            }


            if (LTRL_ZProcID.Text == DT.Rows[i]["ZProcID"].ToString())
            {
                objConductivity.Points[i].MarkerSize = 6;
                objConductivity.Points[i].MarkerStyle = MarkerStyle.Circle;
                objConductivity.Points[i].MarkerImage = "Images/MarkerGreen.gif";

                objTemperature.Points[i].MarkerSize = 6;
                objTemperature.Points[i].MarkerStyle = MarkerStyle.Circle;
                objTemperature.Points[i].MarkerImage = "Images/MarkerGreen.gif";
            }
            #endregion
        }
        #endregion
        
        #region Plates Per Period

        DT.DefaultView.RowFilter = "NumOfPlatesDiff <> 0";
        //========== Get all the series ==========//
        Series objPlatesPerPeriod = (Series)CHRT_TrendPlatePerPeriod.Series["Series1"];
        Series objPPPAVG = (Series)CHRT_TrendPlatePerPeriod.Series["Series2"];

        //========== Assign Series Settings ==========//

        objPlatesPerPeriod.Type = SeriesChartType.Line;
        objPlatesPerPeriod.Color = Color.Aqua;
        objPlatesPerPeriod.XValueType = ChartValueTypes.DateTime;
        objPlatesPerPeriod.YValueType = ChartValueTypes.Double;

        objPPPAVG.Type = SeriesChartType.Line;
        objPPPAVG.Color = Color.Gray;
        objPPPAVG.XValueType = ChartValueTypes.DateTime;
        objPPPAVG.YValueType = ChartValueTypes.Double;

        if (DT.DefaultView.Count > 0)
        {

            object PlatesPerPeriodSum = DT.Compute("Sum(NumOfPlatesDiff)", "");
            Decimal PPPSum = Convert.ToDecimal(PlatesPerPeriodSum);

            Decimal PPAVG = PPPSum / DT.DefaultView.Count;



            for (int i = 0; i < DT.DefaultView.Count; i = i + 1)
            {

                #region TrendLine
                objPlatesPerPeriod.Points.AddXY(DT.DefaultView[i]["CreatedDT"].ToString(), DT.DefaultView[i]["NumOfPlatesDiff"].ToString());
                objPlatesPerPeriod.Points[i].MarkerSize = 4;
                objPlatesPerPeriod.Points[i].MarkerStyle = MarkerStyle.Circle;
                objPlatesPerPeriod.Points[i].ToolTip = "Created Date : " + DT.DefaultView[i]["CreatedDT"].ToString() + ", Value : " + DT.DefaultView[i]["NumOfPlatesDiff"].ToString();


                objPPPAVG.Points.AddXY(DT.DefaultView[i]["CreatedDT"].ToString(), PPAVG);


                if (Convert.ToBoolean(DT.Rows[i]["IsDevChanged"]))
                {
                    objPlatesPerPeriod.Points[i].MarkerSize = 10;
                    objPlatesPerPeriod.Points[i].MarkerStyle = MarkerStyle.Diamond;
                    objPlatesPerPeriod.Points[i].MarkerColor = Color.Orange;
                }
                else
                {
                    objPlatesPerPeriod.Points[i].MarkerSize = 4;
                    objPlatesPerPeriod.Points[i].MarkerStyle = MarkerStyle.Circle;
                }
                #endregion
            }
        }

        #endregion

        ProcessorData objProcessorMD = new ProcessorData();
        objProcessorMD.DongleID = int.Parse(Convert.ToString(Request.QueryString["DongleID"]));//LTRL_DongleID.Text);
        objProcessorMD.ZProcID = int.Parse(LTRL_ZProcID.Text);
        DataSet DSMD = objProcessorMD.GetStatusDetail();

        #region Developer
        if (DSMD.Tables[1].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[1].Rows[0]["MaxDevPumpedRel"]))
        {
            GCDeveloper.LinearGauges["Developer"].Scales["Default"].Minimum = 0;
            GCDeveloper.LinearGauges["Developer"].Scales["Default"].Maximum = Math.Round(Double.Parse(DSMD.Tables[1].Rows[0]["MaxDevPumpedRel"].ToString())) + 20;
            GCDeveloper.LinearGauges["Developer"].Pointers["Default"].Value = Double.Parse(DSMD.Tables[1].Rows[0]["MaxDevPumpedRel"].ToString());
        }


        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["DevPumpedRel"]))
        {
            GCDeveloper.LinearGauges["Developer"].Ranges["Default"].EndValue = float.Parse(DSMD.Tables[0].Rows[0]["DevPumpedRel"].ToString());
            GCDeveloper.NumericIndicators["Default"].Value = float.Parse(DSMD.Tables[0].Rows[0]["DevPumpedRel"].ToString());
        }
        else
        {
            GCDeveloper.LinearGauges["Developer"].Ranges["Default"].EndValue = 0;
            GCDeveloper.NumericIndicators["Default"].Value = 0;
        }
        #endregion

        #region Plate Area
        if (DSMD.Tables[1].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[1].Rows[0]["MaxPlateAreaRel"]))
        {
            GCPlateArea.LinearGauges["PlateArea"].Scales["Default"].Minimum = 0;
            GCPlateArea.LinearGauges["PlateArea"].Scales["Default"].Maximum = Math.Round(Double.Parse(DSMD.Tables[1].Rows[0]["MaxPlateAreaRel"].ToString())) + 20;
            GCPlateArea.LinearGauges["PlateArea"].Pointers["Default"].Value = Double.Parse(DSMD.Tables[1].Rows[0]["MaxPlateAreaRel"].ToString());
        }
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["PlateAreaRel"]))
        {
            GCPlateArea.LinearGauges["PlateArea"].Ranges["Default"].EndValue = float.Parse(DSMD.Tables[0].Rows[0]["PlateAreaRel"].ToString());
            GCPlateArea.NumericIndicators["Default"].Value = float.Parse(DSMD.Tables[0].Rows[0]["PlateAreaRel"].ToString());
        }
        else
        {
            GCPlateArea.LinearGauges["PlateArea"].Ranges["Default"].EndValue = 0;
            GCPlateArea.NumericIndicators["Default"].Value = 0;
        }
        #endregion

        #region Number Of Plates
        if (DSMD.Tables[1].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[1].Rows[0]["MaxRelNumOfPlates"]))
        {

            GCNumberOfPlate.LinearGauges["NumberOfPlate"].Scales["Default"].Minimum = 0;
            GCNumberOfPlate.LinearGauges["NumberOfPlate"].Scales["Default"].Maximum = Math.Round(Double.Parse(DSMD.Tables[1].Rows[0]["MaxRelNumOfPlates"].ToString())) + 20;
            GCNumberOfPlate.LinearGauges["NumberOfPlate"].Pointers["Default"].Value = Double.Parse(DSMD.Tables[1].Rows[0]["MaxRelNumOfPlates"].ToString());
        }
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["NumOfPlatesRel"]))
        {
            GCNumberOfPlate.LinearGauges["NumberOfPlate"].Ranges["Default"].EndValue = float.Parse(DSMD.Tables[0].Rows[0]["NumOfPlatesRel"].ToString());
            GCNumberOfPlate.NumericIndicators["Default"].Value = float.Parse(DSMD.Tables[0].Rows[0]["NumOfPlatesRel"].ToString());
        }
        else
        {
            GCNumberOfPlate.LinearGauges["NumberOfPlate"].Ranges["Default"].EndValue = 0;
            GCNumberOfPlate.NumericIndicators["Default"].Value = 0;
        }
        #endregion

        #region Time Stamp
        if (DSMD.Tables[0].Rows.Count > 0)
            LBLTimeStamp.Text = Convert.ToDateTime(DSMD.Tables[0].Rows[0]["CreatedDT"]).ToString("HH:mm:ss") + "&nbsp;&nbsp;&nbsp;" + Convert.ToDateTime(DSMD.Tables[0].Rows[0]["CreatedDT"]).ToString("MMM dd yyyy");
        #endregion

        #region Plates Per Period
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["PlatesPerPeriod"]))
            GCPlatesPerPeriod.NumericIndicators["Default"].Value = float.Parse(DSMD.Tables[0].Rows[0]["PlatesPerPeriod"].ToString());
        else
            GCPlatesPerPeriod.NumericIndicators["Default"].Value = 0;

        #endregion

        #region Total Area
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["PlateAreaAbs"]))
            GCTotalArea.NumericIndicators["Default"].Value = float.Parse(DSMD.Tables[0].Rows[0]["PlateAreaAbs"].ToString());
        else
            GCTotalArea.NumericIndicators["Default"].Value = 0;

        #endregion

        #region Total Plates
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["NumOfPlatesAbs"]))
            GCTotalPlates.NumericIndicators["Default"].Value = float.Parse(DSMD.Tables[0].Rows[0]["NumOfPlatesAbs"].ToString());
        else
            GCTotalPlates.NumericIndicators["Default"].Value = 0;
        #endregion

        #region BigCircularChart
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["DaystoDevChange"]))
        {
            GCCircular.CircularGauges["Big"].Pointers["Default"].Value = Double.Parse(DSMD.Tables[0].Rows[0]["DaystoDevChange"].ToString());
            GCCircular.CircularGauges["Big"].Pointers["Default"].ToolTip = "Days Remaining : "+DSMD.Tables[0].Rows[0]["DaystoDevChange"].ToString();
            GCCircular.NumericIndicators["NIBig"].Value = float.Parse(DSMD.Tables[0].Rows[0]["DaystoDevChange"].ToString());
        }
        else
        {
            GCCircular.CircularGauges["Big"].Pointers["Default"].Value = 0;
            GCCircular.CircularGauges["Big"].Pointers["Default"].ToolTip = "Days Remaining : 0";
            GCCircular.NumericIndicators["NIBig"].Value = 0;
        }
        #endregion

        #region SmallTopCircularChart
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["Target_Cond"]))
        {
            GCCircular.CircularGauges["SmallTop"].Scales["Default"].Minimum = 0;
            GCCircular.CircularGauges["SmallTop"].Scales["Default"].Maximum = Math.Round(Double.Parse(DSMD.Tables[0].Rows[0]["Target_Cond"].ToString()) + ((Double.Parse(DSMD.Tables[0].Rows[0]["Target_Cond"].ToString()) * 20) / 100));            
            GCCircular.CircularGauges["SmallTop"].Pointers["Average"].Value = Double.Parse(DSMD.Tables[0].Rows[0]["Target_Cond"].ToString());
            GCCircular.CircularGauges["SmallTop"].Pointers["Average"].ToolTip = "Target : "+DSMD.Tables[0].Rows[0]["Target_Cond"].ToString();
        }
        else
        {
            GCCircular.CircularGauges["SmallTop"].Scales["Default"].Minimum = 0;
            GCCircular.CircularGauges["SmallTop"].Scales["Default"].Maximum = 10;            
            GCCircular.CircularGauges["SmallTop"].Pointers["Average"].Value = 0;
            GCCircular.CircularGauges["SmallTop"].Pointers["Average"].ToolTip = "Target : 0";
        }


        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["Activity_Cond"]))
        {
            GCCircular.CircularGauges["SmallTop"].Pointers["Default"].Value = Double.Parse(DSMD.Tables[0].Rows[0]["Actual_Cond"].ToString());
            GCCircular.CircularGauges["SmallTop"].Pointers["Default"].ToolTip = "Conductivity ms : " + DSMD.Tables[0].Rows[0]["Actual_Cond"].ToString();
            GCCircular.NumericIndicators["MISmallTop"].Value = float.Parse(DSMD.Tables[0].Rows[0]["Actual_Cond"].ToString());
        }
        else
        {
            GCCircular.CircularGauges["SmallTop"].Pointers["Default"].Value = 0;
            GCCircular.CircularGauges["SmallTop"].Pointers["Default"].ToolTip = "Conductivity ms : 0";
            GCCircular.NumericIndicators["MISmallTop"].Value = 0;
        }
        #endregion

        #region SmallBottomCircularChart
        if (DSMD.Tables[0].Rows.Count > 0 && !Convert.IsDBNull(DSMD.Tables[0].Rows[0]["Current_Dev_Temp"]))
        {
            GCCircular.CircularGauges["SmallBottom"].Scales["Default"].Minimum = 0;
            GCCircular.CircularGauges["SmallBottom"].Scales["Default"].Maximum = Math.Round(Double.Parse(DSMD.Tables[0].Rows[0]["Current_Dev_Temp"].ToString()) + ((Double.Parse(DSMD.Tables[0].Rows[0]["Current_Dev_Temp"].ToString()) * 20) / 100));
            GCCircular.CircularGauges["SmallBottom"].Pointers["Default"].Value = Double.Parse(DSMD.Tables[0].Rows[0]["Current_Dev_Temp"].ToString());
            GCCircular.CircularGauges["SmallBottom"].Pointers["Default"].ToolTip = "Temperature C : "+DSMD.Tables[0].Rows[0]["Current_Dev_Temp"].ToString();
            GCCircular.NumericIndicators["NISmallBottom"].Value = float.Parse(DSMD.Tables[0].Rows[0]["Current_Dev_Temp"].ToString());
        }
        else
        {
            GCCircular.CircularGauges["SmallBottom"].Scales["Default"].Minimum = 0;
            GCCircular.CircularGauges["SmallBottom"].Scales["Default"].Maximum = 10;            
            GCCircular.CircularGauges["SmallBottom"].Pointers["Default"].Value = 0;
            GCCircular.CircularGauges["SmallBottom"].Pointers["Default"].ToolTip = "Temperature C : 0";
            GCCircular.NumericIndicators["NISmallBottom"].Value = 0;
        }
        #endregion
    }
    private void GetAllGuids()
    {
        try
        {
            //========== Clear Old Labels ==========//
            LTRL_ZProcIDs.Text = "";
            LTRL_ZProcID.Text = Request.QueryString["ZProcID"].ToString();


            //========== First Get the Data Set ==========//
            ProcessorData objProcessor = new ProcessorData();
            objProcessor.DongleID = int.Parse(Convert.ToString(Request.QueryString["DongleID"]));//LTRL_DongleID.Text);
            DataTable DT = objProcessor.GetTrendLine();

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
                    if (LTRL_ZProcID.Text == DT.Rows[i]["ZProcID"].ToString())
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

            LTRL_ZProcIDs.Text = "";
            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {
                if (LTRL_ZProcIDs.Text == "")
                { LTRL_ZProcIDs.Text = DT.Rows[i]["ZProcID"].ToString(); }
                else
                { LTRL_ZProcIDs.Text += "," + DT.Rows[i]["ZProcID"].ToString(); }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion


}
