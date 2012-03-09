using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ClosePrintCondition : System.Web.UI.UserControl
    {
        #region Declaration
        #endregion

        #region Page events
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (!IsPostBack && Request.QueryString["ProjectName"] != null)
            //{
            //    this.Visible = Request.QueryString["ModelControlId"].ToString().Equals("2", StringComparison.OrdinalIgnoreCase);
            //    lblMessage.Text = string.Format("Options for '{0}' Print Condition", Request.QueryString["ProjectName"]);
            //}

        }
        #endregion

        #region Controls events
        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            Response.Redirect("Measurement.aspx?ProjectName=''");
        }
        #endregion

        #region "Methods"
        public void DisplayMessage(string message, bool status)
        {
        }
#endregion

        

    


        
    }
}