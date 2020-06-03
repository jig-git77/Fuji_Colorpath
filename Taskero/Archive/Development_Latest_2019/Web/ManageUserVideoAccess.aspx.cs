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

public partial class ManageUserVideoAccess : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //=========== Clear the Error message ==========//
            LblError.Text = "";

            //=========== Set the grid default stylesheet==========//
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(GV_User_And_Categories);

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
                    Fuji.Common.Sessions.SetSessions("CategoryUser_DataTable", null);

                    //=========== Bing the branch id to the label ==========//
                    LTRL_BranchID.Text = Request.QueryString["BranchID"].ToString().Trim();

                    //=========== Fill the Grid by user list and dongle list ==========//
                    FillUserListWithCategory();
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

    protected void GV_User_And_Categories_RowDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            //=========== Check the item or alternating item of the grid ==========//
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //=========== Find the label,Dropdownlist and checkbox list from grid ==========//
                Literal LTRL_UserCategory = (Literal)e.Item.FindControl("LTRL_UserCategory");
                Literal LTRL_UserCategory_ID = (Literal)e.Item.FindControl("LTRL_UserCategory_ID");
                //DropDownList DDL_Categories = (DropDownList)e.Item.FindControl("DDL_Categories");
                RadComboBox DDL_Categories = (RadComboBox)e.Item.FindControl("DDL_Categories");
                CheckBoxList CHK_Users = (CheckBoxList)e.Item.FindControl("CHK_Users");
                Panel PNL_Users = (Panel)e.Item.FindControl("PNL_Users");
                ImageButton IMGBTN_Save = (ImageButton)e.Item.FindControl("IMGBTN_Save");
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");
                IMGBTN_Save.CommandArgument = ((DataRowView)e.Item.DataItem)["CategoryID"].ToString();
                IMGBTN_Delete.CommandArgument = ((DataRowView)e.Item.DataItem)["CategoryID"].ToString();
                DataSet DS_Dongle = new DataSet();

                LTRL_UserCategory.Text = ((DataRowView)e.Item.DataItem)["Description"].ToString();
                LTRL_UserCategory_ID.Text = ((DataRowView)e.Item.DataItem)["CategoryID"].ToString();

                //=========== Check whether the dongle list session is null or not ==========//
                if (Fuji.Common.Sessions.GetSession("CategoryUser_DataTable") != null)
                {
                    //=========== Get the dongle list from the session==========//
                    DS_Dongle = (DataSet)Fuji.Common.Sessions.GetSession("CategoryUser_DataTable");
                }

                FillUsers(CHK_Users, PNL_Users);
                
                ////=========== Check whether the dongle list session is null or not ==========//
                //if (DS_Dongle != null && DS_Dongle.Tables[1].Rows.Count > 0)
                //{
                //    //=========== Assign the dongle list to the dropdownlist==========//
                //    CHK_Users.DataSource = DS_Dongle.Tables[1];
                //    CHK_Users.DataTextField = "DongleSerial";
                //    CHK_Users.DataValueField = "CPDongleID";
                //    CHK_Users.DataBind();
                //    //=========== Check whether the user has rights to save dongles==========//
                //    if (Fuji.Common.Sessions.Add_Support_User == true)
                //        IMGBTN_Save.Visible = true;
                //    else IMGBTN_Save.Visible = true;
                //    //=========== Assign the panel height from the dongle list ==========//
                //    if (CHK_Users.Items.Count == 0)
                //    { PNL_Users.Height = 0; PNL_Users.Visible = false; }
                //    else if (CHK_Users.Items.Count <= 2)
                //    { PNL_Users.Height = 80; PNL_Users.Visible = true; }
                //    else
                //    { PNL_Users.Height = 105; PNL_Users.Visible = true; }
                //    PNL_Users.Attributes.Add("width", "150px");
                //}
                //else { IMGBTN_Save.Visible = false; PNL_Users.Height = 0; PNL_Users.Visible = false; }

                //=========== Check Whether The TaskeroUserID is zero then bind dropdownlist with userlist ==========//
                if (((DataRowView)e.Item.DataItem)["CategoryID"].ToString() != "0")
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
                    DDL_Categories.Visible = false;
                    LTRL_UserCategory.Visible = true;
                    LTRL_UserCategory.Text = ((DataRowView)e.Item.DataItem)["Description"].ToString();
                    //=========== Set the user dongle list the dongle list of the branch ==========//
                    GetCategoryUserData(Convert.ToInt32(((DataRowView)e.Item.DataItem)["CategoryID"].ToString()), CHK_Users);
                }
                else
                {
                    //=========== Hide the delete button if there is user with 0 ID find==========//
                    IMGBTN_Delete.Visible = false;
                    IMGBTN_Delete.Attributes.Add("OnClick", "");
                    //=========== Hide the label user name and show the userlist dropdownlist==========//
                    LTRL_UserCategory.Visible = false;
                    DDL_Categories.Visible = true;

                    //=========== Assign the userlist to the dropdownlist==========//
                    //DDL_Categories.DataSource = DS_Dongle.Tables[2];
                    //DDL_Categories.DataTextField = "LoginID";
                    //DDL_Categories.DataValueField = "TaskeroUserID";
                    //DDL_Categories.DataBind();
                    BindVideoCategoriesDropDown(DDL_Categories);

                    //DDL_Categories.Items.Insert(0, new ListItem("---Select Category---", ""));
                    DDL_Categories.Items.Insert(0, new RadComboBoxItem("---Select Category---", ""));
                    //========== Remove item support user from the dropdownlist ==========//
                    if (DDL_Categories.FindItemByValue("0") != null)
                        DDL_Categories.Items.Remove(DDL_Categories.FindItemByValue("0"));
                    //if (DDL_Categories.Items.FindByValue("0") != null)
                    //    DDL_Categories.Items.Remove(DDL_Categories.Items.FindByValue("0"));
                }
            }
        }
        catch (Exception exc)
        {
            LblError.Text = HandleException(exc);
        }
    }

    protected void GV_User_And_Categories_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            //Label LTRL_UserCategory_ID = (Label)e.Item.FindControl("LTRL_UserCategory_ID");
            if (e.CommandName == "SaveUserCategory")
            {
                int _IntCategoryID = 0;
                //Array arrID = CheckGridID(e.Item.ItemIndex);
                //=========== Find the image button of save to get the command argument of the userid ==========//
                ImageButton IMGBTN_Save = (ImageButton)e.Item.FindControl("IMGBTN_Save");
                CheckBoxList CHK_Users = (CheckBoxList)e.Item.FindControl("CHK_Users");
                string TaskeroUserIDs = "";
                for (int i = 0; i < CHK_Users.Items.Count; i++)
                {
                    if (CHK_Users.Items[i].Selected == true)
                    {
                        if (TaskeroUserIDs == "")
                            TaskeroUserIDs = TaskeroUserIDs + CHK_Users.Items[i].Value;
                        else
                            TaskeroUserIDs = TaskeroUserIDs + "," + CHK_Users.Items[i].Value;
                    }
                }
                

                if (IMGBTN_Save.CommandArgument.ToString().Equals("0"))
                {
                    //=========== find the user dropdownlist ==========//
                    RadComboBox DDL_Categories = (RadComboBox)e.Item.FindControl("DDL_Categories");

                    //=========== Check whether the user is selected or not ==========//
                    if (DDL_Categories.SelectedIndex == 0)
                    { LblError.Text = "Please select category"; return; }
                    else _IntCategoryID = Convert.ToInt32(DDL_Categories.SelectedValue);

                    ////=========== Check whether the dongles are Selected or not for the first time insert ==========//
                    //if (arrID.GetValue(0).ToString().Length == 0)
                    //{ LblError.Text = "Please check users."; return; }
                }
                else 
                    _IntCategoryID = Convert.ToInt32(IMGBTN_Save.CommandArgument.ToString());

                //=========== Check whether the dongles are Selected or not for the first time insert ==========//
                if (TaskeroUserIDs == "")
                { LblError.Text = "Please check users."; return; }

                CategoryUserData objCategoryUser = new CategoryUserData();
                objCategoryUser.CategoryID = _IntCategoryID;
                objCategoryUser.uspInsertCategoryBranchUser(TaskeroUserIDs,int.Parse(Request.QueryString["BranchID"].ToString()));

                //=========== Update user dongle and branch data ==========//
                //UpdateUserDongle(_IntUserID, arrID.GetValue(0).ToString(), arrID.GetValue(1).ToString(), LTRL_BranchID.Text, "", LTRL_CompanyID.Text, "");
                //========= Show the confirmation message for the updation =========//
                LblError.Text = "Category permission to user updated successfully.";
                //=========== Fill the Grid by user list and dongle list ==========//
                FillUserListWithCategory();
            }
            if (e.CommandName == "DeleteUserCategory")
            {
                //Array arrID = CheckGridID(e.Item.ItemIndex);

                //=========== Find the image button of delete to get the command argument of the userid ==========//
                ImageButton IMGBTN_Delete = (ImageButton)e.Item.FindControl("IMGBTN_Delete");

                ////=========== Update user dongle and branch data ==========//
                //string Str_All_ID = "";
                //if (arrID.GetValue(0).ToString().Length == 0)
                //    Str_All_ID = arrID.GetValue(1).ToString();
                //else if (arrID.GetValue(1).ToString().Length == 0)
                //    Str_All_ID = arrID.GetValue(0).ToString();
                //else
                //    Str_All_ID = arrID.GetValue(0).ToString() + "," + arrID.GetValue(1).ToString();

                CategoryUserData objCategoryUser = new CategoryUserData();
                objCategoryUser.CategoryID = Convert.ToInt32(IMGBTN_Delete.CommandArgument.ToString());
                objCategoryUser.uspDeleteCategoryBranchUser(int.Parse(Request.QueryString["BranchID"].ToString()));

                //=========== Update user dongle and branch data ==========//
                //UpdateUserDongle(Convert.ToInt32(IMGBTN_Delete.CommandArgument.ToString()), "", Str_All_ID, "", LTRL_BranchID.Text, "", LTRL_CompanyID.Text);

                //========= Show the confirmation message for the updation =========//
                LblError.Text = "Category permission to user deleted successfully.";

                //=========== Fill the Grid by user list and dongle list ==========//
                FillUserListWithCategory();
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
    private void FillUserListWithCategory()
    {
        try
        {
            //==========  Declaring Objects ==========//
            CategoryUserData objCategoryUser = new CategoryUserData();

            //==========  Fetching the Data From Taskero User ==========//
            DataSet _DSet = objCategoryUser.uspGetAllCategoryUserByBranchID(int.Parse(Request.QueryString["BranchID"].ToString()));

            //=========== bind the user list and dongle to grid ==========//
            if (_DSet != null && _DSet.Tables.Count > 0)
            {
                //=========== Setting the Session with Datatable of Support User And Dongle Data ==========//
                Fuji.Common.Sessions.SetSessions("CategoryUser_DataTable", _DSet);

                //if (_DSet.Tables[0].Rows.Count == 0)
                //{
                DataRow dr = _DSet.Tables[0].NewRow();
                dr["CategoryID"] = 0;
                dr["Description"] = "";
                _DSet.Tables[0].Rows.Add(dr);
                //}

                //=========== Show the Grid ==========//
                GV_User_And_Categories.Visible = true;

                //=========== Fill the grid with user list ==========//
                GV_User_And_Categories.DataSource = _DSet;
                GV_User_And_Categories.DataBind();

                //=========== Show/Hide the pager style of the grid==========//
                if (GV_User_And_Categories.PageCount == 1)
                    GV_User_And_Categories.PagerStyle.Visible = false;
                else GV_User_And_Categories.PagerStyle.Visible = true;
            }
            else
            {
                //=========== hide the Grid ==========//
                GV_User_And_Categories.Visible = false;

                //=========== fill the grid with null values==========//
                GV_User_And_Categories.DataSource = null;
                GV_User_And_Categories.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillUsers(CheckBoxList CHK_Users, Panel PNL_Users)
    {
        try
        {
            CHK_Users.Items.Clear();
            string BranchIDs = "";
            BranchIDs = Request.QueryString["BranchID"].ToString();

            CategoryUserData objCategoryUser = new CategoryUserData();
            DataTable dt = objCategoryUser.uspGetUserOfBranchID(BranchIDs, Sessions.UserID);

            CHK_Users.DataTextField = "LoginID";
            CHK_Users.DataValueField = "TaskeroUserID";
            CHK_Users.DataSource = dt;
            CHK_Users.DataBind();


            if (CHK_Users.Items.Count == 0)
            { PNL_Users.Height = 0; PNL_Users.Visible = false; }
            else if (CHK_Users.Items.Count >= 5)
            { PNL_Users.Height = 120; PNL_Users.Visible = true; }
            else if (CHK_Users.Items.Count <= 2)
            { PNL_Users.Height = 48; PNL_Users.Visible = true; }
            else
            { PNL_Users.Height = 75; PNL_Users.Visible = true; }

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region Get Categiry User Data
    /// <summary>
    /// Used to get the category user data
    /// </summary>
    private void GetCategoryUserData(int _CategoryID, CheckBoxList _CHK_Users)
    {
        try
        {
            //==========  Declaring Objects ==========//
            CategoryUserData objCategoryUser = new CategoryUserData();

            //==========  Fetching the Data From Taskero User ==========//
            DataSet _DSet = objCategoryUser.uspGetAllCategoryUserByBranchID(int.Parse(Request.QueryString["BranchID"].ToString()));

            ////========== Declare object of the UserDongle data ===========//
            //UserDongleData ObjUserDongle = new UserDongleData();

            ////========== Assign the values to the user Dongle object ===========//
            //ObjUserDongle.TaskeroUserID = _UserID;

            //DataTable DtUserDongle = ObjUserDongle.uspGetUserDongleData();

            //========== Check Whether the data is returned or not===========//
            DataView dv = _DSet.Tables[1].DefaultView;
            dv.RowFilter = " CategoryID = " + _CategoryID;

            if (dv.Count > 0)
            {
                for (int i = 0; i < _CHK_Users.Items.Count; i++)
                {
                    for (int j = 0; j < dv.Count; j++)
                    {
                        //========= find the value of the dongle =========//
                        int _UserID = Convert.ToInt32(_CHK_Users.Items[i].Value);

                        //========= Check that user contains the data with that dongle then check that checkbox =========//
                        if (_UserID == Convert.ToInt32(dv[j]["TaskeroUserID"].ToString()))
                            _CHK_Users.Items[i].Selected = true;
                    }
                }
            }

            //if (_DSet != null && _DSet.Tables[1].Rows.Count > 0)
            //{
            //    for (int i = 0; i < _CHK_Users.Items.Count; i++)
            //    {
            //        for (int j = 0; j < _DSet.Tables[1].Rows.Count; j++)
            //        {
            //            //========= find the value of the dongle =========//
            //            int _UserID= Convert.ToInt32(_CHK_Users.Items[i].Value);

            //            //========= Check that user contains the data with that dongle then check that checkbox =========//
            //            if (_UserID == Convert.ToInt32(_DSet.Tables[1].Rows[j]["TaskeroUserID"].ToString()))
            //                _CHK_Users.Items[i].Selected = true;
            //        }
            //    }
            //}
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
            Resources.ManageUserVideoAccess.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values For Header of the User Branch and Dongle ==========//
            LTRL_Assign_VideoAccess.Text = Resources.ManageUserVideoAccess.LTRL_Assign_VideoAccess_Text;
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
            CheckBoxList CHK_Data = (CheckBoxList)GV_User_And_Categories.Items[_Index].FindControl("CHK_Users");
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

    #endregion            

    private void BindVideoCategoriesDropDown(RadComboBox DDL_Categories)
    {
        try
        {
            DDL_Categories.Items.Clear();
            RadComboBoxItem item = new Telerik.WebControls.RadComboBoxItem();
            item.Text = "Select One";
            item.Value = "0";
            DDL_Categories.Items.Add(item);

            CategoriesData objCategory = new CategoriesData();
            DataTable DT_Categories = objCategory.uspGetAllCategories();
            DataView dv = DT_Categories.DefaultView;
            DataTable DT_CategoriesCopy = DT_Categories.Copy();
            dv.RowFilter = "ParentID = 0";
            for (int i = 0; i < dv.Count; i++)
            {
                item = new Telerik.WebControls.RadComboBoxItem();
                DataView dvCopy = DT_CategoriesCopy.DefaultView;
                dvCopy.RowFilter = "ParentID = " + int.Parse(dv[i]["CategoryID"].ToString());
                if (dvCopy.Count > 0)
                {
                    item.Enabled = false;
                    item.Font.Bold = true;
                    item.Font.Italic = true;
                }
                else
                {
                    item.Enabled = true;
                    item.Font.Bold = false;
                    item.Font.Italic = false;
                }
                item.Text = dv[i]["Description"].ToString();
                item.Value = dv[i]["CategoryID"].ToString();
                DDL_Categories.Items.Add(item);
                dvCopy.RowFilter = "";
                AddChildDataToVideoDropDown(DT_CategoriesCopy, int.Parse(dv[i]["CategoryID"].ToString()), 0, DDL_Categories);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void AddChildDataToVideoDropDown(DataTable DT_Categories, int CategoryID, int intSpaces, RadComboBox DDL_Categories)
    {
        try
        {
            string strDescription = "";
            string strCategoryID = "";
            DataTable DT_CategoriesCopy = DT_Categories.Copy();
            DataView dv = DT_Categories.DefaultView;
            dv.RowFilter = "ParentID = " + CategoryID;
            for (int i = 0; i < dv.Count; i++)
            {
                strDescription = dv[i]["Description"].ToString();
                strCategoryID = dv[i]["CategoryID"].ToString();
                RadComboBoxItem item = new Telerik.WebControls.RadComboBoxItem();
                DataView dvCopy = DT_CategoriesCopy.DefaultView;
                //if (int.Parse(dv[i]["CountVideos"].ToString()) > 0)
                //{
                //    item.Enabled = false;
                //    item.Font.Bold = true;
                //    item.Font.Italic = true;
                //}
                //else
                //{
                //    item.Enabled = true;
                //    item.Font.Bold = false;
                //    item.Font.Italic = false;
                //}
                dvCopy.RowFilter = "ParentID = " + int.Parse(dv[i]["CategoryID"].ToString());
                if (dvCopy.Count > 0)
                {
                    item.Enabled = false;
                    item.Font.Bold = true;
                    item.Font.Italic = true;
                }
                else
                {
                    item.Enabled = true;
                    item.Font.Bold = false;
                    item.Font.Italic = false;
                }
                //intSpaces = intSpaces + 10;
                item.Style.Add("padding-left", (intSpaces + 20).ToString() + "px");
                item.Text = dv[i]["Description"].ToString();
                item.Value = dv[i]["CategoryID"].ToString();
                DDL_Categories.Items.Add(item);
                dvCopy.RowFilter = "";
                AddChildDataToVideoDropDown(DT_CategoriesCopy, int.Parse(dv[i]["CategoryID"].ToString()), intSpaces + 20, DDL_Categories);
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}
