using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class OperationSelection : BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Device;
                lblDeviceName.Text = SessionManager.Instance.CurrentDevice.DeviceName;
               
                if (SessionManager.Instance.CurrentDevice.LicenseTypeProject.Count > 0)
                {                   

                    foreach (KeyValuePair<ELicenseType, int> item in SessionManager.Instance.CurrentDevice.LicenseTypeProject)
                    {
                        string key = Common.Utilities.GetEnumDescriptionAttributeValue(item.Key);
                        int val = item.Value;

                        if (item.Key.ToString() == "Sync1D")
                        {
                            spanAligmentCurves.InnerText = key;
                            lbtAligmentCurves.CssClass = "big_bt";
                            lbtAligmentCurves.OnClientClick = "";
                            Hidden_AligmentCurves.Value = val.ToString();
                        }
                        else if (item.Key.ToString() == "DLP")
                        {
                            spanDLPOptimization.InnerText = key;
                            lbtnDLPOptimization.CssClass = "big_bt";
                            lbtnDLPOptimization.OnClientClick = "";
                            Hidden_DLPOptimization.Value = val.ToString();
                        }
                        else if (item.Key.ToString() == "InkOptimization")
                        {
                            spanInkOptimization.InnerText = key;
                            lbtInkOptimization.CssClass = "big_bt";
                            lbtInkOptimization.OnClientClick = "";
                            Hidden_InkOptimization.Value = val.ToString();
                        }
                        else if (item.Key.ToString() == "Verification")
                        {
                            spanColorValidation.InnerText = key;
                            lbtnColorValidation.CssClass = "big_bt";
                            lbtnColorValidation.OnClientClick = "";
                            Hidden_ColorValidation.Value = val.ToString();
                        }
                        //Response.Write(key.ToString() + "     " + val.ToString() +"<br/>");
                    }
                }
            }
        }
        protected void lbtnDLPOptimization_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentOperation = EOperation.DlpOptimization;
            SessionManager.Instance.CurrentProjLimit = Convert.ToInt32(Hidden_DLPOptimization.Value);
            Response.Redirect("~/ProjectList.aspx");
        }

        protected void lbtInkOptimization_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentOperation = EOperation.InkOptimization;
            SessionManager.Instance.CurrentProjLimit = Convert.ToInt32(Hidden_InkOptimization.Value);
            Response.Redirect("~/ProjectList.aspx");
        }
    }
}

