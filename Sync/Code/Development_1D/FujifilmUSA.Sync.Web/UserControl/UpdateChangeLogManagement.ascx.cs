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
using System.Data;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class UpdateChangeLogManagement : BaseControl, IViewUpdateChangeLog
    {
        private PresenterUpdateChangeLog _presenterUpdateChangeLogManagement;

        #region Declaration
        public string MToolVersionManagement_DeleteMToolVersion_Confirmation
        {
            get
            {
                return "Are your sure want to delete this version?";
            }
        }
        #endregion
        public UpdateChangeLogManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterUpdateChangeLogManagement = new PresenterUpdateChangeLog(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgUpdateChangeLogList.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                BindUpdateChangeLogsList();
            }
        }
        #endregion

        #region Methods
        private void GetAllUpdateChangeLogsList()
        {
            resultData = new DataSet();
            objUpdateChangeLog = new Entity.UpdateChangeLog();
            objUpdateChangeLog.UpdateListID = 0;
            _presenterUpdateChangeLogManagement.GetAllUpdateChangeLogs();
        }
        private void BindUpdateChangeLogsList()
        {
            GetAllUpdateChangeLogsList();
            if (!IsErrorOccured)
            {
                rgUpdateChangeLogList.DataSource = resultData;
                rgUpdateChangeLogList.DataBind();
            }
        }
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddUpdateChangeLogManagement.aspx");
        }
        protected void rgUpdateChangeLogList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            objUpdateChangeLog = new Entity.UpdateChangeLog();
            switch (e.CommandName)
            {
                case "Filter":
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgUpdateChangeLogList.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgUpdateChangeLogList.MasterTableView.FilterExpression = string.Empty;
                    rgUpdateChangeLogList.MasterTableView.Rebind();
                    break;
                case "Edit":
                    objUpdateChangeLog.UpdateListID = Convert.ToInt16(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item;
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(objUpdateChangeLog.UpdateListID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddUpdateChangeLogManagement.aspx?UpdateListID=" + ContentEncoder.Encode(parms));
                    break;
                case "Delete":
                    objUpdateChangeLog.UpdateListID = Convert.ToInt16(e.CommandArgument);
                    _presenterUpdateChangeLogManagement.DeleteUpdateChangeLogs();
                    break;
            }
        }
        protected void rgUpdateChangeLogList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetAllUpdateChangeLogsList();
            if (!IsErrorOccured)
            {
                rgUpdateChangeLogList.DataSource = resultData;
            }
        }


        #endregion

        #region Implement Interface IViewUpdateChangeLog
        public List<Entity.UpdateChangeLog> UpdateChangeLogList { get; set; }
        public DataSet resultData { get; set; }
        public Entity.UpdateChangeLog objUpdateChangeLog { get; set; }
        #endregion
    }
}