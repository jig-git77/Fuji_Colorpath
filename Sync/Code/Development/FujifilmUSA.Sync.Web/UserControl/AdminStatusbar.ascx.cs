using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AdminStatusbar : System.Web.UI.UserControl
    {
        #region Declaration
        public string Message { get; set; }
        public bool Status { get; set; }
        #endregion

        #region Page events
        protected void Page_Load(object sender, EventArgs e)
        {
            //Note : Every time this event fire and control set visible = false
            this.Visible = (this.Message != null);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            lblMessage.Text = this.Message;
            lblMessage.ForeColor = (this.Status) ? Color.Green : Color.Red;
        }
        #endregion

        #region "Methods"

        public void DisplayMessage(string message, bool status)
        {
            this.Message = message;
            this.Status = status;
            this.Visible = true;
        }

      
        #endregion


        
    }
}