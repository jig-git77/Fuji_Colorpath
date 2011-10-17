#region "Using System"
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

#region "Using Application"
using Telerik.WebControls;
using Fuji.Common;
using Fuji.Business;
#endregion

public partial class SyncMeasurementDevice : PageBase
{
    #region Private Variable Declaration
    //========== Declare the constant page size for the grid ===========//
    private const int ConstDefaultPageSize = 25;
    
    #endregion

    #region PageLoad and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear Error ==========//
            LBL_Error.Text = "";

            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //=========== Check Login ==========//
                Sessions.CheckUserLogin();

                //========== Check User Role and redirect to home page as per user role ===========//
                Fuji.Common.CommonFunctions.CheckUserRole();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Clear DongleList From Session ==========//   
                Fuji.Common.Sessions.SetSessions("SyncMeasurementDeviceData", null);
                //========= Clear the date for first time =========//
                Fuji.Common.Sessions.SetSessions("StartDate", null);
                //=========== Bind DongleList ==========//   
                BindSyncMeasurementDevice();

                //========== Fill CompanyList in DropDownList ===========//
                FillCompany();

                //========== Fill BranchList by CompanyID in DropDownList ===========//
                FillBranch(0);

                //========== Fill DongleList by  ===========//
                FillDongle(0);

                //========== Fill DongleList by  ===========//
                FillDeviceType();

                //=========== Set Css ==========//
                //Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_MasterCompany);
                RG_SyncMeasurementDevice.PageSize = ConstDefaultPageSize;

                //=========== Clear Form ==========//
                BTN_Cancel_Click(null, null);

                //========== Check User rights  for the dongle management===========//
                CheckRights();
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void DDL_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Check Company Dropdownlist and its index and bind branch based on selected company ===========//
            if (DDL_Company.SelectedIndex != 0)
            { FillBranch(Convert.ToInt32(DDL_Company.SelectedValue.Trim())); }
            else
            {
                //========== Show all branch list, if company not selected ===========//
                FillBranch(0);
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void DDL_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Check Branch Dropdownlist and its index and bind dongle based on selected branch ===========//
            if (DDL_Branch.SelectedIndex != 0)
            { FillDongle(Convert.ToInt32(DDL_Branch.SelectedValue.Trim())); }
            else
            {
                //========== Show all branch list, if company not selected ===========//
                FillDongle(0);
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void BTN_AddSycnMeasurementDevice_Click(object sender, EventArgs e)
    {
        try
        {
            TBLFORM.Visible = true;
            TBLGRID.Visible = false;
            BTN_AddSycnMeasurementDevice.Visible = false;
            LTRL_SubTitle.Text = "Add Sync Measurement Device";
            //LTRL_DeviceSerialNum.Text = "Device Serial Number";
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void BTN_AddSave_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validations For the new company inputs ==========//
            if (DDL_Company.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.CompanyNameIsRequired; return; }
            if (DDL_Branch.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.BranchNameIsRequired; return; }
            if (DDL_Dongle.SelectedIndex == 0) { LBL_Error.Text = "Dongle is required"; return; }
            if (TXT_SerialNum.Text.Trim() == "") { LBL_Error.Text = "Serial Number is required"; return; }

            //==========  Declaring Objects ==========//
            CPSyncMeasurementDeviceData ObjSyncMeasurementDevice = new CPSyncMeasurementDeviceData();

            //========= Validation ==================//
            DataTable DT_SyncMeasurementDeviceList = new DataTable();
            if (Fuji.Common.Sessions.GetSession("SyncMeasurementDeviceData") != null)
            {
                //========== Get the session Data From the Session ===========//
                DT_SyncMeasurementDeviceList = (DataTable)Fuji.Common.Sessions.GetSession("SyncMeasurementDeviceData");
                if (DT_SyncMeasurementDeviceList != null)
                {
                    DT_SyncMeasurementDeviceList.DefaultView.RowFilter = "SerialNumber = " + TXT_SerialNum.Text + " AND DeviceTypeID = " + Convert.ToInt16(Enum.Parse(typeof(Enums.SyncMeasurementDeviceType), DDL_DeviceType.SelectedValue)).ToString() + " AND CPSyncMeasurementDeviceID <> " + LTRL_CPSyncMeasurementDeviceID.Text.Trim();
                    if (DT_SyncMeasurementDeviceList.DefaultView.Count > 0)
                    {
                        //========== Show the new dongle insert message ===========//
                        LBL_Error.Text = "This device is already added.";
                        return;
                    }
                }
            }


            
            //========== Declate the dongle data table object ===========//
            DataTable DT_SyncMeasurementDevice = new DataTable();

            //========== Assign values to the dongle object ===========//
            ObjSyncMeasurementDevice.CPDongleID = Convert.ToInt16(DDL_Dongle.SelectedValue.Trim());

            //========== check the Sync device id, if not exist then insert new dongle ===========//
            if (LTRL_CPSyncMeasurementDeviceID.Text.Trim() == "0")
            {

                ObjSyncMeasurementDevice.CPDongleID = Convert.ToInt16(DDL_Dongle.SelectedValue.Trim());
                ObjSyncMeasurementDevice.SerialNumber = TXT_SerialNum.Text.Trim();
                ObjSyncMeasurementDevice.DeviceTypeID = Convert.ToInt16(Enum.Parse(typeof(Enums.SyncMeasurementDeviceType), DDL_DeviceType.SelectedValue));                
                ObjSyncMeasurementDevice.isActive= Convert.ToBoolean(CHK_IsActive.Checked);
                ObjSyncMeasurementDevice.Insert();

                //========== Show the new dongle insert message ===========//
                LBL_Error.Text = "Sync measurement device added successfully.";
            }

            //========== check the Sync device id, if exist then update existing dongle ===========//
            if (LTRL_CPSyncMeasurementDeviceID.Text.Trim() != "0")
            {
                ObjSyncMeasurementDevice.CPSyncMeasurementDeviceID = Convert.ToInt16(LTRL_CPSyncMeasurementDeviceID.Text.Trim());
                ObjSyncMeasurementDevice.SerialNumber = TXT_SerialNum.Text.Trim();
                ObjSyncMeasurementDevice.DeviceTypeID = Convert.ToInt16(Enum.Parse(typeof(Enums.SyncMeasurementDeviceType), DDL_DeviceType.SelectedValue));
                ObjSyncMeasurementDevice.isActive = Convert.ToBoolean(CHK_IsActive.Checked);

                ObjSyncMeasurementDevice.Update();

                //========== Show the dongle update message ===========//
                LBL_Error.Text = "Sync measurement device updated successfully.";
            }

            //=========== Default Settings ==========//
            BTN_Cancel_Click(null, null); Fuji.Common.Sessions.SetSessions("SyncMeasurementDeviceData", null);

            
            //========== Bind the device data ===========//
            BindSyncMeasurementDevice();
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void BTN_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            BTN_AddSave.Text = Resources.UserManagement.BTN_AddSave_Add;
            LTRL_CPSyncMeasurementDeviceID.Text = "0";
            DDL_Company.SelectedIndex = 0;
            DDL_Branch.SelectedIndex = 0;
            DDL_Dongle.SelectedIndex = 0;
            TXT_SerialNum.Text = "";
            BTN_AddSycnMeasurementDevice.Text = "Add Sync Measurement Device";
            TBLFORM.Visible = false;
            TBLGRID.Visible = true; BTN_AddSycnMeasurementDevice.Visible = true;

            DDL_Company.Enabled = true;
            DDL_Branch.Enabled = true;
            DDL_Dongle.Enabled = true;
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_SyncMeasurementDevice_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        try
        {
            //========== Assign new page index to current page indes ===========//
            RG_SyncMeasurementDevice.CurrentPageIndex = e.NewPageIndex;

            //========== Bind the dongle data ===========//
            BindSyncMeasurementDevice();
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_SyncMeasurementDevice_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========== Check Session For the DongleData ===========//
            if (Fuji.Common.Sessions.GetSession("SyncMeasurementDeviceData") != null)
            {
                //========== Get the session Data From the Session ===========//                
                RG_SyncMeasurementDevice.DataSource = Fuji.Common.Sessions.GetSession("SyncMeasurementDeviceData");
            }
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_SyncMeasurementDevice_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== If Command Name is Edit then comes to this end of code ===========//
            if (e.CommandName == "EditSyncMeasurementDevice")
            {
                LTRL_CPSyncMeasurementDeviceID.Text = e.Item.Cells[2].Text.Trim();
                DDL_Company.SelectedValue = e.Item.Cells[4].Text.Trim();
                DDL_Company_SelectedIndexChanged(null, null);
                DDL_Branch.SelectedValue = e.Item.Cells[3].Text.Trim();
                DDL_Dongle.SelectedValue = e.Item.Cells[8].Text.Trim();
                TXT_SerialNum.Text = e.Item.Cells[9].Text.Trim();
                DDL_DeviceType.SelectedValue = Enum.GetName(typeof(Fuji.Common.Enums.SyncMeasurementDeviceType), Convert.ToInt16(e.Item.Cells[10].Text.Trim()));
                CHK_IsActive.Checked = Convert.ToBoolean(e.Item.Cells[12].Text.Trim());
                BTN_AddSave.Text = "Save"; TBLFORM.Visible = true;
                LTRL_SubTitle.Text = "Edit Sync Measurement Device";
                TBLGRID.Visible = false; BTN_AddSycnMeasurementDevice.Visible = false;
                //LTRL_DeviceSerialNum.Text = "Device Serial Number";


                DDL_Company.Enabled = false;
                DDL_Branch.Enabled = false;
                DDL_Dongle.Enabled = false;
            }
          
            if (e.CommandName == "SelectDate")
            {
                TableCell cell = ((Telerik.WebControls.GridFilteringItem)e.Item)["LastModifiedDate"];//PNL_DATE_PICKER
                Panel Pnl_Date_Picker = (Panel)cell.Controls[0].FindControl("PNL_DATE_PICKER");
                RadDatePicker RDP_Date = (RadDatePicker)Pnl_Date_Picker.FindControl("RadDatePicker1");

                Fuji.Common.Sessions.SetSessions("StartDate", RDP_Date.SelectedDate);

                //e.Item.OwnerTableView.FilterExpression = String.Format("LastModifiedDate > '{0}'", RDP_Date.SelectedDate.ToString());
                e.Item.OwnerTableView.FilterExpression = String.Format("LastModifiedDate > '{0}' AND LastModifiedDate < '{1}'", RDP_Date.SelectedDate.ToString(), Convert.ToDateTime(RDP_Date.SelectedDate.ToString()).AddDays(1).ToString());
                e.Item.OwnerTableView.Rebind();
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_SyncMeasurementDevice_SortCommand(object source, GridSortCommandEventArgs e)
    {
        try
        {
            //========== Check Whether the table contains the passed sort expression or not ===========//
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                //========== Declare the sort expression object ===========//
                GridSortExpression SortExpr = new GridSortExpression();

                //========== Assign the sort expression value to its object ===========//
                SortExpr.FieldName = e.SortExpression;
                SortExpr.SortOrder = GridSortOrder.Ascending;

                //========== Add sort expression object to Table ===========//
                e.Item.OwnerTableView.SortExpressions.AddSortExpression(SortExpr);
            }
        }
        catch (Exception Ex)
        {
            LBL_Error.Text = HandleException(Ex);
        }
    }

    protected void RG_SyncMeasurementDevice_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {            
            //========== Check for the pager style function ===========//
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                #region Pager Style

                //========== Find the label and link button inthe pager ===========//
                Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");
                ImageButton LBTN_First = (ImageButton)e.Item.FindControl("LBTN_First");
                ImageButton LBTN_Prev = (ImageButton)e.Item.FindControl("LBTN_Prev");
                ImageButton LBTN_Next = (ImageButton)e.Item.FindControl("LBTN_Next");
                ImageButton LBTN_Last = (ImageButton)e.Item.FindControl("LBTN_Last");

                //========== Declare the variable fot the pages and assign to initial value ===========//
                int IntFirstIndexInPage = 0, IntLastIndexInPage = 0, IntDataSourceCount = 0;

                //========== Assign Grid value to Variable ===========//
                IntFirstIndexInPage = RG_SyncMeasurementDevice.MasterTableView.PagingManager.FirstIndexInPage;
                IntLastIndexInPage = RG_SyncMeasurementDevice.MasterTableView.PagingManager.LastIndexInPage + 1;
                IntDataSourceCount = RG_SyncMeasurementDevice.MasterTableView.PagingManager.DataSourceCount;

                if (IntDataSourceCount > 0)
                {
                    IntFirstIndexInPage++;
                }

                LTRL_RecordText.Text = "Record(s) : " + IntFirstIndexInPage + " to " + IntLastIndexInPage + " of " + IntDataSourceCount + " | ";

                if (RG_SyncMeasurementDevice.MasterTableView.PagingManager.IsFirstPage)
                {
                    LBTN_First.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    LBTN_Prev.ImageUrl = "Images/Nav_PreviousPage_off.gif";
                    LBTN_First.Style.Add("cursor", "");
                    LBTN_Prev.Style.Add("cursor", "");
                    LBTN_First.Attributes.Add("disabled", "true");
                    LBTN_Prev.Attributes.Add("disabled", "true");
                }
                else
                {
                    LBTN_First.ImageUrl = "Images/Nav_FirstPage.gif";
                    LBTN_Prev.ImageUrl = "Images/Nav_PreviousPage.gif";
                    LBTN_First.Style.Add("cursor", "pointer");
                    LBTN_Prev.Style.Add("cursor", "pointer");
                }
                if (RG_SyncMeasurementDevice.MasterTableView.PagingManager.IsLastPage == true)
                {
                    LBTN_Next.ImageUrl = "Images/Nav_NextPage_off.gif";
                    LBTN_Last.ImageUrl = "Images/Nav_LastPage_off.gif";
                    LBTN_Next.Style.Add("cursor", "");
                    LBTN_Last.Style.Add("cursor", "");
                    LBTN_Next.Attributes.Add("disabled", "true");
                    LBTN_Last.Attributes.Add("disabled", "true");
                }
                else
                {
                    LBTN_Next.ImageUrl = "Images/Nav_NextPage.gif";
                    LBTN_Last.ImageUrl = "Images/Nav_LastPage.gif";
                    LBTN_Next.Style.Add("cursor", "pointer");
                    LBTN_Last.Style.Add("cursor", "pointer");
                }
                #endregion
            }
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            //========== Declare the variable for the selection value of the record page dropdownlist ===========//
            string StrSelectedValue;
            StrSelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = StrSelectedValue;

            //========== Set the paze size ===========//
            RG_SyncMeasurementDevice.PageSize = Convert.ToInt32(StrSelectedValue);
            RG_SyncMeasurementDevice.CurrentPageIndex = 0;
            //========== Check for branch data in session and assign if exist ===========//
            if (Fuji.Common.Sessions.GetSession("DongleData") != null)
            {
                //========== Assign the branch data fro the session value ===========//
                RG_SyncMeasurementDevice.DataSource = Fuji.Common.Sessions.GetSession("DongleData");
            }

            //========== Bind the grid ===========//
            RG_SyncMeasurementDevice.DataBind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    private void BTN_Reset_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Check grid column in the grid ===========//
            foreach (Telerik.WebControls.GridColumn column in RG_SyncMeasurementDevice.MasterTableView.Columns)
            {
                //========== set all column filter value to empty ===========//
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }
            Fuji.Common.Sessions.SetSessions("StartDate", null);
            //========== set all filter expression to empty ===========//
            RG_SyncMeasurementDevice.MasterTableView.FilterExpression = String.Empty;
            RG_SyncMeasurementDevice.MasterTableView.Rebind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void RG_SyncMeasurementDevice_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check and create events for the pager selection  ===========//
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
            {
                //========== Find the Record page dropdownlist ===========//
                DropDownList DDL_RecordPage = (DropDownList)e.Item.FindControl("DDL_RecordPage");

                if (DDL_RecordPage != null)
                {
                    //========== Declare its event ===========//
                    DDL_RecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPage_SelectedIndexChanged);

                    if (!IsPostBack)
                    {
                        //========== Assign record page selection value ===========//
                        DDL_RecordPage.SelectedValue = ConstDefaultPageSize.ToString();
                    }
                    else
                    {
                        if (ViewState["RecordPerPage"] != null)
                        {
                            DDL_RecordPage.SelectedIndex = -1;
                            DDL_RecordPage.Items.FindByValue(ViewState["RecordPerPage"].ToString()).Selected = true;
                        }
                    }
                }

                //========== Find the Reset button in the pager style ===========//
                Button BTN_Reset = (Button)e.Item.FindControl("BTN_Reset");
                BTN_Reset.Text = Resources.DongleManagement.BTN_Reset_Text;
                if (BTN_Reset != null)
                {
                    //========== Declate the reset button event ===========//
                    BTN_Reset.Click += new EventHandler(BTN_Reset_Click);
                }
            }
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }
    #endregion

    #region Methods and Events

    #region Set Labels
    /// <summary>
    /// Used to set the labels of the form
    /// </summary>
    private void SetLabels()
    {
        try
        {

            //========== Set Values ==========//
            LTRL_Title.Text = "Sync Measurement Device";
            LTRL_SubTitle.Text = "Add Sync Measurement Device";
            LTRL_CompanyName.Text = "Company Name";
            LTRL_BranchName.Text = "Branch Name";
            LTRL_DongleSerialNo.Text = "Dongle Serial No.";
            LTRL_SerialNum.Text = "Serial No.";
            LTRL_DeviceType.Text = "Device Type";
            LTRL_Active.Text = "Is Active";

            BTN_AddSycnMeasurementDevice.Text = "Add Sync Measurement Device";
            BTN_AddSave.Text = "Add";
            BTN_Cancel.Text = "Cancel";

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Bind Sync Measurement Device 
    /// <summary>
    /// Used to bind the Sync Measurement Device
    /// </summary>
    private void BindSyncMeasurementDevice()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncMeasurementDeviceData ObjCPSyncMeasurementDevice = new CPSyncMeasurementDeviceData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncMeasurementDevice = new DataTable();

            if (Fuji.Common.Sessions.GetSession("SyncMeasurementDeviceData") == null)
            {
                DT_SyncMeasurementDevice = ObjCPSyncMeasurementDevice.uspGetSyncMeasurementDevice();
                Fuji.Common.Sessions.SetSessions("SyncMeasurementDeviceData", DT_SyncMeasurementDevice);
            }

            //========== Get the session Data From the Session ===========//
            DT_SyncMeasurementDevice = (DataTable)Fuji.Common.Sessions.GetSession("SyncMeasurementDeviceData");

            //========== Assign the session data to Grid ===========//
            if (DT_SyncMeasurementDevice != null && DT_SyncMeasurementDevice.Rows.Count > 0)
            { RG_SyncMeasurementDevice.DataSource = DT_SyncMeasurementDevice; RG_SyncMeasurementDevice.DataBind(); }
            else
            { RG_SyncMeasurementDevice.DataSource = null; RG_SyncMeasurementDevice.DataBind(); }

            //========== Show the pager style if page count is greater than 1 ===========//
            if (RG_SyncMeasurementDevice.Items.Count == 0)
            { RG_SyncMeasurementDevice.PagerStyle.Visible = false; RG_SyncMeasurementDevice.MasterTableView.AllowFilteringByColumn = false; RG_SyncMeasurementDevice.AllowSorting = false; }
            else
            { RG_SyncMeasurementDevice.PagerStyle.Visible = true; RG_SyncMeasurementDevice.MasterTableView.AllowFilteringByColumn = true; RG_SyncMeasurementDevice.AllowSorting = true; }

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region FillCompany
    /// <summary>
    /// Used to fill the company data to dropdownlist
    /// </summary>
    private void FillCompany()
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_Company.Items.Clear();

            //==========  Declaring Objects ==========//
            CPSyncPressData ObjSyncPress = new CPSyncPressData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Company = new DataTable();
            DT_Company = ObjSyncPress.uspGetCompanyListAndBranchList(0);
          
            DT_Company = DT_Company.DefaultView.ToTable(true, new String[] { "CompanyName", "CompanyID" });
            if (DT_Company != null && DT_Company.Rows.Count > 0)
            {
                DDL_Company.DataSource = DT_Company;
                DDL_Company.DataValueField = "CompanyID"; DDL_Company.DataTextField = "CompanyName";
                DDL_Company.DataBind();
            }
            else
            { DDL_Company.DataSource = null; DDL_Company.DataBind(); }

            //========== Insert Items DropDownList in Company ===========//
            DDL_Company.Items.Insert(0, new ListItem("--- Select Company ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region FillBranch
    /// <summary>
    /// Used to bind the branches based on the company
    /// </summary>
    /// <param name="intCompanyID">Company id to be passed to fetch the branches</param>
    private void FillBranch(int _CompanyID)
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_Branch.Items.Clear();

            //==========  Declaring Objects ==========//
            CPSyncPressData ObjSyncPress = new CPSyncPressData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Branch = new DataTable();
            DT_Branch = ObjSyncPress.uspGetCompanyListAndBranchList(_CompanyID);
            if (DT_Branch != null && DT_Branch.Rows.Count > 0)
            {
                DDL_Branch.DataSource = DT_Branch;
                DDL_Branch.DataValueField = "BranchID"; DDL_Branch.DataTextField = "BranchName";
                DDL_Branch.DataBind();
            }
            else
            { DDL_Branch.DataSource = null; DDL_Branch.DataBind(); }

            //========== Insert Items DropDownList in Company ===========//
            DDL_Branch.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Bind Device Type
    private void FillDeviceType()
    {
        DDL_DeviceType.DataSource = Enum.GetNames(typeof(Enums.SyncMeasurementDeviceType));
        DDL_DeviceType.DataBind();
    }
    #endregion



    #region Check User Rights
    /// <summary>
    /// Used to check user rights for the dongle management
    /// </summary>
    public void CheckRights()
    {
        try
        {
            //========== Check Whether the logged in user has the permission to Add New Dongle, if not then hide Add new Dongle button ===========//
            //if (Fuji.Common.Sessions.Add_Dongle == true)
            //    BTN_AddDevice.Visible = true;
            //else BTN_AddDevice.Visible = false;

            //========== Check Whether the logged in user has the permission to Edit Dongle, if not then hide Edit Column from the Dongle list ===========//
            //if (Fuji.Common.Sessions.Update_Dongle == true)
            //    RG_SyncDevice.Columns[10].Visible = true;
            //else RG_SyncDevice.Columns[10].Visible = false;

            //========== Check Whether the logged in user has the permission to Delete Dongle, if not then hide Delete Column from the Dongle list ===========//
            //if (Fuji.Common.Sessions.Delete_Dongle == true)
            //    RG_Dongle.Columns[11].Visible = true;
            //else 
            //RG_SyncDevice.Columns[10].Visible = false;


        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region FillDongle
    /// <summary>
    /// Used to bind the sync dongle based on the branch
    /// </summary>
    /// <param name="_BranchID">Branch id of the branch</param>
    private void FillDongle(int _BranchID)
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_Dongle.Items.Clear();

            CPSyncDeviceData objSyncDongleData = new CPSyncDeviceData();


            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Dongle = new DataTable();
            DT_Dongle = objSyncDongleData.uspGetDongleByBranch(_BranchID);


            if (DT_Dongle != null && DT_Dongle.Rows.Count > 0)
            {
                DDL_Dongle.DataSource = DT_Dongle;
                DDL_Dongle.DataValueField = "CPDongleID"; DDL_Dongle.DataTextField = "CPSerialNum";//CPSerialNUM
                DDL_Dongle.DataBind();
            }
            else
            { DDL_Dongle.DataSource = null; DDL_Dongle.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_Dongle.Items.Insert(0, new ListItem("--- Select Dongle ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion
   
}