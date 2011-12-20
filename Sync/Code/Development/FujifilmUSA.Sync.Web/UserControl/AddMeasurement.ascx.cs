using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMeasurement : System.Web.UI.UserControl
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

        
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();            

        }

       protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            Response.Redirect("SyncRunAnalyze.aspx");
        }
    }
}
