using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SyncRunExport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
        }
    }
}