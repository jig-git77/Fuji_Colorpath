#region Page Load and Events
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using Telerik.WebControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Drawing;
using Dundas.Charting.WebControl;
#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Fuji.Business;
using Fuji.DataAccess;
#endregion


public partial class ReportSingleMeasurementReport : PageBase
{

    #region Declaration

    //====  This is set for Default Page Size ====//
    private const int _DefaultPageSize = 25;

    //====  Set Grid Column Number ====//
    private enum enumGridColumn
    {
        Status = 2,
        hdnSatus = 3,
        View = 4,
        Job = 5,
        PrintTarget = 6,
        DeviceDesc = 7,
        //DongleDescription = 8,
        BranchName = 8,
        DeviceType = 9,
        DateTime = 10,
        //Date = 12,
        //TIME = 13,
        GUID = 11,
        //CPMeasurementID = 15,
        //CompanyID = 12,
        //CompanyName = 13,
        //BranchID = 14,
        //DongleID = 15,
        //DeviceID = 16,
        //CPDevicePrintTargetID = 17,
        //Customer = 18,
        CPDeviceTypeID = 12,
        //StatusID = 20,
        //CPSerialNUM = 21

    }
    private enum enumJobTypeID
    {
        CMYK_Spot = 1,
        G7 = 2,
        NonCMYK = 3
    }

    #region " === Private Property === "
    public string paraCPMeasurementGUID { get; set; }
    public Double NPDMaxValue  { get; set; }
    public Double NPDMinValue { get; set; }
    #endregion

    #endregion

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Bind Grid ==========//     
            if (IsPostBack == false)
            {
                //========= Clear the date for first time =========//
                Fuji.Common.Sessions.SetSessions("StartDate", null);
                FillGrid();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string _SelectedValue;
            _SelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = _SelectedValue;

            //====  Set No of Record Per Page for Press Grid  ====//
            GridJobReportPrepress.PageSize = Convert.ToInt32(_SelectedValue);
            GridJobReportPrepress.CurrentPageIndex = 0;

            //====  Bind Press Grid  ====//
            GridJobReportPrepress.Rebind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    #region Event Prepress Grid
   
    protected void GridJobReportPrepress_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                #region ====  Set Pager Style ====

                //====  Set Label of Record Text  ====//
                Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");

                //==== Varible Initiate  ====//
                int _FirstIndexInPage = 0, _LastIndexInPage = 0, _DataSourceCount = 0;

                //====  Assign Values ====//
                _FirstIndexInPage = GridJobReportPrepress.MasterTableView.PagingManager.FirstIndexInPage;
                _LastIndexInPage = GridJobReportPrepress.MasterTableView.PagingManager.LastIndexInPage + 1;
                _DataSourceCount = GridJobReportPrepress.MasterTableView.PagingManager.DataSourceCount;

                //====  When Record more then 1  ====//
                if (_DataSourceCount > 0)
                {
                    _FirstIndexInPage++;
                }

                //====  Fill Value in Record Label  ====//
                LTRL_RecordText.Text = "Record(s) : " + _FirstIndexInPage + " to " + _LastIndexInPage + " of " + _DataSourceCount + " | ";

                //==== Create Objects For First, Last, Prev, Next Button  ====//
                ImageButton IBTN_First = (ImageButton)e.Item.FindControl("IBTN_First");
                ImageButton IBTN_Prev = (ImageButton)e.Item.FindControl("IBTN_Prev");
                ImageButton IBTN_Next = (ImageButton)e.Item.FindControl("IBTN_Next");
                ImageButton IBTN_Last = (ImageButton)e.Item.FindControl("IBTN_Last");

                //====  Here when Click on First then Set Images means ====//
                //====  Which i enable and which disable  ====//
                if (GridJobReportPrepress.MasterTableView.PagingManager.IsFirstPage)
                {
                    IBTN_First.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    IBTN_Prev.ImageUrl = "Images/Nav_PreviousPage_off.gif";
                    IBTN_First.Style.Add("cursor", "");
                    IBTN_Prev.Style.Add("cursor", "");
                    IBTN_First.Attributes.Add("disabled", "true");
                    IBTN_Prev.Attributes.Add("disabled", "true");
                }
                else
                {
                    IBTN_First.ImageUrl = "Images/Nav_FirstPage.gif";
                    IBTN_Prev.ImageUrl = "Images/Nav_PreviousPage.gif";
                    IBTN_First.Style.Add("cursor", "pointer");
                    IBTN_Prev.Style.Add("cursor", "pointer");
                }

                //====  Here when Click on Last then Set Images means ====//
                if (GridJobReportPrepress.MasterTableView.PagingManager.IsLastPage == true)
                {
                    IBTN_Next.ImageUrl = "Images/Nav_NextPage_off.gif";
                    IBTN_Last.ImageUrl = "Images/Nav_LastPage_off.gif";
                    IBTN_Next.Style.Add("cursor", "");
                    IBTN_Last.Style.Add("cursor", "");
                    IBTN_Next.Attributes.Add("disabled", "true");
                    IBTN_Last.Attributes.Add("disabled", "true");
                }
                else
                {
                    IBTN_Next.ImageUrl = "Images/Nav_NextPage.gif";
                    IBTN_Last.ImageUrl = "Images/Nav_LastPage.gif";
                    IBTN_Next.Style.Add("cursor", "pointer");
                    IBTN_Last.Style.Add("cursor", "pointer");
                }
                #endregion

            }

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
            {
                #region ====  Status Settings  ====

                //========  Initiate Object For Status Image  ========//
                System.Web.UI.HtmlControls.HtmlImage ibtnStatus;
                ibtnStatus = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ibtnStatus");
                if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumStatus.Failed.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/RedStar.gif"; //========  Failed Then Red Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumStatus.Failed.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumStatus.Caution.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/YellowStar.gif";//========  Caution Then Yellow Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumStatus.Caution.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumStatus.Passed.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/GreenStar.gif";//========  Passed Then Green Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumStatus.Passed.ToString());
                }
                if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                { ibtnStatus.Visible = false; }
                #endregion

                #region ====  View Setting  ====               
                string strGUID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblGUID")).Text;
                System.Web.UI.HtmlControls.HtmlImage imgCreateReport;
                imgCreateReport = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgCreateReport");
                imgCreateReport.Attributes.Add("onclick", "OpenMeasurementWindow('" + strGUID + "');");

                #endregion

            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void GridJobReportPrepress_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                //====  Initiate the object for Record DropDown  ====//
                DropDownList DDL_RecordPage = (DropDownList)e.Item.FindControl("DDL_RecordPage");

                if (DDL_RecordPage != null)
                {
                    //====  Set Event Handler ====//
                    DDL_RecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPage_SelectedIndexChanged);

                    if (!IsPostBack)
                    {
                        //====  Set Default Value  ====//
                        DDL_RecordPage.SelectedValue = _DefaultPageSize.ToString();
                    }
                    else
                    {
                        if (ViewState["RecordPerPage"] != null)
                        {
                            //==== Find How many Records Show per Page and show it ====//
                            DDL_RecordPage.SelectedIndex = -1;
                            DDL_RecordPage.Items.FindByValue(ViewState["RecordPerPage"].ToString()).Selected = true;
                        }
                    }
                }

                //====  For Reset Filter Option  ====//
                Button BTN_Reset = (Button)e.Item.FindControl("BTN_Reset");
                if (BTN_Reset != null)
                {
                    //====  Set Event Handler  ====//
                    BTN_Reset.Click += new EventHandler(btnReset_Click);
                }
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void GridJobReportPrepress_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "SelectDate")
            {

                TableCell cell = ((Telerik.WebControls.GridFilteringItem)e.Item)["DateTime"];
                Panel Pnl_Date_Picker = (Panel)cell.Controls[0].FindControl("PNL_DATE_PICKER");
                RadDatePicker RDP_Date = (RadDatePicker)Pnl_Date_Picker.FindControl("RadDatePicker1");

                if (RDP_Date.SelectedDate != null)  //changed by chirag 06/29
                {
                    Fuji.Common.Sessions.SetSessions("StartDate", RDP_Date.SelectedDate);

                    //e.Item.OwnerTableView.FilterExpression = String.Format("DateTime > '{0}'", RDP_Date.SelectedDate.ToString());
                    e.Item.OwnerTableView.FilterExpression = String.Format("DateTime > '{0}' AND DateTime < '{1}'", RDP_Date.SelectedDate.ToString(), Convert.ToDateTime(RDP_Date.SelectedDate.ToString()).AddDays(1).ToString());
                    e.Item.OwnerTableView.Rebind();
                }
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            //====  Clear Filter Controls for Press Grid  ====//
            foreach (Telerik.WebControls.GridColumn column in GridJobReportPrepress.MasterTableView.Columns)
            {
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }

            Fuji.Common.Sessions.SetSessions("StartDate", null);

            //=== Set For Press Grid

            //====  Set Filter Empty  ====//
            GridJobReportPrepress.MasterTableView.FilterExpression = String.Empty;

            //====  Rebind Master View  ====//
            GridJobReportPrepress.MasterTableView.Rebind();

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void GridJobReportPrepress_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========  Check Session  ========//
            if (Fuji.Common.Sessions.GetSession("DSGridJobReportPrePress") != null)
            {
                //========  Session Store in DataTable  ========//
                DataTable dtJobReport = (DataTable)Fuji.Common.Sessions.GetSession("DSGridJobReportPrePress");

                //========  Grid Bind  ========//
                GridJobReportPrepress.DataSource = dtJobReport;
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }

    private void FillGrid()
    {
        try
        {
            DataTable dt;
            //========  Session Store in DataTable  ========//

            if (Fuji.Common.Sessions.GetSession("DSGridJobReportPrePress") == null)
            {
                SqlCommand CmdJobReport = new SqlCommand();
                CmdJobReport.CommandType = CommandType.StoredProcedure;
                CmdJobReport.CommandText = "Report.usp_CPV_JobData";

                if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                {
                    CmdJobReport.Parameters.AddWithValue("@TaskeroUserID", 0); // 0 as Admin in SP
                    CmdJobReport.Parameters.AddWithValue("@CompanyID", 0); // 0 as Admin in SP
                }
                else
                {
                    CmdJobReport.Parameters.AddWithValue("@TaskeroUserID", Sessions.UserID);
                    CmdJobReport.Parameters.AddWithValue("@CompanyID", Sessions.CompanyID);
                }                
                Fuji.Common.Sessions.SetSessions("DSGridJobReportPrePress", (new Fuji.DataAccess.Database()).GetDataTable(CmdJobReport));

                dt = (DataTable)Fuji.Common.Sessions.GetSession("DSGridJobReportPrePress");
            }
            else
            {                
                dt = (DataTable)Fuji.Common.Sessions.GetSession("DSGridJobReportPrePress");
            }

            //========  Grid Bind  ========//
            GridJobReportPrepress.DataSource = dt;
            //====  Bind Data Grid  ====//
            GridJobReportPrepress.DataBind();
            
            

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion

    protected void BtnCreateReport_Click(object sender, EventArgs e)
    {
        try
        {
            paraCPMeasurementGUID = Convert.ToString(HDFCPMeasurementGUID.Value);
            // === Fill Chart For Report === //
            CreateReport();

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }      
    #endregion

    #region Validations
    protected void TXT_DateText(object sender, EventArgs e)
    {
        if (Fuji.Common.Validations.IsDateTime(((TextBox)sender).Text) == false)
        { LblError.Text = "Please enter a valid date!"; ((TextBox)sender).Text = ""; return; }
    }
    #endregion

    #region Fill Charts
    private void CreateReport()
    {
        try
        {
            // ===== Create Object of ReportPrepressSingleMeasurementData ======//
            ReportPrepressSingleMeasurementData objSingleMeasurement = new ReportPrepressSingleMeasurementData();


            // ===== Get Data from data base  ==================//
            objSingleMeasurement.CPMeasurementGUID = paraCPMeasurementGUID;
            //objSingleMeasurement.CPMeasurementGUID = "ABB1A8C9-366F-4300-BE34-7342ADCE3283";// "ABB1A8C9-366F-4300-BE34-7342ADCE3283";
            DataSet dsSingleMeasurement = objSingleMeasurement.GetSingleMeasurement();

            if (dsSingleMeasurement != null && dsSingleMeasurement.Tables.Count > 0)
            {

                #region " === Page : 1 Result Summary === "
                //Fuji.Common.CommonFunctions.FileTimeLog("Start a* b* Chart", true);
                Bind_TargetVersusActual_CHART(dsSingleMeasurement.Tables[1]); // Bind Chart Of Target And Actual Value
                
                //Fuji.Common.CommonFunctions.FileTimeLog("Start Summary Delta Value Chart", false);
                BindSummaryDeltaValueBarChart(dsSingleMeasurement.Tables[2]); // Bind Chart of Summary Delta Value Bar Chart
                
                //Fuji.Common.CommonFunctions.FileTimeLog("Start Summary Delta E Value Chart", false);
                BindSummaryDeltaEValueBarChart(dsSingleMeasurement.Tables[3], 0, 10); //Bind Chart of Summary Delta E Value Bar Chart
                

                #endregion

                #region " === Page : 2 === "
                //Fuji.Common.CommonFunctions.FileTimeLog("Start Gray L* A* B* Chart", false);
                BindLABDeviationGraph(dsSingleMeasurement.Tables[4]); //Bind Charts Of Gray L* A* B* Deviation Line Chart
                

                //Fuji.Common.CommonFunctions.FileTimeLog("Start Gray Balck NPD Chart", false);
                CalcNPDBlackAndGrayChartsMaxMin(dsSingleMeasurement.Tables[4], dsSingleMeasurement.Tables[5]); // Calculating Max Min Value For NPD Charts
                BindGrayNPDGraph(dsSingleMeasurement.Tables[4]);  //Bind Chart Of Gray NPD Line Chart
                BindBlackNPDGraph(dsSingleMeasurement.Tables[5]);  //Bind Chart Of Black NPD Line Chart
                
                #endregion

                #region " === Page : 3  === "
                //Fuji.Common.CommonFunctions.FileTimeLog("Start DotGain Actual Chart", false);
                BindDotgainActualGraph(dsSingleMeasurement.Tables[6]);  //Bind Chart DotGain Actual Line Chart
                
                //Fuji.Common.CommonFunctions.FileTimeLog("Start Dotgain Deviation Chart", false);
                BindDotgainDeviationGraph(dsSingleMeasurement.Tables[8]);  //Bind Chart DotGain Deviation Bar Chart

                //Fuji.Common.CommonFunctions.FileTimeLog("Start Density Deviation Chart", false);
                BindDensityDeviationGraph(dsSingleMeasurement.Tables[7]);  //Bind Chart Density Deviation Bar Chart               

                //Fuji.Common.CommonFunctions.FileTimeLog("Start Print Contrast Deviation Chart", false);
                BindPrintContrastDeviationGraph(dsSingleMeasurement.Tables[9]); //Bind Chart Print Contrast Deviation Bar Chart
                #endregion

            }
            CreateReportPDF(dsSingleMeasurement); // Generates PDF of Reports.
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void CreateReportPDF(DataSet Ds)
    {
        try
        {
            // === Set all parameters to generate pdf file === //

            //Fuji.Common.CommonFunctions.FileTimeLog("Start PDF", false);

            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.ReportSingleMeasurement;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_ReportSingleMeasurement.PDF";
            objReportPDF.DS_Common = Ds;            
            objReportPDF.MakePDF();

            // === After generat and save PDF file, Download it at a time === //
            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region " === Spider Actual AB Plot === "
    private void Bind_TargetVersusActual_CHART(DataTable dtGamut)
    {
        try
        {
            DataView DV = new DataView();
            if (dtGamut != null && dtGamut.Rows.Count > 0)
            {
                #region For Cyan Line
                dtGamut.DefaultView.RowFilter = "ColorType = 'Cyan' OR ColorType = 'Paper'";
                dtGamut.DefaultView.Sort = " Rank asc";
                DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {                        
                        CHART_TargetVersusActualAB.Series[0].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_B1"])));
                        CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Line;
                        CHART_TargetVersusActualAB.Series[0].Points[i].BorderWidth = 1;
                        CHART_TargetVersusActualAB.Series[0].Points[i].Color = Color.Green;
                        CHART_TargetVersusActualAB.Series[0].Points[i].MarkerColor = Color.Green;

                        if (i == 0)
                        {
                            CHART_TargetVersusActualAB.Series[6].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[6].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[6].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[6].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[6].Points[i].MarkerColor = Color.White;
                            CHART_TargetVersusActualAB.Series[6].Points[i].MarkerBorderColor = Color.Black;
                        }
                        else
                        {

                            CHART_TargetVersusActualAB.Series[6].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[6].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[6].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[6].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[6].Points[i].MarkerColor = Color.Cyan;
                            CHART_TargetVersusActualAB.Series[6].Points[i].MarkerBorderColor = Color.Black;
                        }


                    }


                #endregion

                #region For Green Line
                dtGamut.DefaultView.RowFilter = "ColorType = 'Green' OR ColorType = 'Paper'";
                dtGamut.DefaultView.Sort = " Rank asc";
                DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {
                        CHART_TargetVersusActualAB.Series[1].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_B1"])));
                        CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Line;
                        CHART_TargetVersusActualAB.Series[1].Points[i].BorderWidth = 1;
                        CHART_TargetVersusActualAB.Series[1].Points[i].Color = Color.Green;
                        CHART_TargetVersusActualAB.Series[1].Points[i].MarkerColor = Color.Green;
                        if (i == 0)
                        {
                            CHART_TargetVersusActualAB.Series[7].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[7].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[7].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[7].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[7].Points[i].MarkerColor = Color.White;
                            CHART_TargetVersusActualAB.Series[7].Points[i].MarkerBorderColor = Color.Black;
                        }
                        else
                        {

                            CHART_TargetVersusActualAB.Series[7].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[7].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[7].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[7].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[7].Points[i].MarkerColor = Color.Green;
                            CHART_TargetVersusActualAB.Series[7].Points[i].MarkerBorderColor = Color.Black;
                        }
                    }
                #endregion

                #region For Yellow Line
                dtGamut.DefaultView.RowFilter = "ColorType = 'Yellow' OR ColorType = 'Paper'";
                dtGamut.DefaultView.Sort = " Rank asc";
                DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {
                        CHART_TargetVersusActualAB.Series[2].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_B1"])));
                        CHART_TargetVersusActualAB.Series[2].Type = SeriesChartType.Line;
                        CHART_TargetVersusActualAB.Series[2].Points[i].BorderWidth = 1;
                        CHART_TargetVersusActualAB.Series[2].Points[i].Color = Color.Green;
                        CHART_TargetVersusActualAB.Series[2].Points[i].MarkerColor = Color.Green;

                        if (i == 0)
                        {
                            CHART_TargetVersusActualAB.Series[8].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[8].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[8].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[8].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[8].Points[i].MarkerColor = Color.White;
                            CHART_TargetVersusActualAB.Series[8].Points[i].MarkerBorderColor = Color.Black;
                        }
                        else
                        {

                            CHART_TargetVersusActualAB.Series[8].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[8].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[8].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[8].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[8].Points[i].MarkerColor = Color.Yellow;
                            CHART_TargetVersusActualAB.Series[8].Points[i].MarkerBorderColor = Color.Black;
                        }
                    }
                #endregion

                #region For Red Line
                dtGamut.DefaultView.RowFilter = "ColorType = 'Red' OR ColorType = 'Paper'";
                dtGamut.DefaultView.Sort = " Rank asc";
                DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {
                        CHART_TargetVersusActualAB.Series[3].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_A1"])),Fuji.Common.CommonFunctions.DecimalRound( Convert.ToDouble(DV[i]["Targ_B1"])));
                        CHART_TargetVersusActualAB.Series[3].Type = SeriesChartType.Line;
                        CHART_TargetVersusActualAB.Series[3].Points[i].BorderWidth = 1;
                        CHART_TargetVersusActualAB.Series[3].Points[i].Color = Color.Green;
                        CHART_TargetVersusActualAB.Series[3].Points[i].MarkerColor = Color.Green;

                        if (i == 0)
                        {
                            CHART_TargetVersusActualAB.Series[9].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[9].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[9].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[9].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[9].Points[i].MarkerColor = Color.White;
                            CHART_TargetVersusActualAB.Series[9].Points[i].MarkerBorderColor = Color.Black;
                        }
                        else
                        {

                            CHART_TargetVersusActualAB.Series[9].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[9].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[9].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[9].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[9].Points[i].MarkerColor = Color.Red;
                            CHART_TargetVersusActualAB.Series[9].Points[i].MarkerBorderColor = Color.Black;
                        }
                    }
                #endregion

                #region For Magenta Line
                dtGamut.DefaultView.RowFilter = "ColorType = 'Magenta' OR ColorType = 'Paper'";
                dtGamut.DefaultView.Sort = " Rank asc";
                DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {
                        CHART_TargetVersusActualAB.Series[4].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_B1"])));
                        CHART_TargetVersusActualAB.Series[4].Type = SeriesChartType.Line;
                        CHART_TargetVersusActualAB.Series[4].Points[i].BorderWidth = 1;
                        CHART_TargetVersusActualAB.Series[4].Points[i].Color = Color.Green;
                        CHART_TargetVersusActualAB.Series[4].Points[i].MarkerColor = Color.Green;
                        if (i == 0)
                        {
                            CHART_TargetVersusActualAB.Series[10].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[10].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[10].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[10].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[10].Points[i].MarkerColor = Color.White;
                            CHART_TargetVersusActualAB.Series[10].Points[i].MarkerBorderColor = Color.Black;
                        }
                        else
                        {

                            CHART_TargetVersusActualAB.Series[10].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[10].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[10].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[10].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[10].Points[i].MarkerColor = Color.Magenta;
                            CHART_TargetVersusActualAB.Series[10].Points[i].MarkerBorderColor = Color.Black;
                        }
                    }
                #endregion

                #region For Blue Line
                dtGamut.DefaultView.RowFilter = "ColorType = 'Blue' OR ColorType = 'Paper'";
                dtGamut.DefaultView.Sort = " Rank asc";
                DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {

                        CHART_TargetVersusActualAB.Series[5].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["Targ_B1"])));
                        CHART_TargetVersusActualAB.Series[5].Type = SeriesChartType.Line;
                        CHART_TargetVersusActualAB.Series[5].Points[i].BorderWidth = 1;
                        CHART_TargetVersusActualAB.Series[5].Points[i].Color = Color.Green;
                        CHART_TargetVersusActualAB.Series[5].Points[i].MarkerColor = Color.Green;
                        if (i == 0)
                        {
                            CHART_TargetVersusActualAB.Series[11].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[11].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[11].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[11].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[11].Points[i].MarkerColor = Color.White;
                            CHART_TargetVersusActualAB.Series[11].Points[i].MarkerBorderColor = Color.Black;
                        }
                        else
                        {

                            CHART_TargetVersusActualAB.Series[11].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_A1"])), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["ACT_B1"])));
                            CHART_TargetVersusActualAB.Series[11].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[11].Points[i].BorderWidth = 2;
                            CHART_TargetVersusActualAB.Series[11].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[11].Points[i].MarkerColor = Color.Blue;
                            CHART_TargetVersusActualAB.Series[11].Points[i].MarkerBorderColor = Color.Black;
                        }
                    }
                #endregion

            }
            //else
            //{
            //    CHART_TargetVersusActualAB.BackImage = "~/Images/JobRunCertification_PointBack.gif";
            //}
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_TargetVersusActualAB.ImageUrl) + "." + CHART_TargetVersusActualAB.ImageType;
            CHART_TargetVersusActualAB.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region " ==== Summary Delta Value Chart === "
    private void BindSummaryDeltaValueBarChart(DataTable DT)
    {
        try
        {
            ChartResultSummary_Delta.BorderColor = Color.White;
            ChartResultSummary_Delta.BorderWidth = 0;
            ChartResultSummary_Delta.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            ChartResultSummary_Delta.Visible = true;
            ChartResultSummary_Delta.Series[0].Points.Clear();
            ChartResultSummary_Delta.Series[1].Points.Clear();
            ChartResultSummary_Delta.Series[2].Points.Clear();
            
            #region //========== Declarations ==========//            
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)ChartResultSummary_Delta.Series[0];
            objSeries0.XValueType = ChartValueTypes.Int;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Bar;            
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)ChartResultSummary_Delta.Series[1];
            objSeries1.XValueType = ChartValueTypes.Int;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Bar;

            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)ChartResultSummary_Delta.Series[2];
            objSeries2.XValueType = ChartValueTypes.Int;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Bar;          

            // === Make Chart area look like 2D even it is 3D === //
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].BackColor = Color.White;
            #endregion

            #region //========== ChartArea Settings ==========//
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Auto = true;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

            ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

            ChartResultSummary_Delta.ChartAreas["ChartArea1"].ShadowOffset = 0;
            ChartResultSummary_Delta.BorderSkin.SkinStyle = BorderSkinStyle.None;
            ChartResultSummary_Delta.ChartAreas["ChartArea1"].BorderWidth = 0;
            ChartResultSummary_Delta.BorderSkin.FrameBorderWidth = 0;



            #endregion

            #region " === Fill Series === "
            // === Limit with red marker === //
            if (DT != null && DT.Rows.Count > 0)
            {
                if(Convert.IsDBNull(DT.Rows[0]["AverageDE_ACT"]) || Convert.IsDBNull(DT.Rows[0]["AverageDE_TH"]))
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 0);
                else
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["AverageDE_ACT"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["AverageDE_TH"].ToString())), 0);

                if (Convert.IsDBNull(DT.Rows[0]["MaximumDE_ACT"]) || Convert.IsDBNull(DT.Rows[0]["MaximumDE_TH"]))
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 1);
                else
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["MaximumDE_ACT"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["MaximumDE_TH"].ToString())), 1);

                if (Convert.IsDBNull(DT.Rows[0]["PrimaryDE_ACT"]) || Convert.IsDBNull(DT.Rows[0]["PrimaryDE_TH"]))
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 2);
                else
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["PrimaryDE_ACT"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["PrimaryDE_TH"].ToString())), 2);


                if (Convert.IsDBNull(DT.Rows[0]["PrimaryDH_ACT"]) || Convert.IsDBNull(DT.Rows[0]["PrimaryDH_TH"]))
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 3);
                else
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["PrimaryDH_ACT"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["PrimaryDH_TH"].ToString())), 3);

                if (Convert.IsDBNull(DT.Rows[0]["GrayAverageDH_ACT"]) || Convert.IsDBNull(DT.Rows[0]["GrayAverageDH_TH"]))
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 4);
                else
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["GrayAverageDH_ACT"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["GrayAverageDH_TH"].ToString())), 4);

                if (Convert.IsDBNull(DT.Rows[0]["dE_95pct_Patches_ACT"]) || Convert.IsDBNull(DT.Rows[0]["dE_95pct_Patches_TH"]))
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 5);
                else
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["dE_95pct_Patches_ACT"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["dE_95pct_Patches_TH"].ToString())), 5);

                if (Convert.IsDBNull(DT.Rows[0]["dE_OuterGamut_ACT"]) || Convert.IsDBNull(DT.Rows[0]["dE_OuterGamut_TH"]))
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 6);
                else
                    BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["dE_OuterGamut_ACT"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[0]["dE_OuterGamut_TH"].ToString())), 6);

                
                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(ChartResultSummary_Delta.ImageUrl) + "." + ChartResultSummary_Delta.ImageType;
                ChartResultSummary_Delta.Save(Server.MapPath(FileName));
            }
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void BindDeltaValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, double ACT, double TH, int Index)
    {
        try
        {
            
            Double Y1, Y2;
            Y1 = Y2 = 0;

            Y1 = (Convert.ToString(TH) == "" ? 0 : Convert.ToDouble(TH.ToString()));
            Y2 = (Convert.ToString(ACT) == "" ? 0 : Convert.ToDouble(ACT.ToString()));

            // === When Actual value is not available then no series will display and no marker will display
            if (Y2 == 0)
                Y1 = Y2;

            
            Series SeriesActual = objSeries1;            
            objSeries0.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index]["PointWidth"] = "0.8";
            objSeries1.Points.AddXY((Double)0, Y2);
            objSeries2.Points.AddXY((Double)0, Y1);            
            objSeries0.Points[Index].BackGradientType = GradientType.DiagonalRight;
            
            SeriesActual.Points[Index]["PointWidth"] = "0.8";
            //SeriesActual.Points[PointIndex].Href = "#";

            if (Y1 == 0 && Y2 == 0)
            {
                objSeries0.Points[Index].MarkerImage = "";
                objSeries2.Points[Index].MarkerImage = "";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region " ==== Summary Delta E Value Chart === "
    private void BindSummaryDeltaEValueBarChart(DataTable DT,int _StartIndex, int _EndIndex)
    {
        try
        {
            ChartResultSummary_DeltaEValue.BorderColor = Color.White;
            ChartResultSummary_DeltaEValue.BorderWidth = 0;
            ChartResultSummary_DeltaEValue.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            ChartResultSummary_DeltaEValue.Visible = true;
            ChartResultSummary_DeltaEValue.Series[0].Points.Clear();
            ChartResultSummary_DeltaEValue.Series[1].Points.Clear();
            ChartResultSummary_DeltaEValue.Series[2].Points.Clear();

            #region //========== Declarations ==========//
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)ChartResultSummary_DeltaEValue.Series[0];
            objSeries0.XValueType = ChartValueTypes.Int;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Bar;
            //objSeries0.Color = Color.Gray;
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)ChartResultSummary_DeltaEValue.Series[1];
            objSeries1.XValueType = ChartValueTypes.Int;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Bar;

            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)ChartResultSummary_DeltaEValue.Series[2];
            objSeries2.XValueType = ChartValueTypes.Int;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Bar;

            // === Make Chart area look like 2D even it is 3D === //
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].BackColor = Color.White;
            #endregion

            #region //========== ChartArea Settings ==========//
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Auto = true;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].ShadowOffset = 0;
            ChartResultSummary_DeltaEValue.BorderSkin.SkinStyle = BorderSkinStyle.None;
            ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].BorderWidth = 0;
            ChartResultSummary_DeltaEValue.BorderSkin.FrameBorderWidth = 0;



            #endregion

            #region " === Fill Series === "
            // === Limit with red marker === //
            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {
                if (DT != null && i < DT.Rows.Count)
                    BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, DT.Rows[i], i);                
                else
                    BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, null, i);                
            }
          

            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(ChartResultSummary_DeltaEValue.ImageUrl) + "." + ChartResultSummary_DeltaEValue.ImageType;
            ChartResultSummary_DeltaEValue.Save(Server.MapPath(FileName));
          
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        
    }

    private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, DataRow DR , int Index)
    {
        try
        {
            string strHTMLColor="";
            Double Y1, Y2;
            Y1 = Y2 = 0;

            if (DR != null)
            {
                Y1 = (Convert.ToString(DR["TOL"]) == "" ? 0 : Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["TOL"].ToString())));
                Y2 = (Convert.ToString(DR["DE"]) == "" ? 0 : Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["DE"].ToString())));
                strHTMLColor = Formulas.ColorCalcuations.LabtoRGB(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["LAB_L1"].ToString())),Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["LAB_A1"].ToString())),Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["LAB_B1"].ToString())));                
            }

            // === When Actual value is not available then no series will display and no marker will display
            if (Y2 == 0)
                Y1 = Y2;


            Series SeriesActual = objSeries1;
            objSeries0.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index]["PointWidth"] = "0.8";
            objSeries1.Points.AddXY((Double)0, Y2);
            objSeries2.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index].BackGradientType = GradientType.DiagonalRight;

            SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
            SeriesActual.Points[Index]["PointWidth"] = "0.8";
            //SeriesActual.Points[PointIndex].Href = "#";

            if (Y1 == 0 && Y2 == 0)
            {
                objSeries0.Points[Index].MarkerImage = "";
                objSeries2.Points[Index].MarkerImage = "";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion

    #region " ==== Gray L* A* B* Deviation Graph ==== "
    private void BindLABDeviationGraph(DataTable DT)
    {
        try
        {
            
            Double LCValue = 0;
            Double LMaxValue = 0;
            Double LMinValue = 0;
            Double aCValue = 0;
            Double aMaxValue = 0;
            Double aMinValue = 0;
            Double bCValue = 0;
            Double bMaxValue = 0;
            Double bMinValue = 0;

            
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {

                //========= Setting For Gray L Deviation Graph ==============//
                CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Auto = true;
                CHRT_GrayDeviation_L.ChartAreas["Default"].Position.X = float.Parse("3");
                CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Y = float.Parse("5");
                CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Width = float.Parse("91");
                CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Height = float.Parse("86");

                CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("86");

                CHRT_GrayDeviation_L.ChartAreas["Default"].ShadowOffset = 0;
                CHRT_GrayDeviation_L.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT_GrayDeviation_L.ChartAreas["Default"].BorderWidth = 1;
                CHRT_GrayDeviation_L.BorderSkin.FrameBorderWidth = 0;

                //========= Setting For Gray A Deviation Graph ==============//
                CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Auto = true;
                CHRT_GrayDeviation_A.ChartAreas["Default"].Position.X = float.Parse("3");
                CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Y = float.Parse("5");
                CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Width = float.Parse("91");
                CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Height = float.Parse("86");

                CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("86");

                CHRT_GrayDeviation_A.ChartAreas["Default"].ShadowOffset = 0;
                CHRT_GrayDeviation_A.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT_GrayDeviation_A.ChartAreas["Default"].BorderWidth = 1;
                CHRT_GrayDeviation_A.BorderSkin.FrameBorderWidth = 0;

                //========= Setting For Gray B Deviation Graph ==============//
                CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Auto = true;
                CHRT_GrayDeviation_B.ChartAreas["Default"].Position.X = float.Parse("3");
                CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Y = float.Parse("5");
                CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Width = float.Parse("91");
                CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Height = float.Parse("86");

                CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("86");

                CHRT_GrayDeviation_B.ChartAreas["Default"].ShadowOffset = 0;
                CHRT_GrayDeviation_B.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT_GrayDeviation_B.ChartAreas["Default"].BorderWidth = 1;
                CHRT_GrayDeviation_B.BorderSkin.FrameBorderWidth = 0;


               
               


                #region Find And Set Maximum And Minimum To Chart
                //for (int i = 0; i < DT.Rows.Count; i = i + 1)
                //{
                //    LCValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["DL"]));
                //    aCValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["DA"]));
                //    bCValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["DB"]));

                //    if (LCValue > LMaxValue) LMaxValue = LCValue;
                //    if (LCValue < LMinValue) LMinValue = LCValue;

                //    if (aCValue > aMaxValue) aMaxValue = aCValue;
                //    if (aCValue < aMinValue) aMinValue = aCValue;

                //    if (bCValue > bMaxValue) bMaxValue = bCValue;
                //    if (bCValue < bMinValue) bMinValue = bCValue;


                //}

                //#region Set Min Max For L Chart
                //if (LMinValue < 0) LMinValue = (LMinValue * -1);
                
                //LMaxValue += 2;                
                //LMinValue += 2;

                //if (LMaxValue > LMinValue)
                //    LMinValue = (LMaxValue * -1);
                //else
                //{
                //    LMaxValue = LMinValue ;
                //    LMinValue = (LMinValue * -1);
                //}
                

                CHRT_GrayDeviation_L.ChartAreas["Default"].AxisY.Minimum =    -10.0;          //Math.Round(LMinValue);
                CHRT_GrayDeviation_L.ChartAreas["Default"].AxisY.Maximum = 10.0;         //Math.Round(LMaxValue);
                #endregion

                #region Set Min Max For a Chart
                //if (aMinValue < 0) aMinValue = (aMinValue * -1);

                //aMaxValue += 2;
                //aMinValue += 2;

                //if (aMaxValue > aMinValue)
                //    aMinValue = (aMaxValue * -1);
                //else
                //{
                //    aMaxValue = aMinValue;
                //    aMinValue = (aMinValue * -1);
                //}
                
                CHRT_GrayDeviation_A.ChartAreas["Default"].AxisY.Minimum =   -10.0; //Math.Round(aMinValue);
                CHRT_GrayDeviation_A.ChartAreas["Default"].AxisY.Maximum = 10.0;    //Math.Round(aMaxValue);
                //#endregion

                //#region Set Min Max For b Chart
                //if (bMinValue < 0) bMinValue = (bMinValue * -1);

                //bMaxValue += 2;
                //bMinValue += 2;

                //if (bMaxValue > bMinValue)
                //    bMinValue = (bMaxValue * -1);
                //else
                //{
                //    bMaxValue = bMinValue;
                //    bMinValue = (bMinValue * -1);
                //}

                CHRT_GrayDeviation_B.ChartAreas["Default"].AxisY.Minimum = -10.0;     //Math.Round(bMinValue);
                CHRT_GrayDeviation_B.ChartAreas["Default"].AxisY.Maximum = 10.0;     //    Math.Round(bMaxValue);
                #endregion
                #endregion

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    //Plote Target Point in Chart L A B

                    AddTargetPointChart(i, CHRT_GrayDeviation_L.Series["Series1"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(),"0");// DT.Rows[i]["LAB_L1"].ToString());
                    AddTargetPointChart(i, CHRT_GrayDeviation_A.Series["Series1"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(),"0");// DT.Rows[i]["LAB_A1"].ToString());
                    AddTargetPointChart(i, CHRT_GrayDeviation_B.Series["Series1"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(),"0");// DT.Rows[i]["LAB_B1"].ToString());

               
                    AddPointChart(i, CHRT_GrayDeviation_L.Series["Series2"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(), DT.Rows[i]["DL"].ToString(),Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_L1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_A1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_B1"].ToString())));
                    AddPointChart(i, CHRT_GrayDeviation_A.Series["Series2"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(), DT.Rows[i]["DA"].ToString(),Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_L1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_A1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_B1"].ToString())));
                    AddPointChart(i, CHRT_GrayDeviation_B.Series["Series2"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(), DT.Rows[i]["DB"].ToString(),Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_L1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_A1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_B1"].ToString())));
                    
                }

                // === Save Chart Image after generating image with proper data === //
                string LFileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_GrayDeviation_L.ImageUrl) + "." + CHRT_GrayDeviation_L.ImageType;
                CHRT_GrayDeviation_L.Save(Server.MapPath(LFileName));
                string AFileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_GrayDeviation_A.ImageUrl) + "." + CHRT_GrayDeviation_A.ImageType;
                CHRT_GrayDeviation_A.Save(Server.MapPath(AFileName));
                string BFileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_GrayDeviation_B.ImageUrl) + "." + CHRT_GrayDeviation_B.ImageType;
                CHRT_GrayDeviation_B.Save(Server.MapPath(BFileName));

            }
        }
        catch (Exception Exc)
        {

            throw Exc;
        }

    }   
    //Add Point In Chart for checking Paper Point
    private void AddTargetPointChart(int _Index, Series _objSeries, string _XValue, string _YValue)
    {
        try
        {   

            _objSeries.Type = SeriesChartType.Spline;            
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerColor = Color.Green;
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 9;
            _objSeries.Points[_Index].BorderWidth = 2;
            _objSeries.Points[_Index].MarkerBorderColor = Color.Green;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //Add Point In Chart for checking Paper Point
    private void AddPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, Double L, Double A, Double B)
    {
        try
        {

            //string strHTMLColor = Formulas.ColorCalcuations.LabtoRGB();    
            string strHTMLColor = Formulas.ColorCalcuations.LabtoRGB(L, A, B);

            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 9;
            _objSeries.Points[_Index].BorderWidth = 2;
            _objSeries.Points[_Index].MarkerBorderColor = Color.Black;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    

    #region " === Calculate NPD Balck And Gray Chart Maximum And Minimume Value ==="
    private void CalcNPDBlackAndGrayChartsMaxMin(DataTable DTGray, DataTable DTBlack)
    {
        try
        {
            NPDMaxValue = 0;
            NPDMinValue = 0;

            Double GCValue = 0;
            Double GMaxValue = 0;
            Double GMinValue = 0;
            Double BCValue = 0;
            Double BMaxValue = 0;
            Double BMinValue = 0;

            if (DTGray != null && DTGray.Rows.Count > 0 && DTGray.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DTGray.Rows.Count; i = i + 1)
                {
                    GCValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DTGray.Rows[i]["Density"]));

                    if (GCValue > GMaxValue) GMaxValue = GCValue;
                    if (GCValue < GMinValue) GMinValue = GCValue;
                }
            }

            if (DTBlack != null && DTBlack.Rows.Count > 0 && DTBlack.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DTBlack.Rows.Count; i = i + 1)
                {
                    BCValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DTBlack.Rows[i]["Density"]));

                    if (BCValue > BMaxValue) BMaxValue = BCValue;
                    if (BCValue < BMinValue) BMinValue = BCValue;
                }
            }
            

            if (GMaxValue > BMaxValue)
                NPDMaxValue = GMaxValue;
            else
                NPDMaxValue = BMaxValue;

            if (GMinValue < 0 || GMinValue < 0)
            {
                GMinValue = (GMinValue * -1);
                BMinValue = (BMinValue * -1);

                if (GMinValue > BMinValue)
                    NPDMinValue = (GMinValue * -1);
                else
                    NPDMinValue = (BMinValue * -1);
            }

        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    #endregion


    #region " ==== Gray NPD ==== "
    private void BindGrayNPDGraph(DataTable DT)
    {
        try
        {
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {

                //========= Setting For Gray L Deviation Graph ==============//
                CHRT_GrayNPD.ChartAreas["Default"].Position.Auto = true;
                CHRT_GrayNPD.ChartAreas["Default"].Position.X = float.Parse("6");
                CHRT_GrayNPD.ChartAreas["Default"].Position.Y = float.Parse("7");
                CHRT_GrayNPD.ChartAreas["Default"].Position.Width = float.Parse("91");
                CHRT_GrayNPD.ChartAreas["Default"].Position.Height = float.Parse("92");

                CHRT_GrayNPD.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                CHRT_GrayNPD.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                CHRT_GrayNPD.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                CHRT_GrayNPD.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                CHRT_GrayNPD.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("80");

                CHRT_GrayNPD.ChartAreas["Default"].ShadowOffset = 0;
                CHRT_GrayNPD.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT_GrayNPD.ChartAreas["Default"].BorderWidth = 1;
                CHRT_GrayNPD.BorderSkin.FrameBorderWidth = 0;

                CHRT_GrayNPD.ChartAreas["Default"].AxisY.Minimum = Math.Round(NPDMinValue);
                CHRT_GrayNPD.ChartAreas["Default"].AxisY.Maximum = Math.Round(NPDMaxValue);
                


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    //if (Convert.ToInt64(DT.Rows[i]["GRAY"]) == 0)
                    //    AddPointChart(i, CHRT_GrayNPD.Series["Series1"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(), DT.Rows[i]["Density"].ToString(), true, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_L1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_A1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_B1"].ToString())));                    
                    //else
                    AddPointChart(i, CHRT_GrayNPD.Series["Series1"], Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString(), DT.Rows[i]["Density"].ToString(),Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_L1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_A1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_B1"].ToString())));                    

                }

                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_GrayNPD.ImageUrl) + "." + CHRT_GrayNPD.ImageType;
                CHRT_GrayNPD.Save(Server.MapPath(FileName));

            }
        }
        catch (Exception Exc)
        {

            throw Exc;
        }

    }   
    #endregion

    #region " ==== Black NPD ==== "
    private void BindBlackNPDGraph(DataTable DT)
    {
        try
        {
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                

                //========= Setting For Gray L Deviation Graph ==============//
                CHRT_BlackNPD.ChartAreas["Default"].Position.Auto = true;
                CHRT_BlackNPD.ChartAreas["Default"].Position.X = float.Parse("6");
                CHRT_BlackNPD.ChartAreas["Default"].Position.Y = float.Parse("7");
                CHRT_BlackNPD.ChartAreas["Default"].Position.Width = float.Parse("91");
                CHRT_BlackNPD.ChartAreas["Default"].Position.Height = float.Parse("92");

                CHRT_BlackNPD.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                CHRT_BlackNPD.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                CHRT_BlackNPD.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                CHRT_BlackNPD.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                CHRT_BlackNPD.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("80");

                CHRT_BlackNPD.ChartAreas["Default"].ShadowOffset = 0;
                CHRT_BlackNPD.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT_BlackNPD.ChartAreas["Default"].BorderWidth = 1;
                CHRT_BlackNPD.BorderSkin.FrameBorderWidth = 0;

                CHRT_BlackNPD.ChartAreas["Default"].AxisY.Minimum = Math.Round(NPDMinValue);
                CHRT_BlackNPD.ChartAreas["Default"].AxisY.Maximum = Math.Round(NPDMaxValue);
                


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToInt64(DT.Rows[i]["Black"]) == 0)
                        AddPointChartNPD(i, CHRT_BlackNPD.Series["Series1"], Convert.ToInt64(DT.Rows[i]["Black"]).ToString(), DT.Rows[i]["Density"].ToString(), true, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_L1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_A1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_B1"].ToString())));
                    else
                        AddPointChartNPD(i, CHRT_BlackNPD.Series["Series1"], Convert.ToInt64(DT.Rows[i]["Black"]).ToString(), DT.Rows[i]["Density"].ToString(), false, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_L1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_A1"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["LAB_B1"].ToString())));

                }

                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_BlackNPD.ImageUrl) + "." + CHRT_BlackNPD.ImageType;
                CHRT_BlackNPD.Save(Server.MapPath(FileName));

            }
        }
        catch (Exception Exc)
        {

            throw Exc;
        }

    }
    private void AddPointChartNPD(int _Index, Series _objSeries, string _XValue, string _YValue, bool IsPaper, Double L, Double A, Double B)
    {
        try
        {
            string strHTMLColor = Formulas.ColorCalcuations.LabtoRGB(L, A, B);

            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerSize = 4;
            if (IsPaper)
            {
                _objSeries.Points[_Index].MarkerColor = Color.White;
                _objSeries.Points[_Index].BorderColor = Color.Black;
            }
            else
                _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);// Color.FromArgb(200, 25, 25, 25);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 9;
            _objSeries.Points[_Index].BorderWidth = 2;
            _objSeries.Points[_Index].MarkerBorderColor = Color.Black;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region " ==== Actual Dot Percentage  ==== "
    private void BindDotgainActualGraph(DataTable DT)
    {
        try
        {
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                int C = 0;
                int M = 0;
                int Y = 0;
                int K = 0;

                //========= Setting For Gray L Deviation Graph ==============//
                CHRT_DotgainActual.ChartAreas["Default"].Position.Auto = true;
                CHRT_DotgainActual.ChartAreas["Default"].Position.X = float.Parse("3");
                CHRT_DotgainActual.ChartAreas["Default"].Position.Y = float.Parse("15");
                CHRT_DotgainActual.ChartAreas["Default"].Position.Width = float.Parse("91");
                CHRT_DotgainActual.ChartAreas["Default"].Position.Height = float.Parse("80");

                CHRT_DotgainActual.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                CHRT_DotgainActual.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                CHRT_DotgainActual.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                CHRT_DotgainActual.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                CHRT_DotgainActual.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("80");

                CHRT_DotgainActual.ChartAreas["Default"].ShadowOffset = 0;
                CHRT_DotgainActual.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT_DotgainActual.ChartAreas["Default"].BorderWidth = 1;
                CHRT_DotgainActual.BorderSkin.FrameBorderWidth = 0;


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_C"]) == 0 && Convert.ToInt16(DT.Rows[i]["CMYK_M"]) == 0 && Convert.ToInt16(DT.Rows[i]["CMYK_Y"]) == 0 && Convert.ToInt16(DT.Rows[i]["CMYK_K"]) == 0)
                    {
                        AddPointChartWithColor(C, CHRT_DotgainActual.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString());
                        AddPointChartWithColor(M, CHRT_DotgainActual.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_M"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString());
                        AddPointChartWithColor(Y, CHRT_DotgainActual.Series["Series3"], Convert.ToInt64(DT.Rows[i]["CMYK_Y"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString());
                        AddPointChartWithColor(K, CHRT_DotgainActual.Series["Series4"], Convert.ToInt64(DT.Rows[i]["CMYK_K"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Black.Name.ToString());
                        C++; M++; Y++; K++;

                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_C"]) != 0)
                    {
                        AddPointChartWithColor(C, CHRT_DotgainActual.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString());
                        C++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_M"]) != 0)
                    {
                        AddPointChartWithColor(M, CHRT_DotgainActual.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_M"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString());
                        M++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_Y"]) != 0)
                    {
                        AddPointChartWithColor(Y, CHRT_DotgainActual.Series["Series3"], Convert.ToInt64(DT.Rows[i]["CMYK_Y"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString());
                        Y++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_K"]) != 0)
                    {
                        AddPointChartWithColor(K, CHRT_DotgainActual.Series["Series4"], Convert.ToInt64(DT.Rows[i]["CMYK_K"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Black.Name.ToString());
                        K++;
                    }

                    

                }

                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DotgainActual.ImageUrl) + "." + CHRT_DotgainActual.ImageType;
                CHRT_DotgainActual.Save(Server.MapPath(FileName));

            }
        }
        catch (Exception Exc)
        {

            throw Exc;
        }

    }
    //Add Point with color
    private void AddPointChartWithColor(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;            
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)),Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));           
            _objSeries.Points[_Index].MarkerColor =  System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 9;
            _objSeries.Points[_Index].BorderWidth = 2;
            _objSeries.Points[_Index].MarkerBorderColor = Color.Black;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }    
    #endregion

    #region " ==== Dotgain Deviation === "
    private void BindDotgainDeviationGraph(DataTable DT)
    {
        Double TolValue = 0;
        Double CValue = 0;
        Double MaxValue = 0;
        Double MinValue = 0;

        CHART_DotgainDeviation.Series["Series1"].Points.Clear();

        //========Line Chart Series For Dispaly Line on 0 value ================//
        #region Line Chart Series For Dispaly Line on 0 value
        CHART_DotgainDeviation.Series["Series2"].Type = SeriesChartType.Line;
        CHART_DotgainDeviation.Series["Series2"].XAxisType = AxisType.Secondary;
        CHART_DotgainDeviation.Series["Series2"].Points.AddXY(0, 0);
        CHART_DotgainDeviation.Series["Series2"].Points[0].Color = Color.Green;
        CHART_DotgainDeviation.Series["Series2"].Points.AddXY(1, 0);
        CHART_DotgainDeviation.Series["Series2"].Points[1].Color = Color.Green;
        #endregion

        #region Threshold Value for Positive
        CHART_DotgainDeviation.Series["Series3"].Type = SeriesChartType.Line;
        #endregion

        #region Threshold Value for Nagative
        CHART_DotgainDeviation.Series["Series4"].Type = SeriesChartType.Line;
        #endregion

        if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
        {
            //========= Find Maximum And Minmum Value For Set Chart Property  ============//

            #region Find And Set Maximum And Minimum To Chart
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Deviation"]));
                if (CValue > MaxValue)
                    MaxValue = Convert.ToDouble(CValue);

                if (CValue < MinValue)
                    MinValue = Convert.ToDouble(CValue);

                if (TolValue < Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["DotGain_TH"])))
                    TolValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["DotGain_TH"]));
            }

            if(TolValue > MaxValue)
                MaxValue = TolValue;

            if (MinValue < 0) MinValue = (MinValue * -1);

            if (MaxValue > 2)
            {
                MaxValue += 2;
                MinValue += 2;
            }
            else
            {
                MaxValue += 0.5;
                MinValue += 0.5;
            }

            if (MaxValue > MinValue)
                MinValue = (MaxValue * -1);
            else
            {
                MaxValue = MinValue;
                MinValue = (MinValue * -1);
            }


            CHART_DotgainDeviation.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
            CHART_DotgainDeviation.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
            #endregion


            for (int i = 0; i < DT.Rows.Count; i++)
            {
                CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Deviation"]));
                if (Convert.ToInt16(DT.Rows[i]["CMYK_C"]) != 0)
                {
                    AddPoint(i, CHART_DotgainDeviation.Series["Series1"], "C", CValue.ToString(), Color.Cyan.Name.ToString());
                    AddPointChartRed(i, CHART_DotgainDeviation.Series["Series3"], CHART_DotgainDeviation.Series["Series4"], "C", DT.Rows[i]["DotGain_TH"].ToString(), DT.Rows[i]["Targ_DotGain"].ToString());
                }
                else if (Convert.ToInt16(DT.Rows[i]["CMYK_M"]) != 0)
                {
                    AddPoint(i, CHART_DotgainDeviation.Series["Series1"], "M", CValue.ToString(), Color.Magenta.Name.ToString());
                    AddPointChartRed(i, CHART_DotgainDeviation.Series["Series3"], CHART_DotgainDeviation.Series["Series4"], "M", DT.Rows[i]["DotGain_TH"].ToString(), DT.Rows[i]["Targ_DotGain"].ToString());
                }
                else if (Convert.ToInt16(DT.Rows[i]["CMYK_Y"]) != 0)
                {
                    AddPoint(i, CHART_DotgainDeviation.Series["Series1"], "Y", CValue.ToString(), Color.Yellow.Name.ToString());
                    AddPointChartRed(i, CHART_DotgainDeviation.Series["Series3"], CHART_DotgainDeviation.Series["Series4"], "Y", DT.Rows[i]["DotGain_TH"].ToString(), DT.Rows[i]["Targ_DotGain"].ToString());
                }
                else if (Convert.ToInt16(DT.Rows[i]["CMYK_K"]) != 0)
                {
                    AddPoint(i, CHART_DotgainDeviation.Series["Series1"], "K", CValue.ToString(), Color.Black.Name.ToString());
                    AddPointChartRed(i, CHART_DotgainDeviation.Series["Series3"], CHART_DotgainDeviation.Series["Series4"], "K", DT.Rows[i]["DotGain_TH"].ToString(), DT.Rows[i]["Targ_DotGain"].ToString());
                }
            }

        }
        else
        {
            CHART_DotgainDeviation.ChartAreas["Default"].AxisY.Minimum = -10;
            CHART_DotgainDeviation.ChartAreas["Default"].AxisY.Maximum = 10;

            AddPoint(0, CHART_DotgainDeviation.Series["Series1"], "C", "0", "#E9E9E9");
            AddPoint(1, CHART_DotgainDeviation.Series["Series1"], "M", "0", "#E9E9E9");
            AddPoint(2, CHART_DotgainDeviation.Series["Series1"], "Y", "0", "#E9E9E9");
            AddPoint(3, CHART_DotgainDeviation.Series["Series1"], "K", "0", "#E9E9E9");
        }

        // === Save Chart Image after generating image with proper data === //
        string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_DotgainDeviation.ImageUrl) + "." + CHART_DotgainDeviation.ImageType;
        CHART_DotgainDeviation.Save(Server.MapPath(FileName));
    }
    #endregion

    #region " ==== Density Deviation === "
    private void BindDensityDeviationGraph(DataTable DT)
    {
        Double TolValue = 0;
        Double CValue = 0;
        Double MaxValue = 0;
        Double MinValue = 0;

        CHRT_DensityDeviation.Series["Series1"].Points.Clear();

        //========Line Chart Series For Dispaly Line on 0 value ================//
        #region Line Chart Series For Dispaly Line on 0 value
        CHRT_DensityDeviation.Series["Series2"].Type = SeriesChartType.Line;
        CHRT_DensityDeviation.Series["Series2"].XAxisType = AxisType.Secondary;
        CHRT_DensityDeviation.Series["Series2"].Points.AddXY(0, 0);
        CHRT_DensityDeviation.Series["Series2"].Points[0].Color = Color.Green;
        CHRT_DensityDeviation.Series["Series2"].Points.AddXY(1, 0);
        CHRT_DensityDeviation.Series["Series2"].Points[1].Color = Color.Green;
        #endregion

        #region Threshold Value for Positive
        CHRT_DensityDeviation.Series["Series3"].Type = SeriesChartType.Line;        
        #endregion

        #region Threshold Value for Nagative
        CHRT_DensityDeviation.Series["Series4"].Type = SeriesChartType.Line;       
        #endregion

        if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
        {
            //========= Find Maximum And Minmum Value For Set Chart Property  ============//

            #region Find And Set Maximum And Minimum To Chart
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Dens_Deviation"]));
                if (CValue > MaxValue)
                    MaxValue = Convert.ToDouble(CValue);

                if (CValue < MinValue)
                    MinValue = Convert.ToDouble(CValue);

                if (TolValue < Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Density_TH"])))
                    TolValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Density_TH"]));
            }

            if (TolValue > MaxValue)
                MaxValue = TolValue;


            if (MinValue < 0) MinValue = (MinValue * -1);

            if (MaxValue > 2)
            {
                MaxValue += 2;
                MinValue += 2;
            }
            else
            {
                MaxValue += 0.5;
                MinValue += 0.5;
            }

            if (MaxValue > MinValue)
                MinValue = (MaxValue * -1);
            else
            {
                MaxValue = MinValue;
                MinValue = (MinValue * -1);
            }


            

            CHRT_DensityDeviation.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
            CHRT_DensityDeviation.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
            #endregion


            for (int i = 0; i < DT.Rows.Count; i++)
            {
                CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Dens_Deviation"]));
                if (DT.Rows[i]["ColorName"].ToString() == "Cyan")
                {                 
                    AddPoint(i, CHRT_DensityDeviation.Series["Series1"], "C", CValue.ToString(), Color.Cyan.Name.ToString());
                    AddPointChartRed(i, CHRT_DensityDeviation.Series["Series3"], CHRT_DensityDeviation.Series["Series4"], "C", DT.Rows[i]["Density_TH"].ToString(), DT.Rows[i]["Density_Targ"].ToString());
                }
                else if (DT.Rows[i]["ColorName"].ToString() == "Magenta")
                {                 
                    AddPoint(i, CHRT_DensityDeviation.Series["Series1"], "M", CValue.ToString(), Color.Magenta.Name.ToString());
                    AddPointChartRed(i, CHRT_DensityDeviation.Series["Series3"], CHRT_DensityDeviation.Series["Series4"], "M", DT.Rows[i]["Density_TH"].ToString(), DT.Rows[i]["Density_Targ"].ToString());
                }
                else if (DT.Rows[i]["ColorName"].ToString() == "Yellow")
                {                 
                    AddPoint(i, CHRT_DensityDeviation.Series["Series1"], "Y", CValue.ToString(), Color.Yellow.Name.ToString());
                    AddPointChartRed(i, CHRT_DensityDeviation.Series["Series3"], CHRT_DensityDeviation.Series["Series4"], "Y", DT.Rows[i]["Density_TH"].ToString(), DT.Rows[i]["Density_Targ"].ToString());
                }
                else if (DT.Rows[i]["ColorName"].ToString() == "Black")
                {                    
                    AddPoint(i, CHRT_DensityDeviation.Series["Series1"], "K", CValue.ToString(), Color.Black.Name.ToString());
                    AddPointChartRed(i, CHRT_DensityDeviation.Series["Series3"], CHRT_DensityDeviation.Series["Series4"], "K", DT.Rows[i]["Density_TH"].ToString(), DT.Rows[i]["Density_Targ"].ToString());
                }
            }           

        }
        else
        {
            CHRT_DensityDeviation.ChartAreas["Default"].AxisY.Minimum = -10;
            CHRT_DensityDeviation.ChartAreas["Default"].AxisY.Maximum = 10;

            AddPoint(0, CHRT_DensityDeviation.Series["Series1"], "C", "0", "#E9E9E9");
            AddPoint(1, CHRT_DensityDeviation.Series["Series1"], "M", "0", "#E9E9E9");
            AddPoint(2, CHRT_DensityDeviation.Series["Series1"], "Y", "0", "#E9E9E9");
            AddPoint(3, CHRT_DensityDeviation.Series["Series1"], "K", "0", "#E9E9E9");
        }

        // === Save Chart Image after generating image with proper data === //
        string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DensityDeviation.ImageUrl) + "." + CHRT_DensityDeviation.ImageType;
        CHRT_DensityDeviation.Save(Server.MapPath(FileName));
    }
    private void AddPoint(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName)
    {
        try
        {
            _objSeries.Points.AddXY(_XValue, _YValue);
            _objSeries.Points[_Index].Color = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].Href = "#";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AddPointChartRed(int _Index, Series _objPSeries, Series _objNSeries, string _XValue, string _YValue,string _YTarget)
    {
        try
        {
            Double PValue = 0;
            Double NValue = 0;
            Double YValue = Convert.ToDouble(_YValue);


            if (Convert.ToDouble(YValue) > 0)
            {
                PValue = Convert.ToDouble(YValue);
                NValue = PValue - ((PValue * 2));
            }
            else
            {
                NValue = Convert.ToDouble(YValue);
                PValue = NValue - ((NValue * 2));

            }

            _objPSeries.Type = SeriesChartType.Spline;
            _objPSeries.Points.AddXY(_XValue, Fuji.Common.CommonFunctions.DecimalRound(PValue));
            _objPSeries.Points[_Index].Color = Color.Red;
            _objPSeries.Points[_Index].BorderWidth = 2;

            _objNSeries.Type = SeriesChartType.Spline;
            _objNSeries.Points.AddXY(_XValue, Fuji.Common.CommonFunctions.DecimalRound(NValue));
            _objNSeries.Points[_Index].Color = Color.Red;
            _objNSeries.Points[_Index].BorderWidth = 2;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region " ==== Print Contrast Deviation === "
    private void BindPrintContrastDeviationGraph(DataTable DT)
    {
        Double TolValue = 0;
        Double CValue = 0;
        Double MaxValue = 0;
        Double MinValue = 0;

        CHRT_PrintContrastDeviation.Series["Series1"].Points.Clear();

        //========Line Chart Series For Dispaly Line on 0 value ================//
        #region Line Chart Series For Dispaly Line on 0 value
        CHRT_PrintContrastDeviation.Series["Series2"].Type = SeriesChartType.Line;
        CHRT_PrintContrastDeviation.Series["Series2"].XAxisType = AxisType.Secondary;
        CHRT_PrintContrastDeviation.Series["Series2"].Points.AddXY(0, 0);
        CHRT_PrintContrastDeviation.Series["Series2"].Points[0].Color = Color.Green;
        CHRT_PrintContrastDeviation.Series["Series2"].Points.AddXY(1, 0);
        CHRT_PrintContrastDeviation.Series["Series2"].Points[1].Color = Color.Green;
        #endregion

        #region Threshold Value for Positive
        CHRT_PrintContrastDeviation.Series["Series3"].Type = SeriesChartType.Line;
        #endregion

        #region Threshold Value for Nagative
        CHRT_PrintContrastDeviation.Series["Series4"].Type = SeriesChartType.Line;
        #endregion

        if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
        {
            //========= Find Maximum And Minmum Value For Set Chart Property  ============//

            #region Find And Set Maximum And Minimum To Chart
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(Formulas.DotGain.CalcPrintContrast(Convert.ToDecimal(DT.Rows[i]["D100"]),Convert.ToDecimal(DT.Rows[i]["D75"]))));
                CValue = CValue - Convert.ToDouble(DT.Rows[i]["Print_Contrast_Tol"]);  //change by CSS on 03/29/10
                if (CValue > MaxValue)
                    MaxValue = Convert.ToDouble(CValue);

                if (CValue < MinValue)
                    MinValue = Convert.ToDouble(CValue);

                if (TolValue < Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Print_Contrast_TH"])))
                    TolValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Print_Contrast_TH"]));
            }


            if (TolValue > MaxValue)
                MaxValue = TolValue;


            if (MinValue < 0) MinValue = (MinValue * -1);

            if (MaxValue > 2)
            {
                MaxValue += 2;
                MinValue += 2;
            }
            else
            {
                MaxValue += 0.5;
                MinValue += 0.5;
            }

            if (MaxValue > MinValue)
                MinValue = (MaxValue * -1);
            else
            {
                MaxValue = MinValue;
                MinValue = (MinValue * -1);
            }

            CHRT_PrintContrastDeviation.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
            CHRT_PrintContrastDeviation.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
            #endregion

            
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(Formulas.DotGain.CalcPrintContrast(Convert.ToDecimal(DT.Rows[i]["D100"]), Convert.ToDecimal(DT.Rows[i]["D75"]))));
                CValue = CValue - Convert.ToDouble(DT.Rows[i]["Print_Contrast_Tol"]); //change by CSS on 03/29/10

                if (DT.Rows[i]["Color"].ToString() == "Cyan")
                {
                        
                    AddPoint(i, CHRT_PrintContrastDeviation.Series["Series1"], "C", CValue.ToString(), Color.Cyan.Name.ToString());
                    AddPointChartRed(i, CHRT_PrintContrastDeviation.Series["Series3"], CHRT_PrintContrastDeviation.Series["Series4"], "C", DT.Rows[i]["Print_Contrast_TH"].ToString(), DT.Rows[i]["Print_Contrast_Tol"].ToString());
                }
                else if (DT.Rows[i]["Color"].ToString() == "Magenta")
                {
                    AddPoint(i, CHRT_PrintContrastDeviation.Series["Series1"], "M", CValue.ToString(), Color.Magenta.Name.ToString());
                    AddPointChartRed(i, CHRT_PrintContrastDeviation.Series["Series3"], CHRT_PrintContrastDeviation.Series["Series4"], "M", DT.Rows[i]["Print_Contrast_TH"].ToString(), DT.Rows[i]["Print_Contrast_Tol"].ToString());
                }
                else if (DT.Rows[i]["Color"].ToString() == "Yellow")
                {
                    AddPoint(i, CHRT_PrintContrastDeviation.Series["Series1"], "Y", CValue.ToString(), Color.Yellow.Name.ToString());
                    AddPointChartRed(i, CHRT_PrintContrastDeviation.Series["Series3"], CHRT_PrintContrastDeviation.Series["Series4"], "Y", DT.Rows[i]["Print_Contrast_TH"].ToString(), DT.Rows[i]["Print_Contrast_Tol"].ToString());
                }
                else if (DT.Rows[i]["Color"].ToString() == "Black")
                {
                    AddPoint(i, CHRT_PrintContrastDeviation.Series["Series1"], "K", CValue.ToString(), Color.Black.Name.ToString());
                    AddPointChartRed(i, CHRT_PrintContrastDeviation.Series["Series3"], CHRT_PrintContrastDeviation.Series["Series4"], "K", DT.Rows[i]["Print_Contrast_TH"].ToString(), DT.Rows[i]["Print_Contrast_Tol"].ToString());
                }
            }

        }
        else
        {
            CHRT_PrintContrastDeviation.ChartAreas["Default"].AxisY.Minimum = -10;
            CHRT_PrintContrastDeviation.ChartAreas["Default"].AxisY.Maximum = 10;

            AddPoint(0, CHRT_PrintContrastDeviation.Series["Series1"], "C", "0", "#E9E9E9");
            AddPoint(1, CHRT_PrintContrastDeviation.Series["Series1"], "M", "0", "#E9E9E9");
            AddPoint(2, CHRT_PrintContrastDeviation.Series["Series1"], "Y", "0", "#E9E9E9");
            AddPoint(3, CHRT_PrintContrastDeviation.Series["Series1"], "K", "0", "#E9E9E9");
        }

        // === Save Chart Image after generating image with proper data === //
        string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_PrintContrastDeviation.ImageUrl) + "." + CHRT_PrintContrastDeviation.ImageType;
        CHRT_PrintContrastDeviation.Save(Server.MapPath(FileName));
    }
    #endregion

}
