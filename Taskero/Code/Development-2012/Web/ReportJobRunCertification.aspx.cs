using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Fuji.Common;
using Fuji.Business;
using Fuji.DataAccess;
using Telerik.WebControls;
using Dundas.Charting.WebControl;
using System.IO;
using System.Drawing;

public partial class ReportJobRunCertification : PageBase
{
    private const int _DefaultPageSize = 25;
    private enum enumGridColumn
    {
        Status = 2,
        hdnSatus = 3,
        View = 4,
        Job = 5,
        PrintTarget = 6,
        DeviceDesc = 7,
        DongleDescription = 8,
        BranchName = 9,
        DeviceType = 10,
        DateTime = 11,
        Date = 12,
        TIME = 13,
        GUID = 14,
        CPMeasurementID = 15,
        CompanyID = 16,
        CompanyName = 17,
        BranchID = 18,
        DongleID = 19,
        DeviceID = 20,
        CPDevicePrintTargetID = 21,
        Customer = 22,
        CPDeviceTypeID = 23,
        StatusID = 24,
        CPSerialNUM = 25

    }
    private enum enumJobTypeID
    {
        CMYK_Spot = 1,
        G7 = 2,
        NonCMYK = 3
    }

    #region " === Private Property === "
    public int paraJobID { get; set; }
    public string paraDongleID { get; set; }
    public bool paraIsNonProcess { get; set; }// If nonProcess then "true" else "false"
    public bool paraIsShowError { get; set; }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void chkShowErrors_CheckedChanged(object sender, EventArgs e)
    {
        GridJobReportPress.Rebind();
    }
    #region Validations
    protected void TXT_DateText(object sender, EventArgs e)
    {
        if (Fuji.Common.Validations.IsDateTime(((TextBox)sender).Text) == false)
        { LblError.Text = "Please enter a valid date!"; ((TextBox)sender).Text = ""; return; }
    }
    #endregion
    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string _SelectedValue;
            _SelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = _SelectedValue;

            //====  Set No of Record Per Page for Press Grid  ====//
            GridJobReportPress.PageSize = Convert.ToInt32(_SelectedValue);
            GridJobReportPress.CurrentPageIndex = 0;

            //====  Bind Press Grid  ====//
            GridJobReportPress.Rebind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void GridJobReportPress_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
    {
    }
    protected void GridJobReportPress_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
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
                _FirstIndexInPage = GridJobReportPress.MasterTableView.PagingManager.FirstIndexInPage;
                _LastIndexInPage = GridJobReportPress.MasterTableView.PagingManager.LastIndexInPage + 1;
                _DataSourceCount = GridJobReportPress.MasterTableView.PagingManager.DataSourceCount;

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
                if (GridJobReportPress.MasterTableView.PagingManager.IsFirstPage)
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
                if (GridJobReportPress.MasterTableView.PagingManager.IsLastPage == true)
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
                if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Replace(" ", "").Trim() == Fuji.Common.Enums.enumJobStatus.MakeReady.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/Makeready.png"; //========  Make Ready Then Red Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumJobStatus.MakeReady.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumJobStatus.Production.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/Production.png";//========  Production Then Yellow Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumJobStatus.Production.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumJobStatus.Complete.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/Complete.png";//========  Complete Then Green Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumJobStatus.Complete.ToString());
                }
                if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                { ibtnStatus.Visible = false; }
                #endregion

                #region ====  View Setting  ====


                //========  Bind String For QueryString's Parameters ========//

                Int32 intDeviceType = Convert.ToInt32(((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCPDeviceTypeID")).Text);
                string strGUID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblGUID")).Text;
                string strCPDevicePrintTargetID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCPDevicePrintTargetID")).Text;
                string strCPDeviceID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCPDeviceID")).Text;
                string strCPSerialNUM = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCPSerialNUM")).Text;
                string strCPMeasurementID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblCPMeasurementID")).Text;

                //===========: Updated By : Manish(06-02-2009) - If JobTypeID = 3 then its non Processed Type CMYK :==================//
                string strJobTypeID = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lblJobTypeID")).Text;
                strJobTypeID = strJobTypeID.Trim() == "" ? "0" : strJobTypeID.Trim();


                System.Web.UI.HtmlControls.HtmlImage imgCreateReport;
                imgCreateReport = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("imgCreateReport");
                string _NewId = e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text.Replace("-", "");


                if (int.Parse(strJobTypeID) == (int)enumJobTypeID.NonCMYK)
                    imgCreateReport.Attributes.Add("onclick", "OpenMeasurementWindow('" + strCPMeasurementID + "','" + strCPSerialNUM + "','1');");
                else
                    imgCreateReport.Attributes.Add("onclick", "OpenMeasurementWindow('" + strCPMeasurementID + "','" + strCPSerialNUM + "','0');");


                //Hide Views Button
                if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim().Replace(" ", "") == Fuji.Common.Enums.enumJobStatus.MakeReady.ToString())
                {
                    //ImgView.Visible = false;
                    //ImgView_New.Visible = false;

                }

                #region Hide ImgView_New if Press
                //if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                //{ ImgView_New.Visible = false; }
                #endregion

                #endregion

            }
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.FilteringItem)
            {
                Control FirstSheetControl = e.Item.FindControl("ComboFirstSheet");
                Button FirstSheetGo = (Button)FirstSheetControl.FindControl("Button1");
                RadDatePicker FirstSheetRDP_Date = (RadDatePicker)FirstSheetControl.FindControl("RadDatePicker1");
                FirstSheetGo.CommandName = "FirstSheet";
                Fuji.Common.Sessions.SetSessions("StartDate", null);
                FirstSheetRDP_Date.Clear();

                Control LastSheetControl = e.Item.FindControl("ComboLastSheet");
                Button LastSheetGo = (Button)LastSheetControl.FindControl("Button1");
                RadDatePicker LastSheetRDP_Date = (RadDatePicker)LastSheetControl.FindControl("RadDatePicker1");
                LastSheetGo.CommandName = "LastSheet";
                Fuji.Common.Sessions.SetSessions("StartDate2", null);
                LastSheetRDP_Date.Clear();
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void GridJobReportPress_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
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
    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            //====  Clear Filter Controls for Press Grid  ====//
            foreach (Telerik.WebControls.GridColumn column in GridJobReportPress.MasterTableView.Columns)
            {
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }

            Fuji.Common.Sessions.SetSessions("StartDate", null);

            //=== Set For Press Grid

            //====  Set Filter Empty  ====//
            GridJobReportPress.MasterTableView.FilterExpression = String.Empty;

            //====  Rebind Master View  ====//
            GridJobReportPress.MasterTableView.Rebind();

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void GridJobReportPress_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            DataTable dt;
            //========  Session Store in DataTable  ========//

            if (ViewState["DSGridJobReportPress"] == null)
            {
                SqlCommand CmdJobReport = new SqlCommand();
                CmdJobReport.CommandType = CommandType.StoredProcedure;
                CmdJobReport.CommandText = "Report.usp_Press_GetJobRunCetificationData";

                if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                    CmdJobReport.Parameters.AddWithValue("@TaskeroUserID", 0); // 0 as Admin in SP
                else
                    CmdJobReport.Parameters.AddWithValue("@TaskeroUserID", Sessions.UserID);

                dt = (new Fuji.DataAccess.Database()).GetDataTable(CmdJobReport);
                ViewState["DSGridJobReportPress"] = dt;
            }
            else
            {
                dt = (DataTable)(ViewState["DSGridJobReportPress"]);
            }

            //========  Grid Bind  ========//
            GridJobReportPress.DataSource = dt.DefaultView;
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void BtnCreateReport_Click(object sender, EventArgs e)
    {
        try
        {
            paraJobID = HDFJobID.Value.Trim() == "" ? 0 : Convert.ToInt16(HDFJobID.Value.Trim());
            paraDongleID = Convert.ToString(HDFDongalID.Value);
            if (Convert.ToString(HDFIsNonProcess.Value) == "1")
                paraIsNonProcess = true;
            else
                paraIsNonProcess = false;

            paraIsShowError = chkShowErrors.Checked;

            // === Fill Chart For Report === //

            CreateReport();

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    #region Fill Charts
    private void CreateReport()
    {
        try
        {
            ReportPressJobRunCertificationData objJobRun = new ReportPressJobRunCertificationData();
            objJobRun.JobID = paraJobID;
            objJobRun.DongleID = paraDongleID;
            DataSet dsABPlot;// = objJobRun.GetJobInformation_P();
            DataSet dsResultSummary;
            DataSet dsMetrik;
            if (paraIsNonProcess)
            {
                dsABPlot = objJobRun.GetJobInformation_NP();
                dsResultSummary = objJobRun.GetJobDeltaESummary_NP();
                dsMetrik = objJobRun.GetJobMetrics_NP();
            }
            else
            {
                dsABPlot = objJobRun.GetJobInformation_P();
                dsResultSummary = objJobRun.GetJobDeltaESummary_P();
                dsMetrik = objJobRun.GetJobMetrics_P();
            }

            if (dsABPlot.Tables.Count >= 2)
            {
                Bind_TargetVersusActual_CHART(dsABPlot.Tables[2]);
            }

            #region " === Page : 1 Result Summary === "
            if (dsResultSummary != null)
                Bind_ResultSummary(dsResultSummary);
            #endregion

            #region " === Page : 2 Gray Balance Page This Page Is Only For Process === "
            DataSet dsGrayBalance = new DataSet();
            if (!paraIsNonProcess) //2009_06_15 by Softweb - Ajay
            {
                dsGrayBalance = objJobRun.GetColorCharsGray_P();
                if (dsGrayBalance != null)
                    Bind_GrayBalancePage(dsGrayBalance);
            }
            else
            {
                dsGrayBalance = null; //2009_06_15 by Softweb - Ajay
                Bind_GrayBalancePage(null); //2009_06_15 by Softweb - Ajay
            }
            #endregion

            #region " === Page : 3 Metrik(For Process) Page === "
            if (dsMetrik != null)
                Bind_Metric(dsMetrik);
            #endregion

            FillTrendsCharts();

            //FillChart();


            DataSet DS = new DataSet();
            DataTable dt = new DataTable();
            // === -------------- Page : 1 Tables Add To PDF DataSet --------------- === //
            //Table=0 : Job Detail, Table=1 : Ink Information, Table=2 : AB Plot
            dt = dsABPlot.Tables[0].Copy();
            dt.TableName = "JobDetail"; //2009_06_15 by Softweb - Ajay
            DS.Tables.Add(dt);//Table 0

            dt = dsABPlot.Tables[1].Copy();
            dt.TableName = "InkInformation"; //2009_06_15 by Softweb - Ajay
            DS.Tables.Add(dt);//Table 1

            if (dsABPlot.Tables.Count >= 2)
            {
                dt = dsABPlot.Tables[2].Copy();
                dt.TableName = "ABPlot"; //2009_06_15 by Softweb - Ajay
                DS.Tables.Add(dt);//Table 2
            }

            // Table = 1 : ErrorRate, Table = 2 : DeltaE Summary For Job
            dt = dsResultSummary.Tables[0].Copy();
            dt.TableName = "ErrorRate";
            DS.Tables.Add(dt);//Table 3

            dt = dsResultSummary.Tables[1].Copy();
            dt.TableName = "DeltaESummary";
            DS.Tables.Add(dt);//Table 4

            //2009_06_15 by Softweb - Ajay ====== Start Comment === 
            // === -------------- Page : 2 Tables Add To PDF DataSet--------------- === //
            //if (!paraIsNonProcess) // Is data is only for Process
            //{
            //    // === Gray Balance AB 25, 50, 75 === //
            //    dt = dsGrayBalance.Tables[1].Copy(); 
            //    dt.TableName = "GrayBalance25";
            //    DS.Tables.Add(dt);//Table 5

            //    dt = dsGrayBalance.Tables[2].Copy();
            //    dt.TableName = "GrayBalance50";
            //    DS.Tables.Add(dt);//Table 6

            //    dt = dsGrayBalance.Tables[3].Copy();
            //    dt.TableName = "GrayBalance75";
            //    DS.Tables.Add(dt);//Table 7

            //    dt = dsGrayBalance.Tables[4].Copy();
            //    dt.TableName = "GrayNPD";
            //    DS.Tables.Add(dt);//Table 8

            //    dt = dsGrayBalance.Tables[5].Copy();
            //    dt.TableName = "K_NPD";
            //    DS.Tables.Add(dt);//Table 9
            //}
            //2009_06_15 by Softweb - Ajay ====== End Comment === 

            // === -------------- Page : 3 Metrics Page --------------- === //
            DataTable myDt = new DataTable();
            DataColumn dc;
            DataRow dr = myDt.NewRow();
            //myDt.TableName = "IsNonProcessANDIsShowError";  //2009_06_15 by Softweb - Ajay
            myDt.TableName = "IsShowError";
            //dc = new DataColumn("IsNonProcess"); //2009_06_15 by Softweb - Ajay
            //myDt.Columns.Add(dc); //2009_06_15 by Softweb - Ajay
            dc = new DataColumn("IsShowError");
            myDt.Columns.Add(dc);
            //dr["IsNonProcess"] = paraIsNonProcess ? "1" : "0"; //2009_06_15 by Softweb - Ajay
            dr["IsShowError"] = paraIsShowError ? "1" : "0";
            myDt.Rows.Add(dr);

            // if Job Is Process Then Table 10 else Table 5
            DS.Tables.Add(myDt);

            CreateReportPDF(DS);
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
            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            if (paraIsNonProcess)
                objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.ReportJobRunCertificationNonProcess;
            else
                objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.ReportJobRunCertificationProcess;


            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_ReportJobRunCertification.PDF";
            objReportPDF.DS_Common = Ds;
            //objReportPDF.DS_Common = null;
            objReportPDF.MakePDF();

            // === After generat and save PDF file, Download it at a time === //
            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    #region " === Page : 1 Bar Charts === "
    private void Bind_ResultSummary(DataSet DS)
    {
        try
        {
            //if (DS.Tables[0] != null)
            BindBarChartError(ChartResultSummary_PatchError, DS.Tables[0]);

            //if (DS.Tables[1] != null)
            BindBarChart(ChartResultSummary_DeltaEValue, DS.Tables[1], 0, 15);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void BindBarChartError(Chart CHRT, DataTable DT)
    {
        try
        {
            CHRT.BorderColor = Color.White;
            CHRT.BorderWidth = 0;
            CHRT.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            CHRT.Visible = true;
            CHRT.Series[0].Points.Clear();


            #region //========== Declarations ==========//

            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)CHRT.Series[0];
            objSeries0.XValueType = ChartValueTypes.Int;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Bar;
            //objSeries0.Color = Color.Gray;
            objSeries0.BackGradientType = GradientType.DiagonalRight;


            // === Make Chart area look like 2D even it is 3D === //
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            CHRT.ChartAreas["ChartArea1"].BackColor = Color.White;
            CHRT.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            CHRT.ChartAreas["ChartArea1"].AxisY.Maximum = 100;
            #endregion

            #region //========== ChartArea Settings ==========//
            CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

            CHRT.ChartAreas["ChartArea1"].Position.Auto = true;
            CHRT.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            CHRT.ChartAreas["ChartArea1"].Position.Y = float.Parse("20");
            CHRT.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            CHRT.ChartAreas["ChartArea1"].Position.Height = float.Parse("70");

            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("91");

            CHRT.ChartAreas["ChartArea1"].ShadowOffset = 0;
            CHRT.BorderSkin.SkinStyle = BorderSkinStyle.None;
            CHRT.ChartAreas["ChartArea1"].BorderWidth = 0;
            CHRT.BorderSkin.FrameBorderWidth = 0;

            #endregion

            #region " === Fill Series === "
            //========== Loop and add values ==========//
            Double Y1 = (DT == null || DT.Rows.Count == 0) ? 0 : Convert.ToDouble(DT.Rows[0]["ErrorRate"].ToString());

            objSeries0.Points.AddXY((Double)0, Y1);
            objSeries0.Points[0]["PointWidth"] = "1.0";

            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT.ImageUrl) + "." + CHRT.ImageType;
            CHRT.Save(Server.MapPath(FileName));
            #endregion
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
            CHRT.BorderColor = Color.White;
            CHRT.BorderWidth = 0;
            CHRT.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            CHRT.Visible = true;
            CHRT.Series[0].Points.Clear();
            CHRT.Series[1].Points.Clear();
            CHRT.Series[2].Points.Clear();

            #region //========== Declarations ==========//
            int PointIndex = 0;
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)CHRT.Series[0];
            objSeries0.XValueType = ChartValueTypes.Int;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Bar;
            //objSeries0.Color = Color.Gray;
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)CHRT.Series[1];
            objSeries1.XValueType = ChartValueTypes.Int;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Bar;

            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)CHRT.Series[2];
            objSeries2.XValueType = ChartValueTypes.Int;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Bar;

            // === Series3 to display Yellow marker on Thresold value === //
            Series objSeries3 = (Series)CHRT.Series[3];
            objSeries3.XValueType = ChartValueTypes.Int;
            objSeries3.YValueType = ChartValueTypes.String;
            objSeries3.ShowInLegend = false;
            objSeries3.Type = SeriesChartType.Bar;

            // === Make Chart area look like 2D even it is 3D === //
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            CHRT.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            CHRT.ChartAreas["ChartArea1"].BackColor = Color.White;
            #endregion

            #region //========== ChartArea Settings ==========//
            CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            CHRT.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

            CHRT.ChartAreas["ChartArea1"].Position.Auto = true;
            CHRT.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            CHRT.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
            CHRT.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            CHRT.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            CHRT.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

            CHRT.ChartAreas["ChartArea1"].ShadowOffset = 0;
            CHRT.BorderSkin.SkinStyle = BorderSkinStyle.None;
            CHRT.ChartAreas["ChartArea1"].BorderWidth = 0;
            CHRT.BorderSkin.FrameBorderWidth = 0;



            #endregion

            #region " === Fill Series === "
            //========== Loop and add values ==========//

            string strHTMLColor;
            Double Y1, Y2, Y3;
            //for (int i = 0; i < DT.Rows.Count; i = i + 1)
            for (int i = _StartIndex; i < _EndIndex; i = i + 1)
            {

                // === Limit with red marker === //
                if (DT != null && i < DT.Rows.Count)
                {
                    Y1 = (Convert.ToString(DT.Rows[i]["DeltaE_L"]) == "" ? 0 : Convert.ToDouble(DT.Rows[i]["DeltaE_L"].ToString()));
                    Y2 = (Convert.ToString(DT.Rows[i]["AvgDeltaE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[i]["AvgDeltaE"].ToString()));
                    // === Caution with Yellow marker === //
                    Y3 = (Convert.ToString(DT.Rows[i]["DeltaE_C"]) == "" ? 0 : Convert.ToDouble(DT.Rows[i]["DeltaE_C"].ToString()));
                    strHTMLColor = Convert.ToString(DT.Rows[i]["HTMLColor"]);
                }
                else
                {
                    Y1 = Y2 = Y3 = 0;
                    strHTMLColor = "";
                }
                // === When Actual value is not available then no series will display and no marker will display
                if (Y2 == 0)
                    Y1 = Y2;



                Series SeriesActual = objSeries1;
                if (!paraIsShowError)
                { Y1 = 0; }


                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[PointIndex]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);
                objSeries2.Points.AddXY((Double)0, Y1);
                objSeries3.Points.AddXY((Double)0, Y3);
                objSeries0.Points[PointIndex].BackGradientType = GradientType.DiagonalRight;


                SeriesActual.Points[PointIndex].Color = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                SeriesActual.Points[PointIndex]["PointWidth"] = "0.8";
                //SeriesActual.Points[PointIndex].Href = "#";

                if (Y1 == 0 && Y2 == 0)
                {
                    objSeries0.Points[PointIndex].MarkerImage = "";
                    objSeries2.Points[PointIndex].MarkerImage = "";
                }

                //========== Increase Point Index ==========//
                PointIndex = PointIndex + 1;
            }

            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT.ImageUrl) + "." + CHRT.ImageType;
            CHRT.Save(Server.MapPath(FileName));
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion

    #region " === Page 2 : Gray Balance Page === "
    private void Bind_GrayBalancePage(DataSet DS)
    {
        try
        {
            int intTableIndex = 0; // 0 Table is for Paper Patch

            intTableIndex = 1;
            if (DS != null && DS.Tables[intTableIndex] != null) // === 25%
                Fill_GrayBalanceChart(CHART_AB25, DS.Tables[intTableIndex]);
            else
                Fill_GrayBalanceChart(CHART_AB25, null);

            intTableIndex = 2;
            if (DS != null && DS.Tables[intTableIndex] != null) // === 50%
                Fill_GrayBalanceChart(CHART_AB50, DS.Tables[intTableIndex]);
            else
                Fill_GrayBalanceChart(CHART_AB50, null);


            intTableIndex = 3;
            if (DS != null && DS.Tables[intTableIndex] != null) // === 75%
                Fill_GrayBalanceChart(CHART_AB75, DS.Tables[intTableIndex]);
            else
                Fill_GrayBalanceChart(CHART_AB75, null);

            // === Fill Gray Balance L Chart === //
            if (DS != null && DS.Tables[0] != null && DS.Tables[1] != null && DS.Tables[2] != null && DS.Tables[3] != null)
                FillChartGrayBalanceL(DS);
            else
                FillChartGrayBalanceL(null);

            intTableIndex = 4;
            if (DS != null && DS.Tables[intTableIndex] != null) // === Gray NPD
                Fill_GrayNPD(CHRT_GrayNPD, DS.Tables[intTableIndex]);
            else
                Fill_GrayNPD(CHRT_GrayNPD, null);


            intTableIndex = 5;
            if (DS != null && DS.Tables[intTableIndex] != null) // === K NPD	(Black)
                Fill_KNPD(DS.Tables[intTableIndex]);
            else
                Fill_KNPD(null);

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillABCharts(Chart CHRT_AB, Double A1, Double B1, Double A2, Double B2)
    {
        try
        {
            CHRT_AB.Series[0].ToolTip = "Target(a) : " + A1 + " | Target(b) : " + B1;
            CHRT_AB.Series[1].ToolTip = "Actual(a) : " + A2 + " | Actual(b) : " + B2;

            CHRT_AB.Series[0].Points.AddXY(A1, B1);
            CHRT_AB.Series[0].Type = SeriesChartType.Point;
            CHRT_AB.Series[0].Href = "#";

            CHRT_AB.Series[1].Points.AddXY(A2, B2);
            CHRT_AB.Series[1].Type = SeriesChartType.Point;
            CHRT_AB.Series[1].Label = "a*=" + Math.Round(A2).ToString() + ",b*=" + Math.Round(B2).ToString();
            CHRT_AB.Series[1].Href = "#";

            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_AB.ImageUrl) + "." + CHRT_AB.ImageType;
            CHRT_AB.Save(Server.MapPath(FileName));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void Fill_GrayBalanceChart(Chart CHART, DataTable dt)
    {
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                FillABCharts(CHART
                            , Convert.ToDouble(dt.Rows[0]["Target_A"]), Convert.ToDouble(dt.Rows[0]["Target_B"])
                            , Convert.ToDouble(dt.Rows[0]["Actual_A"]), Convert.ToDouble(dt.Rows[0]["Actual_B"]));
            }
            else
                CHART.BackImage = "~/Images/FarbKreis_LAB3_150_Gray.gif";

            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART.ImageUrl) + "." + CHART.ImageType;
            CHART.Save(Server.MapPath(FileName));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void Fill_KNPD(DataTable dt)// === K(Black) NPD
    {
        try
        {
            CHRT_KNPD.Series[0].Type = SeriesChartType.Spline;
            CHRT_KNPD.Series[1].Type = SeriesChartType.Spline;

            CHRT_KNPD.Series[0].MarkerSize = 10;
            CHRT_KNPD.Series[1].MarkerSize = 10;


            CHRT_KNPD.ChartAreas["Default"].Position.Auto = true;
            CHRT_KNPD.ChartAreas["Default"].Position.X = float.Parse("20");
            CHRT_KNPD.ChartAreas["Default"].Position.Y = float.Parse("20");
            CHRT_KNPD.ChartAreas["Default"].Position.Width = float.Parse("91");
            CHRT_KNPD.ChartAreas["Default"].Position.Height = float.Parse("91");

            CHRT_KNPD.ChartAreas["Default"].InnerPlotPosition.Auto = true;
            CHRT_KNPD.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
            CHRT_KNPD.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
            CHRT_KNPD.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("91");
            CHRT_KNPD.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("85");

            CHRT_KNPD.ChartAreas["Default"].ShadowOffset = 0;
            CHRT_KNPD.BorderSkin.SkinStyle = BorderSkinStyle.None;
            CHRT_KNPD.ChartAreas["Default"].BorderWidth = 0;
            CHRT_KNPD.BorderSkin.FrameBorderWidth = 0;


            if (dt != null)
            {
                DataView DV8 = dt.DefaultView;
                if (DV8.Count > 0)
                {
                    for (int i = 0; i < DV8.Count; i = i + 1)
                    {
                        CHRT_KNPD.Series[0].Points.AddXY(DV8[i]["XValue"].ToString(), DV8[i]["Density"].ToString());
                        CHRT_KNPD.Series[1].Points.AddXY(DV8[i]["XValue"].ToString(), DV8[i]["TargetDensity"].ToString());
                        if (DV8[i]["ColorName"].ToString() == "PAPER")
                        {
                            CHRT_KNPD.Series[0].Points[i].MarkerColor = Color.White;
                            CHRT_KNPD.Series[0].Points[i].BorderColor = Color.Black;
                            CHRT_KNPD.Series[1].Points[i].MarkerColor = Color.White;
                            CHRT_KNPD.Series[1].Points[i].BorderColor = Color.Black;
                        }
                    }
                }
            }
            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_KNPD.ImageUrl) + "." + CHRT_KNPD.ImageType;
            CHRT_KNPD.Save(Server.MapPath(FileName));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void Fill_GrayNPD(Chart CHARTs, DataTable dt)
    {
        try
        {
            CHARTs.Series[0].Type = SeriesChartType.Spline;
            CHARTs.Series[1].Type = SeriesChartType.Spline;

            CHARTs.Series[0].MarkerSize = 10;
            CHARTs.Series[1].MarkerSize = 10;


            CHARTs.ChartAreas["Default"].Position.Auto = true;
            CHARTs.ChartAreas["Default"].Position.X = float.Parse("20");
            CHARTs.ChartAreas["Default"].Position.Y = float.Parse("20");
            CHARTs.ChartAreas["Default"].Position.Width = float.Parse("91");
            CHARTs.ChartAreas["Default"].Position.Height = float.Parse("91");

            CHARTs.ChartAreas["Default"].InnerPlotPosition.Auto = true;
            CHARTs.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
            CHARTs.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
            CHARTs.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("91");
            CHARTs.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("85");

            CHARTs.ChartAreas["Default"].ShadowOffset = 0;
            CHARTs.BorderSkin.SkinStyle = BorderSkinStyle.None;
            CHARTs.ChartAreas["Default"].BorderWidth = 0;
            CHARTs.BorderSkin.FrameBorderWidth = 0;

            if (dt != null)
            {
                DataView DV = dt.DefaultView;
                if (DV.Count > 0)
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {

                        CHARTs.Series[0].Points.AddXY(DV[i]["XValue"].ToString(), DV[i]["Density"].ToString());
                        CHARTs.Series[0].Points[i].ToolTip = "Color: " + DV[i]["ColorName"].ToString() + ", Density: " + DV[i]["Density"].ToString();
                        CHARTs.Series[0].Points[i].Href = "#";

                        //if value is not zero then plot the target value
                        if (Convert.ToDouble(DV[i]["TargetDensity"]) != 0.0)
                        {
                            CHARTs.Series[1].Points.AddXY(DV[i]["XValue"].ToString(), DV[i]["TargetDensity"].ToString());
                            CHARTs.Series[1].Points[i].ToolTip = "Color: " + DV[i]["ColorName"].ToString() + ", Neutral Density:" + DV[i]["TargetDensity"].ToString();
                            CHARTs.Series[1].Points[i].Href = "#";
                        }
                        if (DV[i]["ColorName"].ToString() == "PAPER")
                        {
                            CHARTs.Series[0].Points[i].MarkerColor = Color.White;
                            CHARTs.Series[0].Points[i].BorderColor = Color.Black;
                            CHARTs.Series[1].Points[i].MarkerColor = Color.White;
                            CHARTs.Series[1].Points[i].BorderColor = Color.Black;
                        }
                    }
            }
            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHARTs.ImageUrl) + "." + CHARTs.ImageType;
            CHARTs.Save(Server.MapPath(FileName));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region " === Page 3 : Metrics Page === "
    private void Bind_Metric(DataSet DS)
    {
        int intTable;
        Decimal CValue = 0;
        Double MaxValue = 0;
        Double MinValue = 0;
        string FileName = null;
        DataTable DT = null;

        #region " === Table : 0 -- Job DeltaE Summary === "
        //intTable = 0;
        //DataTable DT = DS.Tables[intTable];
        //if (DT != null)
        //{
        //    CHRT_TrendDeltaE.Series["Series1"].Points.Clear();
        //    CHRT_TrendDeltaE.Series["Series2"].Points.Clear();
        //    for (int i = 0; i < DT.Rows.Count; i = i + 1)
        //    {
        //        AddPointToTrendDeltaE(i, CHRT_TrendDeltaE.Series["Series1"], "Sheet Number", "", "Average Delta E", DT.Rows[i]["AvgDeltaE"].ToString());
        //        AddPointToTrendDeltaE(i, CHRT_TrendDeltaE.Series["Series2"], "Sheet Number", "", "Maximum Delta E", DT.Rows[i]["MaxDeltaE"].ToString());
        //        //AddPointToTrendDeltaE(i, CHRT_TrendDeltaE.Series["Series3"], "Sheet Number", DT.Rows[i]["SheetNumber"].ToString(), "Standard Deviation", DT.Rows[i]["StdDevdeltaE"].ToString());
        //    }
        //}
        //// === Save Chart Image after generating image with proper data === //
        //string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_TrendDeltaE.ImageUrl) + "." + CHRT_TrendDeltaE.ImageType;
        //CHRT_TrendDeltaE.Save(Server.MapPath(FileName));

        #endregion


        #region " === Table : 0 -- Job Zone Delta E Summary === "
        intTable = 0;
        DT = DS.Tables[intTable];
        if (DT != null && DT.Rows.Count > 0)
        {
            CHRT_ZoneDeltaE.Series["Series1"].Points.Clear();
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                AddPointToZoneDeltaE(i, CHRT_ZoneDeltaE.Series["Series1"], "Zone Number", DT.Rows[i]["ZoneNumber"].ToString(), "Average Delta E", DT.Rows[i]["AvgDeltaE"].ToString(), DT.Rows[i]["ZoneColor"].ToString());
            }
        }
        else
        {
            AddPointToZoneDeltaE(0, CHRT_ZoneDeltaE.Series["Series1"], "Zone Number", "0", "Average Delta E", "0", "Gray");
        }

        // === Save Chart Image after generating image with proper data === //
        FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_ZoneDeltaE.ImageUrl) + "." + CHRT_ZoneDeltaE.ImageType;
        CHRT_ZoneDeltaE.Save(Server.MapPath(FileName));
        #endregion

        if (!paraIsNonProcess)
        {

            #region " === Table : 1 -- Density Deviation Chart === "
            intTable = 1;
            DT = DS.Tables[intTable];
            CHRT_DensityDeviationP.Series["Series1"].Points.Clear();

            //========Line Chart Series For Dispaly Line on 0 value ================//
            #region Line Chart Series For Dispaly Line on 0 value
            CHRT_DensityDeviationP.Series["Series2"].Type = SeriesChartType.Line;
            CHRT_DensityDeviationP.Series["Series2"].XAxisType = AxisType.Secondary;
            CHRT_DensityDeviationP.Series["Series2"].Points.AddXY(0, 0);
            CHRT_DensityDeviationP.Series["Series2"].Points[0].Color = Color.Green;
            CHRT_DensityDeviationP.Series["Series2"].Points.AddXY(1, 0);
            CHRT_DensityDeviationP.Series["Series2"].Points[1].Color = Color.Green;
            #endregion

            if (DT != null && DT.Rows.Count > 0)
            {
                //========= Find Maximum And Minmum Value For Set Chart Property  ============//
                #region Find And Set Maximum And Minimum To Chart
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["TargetDensity"]) ? "0" : DT.Rows[i]["TargetDensity"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Density"]) ? "0" : DT.Rows[i]["Density"].ToString());
                    if (CValue > Convert.ToDecimal(MaxValue))
                        MaxValue = Convert.ToDouble(CValue);

                    if (CValue < Convert.ToDecimal(MinValue))
                        MinValue = Convert.ToDouble(CValue);
                }

                if (MinValue < 0)
                    MinValue += (MinValue * 2);

                MaxValue += 1;
                MinValue += 1;


                if (MaxValue > MinValue)
                    MinValue = MaxValue - ((MaxValue * 2));
                else
                    MaxValue = MinValue - ((MinValue * 2));

                CHRT_DensityDeviationP.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
                CHRT_DensityDeviationP.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
                #endregion

                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["TargetDensity"]) ? "0" : DT.Rows[i]["TargetDensity"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Density"]) ? "0" : DT.Rows[i]["Density"].ToString());
                    AddPoint(i, CHRT_DensityDeviationP.Series["Series1"], DT.Rows[i]["ShortColorName"].ToString(), CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                }
            }
            else
            {
                CHRT_DensityDeviationP.ChartAreas["Default"].AxisY.Minimum = -2;
                CHRT_DensityDeviationP.ChartAreas["Default"].AxisY.Maximum = 2;
                AddPoint(0, CHRT_DensityDeviationP.Series["Series1"], "", "0", "#E9E9E9");
            }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DensityDeviationP.ImageUrl) + "." + CHRT_DensityDeviationP.ImageType;
            CHRT_DensityDeviationP.Save(Server.MapPath(FileName));

            #endregion


            #region " === Table : 2 -- Dotgain Deviation Chart === "
            intTable = 2;
            DT = DS.Tables[intTable];


            CHART_DotgainDeviationP.Series["Series1"].Points.Clear();
            CHART_DotgainDeviationP.Series["Series2"].Points.Clear();
            CHART_DotgainDeviationP.Series["Series3"].Points.Clear();
            CHART_DotgainDeviationP.Series["Series4"].Points.Clear();



            string[] ArrDisticeValue;
            string DisticeValue = "";
            string PerValue = "";
            DataView DV = new DataView();
            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (PerValue != DT.Rows[i]["ColorPer"].ToString())
                    {
                        PerValue = DT.Rows[i]["ColorPer"].ToString();
                        if (DisticeValue.Trim() == "")
                            DisticeValue = PerValue;
                        else
                            DisticeValue += "," + PerValue;
                    }
                }
            }

            ArrDisticeValue = DisticeValue.Split(',');


            //========Line Chart Series For Dispaly Line on 0 value ================//
            #region Line Chart Series For Dispaly Line on 0 value
            CHART_DotgainDeviationP.Series["Series5"].Type = SeriesChartType.Line;
            CHART_DotgainDeviationP.Series["Series5"].XAxisType = AxisType.Secondary;
            CHART_DotgainDeviationP.Series["Series5"].Points.AddXY(0, 0);
            CHART_DotgainDeviationP.Series["Series5"].Points[0].Color = Color.Green;
            CHART_DotgainDeviationP.Series["Series5"].Points.AddXY(1, 0);
            CHART_DotgainDeviationP.Series["Series5"].Points[1].Color = Color.Green;
            #endregion

            if (ArrDisticeValue.Length > 0 && DisticeValue.Trim() != "")
            {
                ////========= Find Maximum And Minmum Value For Set Chart Property  ============//

                #region Find And Set Maximum And Minimum To Chart
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_DotGain"]) ? "0" : DT.Rows[i]["Target_DotGain"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_DotGain"]) ? "0" : DT.Rows[i]["Actual_DotGain"].ToString());
                    if (CValue > Convert.ToDecimal(MaxValue))
                        MaxValue = Convert.ToDouble(CValue);

                    if (CValue < Convert.ToDecimal(MinValue))
                        MinValue = Convert.ToDouble(CValue);
                }

                if (MinValue < 0)
                    MinValue += (MinValue * 2);

                MaxValue += 2;
                MinValue += 2;

                if (MaxValue > MinValue)
                    MinValue = MaxValue - ((MaxValue * 2));
                else
                    MaxValue = MinValue - ((MinValue * 2));

                CHART_DotgainDeviationP.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
                CHART_DotgainDeviationP.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
                #endregion

                for (int i = 0; i < ArrDisticeValue.Length; i++)
                {

                    DT.DefaultView.RowFilter = "(ShortColor = 'C' AND ColorPer = '" + ArrDisticeValue[i].ToString() + "')";
                    DV = DT.DefaultView;
                    if (DV.Count > 0)
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DV[0]["Target_DotGain"]) ? "0" : DV[0]["Target_DotGain"].ToString()) - Decimal.Parse(Convert.IsDBNull(DV[0]["Actual_DotGain"]) ? "0" : DV[0]["Actual_DotGain"].ToString());
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series1"], ArrDisticeValue[i].ToString() + "%", CValue.ToString(), DV[0]["HTMLColor"].ToString());
                    }
                    else
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series1"], ArrDisticeValue[i].ToString() + "%", "0", "#E9E9E9");

                    DT.DefaultView.RowFilter = "(ShortColor = 'M' AND ColorPer = '" + ArrDisticeValue[i].ToString() + "')";
                    DV = DT.DefaultView;
                    if (DV.Count > 0)
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DV[0]["Target_DotGain"]) ? "0" : DV[0]["Target_DotGain"].ToString()) - Decimal.Parse(Convert.IsDBNull(DV[0]["Actual_DotGain"]) ? "0" : DV[0]["Actual_DotGain"].ToString());
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series2"], ArrDisticeValue[i].ToString() + "%", CValue.ToString(), DV[0]["HTMLColor"].ToString());
                    }
                    else
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series2"], ArrDisticeValue[i].ToString() + "%", "0", "#E9E9E9");


                    DT.DefaultView.RowFilter = "(ShortColor = 'Y' AND ColorPer = '" + ArrDisticeValue[i].ToString() + "')";
                    DV = DT.DefaultView;
                    if (DV.Count > 0)
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DV[0]["Target_DotGain"]) ? "0" : DV[0]["Target_DotGain"].ToString()) - Decimal.Parse(Convert.IsDBNull(DV[0]["Actual_DotGain"]) ? "0" : DV[0]["Actual_DotGain"].ToString());
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series3"], ArrDisticeValue[i].ToString() + "%", CValue.ToString(), DV[0]["HTMLColor"].ToString());
                    }
                    else
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series3"], ArrDisticeValue[i].ToString() + "%", "0", "#E9E9E9");

                    DT.DefaultView.RowFilter = "(ShortColor = 'K' AND ColorPer = '" + ArrDisticeValue[i].ToString() + "')";
                    DV = DT.DefaultView;
                    if (DV.Count > 0)
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DV[0]["Target_DotGain"]) ? "0" : DV[0]["Target_DotGain"].ToString()) - Decimal.Parse(Convert.IsDBNull(DV[0]["Actual_DotGain"]) ? "0" : DV[0]["Actual_DotGain"].ToString());
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series4"], ArrDisticeValue[i].ToString() + "%", CValue.ToString(), DV[0]["HTMLColor"].ToString());
                    }
                    else
                        AddPoint(i, CHART_DotgainDeviationP.Series["Series4"], ArrDisticeValue[i].ToString() + "%", "0", "#E9E9E9");

                }
            }
            else
            {
                CHART_DotgainDeviationP.ChartAreas["Default"].AxisY.Minimum = -10;
                CHART_DotgainDeviationP.ChartAreas["Default"].AxisY.Maximum = 10;

                AddPoint(0, CHART_DotgainDeviationP.Series["Series1"], "", "0", "#E9E9E9");
                AddPoint(0, CHART_DotgainDeviationP.Series["Series2"], "", "0", "#E9E9E9");
                AddPoint(0, CHART_DotgainDeviationP.Series["Series3"], "", "0", "#E9E9E9");
                AddPoint(0, CHART_DotgainDeviationP.Series["Series4"], "", "0", "#E9E9E9");
            }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_DotgainDeviationP.ImageUrl) + "." + CHART_DotgainDeviationP.ImageType;
            CHART_DotgainDeviationP.Save(Server.MapPath(FileName));


            #endregion


            #region " === Table : 3 -- Print Deviation Chart === "
            intTable = 3;
            DT = DS.Tables[intTable];

            CHART_PrintDeviationP.Series["Series1"].Points.Clear();

            //========Line Chart Series For Dispaly Line on 0 value ================//
            #region Line Chart Series For Dispaly Line on 0 value
            CHART_PrintDeviationP.Series["Series2"].Type = SeriesChartType.Line;
            CHART_PrintDeviationP.Series["Series2"].XAxisType = AxisType.Secondary;
            CHART_PrintDeviationP.Series["Series2"].Points.AddXY(0, 0);
            CHART_PrintDeviationP.Series["Series2"].Points[0].Color = Color.Green;
            CHART_PrintDeviationP.Series["Series2"].Points.AddXY(1, 0);
            CHART_PrintDeviationP.Series["Series2"].Points[1].Color = Color.Green;
            #endregion

            if (DT != null && DT.Rows.Count > 0)
            {
                //========= Find Maximum And Minmum Value For Set Chart Property  ============//

                #region Find And Set Maximum And Minimum To Chart
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_PrintContrast"]) ? "0" : DT.Rows[i]["Target_PrintContrast"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_PrintContrast"]) ? "0" : DT.Rows[i]["Actual_PrintContrast"].ToString());
                    if (CValue > Convert.ToDecimal(MaxValue))
                        MaxValue = Convert.ToDouble(CValue);

                    if (CValue < Convert.ToDecimal(MinValue))
                        MinValue = Convert.ToDouble(CValue);
                }

                if (MinValue < 0)
                    MinValue += (MinValue * 2);

                MaxValue += 2;
                MinValue += 2;

                if (MaxValue > MinValue)
                    MinValue = MaxValue - ((MaxValue * 2));
                else
                    MaxValue = MinValue - ((MinValue * 2));

                CHART_PrintDeviationP.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
                CHART_PrintDeviationP.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
                #endregion


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (DT.Rows[i]["ShortColorName"].ToString().Substring(0, 1).ToUpper() == "C")
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_PrintContrast"]) ? "0" : DT.Rows[i]["Target_PrintContrast"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_PrintContrast"]) ? "0" : DT.Rows[i]["Actual_PrintContrast"].ToString());
                        AddPoint(i, CHART_PrintDeviationP.Series["Series1"], "C", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }
                    else if (DT.Rows[i]["ShortColorName"].ToString().Substring(0, 1).ToUpper() == "M")
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_PrintContrast"]) ? "0" : DT.Rows[i]["Target_PrintContrast"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_PrintContrast"]) ? "0" : DT.Rows[i]["Actual_PrintContrast"].ToString());
                        AddPoint(i, CHART_PrintDeviationP.Series["Series1"], "M", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }
                    else if (DT.Rows[i]["ShortColorName"].ToString().Substring(0, 1).ToUpper() == "Y")
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_PrintContrast"]) ? "0" : DT.Rows[i]["Target_PrintContrast"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_PrintContrast"]) ? "0" : DT.Rows[i]["Actual_PrintContrast"].ToString());
                        AddPoint(i, CHART_PrintDeviationP.Series["Series1"], "Y", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }
                    else if (DT.Rows[i]["ShortColorName"].ToString().Substring(0, 1).ToUpper() == "K")
                    {
                        CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_PrintContrast"]) ? "0" : DT.Rows[i]["Target_PrintContrast"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_PrintContrast"]) ? "0" : DT.Rows[i]["Actual_PrintContrast"].ToString());
                        AddPoint(i, CHART_PrintDeviationP.Series["Series1"], "K", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }

                }
            }
            else
            {
                CHART_PrintDeviationP.ChartAreas["Default"].AxisY.Minimum = -10;
                CHART_PrintDeviationP.ChartAreas["Default"].AxisY.Maximum = 10;

                AddPoint(0, CHART_PrintDeviationP.Series["Series1"], "C", "0", "#E9E9E9");
                AddPoint(1, CHART_PrintDeviationP.Series["Series1"], "M", "0", "#E9E9E9");
                AddPoint(2, CHART_PrintDeviationP.Series["Series1"], "Y", "0", "#E9E9E9");
                AddPoint(3, CHART_PrintDeviationP.Series["Series1"], "K", "0", "#E9E9E9");
            }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_PrintDeviationP.ImageUrl) + "." + CHART_PrintDeviationP.ImageType;
            CHART_PrintDeviationP.Save(Server.MapPath(FileName));
            #endregion


            #region " === Table : 4 -- Trap Deviation Chart === "
            intTable = 4;
            DT = DS.Tables[intTable];

            CHART_TrapDeviationP.Series["Series1"].Points.Clear();

            //========Line Chart Series For Dispaly Line on 0 value ================//
            #region Line Chart Series For Dispaly Line on 0 value
            CHART_TrapDeviationP.Series["Series2"].Type = SeriesChartType.Line;
            CHART_TrapDeviationP.Series["Series2"].XAxisType = AxisType.Secondary;
            CHART_TrapDeviationP.Series["Series2"].Points.AddXY(0, 0);
            CHART_TrapDeviationP.Series["Series2"].Points[0].Color = Color.Green;
            CHART_TrapDeviationP.Series["Series2"].Points.AddXY(1, 0);
            CHART_TrapDeviationP.Series["Series2"].Points[1].Color = Color.Green;
            #endregion

            if (DT != null && DT.Rows.Count > 0)
            {
                //========= Find Maximum And Minmum Value For Set Chart Property  ============//

                #region Find And Set Maximum And Minimum To Chart
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_ApparentTrap"]) ? "0" : DT.Rows[i]["Target_ApparentTrap"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_ApparentTrap"]) ? "0" : DT.Rows[i]["Actual_ApparentTrap"].ToString());
                    if (CValue > Convert.ToDecimal(MaxValue))
                        MaxValue = Convert.ToDouble(CValue);

                    if (CValue < Convert.ToDecimal(MinValue))
                        MinValue = Convert.ToDouble(CValue);
                }

                if (MinValue < 0)
                    MinValue += (MinValue * 2);

                MaxValue += 2;
                MinValue += 2;

                if (MaxValue > MinValue)
                    MinValue = MaxValue - ((MaxValue * 2));
                else
                    MaxValue = MinValue - ((MinValue * 2));

                CHART_TrapDeviationP.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
                CHART_TrapDeviationP.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
                #endregion


                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    string Name = "";
                    if (DT.Rows[i]["ShortColorName"].ToString() == "M+Y")
                        Name = "R";
                    else if (DT.Rows[i]["ShortColorName"].ToString() == "C+Y")
                        Name = "G";
                    else
                        Name = "B";
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Target_ApparentTrap"]) ? "0" : DT.Rows[i]["Target_ApparentTrap"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Actual_ApparentTrap"]) ? "0" : DT.Rows[i]["Actual_ApparentTrap"].ToString());
                    AddPoint(i, CHART_TrapDeviationP.Series["Series1"], Name, CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                }
            }
            else
            {
                CHART_TrapDeviationP.ChartAreas["Default"].AxisY.Minimum = -10;
                CHART_TrapDeviationP.ChartAreas["Default"].AxisY.Maximum = 10;

                AddPoint(0, CHART_TrapDeviationP.Series["Series1"], "R", "0", "Red");
                AddPoint(1, CHART_TrapDeviationP.Series["Series1"], "G", "0", "Green");
                AddPoint(2, CHART_TrapDeviationP.Series["Series1"], "B", "0", "Blue");
            }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_TrapDeviationP.ImageUrl) + "." + CHART_TrapDeviationP.ImageType;
            CHART_TrapDeviationP.Save(Server.MapPath(FileName));

            #endregion

            #region "Fill Null Value To Non Process Chart"
            AddPoint(0, CHART_DensityDeviationNP.Series["Series1"], "", "0", "#E9E9E9");
            #endregion
        }
        else
        {

            #region " === Table : 1 -- Density Deviation Chart === "
            intTable = 1;
            DT = DS.Tables[intTable];


            CHART_DensityDeviationNP.Series["Series1"].Points.Clear();

            //========Line Chart Series For Dispaly Line on 0 value ================//
            #region Line Chart Series For Dispaly Line on 0 value
            CHART_DensityDeviationNP.Series["Series2"].Type = SeriesChartType.Line;
            CHART_DensityDeviationNP.Series["Series2"].XAxisType = AxisType.Secondary;
            CHART_DensityDeviationNP.Series["Series2"].Points.AddXY(0, 0);
            CHART_DensityDeviationNP.Series["Series2"].Points[0].Color = Color.Green;
            CHART_DensityDeviationNP.Series["Series2"].Points.AddXY(1, 0);
            CHART_DensityDeviationNP.Series["Series2"].Points[1].Color = Color.Green;
            #endregion


            if (DT != null && DT.Rows.Count > 0)
            {

                //========= Find Maximum And Minmum Value For Set Chart Property  ============//

                #region Find And Set Maximum And Minimum To Chart
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["TargetDensity"]) ? "0" : DT.Rows[i]["TargetDensity"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Density"]) ? "0" : DT.Rows[i]["Density"].ToString());
                    if (CValue > Convert.ToDecimal(MaxValue))
                        MaxValue = Convert.ToDouble(CValue);

                    if (CValue < Convert.ToDecimal(MinValue))
                        MinValue = Convert.ToDouble(CValue);
                }

                if (MinValue < 0)
                    MinValue += (MinValue * 2);

                MaxValue += 2;
                MinValue += 2;

                if (MaxValue > MinValue)
                    MinValue = MaxValue - ((MaxValue * 2));
                else
                    MaxValue = MinValue - ((MinValue * 2));

                CHART_DensityDeviationNP.ChartAreas["Default"].AxisY.Minimum = Math.Round(MinValue);
                CHART_DensityDeviationNP.ChartAreas["Default"].AxisY.Maximum = Math.Round(MaxValue);
                #endregion


                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    CValue = Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["TargetDensity"]) ? "0" : DT.Rows[i]["TargetDensity"].ToString()) - Decimal.Parse(Convert.IsDBNull(DT.Rows[i]["Density"]) ? "0" : DT.Rows[i]["Density"].ToString());
                    AddPoint(i, CHART_DensityDeviationNP.Series["Series1"], DT.Rows[i]["ShortColorName"].ToString(), CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                }

            }
            else
            {
                CHART_DensityDeviationNP.ChartAreas["Default"].AxisY.Minimum = -10;
                CHART_DensityDeviationNP.ChartAreas["Default"].AxisY.Maximum = 10;

                AddPoint(0, CHART_DensityDeviationNP.Series["Series1"], "", "0", "#E9E9E9");
            }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_DensityDeviationNP.ImageUrl) + "." + CHART_DensityDeviationNP.ImageType;
            CHART_DensityDeviationNP.Save(Server.MapPath(FileName));

            #endregion

            #region "Fill Null Value To Process Chart"
            AddPoint(0, CHRT_DensityDeviationP.Series["Series1"], "", "0", "#E9E9E9");

            AddPoint(0, CHART_DotgainDeviationP.Series["Series1"], "", "0", "#E9E9E9");
            AddPoint(0, CHART_DotgainDeviationP.Series["Series2"], "", "0", "#E9E9E9");
            AddPoint(0, CHART_DotgainDeviationP.Series["Series3"], "", "0", "#E9E9E9");
            AddPoint(0, CHART_DotgainDeviationP.Series["Series4"], "", "0", "#E9E9E9");


            AddPoint(0, CHART_PrintDeviationP.Series["Series1"], "C", "0", "#E9E9E9");
            AddPoint(1, CHART_PrintDeviationP.Series["Series1"], "M", "0", "#E9E9E9");
            AddPoint(2, CHART_PrintDeviationP.Series["Series1"], "Y", "0", "#E9E9E9");
            AddPoint(3, CHART_PrintDeviationP.Series["Series1"], "K", "0", "#E9E9E9");


            AddPoint(0, CHART_TrapDeviationP.Series["Series1"], "R", "0", "Red");
            AddPoint(1, CHART_TrapDeviationP.Series["Series1"], "G", "0", "Green");
            AddPoint(2, CHART_TrapDeviationP.Series["Series1"], "B", "0", "Blue");

            #endregion

        }


    }
    private void AddPointToTrendDeltaE(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(_XValue, _YValue);


            _objSeries.Points[_Index].MarkerSize = 4;
            _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;

            /* Added By Chirag Shah cshah@fujifilmgs.com on 24th Feb-09 to display diamond if deltaE values are zero (i.e. for an OK Sheet) */
            if ((Convert.ToDouble(_YValue) == 0.0))
            {
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Diamond;
                _objSeries.Points[_Index].MarkerSize = 15;
                _objSeries.Points[_Index].MarkerColor = Color.Green;
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
            //if (Convert.ToDecimal(_YValue) > 20) { _objSeries.Points.AddXY(_XValue, "20"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
            _objSeries.Points.AddXY(_XValue, _YValue);
            _objSeries.Points[_Index].ToolTip = _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
            _objSeries.Points[_Index].Font = new Font(FontFamily.GenericSerif, 7, FontStyle.Bold);


            //'**********Changed By Chirag on 1/23/2008
            string ColorName = ZoneColor;
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

            _objSeries.Points[_Index].Color = Color.FromName(ColorName);
            _objSeries.Points[_Index].Href = "#";

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region " === Target Versus Actual AB Plot === "
    private void Bind_TargetVersusActual_CHART1(DataTable dtGamut)
    {
        try
        {
            #region Plotting the Chart

            DataView DV = dtGamut.DefaultView;


            #region Other Points
            DV = dtGamut.DefaultView;
            for (int i = 0; i < DV.Count; i = i + 1)
            {
                CHART_TargetVersusActualAB.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["TARGET_A"]), Convert.ToDouble(DV[i]["TARGET_B"]));
                CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Line;
                CHART_TargetVersusActualAB.Series[0].Points[i].BorderWidth = 2;

                CHART_TargetVersusActualAB.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["ACTUAL_A"]), Convert.ToDouble(DV[i]["ACTUAL_B"]));
                CHART_TargetVersusActualAB.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                CHART_TargetVersusActualAB.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Line;
                CHART_TargetVersusActualAB.Series[1].Points[i].BorderWidth = 2;
            }

            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_TargetVersusActualAB.ImageUrl) + "." + CHART_TargetVersusActualAB.ImageType;
            CHART_TargetVersusActualAB.Save(Server.MapPath(FileName));
            CHART_TargetVersusActualAB.ImageType = ChartImageType.Flash;

            #endregion
            #endregion
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }

    }

    private void Bind_TargetVersusActual_CHART(DataTable dtGamut)
    {
        try
        {
            DataView DV = new DataView();

            if (dtGamut != null && dtGamut.Rows.Count > 0)
            {
                if (!paraIsNonProcess) //for process color job
                {
                    if (dtGamut.Rows.Count > 7) //2009_06_15 by Softweb - Ajay
                    {
                        #region Plotting the Chart
                        dtGamut.DefaultView.RowFilter = "ShortColorName = 'PAPER'";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            int i = 0;
                            CHART_TargetVersusActualAB.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["TARGET_A"]), Convert.ToDouble(DV[i]["TARGET_B"]));
                            CHART_TargetVersusActualAB.Series[2].Type = SeriesChartType.FastPoint;


                            CHART_TargetVersusActualAB.Series[3].Points.AddXY(Convert.ToDouble(DV[i]["ACTUAL_A"]), Convert.ToDouble(DV[i]["ACTUAL_B"]));
                            CHART_TargetVersusActualAB.Series[3].Type = SeriesChartType.FastPoint;
                            CHART_TargetVersusActualAB.Series[3].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[3].Points[0].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                        }
                        #endregion

                        #region Other Points
                        dtGamut.DefaultView.RowFilter = "(ShortColorName <> 'PAPER')";
                        DV = dtGamut.DefaultView;

                        //2009_06_15 by Softweb - Ajay
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {
                            CHART_TargetVersusActualAB.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["TARGET_A"]), Convert.ToDouble(DV[i]["TARGET_B"]));
                            CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Point;
                            CHART_TargetVersusActualAB.Series[0].Points[i].BorderWidth = 2;

                            CHART_TargetVersusActualAB.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["ACTUAL_A"]), Convert.ToDouble(DV[i]["ACTUAL_B"]));
                            CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Point;
                            CHART_TargetVersusActualAB.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                            CHART_TargetVersusActualAB.Series[1].Points[i].BorderWidth = 2;

                        }
                        #endregion

                    }
                    else
                    {

                        #region Plotting the Chart
                        dtGamut.DefaultView.RowFilter = "ShortColorName = 'PAPER'";
                        DV = dtGamut.DefaultView;
                        if (DV.Count > 0)
                        {
                            int i = 0;
                            CHART_TargetVersusActualAB.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["TARGET_A"]), Convert.ToDouble(DV[i]["TARGET_B"]));
                            CHART_TargetVersusActualAB.Series[2].Type = SeriesChartType.FastPoint;


                            CHART_TargetVersusActualAB.Series[3].Points.AddXY(Convert.ToDouble(DV[i]["ACTUAL_A"]), Convert.ToDouble(DV[i]["ACTUAL_B"]));
                            CHART_TargetVersusActualAB.Series[3].Type = SeriesChartType.FastPoint;
                            CHART_TargetVersusActualAB.Series[3].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[3].Points[0].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                        }
                        #endregion

                        #region Other Points
                        dtGamut.DefaultView.RowFilter = "(ShortColorName <> 'PAPER')";
                        DV = dtGamut.DefaultView;
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {
                            CHART_TargetVersusActualAB.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["TARGET_A"]), Convert.ToDouble(DV[i]["TARGET_B"]));
                            CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[0].Points[i].BorderWidth = 2;

                            CHART_TargetVersusActualAB.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["ACTUAL_A"]), Convert.ToDouble(DV[i]["ACTUAL_B"]));
                            CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                            CHART_TargetVersusActualAB.Series[1].Points[i].BorderWidth = 2;
                        }
                        if (DV.Count > 1)
                        {
                            int i = 0;
                            CHART_TargetVersusActualAB.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["TARGET_A"]), Convert.ToDouble(DV[i]["TARGET_B"]));
                            CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[0].Points[DV.Count].BorderWidth = 2;

                            CHART_TargetVersusActualAB.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["ACTUAL_A"]), Convert.ToDouble(DV[i]["ACTUAL_B"]));
                            CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[1].Points[DV.Count].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                            CHART_TargetVersusActualAB.Series[1].Points[DV.Count].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                            CHART_TargetVersusActualAB.Series[1].Points[DV.Count].BorderWidth = 2;
                        }
                        #endregion
                    }
                }
                else  //non-process  
                {
                    DV = dtGamut.DefaultView;
                    for (int i = 0; i < DV.Count; i = i + 1)
                    {
                        CHART_TargetVersusActualAB.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["TARGET_A"]), Convert.ToDouble(DV[i]["TARGET_B"]));
                        CHART_TargetVersusActualAB.Series[0].Type = SeriesChartType.Point;
                        CHART_TargetVersusActualAB.Series[0].Points[i].BorderWidth = 2;

                        CHART_TargetVersusActualAB.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["ACTUAL_A"]), Convert.ToDouble(DV[i]["ACTUAL_B"]));
                        CHART_TargetVersusActualAB.Series[1].Type = SeriesChartType.Point;
                        CHART_TargetVersusActualAB.Series[1].Points[i].Color = Color.Red; //System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                        CHART_TargetVersusActualAB.Series[1].Points[i].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(DV[i]["HTMLColor"]));
                        CHART_TargetVersusActualAB.Series[1].Points[i].BorderWidth = 2;
                    }

                }
            }
            else
            {
                CHART_TargetVersusActualAB.BackImage = "~/Images/JobRunCertification_PointBack.gif";
            }


            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_TargetVersusActualAB.ImageUrl) + "." + CHART_TargetVersusActualAB.ImageType;
            CHART_TargetVersusActualAB.Save(Server.MapPath(FileName));



        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }

    }
    #endregion

    #region Fill TrendLine Chart
    private void FillTrendsCharts()
    {
        try
        {
            int intTable;

            #region " === Trend Delta E === "
            //========== Declaring Objects ==========//
            PressJobsData objPressJobs = new PressJobsData();

            //========== Get The Data Table ==========//
            objPressJobs.JobID = paraJobID;
            objPressJobs.DongleID = paraDongleID;
            DataTable DT = objPressJobs.uspGetPressJobSheetSummary();


            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                AddPointChart(i, CHRT_TrendDeltaEP.Series["Series1"], DT.Rows[i]["SheetNumber"].ToString(), DT.Rows[i]["AvgDeltaE"].ToString());
                AddPointChart(i, CHRT_TrendDeltaEP.Series["Series2"], DT.Rows[i]["SheetNumber"].ToString(), DT.Rows[i]["MaxDeltaE"].ToString());
                AddPointChart(i, CHRT_TrendDeltaEP.Series["Series3"], DT.Rows[i]["SheetNumber"].ToString(), DT.Rows[i]["StdDevdeltaE"].ToString());

            }

            //========== Ads a blank row ==========//
            if (DT.Rows.Count == 0) { AddBlankRow(CHRT_TrendDeltaEP); }

            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_TrendDeltaEP.ImageUrl) + "." + CHRT_TrendDeltaEP.ImageType;
            CHRT_TrendDeltaEP.Save(Server.MapPath(FileName));
            #endregion


            //========== Declaring Objects ==========//
            PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();

            //========== Get The Data Table ==========//
            objPressPatchCalculations.JobID = paraJobID;
            objPressPatchCalculations.DongleID = paraDongleID;
            DataSet DSet = objPressPatchCalculations.uspGetJobTrendView();


            #region " === Table : 0 -- DeltaEP & Density Chart === "
            intTable = 0;
            DT = DSet.Tables[intTable];

            //========== GET All For Color = Cyan ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 1"; //Cyan
            DataTable DT_Cyan = DT.DefaultView.ToTable("Cyan");

            //========== GET All For Color = Magenta ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 2"; //Magenta
            DataTable DT_Magenta = DT.DefaultView.ToTable("Magenta");

            //========== GET All For Color = Yellow ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 3"; //Yellow
            DataTable DT_Yellow = DT.DefaultView.ToTable("Yellow");

            //========== GET All For Color = Black ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 0";//Black
            DataTable DT_Black = DT.DefaultView.ToTable("Black");

            #region Cyan
            for (int i = 0; i < DT_Cyan.Rows.Count; i = i + 1)
            {
                AddPointChart(i, CHRT_DeltaEP.Series["Series1"], DT_Cyan.Rows[i]["SheetNumber"].ToString(), DT_Cyan.Rows[i]["AvgDeltaE"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString());
                AddPointChart(i, CHRT_DensityP.Series["Series1"], DT_Cyan.Rows[i]["SheetNumber"].ToString(), DT_Cyan.Rows[i]["AvgDensity"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString());
            }
            #endregion

            #region Magenta
            for (int i = 0; i < DT_Magenta.Rows.Count; i = i + 1)
            {
                AddPointChart(i, CHRT_DeltaEP.Series["Series2"], DT_Magenta.Rows[i]["ScannedDateTime"].ToString(), DT_Magenta.Rows[i]["AvgDeltaE"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString());
                AddPointChart(i, CHRT_DensityP.Series["Series2"], DT_Magenta.Rows[i]["ScannedDateTime"].ToString(), DT_Magenta.Rows[i]["AvgDensity"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString());

            }
            #endregion

            #region Yellow
            for (int i = 0; i < DT_Yellow.Rows.Count; i = i + 1)
            {
                AddPointChart(i, CHRT_DeltaEP.Series["Series3"], DT_Yellow.Rows[i]["ScannedDateTime"].ToString(), DT_Yellow.Rows[i]["AvgDeltaE"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString());
                AddPointChart(i, CHRT_DensityP.Series["Series3"], DT_Yellow.Rows[i]["ScannedDateTime"].ToString(), DT_Yellow.Rows[i]["AvgDensity"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString());
            }
            #endregion

            #region Black
            for (int i = 0; i < DT_Black.Rows.Count; i = i + 1)
            {
                AddPointChart(i, CHRT_DeltaEP.Series["Series4"], DT_Black.Rows[i]["ScannedDateTime"].ToString(), DT_Black.Rows[i]["AvgDeltaE"].ToString(), DT_Black.Rows[i]["ColorName"].ToString());
                AddPointChart(i, CHRT_DensityP.Series["Series4"], DT_Black.Rows[i]["ScannedDateTime"].ToString(), DT_Black.Rows[i]["AvgDensity"].ToString(), DT_Black.Rows[i]["ColorName"].ToString());

            }
            #endregion

            //========== Ads a blank row ==========//
            if (DT.Rows.Count == 0) { AddBlankRow(CHRT_DeltaEP); AddBlankRow(CHRT_DensityP); }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DeltaEP.ImageUrl) + "." + CHRT_DeltaEP.ImageType;
            CHRT_DeltaEP.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DensityP.ImageUrl) + "." + CHRT_DensityP.ImageType;
            CHRT_DensityP.Save(Server.MapPath(FileName));


            #endregion

            #region " === Table : 1 -- Dotgain Chart === "
            intTable = 1;
            DT = DSet.Tables[intTable];

            //========== GET All For Color = Cyan ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 1"; //Cyan
            DT_Cyan = DT.DefaultView.ToTable("Cyan");

            //========== GET All For Color = Magenta ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType= 2"; //Magenta
            DT_Magenta = DT.DefaultView.ToTable("Magenta");

            //========== GET All For Color = Yellow ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 3"; //Yellow
            DT_Yellow = DT.DefaultView.ToTable("Yellow");

            //========== GET All For Color = Black ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 0";//Black
            DT_Black = DT.DefaultView.ToTable("Black");

            int j = 0;
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                #region Cyan
                if (i < DT_Cyan.Rows.Count)
                    AddPointChart(j, CHRT_DotgainP.Series["Series1"], DT_Cyan.Rows[i]["SheetNumber"].ToString(), DT_Cyan.Rows[i]["AvgDotgain"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString());

                #endregion

                #region Magenta
                if (i < DT_Magenta.Rows.Count)
                    AddPointChart(j, CHRT_DotgainP.Series["Series2"], DT_Magenta.Rows[i]["SheetNumber"].ToString(), DT_Magenta.Rows[i]["AvgDotgain"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString());


                #endregion

                #region Yellow
                if (i < DT_Yellow.Rows.Count)
                    AddPointChart(j, CHRT_DotgainP.Series["Series3"], DT_Yellow.Rows[i]["SheetNumber"].ToString(), DT_Yellow.Rows[i]["AvgDotgain"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString());

                #endregion

                #region Black
                if (i < DT_Black.Rows.Count)
                    AddPointChart(j, CHRT_DotgainP.Series["Series4"], DT_Black.Rows[i]["SheetNumber"].ToString(), DT_Black.Rows[i]["AvgDotgain"].ToString(), DT_Black.Rows[i]["ColorName"].ToString());
                #endregion

                j = j + 1;
            }

            if (DT.Rows.Count == 0) { AddBlankRow(CHRT_DotgainP); }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DotgainP.ImageUrl) + "." + CHRT_DotgainP.ImageType;
            CHRT_DotgainP.Save(Server.MapPath(FileName));


            #endregion

            #region " === Table : 2 -- Print Contrast Chart === "
            intTable = 2;
            DT = DSet.Tables[intTable];

            //========== GET All For Color = Cyan ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType= 1"; //Cyan
            DT_Cyan = DT.DefaultView.ToTable("Cyan");

            //========== GET All For Color = Magenta ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 2"; //Magenta
            DT_Magenta = DT.DefaultView.ToTable("Magenta");

            //========== GET All For Color = Yellow ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 3"; //Yellow
            DT_Yellow = DT.DefaultView.ToTable("Yellow");

            //========== GET All For Color = Black ==========//
            DT.DefaultView.RowFilter = "PrimaryColorType = 0";//Black
            DT_Black = DT.DefaultView.ToTable("Black");

            j = 0;
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                #region Cyan
                if (i < DT_Cyan.Rows.Count)
                    AddPointChart(j, CHRT_PrintContrastP.Series["Series1"], DT_Cyan.Rows[i]["SheetNumber"].ToString(), DT_Cyan.Rows[i]["AvgPrintContrast"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString());
                #endregion

                #region Magenta
                if (i < DT_Magenta.Rows.Count)
                    AddPointChart(j, CHRT_PrintContrastP.Series["Series2"], DT_Magenta.Rows[i]["SheetNumber"].ToString(), DT_Magenta.Rows[i]["AvgPrintContrast"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString());
                #endregion

                #region Yellow
                if (i < DT_Yellow.Rows.Count)
                    AddPointChart(j, CHRT_PrintContrastP.Series["Series3"], DT_Yellow.Rows[i]["SheetNumber"].ToString(), DT_Yellow.Rows[i]["AvgPrintContrast"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString());
                #endregion

                #region Black
                if (i < DT_Black.Rows.Count)
                    AddPointChart(j, CHRT_PrintContrastP.Series["Series4"], DT_Black.Rows[i]["SheetNumber"].ToString(), DT_Black.Rows[i]["AvgPrintContrast"].ToString(), DT_Black.Rows[i]["ColorName"].ToString());
                #endregion

                j = j + 1;
            }

            if (DT.Rows.Count == 0) { AddBlankRow(CHRT_PrintContrastP); }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_PrintContrastP.ImageUrl) + "." + CHRT_PrintContrastP.ImageType;
            CHRT_PrintContrastP.Save(Server.MapPath(FileName));

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
    //Add Default Point Chart
    private void AddPointChart(int _Index, Series _objSeries, string _XValue, string _YValue)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            //if (Convert.ToDecimal(_YValue) > 30) { _objSeries.Points.AddXY(_XValue, "30"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
            _objSeries.Points.AddXY(_XValue, _YValue);

            _objSeries.Points[_Index].MarkerSize = 4;
            _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //Add Point In Chart for Specific Color
    private void AddPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, string _ColorName)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            //if (Convert.ToDecimal(_YValue) > 30) { _objSeries.Points.AddXY(_XValue, "30"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
            _objSeries.Points.AddXY(_XValue, _YValue);

            _objSeries.Points[_Index].MarkerSize = 4;
            _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    //Add Point In Chart for checking Paper Point
    private void AddPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, bool IsPaper)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            //if (Convert.ToDecimal(_YValue) > 30) { _objSeries.Points.AddXY(_XValue, "30"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
            _objSeries.Points.AddXY(_XValue, _YValue);
            _objSeries.Points[_Index].MarkerSize = 4;
            if (IsPaper)
            {
                _objSeries.Points[_Index].MarkerColor = Color.White;
                _objSeries.Points[_Index].BorderColor = Color.Black;
            }
            else
                _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
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
    #endregion

    #region Gray Balance L Chart
    private void FillChartGrayBalanceL(DataSet ds)
    {
        try
        {
            if (ds != null)
            {
                CHRT_GrayBalance_L.ChartAreas["Default"].Position.Auto = true;
                CHRT_GrayBalance_L.ChartAreas["Default"].Position.X = float.Parse("3");
                CHRT_GrayBalance_L.ChartAreas["Default"].Position.Y = float.Parse("15");
                CHRT_GrayBalance_L.ChartAreas["Default"].Position.Width = float.Parse("91");
                CHRT_GrayBalance_L.ChartAreas["Default"].Position.Height = float.Parse("80");

                CHRT_GrayBalance_L.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                CHRT_GrayBalance_L.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                CHRT_GrayBalance_L.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                CHRT_GrayBalance_L.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                CHRT_GrayBalance_L.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("80");

                CHRT_GrayBalance_L.ChartAreas["Default"].ShadowOffset = 0;
                CHRT_GrayBalance_L.BorderSkin.SkinStyle = BorderSkinStyle.None;
                CHRT_GrayBalance_L.ChartAreas["Default"].BorderWidth = 1;
                CHRT_GrayBalance_L.BorderSkin.FrameBorderWidth = 0;


                Fill_GrayBalance_L_Chart(ds.Tables[0], ds.Tables[1], ds.Tables[2], ds.Tables[3]);

                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_GrayBalance_L.ImageUrl) + "." + CHRT_GrayBalance_L.ImageType;
                CHRT_GrayBalance_L.Save(Server.MapPath(FileName));

            }
        }
        catch (Exception Exc)
        {

            throw Exc;
        }

    }
    private void Fill_GrayBalance_L_Chart(DataTable dtPAPER, DataTable dt25, DataTable dt50, DataTable dt75)
    {
        try
        {
            int idx;
            idx = 0;
            if (dtPAPER != null && dtPAPER.Rows.Count > 0)
            {
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series1"], "0%", dtPAPER.Rows[0]["Actual_L"].ToString(), true);
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series2"], "0%", dtPAPER.Rows[0]["Target_L"].ToString(), true);
                idx = idx + 1;
            }


            if (dt25 != null && dt25.Rows.Count > 0)
            {
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series1"], "25%", dt25.Rows[0]["Actual_L"].ToString(), false);
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series2"], "25%", dt25.Rows[0]["Target_L"].ToString(), false);
                idx = idx + 1;
            }


            if (dt50 != null && dt50.Rows.Count > 0)
            {
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series1"], "50%", dt50.Rows[0]["Actual_L"].ToString(), false);
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series2"], "50%", dt50.Rows[0]["Target_L"].ToString(), false);
                idx = idx + 1;
            }


            if (dt75 != null && dt75.Rows.Count > 0)
            {
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series1"], "75%", dt75.Rows[0]["Actual_L"].ToString(), false);
                AddPointChart(idx, CHRT_GrayBalance_L.Series["Series2"], "75%", dt75.Rows[0]["Target_L"].ToString(), false);
                idx = idx + 1;
            }

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion



}
