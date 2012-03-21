using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuProjectItem : System.Web.UI.UserControl
    {
        #region Variable Declarations
        public ELeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }
        public OptimizeRun CurrentRun { get; set; }
        #endregion

        #region "Page Events"
        protected void Page_PreRender(object sender, EventArgs e)
        {
            lnkRunName.Text = ItemName;

            switch (ItemLevel)
            {
                case ELeftMenuItemLevel.OptimizeRun:
                    if (CurrentRun.Status == EOptimizeRunStatus.Closed)
                    {
                        lnkMeasurement.Visible = false;
                    }

                    if (CurrentRun != null && SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == CurrentRun.OptimizeRunId)
                    {
                        ulSubMenu.Visible = true;
                        lnkRunName.Font.Bold = true;
                        lnkMeasurement.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Measure;
                        lnkAnalyse.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Analyze;
                        lnkDownload.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Download;
                    }
                    else
                    {
                        ulSubMenu.Visible = false;
                    }
                    break;
            }
        }
        #endregion

        #region Control events
        protected void lnkItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                var lnkItem = sender as LinkButton;
                if (lnkItem != null)
                {
                    SessionManager.Instance.CurrentOptimizeRun = this.CurrentRun;
                    if (lnkItem.ID == "lnkMeasurement")
                    {
                        Response.Redirect("Measurement.aspx");
                    }
                    else if (lnkItem.ID == "lnkAnalyse")
                    {
                        Response.Redirect("Analyze.aspx");
                    }
                    else if (lnkItem.ID == "lnkDownload")
                    {
                        Response.Redirect("Export.aspx");
                    }
                }
            }
        }

        protected void lnkRunName_Click(object sender, EventArgs e)
        {
            switch (ItemLevel)
            {
                case ELeftMenuItemLevel.OptimizeRun:
                    SessionManager.Instance.CurrentOptimizeRun = this.CurrentRun;
                    Response.Redirect(
                        CurrentRun.Status == EOptimizeRunStatus.Open
                            ? "Measurement.aspx"
                            : "Analyze.aspx");
                    break;
            }
        }
        #endregion
    }
}