using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class Pagging : System.Web.UI.UserControl
    {
        public delegate void PaggingPreRender(object sender, EventArgs e);
        public event EventHandler MyPreRender;

        #region "Property"
        public DataPager DataPager
        {
            get
            {
                return this.dpPagging;
            }
        }
        public string PagedControlID
        {
            set
            {
                this.dpPagging.PagedControlID = value;
            }
            get
            {
                return this.dpPagging.PagedControlID;
            }
        }
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            this.dpPagging.PreRender +=new EventHandler(dpPagging_PreRender);
            BindPageSize();
        }
        #endregion

        #region Control events
        protected void dpPagging_PreRender(object sender, EventArgs e)
        {
            this.dpPagging.PageSize = Convert.ToInt32(cmbPageSize.SelectedValue);
            this.MyPreRender(this, new EventArgs());
            SetPageNumber();
        }
        #endregion

        #region Method
        public void SetPageNumber()
        {
            //int currentPage = (dpPagging.StartRowIndex / dpPagging.PageSize) + 1;
            //int totalPages = (dpPagging.TotalRowCount / dpPagging.PageSize) + 1;
            lblPageMessage.Text = string.Format("<b>{0}</b> Items in <b>{1}</b> Pages", dpPagging.TotalRowCount, dpPagging.TotalRowCount != 0 ? (dpPagging.TotalRowCount / dpPagging.PageSize) + 1 : 0);
        }
        public void BindPageSize()
        {
            cmbPageSize.Items.Clear();
            for (int i = 5; i < 100; i=i+5)
            {
                cmbPageSize.Items.Add(i.ToString());
            }
        }
        #endregion

    }
}

