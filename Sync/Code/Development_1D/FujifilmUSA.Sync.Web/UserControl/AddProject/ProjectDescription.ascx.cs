using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using System.Linq;
namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProjectDescription : BaseControl
    {

        #region Private Variables - Globalization
        private const string _recrfvcLicenseType = "rfvcLicenseType";
        private const string _recrfvcMediaContainer = "rfvcMediaContainer";
        private const string _recrfvColorantContainer = "rfvColorantContainer";
        private const string _recrfvRipSettingsContainer = "rfvRipSettingsContainer";
        private const string _recoperationSelectText = "operationSelectText";
        #endregion

        public ProjectDescription()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadLicenseType();
                LoadInkOrder();
                LoadDeviceInfoFromSession();
                LoadProjectInfoFromSession();
            }
            if (IsXmfUser)
            {
                txtMedia.Enabled = false;
                txtColorant.Enabled = false;
                txtRipSetting.Enabled = false;
            }

            //Set localized text
            rfvRipSettingsContainer.ValidationMsg = GetResourceString(_recrfvRipSettingsContainer);
            rfvColorantContainer.ValidationMsg = GetResourceString(_recrfvColorantContainer);
            rfvcMediaContainer.ValidationMsg = GetResourceString(_recrfvcMediaContainer);
            rfvcLicenseType.ValidationMsg = GetResourceString(_recrfvcLicenseType);
        }
        #endregion

        #region Methods
        private void LoadLicenseType()
        {
            ddlLicenseType.Items.Clear();
            //Here we are supposed to load license type which can be accessed by user
            if (SessionManager.Instance.CurrentDevice != null
                && SessionManager.Instance.CurrentDevice.LicenseTypeProject != null
                    && SessionManager.Instance.CurrentDevice.LicenseTypeProject.Count > 0)
            {

                foreach (KeyValuePair<ELicenseType, int> item in SessionManager.Instance.CurrentDevice.LicenseTypeProject)
                {
                    ddlLicenseType.Items.Add(new System.Web.UI.WebControls.ListItem() { Text = Common.Utilities.GetEnumDescriptionAttributeValue(item.Key), Value = Convert.ToString((int)item.Key) });
                }

                ddlLicenseType.Items.Insert(0, new System.Web.UI.WebControls.ListItem() { Text = GetResourceString(_recoperationSelectText), Value = "-1" });
                ddlLicenseType.SelectedIndex = 0;
            }

            if (SessionManager.Instance.CurrentProject != null &&                
                SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.None)
            {
                ddlLicenseType.SelectedIndex = ddlLicenseType.Items.IndexOf(ddlLicenseType.Items.FindByValue(Convert.ToString((int)SessionManager.Instance.CurrentProject.LicenseType)));
                ddlLicenseType.Enabled = false;
            }
        }

        private void LoadInkOrder()
        {
            if (cmbInkOrder.DataSource != null) return;

            cmbInkOrder.DataValueField = "Key";
            cmbInkOrder.DataTextField = "Value";
            cmbInkOrder.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<EInkOrder>();
            cmbInkOrder.DataBind();

            cmbInkOrder.SelectedIndex = 0;
        }

        private void LoadDeviceInfoFromSession()
        {
            if (SessionManager.Instance.CurrentDevice.DeviceName != null)
            {
                txtPress.Text = SessionManager.Instance.CurrentDevice.DeviceName;
                txtDeviceFamily.Text = SessionManager.Instance.CurrentDevice.DeviceFamily.DeviceFamilyDesc;
            }
        }
        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.PrintCondition != null)
            {
                txtMedia.Text = SessionManager.Instance.CurrentProject.PrintCondition.Media;
                txtColorant.Text = SessionManager.Instance.CurrentProject.PrintCondition.Colorant;
                txtRipSetting.Text = SessionManager.Instance.CurrentProject.PrintCondition.RipSettings;
                txtNotes.Text = SessionManager.Instance.CurrentProject.PrintCondition.Notes;
                txtTechnicianName.Text = SessionManager.Instance.CurrentProject.PrintCondition.TechnicianName;

                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    Tr_OffsetPrintData.Visible = true;

                cmbInkOrder.SelectedIndex = cmbInkOrder.Items.IndexOf(cmbInkOrder.Items.FindByValue(Convert.ToString((int)SessionManager.Instance.CurrentProject.PrintCondition.InkOrder)));
                txtFountainSol.Text = SessionManager.Instance.CurrentProject.PrintCondition.FountainSolution;
                txtBlanket.Text = SessionManager.Instance.CurrentProject.PrintCondition.Blanket;
                txtBlanketAge.Text = SessionManager.Instance.CurrentProject.PrintCondition.BlanketAge;
                txtScreening.Text = SessionManager.Instance.CurrentProject.PrintCondition.Screening;
            }
        }

        #endregion

        #region Control Events
        protected void lnkbtnPrintDescription_Click(object sender, EventArgs e)
        {
            //Check for project limit 
            int currentProjectLimit = 0;
            if (SessionManager.Instance.ProjectList != null)
                currentProjectLimit = SessionManager.Instance.ProjectList.Where(p => p.LicenseType == (ELicenseType)Convert.ToInt32(ddlLicenseType.SelectedItem.Value)).Select(p => p.ProjectId).Count();

            int maxProjectLimit = 0;
            if (SessionManager.Instance.CurrentDevice.LicenseTypeProject != null)
                maxProjectLimit = SessionManager.Instance.CurrentDevice.LicenseTypeProject.Where(p => p.Key == (ELicenseType)Convert.ToInt32(ddlLicenseType.SelectedItem.Value)).Select(p => p.Value).FirstOrDefault();

            if (currentProjectLimit >= maxProjectLimit)
            {
                //--------------Project Limit reach, so can not create more projects under this Device now-------//  
                Response.Redirect("ProjectMaxLimit.aspx");
            }

            SessionManager.Instance.CurrentProject.PrintCondition = new FujifilmUSA.Sync.Entity.PrintCondition
            {
                Media = txtMedia.Text.Trim(),
                Colorant = txtColorant.Text.Trim(),
                RipSettings = txtRipSetting.Text.Trim(),
                Notes = txtNotes.Text.Trim(),
                TechnicianName = txtTechnicianName.Text.Trim(),
                InkOrder = (EInkOrder)Convert.ToInt32(cmbInkOrder.SelectedItem.Value),
                FountainSolution = txtFountainSol.Text.Trim(),
                Blanket = txtBlanket.Text.Trim(),
                BlanketAge = txtBlanketAge.Text.Trim(),
                Screening = txtScreening.Text.Trim()
            };

            SessionManager.Instance.CurrentProject.LicenseType = (ELicenseType)Convert.ToInt32(ddlLicenseType.SelectedItem.Value);
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.TargetColorSpace;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }
        #endregion

        #region Properties
        private bool IsXmfUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }
        #endregion

        protected void ddlLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((ELicenseType)Convert.ToInt32(ddlLicenseType.SelectedItem.Value) == ELicenseType.Sync1D)
            {
                Tr_OffsetPrintData.Visible = true;
            }
            else
            {
                Tr_OffsetPrintData.Visible = false;
            }
        }
    }
}
