#region System
using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
using Telerik.WebControls;
#endregion

namespace TaskeroWeb
{
    public partial class UserManagement : PageBase
    {
        #region Declarations
        private const int _ConstDefaultPageSize = 25;
        #endregion

        #region PAGE LOAD AND EVENTS
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //=========== Clear Error ==========//
                LblError.Text = "";

                //=========== Check Login ==========//
                Sessions.CheckUserLogin();

                //=========== Check ==========//
                TXT_AllowedIP1.Attributes.Add("onkeyup", "FocusNext(this.id , '" + TXT_AllowedIP2.ClientID + "' )");
                TXT_AllowedIP2.Attributes.Add("onkeyup", "FocusNext(this.id , '" + TXT_AllowedIP3.ClientID + "' )");
                TXT_AllowedIP3.Attributes.Add("onkeyup", "FocusNext(this.id , '" + TXT_AllowedIP4.ClientID + "' )");

                //========== Check User Role and redirect to home page as per user role ===========//
                CommonFunctions.CheckUserRole();

                //=========== Bind the show button with script which show and hide the password fields ==========//
                //BTN_DisplayPasswordField.Attributes.Add("OnClick", "return display();");

                //=========== Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========= Clear the date for first time =========//
                    Sessions.SetSessions("StartDate", null);
                    Sessions.SetSessions("UserMansgementDongle_Data", null);

                    FIL_Username.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";
                    FIL_Password.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";
                    FIL_ConformPassword.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";

                    TRMainTable.Visible = false;
                    TRGridTable.Visible = true;
                    BTN_AddUser.Visible = true;

                    //========== Fill Variables to Form ==========//
                    SetLabels();

                    //=========== Clear the Session with Datatable of User Data==========//
                    Sessions.SetSessions("TaskerUser_DataTable", null);

                    //=========== Calling the Method for binding the User Grid ==========//
                    FillUserGrid();

                    //=========== Calling the Method for Checking User Rights==========//
                    CheckRights();

                    //=========== Show Reports ==========//
                    FillReports();
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
            finally
            {
                //========= finally set the login password information =========//
                TXT_LoginPass.Attributes.Add("value", TXT_LoginPass.Text);
                TXT_LoginConfirmPassword.Attributes.Add("value", TXT_LoginConfirmPassword.Text);
            }
        }
        protected void BTN_DisplayPasswordField_Click(object sender, EventArgs e)
        {
            try
            {
                //========= Check whether the password input box is password then convert it to textbox =========//
                if (TXT_LoginPass.TextMode == TextBoxMode.Password)
                {
                    TXT_LoginPass.TextMode = TextBoxMode.SingleLine;
                    TXT_LoginConfirmPassword.TextMode = TextBoxMode.SingleLine;
                }
                else if (TXT_LoginPass.TextMode == TextBoxMode.SingleLine)
                {
                    TXT_LoginPass.TextMode = TextBoxMode.Password;
                    TXT_LoginConfirmPassword.TextMode = TextBoxMode.Password;
                }
                //========= Change the button value =========//
                if (BTN_DisplayPasswordField.Text == "Show Password")
                {
                    BTN_DisplayPasswordField.Text = "Hide Password";
                    BTN_DisplayPasswordField.ToolTip = "Click to hide password field values";
                }
                else if (BTN_DisplayPasswordField.Text == "Hide Password")
                {
                    BTN_DisplayPasswordField.Text = "Show Password";
                    BTN_DisplayPasswordField.ToolTip = "Click to show password field values";
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
            finally
            {
                //========= finally set the login password information =========//
                TXT_LoginPass.Attributes.Add("value", TXT_LoginPass.Text);
                TXT_LoginConfirmPassword.Attributes.Add("value", TXT_LoginConfirmPassword.Text);
            }
        }
        protected void BTN_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Calling the Method for clearing the Controls ===========//            
                ClearControls();

                //========== Enabling the Grid ===========//
                BTN_AddUser.Visible = true;
                TRGridTable.Visible = true;
                TRMainTable.Visible = false;
                if (DDL_BrandID.Items.Count > 0)
                { DDL_BrandID.SelectedIndex = 0; }
                Sessions.SetSessions("UserMansgementDongle_Data", null);
                SetToDefault();
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void BTN_AddSave_Click(object sender, EventArgs e)
        {
            try
            {
                //RB_Roles.Checked = true;
                //========== Checking the Lable for making the descision of Add or Update===========//
                if (LTRL_ID.Text.Trim().Equals("0", StringComparison.CurrentCultureIgnoreCase))
                {
                    //========== Calling the Add Method ===========//
                    AddUser();
                }
                else
                {
                    //========== Calling the Update Method ===========//
                    UpdateUser();
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void BTN_AddUser_Click(object sender, EventArgs e)
        {
            try
            {

                //=========== Enabling the Controls and Disabling the Grid ==========//
                RB_Roles.Checked = true;
                BTN_AddUser.Visible = false;
                TRGridTable.Visible = false;
                TRMainTable.Visible = true;
                TXT_LoginID.Enabled = true;
                PNL_Branch.Visible = false;
                PNL_Dongle.Visible = false;
                FLD_PrintBuyerInfo.Visible = false;

                Txt_Customer.Text = "";

                //=========== Clearing the All Controls ==========//
                ClearControls();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Fill DDl Company ==========//
                FillCompany();

                //=========== Fill Roles DropDown ==========//
                FillRoles();

                //=========== Binding the Permission Gridview ==========//
                BindPermissionGridView();
                RE_CHECK();

            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }

        }
        protected void DDL_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //==========  Declaring Objects ==========//
                UserBranchData objBranch = new UserBranchData();

                //==========  Get Data Table and Fill ==========//
                objBranch.TaskeroUserID = Sessions.UserID;
                objBranch.CompanyID = Convert.ToInt32(DDL_Company.SelectedValue);

                CHK_Branch.DataSource = objBranch.uspGetBranchOfCompanyByUserID(Sessions.RoleID);
                CHK_Branch.DataTextField = "BranchName";
                CHK_Branch.DataValueField = "BranchID";
                CHK_Branch.DataBind();
                if (CHK_Branch.Items.Count == 0)
                { PNL_Branch.Height = 0; PNL_Branch.Visible = false; }
                else if (CHK_Branch.Items.Count >= 5)
                { PNL_Branch.Height = 120; PNL_Branch.Visible = true; }
                else if (CHK_Branch.Items.Count <= 2)
                { PNL_Branch.Height = 48; PNL_Branch.Visible = true; }
                else
                { PNL_Branch.Height = 75; PNL_Branch.Visible = true; }
                //==========  Fill Default Values ==========//
                //DDL_Branch.Items.Insert(0, (new ListItem("---Select---", "0")));
                CHK_Branch.SelectedIndex = -1;
                if (Sessions.GetSession("UserMansgementDongle_Data") != null)
                {
                    DataSet DT_DongleData = (DataSet)Sessions.GetSession("UserMansgementDongle_Data");
                    CheckDongleData(DT_DongleData.Tables[2], CHK_Branch);
                }
                CHK_Branch_SelectedIndexChanged(null, null);

                FillBrandList(Convert.ToInt32(DDL_Company.SelectedValue));
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void CHK_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //========== Calling the Method for Filling the Dongle Listbox ===========//
                FillDongleListBox();
                if (!LTRL_ID.Text.Trim().Equals("0", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (Sessions.GetSession("UserMansgementDongle_Data") != null)
                    {
                        DataSet DT_DongleData = (DataSet)Sessions.GetSession("UserMansgementDongle_Data");
                        CheckDongleData(DT_DongleData.Tables[3], CHK_Dongle);
                    }
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            //========== Applying the Datasource to RadGrid at the time of postback or calling any event ===========//
            try
            {
                //if (TRMainTable.Visible == false)
                //{
                if (Sessions.GetSession("TaskerUser_DataTable") != null)
                {
                    DataTable DSet = (DataTable)Sessions.GetSession("TaskerUser_DataTable");
                    if (DSet != null && DSet.Rows.Count > 0)
                    {
                        RG_Users.DataSource = DSet;
                    }
                    else
                    {
                        DataTable Dset = GetUserDate();
                        if (DSet != null && DSet.Rows.Count > 0)
                            RG_Users.DataSource = Dset;
                    }
                }
                else
                {
                    DataTable DSet = GetUserDate();
                    if (DSet != null && DSet.Rows.Count > 0)
                        RG_Users.DataSource = DSet;
                }
                //}
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void RadGrid1_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
        {
            //========== Sorting Grid Event ===========//
            try
            {
                if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
                {
                    GridSortExpression sortExpr = new GridSortExpression();
                    sortExpr.FieldName = e.SortExpression;
                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                //========== Check CommandName for the Edit Event ===========//
                if (e.CommandName == "EditUser")
                {
                    //========== Filling the Controls when Edit Event is called ===========//
                    EditUser(e.Item.Cells[2].Text);
                    TRGridTable.Visible = false;
                    TRMainTable.Visible = true;
                    BTN_AddUser.Visible = false;
                    BTN_AddSave.Text = Resources.UserManagement.BTN_AddSave_Save;
                }
                //========== Check CommandName for the Delete Event ===========//
                if (e.CommandName == "Delete")
                {
                    //========== Object Declaration for the Deletion ===========//
                    TaskeroUserData ObjTaskeroUser = new TaskeroUserData();
                    ObjTaskeroUser.LoginID = e.Item.Cells[2].Text;
                    //========== Calling the Method for Delete the Recored ===========//
                    int Result = ObjTaskeroUser.uspDeleteTaskeroUserAndRelations();
                    if (Result == 2)
                    {
                        LblError.Text = "Taskero user deleted successfully.";
                        FillUserGrid();
                    }
                }
                if (e.CommandName == "SelectDate")
                {
                    TableCell cell = ((Telerik.WebControls.GridFilteringItem)e.Item)["LastModifiedDate"];//PNL_DATE_PICKER
                    Panel Pnl_Date_Picker = (Panel)cell.Controls[0].FindControl("PNL_DATE_PICKER");
                    RadDatePicker RDP_Date = (RadDatePicker)Pnl_Date_Picker.FindControl("RadDatePicker1");

                    Sessions.SetSessions("StartDate", RDP_Date.SelectedDate);

                    e.Item.OwnerTableView.FilterExpression = String.Format("LastModifiedDate > '{0}'", RDP_Date.SelectedDate.ToString());
                    e.Item.OwnerTableView.Rebind();
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
                {
                    //========== Declaring the Check and Casting it from Grid ===========//
                    Literal LTRL_IsAvtive = (Literal)e.Item.FindControl("LTRL_IsAvtive");


                    //========== Enabling the Checkboxes if the IsAdmin status is true ===========//
                    if (e.Item.Cells[3].Text.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                    {
                        LTRL_IsAvtive.Text = "Yes";
                    }
                    else
                    {
                        LTRL_IsAvtive.Text = "No";
                    }

                    //========== Declaring the ImageButton and Casting it from Grid ===========//
                    ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");

                    //========== Applying the Javascript to the ImageButton ===========//                
                    IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");

                }
                //========== Check for the pager style function ===========//
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
                {
                    #region Pager Style

                    Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");
                    int iFirstIndexInPage = 0, iLastIndexInPage = 0, iDataSourceCount = 0;

                    iFirstIndexInPage = RG_Users.MasterTableView.PagingManager.FirstIndexInPage;
                    iLastIndexInPage = RG_Users.MasterTableView.PagingManager.LastIndexInPage + 1;
                    iDataSourceCount = RG_Users.MasterTableView.PagingManager.DataSourceCount;

                    if (iDataSourceCount > 0)
                    {
                        iFirstIndexInPage++;
                    }

                    LTRL_RecordText.Text = "Record(s) : " + iFirstIndexInPage + " to " + iLastIndexInPage + " of " + iDataSourceCount + " | ";

                    ImageButton IBTN_First = (ImageButton)e.Item.FindControl("IBTN_First");
                    ImageButton IBTN_Prev = (ImageButton)e.Item.FindControl("IBTN_Prev");
                    ImageButton IBTN_Next = (ImageButton)e.Item.FindControl("IBTN_Next");
                    ImageButton IBTN_Last = (ImageButton)e.Item.FindControl("IBTN_Last");


                    if (RG_Users.MasterTableView.PagingManager.IsFirstPage)
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
                    if (RG_Users.MasterTableView.PagingManager.IsLastPage == true)
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
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                //========== Check and create events for the pager selection  ===========//
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
                {
                    DropDownList DDL_RecordPage = (DropDownList)e.Item.FindControl("DDL_RecordPage");
                    if (DDL_RecordPage != null)
                    {
                        DDL_RecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPage_SelectedIndexChanged);

                        if (!IsPostBack)
                        {
                            DDL_RecordPage.SelectedValue = _ConstDefaultPageSize.ToString();
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

                    //For Reset Filter Option
                    Button BTN_Reset = (Button)e.Item.FindControl("BTN_Reset");
                    BTN_Reset.Text = Resources.UserManagement.btnReset_Text;
                    if (BTN_Reset != null)
                    {
                        BTN_Reset.Click += new EventHandler(btnReset_Click);
                    }
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }
        private void DDlRecordPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                string sSelectedValue;
                sSelectedValue = ((DropDownList)sender).SelectedItem.Value;
                ViewState["RecordPerPage"] = sSelectedValue;

                RG_Users.PageSize = Convert.ToInt32(sSelectedValue);
                RG_Users.CurrentPageIndex = 0;
                if (Sessions.GetSession("TaskerUser_DataTable") != null)
                {
                    RG_Users.DataSource = Sessions.GetSession("TaskerUser_DataTable");
                }

                RG_Users.DataBind();
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            //========== Event for Clearing all the Filter in the Grid ===========//
            try
            {
                //========== Check grid column in the grid ===========//
                foreach (Telerik.WebControls.GridColumn column in RG_Users.MasterTableView.Columns)
                {
                    //========== set all column filter value to empty ===========//
                    column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                    column.CurrentFilterValue = String.Empty;
                }
                Sessions.SetSessions("StartDate", null);
                //========== set all filter expression to empty ===========//
                RG_Users.MasterTableView.FilterExpression = String.Empty;
                RG_Users.MasterTableView.Rebind();
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }
        protected void RB_Roles_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //****** Enabling the Role DDL and hiding the permission Grid*********//
                if (RB_Roles.Checked)
                {
                    DDL_Roles.Visible = true;
                    CHKL_Permission.Visible = false;
                    PNL_Permission.Visible = false;
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }
        protected void RB_Permission_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //****** Disabling the Role DDL and Unhiding the permission Grid*********//
                if (RB_Permission.Checked)
                {
                    DDL_Roles.Visible = false;
                    CHKL_Permission.Visible = true;
                    PNL_Permission.Visible = true;

                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }

        }
        protected void DDL_Roles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FLD_PrintBuyerInfo.Visible = false;
                if (DDL_Roles.SelectedIndex != 0)
                {
                    if (Convert.ToInt32(DDL_Roles.SelectedValue) == 5)
                    { FLD_PrintBuyerInfo.Visible = true; }
                    else
                    { FLD_PrintBuyerInfo.Visible = false; }
                }
                else
                { FLD_PrintBuyerInfo.Visible = false; }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }
        #endregion

        #region Methods and Events
        //=========== Method for Clearing the Controls ==========//
        private void ClearControls()
        {
            try
            {
                DDL_Company.SelectedIndex = -1;
                CHK_Branch.Items.Clear();
                CHK_Dongle.Items.Clear();
                TXT_LastName.Text = "";
                TXT_FirstName.Text = "";
                TXT_LoginID.Text = "";
                TXT_LoginPass.Text = "";
                TXT_LoginConfirmPassword.Text = "";
                TXT_Email.Text = "";
                DDL_Active.SelectedValue = "1";
                //RB_Roles.Checked = false;
                RB_Permission.Checked = false;
                LTRL_ID.Text = "0";
                TXT_AllowedIP1.Text = "";
                TXT_AllowedIP2.Text = "";
                TXT_AllowedIP3.Text = "";
                TXT_AllowedIP4.Text = "";

                //========= Clear the login password information =========//
                TXT_LoginPass.Attributes.Add("value", "");
                TXT_LoginConfirmPassword.Attributes.Add("value", "");

                FillReports();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #region Set Labels
        /// <summary>
        /// Used to set the labels of the form
        /// </summary>
        private void SetLabels()
        {
            try
            {
                //========== Set Culture ==========//
                Resources.Message.Culture = (new System.Globalization.CultureInfo(Sessions.Language));
                Resources.UserManagement.Culture = (new System.Globalization.CultureInfo(Sessions.Language));

                //========== Set Values ==========//
                LTRL_Title.Text = Resources.UserManagement.LBL_Title_Text;
                LTRL_SubTitle.Text = Resources.UserManagement.LBL_SubTitle_Text;
                LTRL_AccessType.Text = Resources.UserManagement.LBL_AccessType_Text;
                LTRL_CompanyName.Text = Resources.UserManagement.LBL_CompanyName_Text;
                LTRL_BranchName.Text = Resources.UserManagement.LBL_BranchName_Text;
                LTRL_DongleName.Text = Resources.UserManagement.LBL_DongleName_Text;
                LTRL_FirstName.Text = Resources.UserManagement.LBL_FirstName_Text;
                LTRL_LastName.Text = Resources.UserManagement.LBL_LastName_Text;
                LTRL_LoginID.Text = Resources.UserManagement.LBL_LoginID_Text;
                LTRL_LoginPass.Text = Resources.UserManagement.LBL_LoginPass_Text;
                LTRL_LoginConfirmPassword.Text = Resources.UserManagement.LBL_LoginConfirmPassword_Text;
                LTRL_Email.Text = Resources.UserManagement.LBL_Email_Text;
                LTRL_Active.Text = Resources.UserManagement.LBL_Active_Text;
                LTRL_Role.Text = Resources.UserManagement.LBL_Role_Text;
                LTRL_Permission.Text = Resources.UserManagement.LBL_Permission_Text;
                LTRL_PrintBuyerInfo.Text = Resources.UserManagement.LBL_PrintBuyerInfo_Text;
                LTRL_Customer.Text = Resources.UserManagement.LBL_Customer_Text;
                LTRL_BrandInfo.Text = Resources.UserManagement.LBL_BrandInfo_Text;

                BTN_AddUser.Text = Resources.UserManagement.BTN_AddUser_Text;
                BTN_AddSave.Text = Resources.UserManagement.BTN_AddSave_Add;
                BTN_Cancel.Text = Resources.UserManagement.BTN_Cancel_Text;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        //=========== Fill Company DropDown ==========//
        private void FillCompany()
        {
            try
            {
                //==========  Declaring Objects ==========//
                UserCompanyData objCompany = new UserCompanyData();

                //==========  Get Data Table and Fill ==========//
                objCompany.TaskeroUserID = Sessions.UserID;
                objCompany.CompanyID = Sessions.RoleID;
                DataTable DTCompany = objCompany.uspGetCompanyByUserID();
                if (DTCompany != null && DTCompany.Rows.Count > 0)
                {
                    DDL_Company.DataSource = DTCompany;
                    DDL_Company.DataTextField = "CompanyName";
                    DDL_Company.DataValueField = "CompanyID";
                    DDL_Company.DataBind();
                }

                //==========  Fill Default Values ==========//
                DDL_Company.Items.Insert(0, (new ListItem("---Select---", "0")));
                DDL_Company.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Fill Roles DropDown ==========//
        private void FillRoles()
        {
            try
            {
                //==========  Declaring Objects ==========//
                RolesData ObjRoles = new RolesData();
                //==========  Get Data Table and Fill ==========//
                ArrayList _ArrRoles = ObjRoles.uspRetrievePermission();
                if (_ArrRoles != null && _ArrRoles.Count > 0)
                {
                    DDL_Roles.DataTextField = "RoleDesc";
                    DDL_Roles.DataValueField = "RoleID";
                    DDL_Roles.DataSource = _ArrRoles;
                    DDL_Roles.DataBind();

                    //========== Remove item support user from the dropdownlist ==========//
                    if (DDL_Roles.Items.FindByValue("6") != null)
                        DDL_Roles.Items.Remove(DDL_Roles.Items.FindByValue("6"));
                }
                //==========  Fill Default Values ==========//
                DDL_Roles.Items.Insert(0, new ListItem("---Select Role---", "0"));
                DDL_Roles.SelectedIndex = -1;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Fill Dongle ListBox ==========//
        private void FillDongleListBox()
        {
            try
            {
                //========== Clearing the Dongle Listbox ==========//
                CHK_Dongle.Items.Clear();

                //========== Creating XML for DongleID ==========//
                string _StrTemp = "";
                for (int i = 0; i < CHK_Branch.Items.Count; i++)
                {
                    if (CHK_Branch.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<BranchID>" + CHK_Branch.Items[i].Value + "</BranchID>";
                        }
                        else
                        {
                            _StrTemp = "<Branch><BranchID>" + CHK_Branch.Items[i].Value + "</BranchID>";
                        }
                    }
                }
                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Branch>";
                }

                //==========  Declaring Objects ==========//
                UserDongleData ObjDongle = new UserDongleData();
                ObjDongle.DongleDesc = _StrTemp;
                ObjDongle.TaskeroUserID = Sessions.UserID;
                ObjDongle.CPDongleID = Sessions.RoleID;
                //==========  Fetching Data from Table ==========//
                ArrayList _ArrRoles = ObjDongle.uspGetDongleByBranchID();
                if (_ArrRoles != null && _ArrRoles.Count > 0)
                {
                    CHK_Dongle.DataSource = _ArrRoles;
                    CHK_Dongle.DataTextField = "CPSerialNUM";//DongleDescription
                    CHK_Dongle.DataValueField = "CPDongleID";
                    CHK_Dongle.DataBind();
                    if (CHK_Dongle.Items.Count == 0)
                    { PNL_Dongle.Height = 0; PNL_Dongle.Visible = false; }
                    else if (CHK_Dongle.Items.Count >= 5)
                    { PNL_Dongle.Height = 120; PNL_Dongle.Visible = true; }
                    else if (CHK_Dongle.Items.Count <= 2)
                    { PNL_Dongle.Height = 48; PNL_Dongle.Visible = true; }
                    else
                    { PNL_Dongle.Height = 75; PNL_Dongle.Visible = true; }
                }
                else { PNL_Dongle.Height = 0; PNL_Dongle.Visible = false; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Binding the Permission Grid==========//
        private void BindPermissionGridView()
        {
            try
            {
                //==========  Declaring Objects ==========//
                PermissionData ObjRoles = new PermissionData();

                //==========  Fetching Data from Table  ==========//
                DataSet _DSet = ObjRoles.uspGetRetrievePermissionUserManagement();
                if (_DSet != null && _DSet.Tables.Count > 0 && _DSet.Tables[0].Rows.Count > 0)
                {
                    CHKL_Permission.DataSource = _DSet.Tables[0].DefaultView;
                    CHKL_Permission.DataTextField = "PermissionDesc";
                    CHKL_Permission.DataValueField = "PermissionID";
                    CHKL_Permission.DataBind();

                    LTRL_Permission.Visible = true;
                    RB_Permission.Visible = true;

                    if (CHKL_Permission.Items.Count == 0)
                    { PNL_Permission.Height = 0; PNL_Permission.Visible = false; }
                    else if (CHKL_Permission.Items.Count >= 10)
                    { PNL_Permission.Height = 300; PNL_Permission.Visible = true; }
                    else if (CHKL_Permission.Items.Count >= 5)
                    { PNL_Permission.Height = 120; PNL_Permission.Visible = true; }
                    else if (CHKL_Permission.Items.Count <= 2)
                    { PNL_Permission.Height = 48; PNL_Permission.Visible = true; }
                    else
                    { PNL_Permission.Height = 75; PNL_Permission.Visible = true; }
                }
                else
                {
                    LTRL_Permission.Visible = false;
                    RB_Permission.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Fetching the PermissionID for the Allowed Radibuttons & Creating XML for PermissionID ==========//    
        private string RetrievePermissionID()
        {
            try
            {
                //==========  Generating the XML variable for PermissionID ==========//
                string _StrPermissionID = "";
                for (int i = 0; i < CHKL_Permission.Items.Count; i++)
                {
                    if (CHKL_Permission.Items[i].Selected == true)
                    {
                        if (_StrPermissionID.Trim().Length > 0)
                        {
                            _StrPermissionID += "<PermissionID>" + CHKL_Permission.Items[i].Value + "</PermissionID>";
                        }
                        else
                        {
                            _StrPermissionID = "<Permission><PermissionID>" + CHKL_Permission.Items[i].Value + "</PermissionID>";
                        }
                    }
                }
                if (_StrPermissionID.Trim().Length > 0)
                {
                    _StrPermissionID += "</Permission>";
                }
                return _StrPermissionID;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Method for Fetching the UserData ==========//
        private DataTable GetUserDate()
        {
            try
            {
                //==========  Declaring Objects ==========//
                TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

                ObjTaskeroUser.RoleID = Convert.ToInt16(Sessions.RoleID);
                ObjTaskeroUser.TaskeroUserID = Sessions.UserID;

                //==========  Fetching the Data From Taskero User ==========//
                DataTable _DSet = ObjTaskeroUser.uspGetTaskeroUser();

                //==========  Filter the Taskero User Data which is other than support user ==========//
                _DSet.DefaultView.RowFilter = "RoleID <> 6";

                //=========== Setting the Session with Datatable of User Data==========//
                Sessions.SetSessions("TaskerUser_DataTable", _DSet.DefaultView.ToTable());

                return _DSet;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Binding the User Data Grid==========//
        private void FillUserGrid()
        {
            try
            {
                //==========  Declaring Objects & calling the function which is fetching the data from TaskerUser Table ==========//
                DataTable _Dset = GetUserDate();
                if (_Dset != null && _Dset.Rows.Count > 0)
                {
                    RG_Users.Visible = true;
                    RG_Users.DataSource = _Dset;
                    RG_Users.DataBind();
                }
                else
                {
                    RG_Users.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Method Used for For Filling the Controls at the time of Editing Taskero User==========//
        private void EditUser(string Username)
        {
            try
            {
                //==========  Filling the Controls at the time of Editing so calling the Default Add method ==========//
                BTN_AddUser_Click(null, null);

                //==========  Declaring Objects ==========//
                TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

                //==========  Fetching the Data from Table ==========//
                ObjTaskeroUser.LoginID = Username;
                DataSet _Dset = ObjTaskeroUser.uspTaskeroUserDataByUsername();
                if (_Dset != null)
                {
                    Sessions.SetSessions("UserMansgementDongle_Data", _Dset);
                    //==========  Setting the Controls i.e textbox ==========//
                    if (_Dset.Tables[0].Rows.Count > 0)
                    {
                        #region IP Address
                        string _AllowIP = _Dset.Tables[0].Rows[0]["AllowedIP"].ToString();
                        if (_AllowIP != "")
                        {
                            TXT_AllowedIP1.Text = _AllowIP.Split('.')[0];
                            TXT_AllowedIP2.Text = _AllowIP.Split('.')[1];
                            TXT_AllowedIP3.Text = _AllowIP.Split('.')[2];
                            TXT_AllowedIP4.Text = _AllowIP.Split('.')[3];
                        }
                        #endregion

                        TXT_LastName.Text = _Dset.Tables[0].Rows[0]["LastName"].ToString();
                        TXT_FirstName.Text = _Dset.Tables[0].Rows[0]["FirstName"].ToString();
                        TXT_LoginID.Text = _Dset.Tables[0].Rows[0]["LoginID"].ToString();
                        TXT_LoginPass.Attributes.Add("value", _Dset.Tables[0].Rows[0]["LoginPass"].ToString());
                        TXT_LoginConfirmPassword.Attributes.Add("value", _Dset.Tables[0].Rows[0]["LoginPass"].ToString());
                        TXT_LoginID.Enabled = false;
                        TXT_Email.Text = _Dset.Tables[0].Rows[0]["Email"].ToString();
                        if (_Dset.Tables[0].Rows[0]["RoleID"] != null && _Dset.Tables[0].Rows[0]["RoleID"].ToString().Length > 0 && Convert.ToInt32(_Dset.Tables[0].Rows[0]["RoleID"].ToString()) > 0)
                        {
                            RB_Roles.Checked = true;
                            DDL_Roles.SelectedValue = _Dset.Tables[0].Rows[0]["RoleID"].ToString();
                            if (Convert.ToInt32(_Dset.Tables[0].Rows[0]["RoleID"].ToString().Trim()) == 5)
                            {
                                FLD_PrintBuyerInfo.Visible = true;
                                Txt_Customer.Text = _Dset.Tables[0].Rows[0]["Customer"].ToString().Trim();
                            }
                            else { Txt_Customer.Text = ""; FLD_PrintBuyerInfo.Visible = false; }
                        }
                        else if (_Dset.Tables[0].Rows[0]["RoleID"] != null && _Dset.Tables[0].Rows[0]["RoleID"].ToString().Length > 0 && Convert.ToInt32(_Dset.Tables[0].Rows[0]["RoleID"].ToString()) == 0)
                        {
                            #region " === Permission Grid === "
                            //==========  Setting the value of Permission Grid ==========//
                            if (_Dset.Tables[4].Rows.Count > 0)
                            {
                                if (CHKL_Permission.Items.Count > 0)
                                {
                                    RB_Permission.Checked = true;
                                    for (int j = 0; j < CHKL_Permission.Items.Count; j++)
                                    {
                                        for (int i = 0; i < _Dset.Tables[4].Rows.Count; i++)
                                        {
                                            if (CHKL_Permission.Items[j].Value == _Dset.Tables[4].Rows[i][0].ToString())
                                            {
                                                CHKL_Permission.Items[j].Selected = true;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        LTRL_ID.Text = _Dset.Tables[0].Rows[0]["TaskeroUserID"].ToString();

                        if (_Dset.Tables[0].Rows[0]["IsActive"].ToString() == "False")
                            DDL_Active.SelectedValue = "0";
                        else
                            DDL_Active.SelectedValue = "1";

                        if (_Dset.Tables[0].Rows[0]["EmailWhenYellow"].ToString() == "False")
                            DDL_EmailWhenYellow.SelectedValue = "0";
                        else
                            DDL_EmailWhenYellow.SelectedValue = "1";

                        if (_Dset.Tables[0].Rows[0]["EmailWhenRed"].ToString() == "False")
                            DDL_EmailWhenRed.SelectedValue = "0";
                        else
                            DDL_EmailWhenRed.SelectedValue = "1";

                        //only visible to taskero admin
                        if (Sessions.RoleID == 1)
                        {
                            PNL_SiteLicense.Visible = true;
                            if (_Dset.Tables[0].Rows[0]["SiteLicenseAgreement"].ToString() == "False")
                                DDL_LicenseAgreed.SelectedValue = "0";
                            else
                                DDL_LicenseAgreed.SelectedValue = "1";
                        }
                    }

                    //==========  Setting the value of Company Dropdown ==========//
                    if (_Dset.Tables[1].Rows.Count > 0)
                    {
                        if (_Dset.Tables[1].Rows[0]["CompanyID"] != null && _Dset.Tables[1].Rows[0]["CompanyID"].ToString().Trim() != "")
                        {
                            if (DDL_Company.Items.FindByValue(_Dset.Tables[1].Rows[0]["CompanyID"].ToString()) != null)
                            {
                                DDL_Company.SelectedValue = _Dset.Tables[1].Rows[0]["CompanyID"].ToString();
                                DDL_Company_SelectedIndexChanged(null, null);
                            }
                        }
                    }

                    //==========  Setting the value of Branch Listbox ==========//
                    if (_Dset.Tables[2].Rows.Count > 0)
                    {
                        if (CHK_Branch.Items.Count > 0)
                        {
                            for (int i = 0; i < _Dset.Tables[2].Rows.Count; i++)
                            {
                                for (int j = 0; j < CHK_Branch.Items.Count; j++)
                                {
                                    if (CHK_Branch.Items[j].Value == _Dset.Tables[2].Rows[i][0].ToString())
                                    {
                                        CHK_Branch.Items[j].Selected = true;
                                    }
                                }

                            }
                            CHK_Branch_SelectedIndexChanged(null, null);
                        }
                    }
                    //==========  Setting the value of Dongle Listbox ==========//
                    if (_Dset.Tables[3].Rows.Count > 0)
                    {
                        if (CHK_Dongle.Items.Count > 0)
                        {
                            CheckDongleData(_Dset.Tables[3], CHK_Dongle);
                        }
                    }
                    if (_Dset.Tables[0].Rows[0]["RoleID"] != null && _Dset.Tables[0].Rows[0]["RoleID"].ToString().Length > 0 && Convert.ToInt32(_Dset.Tables[0].Rows[0]["RoleID"].ToString()) > 0)
                    {
                        if (Convert.ToInt32(_Dset.Tables[0].Rows[0]["RoleID"].ToString()) == 5)
                        {
                            FLD_PrintBuyerInfo.Visible = true;
                            Txt_Customer.Text = _Dset.Tables[0].Rows[0]["Customer"].ToString();
                        }
                    }
                    RE_CHECK();

                    #region Bind Company Branding
                    //BindDDLBranding(Convert.ToInt32(_Dset.Tables[0].Rows[0]["TaskeroUserID"].ToString()));
                    try
                    {
                        if (DDL_BrandID.Items.FindByValue(_Dset.Tables[0].Rows[0]["BrandID"].ToString()) != null)
                            DDL_BrandID.SelectedValue = _Dset.Tables[0].Rows[0]["BrandID"].ToString();
                    }
                    catch { DDL_BrandID.SelectedIndex = 0; }
                    #endregion

                    //==========  Setting the value of Report Listbox ==========//
                    if (_Dset.Tables[5].Rows.Count > 0)
                    {
                        if (CHK_Reports.Items.Count > 0)
                        {
                            for (int i = 0; i < _Dset.Tables[5].Rows.Count; i++)
                            {
                                for (int j = 0; j < CHK_Reports.Items.Count; j++)
                                {
                                    if (CHK_Reports.Items[j].Value == _Dset.Tables[5].Rows[i][0].ToString())
                                    {
                                        CHK_Reports.Items[j].Selected = true;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //========= Method used for checking of the permission radio button =========//
        private void RE_CHECK()
        {
            try
            {
                if (RB_Permission.Checked == true)
                {
                    PNL_Permission.Visible = true;
                    CHKL_Permission.Visible = true;
                    DDL_Roles.Visible = false;
                    RB_Roles.Checked = false;
                }
                else
                {
                    PNL_Permission.Visible = false;
                    CHKL_Permission.Visible = false;
                    RB_Roles.Checked = true;
                    DDL_Roles.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Method Used for Insertng the Taskero User==========//
        private void AddUser()
        {
            try
            {
                //========== Validations ==========//
                if (DDL_Company.SelectedIndex == 0) { LblError.Text = Resources.Message.CompanyNameIsRequired; return; }
                if (CHK_Branch.SelectedIndex == -1) { LblError.Text = Resources.Message.BranchNameIsRequired; return; }
                if (CHK_Dongle.SelectedIndex == -1) { LblError.Text = Resources.Message.DongleNameIsRequired; return; }
                if (TXT_FirstName.Text == "") { LblError.Text = Resources.Message.FirstNameIsRequired; return; }
                if (TXT_LastName.Text == "") { LblError.Text = Resources.Message.LastNameIsRequired; return; }
                if (TXT_Email.Text == "") { LblError.Text = Resources.Message.EmailIsRequired; return; }
                else if (!Validations.IsEmail(TXT_Email.Text.Trim())) { LblError.Text = Resources.Message.ValidEmailRequired; return; }

                if (TXT_LoginID.Text == "") { LblError.Text = Resources.Message.UsernameIsRequired; return; }
                else if (TXT_LoginID.Text.Trim().Length < 6) { LblError.Text = Resources.Message.PasswordLength; return; }
                if (TXT_LoginPass.Text == "") { LblError.Text = Resources.Message.PasswordIsRequired; return; }

                if (TXT_LoginConfirmPassword.Text == "") { LblError.Text = Resources.Message.ConfirmPasswordIsRequired; return; }
                if (!CompareValidator.Equals(TXT_LoginPass.Text, TXT_LoginConfirmPassword.Text))
                { LblError.Text = "Mismatch Password & Confirm Password."; return; }

                if (CheckIPAddress() == false) { LblError.Text = "Please enter a valid IP address."; return; }
                string _AllowedIP = TXT_AllowedIP1.Text + "." + TXT_AllowedIP2.Text + "." + TXT_AllowedIP3.Text + "." + TXT_AllowedIP4.Text;
                if (_AllowedIP == "...")
                { _AllowedIP = ""; }


                if (RB_Roles.Checked == false && RB_Permission.Checked == false)
                { LblError.Text = Resources.Message.AccessTypeIsRequired; return; }

                if (RB_Roles.Checked)
                {
                    if (Convert.ToInt32(DDL_Roles.SelectedValue) == 0)
                    { LblError.Text = Resources.Message.RoleIsRequired; return; }
                    else
                    {
                        if (Convert.ToInt32(DDL_Roles.SelectedValue) == 5)
                        {
                            if (Txt_Customer.Text.Trim() == "")
                            {
                                LblError.Text = "Customer name is required."; return;
                            }
                        }
                    }
                }


                //========== Declarations ==========//
                TaskeroUserData objTaskeroUser = new TaskeroUserData();

                //========== Check Duplicate and Inserting ==========//
                //objTaskeroUser.TaskeroUserID = Convert.ToInt32(LTRL_ID.Text);            


                //========== Creating XML for Company ID ==========//
                objTaskeroUser.CompanyID = "<Company><CompanyID>" + DDL_Company.SelectedValue + "</CompanyID></Company>";

                //========== Creating XML for BranchID ==========//
                string _StrTemp = "";
                for (int i = 0; i < CHK_Branch.Items.Count; i++)
                {
                    if (CHK_Branch.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<BranchID>" + CHK_Branch.Items[i].Value + "</BranchID>";
                        }
                        else
                        {
                            _StrTemp = "<Branch><BranchID>" + CHK_Branch.Items[i].Value + "</BranchID>";
                        }
                    }
                }

                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Branch>";
                }
                objTaskeroUser.BranchID = _StrTemp;


                //========== Creating XML for DongleID ==========//
                _StrTemp = "";
                for (int i = 0; i < CHK_Dongle.Items.Count; i++)
                {
                    if (CHK_Dongle.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<DongleID>" + CHK_Dongle.Items[i].Value + "</DongleID>";
                        }
                        else
                        {
                            _StrTemp = "<Dongle><DongleID>" + CHK_Dongle.Items[i].Value + "</DongleID>";
                        }
                    }
                }
                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Dongle>";
                }
                objTaskeroUser.DongleID = _StrTemp;

                //========== Creating XML for ReportID ==========//
                _StrTemp = "";
                for (int i = 0; i < CHK_Reports.Items.Count; i++)
                {
                    if (CHK_Reports.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<ReportID>" + CHK_Reports.Items[i].Value + "</ReportID>";
                        }
                        else
                        {
                            _StrTemp = "<Report><ReportID>" + CHK_Reports.Items[i].Value + "</ReportID>";
                        }
                    }
                }

                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Report>";
                }
                objTaskeroUser.ReportID = _StrTemp;

                objTaskeroUser.FirstName = TXT_FirstName.Text.Trim();
                objTaskeroUser.LastName = TXT_LastName.Text.Trim();
                objTaskeroUser.Email = TXT_Email.Text.Trim();
                objTaskeroUser.LoginID = TXT_LoginID.Text.Trim();
                objTaskeroUser.LoginPass = TXT_LoginConfirmPassword.Text.Trim();
                objTaskeroUser.AllowedIP = _AllowedIP;

                if (DDL_Active.SelectedIndex == 0)
                { objTaskeroUser.IsActive = true; }
                else
                { objTaskeroUser.IsActive = false; }

                if (DDL_EmailWhenYellow.SelectedIndex == 0)
                { objTaskeroUser.EmailWhenYellow = true; }
                else
                { objTaskeroUser.EmailWhenYellow = false; }

                if (DDL_EmailWhenRed.SelectedIndex == 0)
                { objTaskeroUser.EmailWhenRed = true; }
                else
                { objTaskeroUser.EmailWhenRed = false; }

                if (DDL_LicenseAgreed.SelectedIndex == 0)
                { objTaskeroUser.SiteLicenseAgreed = true; }
                else
                { objTaskeroUser.SiteLicenseAgreed = false; }

                if (Convert.ToInt32(DDL_Roles.SelectedValue) == 5)
                    objTaskeroUser.Customer = Txt_Customer.Text.Trim();
                else objTaskeroUser.Customer = "";
                objTaskeroUser.BrandID = Convert.ToInt32(DDL_BrandID.SelectedValue);

                //========== Check for Role if not selected Permission Grid will be applied ==========//
                if (RB_Roles.Checked)
                { objTaskeroUser.RoleID = Convert.ToInt16(DDL_Roles.SelectedValue); }
                else
                {
                    objTaskeroUser.RoleID = 0;
                    //========== Calling the  XML for PermissionID ==========//
                    objTaskeroUser.PermissionID = RetrievePermissionID();
                }

                //========== Inserting for Taskero User and its Details Table ==========//
                int _Result = objTaskeroUser.uspInsertTaskeroUser();
                //==========  Checking for the Flag ==========//
                switch (_Result)
                {
                    case 1://========= Show message for the duplicate user name exist =========//
                        LblError.Text = Resources.Message.IsAlreadyExists.Replace("[[FIELDNAME]]", "Username");
                        break;
                    case 2://========= Show message for the new user created  =========//
                        LblError.Text = "User created successfully.";
                        ClearControls();
                        BTN_AddUser.Visible = true;
                        TRGridTable.Visible = true;
                        TRMainTable.Visible = false;
                        FillUserGrid();
                        SetToDefault();
                        break;
                    case 3://========= Show message for the duplicate Email address exist =========//
                        LblError.Text = Resources.Message.IsAlreadyExists.Replace("[[FIELDNAME]]", "Email");
                        break;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Method Used for Updating the Taskero User==========//
        private void UpdateUser()
        {
            try
            {
                //========== Validations ==========//
                if (DDL_Company.SelectedIndex == 0) { LblError.Text = Resources.Message.CompanyNameIsRequired; return; }
                if (CHK_Branch.SelectedIndex == -1) { LblError.Text = Resources.Message.BranchNameIsRequired; return; }
                if (CHK_Dongle.SelectedIndex == -1) { LblError.Text = Resources.Message.DongleNameIsRequired; return; }
                if (TXT_FirstName.Text == "") { LblError.Text = Resources.Message.FirstNameIsRequired; return; }
                if (TXT_LastName.Text == "") { LblError.Text = Resources.Message.LastNameIsRequired; return; }
                if (TXT_Email.Text == "") { LblError.Text = Resources.Message.EmailIsRequired; return; }
                else if (!Validations.IsEmail(TXT_Email.Text.Trim())) { LblError.Text = Resources.Message.ValidEmailRequired; return; }

                //if (TXT_LoginID.Text == "") { LblError.Text = "Username is required"; return; }        
                //if (TXT_LoginPass.Text == "") { LblError.Text = "Password is required"; return; }
                //if (TXT_LoginConfirmPassword.Text == "") { LblError.Text = "Confirm Password is required"; return; }
                if (!CompareValidator.Equals(TXT_LoginPass.Text, TXT_LoginConfirmPassword.Text))
                { LblError.Text = "Mismatch Password & Confirm Password."; return; }

                if (CheckIPAddress() == false) { LblError.Text = "Please enter a valid IP address."; return; }
                string _AllowedIP = TXT_AllowedIP1.Text + "." + TXT_AllowedIP2.Text + "." + TXT_AllowedIP3.Text + "." + TXT_AllowedIP4.Text;
                if (_AllowedIP == "...")
                { _AllowedIP = ""; }

                if (RB_Roles.Checked == false && RB_Permission.Checked == false)
                { LblError.Text = "Either select Roles or Permission."; return; }


                if (RB_Roles.Checked)
                {
                    if (Convert.ToInt32(DDL_Roles.SelectedValue) == 0)
                    {
                        LblError.Text = Resources.Message.RoleIsRequired; return;
                    }
                    else
                    {
                        if (Convert.ToInt32(DDL_Roles.SelectedValue) == 5)
                        {
                            if (Txt_Customer.Text.Trim() == "")
                            {
                                LblError.Text = "Customer name is required."; return;
                            }
                        }
                    }
                }


                //========== Declarations ==========//
                TaskeroUserData objTaskeroUser = new TaskeroUserData();

                //========== Check Duplicate and Inserting ==========//
                objTaskeroUser.TaskeroUserID = Convert.ToInt32(LTRL_ID.Text);


                //========== Creating XML for Company ID ==========//
                objTaskeroUser.CompanyID = "<Company><CompanyID>" + DDL_Company.SelectedValue + "</CompanyID></Company>";

                //========== Creating XML for BranchID ==========//
                string _StrTemp = "";
                for (int i = 0; i < CHK_Branch.Items.Count; i++)
                {
                    if (CHK_Branch.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<BranchID>" + CHK_Branch.Items[i].Value + "</BranchID>";
                        }
                        else
                        {
                            _StrTemp = "<Branch><BranchID>" + CHK_Branch.Items[i].Value + "</BranchID>";
                        }
                    }
                }
                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Branch>";
                }
                objTaskeroUser.BranchID = _StrTemp;

                //========== Creating XML for DongleID ==========//
                _StrTemp = "";
                for (int i = 0; i < CHK_Dongle.Items.Count; i++)
                {
                    if (CHK_Dongle.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<DongleID>" + CHK_Dongle.Items[i].Value + "</DongleID>";
                        }
                        else
                        {
                            _StrTemp = "<Dongle><DongleID>" + CHK_Dongle.Items[i].Value + "</DongleID>";
                        }
                    }
                }
                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Dongle>";
                }
                objTaskeroUser.DongleID = _StrTemp;

                //========== Creating XML for ReportID ==========//
                _StrTemp = "";
                for (int i = 0; i < CHK_Reports.Items.Count; i++)
                {
                    if (CHK_Reports.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<ReportID>" + CHK_Reports.Items[i].Value + "</ReportID>";
                        }
                        else
                        {
                            _StrTemp = "<Report><ReportID>" + CHK_Reports.Items[i].Value + "</ReportID>";
                        }
                    }
                }

                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Report>";
                }
                objTaskeroUser.ReportID = _StrTemp;

                objTaskeroUser.FirstName = TXT_FirstName.Text.Trim();
                objTaskeroUser.LastName = TXT_LastName.Text.Trim();
                objTaskeroUser.Email = TXT_Email.Text.Trim();
                objTaskeroUser.LoginID = TXT_LoginID.Text.Trim();
                objTaskeroUser.LoginPass = TXT_LoginConfirmPassword.Text.Trim();
                objTaskeroUser.AllowedIP = _AllowedIP;
                objTaskeroUser.BrandID = Convert.ToInt32(DDL_BrandID.SelectedValue);

                if (DDL_Active.SelectedIndex == 0)
                { objTaskeroUser.IsActive = true; }
                else
                { objTaskeroUser.IsActive = false; }

                if (DDL_EmailWhenYellow.SelectedIndex == 0)
                { objTaskeroUser.EmailWhenYellow = true; }
                else
                { objTaskeroUser.EmailWhenYellow = false; }

                if (DDL_EmailWhenRed.SelectedIndex == 0)
                { objTaskeroUser.EmailWhenRed = true; }
                else
                { objTaskeroUser.EmailWhenRed = false; }


                if (DDL_LicenseAgreed.SelectedIndex == 0)
                { objTaskeroUser.SiteLicenseAgreed = true; }
                else
                { objTaskeroUser.SiteLicenseAgreed = false; }

                //========== Check for Role if not selected Permission Grid will be applied ==========//
                if (RB_Roles.Checked)
                { objTaskeroUser.RoleID = Convert.ToInt16(DDL_Roles.SelectedValue); }
                else
                {
                    objTaskeroUser.RoleID = 0;

                    //========== Calling the  XML for PermissionID ==========//
                    objTaskeroUser.PermissionID = RetrievePermissionID();
                }
                if (Convert.ToInt32(DDL_Roles.SelectedValue) == 5)
                    objTaskeroUser.Customer = Txt_Customer.Text.Trim();
                else objTaskeroUser.Customer = "";

                //========== Inserting for Taskero User and its Details Table ==========//
                int _Result = objTaskeroUser.uspUpdateTaskeroUser();
                //==========  Checking for the Flag ==========//
                if (_Result == 1)
                {
                    LblError.Text = Resources.Message.IsAlreadyExists.Replace("[[FIELDNAME]]", "Email");

                }
                else if (_Result == 2)
                {
                    LblError.Text = "User Updated successfully.";
                    ClearControls();
                    BTN_AddUser.Visible = true;
                    TRGridTable.Visible = true;
                    TRMainTable.Visible = false;
                    Sessions.SetSessions("UserMansgementDongle_Data", null);
                    //==========  Calling the Mehtod for Binding the Grid ==========//
                    FillUserGrid();
                    SetToDefault();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //=========== Method Used for Checking the rights of the User logged in==========//
        private void CheckRights()
        {
            try
            {
                //========== Checking the Add Right for the User Management ===========//
                if (Sessions.Add_User == false)
                    BTN_AddUser.Visible = false;
                else
                    BTN_AddUser.Visible = true;
                //========== Checking the Edit Right for the User Management ===========//
                if (Sessions.Update_User == false)
                { RG_Users.Columns[7].Visible = false; }
                else
                { RG_Users.Columns[7].Visible = true; }
                //========== Checking the Delete Right for the User Management ===========//
                if (Sessions.Delete_User == false)
                { RG_Users.Columns[8].Visible = false; }
                else
                { RG_Users.Columns[8].Visible = true; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        #region Check Dongle Data
        /// <summary>
        /// Used to check the dongle data
        /// </summary>
        /// <param name="DT_Dongle">Dongle list</param>
        private void CheckDongleData(DataTable DT_Dongle, CheckBoxList CHK_My_List)
        {
            try
            {
                for (int i = 0; i < DT_Dongle.Rows.Count; i++)
                {
                    for (int j = 0; j < CHK_My_List.Items.Count; j++)
                    {
                        if (CHK_My_List.Items[j].Value == DT_Dongle.Rows[i][0].ToString())
                        {
                            CHK_My_List.Items[j].Selected = true;
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Set Default Value Of Password Fields
        /// <summary>
        /// Used to set the default value of the password field
        /// </summary>
        private void SetToDefault()
        {
            try
            {
                TXT_LoginPass.TextMode = TextBoxMode.Password;
                TXT_LoginConfirmPassword.TextMode = TextBoxMode.Password;
                BTN_DisplayPasswordField.Text = "Show";
                BTN_DisplayPasswordField.ToolTip = "Click to show password field values";
                if (DDL_BrandID.Items.Count > 0)
                { DDL_BrandID.SelectedIndex = 0; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        private void BindDDLBranding(int _TaskeroUserID)
        {
            try
            {
                CompanyBrandingData objCompanyBranding = new CompanyBrandingData();
                DDL_BrandID.DataSource = objCompanyBranding.uspGetCompanyBrandingData(_TaskeroUserID);
                DDL_BrandID.DataTextField = "BrandName";
                DDL_BrandID.DataValueField = "BrandID";
                DDL_BrandID.DataBind();

                DDL_BrandID.Items.Insert(0, (new ListItem("Default", "0")));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void FillBrandList(int _CompanyID)
        {
            try
            {
                DDL_BrandID.SelectedIndex = -1;

                //==========  Declaring Objects ==========//
                CompanyBrandingData ObjCompanyBrandingData = new CompanyBrandingData();

                //==========  Fetching the Data From Taskero User ==========//
                ObjCompanyBrandingData.CompanyID = _CompanyID;
                DataTable _DT = ObjCompanyBrandingData.uspGetCompanyBrandingByCompanyID();

                DDL_BrandID.DataSource = _DT;
                DDL_BrandID.DataTextField = "BrandName";
                DDL_BrandID.DataValueField = "BrandID";
                DDL_BrandID.DataBind();

                //========== Insert Items DropDownList in Brand ===========//
                DDL_BrandID.Items.Insert(0, new ListItem("Default", "0"));
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }

        //=========== Method Used for filling Reports ==========//
        private void FillReports()
        {
            try
            {
                //========== Declarations ==========//
                ReportData objReport = new ReportData();

                //========== Get The Data Set ==========//
                DataTable DT = objReport.uspGetAllReports(int.Parse(Session["UserID"].ToString()));

                CHK_Reports.DataSource = DT;
                CHK_Reports.DataTextField = "ReportDesc";
                CHK_Reports.DataValueField = "ReportID";
                CHK_Reports.DataBind();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region CheckIP
        private bool CheckIPAddress()
        {
            try
            {
                if (TXT_AllowedIP1.Text == "") { TXT_AllowedIP1.Text = "*"; }
                if (TXT_AllowedIP2.Text == "") { TXT_AllowedIP2.Text = "*"; }
                if (TXT_AllowedIP3.Text == "") { TXT_AllowedIP3.Text = "*"; }
                if (TXT_AllowedIP4.Text == "") { TXT_AllowedIP4.Text = "*"; }

                string _AllowedIP = TXT_AllowedIP1.Text.Trim() + "." + TXT_AllowedIP2.Text.Trim() + "." + TXT_AllowedIP3.Text.Trim() + "." + TXT_AllowedIP4.Text.Trim();
                if (_AllowedIP != "...")
                {
                    if (_AllowedIP.LastIndexOf("*") > -1)
                    {
                        return true;
                    }
                    else
                    {
                        return Validations.IsValidIP(_AllowedIP);
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}