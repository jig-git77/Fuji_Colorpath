#region Page Load and Events
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using Telerik.WebControls;
using System.Web.UI;

#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Telerik.WebControls;
#endregion

public partial class JobsReport : PageBase
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

    #endregion

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Bind Grid ==========//     
            if (IsPostBack == false)
            {
                #region Show Hide
                if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.PrintBuyer)
                { TR_Company.Visible = false; }
                #endregion

                //====  Create ViewState For Session Key  ====//
                //ViewState["SessKey"] = "";

                #region ====  Bind Company List  ====

                DataTable dtCompany = new DataTable();

                //====  Clear List  ====//
                DDL_CompanyList.Items.Clear();

                //===  For Show All as a text in DropDwon  ====
                ListItem DDLiShowAll = new ListItem();
                DDLiShowAll.Text = " - - -All - - -";
                DDLiShowAll.Value = "0";

                //==== Add All Text in DropDwon List ====//
                DDL_CompanyList.Items.Add(DDLiShowAll);

                //====  Get Data For All Company List  ====//
                dtCompany = (new Fuji.Business.CompanyData().uspGetAllCompanyByUserID(Sessions.UserID, Sessions.RoleID));

                //==== Create Object of List Itme  ====//
                ListItem DDLItem;

                if (dtCompany != null)
                {
                    for (int i = 0; i < dtCompany.Rows.Count; i++)
                    {
                        //==== Create List Item  ====//
                        DDLItem = new ListItem();

                        //====  Set Company Name as a Text  ====//
                        DDLItem.Text = dtCompany.Rows[i]["CompanyName"].ToString();

                        //====  Set Company Id as a Value  ====//
                        DDLItem.Value = dtCompany.Rows[i]["CompanyID"].ToString();

                        //====  Add List Item In DropDown  ====//
                        DDL_CompanyList.Items.Add(DDLItem);

                    }
                }

                #endregion

                #region Set Panel

                if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin || Sessions.RoleDesc == Enums.RoleType.SupportUser)//====  For Taskero Admin & Support User  ====//
                {
                    //====  Admin Panel Show ====//
                    DDL_CompanyList.Visible = true;

                    //====  User Panel Hide  ====//
                    LTRL_UserCompany.Visible = false;

                }
                else //====  For Taskero User  ====//
                {
                    //====  Admin Panel Hide ====//
                    DDL_CompanyList.Visible = false;

                    //====  User Panel Show  ====//
                    LTRL_UserCompany.Visible = true;

                }

                #endregion

                //========== Check Login ==========//    
                Sessions.CheckUserLogin();

                //====  Get Data & Fill Session  ====//
                //GetData();
                //========= Clear the date for first time =========//
                Fuji.Common.Sessions.SetSessions("StartDate", null);
                Fuji.Common.Sessions.SetSessions("StartDate2", null);
                //========== Check Login ==========//
                FillGrid();

                //Page.RegisterClientScriptBlock("onload", "<script>alert('ss')<script>");

            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #region Event Press Grid
    protected void GridJobReportPress_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========  Clear State ========//
            ViewState["SessKey"] = DateTime.Now.ToString("yyyyMMddHHmmss");

            //========  Check Session  ========//
            if (Fuji.Common.Sessions.GetSession("dtJobReportPress") != null)
            {
                //========  Session Store in DataTable  ========//
                DataTable dtJobReport = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReportPress");

                //========  Grid Bind  ========//

                GridJobReportPress.DataSource = dtJobReport;

                //GridJobReportPress.Columns[3].CurrentFilterFunction = GridKnownFunction.StartsWith; //GridJobReportPrepress.Columns[3].CurrentFilterFunction;
                //GridJobReportPress.Columns[3].CurrentFilterValue = "manish";// GridJobReportPrepress.Columns[3].CurrentFilterValue;
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
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

                string _MeasurementStr = "";
                //========  Bind String For QueryString's Parameters ========//

                //if ((Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                //   || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                //   || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat)))
                //{
                //   _MeasurementStr = "ColorTrend.aspx?CPMeasurementGUID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text + "&CPDevicePrintTargetID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDevicePrintTargetID)].Text + "&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text + "";                    
                //}
                //else if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate))
                //{
                //    _MeasurementStr = "PlateTrend.aspx?CPMeasurementGUID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text + "&CPDevicePrintTargetID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDevicePrintTargetID)].Text + "&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text + "";                    
                //}
                //else if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                //{
                //    _MeasurementStr = "PressDashboard.aspx?JobID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPMeasurementID)].Text + "&DongleID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPSerialNUM)].Text + "&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text;
                //}
                Int32 intDeviceType = Convert.ToInt32(((Label)e.Item.FindControl("lblCPDeviceTypeID")).Text);
                string strGUID = ((Label)e.Item.FindControl("lblGUID")).Text;
                string strCPDevicePrintTargetID = ((Label)e.Item.FindControl("lblCPDevicePrintTargetID")).Text;
                string strCPDeviceID = ((Label)e.Item.FindControl("lblCPDeviceID")).Text;
                string strCPSerialNUM = ((Label)e.Item.FindControl("lblCPSerialNUM")).Text;
                string strCPMeasurementID = ((Label)e.Item.FindControl("lblCPMeasurementID")).Text;

                //===========: Updated By : Manish(06-02-2009) - If JobTypeID = 3 then its non Processed Type CMYK :==================//
                string strJobTypeID = ((Label)e.Item.FindControl("lblJobTypeID")).Text;
                strJobTypeID = strJobTypeID.Trim() == "" ? "0" : strJobTypeID.Trim();
                if (int.Parse(strJobTypeID) == (int)enumJobTypeID.NonCMYK)
                {
                    if ((intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                         || (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                         || (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat)))
                    {
                        _MeasurementStr = "ColorTrend.aspx?CPMeasurementGUID=" + strGUID + "&CPDevicePrintTargetID=" + strCPDevicePrintTargetID + "&CPDeviceID=" + strCPDeviceID + "";
                    }
                    else if (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate))
                    {
                        _MeasurementStr = "PlateTrend.aspx?CPMeasurementGUID=" + strGUID + "&CPDevicePrintTargetID=" + strCPDevicePrintTargetID + "&CPDeviceID=" + strCPDeviceID + "";
                    }
                    else if (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                    {
                        _MeasurementStr = "PressDashboardNonProcess.aspx?JobID=" + strCPMeasurementID + "&DongleID=" + strCPSerialNUM + "&CPDeviceID=" + strCPDeviceID;
                    }
                }
                else
                {
                    if ((intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                         || (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                         || (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat)))
                    {
                        _MeasurementStr = "ColorTrend.aspx?CPMeasurementGUID=" + strGUID + "&CPDevicePrintTargetID=" + strCPDevicePrintTargetID + "&CPDeviceID=" + strCPDeviceID + "";
                    }
                    else if (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate))
                    {
                        _MeasurementStr = "PlateTrend.aspx?CPMeasurementGUID=" + strGUID + "&CPDevicePrintTargetID=" + strCPDevicePrintTargetID + "&CPDeviceID=" + strCPDeviceID + "";
                    }
                    else if (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                    {
                        _MeasurementStr = "PressDashboard.aspx?JobID=" + strCPMeasurementID + "&DongleID=" + strCPSerialNUM + "&CPDeviceID=" + strCPDeviceID;
                    }
                }
                //========  Initiate Object For View Image  ========//
                System.Web.UI.HtmlControls.HtmlImage ImgView;
                ImgView = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView");

                System.Web.UI.HtmlControls.HtmlImage ImgView_New;
                ImgView_New = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView_New");

                //Hide Views Button
                if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim().Replace(" ", "") == Fuji.Common.Enums.enumJobStatus.MakeReady.ToString())
                {
                    ImgView.Visible = false;
                    ImgView_New.Visible = false;

                }

                string _NewId = e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text.Replace("-", "");


                //========  if CPDeviceType = "Color" then Open popup for Detail  ========//
                ImgView.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "&ModalDialog=Y', true, false , '" + _NewId + "');");
                ImgView_New.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', true, true,'" + _NewId + "');");


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
    protected void GridJobReportPress_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "FirstSheet")
            {
                TableCell FirstSheetCell = ((Telerik.WebControls.GridFilteringItem)e.Item)["FirstSheet"];
                Panel FirstSheetPnl_Date_Picker = (Panel)FirstSheetCell.Controls[0].FindControl("PNL_DATE_PICKER");
                RadDatePicker FirstSheetRDP_Date = (RadDatePicker)FirstSheetPnl_Date_Picker.FindControl("RadDatePicker1");


                if (FirstSheetRDP_Date != null) //changed by Chirag on 08/21/09
                {
                    Fuji.Common.Sessions.SetSessions("StartDate", FirstSheetRDP_Date.SelectedDate);

                    //e.Item.OwnerTableView.FilterExpression = String.Format("DateTime > '{0}'", RDP_Date.SelectedDate.ToString());
                    e.Item.OwnerTableView.FilterExpression = String.Format("FirstSheet > '{0}' AND FirstSheet < '{1}'", FirstSheetRDP_Date.SelectedDate.ToString(), Convert.ToDateTime(FirstSheetRDP_Date.SelectedDate.ToString()).AddDays(1).ToString());
                    e.Item.OwnerTableView.Rebind();
                }
            }
            if (e.CommandName == "LastSheet")
            {
                //TableCell cell = ((Telerik.WebControls.GridFilteringItem)e.Item)["DateTime"];
                TableCell LastSheetCell = ((Telerik.WebControls.GridFilteringItem)e.Item)["LastSheet"];
                Panel LastSheetPnl_Date_Picker = (Panel)LastSheetCell.Controls[0].FindControl("PNL_DATE_PICKER");
                RadDatePicker LastSheetRDP_Date = (RadDatePicker)LastSheetPnl_Date_Picker.FindControl("RadDatePicker1");

                if (LastSheetRDP_Date != null) //changed by Chirag on 08/21/09
                {
                    Fuji.Common.Sessions.SetSessions("StartDate2", LastSheetRDP_Date.SelectedDate);
                    
                    //e.Item.OwnerTableView.FilterExpression = String.Format("DateTime > '{0}'", RDP_Date.SelectedDate.ToString());
                    e.Item.OwnerTableView.FilterExpression = String.Format("LastSheet > '{0}' AND LastSheet < '{1}'", LastSheetRDP_Date.SelectedDate.ToString(), Convert.ToDateTime(LastSheetRDP_Date.SelectedDate.ToString()).AddDays(1).ToString());
                    e.Item.OwnerTableView.Rebind();
                }
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
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
    #endregion


    #region Event Prepress Grid
    protected void GridJobReportPrepress_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========  Clear State ========//
            ViewState["SessKey"] = DateTime.Now.ToString("yyyyMMddHHmmss");

            //========  Check Session  ========//
            if (Fuji.Common.Sessions.GetSession("dtJobReportPrepress") != null)
            {
                //========  Session Store in DataTable  ========//
                DataTable dtJobReport = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReportPrepress");

                //========  Grid Bind  ========//
                GridJobReportPrepress.DataSource = dtJobReport;
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
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

                string _MeasurementStr = "";
                //========  Bind String For QueryString's Parameters ========//
                //if ((Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                //   || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                //   || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat)))
                //{
                //    _MeasurementStr = "ColorTrend.aspx?CPMeasurementGUID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text + "&CPDevicePrintTargetID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDevicePrintTargetID)].Text + "&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text + "";
                //}
                //else if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate))
                //{
                //    _MeasurementStr = "PlateTrend.aspx?CPMeasurementGUID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text + "&CPDevicePrintTargetID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDevicePrintTargetID)].Text + "&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text + "";
                //}
                //else if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                //{
                //    _MeasurementStr = "PressDashboard.aspx?JobID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPMeasurementID)].Text + "&DongleID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPSerialNUM)].Text + "&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text;
                //}


                Int32 intDeviceType = Convert.ToInt32(((Label)e.Item.FindControl("lblCPDeviceTypeID")).Text);
                string strGUID = ((Label)e.Item.FindControl("lblGUID")).Text;
                string strCPDevicePrintTargetID = ((Label)e.Item.FindControl("lblCPDevicePrintTargetID")).Text;
                string strCPDeviceID = ((Label)e.Item.FindControl("lblCPDeviceID")).Text;
                string strCPSerialNUM = ((Label)e.Item.FindControl("lblCPSerialNUM")).Text;
                string strCPMeasurementID = ((Label)e.Item.FindControl("lblCPMeasurementID")).Text;

                if ((intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                   || (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                   || (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat)))
                {
                    _MeasurementStr = "ColorTrend.aspx?CPMeasurementGUID=" + strGUID + "&CPDevicePrintTargetID=" + strCPDevicePrintTargetID + "&CPDeviceID=" + strCPDeviceID + "";
                }
                else if (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate))
                {
                    _MeasurementStr = "PlateTrend.aspx?CPMeasurementGUID=" + strGUID + "&CPDevicePrintTargetID=" + strCPDevicePrintTargetID + "&CPDeviceID=" + strCPDeviceID + "";
                }
                else if (intDeviceType == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                {
                    _MeasurementStr = "PressDashboard.aspx?JobID=" + strCPMeasurementID + "&DongleID=" + strCPSerialNUM + "&CPDeviceID=" + strCPDeviceID;
                }


                //========  Initiate Object For View Image  ========//
                System.Web.UI.HtmlControls.HtmlImage ImgView;
                ImgView = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView");

                System.Web.UI.HtmlControls.HtmlImage ImgView_New;
                ImgView_New = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView_New");

                string _NewId = e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text.Replace("-", "");


                //========  if CPDeviceType = "Color" then Open popup for Detail  ========//
                ImgView.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "&ModalDialog=Y', true, false , '" + _NewId + "');");
                ImgView_New.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', true, true,'" + _NewId + "');");


                #region Hide ImgView_New if Press
                //if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                //{ ImgView_New.Visible = false; }
                #endregion

                #endregion

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

            //====  Set No of Record Per Page for Prepress Grid  ====//
            GridJobReportPrepress.PageSize = Convert.ToInt32(_SelectedValue);
            GridJobReportPrepress.CurrentPageIndex = 0;
            //====  Check Session of Data  ====//
            // Set to Press Grid
            if (Fuji.Common.Sessions.GetSession("dtJobReportPress") != null)
            {
                DataTable DtPress = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReportPress");                                
                GridJobReportPress.DataSource = DtPress;
            }

            // Set to Prepress Grid
            if (Fuji.Common.Sessions.GetSession("dtJobReportPrepress") != null)
            {                
                DataTable DtPrepress = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReportPrepress");                
                GridJobReportPrepress.DataSource = DtPrepress;

            }

            //====  Bind Press Grid  ====//
            GridJobReportPress.DataBind();

            //====  Bind Prepress Grid  ====//
            GridJobReportPrepress.DataBind();


        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
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

            //====  Clear Filter Controls for Prepress Grid  ====//
            foreach (Telerik.WebControls.GridColumn column in GridJobReportPrepress.MasterTableView.Columns)
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


            //=== Set For Prepress Grid

            //====  Set Filter Empty  ====//
            GridJobReportPrepress.MasterTableView.FilterExpression = String.Empty;

            //====  Rebind Master View  ====//
            GridJobReportPrepress.MasterTableView.Rebind();



        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void DDL_CompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //====  Get Data & Fill Session  ====//
            GetData(true);
            GetData(false);

            //====  Bind Grid  ====//
            FillGrid();
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }

    protected void DDL_DateRange_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    #endregion

    #region Methods and Functions
    private void FillGrid()
    {
        try
        {
            //Response.Write("<script language='javascript'>setWidth();</script>");

            DataTable dtJobReportPress = new DataTable();
            DataTable dtJobReportPrepress = new DataTable();

            //====  Check Session ====//

            //====  Get Data & Fill Session  ====//
            if (Fuji.Common.Sessions.GetSession("dtJobReportPress") == null)
                GetData(true);

            if (Fuji.Common.Sessions.GetSession("dtJobReportPrepress") == null)
                GetData(false);


            //====  Fill Data From Session  ====// 
            dtJobReportPress = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReportPress");
            dtJobReportPrepress = (DataTable)Fuji.Common.Sessions.GetSession("dtJobReportPrepress");

            //====  Set Session Key  ====// 
            //ViewState["SessKey"] = DateTime.Now.ToString("yyyyMMddHHmmss");

            // ==== Fill Press Grid ====//

            //====  Set Data Source  ====//
            GridJobReportPress.DataSource = dtJobReportPress;

            //====  Bind Data Grid  ====//
            GridJobReportPress.DataBind();

            // ==== Fill Prepress Grid ====//

            //====  Set Data Source  ====//
            GridJobReportPrepress.DataSource = dtJobReportPrepress;

            //====  Bind Data Grid  ====//
            GridJobReportPrepress.DataBind();

            // ==== Set 100% Width of tab ==== //
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script language='javascript'>setWidth();</script>", false);

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetData(Boolean Press)
    {
        try
        {
            //====  Set Command For Get Data  ====//
            SqlCommand CmdJobReport = new SqlCommand();

            //====  Command Type is StoreProcedure  ====//
            CmdJobReport.CommandType = CommandType.StoredProcedure;

            if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
            {
                //====  Set Command Text  ====//
                CmdJobReport.CommandText = "CPV.uspGetJobReportData_Admin";

                //===  Taskero Admin Select Any one Company ====//
                CmdJobReport.Parameters.AddWithValue("@CompanyID", Convert.ToInt32(DDL_CompanyList.SelectedValue));
                CmdJobReport.Parameters.AddWithValue("@Press", Press);
            }
            else
            {
                //====  Set Command Text  ====//
                CmdJobReport.CommandText = "CPV.uspGetJobReportData";

                //==== Taskero UserID  ====// 
                CmdJobReport.Parameters.AddWithValue("@TaskeroUserID", Sessions.UserID);

                //===  Taskero Admin Select Any one Company ====//
                CmdJobReport.Parameters.AddWithValue("@CompanyID", Convert.ToInt32(DDL_CompanyList.SelectedValue));

                CmdJobReport.Parameters.AddWithValue("@Press", Press);

                if (Sessions.RoleDesc != Enums.RoleType.SupportUser)
                {
                    //====  Find That Company from Company List ====//  
                    DDL_CompanyList.SelectedValue = Sessions.CompanyID.ToString();

                    //====  And show in Label  ====//
                    LTRL_UserCompany.Text = DDL_CompanyList.SelectedItem.Text;
                }
            }

            //===  Fill Data in Session  ====//
            if (Press)
                Fuji.Common.Sessions.SetSessions("dtJobReportPress", (new Fuji.DataAccess.Database()).GetDataTable(CmdJobReport));
            else
                Fuji.Common.Sessions.SetSessions("dtJobReportPrepress", (new Fuji.DataAccess.Database()).GetDataTable(CmdJobReport));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private string GetFilterFormat(Type columnDataType)
    {
        try
        {
            string retVal = String.Empty;

            if (columnDataType.Name.ToString() == "String")
            {
                retVal = "[{0}] = '{1}'";
            }
            else
            {
                retVal = "[{0}] = {1}";
            }

            return retVal;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion    

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
       try
        {
            String[] strSeparator = { " AND " };
            String[] strColumnExp;
            string strFilterExpression = string.Empty;
            string strFindStr = string.Empty;

            if (TabContainer1.ActiveTabIndex == 0)
            {
                foreach (Telerik.WebControls.GridColumn cPress in GridJobReportPress.MasterTableView.Columns)
                {
                    foreach (Telerik.WebControls.GridColumn cPrepress in GridJobReportPrepress.MasterTableView.Columns)
                    {
                        if (cPress.HeaderText == cPrepress.HeaderText)
                        {
                            if (!String.IsNullOrEmpty(cPress.CurrentFilterValue))
                            {
                                cPrepress.CurrentFilterFunction = cPress.CurrentFilterFunction;
                                cPrepress.CurrentFilterValue = cPress.CurrentFilterValue;
                            }
                            else
                            {
                                cPrepress.CurrentFilterFunction = GridKnownFunction.NoFilter;
                                cPrepress.CurrentFilterValue = string.Empty;
                            }
                            break;
                        }
                    }// End Inner Foreach
                }// End outer Foreach

                strColumnExp = GridJobReportPress.MasterTableView.FilterExpression.Split(strSeparator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strColumnExp.Length; i++)
                {
                    strFindStr = strColumnExp[i].ToLower();
                    if (strFindStr.Contains("[job]") || strFindStr.Contains("[dongleserial]") || strFindStr.Contains("[branchname]"))
                    {
                        strFilterExpression += (strFilterExpression == "" ? "" : " AND ") + strColumnExp[i];
                    }
                }

                GridJobReportPrepress.MasterTableView.FilterExpression = strFilterExpression;
                GridJobReportPrepress.MasterTableView.Rebind();
            }
            else
            {

                foreach (Telerik.WebControls.GridColumn cPrepress in GridJobReportPrepress.MasterTableView.Columns)
                {
                    foreach (Telerik.WebControls.GridColumn cPress in GridJobReportPress.MasterTableView.Columns)
                    {
                        if (cPrepress.HeaderText == cPress.HeaderText)
                        {
                            if (!String.IsNullOrEmpty(cPrepress.CurrentFilterValue))
                            {
                                cPress.CurrentFilterValue = cPrepress.CurrentFilterValue;
                                cPress.CurrentFilterFunction = cPrepress.CurrentFilterFunction;
                            }
                            else
                            {
                                cPrepress.CurrentFilterFunction = GridKnownFunction.NoFilter;
                                cPrepress.CurrentFilterValue = string.Empty;
                            }
                            break;
                        }
                    }
                }

                strColumnExp = GridJobReportPrepress.MasterTableView.FilterExpression.Split(strSeparator, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < strColumnExp.Length; i++)
                {
                    strFindStr = strColumnExp[i].ToLower();
                    if (strFindStr.Contains("[job]") || strFindStr.Contains("[dongleserial]") || strFindStr.Contains("[branchname]"))
                    {
                        strFilterExpression += (strFilterExpression == "" ? "" : " AND ") + strColumnExp[i];
                    }

                    GridJobReportPress.MasterTableView.FilterExpression = strFilterExpression;
                    GridJobReportPress.MasterTableView.Rebind();
                }
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }

       ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script language='javascript'>setWidth();</script>", false);
    }
}
