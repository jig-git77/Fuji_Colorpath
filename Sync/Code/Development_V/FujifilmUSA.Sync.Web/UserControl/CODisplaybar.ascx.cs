using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class CODisplaybar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Header
        {
            set
            {
                lblHeader.Text = value;
            }
        }

        public string Message
        {
            set
            {
                lblMessage.Text = value;
            }
        }
    }
}