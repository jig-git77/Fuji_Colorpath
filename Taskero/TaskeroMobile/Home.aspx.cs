using System;
using Taskero.Common;

namespace TaskeroMobile
{
    public partial class Home : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //========== Check User Login ==========//
            Sessions.CheckUserLoginForMobi();
        }


    }
}