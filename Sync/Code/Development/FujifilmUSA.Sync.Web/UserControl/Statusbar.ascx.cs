using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Statusbar : System.Web.UI.UserControl
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        #region Declaration
        
        public void DisplayMessage(string message, bool status)
        {
            this.Message = message;
            this.Status = status;
            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            lblMessage.Text = this.Message;
            lblMessage.ForeColor = (this.Status) ? Color.Green : Color.Red;
        }
        #endregion


        
    }
}