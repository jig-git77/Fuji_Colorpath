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
    public partial class DownloadSoftwares : BaseControl
    {
        #region Private variables - Globalization
        private const string _recColorPathInstallerMac = "colorPathInstallerMac";
        private const string _recColorPathInstallerPC = "colorPathInstallerPC";
        private const string _recValidationCharts = "ValidationCharts";
        #endregion

        #region Constructor
        public DownloadSoftwares()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            rgDownload.PageSize = Common.Utilities.PageSize;
        }

        protected void rgDownload_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgDownload.DataSource = DTable();
        }

        #region Methods       
        public DataTable DTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DownloadId", typeof(int)));
            dt.Columns.Add(new DataColumn("DownloadName", typeof(string)));
            dt.Columns.Add(new DataColumn("DownloadPath", typeof(string)));  

            DataRow dr = dt.NewRow();
            dr["DownloadId"] = "1";
            dr["DownloadName"] = GetResourceString(_recColorPathInstallerMac);
            dr["DownloadPath"] = "Download/Colorpath MacPlugin Installer.zip";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["DownloadId"] = "2";
            dr1["DownloadName"] = GetResourceString(_recColorPathInstallerPC);
            dr1["DownloadPath"] = "Download/Colorpath PCPlugin Installer.zip";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["DownloadId"] = "3";
            dr2["DownloadName"] = GetResourceString(_recValidationCharts);
            dr2["DownloadPath"] = "Download/Validation Charts.zip";
            dt.Rows.Add(dr2);

            //DataRow dr3 = dt.NewRow();
            //dr3["DownloadId"] = "4";
            //dr3["DownloadName"] = "Taskero Display (ER) Installer - PC";
            //dr3["DownloadPath"] = "Download/4";
            //dt.Rows.Add(dr3);


            return dt;
        }
        #endregion
    }
}