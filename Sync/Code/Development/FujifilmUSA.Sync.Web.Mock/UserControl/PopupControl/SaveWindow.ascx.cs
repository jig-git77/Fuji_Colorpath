using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class SaveWindow : System.Web.UI.UserControl
    {
        #region Declaration
        #endregion

        #region Page events
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.Visible = (this.Parent != null && ((FujifilmUSA.Sync.Web.Mock.UserControl.ModelWindowContainer)Parent).ModelControlId == 3);
        }
        #endregion

        #region "Methods"
        #endregion
        
    }
}