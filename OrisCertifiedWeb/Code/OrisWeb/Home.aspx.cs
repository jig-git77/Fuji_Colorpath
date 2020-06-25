using Oris.Common;
using OrisWeb.Models;
using System;


namespace OrisWeb
{
    public partial class Home1 : PageBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    //========== Check Session ===========//
                    Sessions.CheckUserLogin();
                    IMG_LargeImage.Src = "Branding/LargeImage/" + Sessions.LargeImage.ToString();
                }
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }
        }
        #endregion
    }

}