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
#endregion

#region Others
using Fuji.Common;
using Fuji.Business;
#endregion

public partial class Login : PageBase
{
    #region Description
    //<summary>
    //This page is for the login purpose!
    //</summary>
    #endregion

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Errors ==========//
            LblError1.Text = "";


            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Clear The Session ==========//
                Sessions.ClearLoginSession();
                VisibleLoginForgotPassword(Div_Login);

            }
        }
        catch (Exception Exc) { LblError1.Text = HandleException(Exc); }
    }

    protected void IBtnLogin_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //========== Validations ==========//
            if (TXT_LoginUserName.Text == "") { LblError1.Text = "Username is required."; return; }
            if (TXT_LoginPassword.Text == "") { LblError1.Text = "Password is required."; return; }

            //========== Declarations ==========//
            TaskeroUserData objTaskeroUser = new TaskeroUserData();

            //========== Check the Login ==========//
            objTaskeroUser.LoginID = TXT_LoginUserName.Text;
            objTaskeroUser.LoginPass = TXT_LoginPassword.Text;
            objTaskeroUser.AllowedIP = Request.UserHostAddress;
            DataSet DSet = objTaskeroUser.uspGetCheckLogin();

            //========== Value ==========//
            if (DSet.Tables[0] != null && DSet.Tables[0].Rows.Count == 0)
            {
                LblError1.Text = "Invalid username entered.";
            }
            else
            {
                //========== Fill To Variables ==========//
                string Username = DSet.Tables[0].Rows[0]["LoginID"].ToString();
                string Password = DSet.Tables[0].Rows[0]["LoginPass"].ToString();

                //========== Check The password ==========//
                if (TXT_LoginPassword.Text != Password) { LblError1.Text = "Invalid password entered."; return; }
                if (Convert.ToInt32(DSet.Tables[0].Rows[0]["IPAccess"]) == 0) { LblError1.Text = "IP restricted for this login."; return; }

                //========== Fill The Session ==========//
                Sessions.SetLoginSessions(DSet);

                #region Insert User Login Info
                //========== Declare object of the UserLog ===========//
                UserLogData ObjUserLog = new UserLogData();
                //========== Assign the user log information to object of the user log ===========//
                ObjUserLog.TaskeroUserID = Fuji.Common.Sessions.UserID;
                ObjUserLog.IPAddress = Request.UserHostAddress;
                ObjUserLog.uspInsertUserLog();
                #endregion


                //========== Send to Next Page ==========//
                Fuji.Common.CommonFunctions.PageRedirect("Home", "");
            }
        }
        catch (Exception Exc) { LblError1.Text = HandleException(Exc); }
    }

    protected void IbtnForgotPassword_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //========== Validations ==========//
            if (TxtForgotPasswordUserName.Text == "") { LblError.Text = "Username is required."; return; }
            if (TxtForgotPasswordEmail.Text == "")
            {
                LblError.Text = "Email is required."; return;
            }
            else if (Fuji.Common.Validations.IsEmail(TxtForgotPasswordEmail.Text) == false)
            {
                LblError.Text = "Enter valid email.";
                return;
            }
           

            //========== Declarations ==========//
            TaskeroUserData objTaskeroUser = new TaskeroUserData();

            //========== Check the Login ==========//
            objTaskeroUser.LoginID = TxtForgotPasswordUserName.Text;
            objTaskeroUser.Email = TxtForgotPasswordEmail.Text;
            DataTable DT = objTaskeroUser.uspGetRetrievePassword();

            //========== Send and Email ==========//
            if (DT != null && DT.Rows.Count == 0)
            {
                LblError.Text = "Invalid information.";
                return;
            }
            else
            {
                //========== Mail Sent ==========//
                Fuji.Common.WebMails.SendRetrievePassword(TxtForgotPasswordEmail.Text, TxtForgotPasswordUserName.Text, DT.Rows[0]["LoginPass"].ToString(), DT.Rows[0]["FirstName"].ToString() + " " + DT.Rows[0]["LastName"].ToString());

                //========== Mail Sent ==========//
                LblError.Text = "Email sent to on " + TxtForgotPasswordEmail.Text+".";
            }
        }
        catch (Exception Exc) { LblError.Text = LblError1.Text = HandleException(Exc); }        
    }

    protected void LbtnForgotPassword_Click(object sender, EventArgs e)
    {
        try
        {
            VisibleLoginForgotPassword(Div_ForgotPassword);
        }
        catch (Exception Exc) { LblError.Text = LblError1.Text = HandleException(Exc); }
    }

    protected void LbtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            VisibleLoginForgotPassword(Div_Login);
            
        }
        catch (Exception Exc) { LblError.Text = LblError1.Text = HandleException(Exc); }
    }
    
    #endregion

    #region Methods
    private void VisibleLoginForgotPassword(HtmlGenericControl DIV)
    {
        Div_Login.Visible = false;
        Div_ForgotPassword.Visible = false;

        DIV.Visible = true;
    }
    #endregion
}
