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
using System.Text.RegularExpressions;
using Fuji.Common;
using Fuji.Business;
#endregion

public partial class MyAccount : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //=========== Clear Error ==========//
            LblError.Text = "";

            //=========== Default Settings ==========//
            if (IsPostBack == false)
            {
                ////=========== Check Login ==========//
                Sessions.CheckUserLogin();

                //=========== Check ==========//
                FilteredPassword.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";
                FilteredConformPassword.ValidChars = "~`!@#$%^&*()_+|-=[]{};:\"'.,/<>?\\";

                //========== Fill Variables to Form ==========//
                SetLabels();

                //=========== Filling the User's Data==========//
                FillUserData();

                ////========== Check User Role and redirect to home page as per user role ===========//
                //Fuji.Common.CommonFunctions.CheckUserRole();

                ////=========== Calling the Method for Checking User Rights==========//
                //CheckRights();
            }

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
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
    protected void BTN_Update_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validation Check ==========//            
            if (TXT_FirstName.Text == "") { LblError.Text = Resources.Message.FirstNameIsRequired; return; }
            if (TXT_LastName.Text == "") { LblError.Text = Resources.Message.LastNameIsRequired; return; }
            if (TXT_Email.Text == "") { LblError.Text = Resources.Message.EmailIsRequired; return; }
            else if (!Fuji.Common.Validations.IsEmail(TXT_Email.Text.Trim())) { LblError.Text = Resources.Message.ValidEmailRequired; return; }
            if (TXT_LoginPass.Text.Trim().Length > 0 || TXT_LoginConfirmPassword.Text.Trim().Length > 0)
            {
                if (!CompareValidator.Equals(TXT_LoginPass.Text, TXT_LoginConfirmPassword.Text))
                {
                    LblError.Text = "Mismatch Password & Confirm Password."; return;
                }
            }

            if (Fuji.Common.Sessions.RoleID == 5 && LTRL_CustomerValue.Text == "")
            { LblError.Text = "Customer name is required."; return; }



            //========== Declarations ==========//
            TaskeroUserData objTaskeroUser = new TaskeroUserData();

            //========== Initializing the properties ==========//
            objTaskeroUser.TaskeroUserID = Convert.ToInt32(LTRL_ID.Text);
            objTaskeroUser.FirstName = TXT_FirstName.Text.Trim();
            objTaskeroUser.LastName = TXT_LastName.Text.Trim();
            objTaskeroUser.Email = TXT_Email.Text.Trim();
            objTaskeroUser.LoginPass = TXT_LoginConfirmPassword.Text.Trim();
            objTaskeroUser.LastModified_UserID = Sessions.UserID;
            objTaskeroUser.AllowedIP = LTRL_AllowedIP.Text;

            if (DDL_EmailWhenYellow.SelectedIndex == 0)
            { objTaskeroUser.EmailWhenYellow = true; }
            else
            { objTaskeroUser.EmailWhenYellow = false; }

            if (DDL_EmailWhenRed.SelectedIndex == 0)
            { objTaskeroUser.EmailWhenRed = true; }
            else
            { objTaskeroUser.EmailWhenRed = false; }

            if (Convert.ToInt32(Fuji.Common.Sessions.RoleID.ToString()) == 5)
            {
                //========== Updating Customer Name,Css And Brand image Files ==========//
                objTaskeroUser.Customer = LTRL_CustomerValue.Text.ToString();
            }

            //========== Updating MyAccount Taskero User ==========//
            int _Result = objTaskeroUser.uspUpdateMyAccountTaskeroUser();
            
            //==========  Checking for the Flag ==========//
            if (_Result == 2)
            {
                LblError.Text = "Record Updated successfully.";
                Fuji.Common.Sessions.FirstName = TXT_FirstName.Text.Trim();
                Fuji.Common.Sessions.LastName = TXT_LastName.Text.Trim();
                Fuji.Common.Sessions.Email = TXT_Email.Text.Trim();
                //Fuji.Common.CommonFunctions.PageRedirect("MyAccount","");
                Fuji.Common.Sessions.SetSessions("strCSSFile", null);
                Fuji.Common.Sessions.SetSessions("strHeaderFile", null);
                Fuji.Common.Sessions.SetSessions("strLargeFile", null);
                Page_Load(null, null);
                LblError.Text = "Record Updated successfully.";
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
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
            //========== Set Culture ==========//
            Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
            Resources.UserManagement.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Title.Text = Resources.MyAccount.LBL_Title_Text;
            LTRL_SubTitle.Text = Resources.MyAccount.LBL_SubTitle_Text;
            LTRL_SubTitle1.Text = Resources.MyAccount.LBL_SubTitle2_Text;
            LTRL_CompanyName.Text = Resources.MyAccount.LBL_CompanyName_Text;
            LTRL_BranchName.Text = Resources.MyAccount.LBL_BranchName_Text;
            LTRL_DongleName.Text = Resources.MyAccount.LBL_DongleName_Text;
            LTRL_FirstName.Text = Resources.MyAccount.LBL_FirstName_Text;
            LTRL_LastName.Text = Resources.MyAccount.LBL_LastName_Text;
            LTRL_LoginID.Text = Resources.MyAccount.LBL_LoginID_Text;
            LTRL_LoginPass.Text = Resources.MyAccount.LBL_LoginPass_Text;
            LTRL_LoginConfirmPassword.Text = Resources.MyAccount.LBL_LoginConfirmPassword_Text;
            LTRL_Email.Text = Resources.MyAccount.LBL_Email_Text;
            LTRL_Active.Text = Resources.MyAccount.LBL_Active_Text;
            LTRL_RoleAndPermission.Text = Resources.MyAccount.LBL_RoleAndPermission_Text;
            LTRL_PrintBuyerInfo.Text = Resources.MyAccount.LBL_PrintBuyerInfo_Text;
            LTRL_Customer.Text = Resources.MyAccount.LBL_Customer_Text;
            LTRL_BrandInfo.Text = Resources.MyAccount.LBL_BrandInfo_Text;
            BTN_Update.Text = Resources.MyAccount.BTN_Update_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
    //=========== Method User for Filling the User's Data==========//
    private void FillUserData()
    {
        try
        {
            //==========  Declaring Objects ==========//
            TaskeroUserData ObjTaskeroUser = new TaskeroUserData();

            ObjTaskeroUser.TaskeroUserID = Fuji.Common.Sessions.UserID;

            //==========  Fetching the Data From Taskero User ==========//
            DataSet _DSTaskero = ObjTaskeroUser.uspGetTaskeroUserAndDetails();
            if (_DSTaskero != null)
            {
                if (_DSTaskero.Tables.Count > 0 && _DSTaskero.Tables[0].Rows.Count > 0)
                {
                    #region Fill User General Information
                    //========== Fill User General Information ==========//
                    LTRL_ID.Text = Sessions.UserID.ToString();
                    TXT_FirstName.Text = _DSTaskero.Tables[0].Rows[0]["FirstName"].ToString();
                    TXT_LastName.Text = _DSTaskero.Tables[0].Rows[0]["LastName"].ToString();
                    TXT_Email.Text = _DSTaskero.Tables[0].Rows[0]["Email"].ToString();
                    LTRL_LoginIDDisplay.Text = _DSTaskero.Tables[0].Rows[0]["LoginID"].ToString();
                    TXT_LoginPass.Attributes.Add("value", _DSTaskero.Tables[0].Rows[0]["LoginPass"].ToString());
                    TXT_LoginConfirmPassword.Attributes.Add("value", _DSTaskero.Tables[0].Rows[0]["LoginPass"].ToString());
                    LTRL_ActiveDisplay.Text = _DSTaskero.Tables[0].Rows[0]["IsActive"].ToString();

                    if (_DSTaskero.Tables[0].Rows[0]["EmailWhenYellow"].ToString() == "False")
                        DDL_EmailWhenYellow.SelectedValue = "0";
                    else
                        DDL_EmailWhenYellow.SelectedValue = "1";

                    if (_DSTaskero.Tables[0].Rows[0]["EmailWhenRed"].ToString() == "False")
                        DDL_EmailWhenRed.SelectedValue = "0";
                    else
                        DDL_EmailWhenRed.SelectedValue = "1";


                    if (Convert.ToInt32(_DSTaskero.Tables[0].Rows[0]["RoleID"].ToString()) == 5)
                    {
                        FLD_PrintBuyerInfo.Visible = true;
                        LTRL_CustomerValue.Text = _DSTaskero.Tables[0].Rows[0]["Customer"].ToString();
                        LTRL_BrandName.Text = _DSTaskero.Tables[0].Rows[0]["BrandName"].ToString();
                    }

                    #endregion

                    #region Display Company Data
                    if (Fuji.Common.Sessions.RoleID == 1)
                        LTRL_CompanyNameDisplay.Text = "All";
                    else
                    {
                        if (_DSTaskero.Tables.Count > 1 && _DSTaskero.Tables[1].Rows.Count > 0)
                        {
                            LTRL_CompanyNameDisplay.Text = "";
                            for (int i = 0; i < _DSTaskero.Tables[1].Rows.Count; i++)
                            {
                                if (LTRL_CompanyNameDisplay.Text.Trim().Length > 0)
                                    LTRL_CompanyNameDisplay.Text = LTRL_CompanyNameDisplay.Text + "</br> " + (i + 1).ToString() + ". " + _DSTaskero.Tables[1].Rows[i]["CompanyName"].ToString();
                                else
                                    LTRL_CompanyNameDisplay.Text = LTRL_CompanyNameDisplay.Text + (i + 1).ToString() + ". " + _DSTaskero.Tables[1].Rows[i]["CompanyName"].ToString();
                            }
                        }
                    }
                    #endregion

                    #region Users Branch Data
                    if (Fuji.Common.Sessions.RoleID == 1)
                        LTRL_BranchNameDisplay.Text = "All";
                    else
                    {
                        if (_DSTaskero.Tables.Count > 2 && _DSTaskero.Tables[2].Rows.Count > 0)
                        {
                            LTRL_BranchNameDisplay.Text = "";
                            for (int i = 0; i < _DSTaskero.Tables[2].Rows.Count; i++)
                            {
                                if (LTRL_BranchNameDisplay.Text.Trim().Length > 0)
                                    LTRL_BranchNameDisplay.Text = LTRL_BranchNameDisplay.Text + "</br> " + (i + 1).ToString() + ". " + _DSTaskero.Tables[2].Rows[i]["BranchName"].ToString();
                                else
                                    LTRL_BranchNameDisplay.Text = LTRL_BranchNameDisplay.Text + (i + 1).ToString() + ". " + _DSTaskero.Tables[2].Rows[i]["BranchName"].ToString();
                            }
                        }
                    }
                    #endregion

                    #region Users Dongle Data
                    //========== User Dongle Data ==========//
                    if (Fuji.Common.Sessions.RoleID == 1)
                        LTRL_DongleNameDisplay.Text = "All";
                    else
                    {
                        if (_DSTaskero.Tables.Count > 3 && _DSTaskero.Tables[3].Rows.Count > 0)
                        {
                            LTRL_DongleNameDisplay.Text = "";
                            for (int i = 0; i < _DSTaskero.Tables[3].Rows.Count; i++)
                            {
                                if (LTRL_DongleNameDisplay.Text.Trim().Length > 0)
                                    LTRL_DongleNameDisplay.Text = LTRL_DongleNameDisplay.Text + "</br> " + (i + 1).ToString() + ". " + _DSTaskero.Tables[3].Rows[i]["DongleSerial"].ToString();
                                else
                                    LTRL_DongleNameDisplay.Text = (i + 1).ToString() + ". " + LTRL_DongleNameDisplay.Text + _DSTaskero.Tables[3].Rows[i]["DongleSerial"].ToString();
                            }
                        }
                    }
                    #endregion

                    #region Permission
                    if (Fuji.Common.Sessions.RoleID == 1)
                        LTRL_RoleAndPermissionDisplay.Text = "All";
                    else
                    {
                        if (_DSTaskero.Tables.Count > 4 && _DSTaskero.Tables[4].Rows.Count > 0)
                        {
                            LTRL_RoleAndPermissionDisplay.Text = "";
                            for (int i = 0; i < _DSTaskero.Tables[4].Rows.Count; i++)
                            {
                                if (LTRL_RoleAndPermissionDisplay.Text.Trim().Length > 0)
                                    LTRL_RoleAndPermissionDisplay.Text = LTRL_RoleAndPermissionDisplay.Text + "<br> " + (i + 1).ToString() + ". " + _DSTaskero.Tables[4].Rows[i]["PermissionDesc"].ToString();
                                else
                                    LTRL_RoleAndPermissionDisplay.Text = (i + 1).ToString() + ". " + LTRL_RoleAndPermissionDisplay.Text + _DSTaskero.Tables[4].Rows[i]["PermissionDesc"].ToString();
                            }
                        }
                    }
                    #endregion

                    #region IP Address
                    LTRL_AllowedIP.Text = _DSTaskero.Tables[0].Rows[0]["AllowedIP"].ToString();
                    #endregion

                    FillReports();
                }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void FillReports()
    {
        try
        {
            //========== Declarations ==========//
            ReportData objReport = new ReportData();

            //========== Get Data ==========//
            DataTable DT = objReport.uspGetAllReportsOnTaskeroUserID(Sessions.UserID);

            //========== Bind ==========//
            RPT_Reports.DataSource = DT;
            RPT_Reports.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
