using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class COProjectMaxLimit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucCODisplaybar.Header = "Project Limit";
            ucCODisplaybar.Message = "Opps!!! You reach your maximum limit of projects, so you can't create more projects.";
        }
    }
}