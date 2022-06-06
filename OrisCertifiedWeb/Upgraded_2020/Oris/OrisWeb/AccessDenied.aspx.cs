using Oris.Common;
using OrisWeb.Models;
using System;



namespace OrisWeb
{
    public partial class AccessDenied : PageBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //==========  Clear Error ==========//
                LblError.Text = "";

                //==========  Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========= Check User Login =========//
                    Sessions.CheckUserLogin();
                }
            }
            catch (Exception exc)
            {
                LblError.Text = HandleException(exc);
            }
        }
        #endregion
    }

}