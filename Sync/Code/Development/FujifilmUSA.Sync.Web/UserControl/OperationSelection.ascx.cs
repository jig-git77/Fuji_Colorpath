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
    public partial class OperationSelection : BaseControl
    {       
        #region "Page Events"
       
        protected void Page_Load(object sender, EventArgs e)
        {
         if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Device;
                lblDeviceName.Text = SessionManager.Instance.CurrentDevice.DeviceName;           

                spanAligmentCurves.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync1D);
                spanDLPOptimization.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.DLP);
                spanInkOptimization.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.InkOptimization);
                spanColorValidation.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Verification);
                spanICCProfiling.InnerHtml = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.IccProfiling);

                if (SessionManager.Instance.CurrentDevice.LicenseTypeProject.Count > 0)
                {
                    lbtnAligmentCurves.Enabled =
                    lbtnDLPOptimization.Enabled =
                    lbtnInkOptimization.Enabled =
                    lbtnColorValidation.Enabled =
                    lbtnICCProfiling.Enabled = false;

                    foreach (KeyValuePair<ELicenseType, int> license in SessionManager.Instance.CurrentDevice.LicenseTypeProject)
                    {
                        int val = license.Value;

                         switch (license.Key)
                         {
                             case ELicenseType.Sync1D:
                                 lbtnAligmentCurves.Enabled = true;
                                 Hidden_AligmentCurves.Value = val.ToString();
                                 break;
                             case ELicenseType.DLP:
                                 lbtnDLPOptimization.Enabled = true;
                                 Hidden_DLPOptimization.Value = val.ToString();
                                 break;
                             case ELicenseType.InkOptimization:
                                 lbtnInkOptimization.Enabled = true;
                                 Hidden_InkOptimization.Value = val.ToString();
                                 break;
                             case ELicenseType.Verification:
                                 lbtnColorValidation.Enabled = true;
                                 Hidden_ColorValidation.Value = val.ToString();
                                 break;
                             case ELicenseType.IccProfiling:
                                 lbtnICCProfiling.Enabled = true;
                                 Hidden_ICCProfiling.Value = val.ToString();
                                 break;
                         }
                    }

                    lbtnAligmentCurves.CssClass = lbtnAligmentCurves.Enabled ? "big_bt" : "disable_big_bt";
                    lbtnDLPOptimization.CssClass = lbtnDLPOptimization.Enabled ? "big_bt" : "disable_big_bt";
                    lbtnInkOptimization.CssClass = lbtnInkOptimization.Enabled ? "big_bt" : "disable_big_bt";
                    lbtnColorValidation.CssClass = lbtnColorValidation.Enabled ? "big_bt" : "disable_big_bt";
                    lbtnICCProfiling.CssClass = lbtnICCProfiling.Enabled ? "big_bt" : "disable_big_bt";
                }
            }
        }

        #endregion

        #region Control Events
        protected void lbtn_Click(object sender, EventArgs e)
        {           
            if (!IsErrorOccured)
            {             
                var lnkButton = (LinkButton)sender;
                switch (lnkButton.ID.ToUpper())
                {
                    case "LBTNALIGMENTCURVES":
                        SessionManager.Instance.CurrentOperation = ELicenseType.Sync1D;
                        SessionManager.Instance.CurrentProjLimit = Convert.ToInt32(Hidden_AligmentCurves.Value);
                        break;
                    case "LBTNDLPOPTIMIZATION":
                        SessionManager.Instance.CurrentOperation = ELicenseType.DLP;
                        SessionManager.Instance.CurrentProjLimit = Convert.ToInt32(Hidden_DLPOptimization.Value);
                        break;
                    case "LBTNINKOPTIMIZATION":
                        SessionManager.Instance.CurrentOperation = ELicenseType.InkOptimization;
                        SessionManager.Instance.CurrentProjLimit = Convert.ToInt32(Hidden_InkOptimization.Value);
                        break;
                    case "LBTNCOLORVALIDATION":
                        SessionManager.Instance.CurrentOperation = ELicenseType.Verification;
                        SessionManager.Instance.CurrentProjLimit = Convert.ToInt32(Hidden_ColorValidation.Value);
                        break;
                    case "LBTNICCPROFILING":
                        SessionManager.Instance.CurrentOperation = ELicenseType.IccProfiling;
                        SessionManager.Instance.CurrentProjLimit = Convert.ToInt32(Hidden_ICCProfiling.Value);
                        break;
                }              
                Response.RedirectPermanent("~/ProjectList.aspx");
            }
        }

        #endregion
     
    }
}

