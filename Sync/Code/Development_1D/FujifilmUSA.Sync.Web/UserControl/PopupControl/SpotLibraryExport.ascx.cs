using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Data;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class SpotLibraryExport : BaseControl, IViewExport
    {
        #region Private Variables - Globalization     
        private PresenterExport _presenterExport;
        private const string _recSelectText = "selectText";
        #endregion

        #region Construtor
        public SpotLibraryExport()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Methods
        protected override void OnInit(EventArgs e)
        {
            _presenterExport = new PresenterExport(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
            hdfAuthToken.Value = SessionManager.Instance.CurrentUser.APIAuthKey;
            if (string.IsNullOrEmpty(Request.QueryString["SpotProjectIds"]))
            {
                hdnOptiomizatoinRunID.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
            }
            else
            {
                hdnSpotProjectIDs.Value = Convert.ToString(Request.QueryString["SpotProjectIds"]);
            }

        }
        #endregion

        #region Control Methods       
        protected void cmbCMYKLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCMYKLibrary.SelectedValue.ToString() == "1")//Caldera option
            {
                trCalderaRow1.Visible = true;
                trCalderaRow2.Visible = true;
                trCaldera2Row1.Visible = false;
                trXMF.Visible = false;
                trPackZ.Visible = false;
            }
            else if (cmbCMYKLibrary.SelectedValue.ToString() == "2")//Caldera2 option
            {
                trCalderaRow1.Visible = true;
                trCalderaRow2.Visible = true;
                trCaldera2Row1.Visible = true;
                trXMF.Visible = false;
                trPackZ.Visible = false;
            }
            else if (cmbCMYKLibrary.SelectedValue.ToString() == "4")//XMF option
            {
                trCalderaRow1.Visible = false;
                trCalderaRow2.Visible = false;
                trCaldera2Row1.Visible = false;
                trXMF.Visible = true;
                trPackZ.Visible = false;
            }
            else if (cmbCMYKLibrary.SelectedValue.ToString() == "5")//PackZ option
            {
                trCalderaRow1.Visible = false;
                trCalderaRow2.Visible = false;
                trCaldera2Row1.Visible = false;
                trXMF.Visible = false;
                trPackZ.Visible = true;
            }
            else if (cmbCMYKLibrary.SelectedValue.ToString() == "6")//Revoria option
            {
                trCalderaRow1.Visible = false;
                trCalderaRow2.Visible = false;
                trCaldera2Row1.Visible = false;
                trXMF.Visible = false;
                trPackZ.Visible = false;
            }
            else if (cmbCMYKLibrary.SelectedValue.ToString() == "8")//Revoria flow option
            {
                trCalderaRow1.Visible = false;
                trCalderaRow2.Visible = false;
                trCaldera2Row1.Visible = false;
                trXMF.Visible = false;
                trPackZ.Visible = false;
            }
            else//ColorGate option
            {
                trCalderaRow1.Visible = false;
                trCalderaRow2.Visible = false;
                trCaldera2Row1.Visible = false;
                trXMF.Visible = false;
                trPackZ.Visible = false;
            }
        }

        #endregion       

        #region IViewExport Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public Project Project { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public short CurveTypeId { get; set; }
        public Business.Messages.CurveDataResponse CurveData { get; set; }
        public int SpotProjectId { get; set; }
        public DataTable dtPatchDetails { get; set; }
        public SpotProjectChartSetting SpotProjectChartSetting { get; set; }
        public string SpotProjectIDs { get; set; }
        public DataSet dsPatchDetails { get; set; }
        #endregion


    }
}