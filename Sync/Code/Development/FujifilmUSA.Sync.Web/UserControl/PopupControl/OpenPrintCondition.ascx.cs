using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class OpenPrintCondition : System.Web.UI.UserControl
    {
        #region Declaration
        #endregion

        #region Page events
        protected void Page_PreRender(object sender, EventArgs e)
        {
        }
        #endregion

        #region Control events
        protected void lbtnContinue_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("Measurement.aspx?ProjectName='" + hdProjectName.Value + "'");
        }
        #endregion

        #region "Methods"
        #endregion
        
    }
}