using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;
using System.Data;
using System.Web.UI.WebControls;


namespace OrisWeb
{
    public partial class CategoryUserManagement : PageBase
    {
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

                    //========== Fill Variables to Form ==========//
                    SetLabels();

                    //=========== Calling the Method for binding the User Grid ==========//
                    FillCompany();

                    GetCategoryUserData();

                    //=========== Calling the Method for Checking User Rights==========//
                    CheckRights();

                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
            finally
            {
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
                Resources.CategoryUserManagement.Culture = (new System.Globalization.CultureInfo(Sessions.Language));

                //========== Set Values ==========//
                LTRL_Assign_CategoryUser.Text = Resources.CategoryUserManagement.LTRL_Assign_CategoryUser_Text;
                LTRL_CompanyName.Text = Resources.CategoryUserManagement.LTRL_CompanyName_Text;
                LTRL_BranchName.Text = Resources.CategoryUserManagement.LTRL_BranchName_Text;
                LTRL_Users.Text = Resources.CategoryUserManagement.LTRL_Users_Text;
                BTN_Save.Text = Resources.CategoryUserManagement.BTN_Save_Text;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        private void GetCategoryUserData()
        {
            try
            {
                CategoryUserData objCategoryUser = new CategoryUserData();
                objCategoryUser.CategoryID = Request.QueryString["CategoryID"] == null ? 0 : int.Parse(Request.QueryString["CategoryID"].ToString());

                DataSet ds = objCategoryUser.uspGetAllCategoryUserByCategoryID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        ListItem item = CHK_Company.Items.FindByValue(ds.Tables[2].Rows[i]["CompanyID"].ToString());
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }

                    FillBranch();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        ListItem item = CHK_Branch.Items.FindByValue(ds.Tables[1].Rows[i]["BranchID"].ToString());
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }

                    FillUsers();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ListItem item = CHK_Users.Items.FindByValue(ds.Tables[0].Rows[i]["TaskeroUserID"].ToString());
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
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
                    CHK_Company.DataTextField = "CompanyName";
                    CHK_Company.DataValueField = "CompanyID";
                    CHK_Company.DataSource = DTCompany;
                    CHK_Company.DataBind();

                    if (CHK_Company.Items.Count == 0)
                    { PNL_Company.Height = 0; PNL_Company.Visible = false; }
                    else if (CHK_Company.Items.Count >= 5)
                    { PNL_Company.Height = 120; PNL_Company.Visible = true; }
                    else if (CHK_Company.Items.Count <= 2)
                    { PNL_Company.Height = 48; PNL_Company.Visible = true; }
                    else
                    { PNL_Company.Height = 75; PNL_Company.Visible = true; }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void CHK_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CHK_Users.Items.Clear();
                if (CHK_Users.Items.Count == 0)
                { PNL_Users.Height = 0; PNL_Users.Visible = false; }
                else if (CHK_Users.Items.Count >= 5)
                { PNL_Users.Height = 120; PNL_Users.Visible = true; }
                else if (CHK_Users.Items.Count <= 2)
                { PNL_Users.Height = 48; PNL_Users.Visible = true; }
                else
                { PNL_Users.Height = 75; PNL_Users.Visible = true; }
                FillBranch();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        protected void CHK_Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillUsers();
            }
            catch (Exception Ex)
            {
                LBL_Error.Text = HandleException(Ex);
            }
        }

        protected void CHK_Users_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void FillBranch()
        {
            try
            {
                CHK_Branch.Items.Clear();
                for (int i = 0; i < CHK_Company.Items.Count; i++)
                {

                    if (CHK_Company.Items[i].Selected == true)
                    {
                        //==========  Declaring Objects ==========//
                        UserBranchData objBranch = new UserBranchData();

                        //==========  Get Data Table and Fill ==========//
                        objBranch.TaskeroUserID = Sessions.UserID;
                        objBranch.CompanyID = Convert.ToInt32(CHK_Company.Items[i].Value);

                        DataTable dtBranch = objBranch.uspGetBranchOfCompanyByUserID(Sessions.RoleID);

                        for (int j = 0; j < dtBranch.Rows.Count; j++)
                        {
                            CHK_Branch.Items.Add(new ListItem(dtBranch.Rows[j]["BranchName"].ToString(), dtBranch.Rows[j]["BranchID"].ToString()));
                        }

                        //CHK_Branch.DataSource = objBranch.uspGetBranchOfCompanyByUserID(Sessions.RoleID);
                        //CHK_Branch.DataTextField = "BranchName";
                        //CHK_Branch.DataValueField = "BranchID";
                        //CHK_Branch.DataBind();
                    }
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
                //CHK_Branch.SelectedIndex = -1;
                //if (Sessions.GetSession("UserMansgementDongle_Data") != null)
                //{
                //    //DataSet DT_DongleData = (DataSet)Sessions.GetSession("UserMansgementDongle_Data");
                //    //CheckDongleData(DT_DongleData.Tables[2], CHK_Branch);
                //}
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }

        private void FillUsers()
        {
            try
            {
                CHK_Users.Items.Clear();
                string BranchIDs = "";
                for (int i = 0; i < CHK_Branch.Items.Count; i++)
                {
                    if (CHK_Branch.Items[i].Selected == true)
                    {
                        if (BranchIDs == "")
                            BranchIDs = BranchIDs + CHK_Branch.Items[i].Value;
                        else
                            BranchIDs = BranchIDs + "," + CHK_Branch.Items[i].Value;
                    }
                }

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
        //=========== Method Used for Checking the rights of the User logged in==========//
        private void CheckRights()
        {
        }

        protected void BTN_Save_Click(object sender, EventArgs e)
        {
            try
            {
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

                CategoryUserData objCategoryUser = new CategoryUserData();
                objCategoryUser.CategoryID = Request.QueryString["CategoryID"] == null ? 0 : int.Parse(Request.QueryString["CategoryID"].ToString());
                objCategoryUser.uspInsertCategoryUser(TaskeroUserIDs);

                LBL_Error.Text = "Category User Permission saved successfully.";
            }
            catch (Exception exc)
            {
                LBL_Error.Text = HandleException(exc);
            }
        }
    }
}