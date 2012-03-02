using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class OperationSelection : System.Web.UI.UserControl
    {
        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                lblDeviceName.Text = "Komori LS 40";
            }
        }
        #endregion

        #region Control events
        protected void lbtnDLPOptimization_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectList.aspx");
        }
        #endregion

        #region Method
      

        #endregion

    }

 
}

