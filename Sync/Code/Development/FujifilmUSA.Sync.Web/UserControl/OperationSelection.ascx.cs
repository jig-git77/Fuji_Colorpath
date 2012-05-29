using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class OperationSelection : BaseControl, IViewDevice
    {
        #region "Declaration"
        private PresenterDevice _presenterdevice;

        public int DeviceId
        {
            get
            {
                if (ViewState["DeviceId"] == null)
                {
                    return 0;
                }
                return (int)ViewState["DeviceId"];
            }
            set
            {

                ViewState["DeviceId"] = value;

            }
        }

        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterdevice = new PresenterDevice(this);
        }              

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.lbl_Title.Text = "Device ID is: " + DeviceId;          

            DeviceId = Convert.ToInt32(DeviceId);
            CurrentUserId = SessionManager.Instance.CurrentUser.UserId;
            _presenterdevice.DeviceListByDeviceId();

            SessionManager.Instance.CurrentDevice = SelectedDevice;

            SessionManager.Instance.CurrentBranchName = SelectedDevice.License.Branch.BranchName.ToString();

            //------------Give by default stylesheet classes----------//
            lbtAligmentCurves.CssClass = "disable_Popup_bt";
            lbtnDLPOptimization.CssClass = "disable_Popup_bt";
            lbtInkOptimization.CssClass = "disable_Popup_bt";
            lbtnColorValidation.CssClass = "disable_Popup_bt";
            lbtnICCProfiling.CssClass = "disable_Popup_bt";

            //lbtAligmentCurves.CssClass = "Popup_bt_first";
            //lbtnDLPOptimization.CssClass = "Popup_bt";
            //lbtInkOptimization.CssClass = "Popup_bt";
            //lbtnColorValidation.CssClass = "Popup_bt";
            //lbtnICCProfiling.CssClass = "Popup_bt_last";

            //--------Start code for applying enabled buttons stylesheet classes----------//
            if (SessionManager.Instance.CurrentDevice.LicenseTypeProject.Count > 0)
            {
                foreach (KeyValuePair<ELicenseType, int> item in SessionManager.Instance.CurrentDevice.LicenseTypeProject)
                {
                    string key = Common.Utilities.GetEnumDescriptionAttributeValue(item.Key);
                    int val = item.Value;

                    if (item.Key.ToString() == "Sync1D")
                    {
                        spanAligmentCurves.InnerText = key;
                        lbtAligmentCurves.CssClass = "Popup_bt_first";
                        lbtAligmentCurves.OnClientClick = "";
                        Hidden_AligmentCurves.Value = val.ToString();
                    }
                    else if (item.Key.ToString() == "DLP")
                    {
                        spanDLPOptimization.InnerText = key;
                        lbtnDLPOptimization.CssClass = "Popup_bt";
                        lbtnDLPOptimization.OnClientClick = "";
                        Hidden_DLPOptimization.Value = val.ToString();
                    }
                    else if (item.Key.ToString() == "InkOptimization")
                    {
                        spanInkOptimization.InnerText = key;
                        lbtInkOptimization.CssClass = "Popup_bt";
                        lbtInkOptimization.OnClientClick = "";
                        Hidden_InkOptimization.Value = val.ToString();
                    }
                    else if (item.Key.ToString() == "Verification")
                    {
                        spanColorValidation.InnerText = key;
                        lbtnColorValidation.CssClass = "Popup_bt";
                        lbtnColorValidation.OnClientClick = "";
                        Hidden_ColorValidation.Value = val.ToString();
                    }
                    //Response.Write(key.ToString() + "     " + val.ToString() +"<br/>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        #endregion

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


        #region IViewDevice Members   
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public MeasurementDevice MeasurementDevice { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
        public Dictionary<int, string> BranchList { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public Dictionary<int, string> LicenseList { get; set; }
        public Dictionary<int, string> InstrumentTypeList { get; set; }
        public List<Device> DeviceListById { get; set; }      
        public Device SelectedDevice { get; set; }
        #endregion
    }
}

