using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuProjectItem : System.Web.UI.UserControl
    {
        public string SyncRunName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            lnkProjectName.Text = SyncRunName;
        }
    }
}