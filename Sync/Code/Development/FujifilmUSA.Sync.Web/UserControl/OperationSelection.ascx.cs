using System;

namespace FujifilmUSA.Sync.Web.UserControl
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
            Response.Redirect("~/ProjectList.aspx");
        }
        #endregion

        #region Method
      

        #endregion

    }

 
}

