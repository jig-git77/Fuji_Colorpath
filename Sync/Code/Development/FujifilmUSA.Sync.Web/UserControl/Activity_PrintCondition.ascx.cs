using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Data;

using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Activity_PrintCondition : BaseControl, IViewActivityPrintCondition
    {
        private PresenterActivityPrintCondition _presenterActivityPrintCondition;

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterActivityPrintCondition = new PresenterActivityPrintCondition(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RadGrid1.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                BindActivityPrintList();
            }
        }
        #endregion

        #region Methods
        private void GetActivityPrintList()
        {
            RegionID = SessionManager.Instance.CurrentUser.RegionId;
            ActivityPrintList = new List<Activity>();
            _presenterActivityPrintCondition.GetPrintActivities();
        }
        private void BindActivityPrintList()
        {
            GetActivityPrintList();
            if (!IsErrorOccured)
            {
                RadGrid1.DataSource = ActivityPrintList;
                RadGrid1.DataBind();
            }
        }
        #endregion

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GetActivityPrintList();
            if (!IsErrorOccured)
            {
                RadGrid1.DataSource = ActivityPrintList;
            }
            //if (!e.IsFromDetailTable)
            //{
            //    RadGrid1.DataSource = GetDataTable("SELECT * FROM Customers");
            //}
        }

        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            //GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            //switch (e.DetailTableView.Name)
            //{
            //    case "Orders":
            //        {
            //            string CustomerID = dataItem.GetDataKeyValue("CustomerID").ToString();
            //            e.DetailTableView.DataSource = GetDataTable("SELECT * FROM Orders WHERE CustomerID = '" + CustomerID + "'");
            //            break;
            //        }

            //    case "OrderDetails":
            //        {
            //            string OrderID = dataItem.GetDataKeyValue("OrderID").ToString();
            //            e.DetailTableView.DataSource = GetDataTable("SELECT * FROM [Order Details] WHERE OrderID = " + OrderID);
            //            break;
            //        }
            //}
        }

        public DataTable GetDataTable(string query)
        {
            //String ConnString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
            //SqlConnection conn = new SqlConnection(ConnString);
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = new SqlCommand(query, conn);

            DataTable myDataTable = new DataTable();

            //conn.Open();
            //try
            //{
            //    adapter.Fill(myDataTable);
            //}
            //finally
            //{
            //    conn.Close();
            //}

            return myDataTable;
        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RadGrid1.MasterTableView.Items[0].Expanded = true;
                //RadGrid1.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        #region Implement Interface

        public int RegionID { get; set; }
        public List<Activity> ActivityPrintList { get; set; }

        #endregion

    }
}