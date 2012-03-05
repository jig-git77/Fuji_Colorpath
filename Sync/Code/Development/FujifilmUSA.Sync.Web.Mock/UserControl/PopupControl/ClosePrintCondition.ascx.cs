using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class ClosePrintCondition : System.Web.UI.UserControl
    {
        #region Declaration
        #endregion

        #region Page events
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //this.Visible = (this.Parent != null && ((FujifilmUSA.Sync.Web.Mock.UserControl.ModelWindowContainer)Parent).ModelControlId == 2);
            if (!IsPostBack && Request.QueryString["ProjectName"] != null)
            {
                lblMessage.Text = string.Format("Options for '{0}' Print Condition", Request.QueryString["ProjectName"]);
            }

        }
        #endregion

        #region "Methods"
        public void DisplayMessage(string message, bool status)
        {
        }
#endregion


        
    }
}