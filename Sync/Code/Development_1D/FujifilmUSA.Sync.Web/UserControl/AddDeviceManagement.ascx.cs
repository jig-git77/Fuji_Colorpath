using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDeviceManagement : BaseControl, IViewAddDeviceManagement
    {
        public string ELicenseSync1DText { get; set; }
        public string ELicenseDlpText { get; set; }
        public string ELicenseInkOptimizationText { get; set; }
        public string ELicenseInkOptimizationSourceText { get; set; }
        public string ELicenseSync4DCertificationText { get; set; }
        public string ELicenseCustomSourceProfileText { get; set; }
        public string ELicenseReProfileCustomSourceText { get; set; }
        public string ELicenseFullGamutCustomSourceText { get; set; }
        public string ELicenseMultiColorCustomSourceText { get; set; }
        public string ELicenseSpotLibraryText { get; set; }
        public string ELicenseDLpPlusText { get; set; }
        public string ELicenseDLPMultiColorText { get; set; }
        public string ELicenseSync1DMultiColorText { get; set; }
        public string ELicenseCustomBCOText { get; set; }
        public string ELicenseCustomDestinationProfileText { get; set; }

        #region Private Variables - Globalization
        private const string _recAddDevice = "addDeviceText";
        private const string _recEditDevice = "editDeviceText";
        private const string _recSelectDeviceFamily = "selectDeviceFamilyText";
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recSelectBranch = "selectBranchText";
        private const string _recNoLicenseFound = "noLicenseFoundText";
        private const string _recPlzSelectCompany = "plzSelectCompanyText";
        private const string _recPlzSelectBranch = "plzSelectBranchText";
        private const string _recPlzSelectDeviceFamily = "plzSelectDeviceFamilyText";
        private const string _recSelect = "selectText";
        #endregion

        private PresenterDeviceManagement _presenterDeviceManagement;
        #region Declaration
        private int DId
        {
            get
            {
                return (ViewState["DeviceID"] != null && !string.IsNullOrEmpty(ViewState["DeviceID"].ToString())) ? Convert.ToInt32(ViewState["DeviceID"]) : 0;
            }
            set { ViewState["DeviceID"] = value.ToString(); }
        }
        #endregion
        public AddDeviceManagement()
        {
            Load += (sender, e) => ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterDeviceManagement = new PresenterDeviceManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            ELicenseSync1DText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync1D);
            ELicenseDlpText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.DLP);
            ELicenseInkOptimizationText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.InkOptimization);
            ELicenseInkOptimizationSourceText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.InkOptimizationSource);
            ELicenseSync4DCertificationText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync4DCertification);
            ELicenseCustomSourceProfileText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.CustomSourceProfile);
            ELicenseReProfileCustomSourceText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.ReProfileCustomSource);
            ELicenseFullGamutCustomSourceText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.FullGamutCustomSource);
            ELicenseMultiColorCustomSourceText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.MultiColorCustomSource);
            ELicenseSpotLibraryText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.SpotLibrary);
            ELicenseDLpPlusText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.DLPPlus);
            ELicenseDLPMultiColorText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.DLPMultiColor);
            ELicenseSync1DMultiColorText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.Sync1DMultiColor);
            ELicenseCustomBCOText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.CustomBCO);
            ELicenseCustomDestinationProfileText = Common.Utilities.GetEnumDescriptionAttributeValue(ELicenseType.CustomDestinationProfile);
            trTotalNoColor.Visible = false;
            trChannel.Visible = false;
            if (Page.IsPostBack)
            {
                return;
            }

            lblHeader.Text = GetResourceString(_recAddDevice); //"Add Device";
            Bindcompany();
            BindDeviceFamily();
            BindBranch();
            BindLicenses();
            BindMultiColorChannel();

            if (Request.QueryString["DeviceID"] != null)
            {
                var DeviceID = Request.QueryString["DeviceID"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(DeviceID, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _DeviceID;
                if (Int32.TryParse(id, out _DeviceID))
                {
                    lblHeader.Text = GetResourceString(_recEditDevice); //"Edit Device";
                    SetDevice(_DeviceID);
                }
            }
        }
        #endregion

        #region Methods
        private void BindDeviceFamily()
        {
            cmbDeviceFamily.Items.Clear();
            DeviceFamilyList = new Dictionary<int, string>();

            _presenterDeviceManagement.GetDeviceFamilyList();
            if (!IsErrorOccured)
            {
                cmbDeviceFamily.DataTextField = "Value";
                cmbDeviceFamily.DataValueField = "Key";
                cmbDeviceFamily.DataSource = DeviceFamilyList;
                cmbDeviceFamily.DataBind();
                cmbDeviceFamily.Items.Insert(0, new ListItem(GetResourceString(_recSelectDeviceFamily), string.Empty));//--Select Device Family--
            }
        }
        private void Bindcompany()
        {
            cmbCompany.Items.Clear();
            Compnaies = new Dictionary<int, string>();

            _presenterDeviceManagement.GetCompanies();
            if (!IsErrorOccured)
            {
                cmbCompany.DataTextField = "Value";
                cmbCompany.DataValueField = "Key";
                cmbCompany.DataSource = Compnaies;
                cmbCompany.DataBind();
                cmbCompany.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompany), string.Empty));//--Select Company--
            }
        }
        private void BindBranch()
        {
            cmbBranch.Items.Clear();
            if (cmbCompany.SelectedIndex != 0 && cmbCompany.SelectedValue != null && !string.IsNullOrEmpty(cmbCompany.SelectedValue))
            {
                Branches = new Dictionary<int, string>();
                CompanyId = Convert.ToInt32(cmbCompany.SelectedValue);

                _presenterDeviceManagement.GetBranches();
                if (!IsErrorOccured)
                {
                    cmbBranch.DataTextField = "Value";
                    cmbBranch.DataValueField = "Key";
                    cmbBranch.DataSource = Branches;
                    cmbBranch.DataBind();
                }
            }
            cmbBranch.Items.Insert(0, new ListItem(GetResourceString(_recSelectBranch), string.Empty)); //--Select Branch--
        }
        private void BindLicenses()
        {
            cmbLicense.Items.Clear();
            if (cmbBranch.SelectedIndex != 0 && cmbBranch.SelectedValue != null && !string.IsNullOrEmpty(cmbBranch.SelectedValue))
            {
                Licenses = new Dictionary<int, string>();
                BranchId = Convert.ToInt32(cmbBranch.SelectedValue);

                _presenterDeviceManagement.GetLicenses();
                if (!IsErrorOccured)
                {
                    cmbLicense.DataTextField = "Value";
                    cmbLicense.DataValueField = "Key";
                    cmbLicense.DataSource = Licenses;
                    cmbLicense.DataBind();
                    if (cmbLicense.Items.Count == 0)
                    {
                        fujiStatusbar.DisplayMessage(GetResourceString(_recNoLicenseFound), false);//No license found for this branch!
                        txtSync1D.Enabled = false;
                        txtDlp.Enabled = false;
                        txtInkOpt.Enabled = false;
                        txtInkOptSource.Enabled = false;
                        txt4DCertify.Enabled = false;
                        txtCustomSourceProfile.Enabled = false;
                        txtReProfileCustomSource.Enabled = false;
                        txtFullGamutCustomSource.Enabled = false;
                        txtMultiColorCustomSource.Enabled = false;
                        txtSpotLibrary.Enabled = false;
                        txtDlpPlus.Enabled = false;
                        txtDLPMultiColor.Enabled = false;
                        txtSync1DMultiColor.Enabled = false;
                        txtCustomBCO.Enabled = false;
                        txtCustomDestinationProfile.Enabled = false;
                    }
                    else
                    {
                        cmbLicense.SelectedIndex = 0;
                        txtSync1D.Text = txtDlp.Text = txtInkOpt.Text = txtInkOptSource.Text = txt4DCertify.Text = txtCustomSourceProfile.Text = txtReProfileCustomSource.Text = txtFullGamutCustomSource.Text = txtMultiColorCustomSource.Text = txtSpotLibrary.Text = txtDlpPlus.Text = txtDLPMultiColor.Text = txtSync1DMultiColor.Text = txtCustomBCO.Text = txtCustomDestinationProfile.Text = "0";
                        txtSync1D.Enabled = (License.LicenseType.Where(x => x == ELicenseType.Sync1D).Count() > 0);
                        txtDlp.Enabled = (License.LicenseType.Where(x => x == ELicenseType.DLP).Count() > 0);
                        txtInkOpt.Enabled = (License.LicenseType.Where(x => x == ELicenseType.InkOptimization).Count() > 0);
                        txtInkOptSource.Enabled = (License.LicenseType.Where(x => x == ELicenseType.InkOptimizationSource).Count() > 0);
                        txt4DCertify.Enabled = (License.LicenseType.Where(x => x == ELicenseType.Sync4DCertification).Count() > 0);
                        txtCustomSourceProfile.Enabled = (License.LicenseType.Where(x => x == ELicenseType.CustomSourceProfile).Count() > 0);
                        txtReProfileCustomSource.Enabled = (License.LicenseType.Where(x => x == ELicenseType.ReProfileCustomSource).Count() > 0);
                        txtFullGamutCustomSource.Enabled = (License.LicenseType.Where(x => x == ELicenseType.FullGamutCustomSource).Count() > 0);
                        txtMultiColorCustomSource.Enabled = (License.LicenseType.Where(x => x == ELicenseType.MultiColorCustomSource).Count() > 0);
                        txtSpotLibrary.Enabled = (License.LicenseType.Where(x => x == ELicenseType.SpotLibrary).Count() > 0);
                        txtDlpPlus.Enabled = (License.LicenseType.Where(x => x == ELicenseType.DLPPlus).Count() > 0);
                        txtDLPMultiColor.Enabled = (License.LicenseType.Where(x => x == ELicenseType.DLPMultiColor).Count() > 0);
                        txtSync1DMultiColor.Enabled = (License.LicenseType.Where(x => x == ELicenseType.Sync1DMultiColor).Count() > 0);
                        txtCustomBCO.Enabled = (License.LicenseType.Where(x => x == ELicenseType.CustomBCO).Count() > 0);
                        txtCustomDestinationProfile.Enabled = (License.LicenseType.Where(x => x == ELicenseType.CustomDestinationProfile).Count() > 0);
                    }
                }
            }
        }

        private void SaveDevice()
        {
            if (cmbCompany.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recPlzSelectCompany), false);//Please select company.
                return;
            }
            if (cmbBranch.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recPlzSelectBranch), false);//Please select branch.
                return;
            }
            if (cmbDeviceFamily.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recPlzSelectDeviceFamily), false);//Please select device family.
                return;
            }
            if (chkIsSupportMulticolor.Checked)
            {
                trTotalNoColor.Visible = true;

                if (drpTotalNoColor.SelectedValue == "0")
                {
                    fujiStatusbar.DisplayMessage("Please Select Total number of Color", false);
                    return;
                }
                if (Convert.ToInt16(drpTotalNoColor.SelectedValue) >= 0)
                {
                    trChannel.Visible = true;
                }
                if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 5)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 6)
                {
                    trChannel.Visible = true;
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 7)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }
                    if (drpChannel7.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel7", false);
                        return;
                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 8)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }
                    if (drpChannel7.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel7", false);
                        return;
                    }
                    if (drpChannel8.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel8", false);
                        return;
                    }

                }
                else if (Convert.ToInt16(drpTotalNoColor.SelectedValue) == 9)
                {
                    if (drpChannel5.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel5", false);
                        return;

                    }
                    if (drpChannel6.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel6", false);
                        return;
                    }
                    if (drpChannel7.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel7", false);
                        return;
                    }
                    if (drpChannel8.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel8", false);
                        return;
                    }
                    if (drpChannel9.SelectedValue == "0")
                    {
                        fujiStatusbar.DisplayMessage("Please select channel9", false);
                        return;

                    }

                }
            }

            int? licenseId = Convert.ToInt32(cmbLicense.SelectedValue.Equals(GetResourceString(_recSelect)) ? null : cmbLicense.SelectedValue);//--Select--
            Device = new Device()
            {
                DeviceName = txtDeviceName.Text,
                LicenseId = (int)licenseId,
                DeviceFamily =
                                 new DeviceFamily() { DeviceFamilyId = Convert.ToInt16(cmbDeviceFamily.SelectedValue) },
                IsActive = chkIsActive.Checked,
                IsValidation = chkIsValidation.Checked,
                IsFlexo = chkIsFlexo.Checked,
                IsSupportMulticolor = chkIsSupportMulticolor.Checked,



            };
            if (chkIsSupportMulticolor.Checked)
            {
                Device.TotalNoColor = Convert.ToInt16(drpTotalNoColor.SelectedValue.ToString());
                Device.Channel_5 = Convert.ToInt16(drpChannel5.SelectedValue.ToString());
                Device.Channel_6 = Convert.ToInt16(drpChannel6.SelectedValue.ToString());
                Device.Channel_7 = Convert.ToInt16(drpChannel7.SelectedValue.ToString());
                Device.Channel_8 = Convert.ToInt16(drpChannel8.SelectedValue.ToString());
                Device.Channel_9 = Convert.ToInt16(drpChannel9.SelectedValue.ToString());

            }
            if (txtSync1D.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.Sync1D, Convert.ToInt32(txtSync1D.Text));
            }

            if (txtCustomBCO.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.CustomBCO, Convert.ToInt32(txtCustomBCO.Text));
            }


            if (txtDlp.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.DLP, Convert.ToInt32(txtDlp.Text));
            }

            if (txtInkOpt.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.InkOptimization, Convert.ToInt32(txtInkOpt.Text));
            }

            if (txtInkOptSource.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.InkOptimizationSource, Convert.ToInt32(txtInkOptSource.Text));
            }

            if (txt4DCertify.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.Sync4DCertification, Convert.ToInt32(txt4DCertify.Text));
            }

            if (txtCustomSourceProfile.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.CustomSourceProfile, Convert.ToInt32(txtCustomSourceProfile.Text));
            }

            if (txtReProfileCustomSource.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.ReProfileCustomSource, Convert.ToInt32(txtReProfileCustomSource.Text));
            }

            if (txtFullGamutCustomSource.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.FullGamutCustomSource, Convert.ToInt32(txtFullGamutCustomSource.Text));
            }

            if (txtMultiColorCustomSource.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.MultiColorCustomSource, Convert.ToInt32(txtMultiColorCustomSource.Text));
            }

            if (txtSpotLibrary.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.SpotLibrary, Convert.ToInt32(txtSpotLibrary.Text));
            }
            if (txtDlpPlus.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.DLPPlus, Convert.ToInt32(txtDlpPlus.Text));
            }

            if (txtDLPMultiColor.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.DLPMultiColor, Convert.ToInt32(txtDLPMultiColor.Text));
            }
            if (txtSync1DMultiColor.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.Sync1DMultiColor, Convert.ToInt32(txtSync1DMultiColor.Text));
            }

            if (txtCustomDestinationProfile.Enabled)
            {
                Device.LicenseTypeProject.Add(ELicenseType.CustomDestinationProfile, Convert.ToInt32(txtCustomDestinationProfile.Text));
            }

            if (DId == 0) // Add New company
            {
                _presenterDeviceManagement.InsertDevice();
                if (IsErrorOccured)
                {
                    return;
                }
            }
            else // Edit New company
            {
                Device.DeviceId = DId;
                _presenterDeviceManagement.UpdateDevice();
                if (IsErrorOccured)
                {
                    return;
                }
            }
            Response.Redirect("DeviceManagement.aspx");
        }
        private void SetDevice(int deviceId)
        {
            Device = new Device { DeviceId = deviceId };
            _presenterDeviceManagement.GetDevice();
            if (!IsErrorOccured)
            {
                DId = deviceId;
                cmbCompany.SelectedValue = Device.License.Branch.Company.CompanyId.ToString();
                BindBranch();
                cmbBranch.SelectedValue = Device.License.Branch.BranchId.ToString();
                BindLicenses();
                cmbLicense.SelectedValue = Device.License.LicenseId.ToString();

                txtDeviceName.Text = Device.DeviceName;
                cmbDeviceFamily.SelectedValue = Device.DeviceFamilyId.ToString();
                chkIsActive.Checked = Device.IsActive;
                chkIsValidation.Checked = Device.IsValidation;
                chkIsFlexo.Checked = Device.IsFlexo;
                chkIsSupportMulticolor.Checked = Device.IsSupportMulticolor;
                if (Device.IsSupportMulticolor)
                {
                    trTotalNoColor.Visible = true;
                    trChannel.Visible = true;
                    drpTotalNoColor.SelectedValue = Device.TotalNoColor.ToString();
                    drpChannel5.SelectedValue = Device.Channel_5.ToString();
                    drpChannel6.SelectedValue = Device.Channel_6.ToString();
                    drpChannel7.SelectedValue = Device.Channel_7.ToString();
                    drpChannel8.SelectedValue = Device.Channel_8.ToString();
                    drpChannel9.SelectedValue = Device.Channel_9.ToString();
                    SetMultiColorChannel();
                }


                //-----------In Edit Device page, put MaxProjects value from DB----------------//
                foreach (KeyValuePair<ELicenseType, int> item in Device.LicenseTypeProject)
                {
                    int val = item.Value;

                    switch (item.Key)
                    {
                        case ELicenseType.None:
                            break;
                        case ELicenseType.Sync1D:
                            txtSync1D.Text = val.ToString();
                            break;
                        case ELicenseType.DLP:
                            txtDlp.Text = val.ToString();
                            break;
                        case ELicenseType.InkOptimization:
                            txtInkOpt.Text = val.ToString();
                            break;
                        case ELicenseType.InkOptimizationSource:
                            txtInkOptSource.Text = val.ToString();
                            break;
                        case ELicenseType.Sync4DCertification:
                            txt4DCertify.Text = val.ToString();
                            break;
                        case ELicenseType.CustomSourceProfile:
                            txtCustomSourceProfile.Text = val.ToString();
                            break;
                        case ELicenseType.ReProfileCustomSource:
                            txtReProfileCustomSource.Text = val.ToString();
                            break;
                        case ELicenseType.FullGamutCustomSource:
                            txtFullGamutCustomSource.Text = val.ToString();
                            break;
                        case ELicenseType.MultiColorCustomSource:
                            txtMultiColorCustomSource.Text = val.ToString();
                            break;
                        case ELicenseType.SpotLibrary:
                            txtSpotLibrary.Text = val.ToString();
                            break;
                        case ELicenseType.IccProfiling:
                            break;
                        case ELicenseType.DLPPlus:
                            txtDlpPlus.Text = val.ToString();
                            break;
                        case ELicenseType.DLPMultiColor:
                            txtDLPMultiColor.Text = val.ToString();
                            break;
                        case ELicenseType.Sync1DMultiColor:
                            txtSync1DMultiColor.Text = val.ToString();
                            break;
                        case ELicenseType.CustomBCO:
                            txtCustomBCO.Text = val.ToString();
                            break;
                        case ELicenseType.CustomDestinationProfile:
                            txtCustomDestinationProfile.Text = val.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void BindMultiColorChannel()
        {
            DataSet dataSet = _presenterDeviceManagement.GetMultiColorChannelList();
            if (dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    drpChannel5.DataSource = dataSet.Tables[0];
                    drpChannel5.DataValueField = "MultiColorChannelID";
                    drpChannel5.DataTextField = "MultiColorChannelName";
                    drpChannel5.DataBind();
                    drpChannel5.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel6.DataSource = dataSet.Tables[0];
                    drpChannel6.DataValueField = "MultiColorChannelID";
                    drpChannel6.DataTextField = "MultiColorChannelName";
                    drpChannel6.DataBind();
                    drpChannel6.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel7.DataSource = dataSet.Tables[0];
                    drpChannel7.DataValueField = "MultiColorChannelID";
                    drpChannel7.DataTextField = "MultiColorChannelName";
                    drpChannel7.DataBind();
                    drpChannel7.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel8.DataSource = dataSet.Tables[0];
                    drpChannel8.DataValueField = "MultiColorChannelID";
                    drpChannel8.DataTextField = "MultiColorChannelName";
                    drpChannel8.DataBind();
                    drpChannel8.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                    drpChannel9.DataSource = dataSet.Tables[0];
                    drpChannel9.DataValueField = "MultiColorChannelID";
                    drpChannel9.DataTextField = "MultiColorChannelName";
                    drpChannel9.DataBind();
                    drpChannel9.Items.Insert(0, new ListItem("---SELECT ANY----", "0"));

                }
            }

        }

        private void SetMultiColorChannel()
        {
            if (drpTotalNoColor.SelectedValue == "5")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = false;
                trChannel7.Visible = false;
                trChannel8.Visible = false;
                trChannel9.Visible = false;
                drpChannel6.SelectedValue = "0";
                drpChannel7.SelectedValue = "0";
                drpChannel8.SelectedValue = "0";
                drpChannel9.SelectedValue = "0";

            }
            else if (drpTotalNoColor.SelectedValue == "6")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = false;
                trChannel8.Visible = false;
                trChannel9.Visible = false;
                drpChannel7.SelectedValue = "0";
                drpChannel8.SelectedValue = "0";
                drpChannel9.SelectedValue = "0";
            }
            else if (drpTotalNoColor.SelectedValue == "7")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = true;
                trChannel8.Visible = false;
                trChannel9.Visible = false;

                drpChannel8.SelectedValue = "0";
                drpChannel9.SelectedValue = "0";
            }
            else if (drpTotalNoColor.SelectedValue == "8")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = true;
                trChannel8.Visible = true;
                trChannel9.Visible = false;
                drpChannel9.SelectedValue = "0";
            }
            else if (drpTotalNoColor.SelectedValue == "9")
            {
                trChannel5.Visible = true;
                trChannel6.Visible = true;
                trChannel7.Visible = true;
                trChannel8.Visible = true;
                trChannel9.Visible = true;
            }
        }
        #endregion

        #region Control Events
        protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranch();
            BindLicenses();
        }
        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindLicenses();
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveDevice();
        }
        #endregion

        #region Implement Interface
        public Device Device { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public Dictionary<int, string> Compnaies { get; set; }

        public Dictionary<int, string> Branches { get; set; }

        public Dictionary<int, string> Licenses { get; set; }

        public Dictionary<int, string> DeviceFamilyList { get; set; }

        public License License { get; set; }
        #endregion

        protected void chkIsSupportMulticolor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsSupportMulticolor.Checked)
            {
                trTotalNoColor.Visible = true;
            }
            else
            {
                trTotalNoColor.Visible = false;
            }
        }

        protected void drpTotalNoColor_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpTotalNoColor.SelectedIndex > 0)
            {
                trChannel.Visible = true;
                trTotalNoColor.Visible = true;
                SetMultiColorChannel();

            }
            else
            {
                trChannel.Visible = false;
            }

        }
    }
}