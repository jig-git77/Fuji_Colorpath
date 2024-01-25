#region Using System
using System;
using System.Data;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
using Telerik.WebControls;
#endregion

namespace TaskeroWeb
{
    public partial class BranchManagement : PageBase
    {
        #region Declaration
        bool BlnViewDonglePage = false;
        private const int ConstDefaultPageSize = 25;
        #endregion

        #region Page Load and Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ===========//
                LblError.Text = "";

                if (IsPostBack == false)
                {
                    //========== Check Session ===========//
                    Sessions.CheckUserLogin();

                    //========== Check User Role and redirect to home page as per user role ===========//
                    CommonFunctions.CheckUserRole();

                    //========== Clear the Session On first view of the page ===========//
                    Sessions.SetSessions("BranchData", null);
                    Sessions.SetSessions("AssignSupportUserBranchData", null);

                    //========== Fill Variables to Form ==========//
                    SetLabels();

                    //========== Show Branch List ===========//
                    ShowBranchList();

                    //========== if company not selected any company then select all the records from the branch===========//
                    FillBranchList();

                    //========== Check User rights for the branch management===========//
                    CheckRights();
                }

                //========== Check Browser type and as per browser set headerstyle of the Grid ===========//
                SetGridHeader();
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void BTN_AddNewBranch_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Clear the input fields before adding new item ===========//
                ClearInputField();

                //========== Hide the Branch List and show the branch input form ===========//
                HideBranchList();

                //========== Fill CompanyList DropDownList to view the branch list ===========//
                FillCompanyList(DDL_CompanyName);

                //========== Set Branch Data Session null ===========//
                Sessions.SetSessions("BranchData", null);

                //========== Set the Header field ===========//
                BTN_Save.Text = Resources.BranchManagement.BTN_Save_Add;
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Clear the input fields before adding new item ===========//
                ClearInputField();

                //========== Show Branch List ===========//
                ShowBranchList();

                //========== Fill BranchList ===========//
                FillBranchList();

                //========== Set Branch ID to initial Zero null ===========//
                LTRL_BranchID.Text = "0";
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputFields())
                {
                    //========== Declare the object of the Branch ===========//
                    BranchData ObjBranch = new BranchData();
                    int intResult;

                    //========== Assign the properties to the object of the branch ===========//
                    ObjBranch.CompanyID = Convert.ToInt32(DDL_CompanyName.SelectedValue);
                    ObjBranch.BranchName = TXT_BranchName.Text.Trim();
                    ObjBranch.BranchLocation = TXT_BranchLocation.Text.Trim();
                    ObjBranch.PrimaryContactName = TXT_PrimaryContactName.Text.Trim();
                    ObjBranch.Tel_CountryCode = TXT_Tel_CountryCode.Text.Trim();
                    ObjBranch.Tel_PhoneNo = TXT_Tel_PhoneNo.Text.Trim();
                    ObjBranch.Tel_Extension = TXT_Tel_Extension.Text.Trim();
                    ObjBranch.PrimaryContactEmail = TXT_PrimaryContactEmail.Text.Trim();
                    ObjBranch.PrimaryContactCell = TXT_PrimaryContactCell.Text.Trim();
                    if (CHK_IsActive.Checked)
                        ObjBranch.IsActive = true;
                    else
                        ObjBranch.IsActive = false;

                    //========== Check That Operation is insert or not by id ===========//
                    if (LTRL_BranchID.Text.Trim().Equals("0"))
                    {
                        ObjBranch.BranchID = 0;

                        //========== Insertion new branch by calling insert method ===========//
                        intResult = ObjBranch.uspInsertBranch();
                        if (intResult == 0)
                        {
                            //========== Show insertion message ===========//
                            LblError.Text = Resources.Message.BranchNameAlreadyExists; return;
                        }
                        else
                            //========== Show insertion message ===========//
                            LblError.Text = "New branch inserted successfully";
                    }
                    else
                    {
                        ObjBranch.BranchID = Convert.ToInt32(LTRL_BranchID.Text);

                        //========== Update branch by calling update method ===========//
                        intResult = ObjBranch.uspUpdateBranch();
                        if (intResult == 0)
                        {
                            //========== Show insertion message ===========//
                            LblError.Text = Resources.Message.BranchNameAlreadyExists; return;
                        }
                        else
                        {
                            //========== Show updation message ===========//
                            LblError.Text = "Branch updated successfully";

                            //========== Set Branch ID to initial Zero null ===========//
                            LTRL_BranchID.Text = "0";
                        }
                    }

                    //========== Clear the Session On first view of the page ===========//
                    Sessions.SetSessions("BranchData", null);

                    //========== Set True wiuth the variable ===========//
                    BlnViewDonglePage = true;

                    //========== Show Branch List ===========//
                    ShowBranchList();

                    //========== Fill All Branch List ===========//
                    FillBranchList();

                    //========== Set Branch ID to initial Zero null ===========//
                    LTRL_CompanyID.Text = "0";
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void RG_Branch_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            try
            {
                //========== Comes to this end when row is edit ===========//
                if (e.CommandName.Equals("EditBranch"))
                {
                    //========== Hide the Branch List and show the branch input form ===========//
                    HideBranchList();

                    //========== Fill CompanyList DropDownList to view the branch list ===========//
                    FillCompanyList(DDL_CompanyName);

                    //========== Get Branch Data by BranchID ===========//
                    GetBranchData(Convert.ToInt32(RG_Branch.Items[e.Item.ItemIndex].Cells[2].Text));

                    //========== Set the Header field ===========//
                    LTRL_Header.Text = Resources.BranchManagement.LBL_Header_Text_Edit;

                    //========== Set the Header field ===========//
                    BTN_Save.Text = Resources.BranchManagement.BTN_Save_Save;
                }
                //========== Comes to this end when row command is Delete ===========//
                if (e.CommandName.Equals("Delete"))
                {
                    //========== Declare the object of the Branch ===========//
                    BranchData ObjBranch = new BranchData();

                    //========== Set the Company id  ===========//
                    ObjBranch.BranchID = Convert.ToInt32(RG_Branch.Items[e.Item.ItemIndex].Cells[2].Text);

                    //========== Delete the Branch ===========//
                    ObjBranch.uspDeleteBranch();

                    //========== Clear the Session On first view of the page ===========//
                    Sessions.SetSessions("BranchData", null);

                    //========== Clear all filtering options , if any ===========//
                    btnReset_Click(null, null);

                    //========== Fill All Branch List ===========//
                    FillBranchList();

                    //========== Show deletion message ===========//
                    LblError.Text = "Branch deleted successfully.";
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void RG_Branch_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            try
            {
                //========== Check Session For the BranchData ===========//
                if (Sessions.GetSession("BranchData") != null)
                {
                    //========== Get the session Data From the Session ===========//
                    DataTable dtBranchList = (DataTable)Sessions.GetSession("BranchData");
                    RG_Branch.DataSource = dtBranchList;
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void RG_Branch_SortCommand(object source, Telerik.WebControls.GridSortCommandEventArgs e)
        {
            try
            {
                //========== Check Sort expression exist or not ===========//
                if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
                {
                    //========== Declare the sort expression object ===========//
                    GridSortExpression SortExpr = new GridSortExpression();

                    //========== Set the sort expression value  ===========//
                    SortExpr.FieldName = e.SortExpression;

                    //========== Add sort expression value ===========//
                    e.Item.OwnerTableView.SortExpressions.AddSortExpression(SortExpr);
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void RG_Branch_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //========== Check And find delete button put custom delete confirmation ===========//
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    //========== Find the ImageButton and set the confirmation message on clicking the delete button ===========//
                    ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                    IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");

                    //========== Find the ImageButton to assign the dongle to support user ===========//
                    ImageButton IMGBTN_Assign = (ImageButton)e.Item.FindControl("IMGBTN_Assign");
                    IMGBTN_Assign.Attributes.Add("OnClick", "OpenAssignSupportUserWindow('ManageSupportUser.aspx?BranchID=" + ((DataRowView)e.Item.DataItem)["BranchID"].ToString() + "&ModalDialog=Y&CompanyID=" + ((DataRowView)e.Item.DataItem)["CompanyID"].ToString() + "');");

                    ImageButton IMGBTN_VideoAccess = (ImageButton)e.Item.FindControl("IMGBTN_VideoAccess");
                    IMGBTN_VideoAccess.Attributes.Add("OnClick", "OpenAssignVideoAccessToUserWindow('ManageUserVideoAccess.aspx?BranchID=" + ((DataRowView)e.Item.DataItem)["BranchID"].ToString() + "&ModalDialog=Y&CompanyID=" + ((DataRowView)e.Item.DataItem)["CompanyID"].ToString() + "');");
                }
                //========== Check for the pager style function ===========//
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
                {
                    #region Pager Style
                    //========== Find Controls for the pager style ===========//
                    Literal LTRL_RecordText = (Literal)e.Item.FindControl("LTRL_RecordText");

                    ImageButton IBTN_First = (ImageButton)e.Item.FindControl("IBTN_First");
                    ImageButton IBTN_Prev = (ImageButton)e.Item.FindControl("IBTN_Prev");
                    ImageButton IBTN_Next = (ImageButton)e.Item.FindControl("IBTN_Next");
                    ImageButton IBTN_Last = (ImageButton)e.Item.FindControl("IBTN_Last");

                    int IntFirstIndexInPage = 0, IntLastIndexInPage = 0, IntDataSourceCount = 0;

                    IntFirstIndexInPage = RG_Branch.MasterTableView.PagingManager.FirstIndexInPage;
                    IntLastIndexInPage = RG_Branch.MasterTableView.PagingManager.LastIndexInPage + 1;
                    IntDataSourceCount = RG_Branch.MasterTableView.PagingManager.DataSourceCount;

                    if (IntDataSourceCount > 0)
                    {
                        IntFirstIndexInPage++;
                    }

                    LTRL_RecordText.Text = "Record(s) : " + IntFirstIndexInPage + " to " + IntLastIndexInPage + " of " + IntDataSourceCount + " | ";


                    if (RG_Branch.MasterTableView.PagingManager.IsFirstPage)
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
                    if (RG_Branch.MasterTableView.PagingManager.IsLastPage == true)
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
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void BTN_Save_Dongle_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Call the Save button method to save the data ===========//
                btnSave_Click(BTN_Save, null);

                //========== If the Dongle to be add then open the new window for the dongle management ===========//
                if (BlnViewDonglePage)
                {
                    //========== View the dongle page ===========//
                    Response.Redirect("DongleManagement.aspx", false);
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        protected void RG_Branch_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                //========== Check and create events for the pager selection  ===========//
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.Pager)
                {
                    //========== Find the Record page dropdownlist ===========//
                    DropDownList DDl_RecordPage = (DropDownList)e.Item.FindControl("DDl_RecordPage");

                    //========== Check Whether teh Record page dropdownlist is null or not ===========//
                    if (DDl_RecordPage != null)
                    {
                        //========== Declare the event handler for the Record page dropdownlist ===========//
                        DDl_RecordPage.SelectedIndexChanged += new EventHandler(DDlRecordPage_SelectedIndexChanged);

                        if (!IsPostBack)
                        {
                            //========== Set initial value of the Record page dropdownlist ===========//
                            DDl_RecordPage.SelectedValue = ConstDefaultPageSize.ToString();
                        }
                        else
                        {
                            if (ViewState["RecordPerPage"] != null)
                            {
                                DDl_RecordPage.SelectedIndex = -1;
                                DDl_RecordPage.Items.FindByValue(ViewState["RecordPerPage"].ToString()).Selected = true;
                            }
                        }
                    }

                    //========== Reset all the filter options value ===========//
                    Button BTN_Reset = (Button)e.Item.FindControl("BTN_Reset");
                    BTN_Reset.Text = Resources.BranchManagement.BTN_Reset_Text;
                    if (BTN_Reset != null)
                    {
                        //========== Declare the event handler for the Reset button ===========//
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
                //========== Declare the variable for the selection value of the record page dropdownlist ===========//
                string StrSelectedValue;
                StrSelectedValue = ((DropDownList)sender).SelectedItem.Value;
                ViewState["RecordPerPage"] = StrSelectedValue;
                //========== Set the paze size ===========//
                RG_Branch.PageSize = Convert.ToInt32(StrSelectedValue);
                RG_Branch.CurrentPageIndex = 0;
                //========== Check for branch data in session and assign if exist ===========//
                if (Sessions.GetSession("BranchData") != null)
                {
                    //========== Assign the branch data fro the session value ===========//
                    DataTable dtBranchList = (DataTable)Sessions.GetSession("BranchData");
                    RG_Branch.DataSource = dtBranchList;
                }

                //========== Bind the grid ===========//
                RG_Branch.DataBind();
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Check grid column in the grid ===========//
                foreach (Telerik.WebControls.GridColumn ColFilter in RG_Branch.MasterTableView.Columns)
                {
                    //========== set all column filter value to empty ===========//
                    ColFilter.CurrentFilterFunction = Telerik.WebControls.GridKnownFunction.NoFilter;
                    ColFilter.CurrentFilterValue = String.Empty;
                }

                //========== set all filter expression to empty ===========//
                RG_Branch.MasterTableView.FilterExpression = String.Empty;
                RG_Branch.MasterTableView.Rebind();
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }

        #endregion

        #region Functions And Methods

        #region Show Branch List
        /// <summary>
        /// Used to Show the Branch List
        /// </summary>
        private void ShowBranchList()
        {
            TD_AddEditBranch.Visible = false;
            TD_ShowBranch.Visible = true;
        }
        #endregion

        #region Hide Branch List
        /// <summary>
        /// Used to Hide the Branch List
        /// </summary>
        private void HideBranchList()
        {
            TD_AddEditBranch.Visible = true;
            TD_ShowBranch.Visible = false;
        }
        #endregion

        #region Fill Company List
        /// <summary>
        /// Used to fill the company list in passed company DropDownList
        /// </summary>
        /// <param name="ddlCompany">Company DropDownList to be filled with company</param>
        private void FillCompanyList(DropDownList DDL_Company)
        {
            try
            {
                //==========  Declaring Objects ==========//
                UserCompanyData objCompany = new UserCompanyData();

                //==========  Get Data Table and Fill ==========//
                objCompany.TaskeroUserID = Sessions.UserID;
                objCompany.CompanyID = Sessions.RoleID;
                DataTable DTCompany = objCompany.uspGetCompanyByUserID();
                //DataTable DtCompany = ObjCompany.uspGetAllCompany();

                //========== Check Whether the data is returned or not===========//
                if (DTCompany != null && DTCompany.Rows.Count > 0)
                {
                    DDL_Company.DataSource = DTCompany;
                    DDL_Company.DataTextField = "CompanyName";
                    DDL_Company.DataValueField = "CompanyID";
                    DDL_Company.DataBind();
                }

                //========== Check Fill the first item as Select Company ===========//
                DDL_Company.Items.Insert(0, new ListItem("-- Select Company --", "-1"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill Branch List
        /// <summary>
        /// Used to fill the branch list based on the company id passed
        /// </summary>
        private void FillBranchList()
        {
            try
            {
                //========== Declare DataTable ===========//
                DataTable DtBranchList;

                //========== Check Session of branch data, if null then fill with the branch data ===========//
                if (Sessions.GetSession("BranchData") == null)
                {
                    //========== Declare the object of the Branch ===========//
                    BranchData ObjBranch = new BranchData();

                    //========== Fetch the all data from Branch ===========//
                    DtBranchList = ObjBranch.uspGetAllBranchWithCompanyName(Sessions.UserID, Sessions.RoleID);

                    //========== Set the branch data to the session ===========//
                    Sessions.SetSessions("BranchData", DtBranchList);
                }
                //========== Get the session Data From the Session ===========//
                DtBranchList = (DataTable)Sessions.GetSession("BranchData");

                //========== Check Whether the data is returned or not===========//
                if (DtBranchList != null && DtBranchList.Rows.Count > 0)
                {
                    //========== Assign the Branch list ===========//
                    RG_Branch.DataSource = DtBranchList;
                    RG_Branch.DataBind();

                    //========== Show the Branch list ===========//
                    RG_Branch.Visible = true;

                    //========== Hide the pager style if page count is 0 ===========//
                    RG_Branch.PagerStyle.Visible = true; RG_Branch.MasterTableView.AllowFilteringByColumn = true; RG_Branch.AllowSorting = true;
                }
                else
                {
                    //========== Bind null values to the null ===========//
                    RG_Branch.DataSource = null;
                    RG_Branch.DataBind();

                    //========== Hide the List when no data found ===========//
                    RG_Branch.Visible = false;

                    //========== Hide the pager style if page count is 0 ===========//
                    RG_Branch.PagerStyle.Visible = false; RG_Branch.MasterTableView.AllowFilteringByColumn = false; RG_Branch.AllowSorting = false;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Clear Input Fields
        /// <summary>
        /// Used to clear the all input fields in the form
        /// </summary>
        private void ClearInputField()
        {
            try
            {
                TXT_BranchName.Text = "";
                TXT_BranchLocation.Text = "";
                TXT_PrimaryContactName.Text = "";
                TXT_Tel_CountryCode.Text = "";
                TXT_Tel_PhoneNo.Text = "";
                TXT_Tel_Extension.Text = "";
                TXT_PrimaryContactEmail.Text = "";
                TXT_PrimaryContactCell.Text = "";
                CHK_IsActive.Checked = false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Set Labels
        /// <summary>
        /// Used to set labels of the form
        /// </summary>
        private void SetLabels()
        {
            try
            {
                //========== Set Culture ==========//
                Resources.Message.Culture = (new System.Globalization.CultureInfo(Sessions.Language));
                Resources.BranchManagement.Culture = (new System.Globalization.CultureInfo(Sessions.Language));

                //========== Set Values ==========//
                LTRL_Title.Text = Resources.BranchManagement.LBL_Title_Text;
                LTRL_Header.Text = Resources.BranchManagement.LBL_Header_Text_Add;
                LTRL_CompanyName.Text = Resources.BranchManagement.LBL_CompanyName_Text;
                LTRL_BranchName.Text = Resources.BranchManagement.LBL_BranchName_Text;
                LTRL_BranchLocation.Text = Resources.BranchManagement.LBL_BranchLocation_Text;
                LTRL_PrimaryContactName.Text = Resources.BranchManagement.LBL_PrimaryContactName_Text;
                LTRL_CountryCode.Text = Resources.BranchManagement.LBL_CountryCode_Text;
                LTRL_Phone.Text = Resources.BranchManagement.LBL_Phone_Text;
                LTRL_Extension.Text = Resources.BranchManagement.LBL_Extension_Text;
                LTRL_PrimaryContactEmail.Text = Resources.BranchManagement.LBL_PrimaryContactEmail_Text;
                LTRL_PrimaryContactCell.Text = Resources.BranchManagement.LBL_PrimaryContactCell_Text;
                LTRL_Active.Text = Resources.BranchManagement.LBL_Active_Text;

                //========== Set Button Text ==========//
                BTN_AddNewBranch.Text = Resources.BranchManagement.BTN_AddNewBranch_Text;
                BTN_Save.Text = Resources.BranchManagement.BTN_Save_Add;
                BTN_Cancel.Text = Resources.BranchManagement.BTN_Cancel_Text;
                BTN_Save_Dongle.Text = Resources.BranchManagement.BTN_Save_Dongle_Text;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Get Branch Data
        /// <summary>
        /// Used to get the Branch data based on the Branch id
        /// </summary>
        /// <param name="intBranchID">Id of the branch</param>
        private void GetBranchData(int _BranchID)
        {
            try
            {
                //========== Declare the object of the Branch ===========//
                BranchData ObjBranch = new BranchData();

                //========== Set the Company id  ===========//
                ObjBranch.BranchID = _BranchID;

                //========== Fetch the data from Branch ===========//
                DataTable DtBranchList = ObjBranch.uspGetBranchByBranchID();

                //========== Check Whether the data is returned or not===========//
                if (DtBranchList != null && DtBranchList.Rows.Count > 0)
                {
                    LTRL_BranchID.Text = DtBranchList.Rows[0]["BranchID"].ToString();
                    LTRL_CompanyID.Text = DtBranchList.Rows[0]["CompanyID"].ToString();
                    TXT_BranchName.Text = DtBranchList.Rows[0]["BranchName"].ToString();
                    TXT_BranchLocation.Text = DtBranchList.Rows[0]["BranchLocation"].ToString();
                    TXT_PrimaryContactName.Text = DtBranchList.Rows[0]["PrimaryContactName"].ToString();
                    TXT_Tel_CountryCode.Text = DtBranchList.Rows[0]["Tel_CountryCode"].ToString();
                    TXT_Tel_PhoneNo.Text = DtBranchList.Rows[0]["Tel_PhoneNo"].ToString();
                    TXT_Tel_Extension.Text = DtBranchList.Rows[0]["Tel_Extension"].ToString();
                    TXT_PrimaryContactEmail.Text = DtBranchList.Rows[0]["PrimaryContactEmail"].ToString();
                    TXT_PrimaryContactCell.Text = DtBranchList.Rows[0]["PrimaryContactCell"].ToString();
                    if (DtBranchList.Rows[0]["IsActive"] != null && DtBranchList.Rows[0]["IsActive"].ToString().Trim().Length != 0)
                    {
                        if (DtBranchList.Rows[0]["IsActive"].ToString().Trim().Equals("False"))
                            CHK_IsActive.Checked = false;
                        else
                            CHK_IsActive.Checked = true;
                    }
                    else CHK_IsActive.Checked = false;
                    if (DtBranchList.Rows[0]["CompanyID"] != null && DtBranchList.Rows[0]["CompanyID"].ToString().Trim().Length != 0)
                    {
                        DDL_CompanyName.SelectedValue = DtBranchList.Rows[0]["CompanyID"].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Validate Input Fields
        /// <summary>
        /// Used to Validate the input fields of the form
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputFields()
        {
            try
            {
                //========== Check Company Name is selected or not, show message if not ===========//
                if (DDL_CompanyName.SelectedIndex == 0)
                { LblError.Text = Resources.Message.CompanyNameIsRequired; return false; }

                //========== Check Branch Name is entered or not, show message if not ===========//
                if (TXT_BranchName.Text.Trim().Length == 0)
                { LblError.Text = Resources.Message.BranchNameIsRequired; return false; }

                //========== Check Branch Location is entered or not, show message if not ===========//
                if (TXT_BranchLocation.Text.Trim().Length == 0)
                { LblError.Text = Resources.Message.BranchLocationIsRequired; return false; }

                //========== Check primary contact name is entered or not, show message if not ===========//
                if (TXT_PrimaryContactName.Text.Trim().Length == 0)
                { LblError.Text = Resources.Message.PrimaryContactNameIsRequired; return false; }

                //========== Check Dial Country Code is entered or not, show message if not ===========//
                if (TXT_Tel_CountryCode.Text.Trim().Length == 0)
                { LblError.Text = Resources.Message.CountryCodeIsRequired; return false; }
                else
                {
                    //========== Check Dial Country Code is valid or not, show message if not ===========//
                    if (Validations.IsInt32(TXT_Tel_CountryCode.Text) == false)
                    { LblError.Text = Resources.Message.InvalidCountryCode; return false; }
                }

                //========== Check phone number is entered or not, show message if not ===========//
                if (TXT_Tel_PhoneNo.Text.Trim().Length == 0)
                { LblError.Text = Resources.Message.PhoneNoIsRequired; return false; }
                else
                {
                    //========== Check telephone number is valid or not, show message if not ===========//
                    //if (Validations.IsValidUsPhone(TXT_Tel_PhoneNo.Text.Trim()) == false)
                    //{ LblError.Text = Resources.Message.InvalidPhoneNo; return false; }
                }

                //========== Check telephone extension is entered or not, show message if not ===========//
                if (TXT_Tel_Extension.Text.Trim().Length == 0)
                {
                    //LblError.Text = Resources.Message.ExtensionIsRequired; return false; 
                }
                else
                {
                    //========== Check telephone extension is valid or not, show message if not ===========//
                    if (Validations.IsInt32(TXT_Tel_Extension.Text) == false)
                    { LblError.Text = Resources.Message.InvalidExtension; return false; }
                }

                //========== Check primary contact email is entered or not, show message if not ===========//
                if (TXT_PrimaryContactEmail.Text.Trim().Length == 0)
                { LblError.Text = Resources.Message.PrimaryContactEmailIsRequired; return false; }
                else
                {
                    //========== Check primary contact email is valid or not, show message if not ===========//
                    if (Validations.IsEmail(TXT_PrimaryContactEmail.Text) == false)
                    { LblError.Text = Resources.Message.InvalidPrimaryContactEmail; return false; }
                }
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Check User Rights
        /// <summary>
        /// Used to check user rights for the branch management
        /// </summary>
        public void CheckRights()
        {
            try
            {
                //========== Check Whether the logged in user has the permission to Add New branch, if not then hide Add new branch button ===========//
                if (Sessions.Add_Branch == true)
                    BTN_AddNewBranch.Visible = true;
                else BTN_AddNewBranch.Visible = false;

                //========== Check Whether the logged in user has the permission to Edit branch, if not then hide Edit Column from the branch list ===========//
                if (Sessions.Update_Branch == true)
                    RG_Branch.Columns[10].Visible = true;

                else RG_Branch.Columns[10].Visible = false;

                //========== Check Whether the logged in user has the permission to Delete branch, if not then hide Delete Column from the branch list ===========//
                //if (Sessions.Delete_Branch == true)
                //    RG_Branch.Columns[11].Visible = true;
                //else 
                //RG_Branch.Columns[12].Visible = false;  /**** chirag commented out htis code giving user ability to edit branch on 06/14/2011

                //========== Check Whether the logged in user has the permission to Add New support user, if not then hide Add new support user button ===========//
                if (Sessions.Add_Support_User == true)
                    RG_Branch.Columns[11].Visible = true;
                else
                    RG_Branch.Columns[11].Visible = false;

                //========== Check Whether the logged in user has the permission to Add New Dongle, if not then hide Save And Add dongle button ===========//
                if (Sessions.Add_Dongle == true)
                    BTN_Save_Dongle.Visible = true;
                else BTN_Save_Dongle.Visible = false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Set Grid Header
        /// <summary>
        /// Used to set the header style for browsers
        /// </summary>
        private void SetGridHeader()
        {
            try
            {
                //========== Check Browser type and as per browser set headerstyle of the Grid ===========//
                if (Request.Browser.Browser == "IE")
                    RG_Branch.HeaderStyle.Width = 90;
                else if (Request.Browser.Browser == "Firefox")
                    RG_Branch.HeaderStyle.Width = 120;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion


        #endregion
    }
}