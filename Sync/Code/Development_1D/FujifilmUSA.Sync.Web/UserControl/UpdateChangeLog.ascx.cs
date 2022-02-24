using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class UpdateChangeLog : BaseControl, IViewUpdateChangeLog
    {
        private PresenterUpdateChangeLog _presenterUpdateChangeLog;
        #region Constructor
        public UpdateChangeLog()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            _presenterUpdateChangeLog = new PresenterUpdateChangeLog(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgUpdateLog.PageSize = Common.Utilities.PageSize;
        }

        protected void rgUpdateLog_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterUpdateChangeLog.GetActiveUpdateChangeLogs(true, false);
            rgUpdateLog.DataSource = resultData;
        }

        #region Implement Interface IViewUpdateChangeLog
        public List<Entity.UpdateChangeLog> UpdateChangeLogList { get; set; }
        public DataSet resultData { get; set; }
        public Entity.UpdateChangeLog objUpdateChangeLog { get; set; }
        #endregion
    }
}