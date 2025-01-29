using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDeviceProfilerSettings : BaseControl, IViewDeviceProfilerSettings
    {
        private PresenterDeviceProfilerSettings _presenterDeviceProfilerSettings;
        private const string _recSelectDeviceFamily = "selectDeviceFamily";
        private const string _recSelectProjectType = "selectProjectType";
        private const string _recAddDeviceProfilerOptionTitle = "lblHeaderResource1.Text";
        private const string _recEditDeviceProfilerOptionTitle = "lblEditHeaderResource1";

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterDeviceProfilerSettings = new PresenterDeviceProfilerSettings(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            lblHeader.Text = GetResourceString(_recAddDeviceProfilerOptionTitle); //"Add Device Family Profiler Presets";
            if (IsPostBack) return;
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            BindDeviceFamily();
            BindProjectType();
            if (Request.QueryString["DeviceFamilyProfilerOptionID"] != null)
            {
                var DeviceFamilyProfilerOptionID = Request.QueryString["DeviceFamilyProfilerOptionID"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(DeviceFamilyProfilerOptionID, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _DeviceFamilyProfilerOptionID;
                if (Int32.TryParse(id, out _DeviceFamilyProfilerOptionID))
                {
                    lblHeader.Text = GetResourceString(_recEditDeviceProfilerOptionTitle);//"Edit Device Family Profiler Presets";
                    LoadProfilerOptionPreset(_DeviceFamilyProfilerOptionID);
                }
            }
        }      
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            short profilerTypeID = 2;
            short emptyLine = 0;
            if (fnUploadProfileSettings.UploadedFiles.Count > 0)
            {
                var sr = new StreamReader(fnUploadProfileSettings.UploadedFiles[0].InputStream);
                List<ProfilerOptionPreset> lstProfilerOptionPreset = new List<ProfilerOptionPreset>();
                var rows = 0;

                while (!sr.EndOfStream)
                {
                    bool bEmptyLine = false;
                    rows++;
                    ProfilerOptionPreset tmpProfilerOptionPreset = new ProfilerOptionPreset();
                    if (rows == 1)
                    {
                        #region Column Validation
                        var columns = sr.ReadLine().Split(',');
                        if (columns.Length != 3)
                        {
                            throw new Exception(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatch"));
                        }
                        columns.ToList().ForEach(x => x = x.Trim());
                        if (!columns[0].Equals("ProfilerOptionID", StringComparison.CurrentCultureIgnoreCase) ||
                            !columns[1].Equals("ProfilerOptionDesc", StringComparison.CurrentCultureIgnoreCase) ||
                            !columns[2].Equals("ProfilerOptionValue", StringComparison.CurrentCultureIgnoreCase)
                          )
                        {
                            throw new Exception(FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnNamesNotMatch"));
                        }
                    }
                    else
                    {
                        var dataRow = sr.ReadLine().Split(',');
                        if (dataRow.Length != 3)
                        {
                            throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnCountNotMatchOnRow"), rows));
                        }
                        dataRow.ToList().ForEach(x => x = x.Trim());
                        short profilerOptionID = 0;

                        if (string.IsNullOrEmpty(dataRow[1]) && string.IsNullOrEmpty(dataRow[2]) && string.IsNullOrEmpty(dataRow[1]))
                        { emptyLine++; bEmptyLine = true; }

                        //Empty line is given between profile types. if not more than 2 empty lines proceed
                        if ((emptyLine < 3) && !bEmptyLine)
                        {
                            if (short.TryParse(dataRow[0].ToString(), out profilerOptionID))
                            {
                                tmpProfilerOptionPreset.ProfilerOption = new ProfilerOption();
                                tmpProfilerOptionPreset.ProfilerOption.ProfilerOptionId = profilerOptionID;
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnConvertFailedOnRow"), rows));
                            }

                            //ProfilerOptionDesc is not needed but cannot be empty                               
                            if ((!string.IsNullOrEmpty(dataRow[2])) || (string.IsNullOrEmpty(dataRow[1])))
                            {
                                tmpProfilerOptionPreset.ProfilerOptionValue = dataRow[2];
                            }
                            else
                            {
                                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_ReferenceChart_ColumnEmptyValueOnRow"), rows));
                            }
                        #endregion

                            if (profilerOptionID == 1)
                            {
                                switch (dataRow[2].ToUpper())
                                {
                                    case "PRINTER":
                                        profilerTypeID = (short)EProfileType.DestinationProfile;
                                        break;
                                    case "DEVICELINK":
                                        profilerTypeID = (short)EProfileType.DeviceLinkProfile;
                                        break;
                                    case "OPTIMIZEDEVICELINK":
                                        profilerTypeID = (short)EProfileType.OptimizeDlp;
                                        break;
                                }
                            }

                            tmpProfilerOptionPreset.ProfileTypeId = profilerTypeID;
                            tmpProfilerOptionPreset.DeviceFamilyProfilerOptionId = 0;
                            tmpProfilerOptionPreset.ProfilerOptionPresetId = 0;

                            lstProfilerOptionPreset.Add(tmpProfilerOptionPreset);
                        }
                    }
                }
                if (hdnDeviceFamilyProfilerOptionID.Value == "0")
                {
                    AddProfilerOptionSettings(lstProfilerOptionPreset);
                }
                else
                {
                    UpdateProfilerOptionSettings(lstProfilerOptionPreset);
                }
            }
            else
            {
                ///-----------Missing message for Upload .csv file
                fujiStatusbar.DisplayMessage(Common.ResourceManager.GetResourceMessage("Error_ProfilerSettings_MissingUploadFile"), false);
            }
        }
        #endregion

        #region Methods
        private void AddProfilerOptionSettings(List<ProfilerOptionPreset> lstProfilerOptionPreset)
        {
            DeviceFamilyProfilerOption = new DeviceFamilyProfilerOption();
            DeviceFamilyProfilerOption.DeviceFamilyId = Convert.ToInt16(ddlDeviceFamily.SelectedValue.ToString());
            DeviceFamilyProfilerOption.ProfilerOptionsName = txtPresetName.Text.Trim();
            DeviceFamilyProfilerOption.ProfilerOptionsDesc = txtPresetDesc.Text.Trim();
            DeviceFamilyProfilerOption.ProjectTypeID = Convert.ToInt16(ddlProjectType.SelectedValue.ToString());
            DeviceFamilyProfilerOption.IsActive = chkIsActive.Checked;
            var rows = 0;
            bool bDestProfileType = false;
            bool bDevProfileType = false;
            bool bOptProfileType = false;

            #region Validation for ProfileTypes
            List<ProfilerOptionPreset> lstFilter = new List<ProfilerOptionPreset>();
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.DestinationProfile);
            if (lstFilter.Count > 0)
                bDestProfileType = true;
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.DeviceLinkProfile);
            if (lstFilter.Count > 0)
                bDevProfileType = true;
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.OptimizeDlp);
            if (lstFilter.Count > 0)
                bOptProfileType = true;
            #endregion

            if (!(bDestProfileType && bDevProfileType && bOptProfileType))
                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_CSV_ProfilerOptionsPreset"), rows));
            else
            {
                DeviceFamilyProfilerOption.ProfilerOptionPresets = (lstProfilerOptionPreset.
                                                            Select(x => new ProfilerOptionPreset()
                                                            {
                                                                ProfileTypeId = x.ProfileTypeId,
                                                                ProfilerOption = x.ProfilerOption,
                                                                ProfilerOptionValue = x.ProfilerOptionValue
                                                            }).ToList());
                _presenterDeviceProfilerSettings.InsertDeviceFamilyProfilerOption();
                if (IsErrorOccured) return;
                Response.Redirect("ProfilerOptionPresetsManagement.aspx");
            }
        }
        private void UpdateProfilerOptionSettings(List<ProfilerOptionPreset> lstProfilerOptionPreset)
        {
            DeviceFamilyProfilerOption = new DeviceFamilyProfilerOption();
            DeviceFamilyProfilerOption.DeviceFamilyId = Convert.ToInt16(ddlDeviceFamily.SelectedValue.ToString());
            DeviceFamilyProfilerOption.ProfilerOptionsName = txtPresetName.Text.Trim();
            DeviceFamilyProfilerOption.ProfilerOptionsDesc = txtPresetDesc.Text.Trim();
            DeviceFamilyProfilerOption.ProjectTypeID = Convert.ToInt16(ddlProjectType.SelectedValue.ToString());
            DeviceFamilyProfilerOption.IsActive = chkIsActive.Checked;
            DeviceFamilyProfilerOption.DeviceFamilyProfilerOptionId = Convert.ToInt32(hdnDeviceFamilyProfilerOptionID.Value);
            var rows = 0;
            bool bDestProfileType = false;
            bool bDevProfileType = false;
            bool bOptProfileType = false;

            #region Validation for ProfileTypes
            List<ProfilerOptionPreset> lstFilter = new List<ProfilerOptionPreset>();
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.DestinationProfile);
            if (lstFilter.Count > 0)
                bDestProfileType = true;
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.DeviceLinkProfile);
            if (lstFilter.Count > 0)
                bDevProfileType = true;
            lstFilter = lstProfilerOptionPreset.FindAll(item => item.ProfileTypeId == (short)EProfileType.OptimizeDlp);
            if (lstFilter.Count > 0)
                bOptProfileType = true;
            #endregion

            if (!(bDestProfileType && bDevProfileType && bOptProfileType))
                throw new Exception(string.Format(Common.ResourceManager.GetResourceMessage("Error_CSV_ProfilerOptionsPreset"), rows));
            else
            {
                DeviceFamilyProfilerOption.ProfilerOptionPresets = (lstProfilerOptionPreset.
                                                            Select(x => new ProfilerOptionPreset()
                                                            {
                                                                ProfileTypeId = x.ProfileTypeId,
                                                                ProfilerOption = x.ProfilerOption,
                                                                ProfilerOptionValue = x.ProfilerOptionValue
                                                            }).ToList());
                _presenterDeviceProfilerSettings.UpdateDeviceFamilyProfilerOption();
                if (IsErrorOccured) return;
                Response.Redirect("ProfilerOptionPresetsManagement.aspx");
            }
        }

        private void BindProjectType()
        {
            ddlProjectType.Items.Clear();
            ProjectTypeList = new Dictionary<int, string>();
            _presenterDeviceProfilerSettings.GetProjectTypeList();
            if (!IsErrorOccured)
            {
                ddlProjectType.DataSource = ProjectTypeList;
                ddlProjectType.DataBind();
                ddlProjectType.Items.Insert(0, new ListItem(GetResourceString(_recSelectProjectType), string.Empty));//--Select Project Type--
            }
        }


        private void BindDeviceFamily()
        {
            ddlDeviceFamily.Items.Clear();
            DeviceFamilyList = new Dictionary<int, string>();
            _presenterDeviceProfilerSettings.GetDeviceFamilyList();
            if (!IsErrorOccured)
            {
                ddlDeviceFamily.DataSource = DeviceFamilyList;
                ddlDeviceFamily.DataBind();
                ddlDeviceFamily.Items.Insert(0, new ListItem(GetResourceString(_recSelectDeviceFamily), string.Empty));//--Select Device Family--
            }
        }
        private void LoadProfilerOptionPreset(int iDeviceFamilyProfilerOptionID)
        {
            PresetsResponse = new ProfilerOptionPresetsResponse { DeviceFamilyProfilerOptionID = iDeviceFamilyProfilerOptionID };
            _presenterDeviceProfilerSettings.GetProfilerOptionPreset();
            if (!IsErrorOccured)
            {

                hdnDeviceFamilyProfilerOptionID.Value = Convert.ToString(iDeviceFamilyProfilerOptionID);
                ddlDeviceFamily.SelectedValue = Convert.ToString(PresetsResponse.DeviceFamilyID);
                ddlProjectType.SelectedValue = Convert.ToString(PresetsResponse.ProfileTypeId);
                txtPresetName.Text = PresetsResponse.PresetName;
                txtPresetDesc.Text = PresetsResponse.ProfilerOptionsDesc;
                //cmbProfileCategory.SelectedValue = Convert.ToString(SourceProfile.Category);
                chkIsActive.Checked = (PresetsResponse.IsActive == true ? true : false);
            }
        }
        #endregion

        #region IViewAddProfilerSettings Members
        public ProfilerOptionPreset ProfilerOptionPreset { get; set; }
        public Dictionary<int, string> DeviceFamilyList { get; set; }
        public Dictionary<int, string> ProjectTypeList { get; set; }
        public DeviceFamilyProfilerOption DeviceFamilyProfilerOption { get; set; }
        public int DeviceFamilyProfilerOptionID { get; set; }
        public ProfilerOptionPresetsResponse PresetsResponse { get; set; }
        public List<ProfilerOptionPresetsResponse> ProfilerOptionPresetsResponse { get; set; }
        #endregion
    }
}