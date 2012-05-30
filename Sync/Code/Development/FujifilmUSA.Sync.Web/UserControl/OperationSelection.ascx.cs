using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class OperationSelection : BaseControl, IViewOperationSelection
    {
        #region "Declaration"
        private PresenterOperationSelection _presenterOperationSelection;
        private const string _vsDeviceId = "_DeviceId";
        public Dictionary<ELicenseType, int> LicenseTypeProject { get; set; }

        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterOperationSelection = new PresenterOperationSelection(this);
        }

        protected override void OnPreRender(EventArgs e)
        {
            spanAligmentCurves.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync1D);
            spanDLPOptimization.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.DLP);
            spanInkOptimization.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.InkOptimization);
            spanColorValidation.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Verification);
            spanICCProfiling.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.IccProfiling);

            if (LicenseTypeProject != null)
            {
                lbtnAligmentCurves.Enabled =
                    lbtnDLPOptimization.Enabled =
                    lbtnInkOptimization.Enabled =
                    lbtnColorValidation.Enabled =
                    lbtnICCProfiling.Enabled = false;

                foreach (KeyValuePair<ELicenseType, int> license in LicenseTypeProject)
                {
                    switch (license.Key)
                    {
                        case ELicenseType.Sync1D:
                            lbtnAligmentCurves.Enabled = true;
                            break;
                        case ELicenseType.DLP:
                            lbtnDLPOptimization.Enabled = true;
                            break;
                        case ELicenseType.InkOptimization:
                            lbtnInkOptimization.Enabled = true;
                            break;
                        case ELicenseType.Verification:
                            lbtnColorValidation.Enabled = true;
                            break;
                        case ELicenseType.IccProfiling:
                            lbtnICCProfiling.Enabled = true;
                            break;
                    }
                }

                lbtnAligmentCurves.CssClass = lbtnAligmentCurves.Enabled ? "Popup_bt_first" : "disable_Popup_bt";
                lbtnDLPOptimization.CssClass = lbtnDLPOptimization.Enabled ? "Popup_bt" : "disable_Popup_bt";
                lbtnInkOptimization.CssClass = lbtnInkOptimization.Enabled ? "Popup_bt" : "disable_Popup_bt";
                lbtnColorValidation.CssClass = lbtnColorValidation.Enabled ? "Popup_bt" : "disable_Popup_bt";
                lbtnICCProfiling.CssClass = lbtnICCProfiling.Enabled ? "Popup_bt_last" : "disable_Popup_bt";

            }
        }

        #endregion

        #region Control Events
        protected void lbtn_Click(object sender, EventArgs e)
        {
            _presenterOperationSelection.GetDeviceById();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurrentDevice = Device;
                var lnkButton = (LinkButton)sender;
                switch (lnkButton.ID.ToUpper())
                {
                    case "LBTNALIGMENTCURVES":
                        SessionManager.Instance.CurrentOperation = ELicenseType.Sync1D;
                        break;
                    case "LBTNDLPOPTIMIZATION":
                        SessionManager.Instance.CurrentOperation = ELicenseType.DLP;
                        break;
                    case "LBTNINKOPTIMIZATION":
                        SessionManager.Instance.CurrentOperation = ELicenseType.InkOptimization;
                        break;
                    case "LBTNCOLORVALIDATION":
                        SessionManager.Instance.CurrentOperation = ELicenseType.Verification;
                        break;
                    case "LBTNICCPROFILING":
                        SessionManager.Instance.CurrentOperation = ELicenseType.IccProfiling;
                        break;
                }
                SessionManager.Instance.CurrentProjLimit = Device.LicenseTypeProject.Single(x => x.Key == SessionManager.Instance.CurrentOperation).Value;
                Response.RedirectPermanent("~/ProjectList.aspx");
            }
        }

        #endregion

        #region IViewOperationSelection Members

        public int DeviceId
        {
            get
            {
                if (ViewState[_vsDeviceId] == null)
                {
                    return 0;
                }
                return (int)ViewState[_vsDeviceId];
            }
            set
            {
                ViewState[_vsDeviceId] = value;
            }
        }

        public Device Device { get; set; }

        #endregion
    }
}

