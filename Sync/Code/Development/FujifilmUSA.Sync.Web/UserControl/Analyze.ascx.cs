using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze : System.Web.UI.UserControl 
    {
        #region Variable Declarations
        //private PresenterSyncRunAnalyze _presenterSyncRunAnalyze;
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }
        public bool IsNewDLP
        {
            get { return Request.QueryString["ProjectName"] == null; }
        }
        #endregion

        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {
            //_presenterSyncRunAnalyze = new PresenterSyncRunAnalyze(this);
            header.InnerText = string.Format("New Print Condition - Analyze (No DLP)");
            lbtnDownloadAndSaveDLP.Text = "<span><img id='imgAdd' src='Images/export.png'/>Download DLP<span>";
            if (Request.QueryString["ProjectName"] != null)
            {
                header.InnerText = string.Format("Analyze - {0}.icc", Request.QueryString["ProjectName"]);
                lbtnDownloadAndSaveDLP.Text = "<span><img id='imgAdd' src='Images/save_icon.png'/>Save<span>";
            }
        }
        #endregion

        #region Methods
        #endregion

        #region Control Events
        protected void lbtnDownloadDLP_Click(object sender, EventArgs e)
        {
        }
        #endregion
    
    }
}
