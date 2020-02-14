using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementCommonNew : BaseControl, IViewMeasurement
    {

        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        public int JsRandomNumber { get { return new Random().Next(1, 100); } }

        //public void SetVerificationControlVisibility()
        //{
        //    tdNotes.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    tdNotesCaption.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    trCustomer.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    trValidationDetails.Visible = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) ? false : (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync1D && SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
        //        lblMeasurementName.Text = GetLocalResourceObject("JobNameCaption").ToString();
        //    else
        //        lblMeasurementName.Text = GetLocalResourceObject("MeasurementNameCaption").ToString();
        //    hdnPCSType.Value = Convert.ToInt32(SessionManager.Instance.CurrentProject.PCSDataTypes).ToString();
        //}
        #endregion

        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsXmfUser && SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    //FillValidationDetailsFromXMF();
                }

                hdnBasePluginServiceUrl.Value = Utilities.ReadConfigValue("BasePluginServiceURL");
                hdIsVerification.Value = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation).ToString().ToLower();
                hdLicenseType.Value = ((int)SessionManager.Instance.CurrentProject.LicenseType).ToString();
                hdfAuthToken.Value = SessionManager.Instance.CurrentUser.APIAuthKey;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary && SessionManager.Instance.CurrentProject.SpotTargetProject != null)
                {
                    hdProjectId.Value = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId.ToString();
                }
                else
                {
                    hdProjectId.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                }

                hdOptimizeRunId.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
                hdDeviceType.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.InstrumentTypeId.ToString();
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key.ToString();
                    hdReferenceChartDisplayType.Value = Convert.ToInt32(EReferenceChartDisType.ValidationChart).ToString();
                }
                else if (SessionManager.Instance.CurrentProject.CurrDlpId > 0)
                {
                    hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString();
                    hdReferenceChartDisplayType.Value = Convert.ToInt32(EReferenceChartDisType.OptimizationChart).ToString();
                }
                else
                {
                    hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key.ToString();
                    hdReferenceChartDisplayType.Value = Convert.ToInt32(EReferenceChartDisType.ProfileChart).ToString();
                }
                hdHasMeasurement.Value = SessionManager.Instance.CurrentOptimizeRun.HasMeasurements.ToString();
                //----------------"Import Measurements" button only available for "Sync4D" or "Ink Optimization" Project Type and only for initial 1st run (Not shown in any type of Validation)--------------//
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimization)
                {
                    if (SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation)
                    {
                        if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                        {
                            if (SessionManager.Instance.CurrentProject.EnabledPaperScaling == false) //---If project has paper scalling enabled, then don't give option to import measurements
                            {
                                hdDisplayImport.Value = "yes";
                            }
                        }
                    }
                }
                hdSpotProjectType.Value = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary).ToString().ToLower();
            }
            //SetVerificationControlVisibility();

            //show the project details doe native app            
            lblPID.Text = SessionManager.Instance.CurrentOptimizeRun.ProjectId.ToString();
            lblORID.Text = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
            lblSN.Text = SessionManager.Instance.CurrentProject.MeasurementSetting.DeviceSerialNo;

            string projectType = "Profiler";
            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
            {
                if (hdReferenceChartDisplayType.Value == "2")
                {
                    projectType = "Optimization";
                }
                else if (hdReferenceChartDisplayType.Value == "3")
                {
                    projectType = "Validation";
                }
            }
            else
            {
                projectType = "Spot Project";
            }

            ProjectId = SessionManager.Instance.CurrentOptimizeRun.ProjectId;
            CurrentUserId = SessionManager.Instance.CurrentUser.UserId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            ProfileType = projectType;
            DeviceSerialNo = SessionManager.Instance.CurrentProject.MeasurementSetting.DeviceSerialNo;
            InstrumentTypeID = Convert.ToInt16(SessionManager.Instance.CurrentProject.MeasurementSetting.InstrumentTypeId);
            LicenseType = SessionManager.Instance.CurrentProject.LicenseType;

            
            if (SessionManager.Instance.CurrentProject.PrintCondition != null)
            {
                if (SessionManager.Instance.CurrentProject.PrintCondition.IsCustomPlugin)
                {
                    bool resultMesaurment = _presenterMeasurementSetting.CheckMeasurementPluginStatus();
                    if (resultMesaurment)
                    {
                        GetMToolMessage(1);                        
                        btnAddPluginStatus.CssClass = "green_btnotactive";
                        //btnAddPluginStatus.Enabled = false;
                    }
                    else
                    {
                        GetMToolMessage(0);                        
                        btnAddPluginStatus.CssClass = "green_bt";
                        //btnAddPluginStatus.Enabled = true;
                    }

                }
            }
            if (SessionManager.Instance.CurrentProject.SpotTargetProject != null)
            {
                if (SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.IsCustomPlugin)
                {
                    bool resultMesaurment = _presenterMeasurementSetting.CheckMeasurementPluginStatus();
                    if (resultMesaurment)
                    {
                        GetMToolMessage(1);                        
                        btnAddPluginStatus.CssClass = "green_btnotactive";
                        //btnAddPluginStatus.Enabled = false;
                    }
                    else
                    {
                        GetMToolMessage(0);
                        btnAddPluginStatus.CssClass = "green_bt";                        
                        //btnAddPluginStatus.Enabled = true;
                    }

                } 
            }
            hdnbtnAnalyzeParent.Value = ((LinkButton)NamingContainer.FindControl("btnAnalyze")).ClientID;
        }

        protected void btnAddPluginStatus_Click(object sender, EventArgs e)
        {
            try
            {
                int result = _presenterMeasurementSetting.CreateMeasurementPluginStatus();
                if (result > 0)
                {
                    GetMToolMessage(1);
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    lblMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        //private void FillValidationDetailsFromXMF()
        //{
        //    if (SessionManager.Instance.CurrentValidationRequest != null)
        //    {
        //        txtMeasurementName.Text = SessionManager.Instance.CurrentValidationRequest.JobName;
        //        txtMeasurementName.Enabled = false;
        //        txtJDFJobId.Text = SessionManager.Instance.CurrentValidationRequest.JDFJobId;
        //        txtJDFJobId.Enabled = false;
        //        txtPageOrSurface.Text = SessionManager.Instance.CurrentValidationRequest.PageOrSurface;
        //        txtPageOrSurface.Enabled = false;
        //        txtCustomer.Text = SessionManager.Instance.CurrentValidationRequest.Customer;
        //        txtCustomer.Enabled = false;
        //    }
        //}


        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordExists"></param>
        /// <returns></returns>
        public void GetMToolMessage(byte recordExists)
        {
            StringBuilder str = new StringBuilder();
            if (recordExists == 1)
            {
                str.Append("<b>This project is currently activated for measurement.</b>");
                str.Append("<br />");
                str.Append("<b>Measure charts and upload using the ColorPath MTool, Return here when you're done.</b>");
                
            }
            else
            {
                str.Append("<b>This project is not currently activated for measurement.</b>");
                
            }
            lblMessage.Text = str.ToString();
        }
        #endregion
        #region Properties
        public bool IsXmfUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }
        #endregion

        #region IViewMeasurement
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public string ProfileType { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }
        public List<Entity.MeasurementLog> MeasurementLogs { get; set; }

        #endregion

       
    }
}
