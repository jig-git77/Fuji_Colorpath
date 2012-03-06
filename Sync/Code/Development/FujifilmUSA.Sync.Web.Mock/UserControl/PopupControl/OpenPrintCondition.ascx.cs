using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class OpenPrintCondition : System.Web.UI.UserControl
    {
        #region Declaration
        #endregion

        #region Page events
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (!IsPostBack && Request.QueryString["ProjectName"] != null)
            //{
            //    this.Visible = Request.QueryString["ModelControlId"].ToString().Equals("1", StringComparison.OrdinalIgnoreCase);
            //    lblOpenMessage.Text = string.Format("Current '{0}' is open and has been inactive for 2.5 days.</br> " +
            //                                    " Confirm which DLP is used in your workflow. </br> In order to continue optimizing "+
            //                                    " this DLP your workflow must be using '{0}'.", Request.QueryString["ProjectName"]);
            //}
        }
        #endregion

        #region Control events
        protected void lbtnContinue_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Methods"
        #endregion
        
    }
}