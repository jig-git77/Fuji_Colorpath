using System;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Permission : BaseControl
    {
        #region "Control Event"
        protected void likIndex_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("Index.aspx");
        }
        #endregion


    }
}