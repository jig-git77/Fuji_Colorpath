using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze : System.Web.UI.UserControl, IViewAnalyze
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        public string AddMeasurementDeleteMeasurementConfirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public bool IsNewDlp
        {

            get { return SessionManager.Instance.CurrentProject.IsNewlyCreated; }
        }

        #endregion

        #region Page Method
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
