using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMToolVersionManagement : BaseControl, IViewMToolVersionManagement
    {
        #region Declartion
        private PresenterMToolVersionManagement _presenterMToolVersionManagement;
        private short OSPlatDetailId
        {
            get
            {
                return (ViewState["OSPlatformDetailsID"] != null && !string.IsNullOrEmpty(ViewState["OSPlatformDetailsID"].ToString())) ? Convert.ToInt16(ViewState["OSPlatformDetailsID"]) : Convert.ToInt16(0);
            }
            set { ViewState["OSPlatformDetailsID"] = value.ToString(); }
        }
        public AddMToolVersionManagement()
        {
            Load += (sender, e) => ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Event
        protected override void OnInit(EventArgs e)
        {
            _presenterMToolVersionManagement = new PresenterMToolVersionManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack)
            {
                return;
            }

            lblHeader.Text = "Add MTool Verion";
             BindOSPlatformDropDown();
            if (Request.QueryString["OSPlatformDetailsID"] != null)
            {
                var OSPlatformDetailsID = Request.QueryString["OSPlatformDetailsID"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(OSPlatformDetailsID, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                short _OSPlatformDetailsID;
                if (Int16.TryParse(id, out _OSPlatformDetailsID))
                {
                    cmbOSPlatForm.Enabled = false;
                    SetMToolVerion(_OSPlatformDetailsID);
                    lblHeader.Text = "Edit MTool Version";
                }
            }
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveMToolVerion();
        }
        #endregion


        #region Method
        private void BindOSPlatformDropDown()
        {
            _presenterMToolVersionManagement.GetMToolOSPlatform();
            if (!IsErrorOccured)
            {
                cmbOSPlatForm.DataSource = OSPlatformNameList;
                cmbOSPlatForm.DataBind();
                cmbOSPlatForm.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        private void SaveMToolVerion()
        {
            MToolVersion = new MToolVersionUpdateList
            {
                OSPlatformID = Convert.ToInt16(cmbOSPlatForm.SelectedValue),
                LatestVersion = txtLatestVersion.Text,
                MinReqVersion = txtMinReqVersion.Text,
                URLlocation = txtURLlocation.Text,
                UpdateInfo = txtUpdateInfo.Text,
                IsUsed = chkIsUsed.Checked
            };
            if (OSPlatDetailId == 0) // Add New Record MToolVersionManagement
            {
                _presenterMToolVersionManagement.InsertMToolUpdateVersion();
                if (IsErrorOccured)
                {
                    return;
                }
            }
            else // Edit New MToolVersionManagement
            {
                MToolVersion.OSPlatformDetailsID = OSPlatDetailId;
                _presenterMToolVersionManagement.UpdateMToolUpdateVersion();
                if (IsErrorOccured)
                {
                    return;
                }
            }
            Response.Redirect("MToolVersionManagement.aspx");
        }
        private void SetMToolVerion(short oSPlatformDetailsId)
        {
            MToolVersion = new MToolVersionUpdateList { OSPlatformDetailsID = oSPlatformDetailsId };
            _presenterMToolVersionManagement.GetMToolVersionsSelect();
            if (!IsErrorOccured)
            {
                OSPlatDetailId = oSPlatformDetailsId;
                cmbOSPlatForm.SelectedValue = Convert.ToString(MToolVersion.OSPlatformID);
                txtLatestVersion.Text = MToolVersion.LatestVersion;
                txtMinReqVersion.Text = MToolVersion.MinReqVersion;
                txtURLlocation.Text = MToolVersion.URLlocation;
                txtUpdateInfo.Text = MToolVersion.UpdateInfo;
                chkIsUsed.Checked = MToolVersion.IsUsed;
            }
        }
        #endregion


        #region Implement Interface
        public MToolVersionUpdateList MToolVersion { get; set; }
        public List<MToolVersionUpdateList> MToolVersionList { get; set; }
        public Dictionary<int, string> OSPlatformNameList { get; set; }
        #endregion
    }
}