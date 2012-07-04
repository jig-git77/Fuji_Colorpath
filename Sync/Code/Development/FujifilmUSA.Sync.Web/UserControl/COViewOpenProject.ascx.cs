using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class COViewOpenProject : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCODisplaybar.Header = "View Last Analyze";
            ucCODisplaybar.Message = "Current Project is open. You can not view last analyze!";
        }
    }
}