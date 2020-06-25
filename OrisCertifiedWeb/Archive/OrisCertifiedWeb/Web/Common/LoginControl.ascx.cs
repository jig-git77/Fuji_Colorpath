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

public partial class Common_LoginControl : ControlBase
{
    #region Description
    //<summary>
    //This page is for the login and forgot password purpose!
    //</summary>
    #endregion

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Errors ==========//
            LblError1.Text = LblError2.Text = "";

            //========== Set Function call on pressing enter on login and forgot process  ===========//
            SetFunctionCallOnEnter();

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Default Settings For the LoginUserName Input Box ==========//
                SetFocus(TXT_LoginUserName);

                //========== Default Settings For the LoginUserName Input Box ==========//
                if ((Request.Cookies["FujiLogin"] == null || Request.Cookies["FujiLogin"].Value == "") && (Request.Cookies["FujiPass"] == null || Request.Cookies["FujiPass"].Value == ""))
                { CHK_RememberMe.Checked = false; }
                else
                {
                    CHK_RememberMe.Checked = true;
                    TXT_LoginUserName.Text = Request.Cookies["FujiLogin"].Value;
                    TXT_LoginPassword.Attributes.Add("value", Request.Cookies["FujiPass"].Value);
                }

                #region Check and Set Session
                //========== Validations ===========//
                if (Request.QueryString["Language"] == null || Request.QueryString["Language"].ToString() == "")
                { }
                else
                {
                    //========== Sets New Sessions ===========//
                    Fuji.Common.Sessions.Language = Request.QueryString["Language"];
                }
                #endregion

                //========== Clear The Session ==========//
                Sessions.ClearLoginSession();

                //========== Fill Variables to Form ==========//
                SetLabels();

                //========== Show Hide Tables ==========//
                TBL_Login.Visible = true;
                TBL_ForgotPassword.Visible = false;
            }
        }
        catch (Exception Exc) { LblError2.Text = LblError1.Text = HandleException(Exc); }
    }
    protected void LBTN_Login1_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validations ==========//
            if (TXT_LoginUserName.Text == "") { LblError1.Text = Resources.Message.UsernameIsRequired; SetFocus(TXT_LoginUserName); return; }
            if (TXT_LoginPassword.Text == "") { LblError1.Text = Resources.Message.PasswordIsRequired; SetFocus(TXT_LoginPassword); return; }

            //========== Declarations ==========//
            TaskeroUserData objTaskeroUser = new TaskeroUserData();

            //========== Check the Login ==========//
            objTaskeroUser.LoginID = TXT_LoginUserName.Text;
            objTaskeroUser.LoginPass = TXT_LoginPassword.Text;
            objTaskeroUser.AllowedIP = Request.UserHostAddress;
            DataSet DSet = objTaskeroUser.uspGetCheckLogin();

            //========== Value ==========//
            if (DSet.Tables[0]!= null && DSet.Tables[0].Rows.Count == 0)
            { 
                LblError1.Text = Resources.Message.InvalidLoginEnterred; 
                SetFocus(TXT_LoginUserName); 
            }
            else
            {
                //========== Fill To Variables ==========//
                string Username = DSet.Tables[0].Rows[0]["LoginID"].ToString();
                string Password = DSet.Tables[0].Rows[0]["LoginPass"].ToString();

                //========== Check The password ==========//
                if (TXT_LoginPassword.Text != Password) { LblError1.Text = Resources.Message.InvalidPassword; SetFocus(TXT_LoginPassword); return; }
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

                //========== Add to cookies ==========//
                if (CHK_RememberMe.Checked == true)
                {
                    HttpCookie objCookie = new HttpCookie("FujiLogin");
                    objCookie.Value = TXT_LoginUserName.Text;
                    objCookie.Expires = System.DateTime.Now.AddDays(20);
                    Response.SetCookie(objCookie);

                    objCookie = new HttpCookie("FujiPass");
                    objCookie.Value = Password;
                    objCookie.Expires = System.DateTime.Now.AddDays(20);
                    Response.SetCookie(objCookie);
                }
                else
                {
                    Response.Cookies["FujiLogin"].Value = "";
                    Response.Cookies["FujiPass"].Value = "";
                }

                //========== Send to Next Page ==========//
                Fuji.Common.CommonFunctions.PageRedirect("Home", "");
            }
        }
        catch (Exception Exc) { LblError2.Text = LblError1.Text = HandleException(Exc); }
    }
    protected void LBTN_ForgotPassword_Click(object sender, EventArgs e)
    {
        try
        {
            TBL_Login.Visible = false;
            TBL_ForgotPassword.Visible = true;
            SetFocus(TXT_ForgotPasswordUserName);
        }
        catch (Exception Exc) { LblError2.Text = LblError1.Text = HandleException(Exc); }
    }
    protected void LBTN_ForgotPassword1_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validations ==========//
            if (TXT_ForgotPasswordUserName.Text == "") 
            { 
                LblError2.Text = Resources.Message.UsernameIsRequired; 
                SetFocus(TXT_ForgotPasswordUserName); 
                return; 
            }
            if (TXT_ForgotPasswordEmail.Text == "") 
            { 
                LblError2.Text = Resources.Message.EmailIsRequired; 
                SetFocus(TXT_ForgotPasswordEmail); 
                return; 
            }
            else if (Fuji.Common.Validations.IsEmail(TXT_ForgotPasswordEmail.Text) == false) 
            { 
                LblError2.Text = Resources.Message.ValidEmailRequired; 
                SetFocus(TXT_ForgotPasswordEmail); 
                return; 
            }

            //========== Declarations ==========//
            TaskeroUserData objTaskeroUser = new TaskeroUserData();

            //========== Check the Login ==========//
            objTaskeroUser.LoginID = TXT_ForgotPasswordUserName.Text;
            objTaskeroUser.Email = TXT_ForgotPasswordEmail.Text;
            DataTable DT = objTaskeroUser.uspGetRetrievePassword();

            //========== Send and Email ==========//
            if (DT!=null && DT.Rows.Count == 0)
            { 
                LblError2.Text = Resources.Message.InvalidInformationEnterred; 
                SetFocus(TXT_ForgotPasswordUserName); 
            }
            else
            {
                //========== Mail Sent ==========//
                Fuji.Common.WebMails.SendRetrievePassword(TXT_ForgotPasswordEmail.Text, TXT_ForgotPasswordUserName.Text, DT.Rows[0]["LoginPass"].ToString(), DT.Rows[0]["FirstName"].ToString() + " " + DT.Rows[0]["LastName"].ToString());

                //========== Mail Sent ==========//
                LblError2.Text = Resources.Message.EmailSentTo.Replace("[[EMAIL]]", TXT_ForgotPasswordEmail.Text);
            }
        }
        catch (Exception Exc) { LblError2.Text = LblError1.Text = HandleException(Exc); }
    }
    protected void LBTN_Login_Click(object sender, EventArgs e)
    {
        try
        {
            TBL_Login.Visible = true;
            TBL_ForgotPassword.Visible = false;
            SetFocus(TXT_LoginUserName);
        }
        catch (Exception Exc) { LblError2.Text = LblError1.Text = HandleException(Exc); }
    }
    protected void TXT_LoginPassword_TextChanged(object sender, EventArgs e)
    {
        LBTN_Login1_Click(null, null);
    }
    protected void TXT_ForgotPasswordEmail_TextChanged(object sender, EventArgs e)
    {
        LBTN_ForgotPassword_Click(null, null);
    }
    #endregion

    #region Methods and Functions

    #region Set Labels
    /// <summary>
    /// Used to set the labels of the forms
    /// </summary>
    private void SetLabels()
    {
        //========== Set Culture ==========//
        Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
        Resources.Default.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

        //========== Set the Labels For Login ==========//        
        LTRL_LoginUserName.Text = Resources.Default.LBL_LoginUserName_Text;
        LTRL_LoginPassword.Text = Resources.Default.LBL_LoginPassword_Text;
        LBTN_ForgotPassword.Text = Resources.Default.LBTN_ForgotPassword_Text;

        //========== Set the Labels For Forgot Password ==========//        
        LTRL_ForgotPasswordUserName.Text = Resources.Default.LBL_ForgotPasswordUserName_Text;
        LTRL_ForgotPasswordEmail.Text = Resources.Default.LBL_ForgotPasswordEmail_Text;
        LBTN_Login.Text = Resources.Default.LBTN_Login_Text;
    }
    #endregion

    #region Call Login or Forgot Password Process
    /// <summary>
    /// Used to set the the function calls on pressing enter
    /// </summary>
    private void SetFunctionCallOnEnter()
    {
        //========== Check Key Press event and Call login process on pressing enter key ===========//
        TXT_LoginUserName.Attributes.Add("onKeyPress", "javascript:CheckOnEnter(event,1);");
        TXT_LoginPassword.Attributes.Add("onKeyPress", "javascript:CheckOnEnter(event,1);");
        CHK_RememberMe.Attributes.Add("onKeyPress", "javascript:CheckOnEnter(event,1);");

        //========== Check Key Press event and Call forgot password process on pressing enter key ===========//
        TXT_ForgotPasswordUserName.Attributes.Add("onKeyPress", "javascript:CheckOnEnter(event,2);");
        TXT_ForgotPasswordEmail.Attributes.Add("onKeyPress", "javascript:CheckOnEnter(event,2);");
    }
    #endregion
    
    #region My Focus
    /// <summary>
    /// Used to focus the input box
    /// </summary>
    /// <param name="TXT_My_Inputbox">TextBox to be focused on</param>
    private void SetFocus(TextBox TXT_My_Inputbox)
    {
        //========== Default Settings For the LoginUserName Input Box ==========//
        ScriptManager ScriptManager1 = (ScriptManager)this.Parent.FindControl("ScriptManager1");
        ScriptManager1.SetFocus(TXT_My_Inputbox.ClientID);
    }
    #endregion

    #endregion
}
