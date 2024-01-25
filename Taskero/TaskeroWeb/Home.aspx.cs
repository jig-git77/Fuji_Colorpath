using System;
using Taskero.Common;
using TaskeroWeb.Models;


namespace TaskeroWeb
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