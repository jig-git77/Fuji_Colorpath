using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class COErrorMessage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.COErrorMessage != null)
            {
                ucCODisplaybar.Header = SessionManager.Instance.COErrorMessage.Header;
                ucCODisplaybar.Message = SessionManager.Instance.COErrorMessage.Message;
                
                //Clear messages
                SessionManager.Instance.COErrorMessage.Header = "";
                SessionManager.Instance.COErrorMessage.Message = "";                
            }
        }
    }
}