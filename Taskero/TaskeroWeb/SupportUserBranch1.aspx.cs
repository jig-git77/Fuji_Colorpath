using System;
using System.Data;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
using Telerik.WebControls;


namespace TaskeroWeb
{
    public partial class SupportUserBranch1 : PageBase
    {
        #region "Private Declaration"
        private const int ConstDefaultPageSize = 25;
        #endregion

        #region "PageLoad & Control Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Clear the error message on load            
                LBL_Error.Text = "";

                if (IsPostBack == false)
                {
                    //========== Check Session ===========//
                    Sessions.CheckUserLogin();

                    //========== Check User Role and redirect to home page as per user role ===========//
                    CommonFunctions.CheckUserRole();

                    //========== Clear the Session On first view of the page ===========//                
                    Sessions.SetSessions("AssignSupportUserBranchData", null);

                    //========== Fill Variables to Form ==========//
                    SetLabels();

                    //========== Show UserBranch List ===========//
                    FillUserBranchList();

                    //========== Show UserDongle List ===========//
                    FillUserDongleList();

                    //========== Show Assign Support User List ===========//
                    FillAssignSupportUserBranchList();
                }

                //========== Check Browser type and as per browser set headerstyle of the Grid ===========//
                SetGridHeader();
            }
            catch (Exception exc)
            { LBL_Error.Text = HandleException(exc); }
        }
        protected void DDL_UserBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_UserBranch.SelectedValue != "0")
                { }
            }
            catch (Exception exc)
            { LBL_Error.Text = HandleException(exc); }
        }
        protected void RG_SupportUser_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {

        }
        protected void RG_SupportUser_ItemCommand(object source, GridCommandEventArgs e)
        {

        }
        protected void RG_SupportUser_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }
        protected void RG_SupportUser_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

        }
        protected void RG_SupportUser_SortCommand(object source, GridSortCommandEventArgs e)
        {

        }
        protected void BTN_UpdateSupportUser_Click(object sender, EventArgs e)
        {

        }
        protected void BTN_UpdateAssignDongle_Click(object sender, EventArgs e)
        {

        }
        protected void DDL_UserDongle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region Functions And Methods

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
                    RG_SupportUser.HeaderStyle.Width = 90;
                else if (Request.Browser.Browser == "Firefox")
                    RG_SupportUser.HeaderStyle.Width = 120;
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
                LTRL_UserBranch.Text = Resources.BranchManagement.LBL_BranchName_Text;
                LTRL_SupportUser.Text = Resources.BranchManagement.LBL_SupportUser_Text;
                LTRL_ModelTitle.Text = Resources.BranchManagement.LBL_ModelTitle_Text;
                LTRL_AssignSupportUser.Text = Resources.BranchManagement.LBL_AssignSupportUser_Text;
                LTRL_AssignUserDongle.Text = Resources.BranchManagement.LBL_AssignUserDongle_Text;
                LTRL_UserBranch.Text = Resources.BranchManagement.LBL_BranchName_Text;
                LTRL_UserDongle.Text = Resources.BranchManagement.LBL_UserDongle_Text;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill UserBranch List
        /// <summary>
        /// Used to fill the UserBranch list 
        /// </summary>
        /// <param name="DDL_UserBranch">UserBranch DropDownList to be filled with UserBranch</param>
        private void FillUserBranchList()
        {
            try
            {
                //========== Declare the object of the UserBrach ===========//
                BranchData objUserBranch = new BranchData();

                //========== Fetch the data from company  ===========//
                //objUserBranch.TaskeroUserID = Sessions.UserID;
                DataTable DTUserBranch = objUserBranch.uspGetAllBranch();

                //========== Check Whether the data is returned or not===========//
                if (DTUserBranch != null && DTUserBranch.Rows.Count > 0)
                {
                    DDL_UserBranch.DataSource = DTUserBranch;
                    DDL_UserBranch.DataValueField = "BranchID"; DDL_UserBranch.DataTextField = "BranchName";
                    DDL_UserBranch.DataBind();
                }
                else
                { DDL_UserBranch.DataSource = null; DDL_UserBranch.DataBind(); }

                //========== Check Fill the first item as Select Company ===========//
                DDL_UserBranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill UserDongle List
        /// <summary>
        /// Used to fill the UserDongle list 
        /// </summary>
        /// <param name="DDL_UserBranch">UserBranch DropDownList to be filled with UserBranch</param>
        private void FillUserDongleList()
        {
            try
            {
                //========== Declare the object of the UserBrach ===========//
                CPDongleData objUserDongle = new CPDongleData();

                //========== Fetch the data from company  ===========//
                //objUserBranch.TaskeroUserID = Sessions.UserID;
                DataTable DTUserDongle = objUserDongle.uspGetAllDongle();

                //========== Check Whether the data is returned or not===========//
                if (DTUserDongle != null && DTUserDongle.Rows.Count > 0)
                {
                    DDL_UserDongle.DataSource = DTUserDongle;
                    DDL_UserDongle.DataValueField = "CPDongleID"; DDL_UserDongle.DataTextField = "CPSerialNUM";
                    DDL_UserDongle.DataBind();
                }
                else
                { DDL_UserDongle.DataSource = null; DDL_UserDongle.DataBind(); }

                //========== Check Fill the first item as Select Company ===========//
                DDL_UserDongle.Items.Insert(0, new ListItem("-- Select Dongle --", "0"));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill Assign Support UserBranch List
        /// <summary>
        /// Used to fill the user list based on the branch id passed
        /// </summary>
        private void FillAssignSupportUserBranchList()
        {
            try
            {
                //========== Declare DataTable ===========//
                DataTable DT_SupportUserBranch = new DataTable();

                //========== Check Session of branch data, if null then fill with the branch data ===========//
                if (Sessions.GetSession("AssignSupportUserBranchData") == null)
                {
                    //========== Declare the object of the Branch ===========//
                    UserBranchData ObjUserBranch = new UserBranchData();

                    //========== Fetch the all data from Branch ===========//
                    DT_SupportUserBranch = ObjUserBranch.uspGetAssignSupportUserOfBranchID(Sessions.RoleID);

                    //========== Set the branch data to the session ===========//
                    Sessions.SetSessions("AssignSupportUserBranchData", DT_SupportUserBranch);
                }
                //========== Get the session Data From the Session ===========//
                DT_SupportUserBranch = (DataTable)Sessions.GetSession("AssignSupportUserBranchData");

                //========== Check Whether the data is returned or not===========//
                if (DT_SupportUserBranch != null && DT_SupportUserBranch.Rows.Count > 0)
                {
                    //========== Assign the Branch list ===========//
                    RG_SupportUser.DataSource = DT_SupportUserBranch;
                    RG_SupportUser.DataBind();

                    //========== Show the Branch list ===========//
                    RG_SupportUser.Visible = true;

                    //========== Hide the pager style if page count is 0 ===========//
                    RG_SupportUser.PagerStyle.Visible = true; RG_SupportUser.MasterTableView.AllowFilteringByColumn = true; RG_SupportUser.AllowSorting = true;
                }
                else
                {
                    //========== Bind null values to the null ===========//
                    RG_SupportUser.DataSource = null;
                    RG_SupportUser.DataBind();

                    //========== Hide the List when no data found ===========//
                    RG_SupportUser.Visible = false;

                    //========== Show message about data not found on selection of this company ===========//
                    LBL_Error.Text = "No data found.";

                    //========== Hide the pager style if page count is 0 ===========//
                    RG_SupportUser.PagerStyle.Visible = false; RG_SupportUser.MasterTableView.AllowFilteringByColumn = false; RG_SupportUser.AllowSorting = false;
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
}