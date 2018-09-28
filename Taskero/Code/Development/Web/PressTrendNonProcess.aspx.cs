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

public partial class PressTrendNonProcess : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Fill Form ==========//
            if (IsPostBack == false)
            {
                //========== Assign Variables ==========//
                LTRL_JobID.Text = Request.QueryString["JobID"] == null ? "0" : Request.QueryString["JobID"].ToString();
                LTRL_DongleID.Text = Request.QueryString["DongleID"] == null ? "0" : Request.QueryString["DongleID"].ToString();
                LTRL_CPDeviceID.Text = Request.QueryString["CPDeviceID"] == null ? "0" : Request.QueryString["CPDeviceID"].ToString();

                //========== Assign Variables ==========//
                LTRL_KeyTrend.Text = LTRL_JobID.Text + "-Press-Trend";
                LTRL_KeyJobSheetSummary.Text = LTRL_JobID.Text + "-Press-DT-JobSheetSummary";

                //========== Clear Sessions ==========//                
                Fuji.Common.Sessions.SetSessions(LTRL_KeyTrend.Text, null);

                //========== Fills Chart ==========//
                FillCharts();
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_TrendDeltaE_Click(object sender, EventArgs e)
    {
        try { FillCharts(); }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_DeltaE_Click(object sender, EventArgs e)
    {
        try { FillCharts(); }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_Density_Click(object sender, EventArgs e)
    {
        try { FillCharts(); }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_Dotgain_Click(object sender, EventArgs e)
    {
        try { FillCharts(); }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_PrintContrast_Click(object sender, EventArgs e)
    {
        try { FillCharts(); }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Methods and Functions
    private void FillCharts()
    {
        try
        {
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyJobSheetSummary.Text) == null)
            {
                //========== Declaring Objects ==========//
                PressJobsData objPressJobs = new PressJobsData();

                //========== Get The Data Table ==========//
                objPressJobs.JobID = Convert.ToInt32(LTRL_JobID.Text);
                objPressJobs.DongleID = LTRL_DongleID.Text;
                Fuji.Common.Sessions.SetSessions(LTRL_KeyJobSheetSummary.Text, objPressJobs.uspGetPressJobSheetSummary());
            }
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyJobSheetSummary.Text);

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
                //for (int i = 0; i < DT.Rows.Count; i = i + 1)
                //{
                //    if (LTRL_CPMeasurementGUID.Text == DT.Rows[i]["CPMeasurementGUID"].ToString())
                //    {
                //        _CPMOnPage = i / _PageSize;
                //        break;
                //    }
                //}
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

            #region Trend Delta E
            int j = 0;
            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {
                if (CHK_TrendDeltaE_AvgDe.Checked == true)
                {
                    AddPointChart(j, CHRT_TrendDeltaE.Series["Series1"], "Sheet Number", DT.Rows[i]["SheetNumber"].ToString(), "Average Delta E", DT.Rows[i]["AvgDeltaE"].ToString());
                }
                else
                {
                    CHRT_TrendDeltaE.Series["Series1"].Enabled = false;
                }
                if (CHK_TrendDeltaE_MaxDe.Checked == true)
                {
                    AddPointChart(j, CHRT_TrendDeltaE.Series["Series2"], "Sheet Number", DT.Rows[i]["SheetNumber"].ToString(), "Maximum Delta E", DT.Rows[i]["MaxDeltaE"].ToString());
                }
                else
                {
                    CHRT_TrendDeltaE.Series["Series2"].Enabled = false;
                }
                if (CHK_TrendDeltaE_StdDE.Checked == true)
                {
                    AddPointChart(j, CHRT_TrendDeltaE.Series["Series3"], "Sheet Number", DT.Rows[i]["SheetNumber"].ToString(), "Standard Deviation", DT.Rows[i]["StdDevdeltaE"].ToString());
                }
                else
                {
                    CHRT_TrendDeltaE.Series["Series3"].Enabled = false;
                }

                j = j + 1;
            }

            //========== Ads a blank row ==========//
            if (DT.Rows.Count == 0) { AddBlankRow(CHRT_TrendDeltaE); }
            #endregion

            #region Fill All Charts
            FillOtherCharts(_StartIndex, _EndIndex);
            //FillDotGainTrendLine();
            //FillPrintContrastTrendLine();
            #endregion

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AddBlankRow(Chart objChart)
    {
        try
        {
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
    private void AddPointChart(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            //if (Convert.ToDecimal(_YValue) > 30) { _objSeries.Points.AddXY(_XValue, "30"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
            _objSeries.Points.AddXY(_XValue, _YValue);

            _objSeries.Points[_Index].ToolTip = _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
            _objSeries.Points[_Index].Href = "#";

            _objSeries.Points[_Index].MarkerSize = 4;
            _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AddPointChart(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue, string _ColorName)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            //if (Convert.ToDecimal(_YValue) > 30) { _objSeries.Points.AddXY(_XValue, "30"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
            _objSeries.Points.AddXY(_XValue, _YValue);

            _objSeries.Points[_Index].ToolTip = " Color Name : " + _ColorName + ", " + _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
            _objSeries.Points[_Index].Href = "#";

            _objSeries.Points[_Index].MarkerSize = 4;
            _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillOtherCharts(int _StartIndex, int _EndIndex)
    {
        try
        {
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyTrend.Text) == null)
            {
                //========== Declaring Objects ==========//
                PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();

                //========== Get The Data Table ==========//
                objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                Fuji.Common.Sessions.SetSessions(LTRL_KeyTrend.Text, objPressPatchCalculations.uspGetJobTrendView());
            }
            DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KeyTrend.Text);

            DataTable dtPrimaryColorType = new DataTable();

            if (ViewState["dtPrimaryColorType"] == null)
            {
                string[] str = { "PrimaryColorType", "ColorName", "HTMLColor" };
                dtPrimaryColorType = (DataTable)DSet.Tables[0].DefaultView.ToTable(true, str);
                ViewState["dtPrimaryColorType"] = dtPrimaryColorType;
            }
            else
            {
                dtPrimaryColorType = (DataTable)ViewState["dtPrimaryColorType"];
            }

            if (!IsPostBack)
            {
                rptr_DeltaE.DataSource = dtPrimaryColorType;
                rptr_DeltaE.DataBind();

                rptr_Density.DataSource = dtPrimaryColorType;
                rptr_Density.DataBind();

            }

            if (dtPrimaryColorType.Rows.Count > 0)
            {
                BTN_DeltaE_Apply.Visible = true;
                BTN_Density_Apply.Visible = true;
            }
            else
            {
                BTN_DeltaE_Apply.Visible = false;
                BTN_Density_Apply.Visible = false;
            }

            DataTable dtDeltaE = new DataTable();
            dtDeltaE = DSet.Tables[0];

            DataTable[] tmpDT = new DataTable[dtPrimaryColorType.Rows.Count];
            for (int indx = 0; indx < dtPrimaryColorType.Rows.Count; indx = indx + 1)
            {
                dtDeltaE.DefaultView.RowFilter = "PrimaryColorType = " + dtPrimaryColorType.Rows[indx]["PrimaryColorType"].ToString();
                tmpDT[indx] = dtDeltaE.DefaultView.ToTable();
            }

            int j = 0;
            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {
                #region All Series
                for (int indx = 0; indx < dtPrimaryColorType.Rows.Count; indx = indx + 1)
                {
                    if (i < tmpDT[indx].Rows.Count)
                    {
                        //----------------- For DeltaE Chart ------------------ //
                        bool flagDeltaE = false;
                        foreach (RepeaterItem itm in rptr_DeltaE.Items)
                        {
                            if (((System.Web.UI.WebControls.Label)itm.FindControl("lblPrimaryColorType")).Text.CompareTo(dtPrimaryColorType.Rows[indx]["PrimaryColorType"].ToString()) == 0)
                            {
                                if (((CheckBox)itm.FindControl("CHK")).Checked)
                                    flagDeltaE = true;
                                break;
                            }
                        }
                        if (flagDeltaE)
                        {
                            AddPointChart(j, CHRT_DeltaE.Series["Series" + (indx + 1).ToString()], "Scanned Date", tmpDT[indx].Rows[i]["SheetNumber"].ToString(), "Avg. Delta E", tmpDT[indx].Rows[i]["AvgDeltaE"].ToString(), tmpDT[indx].Rows[i]["ColorName"].ToString());
                            CHRT_DeltaE.Series["Series" + (indx + 1).ToString()].Color = ColorTranslator.FromHtml(tmpDT[indx].Rows[i]["HTMLColor"].ToString());
                        }
                        else
                            CHRT_DeltaE.Series["Series" + (indx + 1).ToString()].Enabled = false;

                        //----------------- For Density Chart ------------------ //
                        bool flagDensity = false;
                        foreach (RepeaterItem itm in rptr_Density.Items)
                        {
                            if (((System.Web.UI.WebControls.Label)itm.FindControl("lblPrimaryColorType")).Text.CompareTo(dtPrimaryColorType.Rows[indx]["PrimaryColorType"].ToString()) == 0)
                            {
                                if (((CheckBox)itm.FindControl("CHK")).Checked)
                                    flagDensity = true;
                                break;
                            }
                        }
                        if (flagDensity)
                        {
                            AddPointChart(j, CHRT_Density.Series["Series" + (indx + 1).ToString()], "Scanned Date", tmpDT[indx].Rows[i]["SheetNumber"].ToString(), "Avg. Density", tmpDT[indx].Rows[i]["AvgDensity"].ToString(), tmpDT[indx].Rows[i]["ColorName"].ToString());
                            CHRT_Density.Series["Series" + (indx + 1).ToString()].Color = ColorTranslator.FromHtml(tmpDT[indx].Rows[i]["HTMLColor"].ToString());
                        }
                        else
                            CHRT_Density.Series["Series" + (indx + 1).ToString()].Enabled = false;
                    }
                }
                #endregion

                j = j + 1;
            }

            //========== Ads a blank row ==========//
            if (DSet.Tables[0].Rows.Count == 0) { AddBlankRow(CHRT_DeltaE); AddBlankRow(CHRT_Density); }

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    protected void rptr_DeltaE_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lblPrimaryColorType = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrimaryColorType");
                System.Web.UI.WebControls.Label lblColorName = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblColorName");
                System.Web.UI.WebControls.Label lblHTMLColor = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblHTMLColor");

                CheckBox chk = (CheckBox)e.Item.FindControl("CHK");
                chk.Text = "<label style='height:15px;width:15px;border:1px solid #808080;background-color:" + lblHTMLColor.Text.Trim() + "'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;" + lblColorName.Text.Trim();
                chk.ToolTip = lblColorName.Text.Trim();
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void rptr_Density_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lblPrimaryColorType = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrimaryColorType");
                System.Web.UI.WebControls.Label lblColorName = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblColorName");
                System.Web.UI.WebControls.Label lblHTMLColor = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblHTMLColor");

                CheckBox chk = (CheckBox)e.Item.FindControl("CHK");
                chk.Text = "<label style='height:15px;width:15px;border:1px solid #808080;background-color:" + lblHTMLColor.Text.Trim() + "'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;" + lblColorName.Text.Trim();
                chk.ToolTip = lblColorName.Text.Trim();
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void rptr_Dotgain_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lblPrimaryColorType = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrimaryColorType");
                System.Web.UI.WebControls.Label lblColorName = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblColorName");
                System.Web.UI.WebControls.Label lblHTMLColor = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblHTMLColor");

                CheckBox chk = (CheckBox)e.Item.FindControl("CHK");
                chk.Text = "<label style='height:15px;width:15px;border:1px solid #808080;background-color:" + lblHTMLColor.Text.Trim() + "'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;" + lblColorName.Text.Trim();
                chk.ToolTip = lblColorName.Text.Trim();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    protected void rptr_PrintContrast_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lblPrimaryColorType = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblPrimaryColorType");
                System.Web.UI.WebControls.Label lblColorName = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblColorName");
                System.Web.UI.WebControls.Label lblHTMLColor = (System.Web.UI.WebControls.Label)e.Item.FindControl("lblHTMLColor");

                CheckBox chk = (CheckBox)e.Item.FindControl("CHK");
                chk.Text = "<label style='height:15px;width:15px;border:1px solid #808080;background-color:" + lblHTMLColor.Text.Trim() + "'>&nbsp;&nbsp;&nbsp;&nbsp;</label>&nbsp;" + lblColorName.Text.Trim();
                chk.ToolTip = lblColorName.Text.Trim();
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }

    /*
    private void FillDotGainTrendLine()
    {
        DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KeyTrend.Text);

        DataTable dtPrimaryColorType = new DataTable();
        if (ViewState["dtPrimaryColorTypeDotGain"] == null)
        {
            string[] str = { "PrimaryColorType", "ColorName", "HTMLColor" };
            dtPrimaryColorType = (DataTable)DSet.Tables[1].DefaultView.ToTable(true, str);
            ViewState["dtPrimaryColorTypeDotGain"] = dtPrimaryColorType;
        }
        else
        {
            dtPrimaryColorType = (DataTable)ViewState["dtPrimaryColorTypeDotGain"];
        }

        if (!IsPostBack)
        {
            rptr_Dotgain.DataSource = dtPrimaryColorType;
            rptr_Dotgain.DataBind();
        }

        if (dtPrimaryColorType.Rows.Count > 0)
            BTN_Dotgain_Apply.Visible = true;
        else
            BTN_Dotgain_Apply.Visible = false;

        DataTable dtDotGain = new DataTable();
        dtDotGain = DSet.Tables[1];

        int intMaxSeries = 0;
        DataTable[] tmpDT = new DataTable[dtPrimaryColorType.Rows.Count];
        for (int indx = 0; indx < dtPrimaryColorType.Rows.Count; indx = indx + 1)
        {
            dtDotGain.DefaultView.RowFilter = "PrimaryColorType = " + dtPrimaryColorType.Rows[indx]["PrimaryColorType"].ToString();
            tmpDT[indx] = dtDotGain.DefaultView.ToTable();
            if (intMaxSeries < tmpDT[indx].Rows.Count)
                intMaxSeries = tmpDT[indx].Rows.Count;
        }
        
        int j = 0;
        for (int i = 0; i < intMaxSeries; i = i + 1)
        {

            #region All Series
            for (int indx = 0; indx < dtPrimaryColorType.Rows.Count; indx = indx + 1)
            {
                if (i < tmpDT[indx].Rows.Count)
                {
                    bool flagDeltaE = false;
                    foreach (RepeaterItem itm in rptr_Dotgain.Items)
                    {
                        if (((System.Web.UI.WebControls.Label)itm.FindControl("lblPrimaryColorType")).Text.CompareTo(dtPrimaryColorType.Rows[indx]["PrimaryColorType"].ToString()) == 0)
                        {
                            if (((CheckBox)itm.FindControl("CHK")).Checked)
                                flagDeltaE = true;
                            break;
                        }
                    }
                    if (flagDeltaE)
                    {
                        AddPointChart(j, CHRT_Dotgain.Series["Series" + (indx + 1).ToString()], "Scanned Date", tmpDT[indx].Rows[i]["SheetNumber"].ToString(), "Avg. Dotgain", tmpDT[indx].Rows[i]["AvgDotgain"].ToString(), tmpDT[indx].Rows[i]["ColorName"].ToString());
                        CHRT_Dotgain.Series["Series" + (indx + 1).ToString()].Color = ColorTranslator.FromHtml(tmpDT[indx].Rows[i]["HTMLColor"].ToString());
                    }
                    else
                        CHRT_DeltaE.Series["Series" + (indx + 1).ToString()].Enabled = false;
                }
            }
            #endregion
            
            j = j + 1;
        }

        if (DSet.Tables[1].Rows.Count == 0) { AddBlankRow(CHRT_Dotgain); }
    }
    */
    #endregion
    /*
    private void FillPrintContrastTrendLine()
    {
        DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KeyTrend.Text);


        DataTable dtPrimaryColorType = new DataTable();
        if (ViewState["dtPrimaryColorTypeContrast"] == null)
        {
            string[] str = { "PrimaryColorType", "ColorName", "HTMLColor" };
            dtPrimaryColorType = (DataTable)DSet.Tables[2].DefaultView.ToTable(true, str);
            ViewState["dtPrimaryColorTypeContrast"] = dtPrimaryColorType;
        }
        else
        {
            dtPrimaryColorType = (DataTable)ViewState["dtPrimaryColorTypeContrast"];
        }

        if (!IsPostBack)
        {
            rptr_PrintContrast.DataSource = dtPrimaryColorType;
            rptr_PrintContrast.DataBind();
        }

        DataTable dtPrintContrast = new DataTable();
        dtPrintContrast = DSet.Tables[2];

        if (dtPrimaryColorType.Rows.Count > 0)
            BTN_PrintContrast_Apply.Visible = true;
        else
            BTN_PrintContrast_Apply.Visible = false;

        int intMaxSeries = 0;
        DataTable[] tmpDT = new DataTable[dtPrimaryColorType.Rows.Count];
        for (int indx = 0; indx < dtPrimaryColorType.Rows.Count; indx = indx + 1)
        {
            dtPrintContrast.DefaultView.RowFilter = "PrimaryColorType = " + dtPrimaryColorType.Rows[indx]["PrimaryColorType"].ToString();
            tmpDT[indx] = dtPrintContrast.DefaultView.ToTable();
            if (intMaxSeries < tmpDT[indx].Rows.Count)
                intMaxSeries = tmpDT[indx].Rows.Count;
        }
        
        int j = 0;
        for (int i = 0; i < intMaxSeries; i = i + 1)
        {
            #region All Series
            for (int indx = 0; indx < dtPrimaryColorType.Rows.Count; indx = indx + 1)
            {
                if (i < tmpDT[indx].Rows.Count)
                {
                    bool flagDeltaE = false;
                    foreach (RepeaterItem itm in rptr_PrintContrast.Items)
                    {
                        if (((System.Web.UI.WebControls.Label)itm.FindControl("lblPrimaryColorType")).Text.CompareTo(dtPrimaryColorType.Rows[indx]["PrimaryColorType"].ToString()) == 0)
                        {
                            if (((CheckBox)itm.FindControl("CHK")).Checked)
                                flagDeltaE = true;
                            break;
                        }
                    }
                    if (flagDeltaE)
                    {
                        //AddPointChart(j, CHRT_PrintContrast.Series["Series" + (indx + 1).ToString()], "Scanned Date", tmpDT[indx].Rows[i]["SheetNumber"].ToString(), "Avg. Dotgain", tmpDT[indx].Rows[i]["AvgPrintContrast"].ToString(), tmpDT[indx].Rows[i]["ColorName"].ToString());
                        AddPointChart(j, CHRT_PrintContrast.Series["Series" + (indx + 1).ToString()], "Scanned Date", tmpDT[indx].Rows[i]["SheetNumber"].ToString(), "Avg. Print Contrast", tmpDT[indx].Rows[i]["AvgPrintContrast"].ToString(), tmpDT[indx].Rows[i]["ColorName"].ToString()); 
                        CHRT_PrintContrast.Series["Series" + (indx + 1).ToString()].Color = ColorTranslator.FromHtml(tmpDT[indx].Rows[i]["HTMLColor"].ToString());
                    }
                    else
                        CHRT_PrintContrast.Series["Series" + (indx + 1).ToString()].Enabled = false;
                }
            }
            #endregion
            j = j + 1;
        }

        if (DSet.Tables[2].Rows.Count == 0) { AddBlankRow(CHRT_PrintContrast); }
    }
    */

    #region Search Bar Events and Methods
    protected void DDL_PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Alter the page size ==========//
            LTRL_PageSize.Text = DDL_PageSize.SelectedValue;

            //========== Gets The Data Table ==========//
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyJobSheetSummary.Text);
            int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
            int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }

            //========== Filling the DropDown ==========//
            DDL_GoToPage.Items.Clear();
            FillDDLGoToPage(_PageCount);

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
            FillCharts();
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
            FillCharts();
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
            FillCharts();
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
            FillCharts();
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
            FillCharts();
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
