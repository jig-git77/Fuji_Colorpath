using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementStatusbar : System.Web.UI.UserControl
    {
        #region "Page Load"
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region "Methods"
        public void DisplayMessage(string message, bool status)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = (status) ? Color.Green : Color.Red;
            this.Visible = true;
        }
        #endregion

    }
}