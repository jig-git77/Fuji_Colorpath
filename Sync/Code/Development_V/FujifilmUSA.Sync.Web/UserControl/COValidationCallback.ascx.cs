using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class COValidationCallback : BaseControl
    {
        public COValidationCallback()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //XMF User and Validation/Verification action only
                ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.Page.GetType(),
                            "messaging",
                            string.Format("window.parent.postMessage('Action: Validation. ProjectId:{0}. Result:{1}','{2}');",
                                          !string.IsNullOrEmpty(Request.QueryString["projectid"])
                                          ? Convert.ToInt32(Request.QueryString["projectid"])
                                          : 0,
                                          !string.IsNullOrEmpty(Request.QueryString["result"])
                                          ? Request.QueryString["result"].ToString()
                                          : "",
                                          SessionManager.Instance.CurrentValidationRequest.SilverlightPostUrl),
                            true);
            }
        }
    }
}