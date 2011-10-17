#region
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
using System.Collections.Generic;
using System.Text;


#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Telerik.WebControls;
using Fuji.Business;
#endregion

public partial class Devices : PageBase
{
    #region Declaration

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
        FlgOpenMeasurementWindow = 24,
        CPSerialNUM = 25

    }

    //====  This is set for Default Page Size ====//
    private const int _DefaultPageSize = 25;

    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                #region Show Hide
                if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.PrintBuyer)
                { TR_Company.Visible = false; }
                #endregion

                //========== Check Login ==========//
                Fuji.Common.Sessions.CheckUserLogin();

                #region ====  Bind Company List  ====

                DataTable dtCompany = new DataTable();

                //====  Clear List  ====//
                DDL_CompanyList.Items.Clear();

                //===  For Show All as a text in DropDwon  ====
                ListItem DDLiShowAll = new ListItem();
                DDLiShowAll.Text = "- - - - - All - - - - -";
                DDLiShowAll.Value = "0";

                //==== Add All Text in DropDwon List ====//
                DDL_CompanyList.Items.Add(DDLiShowAll);

                //====  Get Data For All Company List  ====//
                dtCompany = (new Fuji.Business.CompanyData().uspGetAllCompanyByUserID(Sessions.UserID, Sessions.RoleID));

                //==== Create Object of List Itme  ====//
                ListItem DDLItem;

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

                //==== Add All Text in DropDwon List as Default ====//
                DDLiShowAll = new ListItem();
                DDLiShowAll.Text = "- - - - - All - - - - -";
                DDLiShowAll.Value = "0";
                DDL_BranchList.Items.Add(DDLiShowAll);

                //==== Add All Text in DropDwon List as Default ====//
                DDLiShowAll = new ListItem();
                DDLiShowAll.Text = "- - - - - All - - - - -";
                DDLiShowAll.Value = "0";
                DDL_DongleList.Items.Add(DDLiShowAll);

                if (Fuji.Common.Sessions.RoleDesc != Enums.RoleType.TaskeroAdmin && Fuji.Common.Sessions.RoleDesc != Enums.RoleType.SupportUser)
                {
                    //====  Find That Company from Company List ====//  
                    DDL_CompanyList.SelectedValue = Sessions.CompanyID.ToString();

                    DDL_CompanyList_SelectedIndexChanged(null, null);

                    DDL_CompanyList.Visible = false;

                    LTRL_CompanyList.Text = DDL_CompanyList.SelectedItem.Text;
                }
                else
                {
                    DDL_CompanyList.Visible = true;

                    LTRL_CompanyList.Visible = false;
                }

                //====  Get Data & Fill Session  ====//
                GetData(false);
                //========= Clear the date for first time =========//
                Fuji.Common.Sessions.SetSessions("StartDate", null);
                //====  Bind Grid  ====//
                FillGrid();


                #endregion
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DDL_CompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            #region ====  Bind Branch List  ====

            DataTable dtBranch = new DataTable();

            //====  Clear List  ====//
            DDL_BranchList.Items.Clear();

            //===  For Show All as a text in DropDwon  ====
            ListItem DDLiShowAll = new ListItem();
            DDLiShowAll.Text = "- - - - - All - - - - -";
            DDLiShowAll.Value = "0";

            //==== Add All Text in DropDwon List ====//
            DDL_BranchList.Items.Add(DDLiShowAll);

            //====  Get Data For All Branch List  ====//
            Fuji.Business.BranchData objBranch = new Fuji.Business.BranchData();
            objBranch.CompanyID = Convert.ToInt32(DDL_CompanyList.SelectedValue);
            dtBranch = objBranch.uspGetBranchByCompanyID();

            //==== Create Object of List Itme  ====//
            ListItem DDLItem;

            for (int i = 0; i < dtBranch.Rows.Count; i++)
            {
                //==== Create List Item  ====//
                DDLItem = new ListItem();

                //====  Set Branch Name as a Text  ====//
                DDLItem.Text = dtBranch.Rows[i]["BranchName"].ToString();

                //====  Set Branch Id as a Value  ====//
                DDLItem.Value = dtBranch.Rows[i]["BranchID"].ToString();

                //====  Add List Item In DropDown  ====//
                DDL_BranchList.Items.Add(DDLItem);

            }

            #endregion

            #region ====  Clear Dongle Data  ====

            DDL_DongleList.Items.Clear();
            DDL_DongleList.Items.Add(DDLiShowAll);

            #endregion

            DDL_BranchList.SelectedIndex = -1;
            DDL_DongleList.SelectedIndex = -1;

            //====  Get Data & Fill Session  ====//
            GetData(false);

            //====  Bind Grid  ====//
            FillGrid();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DDL_BranchList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            #region ====  Bind Dongle List  ====

            DataTable dtDongle = new DataTable();

            //====  Clear List  ====//
            DDL_DongleList.Items.Clear();

            //===  For Show All as a text in DropDwon  ====
            ListItem DDLiShowAll = new ListItem();
            DDLiShowAll.Text = "- - - - - All - - - - -";
            DDLiShowAll.Value = "0";

            //==== Add All Text in DropDwon List ====//
            DDL_DongleList.Items.Add(DDLiShowAll);

            //====  Get Data For All Branch List  ====//
            Fuji.Business.CPDongleData objDongle = new Fuji.Business.CPDongleData();
            objDongle.BranchID = Convert.ToInt32(DDL_BranchList.SelectedValue);
            dtDongle = objDongle.uspGetDongleListByBranchID();

            //==== Create Object of List Itme  ====//
            ListItem DDLItem;

            for (int i = 0; i < dtDongle.Rows.Count; i++)
            {
                //==== Create List Item  ====//
                DDLItem = new ListItem();

                //====  Set Branch Name as a Text  ====//
                DDLItem.Text = dtDongle.Rows[i]["DongleDescription"].ToString();

                //====  Set Branch Id as a Value  ====//
                DDLItem.Value = dtDongle.Rows[i]["CPDongleID"].ToString();

                //====  Add List Item In DropDown  ====//
                DDL_DongleList.Items.Add(DDLItem);

            }

            #endregion

            DDL_DongleList.SelectedIndex = -1;

            //====  Get Data & Fill Session  ====//
            GetData(false);

            //====  Bind Grid  ====//
            FillGrid();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DDL_DongleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //====  Get Data & Fill Session  ====//
            GetData(false);

            //====  Bind Grid  ====//
            FillGrid();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string _SelectedValue;
            _SelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = _SelectedValue;

            
            GridDevicesPrepress.PageSize = Convert.ToInt32(_SelectedValue);
            GridDevicesPrepress.CurrentPageIndex = 0;
            //====  Check Session of Data  ====//
            // Set for Press Grid
            // Set for Prepress Grid
            if (Fuji.Common.Sessions.GetSession("dtDevicesPrepress") != null)
            {
                DataTable Dt = (DataTable)Fuji.Common.Sessions.GetSession("dtDevicesPrepress");
                GridDevicesPrepress.DataSource = Dt;
            }

            //====  Bind Grid  ====//            
            GridDevicesPrepress.DataBind();


        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    private void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
           
           
            //====  Clear Filter Controls for Prepress Grid  ====//
            foreach (Telerik.WebControls.GridColumn column in GridDevicesPrepress.MasterTableView.Columns)
            {
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }

            Fuji.Common.Sessions.SetSessions("StartDate", null);
           
            

            //====  Set Filter Empty for Prepress Grid  ====//
            GridDevicesPrepress.MasterTableView.FilterExpression = String.Empty;

            //====  Rebind Master View for Prepress Grid  ====//
            GridDevicesPrepress.MasterTableView.Rebind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

   


    #region Event Prepress Grid
    protected void GridDevicesPrepress_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (Fuji.Common.Sessions.GetSession("dtDevicesPrepress") != null)
            {
                //========  Session Store in DataTable  ========//
                DataTable dtDevices = (DataTable)Fuji.Common.Sessions.GetSession("dtDevicesPrepress");

                //========  Grid Bind  ========//
                GridDevicesPrepress.DataSource = dtDevices;
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridDevicesPrepress_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
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
                _FirstIndexInPage = GridDevicesPrepress.MasterTableView.PagingManager.FirstIndexInPage;
                _LastIndexInPage = GridDevicesPrepress.MasterTableView.PagingManager.LastIndexInPage + 1;
                _DataSourceCount = GridDevicesPrepress.MasterTableView.PagingManager.DataSourceCount;

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
                if (GridDevicesPrepress.MasterTableView.PagingManager.IsFirstPage)
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
                if (GridDevicesPrepress.MasterTableView.PagingManager.IsLastPage == true)
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

                if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumDeviceStatus.Green.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/Green-Lamp.gif"; //========  Failed Then Green Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumDeviceStatus.Green.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumDeviceStatus.Yellow.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/Yellow-Lamp.gif";//========  Caution Then Yellow Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumDeviceStatus.Yellow.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumDeviceStatus.Orange.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/Orange-Lamp.gif";//========  Passed Then Green Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumDeviceStatus.Orange.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumDeviceStatus.Red.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/Red-Lamp.gif";//========  Passed Then red Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumDeviceStatus.Red.ToString());
                }
                else if (e.Item.Cells[Convert.ToInt32(enumGridColumn.hdnSatus)].Text.Trim() == Fuji.Common.Enums.enumDeviceStatus.None.ToString())
                {
                    //========  Set Status Image Path And Title  ========//
                    ibtnStatus.Src = "Images/spacer.gif";//========  Passed Then No Star  ========//
                    ibtnStatus.Attributes.Add("title", Fuji.Common.Enums.enumDeviceStatus.None.ToString());
                }
                #endregion

                #region ====  View Setting  ====

                //========  Initiate Object For View Image  ========//
                System.Web.UI.HtmlControls.HtmlImage ImgView;
                ImgView = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView");

                System.Web.UI.HtmlControls.HtmlImage ImgView_New;
                ImgView_New = (System.Web.UI.HtmlControls.HtmlImage)e.Item.FindControl("ImgView_New");

                if ((Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.FlgOpenMeasurementWindow)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumYesNo.No)))
                {
                    //========  if No Measurement -- Alert Message  ========//
                    ImgView.Attributes.Add("onclick", "javascript:alert('No records are available for " + Fuji.Common.Sessions.FirstName + " " + Fuji.Common.Sessions.LastName + ".');");
                    ImgView_New.Attributes.Add("onclick", "javascript:alert('No records are available for " + Fuji.Common.Sessions.FirstName + " " + Fuji.Common.Sessions.LastName + ".');");

                }
                else
                {
                    //========  Bind String For QueryString's Parameters ========//

                    string _MeasurementStr = "";
                    //========  Bind String For QueryString's Parameters ========//
                    if ((Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Color))
                       || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Monitor))
                       || (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.WideFormat)))
                    { _MeasurementStr = "ColorTrend.aspx?CPMeasurementGUID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text + "&CPDevicePrintTargetID=0&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text; }
                    else if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Plate))
                    { _MeasurementStr = "PlateTrend.aspx?CPMeasurementGUID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text + "&CPDevicePrintTargetID=0&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text; }
                    else if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                    { _MeasurementStr = "PressDashboard.aspx?JobID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPMeasurementID)].Text + "&DongleID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.CPSerialNUM)].Text + "&CPDeviceID=" + e.Item.Cells[Convert.ToInt32(enumGridColumn.DeviceID)].Text; }

                    string _NewId = e.Item.Cells[Convert.ToInt32(enumGridColumn.GUID)].Text.Replace("-", "");

                    //========  if CPDeviceType = "Color" then Open popup for Detail  ========//
                    ImgView.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "&ModalDialog=Y', true, false, '" + _NewId + "');");
                    ImgView_New.Attributes.Add("onclick", "OpenMeasurementWindow('" + _MeasurementStr + "', true, true, '" + _NewId + "');");
                }
                #endregion

                #region Hide If the Type is Press
                //if (Convert.ToInt32(e.Item.Cells[Convert.ToInt32(enumGridColumn.CPDeviceTypeID)].Text.Trim()) == Convert.ToInt32(Fuji.Common.Enums.enumDeviceType.Press))
                //{ ImgView_New.Visible = false; }
                #endregion
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridDevicesPrepress_ItemCommand(object source, GridCommandEventArgs e)
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
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void GridDevicesPrepress_ItemCreated(object sender, Telerik.WebControls.GridItemEventArgs e)
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
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion



    #endregion

    #region Methods & Functions

    private void FillGrid()
    {
        try
        {
            DataTable dtDevicesPress = new DataTable();
            DataTable dtDevicesPrepress = new DataTable();

            //====  Check Session ====//
            //====  Get Data & Fill Session  ====//        
            if (Fuji.Common.Sessions.GetSession("dtDevicesPrepress") == null)
                GetData(false);

            //====  Fill Data From Session  ====// 
            dtDevicesPress = (DataTable)Fuji.Common.Sessions.GetSession("dtDevicesPress");
            dtDevicesPrepress = (DataTable)Fuji.Common.Sessions.GetSession("dtDevicesPrepress");


            //==== Bind Prepress Grid  =====//
            //====  Set Data Source  ====//
            GridDevicesPrepress.DataSource = dtDevicesPrepress;

            //====  Bind Data Grid  ====//
            GridDevicesPrepress.DataBind();
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
            //========== Declarations ==========//
            CPMeasurementData objCPMeasurement = new CPMeasurementData();
            if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                Fuji.Common.Sessions.SetSessions("dtDevicesPrepress", objCPMeasurement.uspGetDeviceReportData_Admin(Convert.ToInt32(DDL_CompanyList.SelectedValue), Press));
            else
                Fuji.Common.Sessions.SetSessions("dtDevicesPrepress", objCPMeasurement.uspGetDeviceReportData(Convert.ToInt32(DDL_CompanyList.SelectedValue), Sessions.UserID, Press));        

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion

}
