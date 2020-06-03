#region System
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
using Fuji.Common;
using Fuji.Business;
using Telerik.WebControls;
#endregion

public partial class ManageSupportUser : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear the Error message ==========//
            LblError.Text = "";

            //=========== Set the grid default stylesheet==========//
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(GV_User_And_Dongle);

            if (!IsPostBack)
            {
                //========== Check User Logged In ==========//
                if (Request.QueryString["ModalDialog"] == null)
                { Fuji.Common.Sessions.CheckUserLoginPopUp(); }
                else
                { Fuji.Common.Sessions.CheckUserLoginModelDialog(); }

                //=========== Check the Querystring branch id for the dongle selection on branchid ==========//
                if (Request.QueryString["BranchID"] != null && Request.QueryString["BranchID"].ToString().Trim().Length != 0)
                {
                    //=========== Set the labels in the form ==========//
                    SetLabels();

                    //=========== Clear the session on the first load ==========//
                    Fuji.Common.Sessions.SetSessions("TaskerSupportUserWithDongle_DataTable", null);

                    //=========== Bing the branch id to the label ==========//
                    LTRL_BranchID.Text = Request.QueryString["BranchID"].ToString().Trim();

                    //=========== Fill the Grid by user list and dongle list ==========//
                    FillUserListWithDongle();
                }
                //=========== Check the Querystring company id for the company selection on companyid ==========//
                if (Request.QueryString["CompanyID"] != null && Request.QueryString["CompanyID"].ToString().Trim().Length != 0)
                {
                    //=========== Bing the branch id to the label ==========//
                    LTRL_CompanyID.Text = Request.QueryString["CompanyID"].ToString().Trim();
                }
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void GV_User_And_Dongle_RowDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            //=========== Check the item or alternating item of the grid ==========//
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //=========== Find the label,Dropdownlist and checkbox list from grid ==========//
                Literal LTRL_SupportUser = (Literal)e.Item.FindControl("LTRL_SupportUser");
                Literal LTRL_SupportUser_ID = (Literal)e.Item.FindControl("LTRL_SupportUser_ID");                
                DropDownList DDL_SupportUser = (DropDownList)e.Item.FindControl("DDL_SupportUser");
                CheckBoxList CHK_Dongle = (CheckBoxList)e.Item.FindControl("CHK_Dongle");
                Panel PNL_Dongle = (Panel)e.Item.FindControl("PNL_Dongle");
                ImageButton IMGBTN_Save = (ImageButton)e.Item.FindControl("IMGBTN_Save");
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                IMGBTN_Save.CommandArgument = ((DataRowView)e.Item.DataItem)["TaskeroUserID"].ToString();
                IMGBTN_Delete.CommandArgument = ((DataRowView)e.Item.DataItem)["TaskeroUserID"].ToString();
                DataSet DS_Dongle = new DataSet();

                LTRL_SupportUser_ID.Text = ((DataRowView)e.Item.DataItem)["TaskeroUserID"].ToString();

                //=========== Check whether the dongle list session is null or not ==========//
                if (Fuji.Common.Sessions.GetSession("TaskerSupportUserWithDongle_DataTable") != null)
                {
                    //=========== Get the dongle list from the session==========//
                    DS_Dongle = (DataSet)Fuji.Common.Sessions.GetSession("TaskerSupportUserWithDongle_DataTable");
                }

                //=========== Check whether the dongle list session is null or not ==========//
                if (DS_Dongle != null && DS_Dongle.Tables[1].Rows.Count > 0)
                {
                    //=========== Assign the dongle list to the dropdownlist==========//
                    CHK_Dongle.DataSource = DS_Dongle.Tables[1];
                    CHK_Dongle.DataTextField = "DongleSerial";
                    CHK_Dongle.DataValueField = "CPDongleID";
                    CHK_Dongle.DataBind();
                    //=========== Check whether the user has rights to save dongles==========//
                    if (Fuji.Common.Sessions.Add_Support_User == true)
                        IMGBTN_Save.Visible = true;
                    else IMGBTN_Save.Visible = true;
                    //=========== Assign the panel height from the dongle list ==========//
                    if (CHK_Dongle.Items.Count == 0)
                    { PNL_Dongle.Height = 0; PNL_Dongle.Visible = false; }
                    else if (CHK_Dongle.Items.Count <= 2)
                    { PNL_Dongle.Height = 80; PNL_Dongle.Visible = true; }
                    else
                    { PNL_Dongle.Height = 105; PNL_Dongle.Visible = true; }
                    PNL_Dongle.Attributes.Add("width", "150px");
                }
                else { IMGBTN_Save.Visible = false; PNL_Dongle.Height = 0; PNL_Dongle.Visible = false; }

                //=========== Check Whether The TaskeroUserID is zero then bind dropdownlist with userlist ==========//
                if (((DataRowView)e.Item.DataItem)["TaskeroUserID"].ToString() != "0")
                {
                    //=========== Show the Image button and its click event for the deletion ==========//
                    if (DS_Dongle != null && DS_Dongle.Tables[1].Rows.Count > 0)
                    {
                        //=========== Check whether the user has rights to delete dongles==========//
                        if (Fuji.Common.Sessions.Remove_Support_User == true)
                        {
                            IMGBTN_Delete.Visible = true;
                            IMGBTN_Delete.Attributes.Add("OnClick", "javascript : if(confirm('" + Resources.Message.DeleteConfirmMessage_Text + "') == true) return true; else return false;");
                        }
                        else
                        {
                            IMGBTN_Delete.Visible = false;
                            IMGBTN_Delete.Attributes.Add("OnClick", "");
                        }
                    }
                    else
                    {
                        IMGBTN_Delete.Visible = false;
                        IMGBTN_Delete.Attributes.Add("OnClick", "");
                    }
                    //=========== Show label for the user and hide the dropdownlist from the item ==========//
                    DDL_SupportUser.Visible = false;
                    LTRL_SupportUser.Visible = true;
                    LTRL_SupportUser.Text = ((DataRowView)e.Item.DataItem)["LoginID"].ToString();
                    //=========== Set the user dongle list the dongle list of the branch ==========//
                    GetUserDongleData(Convert.ToInt32(((DataRowView)e.Item.DataItem)["TaskeroUserID"].ToString()), CHK_Dongle);
                }
                else
                {
                    //=========== Hide the delete button if there is user with 0 ID find==========//
                    IMGBTN_Delete.Visible = false;
                    IMGBTN_Delete.Attributes.Add("OnClick", "");
                    //=========== Hide the label user name and show the userlist dropdownlist==========//
                    LTRL_SupportUser.Visible = false;
                    DDL_SupportUser.Visible = true;

                    //=========== Assign the userlist to the dropdownlist==========//
                    DDL_SupportUser.DataSource = DS_Dongle.Tables[2];
                    DDL_SupportUser.DataTextField = "LoginID";
                    DDL_SupportUser.DataValueField = "TaskeroUserID";
                    DDL_SupportUser.DataBind();
                    DDL_SupportUser.Items.Insert(0, new ListItem("---Select User---", ""));
                    //========== Remove item support user from the dropdownlist ==========//
                    if (DDL_SupportUser.Items.FindByValue("0") != null)
                        DDL_SupportUser.Items.Remove(DDL_SupportUser.Items.FindByValue("0"));
                }
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void GV_User_And_Dongle_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            //Label LTRL_SupportUser_ID = (Label)e.Item.FindControl("LTRL_SupportUser_ID");
            if (e.CommandName == "SaveUser")
            {
                int _IntUserID = 0;
                Array arrID = CheckGridID(e.Item.ItemIndex);
                //=========== Find the image button of save to get the command argument of the userid ==========//
                ImageButton IMGBTN_Save = (ImageButton)e.Item.FindControl("IMGBTN_Save");
                if (IMGBTN_Save.CommandArgument.ToString().Equals("0"))
                {
                    //=========== find the user dropdownlist ==========//
                    DropDownList DDL_SupportUser = (DropDownList)e.Item.FindControl("DDL_SupportUser");

                    //=========== Check whether the user is selected or not ==========//
                    if (DDL_SupportUser.SelectedIndex == 0)
                    { LblError.Text = "Please select user"; return; }
                    else _IntUserID = Convert.ToInt32(DDL_SupportUser.SelectedValue);

                    //=========== Check whether the dongles are Selected or not for the first time insert ==========//
                    if (arrID.GetValue(0).ToString().Length == 0)
                    { LblError.Text = "Please check dongles."; return; }
                }
                else _IntUserID = Convert.ToInt32(IMGBTN_Save.CommandArgument.ToString());

                //=========== Update user dongle and branch data ==========//
                UpdateUserDongle(_IntUserID, arrID.GetValue(0).ToString(), arrID.GetValue(1).ToString(), LTRL_BranchID.Text, "", LTRL_CompanyID.Text, "");
                //========= Show the confirmation message for the updation =========//
                LblError.Text = "User dongle updated successfully.";
                //=========== Fill the Grid by user list and dongle list ==========//
                FillUserListWithDongle();
            }
            if (e.CommandName == "DeleteUser")
            {
                Array arrID = CheckGridID(e.Item.ItemIndex);

                //=========== Find the image button of delete to get the command argument of the userid ==========//
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");

                //=========== Update user dongle and branch data ==========//
                string Str_All_ID = "";
                if (arrID.GetValue(0).ToString().Length == 0)
                    Str_All_ID = arrID.GetValue(1).ToString();
                else if (arrID.GetValue(1).ToString().Length == 0)
                    Str_All_ID = arrID.GetValue(0).ToString();
                else
                    Str_All_ID = arrID.GetValue(0).ToString() + "," + arrID.GetValue(1).ToString();

                //=========== Update user dongle and branch data ==========//
                UpdateUserDongle(Convert.ToInt32(IMGBTN_Delete.CommandArgument.ToString()), "", Str_All_ID, "", LTRL_BranchID.Text, "", LTRL_CompanyID.Text);

                //========= Show the confirmation message for the updation =========//
                LblError.Text = "User dongle deleted successfully.";

                //=========== Fill the Grid by user list and dongle list ==========//
                FillUserListWithDongle();
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }
    #endregion

    #region Functions & Methods

    #region UserList With Dongle by BranchID
    /// <summary>
    /// Used to get the user list with dongle
    /// </summary>
    private void FillUserListWithDongle()
    {
        try
        {
            //==========  Declaring Objects ==========//
            TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

            //=========== Assign the values to the user object ==========//
            ObjTaskeroUser.RoleID = Convert.ToInt16(Fuji.Common.Sessions.RoleID);
            ObjTaskeroUser.TaskeroUserID = Fuji.Common.Sessions.UserID;
            ObjTaskeroUser.BranchID = "<Branch><BranchID>" + LTRL_BranchID.Text + "</BranchID></Branch>";

            //==========  Fetching the Data From Taskero User ==========//
            DataSet _DSet = ObjTaskeroUser.uspGetSupportUserWithDongleByBranchID(6);

            //=========== bind the user list and dongle to grid ==========//
            if (_DSet != null && _DSet.Tables.Count > 0)
            {
                //=========== Setting the Session with Datatable of Support User And Dongle Data ==========//
                Fuji.Common.Sessions.SetSessions("TaskerSupportUserWithDongle_DataTable", _DSet);

                //=========== Show the Grid ==========//
                GV_User_And_Dongle.Visible = true;

                //=========== Fill the grid with user list ==========//
                GV_User_And_Dongle.DataSource = _DSet;
                GV_User_And_Dongle.DataBind();

                //=========== Show/Hide the pager style of the grid==========//
                if (GV_User_And_Dongle.PageCount == 1)
                    GV_User_And_Dongle.PagerStyle.Visible = false;
                else GV_User_And_Dongle.PagerStyle.Visible = true;
            }
            else
            {
                //=========== hide the Grid ==========//
                GV_User_And_Dongle.Visible = false;

                //=========== fill the grid with null values==========//
                GV_User_And_Dongle.DataSource = null;
                GV_User_And_Dongle.DataBind();
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
    /// <param name="_UserID">Useid from which dongle list to be fetched</param>
    /// <param name="_CHK_Dongle">Checkbox list to be checked with user dongle</param>
    private void GetUserDongleData(int _UserID,CheckBoxList _CHK_Dongle)
    {
        try
        {
            //========== Declare object of the UserDongle data ===========//
            UserDongleData ObjUserDongle = new UserDongleData();

            //========== Assign the values to the user Dongle object ===========//
            ObjUserDongle.TaskeroUserID = _UserID;

            DataTable DtUserDongle = ObjUserDongle.uspGetUserDongleData();

            //========== Check Whether the data is returned or not===========//
            if (DtUserDongle != null && DtUserDongle.Rows.Count > 0)
            {
                for (int i = 0; i < _CHK_Dongle.Items.Count; i++)
                {
                    for (int j = 0; j < DtUserDongle.Rows.Count; j++)
                    {
                        //========= find the value of the dongle =========//
                        int _CPDongleID = Convert.ToInt32(_CHK_Dongle.Items[i].Value);

                        //========= Check that user contains the data with that dongle then check that checkbox =========//
                        if (_CPDongleID == Convert.ToInt32(DtUserDongle.Rows[j]["CPDongleID"].ToString()))
                            _CHK_Dongle.Items[i].Selected = true;
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
            LTRL_Assign_Dongle.Text = Resources.BranchManagement.LBL_AssignUserDongle_Text;
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
    /// <returns>returns the id from the grid</returns>
    private Array CheckGridID(int _Index)
    {
        try
        {
            //========== Declare variable to get id from the grid ===========//
            string STR_Non_Selected = "", STR_Selected = "";
            string[] arrIDs = new string[] { "", "" };
            //========== find check box in the passed grid view for checked value ===========//
            CheckBoxList CHK_Data = (CheckBoxList)GV_User_And_Dongle.Items[_Index].FindControl("CHK_Dongle");
            //========== loop through the grid and find ids===========//
            for (int i = 0; i < CHK_Data.Items.Count; i++)
            {
                //========== Check CheckBoxList Item For Select ==========//
                if (CHK_Data.Items[i].Selected)
                {
                    STR_Selected += Convert.ToInt32(CHK_Data.Items[i].Value) + ",";
                }
                else
                {
                    STR_Non_Selected += Convert.ToInt32(CHK_Data.Items[i].Value) + ",";
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

    #region Update User Dongle ,User Branch And User Company Data
    /// <summary>
    /// Used to update the user company,branch and dongle data
    /// </summary>
    /// <param name="_IntSupportUserID">UserId of the user</param>
    /// <param name="_StrSelDongle">Comma saperated id if the Selected Dongle</param>
    /// <param name="_StrNonSelDongle">Comma saperated id if the NonSelected Dongle</param>
    /// <param name="_StrSelBranch">Comma saperated id if the Selected Branch</param>
    /// <param name="_StrNonSelBranch">Comma saperated id if the NonSelected Branch</param>
    /// <param name="_StrSelCompany">Comma saperated id if the Selected Company</param>
    /// <param name="_StrNonSelCompany">Comma saperated id if the NonSelected Company</param>
    private void UpdateUserDongle(int _IntSupportUserID, string _StrSelDongle, string _StrNonSelDongle, string _StrSelBranch, string _StrNonSelBranch, string _StrSelCompany, string _StrNonSelCompany)
    {
        try
        {
            #region Company Operation
            //========== Declare Company object ===========//
            UserCompanyData ObjUserCompany = new UserCompanyData();

            //========== Assign the value to the user Company object ===========//
            ObjUserCompany.TaskeroUserID = _IntSupportUserID;
            int IntResultCompany = ObjUserCompany.uspInsertUserCompanyData(_StrSelCompany, _StrNonSelCompany);
            #endregion

            #region Branch Operation
            //========== Declare branch object ===========//
            UserBranchData ObjUserBranch = new UserBranchData();

            //========== Assign the value to the user branch object ===========//
            ObjUserBranch.TaskeroUserID = _IntSupportUserID;
            int IntResultBranch = ObjUserBranch.uspInsertUserBranchData(_StrSelBranch, _StrNonSelBranch);
            #endregion

            #region Dongle Operation
            //========== Declare branch object ===========//
            UserDongleData ObjUserDongle = new UserDongleData();

            //========== Assign the value to the user branch object ===========//
            ObjUserDongle.TaskeroUserID = _IntSupportUserID;
            int IntResultDongle = ObjUserDongle.uspInsertUserDongleData(_StrSelDongle, _StrNonSelDongle);
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion            
}
