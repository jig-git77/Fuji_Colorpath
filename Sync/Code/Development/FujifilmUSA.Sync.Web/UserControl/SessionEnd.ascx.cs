using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SessionEnd : System.Web.UI.UserControl
    {
        #region "Control Event"
        protected void likIndex_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("Index.aspx");
        }
        #endregion


    }
}