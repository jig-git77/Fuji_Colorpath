using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ConfirmSave : System.Web.UI.UserControl
    {
        #region Declaration
        #endregion

        #region Page events
        protected void Page_PreRender(object sender, EventArgs e)
        {
        }
        #endregion

       

        #region Control events
        protected void lbtnYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectList.aspx");
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region "Methods"
        #endregion
        
    }
}