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
    public partial class DownloadSoftwares : System.Web.UI.UserControl
    {
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

            DataRow dr = dt.NewRow();
            dr["DownloadId"] = "1";
            dr["DownloadName"] = "Colorpath Verified (CPV) Installer - Mac";            
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["DownloadId"] = "2";
            dr1["DownloadName"] = "Colorpath Verified (CPV) Installer - PC";            
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["DownloadId"] = "3";
            dr2["DownloadName"] = "Taskero Display (ER) Installer - Mac";            
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["DownloadId"] = "4";
            dr3["DownloadName"] = "Taskero Display (ER) Installer - PC";   
            dt.Rows.Add(dr3);

            DataRow dr4 = dt.NewRow();
            dr4["DownloadId"] = "5";
            dr4["DownloadName"] = "TempRH Installer - PC";
            dt.Rows.Add(dr4);

            DataRow dr5 = dt.NewRow();
            dr5["DownloadId"] = "6";
            dr5["DownloadName"] = "ITX-PDCSII Colorpath Verified Installation";
            dt.Rows.Add(dr5);

            DataRow dr6 = dt.NewRow();
            dr6["DownloadId"] = "7";
            dr6["DownloadName"] = "ColorpathXF Installer";
            dt.Rows.Add(dr6);

            DataRow dr7 = dt.NewRow();
            dr7["DownloadId"] = "8";
            dr7["DownloadName"] = "Taskero ColorTuner";
            dt.Rows.Add(dr7);

            DataRow dr8 = dt.NewRow();
            dr8["DownloadId"] = "9";
            dr8["DownloadName"] = "Taskero ColorGATE Client";
            dt.Rows.Add(dr8);

            return dt;
        }
        #endregion
    }
}