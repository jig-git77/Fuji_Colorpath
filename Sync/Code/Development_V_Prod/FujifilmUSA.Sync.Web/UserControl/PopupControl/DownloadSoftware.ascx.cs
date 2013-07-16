using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class DownloadSoftware : BaseControl
    {
        #region Private variables - Globalization
        private const string _recColorPathInstallerMac = "colorPathInstallerMac";
        private const string _recColorPathInstallerPC = "colorPathInstallerPC";
        #endregion

        #region Constructor
        public DownloadSoftware()
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

            //DataRow dr2 = dt.NewRow();
            //dr2["DownloadId"] = "3";
            //dr2["DownloadName"] = "Taskero Display (ER) Installer - Mac";
            //dr2["DownloadPath"] = "Download/3";
            //dt.Rows.Add(dr2);

            //DataRow dr3 = dt.NewRow();
            //dr3["DownloadId"] = "4";
            //dr3["DownloadName"] = "Taskero Display (ER) Installer - PC";
            //dr3["DownloadPath"] = "Download/4";
            //dt.Rows.Add(dr3);

            //DataRow dr4 = dt.NewRow();
            //dr4["DownloadId"] = "5";
            //dr4["DownloadName"] = "TempRH Installer - PC";
            //dt.Rows.Add(dr4);

            //DataRow dr5 = dt.NewRow();
            //dr5["DownloadId"] = "6";
            //dr5["DownloadName"] = "ITX-PDCSII Colorpath Verified Installation";
            //dt.Rows.Add(dr5);

            //DataRow dr6 = dt.NewRow();
            //dr6["DownloadId"] = "7";
            //dr6["DownloadName"] = "ColorpathXF Installer";
            //dt.Rows.Add(dr6);

            //DataRow dr7 = dt.NewRow();
            //dr7["DownloadId"] = "8";
            //dr7["DownloadName"] = "Taskero ColorTuner";
            //dt.Rows.Add(dr7);

            //DataRow dr8 = dt.NewRow();
            //dr8["DownloadId"] = "9";
            //dr8["DownloadName"] = "Taskero ColorGATE Client";
            //dt.Rows.Add(dr8);

            return dt;
        }
        #endregion
    }
}