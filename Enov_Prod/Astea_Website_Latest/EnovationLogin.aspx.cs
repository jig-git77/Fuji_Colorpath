using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //Set Focus on the username field
        AjaxControlToolkit.Utility.SetFocusOnLoad(UserTextBox);

        if (IsPostBack == false)
        {

            ScriptManager ScriptManager1 = (ScriptManager)this.FindControl("ScriptManager1");
            ScriptManager1.SetFocus(UserTextBox.ClientID);
    
            if ((Request.Cookies["FTSSLogin"] == null || Request.Cookies["FTSSLogin"].Value == "") && (Request.Cookies["FTSSPass"] == null || Request.Cookies["FTSSPass"].Value == ""))
            { CHK_RememberMe.Checked = false; }
            else
            {
                CHK_RememberMe.Checked = true;
                UserTextBox.Text = Request.Cookies["FTSSLogin"].Value;
                PwdTextBox.Attributes.Add("value", Request.Cookies["FTSSPass"].Value);
            }
        }

    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
                  
        int iSession=-1;

        try
        {
             SEALAPI.Broker session = new SEALAPI.Broker();
             //store session in some global (Session) variable
             Session["SessionInfo"] = session;
        
            //get user login details and display message when login fails
            Session["UserID"] = UserTextBox.Text;
            Session["PassWd"] = PwdTextBox.Text;
            
            try
            {
                iSession = session.OpenSession("default", UserTextBox.Text, PwdTextBox.Text);
            }
            catch (Exception) {
                //if there is a wrong protocol error from API broker do nothing
            }      

            if (iSession < 1)
            {
                ErrLabel.Text = "Login Failed! Please check if the UserID and Password are correct.";
                ErrLabel.ForeColor = System.Drawing.Color.Red;
                session.CloseSession(iSession);
                return; //Message Login Failed

            }
            session.CloseSession(iSession);

            //========== Add to cookies ==========//
            if (CHK_RememberMe.Checked == true)
            {
                HttpCookie objCookie = new HttpCookie("FTSSLogin");
                objCookie.Value = UserTextBox.Text;
                objCookie.Expires = System.DateTime.Now.AddDays(20);
                Response.SetCookie(objCookie);

                objCookie = new HttpCookie("FTSSPass");
                objCookie.Value = PwdTextBox.Text;
                objCookie.Expires = System.DateTime.Now.AddDays(20);
                Response.SetCookie(objCookie);
            }
            else
            {
                Response.Cookies["FTSSLogin"].Value = "";
                Response.Cookies["FTSSPass"].Value = "";
            }

            //Go to next page
            System.Web.HttpContext.Current.Response.Redirect("AssignedOrders.aspx",true);

        }
        catch (Exception)
        {
            //do nothing
        }      

    }
}
