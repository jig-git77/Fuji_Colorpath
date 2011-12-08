using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuProjectItem : System.Web.UI.UserControl
    {
        public LeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ulSyncSubMenu.Visible = !(ItemLevel == LeftMenuItemLevel.ProjectSetup || ItemLevel == LeftMenuItemLevel.NewSyncRun);
            lnkSyncRunName.Text = ItemName;
        }
    }
}