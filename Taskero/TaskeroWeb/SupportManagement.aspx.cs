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
    public partial class SupportManagement : PageBase
    {
        #region Declarations
        private const int _ConstDefaultPageSize = 25;
        #endregion

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //=========== Clear Error ==========//
                LblError.Text = "";

                //=========== Check Login ==========//
                Sessions.CheckUserLogin();

                //========== Check User Role and redirect to home page as per user role ===========//
                CommonFunctions.CheckUserRole();

                //=========== Bind the show button with script which show and hide the password fields ==========//
                //BTN_DisplayPasswordField.Attributes.Add("OnClick", "return display();");

                //=========== Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========= Clear the date for first time =========//
                    Sessions.SetSessions("StartDate", null);
                    Sessions.SetSessions("SupportDongle_Data", null);

                    FIL_Username.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";
                    FIL_Password.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";
                    FIL_ConformPassword.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";

                    TRMainTable.Visible = false;
                    TRGridTable.Visible = true;
                    BTN_AddUser.Visible = true;

                    //========== Fill Variables to Form ==========//
                    SetLabels();

                    //=========== Clear the Session with Datatable of User Data==========//
                    Sessions.SetSessions("TaskerSupportUser_DataTable", null);

                    //=========== Calling the Method for binding the User Grid ==========//
                    FillUserGrid();

                    //=========== Calling the Method for Checking User Rights==========//
                    CheckRights();

                    //=========== Fill Reports ==========//
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
                Sessions.SetSessions("SupportDongle_Data", null);
                SetToDefault();
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void BTN_AddSave_Click(object sender, EventArgs e)
        {
            try
            {
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
                BTN_AddUser.Visible = false;
                TRGridTable.Visible = false;
                TRMainTable.Visible = true;
                TXT_LoginID.Enabled = true;
                PNL_Branch.Visible = false;
                PNL_Dongle.Visible = false;
                //=========== Clearing the All Controls ==========//
                ClearControls();
                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Fill DDl Company ==========//
                FillCompany();
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }

        }
        protected void DDL_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //========== Find the id values from the grid view ===========//
                if (CHK_Company.SelectedIndex == -1)
                {
                    LblError.Text = "Please Select Company"; CHK_Branch.Items.Clear(); PNL_Branch.Visible = false; PNL_Dongle.Visible = false; return;
                }
                Array arrCompanyID = CheckCompanyID();
                if (arrCompanyID.GetValue(0).ToString().Length == 0)
                {
                    LblError.Text = "Please Select Company"; CHK_Branch.Items.Clear(); PNL_Branch.Visible = false; PNL_Dongle.Visible = false; return;
                }
                //==========  Declaring Objects ==========//
                UserBranchData objBranch = new UserBranchData();

                //==========  Get Data Table and Fill ==========//
                objBranch.TaskeroUserID = Sessions.UserID;
                DataTable DT_Branch = objBranch.uspGetBranchOfCompanyByUserIDAndCompanyID(Sessions.RoleID, arrCompanyID.GetValue(0).ToString());
                if (DT_Branch != null && DT_Branch.Rows.Count > 0)
                {
                    CHK_Branch.Items.Clear();
                    CHK_Branch.DataSource = DT_Branch;
                    CHK_Branch.DataTextField = "BranchName";
                    CHK_Branch.DataValueField = "BranchID";
                    CHK_Branch.DataBind();
                }
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
                if (Sessions.GetSession("SupportDongle_Data") != null)
                {
                    DataSet DT_DongleData = (DataSet)Sessions.GetSession("SupportDongle_Data");
                    CheckDongleData(DT_DongleData.Tables[2], CHK_Branch);
                }
                CHK_Branch_SelectedIndexChanged(null, null);
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
                    if (Sessions.GetSession("SupportDongle_Data") != null)
                    {
                        DataSet DT_DongleData = (DataSet)Sessions.GetSession("SupportDongle_Data");
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
                if (Sessions.GetSession("TaskerSupportUser_DataTable") != null)
                {
                    DataTable DSet = (DataTable)Sessions.GetSession("TaskerSupportUser_DataTable");
                    if (DSet != null && DSet.Rows.Count > 0)
                    {
                        RadGrid1.DataSource = DSet;
                    }
                    else
                    {
                        DataTable Dset = GetUserDate();
                        if (DSet != null && DSet.Rows.Count > 0)
                            RadGrid1.DataSource = Dset;
                    }
                }
                else
                {
                    DataTable DSet = GetUserDate();
                    if (DSet != null && DSet.Rows.Count > 0)
                        RadGrid1.DataSource = DSet;
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
                        LblError.Text = "Taskero support user deleted successfully.";
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
                    Literal LBL_IsAvtive = (Literal)e.Item.FindControl("LBL_IsAvtive");


                    //========== Enabling the Checkboxes if the IsAdmin status is true ===========//
                    if (e.Item.Cells[3].Text.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                    {
                        LBL_IsAvtive.Text = "Yes";
                    }
                    else
                    {
                        LBL_IsAvtive.Text = "No";
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

                    iFirstIndexInPage = RadGrid1.MasterTableView.PagingManager.FirstIndexInPage;
                    iLastIndexInPage = RadGrid1.MasterTableView.PagingManager.LastIndexInPage + 1;
                    iDataSourceCount = RadGrid1.MasterTableView.PagingManager.DataSourceCount;

                    if (iDataSourceCount > 0)
                    {
                        iFirstIndexInPage++;
                    }

                    LTRL_RecordText.Text = "Record(s) : " + iFirstIndexInPage + " to " + iLastIndexInPage + " of " + iDataSourceCount + " | ";

                    ImageButton IBTN_First = (ImageButton)e.Item.FindControl("IBTN_First");
                    ImageButton IBTN_Prev = (ImageButton)e.Item.FindControl("IBTN_Prev");
                    ImageButton IBTN_Next = (ImageButton)e.Item.FindControl("IBTN_Next");
                    ImageButton IBTN_Last = (ImageButton)e.Item.FindControl("IBTN_Last");


                    if (RadGrid1.MasterTableView.PagingManager.IsFirstPage)
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
                    if (RadGrid1.MasterTableView.PagingManager.IsLastPage == true)
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

                RadGrid1.PageSize = Convert.ToInt32(sSelectedValue);
                RadGrid1.CurrentPageIndex = 0;
                if (Sessions.GetSession("TaskerSupportUser_DataTable") != null)
                {
                    RadGrid1.DataSource = Sessions.GetSession("TaskerSupportUser_DataTable");
                }

                RadGrid1.DataBind();
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
                foreach (Telerik.WebControls.GridColumn column in RadGrid1.MasterTableView.Columns)
                {
                    //========== set all column filter value to empty ===========//
                    column.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                    column.CurrentFilterValue = String.Empty;
                }
                Sessions.SetSessions("StartDate", null);
                //========== set all filter expression to empty ===========//
                RadGrid1.MasterTableView.FilterExpression = String.Empty;
                RadGrid1.MasterTableView.Rebind();
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
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
        #endregion

        #region Methods and Events

        #region Method for Clearing the Controls
        /// <summary>
        /// Used the clear the input as well as other controls of the form
        /// </summary>
        private void ClearControls()
        {
            try
            {
                CHK_Company.SelectedIndex = -1;
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
        #endregion

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
                LTRL_Title.Text = Resources.UserManagement.LBL_Title_Support_User;
                LTRL_SubTitle.Text = Resources.UserManagement.LBL_Support_User_Info;
                LTRL_RoleType.Text = Resources.UserManagement.LBL_Support_User_Type;
                LTRL_User_Role.Text = Resources.UserManagement.LBL_Sub_Title_Support_User;
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
                BTN_AddUser.Text = Resources.UserManagement.LBL_Add_Support_User;
                BTN_AddSave.Text = Resources.UserManagement.BTN_AddSave_Add;
                BTN_Cancel.Text = Resources.UserManagement.BTN_Cancel_Text;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill Company DropDown 
        /// <summary>
        /// Used to fill the company
        /// </summary>
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
                    CHK_Company.DataSource = DTCompany;
                    CHK_Company.DataTextField = "CompanyName";
                    CHK_Company.DataValueField = "CompanyID";
                    CHK_Company.DataBind();
                }
                if (CHK_Company.Items.Count == 0)
                { PNL_Company.Height = 0; PNL_Company.Visible = false; }
                else if (CHK_Company.Items.Count >= 5)
                { PNL_Company.Attributes.Add("width", "100%"); PNL_Company.Height = 120; PNL_Company.Visible = true; }
                else if (CHK_Company.Items.Count <= 2)
                { PNL_Company.Attributes.Add("width", "100%"); PNL_Company.Height = 48; PNL_Company.Visible = true; }
                else
                { PNL_Company.Attributes.Add("width", "100%"); PNL_Company.Height = 75; PNL_Company.Visible = true; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill Dongle ListBox 
        /// <summary>
        /// Used to fill the dongle data
        /// </summary>
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
        #endregion

        #region Method for Fetching the UserData 
        /// <summary>
        /// Used to get the user information
        /// </summary>
        /// <returns>returns the user data</returns>
        private DataTable GetUserDate()
        {
            try
            {
                //==========  Declaring Objects ==========//
                TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

                ObjTaskeroUser.RoleID = 6;// Convert.ToInt16(Sessions.RoleID);
                ObjTaskeroUser.TaskeroUserID = Sessions.UserID;

                //==========  Fetching the Data From Taskero User ==========//
                DataTable _DSet = ObjTaskeroUser.uspGetTaskeroUserPassingRoleID();

                //=========== Setting the Session with Datatable of User Data==========//
                Sessions.SetSessions("TaskerSupportUser_DataTable", _DSet);

                return _DSet;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Binding the User Data Grid
        /// <summary>
        /// Used to get the user information
        /// </summary>
        private void FillUserGrid()
        {
            try
            {
                //==========  Declaring Objects & calling the function which is fetching the data from TaskerUser Table ==========//
                DataTable _Dset = GetUserDate();
                if (_Dset != null && _Dset.Rows.Count > 0)
                {
                    RadGrid1.Visible = true;
                    RadGrid1.DataSource = _Dset;
                    RadGrid1.DataBind();
                }
                else
                {
                    RadGrid1.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Method Used for For Filling the Controls at the time of Editing Taskero User
        /// <summary>
        /// Used to edit the existing user informtaion
        /// </summary>
        /// <param name="Username">user name of the user</param>
        private void EditUser(string Username)
        {
            try
            {
                //==========  Filling the Controls at the time of Editing so calling the Default Add method ==========//
                BTN_AddUser_Click(null, null);

                //==========  Declaring Objects ==========//
                TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

                ObjTaskeroUser.LoginID = Username;
                //==========  Fetching the Data from Table ==========//
                DataSet _Dset = ObjTaskeroUser.uspTaskeroUserDataByUsername();
                if (_Dset != null)
                {
                    Sessions.SetSessions("SupportDongle_Data", _Dset);
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

                    }
                    //==========  Setting the value of Company Dropdown ==========//
                    if (_Dset.Tables[1].Rows.Count > 0)
                    {
                        if (_Dset.Tables[1].Rows[0]["CompanyID"] != null && _Dset.Tables[1].Rows[0]["CompanyID"].ToString().Trim() != "")
                        {
                            if (CHK_Company.Items.Count > 0)
                            {
                                for (int i = 0; i < _Dset.Tables[1].Rows.Count; i++)
                                {
                                    for (int j = 0; j < CHK_Company.Items.Count; j++)
                                    {
                                        if (CHK_Company.Items[j].Value == _Dset.Tables[1].Rows[i][0].ToString())
                                        {
                                            CHK_Company.Items[j].Selected = true;
                                        }
                                    }

                                }
                                DDL_Company_SelectedIndexChanged(null, null);
                            }
                        }
                    }
                    //==========  Setting the value of Branch Listbox ==========//
                    if (_Dset.Tables[2].Rows.Count > 0)
                    {
                        if (CHK_Branch.Items.Count > 0)
                        {
                            //for (int i = 0; i < _Dset.Tables[2].Rows.Count; i++)
                            //{
                            //    for (int j = 0; j < CHK_Branch.Items.Count; j++)
                            //    {
                            //        if (CHK_Branch.Items[j].Value == _Dset.Tables[2].Rows[i][0].ToString())
                            //        {
                            //            CHK_Branch.Items[j].Selected = true;
                            //        }
                            //    }
                            //}
                            CheckDongleData(_Dset.Tables[2], CHK_Branch);
                            CHK_Branch_SelectedIndexChanged(null, null);
                        }
                    }
                    //==========  Setting the value of Dongle Listbox ==========//
                    if (_Dset.Tables[3].Rows.Count > 0)
                    {
                        if (CHK_Dongle.Items.Count > 0)
                        {
                            //for (int i = 0; i < _Dset.Tables[3].Rows.Count; i++)
                            //{
                            //    for (int j = 0; j < CHK_Dongle.Items.Count; j++)
                            //    {
                            //        if (CHK_Dongle.Items[j].Value == _Dset.Tables[3].Rows[i][0].ToString())
                            //        {
                            //            CHK_Dongle.Items[j].Selected = true;
                            //        }
                            //    }

                            //}
                            CheckDongleData(_Dset.Tables[3], CHK_Dongle);
                        }
                    }

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
        #endregion

        #region Method Used for Insertng the Taskero User
        /// <summary>
        /// Used to add the new user information
        /// </summary>
        private void AddUser()
        {
            try
            {
                //========== Validations ==========//
                if (CHK_Company.SelectedIndex == -1) { LblError.Text = Resources.Message.CompanyNameIsRequired; return; }
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
                {
                    LblError.Text = "Mismatch Password & Confirm Password."; return;
                }
                if (CheckIPAddress() == false) { LblError.Text = "Please enter a valid IP address."; return; }
                string _AllowedIP = TXT_AllowedIP1.Text + "." + TXT_AllowedIP2.Text + "." + TXT_AllowedIP3.Text + "." + TXT_AllowedIP4.Text;
                if (_AllowedIP == "...")
                { _AllowedIP = ""; }

                //========== Declarations ==========//
                TaskeroUserData objTaskeroUser = new TaskeroUserData();

                //========== Check Duplicate and Inserting ==========//
                //objTaskeroUser.TaskeroUserID = Convert.ToInt32(LTRL_ID.Text);            


                //========== Creating XML for Company ID ==========//
                //objTaskeroUser.CompanyID = "<Company><CompanyID>" + CHK_Company.SelectedValue + "</CompanyID></Company>";
                //========== Creating XML for BranchID ==========//
                string _StrTemp = "";
                for (int i = 0; i < CHK_Company.Items.Count; i++)
                {
                    if (CHK_Company.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<CompanyID>" + CHK_Company.Items[i].Value + "</CompanyID>";
                        }
                        else
                        {
                            _StrTemp = "<Company><CompanyID>" + CHK_Company.Items[i].Value + "</CompanyID>";
                        }
                    }
                }
                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Company>";
                }
                objTaskeroUser.CompanyID = _StrTemp;

                //========== Creating XML for BranchID ==========//
                _StrTemp = "";
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

                objTaskeroUser.FirstName = TXT_FirstName.Text.Trim();
                objTaskeroUser.LastName = TXT_LastName.Text.Trim();
                objTaskeroUser.Email = TXT_Email.Text.Trim();
                objTaskeroUser.LoginID = TXT_LoginID.Text.Trim();
                objTaskeroUser.LoginPass = TXT_LoginConfirmPassword.Text.Trim();
                objTaskeroUser.AllowedIP = _AllowedIP;

                if (DDL_Active.SelectedValue.Equals("1", StringComparison.CurrentCultureIgnoreCase))
                {
                    objTaskeroUser.IsActive = true;
                }
                else
                {
                    objTaskeroUser.IsActive = false;
                }
                objTaskeroUser.Customer = "";


                //========== Check for Role if not selected Permission Grid will be applied ==========//
                objTaskeroUser.RoleID = 6;

                //========== Inserting for Taskero User and its Details Table ==========//
                int _Result = objTaskeroUser.uspInsertTaskeroUser();
                //==========  Checking for the Flag ==========//
                switch (_Result)
                {
                    case 1://========= Show message for the duplicate user name exist =========//
                        LblError.Text = Resources.Message.IsAlreadyExists.Replace("[[FIELDNAME]]", "Username");
                        break;
                    case 2://========= Show message for the new user created  =========//
                        LblError.Text = "Support user created successfully.";
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
        #endregion

        #region Method Used for Updating the Taskero User
        /// <summary>
        /// Used to update the user information
        /// </summary>
        private void UpdateUser()
        {
            try
            {
                //========== Validations ==========//
                if (CHK_Company.SelectedIndex == -1) { LblError.Text = Resources.Message.CompanyNameIsRequired; return; }
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
                {
                    LblError.Text = "Mismatch Password & Confirm Password."; return;
                }
                if (CheckIPAddress() == false) { LblError.Text = "Please enter a valid IP address."; return; }
                string _AllowedIP = TXT_AllowedIP1.Text + "." + TXT_AllowedIP2.Text + "." + TXT_AllowedIP3.Text + "." + TXT_AllowedIP4.Text;
                if (_AllowedIP == "...")
                { _AllowedIP = ""; }

                //========== Declarations ==========//
                TaskeroUserData objTaskeroUser = new TaskeroUserData();

                //========== Check Duplicate and Inserting ==========//
                objTaskeroUser.TaskeroUserID = Convert.ToInt32(LTRL_ID.Text);


                //========== Creating XML for CompanyID ==========//
                string _StrTemp = "";
                for (int i = 0; i < CHK_Company.Items.Count; i++)
                {
                    if (CHK_Company.Items[i].Selected)
                    {
                        if (_StrTemp.Trim().Length > 0)
                        {
                            _StrTemp += "<CompanyID>" + CHK_Company.Items[i].Value + "</CompanyID>";
                        }
                        else
                        {
                            _StrTemp = "<Company><CompanyID>" + CHK_Company.Items[i].Value + "</CompanyID>";
                        }
                    }
                }
                if (_StrTemp.Trim().Length > 0)
                {
                    _StrTemp += "</Company>";
                }
                objTaskeroUser.CompanyID = _StrTemp;
                //========== Creating XML for BranchID ==========//
                _StrTemp = "";
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

                objTaskeroUser.FirstName = TXT_FirstName.Text.Trim();
                objTaskeroUser.LastName = TXT_LastName.Text.Trim();
                objTaskeroUser.Email = TXT_Email.Text.Trim();
                objTaskeroUser.LoginID = TXT_LoginID.Text.Trim();
                objTaskeroUser.LoginPass = TXT_LoginConfirmPassword.Text.Trim();
                if (DDL_Active.SelectedValue.Equals("1", StringComparison.CurrentCultureIgnoreCase))
                {
                    objTaskeroUser.IsActive = true;
                }
                else
                {
                    objTaskeroUser.IsActive = false;
                }
                objTaskeroUser.AllowedIP = _AllowedIP;
                //========== Check for Role if not selected Permission Grid will be applied ==========//
                objTaskeroUser.RoleID = 6;
                objTaskeroUser.Customer = "";

                //========== Inserting for Taskero User and its Details Table ==========//
                int _Result = objTaskeroUser.uspUpdateTaskeroUser();
                //==========  Checking for the Flag ==========//
                if (_Result == 1)
                {
                    LblError.Text = Resources.Message.IsAlreadyExists.Replace("[[FIELDNAME]]", "Email");

                }
                else if (_Result == 2)
                {
                    LblError.Text = "Support user updated successfully.";
                    ClearControls();
                    BTN_AddUser.Visible = true;
                    TRGridTable.Visible = true;
                    TRMainTable.Visible = false;
                    Sessions.SetSessions("SupportDongle_Data", null);
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
        #endregion

        #region Method Used for Checking the rights of the Support User logged in
        /// <summary>
        /// Used to check the rights of the logged in user
        /// </summary>
        private void CheckRights()
        {
            try
            {
                //========== Checking the Add Right for the Support User Management ===========//
                if (Sessions.Add_Support_User == false)
                    BTN_AddUser.Visible = false;
                else
                    BTN_AddUser.Visible = true;
                //========== Checking the Edit Right for the Support User Management ===========//
                if (Sessions.Add_Support_User == false)
                { RadGrid1.Columns[7].Visible = false; }
                else
                { RadGrid1.Columns[7].Visible = true; }
                //========== Checking the Delete Right for the Support User Management ===========//
                if (Sessions.Remove_Support_User == false)
                { RadGrid1.Columns[8].Visible = false; }
                else
                { RadGrid1.Columns[8].Visible = true; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region GET ListBox Selected Company ID
        /// <summary>
        /// Used to get the IDs from the company list box
        /// </summary>
        /// <returns>returns the id from the grid</returns>
        private Array CheckCompanyID()
        {
            try
            {
                //========== Declare variable to get id from the grid ===========//
                string STR_Non_Selected = "", STR_Selected = "";
                string[] arrIDs = new string[] { "", "" };
                //========== loop through the grid and find ids===========//
                for (int i = 0; i < CHK_Company.Items.Count; i++)
                {
                    //========== find check box in the passed grid view for checked value ===========//
                    if (CHK_Company.Items[i].Selected)
                    {
                        STR_Selected += Convert.ToInt32(CHK_Company.Items[i].Value) + ",";
                    }
                    else
                    {
                        STR_Non_Selected += Convert.ToInt32(CHK_Company.Items[i].Value) + ",";
                    }
                }
                STR_Selected = STR_Selected.TrimEnd(',');
                STR_Non_Selected = STR_Non_Selected.TrimEnd(',');

                //========== Assign the selected and non selected values to the array list ===========//
                arrIDs.SetValue(STR_Selected, 0);
                arrIDs.SetValue(STR_Non_Selected, 1);

                //========== return the values of the id ===========//
                return arrIDs;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Check Dongle Data
        /// <summary>
        /// Used to check the dongle data
        /// </summary>
        /// <param name="DT_Dongle">Dongle list</param>
        /// <param name="CHK_My_List">checkbox to be checked with the list</param>
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
                //========== Set To Default ==========//
                TXT_LoginPass.TextMode = TextBoxMode.Password;
                TXT_LoginConfirmPassword.TextMode = TextBoxMode.Password;
                BTN_DisplayPasswordField.Text = "Show";
                BTN_DisplayPasswordField.ToolTip = "Click to show password field values";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion


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
                //========== Check IP Address ==========//
                if (TXT_AllowedIP1.Text == "") { TXT_AllowedIP1.Text = "*"; }
                if (TXT_AllowedIP2.Text == "") { TXT_AllowedIP2.Text = "*"; }
                if (TXT_AllowedIP3.Text == "") { TXT_AllowedIP3.Text = "*"; }
                if (TXT_AllowedIP4.Text == "") { TXT_AllowedIP4.Text = "*"; }
                string _AllowedIP = TXT_AllowedIP1.Text + "." + TXT_AllowedIP2.Text + "." + TXT_AllowedIP3.Text + "." + TXT_AllowedIP4.Text;
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