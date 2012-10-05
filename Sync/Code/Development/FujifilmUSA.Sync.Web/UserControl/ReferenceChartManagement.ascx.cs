using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ReferenceChartManagement : BaseControl, IViewReferenceChartManagement
    {
        #region Private variables - Globalization
        private const string _recDeleteSoureProfileConfirm = "deleteSoureProfileConfirm";
        #endregion

        #region Declaration
        private PresenterAddReferenceChart _presenterAddReferenceChart;
        public string SourceProfileManagement_DeleteSourceProfile_Confirmation
        {
            get
            {
                return GetResourceString(_recDeleteSoureProfileConfirm); //"No Reference Charts";
            }
        }
        #endregion

        #region Constructor
        public ReferenceChartManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        } 
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterAddReferenceChart = new PresenterAddReferenceChart(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgRefChart.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                rgRefChart.ItemCommand += rgRefChart_ItemCommand;
                BindRefChartList();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //------------Apply default CurrentFilterFunction for "NumOfRows", "NumOfCols", "TotalPatches" columns of rgRefChart RadGrid-----------------//
            rgRefChart.Columns[1].CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;
            rgRefChart.Columns[2].CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;
            rgRefChart.Columns[3].CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;

        }
        #endregion

        #region Methods

        public void BindRefChartList()
        {
            _presenterAddReferenceChart.GetReferenceCharts();
            if (!IsErrorOccured)
            {
                rgRefChart.DataSource = ReferenceCharts;
                rgRefChart.DataBind();
            }
        }

        #endregion

        #region Control Events
        protected void rgRefChart_ItemCommand(object sender, GridCommandEventArgs e)
        {
            ReferenceChartType = new Entity.ReferenceChartType();
            switch (e.CommandName)
            {
                case "ClearFilter":
                    foreach (GridColumn column in rgRefChart.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgRefChart.MasterTableView.FilterExpression = string.Empty;
                    rgRefChart.MasterTableView.Rebind();
                    break;
                case "Edit":
                    ReferenceChartType.ReferenceChartTypeId = (short)Convert.ToInt32(e.CommandArgument);
                    Response.Redirect("AddReferenceChart.aspx?RefChartId=" + ReferenceChartType.ReferenceChartTypeId);
                    break;
            }
        }
        protected void rgRefChart_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterAddReferenceChart.GetReferenceCharts();
            if (!IsErrorOccured)
            {
                rgRefChart.DataSource = ReferenceCharts;
            }
        }

        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddReferenceChart.aspx");
        }

        #endregion

        #region IViewReferenceChartManagement Members
        public ReferenceChartType ReferenceChartType { get; set; }
        public List<ReferenceChartType> ReferenceCharts { get; set; }
        #endregion
    }
}