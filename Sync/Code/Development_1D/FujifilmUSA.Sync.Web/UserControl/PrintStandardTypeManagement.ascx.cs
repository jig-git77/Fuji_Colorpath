using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class PrintStandardTypeManagement : BaseControl, IViewPrintStandardType
    {
        #region Declaration
        private PresenterPrintStandardType _presenterPrintStandardType;
        public string PrintStandardTypeManagement_DeletePrintStandardType_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("PrintStandardTypeManagement_DeletePrintStandardType_Confirmation");
            }
        }
        #endregion

        #region Constructor
        public PrintStandardTypeManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterPrintStandardType = new PresenterPrintStandardType(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgPrintStandardType.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                BindPrintStandardTypeList();
            }
        }
        #endregion

        #region Methods

        public void BindPrintStandardTypeList()
        {
            _presenterPrintStandardType.GetPrintStandardTypes();
            if (!IsErrorOccured)
            {
                rgPrintStandardType.DataSource = PrintStandardTypes;
                rgPrintStandardType.DataBind();
            }
        }

        #endregion

        #region Control Events
        protected void rgPrintStandardType_ItemCommand(object sender, GridCommandEventArgs e)
        {
            PrintStandardType = new Entity.CPSync1D.PrintStandardType();
            switch (e.CommandName)
            {               
                case "ClearFilter":
                    foreach (GridColumn column in rgPrintStandardType.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgPrintStandardType.MasterTableView.FilterExpression = string.Empty;
                    rgPrintStandardType.MasterTableView.Rebind();
                    break;
                case "Delete":
                    PrintStandardType.PrintStandardTypeID = (short)Convert.ToInt32(e.CommandArgument);
                    _presenterPrintStandardType.DeletePrintStandardType();
                    break;
                case "Edit":
                    PrintStandardType.PrintStandardTypeID = (short)Convert.ToInt32(e.CommandArgument);
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(PrintStandardType.PrintStandardTypeID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddPrintStandardType.aspx?PrintStandardTypeId=" + ContentEncoder.Encode(parms));
                    break;
            }
        }
        protected void rgPrintStandardType_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterPrintStandardType.GetPrintStandardTypes();
            if (!IsErrorOccured)
            {
                rgPrintStandardType.DataSource = PrintStandardTypes;
            }
        }

        protected void likAdd_Click(object sender, EventArgs e)
        {            
            Response.RedirectPermanent("AddPrintStandardType.aspx");
        }

        #endregion

        #region IViewPrintStandardType Members
        public short PrintStandardTypeID { get; set; }
        public string PrintStandard { get; set; }
        public bool IsSync1DType { get; set; }

        public Entity.CPSync1D.PrintStandardType PrintStandardType { get; set; }
        public List<Entity.CPSync1D.PrintStandardType> PrintStandardTypes { get; set; }
        public Dictionary<int, string> SourceProfileList { get; set; }       
        #endregion
    }
}