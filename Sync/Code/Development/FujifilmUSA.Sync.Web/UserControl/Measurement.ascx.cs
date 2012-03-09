using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Measurement : System.Web.UI.UserControl
    {
        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }
        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            header.InnerText = string.Format("New Print Condition - Measure (No DLP)");
            if (Request.QueryString["ProjectName"] != null)
            {
                header.InnerText = string.Format("Measure - {0}.icc", Request.QueryString["ProjectName"]);
            }
        }
        #endregion

        #region Control Events
        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            Response.Redirect("Analyze.aspx");
        }
        #endregion

    }
}
