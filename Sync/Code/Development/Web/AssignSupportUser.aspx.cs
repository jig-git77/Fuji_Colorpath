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
using Fuji.Business;
using Fuji.Common;
using Telerik.WebControls;
#endregion

public partial class AssignSupportUser : PageBase
{
    #region PageLoad & Control Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Clear the error message on load            
            LblError.Text = "";

            //========= Set the default settings for the grid =========//
            Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_Branch);
            Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_Dongle);

            //========= Set false to paging of the grid  =========//
            GV_Branch.AllowPaging = false;
            GV_Dongle.AllowPaging = false;

            if (IsPostBack == false)
            {
                //========== Check Session ===========//
                Fuji.Common.Sessions.CheckUserLogin();

                //========== Check User Role and redirect to home page as per user role ===========//
                Fuji.Common.CommonFunctions.CheckUserRole();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //========== Fill User DDL ==========//
                FillUserList();

                //========== Fill Company DDL ==========//
                FillCompany();

                //========= Hide the update button at first load =========//
                BTN_UpdateSupportUser.Visible = false;
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void DDL_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //========= Check Whether the item is selected or not=========//
            if (DDL_Company.SelectedIndex != 0)
            {
                //========= Fill Branch list based on the company =========//
                FillBranch(Convert.ToInt32(DDL_Company.SelectedValue));

                //========= Fill Dongle list based on the company =========//
                //FillDongle(Convert.ToInt32(DDL_Company.SelectedValue));
                //========= Assign the null values to the Dongle List =========//
                GV_Dongle.DataSource = null;
                GV_Dongle.DataBind();

                //========== Fill User DDL ==========//
                //FillUser(DDL_Company.SelectedValue);

                //========= Check User =========//
                if (DDL_UserList.SelectedIndex != 0)
                    DDL_UserList_SelectedIndexChanged(null, null);
            }
            else
            {
                //========== Clear the valid of the user dropdownlist ===========//
                //DDL_UserList.Items.Clear();

                //========== Check Fill the first item as select user ===========//
                //DDL_UserList.Items.Insert(0, new ListItem("-- Select User --", "0"));

                //========= Assign the null values to the Branch List =========//
                GV_Branch.DataSource = null;
                GV_Branch.DataBind();

                //========= Assign the null values to the Dongle List =========//
                GV_Dongle.DataSource = null;
                GV_Dongle.DataBind();

                //========= show the Select Company message =========//
                LblError.Text = "Please Select Company";
            }

            //========= Check Whether the Branch / Dongle are filed with data then show/hide the button =========//
            if (GV_Branch.Rows.Count > 0 || GV_Dongle.Rows.Count > 0)
                BTN_UpdateSupportUser.Visible = true;
            else
                BTN_UpdateSupportUser.Visible = false;
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void BTN_UpdateSupportUser_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Check Company selected for the operation or not ===========//
            if (DDL_Company.SelectedIndex == 0)
            { LblError.Text = "Please Select Company."; return; }

            //========== Check User selected for the operation or not ===========//
            if (DDL_UserList.SelectedIndex == 0)
            { LblError.Text = "Please select user."; return; }

            //========= Check Whether the Branch list is filled with branches or not, if so then proceed with branch list =========//
            if (GV_Branch.Rows.Count > 0)
            {
                //========= Check Branch View Data And Update =========//
                //========== Find the id values from the grid view ===========//
                Array arrBranchID = CheckGridID(GV_Branch);

                //========== Declare branch object ===========//
                UserBranchData ObjUserBranch = new UserBranchData();

                //========== Assign the value to the user branch object ===========//
                ObjUserBranch.TaskeroUserID = Convert.ToInt32(DDL_UserList.SelectedValue);
                int IntResultBranch = ObjUserBranch.uspInsertUserBranchData(arrBranchID.GetValue(0).ToString(), arrBranchID.GetValue(1).ToString()); 
            }

            //========= Check Whether the Dongle list is filled with dongless or not, if so then proceed with Dongle list =========//
            if (GV_Dongle.Rows.Count > 0)
            {
                //========= Check Branch View Data And Update =========//
                //========== Find the id values from the grid view ===========//
                Array arrDongleID = CheckGridID(GV_Dongle);

                //========== Declare branch object ===========//
                UserDongleData ObjUserDongle = new UserDongleData();

                //========== Assign the value to the user branch object ===========//
                ObjUserDongle.TaskeroUserID = Convert.ToInt32(DDL_UserList.SelectedValue);
                int IntResultDongle = ObjUserDongle.uspInsertUserDongleData(arrDongleID.GetValue(0).ToString(), arrDongleID.GetValue(1).ToString());
            }
            //========= Show the confirmation message for the updation =========//
            LblError.Text = "User branch & dongle updated successfully."; 
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void DDL_UserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDL_Company.SelectedIndex != 0)
            {
                //========= Check Whether the User is selected or not =========//
                if (DDL_UserList.SelectedIndex != 0)
                {
                    //========== Clear the check box values from both branch and dongle list grid===========//
                    ClearGrid(GV_Branch);
                    ClearGrid(GV_Dongle);

                    //========== Assign the default values to branch data ===========//
                    GetUserBranchData();

                    //========= Check And Assign the checked branch dongle list =========//
                    CHK_Data_CheckedChanged(null,null);
                }
                else
                {
                    //========== Clear the check box values from both branch and dongle list grid===========//
                    ClearGrid(GV_Branch);
                    ClearGrid(GV_Dongle); 
                }
            }
            else
            {
                //========== Clear the check box values from both branch and dongle list grid===========//
                ClearGrid(GV_Branch);
                ClearGrid(GV_Dongle); 

                //========= Show the Select Company message =========//
                LblError.Text = "Please Select Company.";
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void GV_Branch_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox CHK_Data = (CheckBox)e.Row.FindControl("CHK_Data");
                CHK_Data.CheckedChanged += new EventHandler(CHK_Data_CheckedChanged);
            }
        }
        catch (Exception exc)
        {            
            LblError.Text = HandleException(exc);
        }
    }

    protected void CHK_Data_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //========= Fill the dongles as per the branch selected =========//
            FillDongleListBox();
            if (DDL_UserList.SelectedIndex != 0)
                //========== Assign the default values to dongle data ===========//
                GetUserDongleData();
        }
        catch (Exception exc)
        {            
            LblError.Text = HandleException(exc);
        }
    }
    #endregion

    #region Functions And Methods

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
            Resources.BranchManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values For Header of the User Branch and Dongle ==========//
            LTRL_Assign_Support_User.Text = Resources.BranchManagement.LBL_AssignSupportUserHeader_Text;

            //========== Set Values For Header of the Company Name for Branch and Dongle ==========//
            LTRL_Company.Text = Resources.BranchManagement.LBL_Company_Text;

            //========== Set Values For Header of the User Label ==========//
            LTRL_UserList.Text = Resources.BranchManagement.LBL_SupportUser_Text;

            //========== Set Values For Header of the Update button ==========//
            BTN_UpdateSupportUser.Text = Resources.BranchManagement.BTN_Update_Text;

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill User List Of Support User
    /// <summary>
    /// Used to fill the user list
    /// </summary>
    private void FillUserList()
    {
        try
        {
            //========== Declare the object of the Taskero User ===========//
            TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

            //========== Assign tehe  values fo the taskero user object ===========//
            ObjTaskeroUser.RoleID = 6;

            DataTable dtUserList = ObjTaskeroUser.uspGetUserByRoleID();

            //========== Check Whether the data is returned or not===========//
            if (dtUserList != null && dtUserList.Rows.Count > 0)
            {
                //========== Fill the user list  ===========//
                DDL_UserList.DataSource = dtUserList;
                DDL_UserList.DataTextField = "LoginId";
                DDL_UserList.DataValueField = "TaskeroUserID";
                DDL_UserList.DataBind();

                //========== Check Fill the first item as select user ===========//
                DDL_UserList.Items.Insert(0, new ListItem("-- Select User --", "0"));
            }
            else
            {
                //========== Clear the valid of the user dropdownlist ===========//
                DDL_UserList.Items.Clear();

                //========== Check Fill the first item as select user ===========//
                DDL_UserList.Items.Insert(0, new ListItem("-- Select User --", "0"));
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Company List
    /// <summary>
    /// Used to fill company list by logged in user
    /// </summary>
    private void FillCompany()
    {
        try
        {
            //==========  Declaring Objects ==========//
            UserCompanyData objCompany = new UserCompanyData();

            //==========  Get Data Table and Fill ==========//
            objCompany.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objCompany.CompanyID = Fuji.Common.Sessions.RoleID;
            DataTable DT = objCompany.uspGetCompanyByUserID();

            //==========  Fill Branch Company DDL ==========//
            DDL_Company.DataSource = DT;
            DDL_Company.DataTextField = "CompanyName";
            DDL_Company.DataValueField = "CompanyID";
            DDL_Company.DataBind();

            //========== Check Fill the first item as select user ===========//
            DDL_Company.Items.Insert(0, new ListItem("-- Select Company --", "0"));
            //========= Check Whether there is more than 1 company exist and default first company is selected =========//
            if (DDL_Company.Items.Count > 1)
            {
                DDL_Company.SelectedIndex = 1;
                DDL_Company_SelectedIndexChanged(null, null);
            }
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
            //========== Declaring Objects ===========//
            BranchData objBranch = new BranchData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Branch = new DataTable();
            objBranch.CompanyID = _CompanyID;
            DT_Branch = objBranch.uspGetBranchByCompanyID();
            if (DT_Branch != null && DT_Branch.Rows.Count > 0)
            {
                //========== Assign the Branch list ===========//
                GV_Branch.DataSource = DT_Branch.DefaultView;
                GV_Branch.DataBind();

                //========== Show the Branch list ===========//
                GV_Branch.Visible = true;
            }
            else
            {
                //========== Bind null values to the null ===========//
                GV_Branch.DataSource = null;
                GV_Branch.DataBind();

                //========== Hide the List when no data found ===========//
                GV_Branch.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region FillDongle
    /// <summary>
    /// Used to bind the dongles based on the company
    /// </summary>
    /// <param name="_CompanyID">Company id to be passed to fetch the Donglea</param>
    private void FillDongle(int _CompanyID)
    {
        try
        {
            //========== Declaring Objects ===========//
            CPDongleData ObjDongle = new CPDongleData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Dongle = new DataTable();
            DT_Dongle = ObjDongle.uspGetAllDongleByCompany(_CompanyID);
            if (DT_Dongle != null && DT_Dongle.Rows.Count > 0)
            {
                //========== Assign the Dongle list ===========//
                GV_Dongle.DataSource = DT_Dongle.DefaultView;
                GV_Dongle.DataBind();

                //========== Show the Dongle list ===========//
                GV_Dongle.Visible = true;
            }
            else
            {
                //========== Bind null values to the null ===========//
                GV_Dongle.DataSource = null;
                GV_Dongle.DataBind();

                //========== Hide the List when no data found ===========//
                GV_Dongle.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Clear Grid Checked boxes
    /// <summary>
    /// Used to clear the checkboxes
    /// </summary>
    /// <param name="GV_GridView">Grid from checkboxes to be find</param>
    /// <returns>returns the id from the grid</returns>
    private void ClearGrid(GridView GV_GridView)
    {
        try
        {
            //========== loop through the grid and find ids===========//
            for (int i = 0; i < GV_GridView.Rows.Count; i++)
            {
                //========= Find the check box in the grid =========//
                CheckBox CHK_Data = (CheckBox)GV_GridView.Rows[i].FindControl("CHK_Data");

                //========= Check that check box is checked, if checked then uncheck it=========//
                if (CHK_Data.Checked)
                    CHK_Data.Checked = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Get User Branch Data
    /// <summary>
    /// Used to get the user branch data
    /// </summary>
    private void GetUserBranchData()
    {
        try
        {
            //========== Declare object of the UserBranch data ===========//
            UserBranchData ObjUserBranch = new UserBranchData();

            //========== Assign the values to the user branch object ===========//
            ObjUserBranch.TaskeroUserID = Convert.ToInt32(DDL_UserList.SelectedValue);

            DataTable DtUserBranch = ObjUserBranch.uspGetUserBranchData();

            //========== Check Whether the data is returned or not===========//
            if (DtUserBranch != null && DtUserBranch.Rows.Count > 0)
            {
                for (int i = 0; i < GV_Branch.Rows.Count; i++)
                {
                    for (int j = 0; j < DtUserBranch.Rows.Count; j++)
                    {
                        //========= Find the check box in the grid =========//
                        CheckBox CHK_Data = (CheckBox)GV_Branch.Rows[i].FindControl("CHK_Data");

                        //========= find the value of the branch =========//
                        int _CPBranchID = Convert.ToInt32(CHK_Data.Attributes["AID"].ToString());

                        //========= Check that user contains the data with that branch then check that checkbox =========//
                        if (_CPBranchID == Convert.ToInt32(DtUserBranch.Rows[j]["BranchID"].ToString()))
                        {
                            CHK_Data.Checked = true;
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

    #region Get User Dongle Data
    /// <summary>
    /// Used to get the user Dongle data
    /// </summary>
    private void GetUserDongleData()
    {
        try
        {
            //========== Declare object of the UserDongle data ===========//
            UserDongleData ObjUserDongle = new UserDongleData();

            //========== Assign the values to the user Dongle object ===========//
            ObjUserDongle.TaskeroUserID = Convert.ToInt32(DDL_UserList.SelectedValue);

            DataTable DtUserDongle = ObjUserDongle.uspGetUserDongleData();

            //========== Check Whether the data is returned or not===========//
            if (DtUserDongle != null && DtUserDongle.Rows.Count > 0)
            {
                for (int i = 0; i < GV_Dongle.Rows.Count; i++)
                {
                    for (int j = 0; j < DtUserDongle.Rows.Count; j++)
                    {
                        //========= Find the check box in the grid =========//
                        CheckBox CHK_Data = (CheckBox)GV_Dongle.Rows[i].FindControl("CHK_Data");

                        //========= find the value of the dongle =========//
                        int _CPDongleID = Convert.ToInt32(CHK_Data.Attributes["AID"].ToString());

                        //========= Check that user contains the data with that dongle then check that checkbox =========//
                        if (_CPDongleID == Convert.ToInt32(DtUserDongle.Rows[j]["CPDongleID"].ToString()))
                        {

                            CHK_Data.Checked = true;
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

    #region GET Grid Checked ID
    /// <summary>
    /// Used to get the IDs from the data grid
    /// </summary>
    /// <param name="DG_Grid">Grid from ids to be find</param>
    /// <returns>returns the id from the grid</returns>
    private Array CheckGridID(GridView GV_GridView)
    {
        try
        {
            //========== Declare variable to get id from the grid ===========//
            string STR_Non_Selected = "", STR_Selected = "";
            string[] arrIDs = new string[] { "", "" };
            //========== loop through the grid and find ids===========//
            for (int i = 0; i < GV_GridView.Rows.Count; i++)
            {
                //========== find check box in the passed grid view for checked value ===========//
                CheckBox CHK_Data = (CheckBox)GV_GridView.Rows[i].FindControl("CHK_Data");
                if (CHK_Data.Checked)
                {
                    STR_Selected += Convert.ToInt32(CHK_Data.Attributes["AID"].ToString()) + ",";
                }
                else
                {
                    STR_Non_Selected += Convert.ToInt32(CHK_Data.Attributes["AID"].ToString()) + ",";
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

    #region Fill User List Of Support User By Company
    /// <summary>
    /// Used to fill the user list
    /// </summary>
    private void FillUser( string _CompanyID)
    {
        try
        {
            //========== Declare the object of the Taskero User ===========//
            TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

            //========== Assign tehe  values fo the taskero user object ===========//
            ObjTaskeroUser.RoleID = 6;
            ObjTaskeroUser.CompanyID = _CompanyID;

            DataTable dtUserList = ObjTaskeroUser.uspGetUserOfAssignedCompany();

            //========== Check Whether the data is returned or not===========//
            if (dtUserList != null && dtUserList.Rows.Count > 0)
            {
                //========== Fill the user list  ===========//
                DDL_UserList.DataSource = dtUserList;
                DDL_UserList.DataTextField = "LoginId";
                DDL_UserList.DataValueField = "TaskeroUserID";
                DDL_UserList.DataBind();

                //========== Check Fill the first item as select user ===========//
                DDL_UserList.Items.Insert(0, new ListItem("-- Select User --", "0"));
            }
            else
            {
                //========== Clear the valid of the user dropdownlist ===========//
                DDL_UserList.Items.Clear();

                //========== Check Fill the first item as select user ===========//
                DDL_UserList.Items.Insert(0, new ListItem("-- Select User --", "0"));
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region  Fill Dongle ListBox 
    /// <summary>
    /// Used to fill the dongle list based on the branches 
    /// </summary>
    private void FillDongleListBox()
    {
        try
        {
            //========== Creating XML for DongleID ==========//
            string _StrTemp = "";
            for (int i = 0; i < GV_Branch.Rows.Count; i++)
            {
                CheckBox CHK_Data = (CheckBox)GV_Branch.Rows[i].FindControl("CHK_Data");
                if (CHK_Data.Checked)
                {
                    if (_StrTemp.Trim().Length > 0)
                    {
                        _StrTemp += "<BranchID>" + Convert.ToInt32(CHK_Data.Attributes["AID"].ToString()) + "</BranchID>";
                    }
                    else
                    {
                        _StrTemp = "<Branch><BranchID>" + Convert.ToInt32(CHK_Data.Attributes["AID"].ToString()) + "</BranchID>";
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
                //========== Assign the Dongle list ===========//
                GV_Dongle.DataSource = _ArrRoles;
                GV_Dongle.DataBind();

                //========== Hide the List when no data found ===========//
                GV_Dongle.Visible = true;
            }
            else
            {
                //========== Bind null values to the null ===========//
                GV_Dongle.DataSource = null;
                GV_Dongle.DataBind();

                //========== Hide the List when no data found ===========//
                GV_Dongle.Visible = false;
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
