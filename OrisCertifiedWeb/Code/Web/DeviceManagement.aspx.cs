#region Using System
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

#region Using Application
using Telerik.WebControls;
using Fuji.Common;
using Fuji.Business;
#endregion

public partial class DeviceManagement : PageBase
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
                //========== Clear the session on first load ===========//
                Fuji.Common.Sessions.SetSessions("DeviceData", null);

                //=========== Check Login ==========//
                Sessions.CheckUserLogin();

                //========== Check User Role and redirect to home page as per user role ===========//
                Fuji.Common.CommonFunctions.CheckUserRole();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Bind DongleList ==========//                
                BindDeviceManagement();

                //========== Fill CompanyList in DropDownList ===========//
                FillCompany();

                //========== Fill BranchList by Userwise in DropDownList ===========//
                FillBranch(0);

                //========== Fill DongleList by Userwise in DropDownList ===========//
                FillDongle(0);

                //========== Fill MeasurementType & DeviceType in DropDownList ===========//
                FillMeasurementType();
                FillDeviceType();

                //=========== Set Default Setting in Grid ==========//
                //Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_MasterCompany);
                RG_Device.PageSize = ConstDefaultPageSize;

                //=========== Clear Form ==========//
                BTN_Cancel_Click(null, null);

                //========== Check User rights  for the device management===========//
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
            { FillBranch(0); }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void DDL_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
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
            LTRL_SubTitle.Text = Resources.DeviceManagement.LBL_SubTitle_Text_Add;
        }
        catch (Exception Ex)
        {
            LBL_Error.Text = HandleException(Ex);
        }
    }

    protected void BTN_AddSave_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validations For the new Device inputs ==========//
            if (DDL_Company.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.CompanyNameIsRequired; return; }
            if (DDL_Branch.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.BranchNameIsRequired; return; }
            if (DDL_Dongle.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.DongleNameIsRequired; return; }
            if (TXT_DeviceDescription.Text.Trim() == "") { LBL_Error.Text = Resources.Message.DeviceDescriptionIsRequired; return; }
            if (DDL_MeasurementType.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.MeasurementTypeIsRequired; return; }
            if (DDL_DeviceType.SelectedIndex == 0) { LBL_Error.Text = Resources.Message.DeviceTypeIsRequired; return; }

            //==========  Declaring Objects ==========//
            CPDeviceData objDevice = new CPDeviceData();

            //========== check the device id, if not exist then insert new dongle ===========//
            if (LTRL_DeviceID.Text.Trim() == "0")
            {
                objDevice.CPDongleID = Convert.ToInt32(DDL_Dongle.SelectedValue.Trim());
                objDevice.CPDeviceDesc = TXT_DeviceDescription.Text.Trim();
                objDevice.MeasurementTypeID = Convert.ToInt16(DDL_MeasurementType.SelectedValue.Trim());
                objDevice.CPDeviceTypeID = Convert.ToInt16(DDL_DeviceType.SelectedValue.Trim());
                objDevice.LastMeasurementID = 0;
                objDevice.uspInsertDevice();

                //========== Show the new device insert message ===========//
                LBL_Error.Text = "Device added successfully.";
            }

            //========== check the dongle id, if exist then update existing dongle ===========//
            if (LTRL_DeviceID.Text.Trim() != "0")
            {
                objDevice.CPDeviceID = Convert.ToInt32(LTRL_DeviceID.Text.Trim());
                objDevice.CPDongleID = Convert.ToInt32(DDL_Dongle.SelectedValue.Trim());
                objDevice.CPDeviceDesc = TXT_DeviceDescription.Text.Trim();
                objDevice.MeasurementTypeID = Convert.ToInt16(DDL_MeasurementType.SelectedValue.Trim());
                objDevice.CPDeviceTypeID = Convert.ToInt16(DDL_DeviceType.SelectedValue.Trim());
                objDevice.LastMeasurementID = 0;
                objDevice.uspUpdateDevice();

                //========== Show the device update message ===========//
                LBL_Error.Text = "Device updated successfully.";
            }

            //=========== Default Settings ==========//
            BTN_Cancel_Click(null, null); Fuji.Common.Sessions.SetSessions("DeviceData", null);

            //========== Bind the Device data ===========//
            BindDeviceManagement();
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void BTN_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            BTN_AddSave.Text = Resources.UserManagement.BTN_AddSave_Add;
            LTRL_DeviceID.Text = "0";
            DDL_Company.SelectedIndex = 0;
            DDL_Branch.SelectedIndex = 0;
            DDL_Dongle.SelectedIndex = 0;
            TXT_DeviceDescription.Text = "";
            DDL_MeasurementType.SelectedIndex = 0;
            DDL_DeviceType.SelectedIndex = 0;
            BTN_AddDevice.Text = Resources.DeviceManagement.BTN_AddDevice_Text;
            TBLFORM.Visible = false;
            TBLGRID.Visible = true; BTN_AddDevice.Visible = true;
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_Device_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        try
        {
            //========== Assign new page index to current page indes ===========//
            RG_Device.CurrentPageIndex = e.NewPageIndex;

            //========== Bind the device data ===========//
            BindDeviceManagement();
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_Device_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        try
        {
            //========== Check Session For the DongleData ===========//
            if (Fuji.Common.Sessions.GetSession("DeviceData") != null)
            {
                //========== Get the session Data From the Session ===========//
                RG_Device.DataSource = Fuji.Common.Sessions.GetSession("DeviceData");
            }
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_Device_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            //========== If Command Name is Edit then comes to this end of code ===========//
            if (e.CommandName == "EditDevice")
            {
                //========== Assign the values of company Dropdownlist ===========//
                LTRL_DeviceID.Text = e.Item.Cells[2].Text.Trim();
                DDL_Company.SelectedValue = e.Item.Cells[3].Text.Trim();

                //========== Call the method to bind branch list on selecting company dropdownlist ===========//
                DDL_Company_SelectedIndexChanged(null, null);
                DDL_Branch.SelectedValue = e.Item.Cells[4].Text.Trim();

                //========== Call the method to bind dongle list on selecting branch dropdownlist ===========//
                DDL_Branch_SelectedIndexChanged(null, null);
                DDL_Dongle.SelectedValue = e.Item.Cells[5].Text.Trim();
                TXT_DeviceDescription.Text = e.Item.Cells[9].Text.Trim();

                //========== Check Whether MeasurementType Value is blank or not, if so then not to select MeasurementType value in dropdownlist ===========//
                if (Server.HtmlDecode(e.Item.Cells[12].Text).ToString().Trim() == "")
                { }
                else DDL_MeasurementType.SelectedValue = e.Item.Cells[12].Text.Trim();

                //========== Check Whether DDL_DeviceType Value is blank or not, if so then not to select DDL_DeviceType value in dropdownlist ===========//
                if (Server.HtmlDecode(e.Item.Cells[13].Text).ToString().Trim() == "")
                { }
                else DDL_DeviceType.SelectedValue = e.Item.Cells[13].Text.Trim();
                //========== Show/Hide the buttons ===========//
                BTN_AddSave.Text = Resources.DeviceManagement.BTN_AddSave_Save; TBLFORM.Visible = true;
                LTRL_SubTitle.Text = Resources.DeviceManagement.LBL_SubTitle_Text_Edit;
                TBLGRID.Visible = false; BTN_AddDevice.Visible = false;
            }

            //========== If Command Name is Delete then comes to this end of code ===========//
            if (e.CommandName == "Delete")
            {
                //==========  Declaring Objects ==========//
                CPDeviceData objDevice = new CPDeviceData();

                //========== Assign value to company object ===========//
                objDevice.CPDeviceID = Convert.ToInt32(e.Item.Cells[2].Text.Trim());
                objDevice.uspDeleteDevice();

                //========== Show successful deletion message ===========//
                LBL_Error.Text = "Device deleted successfully.";

                //========== Fill Company DAta to session object of Device ===========//
                Fuji.Common.Sessions.SetSessions("DeviceData", null);

                //========== Clear all filtering options , if any ===========//
                BTN_Reset_Click(null, null);

                //========== Bind the device Data ===========//
                BindDeviceManagement();
            }
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    protected void RG_Device_SortCommand(object source, GridSortCommandEventArgs e)
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
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_Device_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check The Item is item or alternating item then comes to this end of code ===========//
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            {
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");
            }

            //========== Check for the Pager Style Functions ===========//
            #region Pager Style
            if (e.Item.ItemType == GridItemType.Pager)
            {
                //========== Find the label and link button inthe pager ===========//
                Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");
                ImageButton IMGBTN_First = (ImageButton)e.Item.FindControl("IMGBTN_First");
                ImageButton IMGBTN_Prev = (ImageButton)e.Item.FindControl("IMGBTN_Prev");
                ImageButton IMGBTN_Next = (ImageButton)e.Item.FindControl("IMGBTN_Next");
                ImageButton IMGBTN_Last = (ImageButton)e.Item.FindControl("IMGBTN_Last");

                //========== Declare the variable fot the pages and assign to initial value ===========//
                int IntFirstIndexInPage = 0, IntLastIndexInPage = 0, IntDataSourceCount = 0;

                //========== Assign Grid value to Variable ===========//
                IntFirstIndexInPage = RG_Device.MasterTableView.PagingManager.FirstIndexInPage;
                IntLastIndexInPage = RG_Device.MasterTableView.PagingManager.LastIndexInPage + 1;
                IntDataSourceCount = RG_Device.MasterTableView.PagingManager.DataSourceCount;

                if (IntDataSourceCount > 0)
                { IntFirstIndexInPage++; }

                LTRL_RecordText.Text = "Record(s) : " + IntFirstIndexInPage + " to " + IntLastIndexInPage + " of " + IntDataSourceCount + " | ";



                if (RG_Device.MasterTableView.PagingManager.IsFirstPage)
                {
                    IMGBTN_First.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    IMGBTN_Prev.ImageUrl = "Images/Nav_PreviousPage_off.gif";
                    IMGBTN_First.Style.Add("cursor", "");
                    IMGBTN_Prev.Style.Add("cursor", "");
                    IMGBTN_First.Attributes.Add("disabled", "true");
                    IMGBTN_Prev.Attributes.Add("disabled", "true");
                }
                else
                {
                    IMGBTN_First.ImageUrl = "Images/Nav_FirstPage.gif";
                    IMGBTN_Prev.ImageUrl = "Images/Nav_PreviousPage.gif";
                    IMGBTN_First.Style.Add("cursor", "pointer");
                    IMGBTN_Prev.Style.Add("cursor", "pointer");
                }
                if (RG_Device.MasterTableView.PagingManager.IsLastPage == true)
                {
                    IMGBTN_Next.ImageUrl = "Images/Nav_NextPage_off.gif";
                    IMGBTN_Last.ImageUrl = "Images/Nav_LastPage_off.gif";
                    IMGBTN_Next.Style.Add("cursor", "");
                    IMGBTN_Last.Style.Add("cursor", "");
                    IMGBTN_Next.Attributes.Add("disabled", "true");
                    IMGBTN_Last.Attributes.Add("disabled", "true");
                }
                else
                {
                    IMGBTN_Next.ImageUrl = "Images/Nav_NextPage.gif";
                    IMGBTN_Last.ImageUrl = "Images/Nav_LastPage.gif";
                    IMGBTN_Next.Style.Add("cursor", "pointer");
                    IMGBTN_Last.Style.Add("cursor", "pointer");
                }
            }
            #endregion
        }
        catch (Exception exc)
        {
            LBL_Error.Text = HandleException(exc);
        }
    }

    protected void RG_Device_ItemCreated(object sender, GridItemEventArgs e)
    {
        try
        {
            //========== Check and Create Events for the Pager Selection  ===========//
            if (e.Item.ItemType == GridItemType.Pager)
            {
                DropDownList DDL_RecordPage = (DropDownList)e.Item.FindControl("DDL_RecordPage");

                if (DDL_RecordPage != null)
                {
                    //========== Find the Record page dropdownlist ===========//
                    DDL_RecordPage.SelectedIndexChanged += new EventHandler(DDL_RecordPage_SelectedIndexChanged);

                    if (!IsPostBack)
                    {
                        //========== Declare its event ===========//
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
                BTN_Reset.Text = Resources.DeviceManagement.BTN_Reset_Text;
                if (BTN_Reset != null)
                {
                    //========== Declate the reset button event ===========//
                    BTN_Reset.Click += new EventHandler(BTN_Reset_Click);
                }
            }
        }
        catch (Exception Ex)
        {
            LBL_Error.Text = HandleException(Ex);
        }
    }

    private void DDL_RecordPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========== Declare the variable for the selection value of the record page dropdownlist ===========//
            string StrSelectedValue = ((DropDownList)sender).SelectedItem.Value;
            ViewState["RecordPerPage"] = StrSelectedValue;

            //========== Set the paze size ===========//
            RG_Device.PageSize = Convert.ToInt32(StrSelectedValue);
            RG_Device.CurrentPageIndex = 0;
            //========== Check for device data in session and assign if exist ===========//
            if (Fuji.Common.Sessions.GetSession("DeviceData") != null)
            {
                //========== Assign the branch data fro the session value ===========//
                RG_Device.DataSource = Fuji.Common.Sessions.GetSession("DeviceData");
            }

            //========== Bind the grid ===========//
            RG_Device.DataBind();
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }

    private void BTN_Reset_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Check grid column in the grid ===========//
            foreach (Telerik.WebControls.GridColumn column in RG_Device.MasterTableView.Columns)
            {
                //========== set all column filter value to empty ===========//
                column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                column.CurrentFilterValue = String.Empty;
            }

            //========== set all filter expression to empty ===========//
            RG_Device.MasterTableView.FilterExpression = String.Empty;
            RG_Device.MasterTableView.Rebind();
        }
        catch (Exception Exc)
        { LBL_Error.Text = HandleException(Exc); }
    }
    #endregion

    #region Methods and Events

    #region Set Labels
    /// <summary>
    /// Used to set labels of the form
    /// </summary>
    private void SetLabels()
    {
        try
        {
            //========== Set Culture ==========//
            Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
            Resources.DeviceManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.DeviceManagement.LBL_Title_Text;
            LTRL_SubTitle.Text = Resources.DeviceManagement.LBL_SubTitle_Text_Add;
            LTRL_CompanyName.Text = Resources.DeviceManagement.LBL_CompanyName_Text;
            LTRL_BranchName.Text = Resources.DeviceManagement.LBL_BranchName_Text;
            LTRL_DongleName.Text = Resources.DeviceManagement.LBL_DongleName_Text;
            LTRL_DeviceDescription.Text = Resources.DeviceManagement.LBL_DeviceDescription_Text;
            LTRL_MeasurementType.Text = Resources.DeviceManagement.LBL_MeasurementType_Text;
            LTRL_DeviceType.Text = Resources.DeviceManagement.LBL_DeviceType_Text;

            //========== Set Button Text ==========//
            BTN_AddSave.Text = Resources.DeviceManagement.BTN_AddSave_Add;
            BTN_Cancel.Text = Resources.DeviceManagement.BTN_Cancel_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Bind Device Management Data
    /// <summary>
    /// Used to bind the device data
    /// </summary>
    private void BindDeviceManagement()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CPDeviceData ObjDevice = new CPDeviceData();

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Device = new DataTable();
            if (Fuji.Common.Sessions.GetSession("DeviceData") == null)
            {
                DT_Device = ObjDevice.uspGetAllDeviceByUserID(Fuji.Common.Sessions.UserID, Fuji.Common.Sessions.RoleID);
                Fuji.Common.Sessions.SetSessions("DeviceData", DT_Device);
            }

            //========== Get the session Data From the Session ===========//
            DT_Device = (DataTable)Fuji.Common.Sessions.GetSession("DeviceData");

            //========== Assign the session data to Grid ===========//
            if (DT_Device != null && DT_Device.Rows.Count > 0)
            { RG_Device.DataSource = DT_Device; RG_Device.DataBind(); }
            else
            { RG_Device.DataSource = null; RG_Device.DataBind(); }

            //========== Show the pager style if page count is greater than 1 ===========//
            if (RG_Device.Items.Count == 0)
            { RG_Device.MasterTableView.PagerStyle.Visible = false; RG_Device.MasterTableView.AllowFilteringByColumn = false; RG_Device.AllowSorting = false; }
            else
            { RG_Device.MasterTableView.PagerStyle.Visible = true; RG_Device.MasterTableView.AllowFilteringByColumn = true; RG_Device.AllowSorting = true; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region FillCompany
    /// <summary>
    /// Used to bind the company data based on the userid and its role
    /// </summary>
    private void FillCompany()
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_Company.Items.Clear();

            //========== Declare the Object of the Company ===========//
            UserCompanyData objUserCompany = new UserCompanyData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Company = new DataTable();
            objUserCompany.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserCompany.CompanyID = Fuji.Common.Sessions.RoleID;
            DT_Company = objUserCompany.uspGetCompanyByUserID();
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
    /// <param name="_CompanyID">Company id of the company </param>
    private void FillBranch(int _CompanyID)
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_Branch.Items.Clear();

            //========== Declare the Object of the Branch ===========//
            UserBranchData objUserBranch = new UserBranchData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Branch = new DataTable();
            objUserBranch.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserBranch.CompanyID = _CompanyID;
            DT_Branch = objUserBranch.uspGetBranchOfCompanyByUserID(Fuji.Common.Sessions.RoleID);
            if (DT_Branch != null && DT_Branch.Rows.Count > 0)
            {
                DDL_Branch.DataSource = DT_Branch;
                DDL_Branch.DataValueField = "BranchID"; DDL_Branch.DataTextField = "BranchName";
                DDL_Branch.DataBind();
            }
            else
            { DDL_Branch.DataSource = null; DDL_Branch.DataBind(); }

            //========== Insert Items DropDownList in Branch ===========//
            DDL_Branch.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region FillDongle
    /// <summary>
    /// Used to bind the dongle based on the branch
    /// </summary>
    /// <param name="_BranchID">Branch id of the branch</param>
    private void FillDongle(int _BranchID)
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_Dongle.Items.Clear();

            //========== Declare the Object of the Dongle ===========//
            UserDongleData objUserDongle = new UserDongleData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Dongle = new DataTable();
            objUserDongle.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserDongle.UserDongleID = _BranchID;
            DT_Dongle = objUserDongle.uspGetDongleByUserIDAndBranchID(Fuji.Common.Sessions.RoleID);
            if (DT_Dongle != null && DT_Dongle.Rows.Count > 0)
            {
                DDL_Dongle.DataSource = DT_Dongle;
                DDL_Dongle.DataValueField = "CPDongleID"; DDL_Dongle.DataTextField = "DongleSerial";//CPSerialNUM
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

    #region FillMeasurementType
    /// <summary>
    /// Used to fill the mesurement type
    /// </summary>
    private void FillMeasurementType()
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_MeasurementType.Items.Clear();

            //========== Declare the Object of the MeasurementType ===========//
            CPMeasurementTypeData objMeasurementType = new CPMeasurementTypeData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_MeasurementType = new DataTable();
            DT_MeasurementType = objMeasurementType.uspGetAllMeasurementType();
            if (DT_MeasurementType != null && DT_MeasurementType.Rows.Count > 0)
            {
                DDL_MeasurementType.DataSource = DT_MeasurementType;
                DDL_MeasurementType.DataValueField = "CPMeasurementTypeID"; DDL_MeasurementType.DataTextField = "CPMeasurementType";
                DDL_MeasurementType.DataBind();
            }
            else
            { DDL_MeasurementType.DataSource = null; DDL_MeasurementType.DataBind(); }

            //========== Insert Items DropDownList in MeasurementType ===========//
            DDL_MeasurementType.Items.Insert(0, new ListItem("--- SELECT MEASUREMENT TYPE ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region FillDeviceType
    /// <summary>
    /// Used to bind the device type
    /// </summary>
    private void FillDeviceType()
    {
        try
        {
            //========== Clear Items in DropDownList ===========//
            DDL_DeviceType.Items.Clear();

            //========== Declare the Object of the DeviceType ===========//
            CPDeviceTypeData objDeviceType = new CPDeviceTypeData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_DeviceType = new DataTable();
            DT_DeviceType = objDeviceType.uspGetAllDeviceType();
            if (DT_DeviceType != null && DT_DeviceType.Rows.Count > 0)
            {
                DDL_DeviceType.DataSource = DT_DeviceType;
                DDL_DeviceType.DataValueField = "CPDeviceTypeID"; DDL_DeviceType.DataTextField = "CPDeviceTypeDesc";
                DDL_DeviceType.DataBind();
            }
            else
            { DDL_DeviceType.DataSource = null; DDL_DeviceType.DataBind(); }

            //========== Insert Items DropDownList in DeviceType ===========//
            DDL_DeviceType.Items.Insert(0, new ListItem("--- Select Device TYPE ---", "0"));
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Check User Rights
    /// <summary>
    /// Used to check user rights for the device management
    /// </summary>
    public void CheckRights()
    {
        try
        {
            //========== Check Whether the logged in user has the permission to Add New Device, if not then hide Add new Devicebutton ===========//
            if (Fuji.Common.Sessions.Add_Device == true)
                BTN_AddDevice.Visible = true;
            else BTN_AddDevice.Visible = false;

            //========== Check Whether the logged in user has the permission to Edit Device, if not then hide Edit Column from the Device list ===========//
            if (Fuji.Common.Sessions.Update_Device == true)
                RG_Device.Columns[12].Visible = true;
            else RG_Device.Columns[12].Visible = false;

            //========== Check Whether the logged in user has the permission to Delete Device, if not then hide Delete Column from the Device list ===========//
            if (System.Web.HttpContext.Current.Session["Username"].ToString().ToUpper() == "MWINN" || System.Web.HttpContext.Current.Session["Username"].ToString().ToUpper() == "ORISWEBADMIN")
                RG_Device.Columns[13].Visible = true;
            else 
            RG_Device.Columns[13].Visible = false;

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion
}
