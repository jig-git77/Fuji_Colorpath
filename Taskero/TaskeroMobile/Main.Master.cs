using System;
using Taskero.Common;

namespace TaskeroMobile
{
    public partial class Main : MasterBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //========== Check User Login ==========//
            Sessions.CheckUserLoginForMobi();

        }
    }
}