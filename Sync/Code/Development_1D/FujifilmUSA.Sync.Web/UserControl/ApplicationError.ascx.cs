using System;
using System.Web.Security;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ApplicationError : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            FormsAuthentication.SignOut();
        }

        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("Login.aspx");
        }
    }
}