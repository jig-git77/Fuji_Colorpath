using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class COTransferSiteControl : BaseControl, IViewCOTransferSiteControl
    {
        private PresenterCOTransferSiteControl _presenterCOTransferSiteControl;
        public COTransferSiteControl()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        protected override void OnInit(EventArgs e)
        {
            _presenterCOTransferSiteControl = new PresenterCOTransferSiteControl(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //_presenterCOTransferSiteControl.GetSilverlightPostUrl();
                ScriptManager.RegisterStartupScript(
                    this.Page,
                    this.Page.GetType(),
                    "messaging",
                    string.Format("window.parent.postMessage('{0}','{1}');",
                                  !string.IsNullOrEmpty(Request.QueryString["projectid"])
                                      ? Convert.ToInt32(Request.QueryString["projectid"])
                                      : 0,
                                  SessionManager.Instance.CurrentColorOrgRequest.SilverlightPostUrl),
                    true);
            }
        }

        //#region IViewCOTransferSiteControl Members

        //public string SilverlightPostUrl { get; set; }

        //#endregion
    }
}