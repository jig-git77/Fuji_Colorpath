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

public partial class SyncManagement : PageBase
{
    #region Private Variable Declaration
    //========== Declare the constant page size for the grid ===========//
    private const int ConstDefaultPageSize = 25;
    bool BlnViewDevicePage = false;
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
                Fuji.Common.Sessions.SetSessions("SyncDeviceData", null);
                //========= Clear the date for first time =========//
                Fuji.Common.Sessions.SetSessions("StartDate", null);
                //=========== Bind DongleList ==========//   
                BindSyncDeviceManagement();

                //========== Fill CompanyList in DropDownList ===========//
                FillCompany();

                //========== Fill BranchList by CompanyID in DropDownList ===========//
                FillBranch(0);

                //========== Fill DongleList by  ===========//
                FillDongle(0);

                //=========== Set Css ==========//
                //Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_MasterCompany);
                RG_SyncDevice.PageSize = ConstDefaultPageSize;

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

    protected void BTN_AddDevice_Click(object sender, EventArgs e)
    {
        try
        {
            TBLFORM.Visible = true;
            TBLGRID.Visible = false;
            BTN_AddDevice.Visible = false;
            LTRL_SubTitle.Text = "Add Sync Device";
            LTRL_DeviceSerialNum.Text = "Device Serial Number"; 
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
            if (TXT_CPSyncDeviceName.Text.Trim() == "") { LBL_Error.Text = "Device name is required"; return; }
          

            //==========  Declaring Objects ==========//
            CPSyncDeviceData ObjSyncDevice = new CPSyncDeviceData();

            //========== Declate the dongle data table object ===========//
            DataTable DT_SyncDevice = new DataTable();

            //========== Assign values to the dongle object ===========//
            ObjSyncDevice.CPDongleID = Convert.ToInt16(DDL_Dongle.SelectedValue.Trim()); 
     

           

            //========== check the Sync device id, if not exist then insert new dongle ===========//
            if (LTRL_CPSyncDeviceID.Text.Trim() == "0")
            {

                ObjSyncDevice.CPDongleID = Convert.ToInt16(DDL_Dongle.SelectedValue.Trim());
                ObjSyncDevice.CPSyncDeviceName = TXT_CPSyncDeviceName.Text.Trim();
                ObjSyncDevice.CPSyncDeviceSerialNum = TXT_DeviceSerialNum.Text.Trim();
                ObjSyncDevice.isUsed = Convert.ToBoolean(CHK_IsActive.Checked);

                ObjSyncDevice.Insert();

                //========== Show the new dongle insert message ===========//
                LBL_Error.Text = "Sync device added successfully.";
            }

            //========== check the Sync device id, if exist then update existing dongle ===========//
            if (LTRL_CPSyncDeviceID.Text.Trim() != "0")
            {
                ObjSyncDevice.CPSyncDeviceID = Convert.ToInt16(LTRL_CPSyncDeviceID.Text.Trim());
                ObjSyncDevice.CPSyncDeviceName = TXT_CPSyncDeviceName.Text.Trim();
                ObjSyncDevice.CPSyncDeviceSerialNum = TXT_DeviceSerialNum.Text.Trim();
                ObjSyncDevice.isUsed = Convert.ToBoolean(CHK_IsActive.Checked);

                ObjSyncDevice.Update();

                //========== Show the dongle update message ===========//
                LBL_Error.Text = "Sync device updated successfully.";
            }

            //=========== Default Settings ==========//
            BTN_Cancel_Click(null, null); Fuji.Common.Sessions.SetSessions("SyncDeviceData", null);

            //========== Set True Flag with the local variable ===========//
            BlnViewDevicePage = true;

            //========== Bind the device data ===========//
            BindSyncDeviceManagement();
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void BTN_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            BTN_AddSave.Text = Resources.UserManagement.BTN_AddSave_Add;
            LTRL_CPSyncDeviceID.Text = "0";
            DDL_Company.SelectedIndex = 0;
            DDL_Branch.SelectedIndex = 0;
            DDL_Dongle.SelectedIndex = 0;
            TXT_CPSyncDeviceName.Text = "";
            BTN_AddDevice.Text = "Add Sync Device";
            TBLFORM.Visible = false;
            TBLGRID.Visible = true; BTN_AddDevice.Visible = true;

            DDL_Company.Enabled = true;
            DDL_Branch.Enabled = true;
            DDL_Dongle.Enabled = true;
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    //protected void BTN_Save_Device_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BTN_AddSave_Click(BTN_AddSave, null);
    //        if (BlnViewDevicePage)
    //        {
    //            //========== Redirect to the DeviceManagement Page ===========//
    //            Response.Redirect("DeviceManagement.aspx", false);
    //        }
    //    }
    //    catch (Exception Exc)
    //    { LBL_Error.Text = HandleException(Exc); }
    //}

    protected void RG_SyncDevice_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        try
        {
            //========== Assign new page index to current page indes ===========//
            RG_SyncDevice.CurrentPageIndex = e.NewPageIndex;

            //========== Bind the dongle data ===========//
            BindSyncDeviceManagement();
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_SyncDevice_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========== Check Session For the DongleData ===========//
            if (Fuji.Common.Sessions.GetSession("SyncDeviceData") != null)
            {
                //========== Get the session Data From the Session ===========//
                DataTable dtDongleList = (DataTable)Fuji.Common.Sessions.GetSession("SyncDeviceData");
                RG_SyncDevice.DataSource = Fuji.Common.Sessions.GetSession("SyncDeviceData");
            }
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_SyncDevice_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== If Command Name is Edit then comes to this end of code ===========//
            if (e.CommandName == "EditSyncDevice")
            {
                LTRL_CPSyncDeviceID.Text = e.Item.Cells[2].Text.Trim();
                DDL_Company.SelectedValue = e.Item.Cells[4].Text.Trim();
                DDL_Company_SelectedIndexChanged(null, null);
                DDL_Branch.SelectedValue = e.Item.Cells[3].Text.Trim();
                DDL_Dongle.SelectedValue = e.Item.Cells[8].Text.Trim();
                TXT_CPSyncDeviceName.Text = e.Item.Cells[9].Text.Trim();
                CHK_IsActive.Checked = Convert.ToBoolean(e.Item.Cells[10].Text.Trim());
                BTN_AddSave.Text = "Save"; TBLFORM.Visible = true;
                LTRL_SubTitle.Text = "Edit Sync Device";
                TBLGRID.Visible = false; BTN_AddDevice.Visible = false;
                LTRL_DeviceSerialNum.Text = "Device Serial Number"; 
                

                DDL_Company.Enabled = false;
                DDL_Branch.Enabled = false;
                DDL_Dongle.Enabled = false;
            }

            //========== If Command Name is Delete then comes to this end of code ===========//
            if (e.CommandName == "Delete")
            {
                //==========  Declaring Objects ==========//
                CPDongleData objDongle = new CPDongleData();

                //========== Assign value to company object ===========//
                objDongle.CPDongleID = Convert.ToInt32(e.Item.Cells[2].Text.Trim());
                objDongle.spDelete();

                //========== Show successful deletion message ===========//
                LBL_Error.Text = "Device deleted successfully.";

                //========== Fill Company DAta to session object of company ===========//
                Fuji.Common.Sessions.SetSessions("DongleData", null);

                //========== Clear all filtering options , if any ===========//
                BTN_Reset_Click(null, null);

                //========== Bind the Dongle Data ===========//
                BindSyncDeviceManagement();
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

    protected void RG_SyncDevice_SortCommand(object source, GridSortCommandEventArgs e)
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

    protected void RG_SyncDevice_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check The Item is item or alternating item then comes to this end of code ===========//
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");
            }
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
                IntFirstIndexInPage = RG_SyncDevice.MasterTableView.PagingManager.FirstIndexInPage;
                IntLastIndexInPage = RG_SyncDevice.MasterTableView.PagingManager.LastIndexInPage + 1;
                IntDataSourceCount = RG_SyncDevice.MasterTableView.PagingManager.DataSourceCount;

                if (IntDataSourceCount > 0)
                {
                    IntFirstIndexInPage++;
                }

                LTRL_RecordText.Text = "Record(s) : " + IntFirstIndexInPage + " to " + IntLastIndexInPage + " of " + IntDataSourceCount + " | ";

                if (RG_SyncDevice.MasterTableView.PagingManager.IsFirstPage)
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
                if (RG_SyncDevice.MasterTableView.PagingManager.IsLastPage == true)
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
            RG_SyncDevice.PageSize = Convert.ToInt32(StrSelectedValue);
            RG_SyncDevice.CurrentPageIndex = 0;
            //========== Check for branch data in session and assign if exist ===========//
            if (Fuji.Common.Sessions.GetSession("DongleData") != null)
            {
                //========== Assign the branch data fro the session value ===========//
                RG_SyncDevice.DataSource = Fuji.Common.Sessions.GetSession("DongleData");
            }

            //========== Bind the grid ===========//
            RG_SyncDevice.DataBind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    private void BTN_Reset_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Check grid column in the grid ===========//
            foreach (Telerik.WebControls.GridColumn column in RG_SyncDevice.MasterTableView.Columns)
            {
                //========== set all column filter value to empty ===========//
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }
            Fuji.Common.Sessions.SetSessions("StartDate", null);
            //========== set all filter expression to empty ===========//
            RG_SyncDevice.MasterTableView.FilterExpression = String.Empty;
            RG_SyncDevice.MasterTableView.Rebind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void RG_SyncDevice_ItemCreated(object sender, GridItemEventArgs e)
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
            LTRL_Title.Text = "Sync Device Management";
            LTRL_SubTitle.Text = "Add Sync Device";
            LTRL_CompanyName.Text = "Company Name";
            LTRL_BranchName.Text = "Branch Name";
            LTRL_DongleSerialNo.Text = "Dongle Serial No.";
            LTRL_DeviceName.Text = "Device Name";
            LTRL_Active.Text = "Is Used";

            BTN_AddDevice.Text = "Add Sync Device";
            BTN_AddSave.Text = "Add";
            BTN_Cancel.Text = "Cancel";
           
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Bind Sync Device Management
    /// <summary>
    /// Used to bind the Sync Device
    /// </summary>
    private void BindSyncDeviceManagement()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPSyncDeviceData ObjCPSyncDevice = new CPSyncDeviceData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_SyncDevice = new DataTable();

            if (Fuji.Common.Sessions.GetSession("SyncDeviceData") == null)
            {
                DT_SyncDevice = ObjCPSyncDevice.uspGetSyncDevice();
                Fuji.Common.Sessions.SetSessions("SyncDeviceData", DT_SyncDevice);
            }

            //========== Get the session Data From the Session ===========//
            DT_SyncDevice = (DataTable)Fuji.Common.Sessions.GetSession("SyncDeviceData");

            //========== Assign the session data to Grid ===========//
            if (DT_SyncDevice != null && DT_SyncDevice.Rows.Count > 0)
            { RG_SyncDevice.DataSource = DT_SyncDevice; RG_SyncDevice.DataBind(); }
            else
            { RG_SyncDevice.DataSource = null; RG_SyncDevice.DataBind(); }

            //========== Show the pager style if page count is greater than 1 ===========//
            if (RG_SyncDevice.Items.Count == 0)
            { RG_SyncDevice.PagerStyle.Visible = false; RG_SyncDevice.MasterTableView.AllowFilteringByColumn = false; RG_SyncDevice.AllowSorting = false; }
            else
            { RG_SyncDevice.PagerStyle.Visible = true; RG_SyncDevice.MasterTableView.AllowFilteringByColumn = true; RG_SyncDevice.AllowSorting = true; }

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

            //get distinct rows from datatable or datarow
            //http://blog.wolffmyren.com/2009/02/24/c-corner-how-to-get-distinct-rows-from-a-dataset-or-datatable/         

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



    #region Check User Rights
    /// <summary>
    /// Used to check user rights for the dongle management
    /// </summary>
    public void CheckRights()
    {
        try
        {
            //========== Check Whether the logged in user has the permission to Add New Dongle, if not then hide Add new Dongle button ===========//
            if (Fuji.Common.Sessions.Add_Dongle == true)
                BTN_AddDevice.Visible = true;
            else BTN_AddDevice.Visible = false;

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
