using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using System.IO;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddSourceProfile : BaseControl, IViewSourceProfile
    {

        #region "Declaration"
        private PresenterSourceProfile _presentersourceprofile;
        private const string _recAddSourceProfile = "lblHeaderResource1.Text";
        private const string _recEditSourceProfile = "editSourceProfile";
        private const string _recSelectCompanyText = "selectCompanyText";
        private const string _recSelectRegionText = "selectRegionText";
        //private int MId
        //{
        //    get
        //    {
        //        return (ViewState["MeasurementDeviceId"] != null && !string.IsNullOrEmpty(ViewState["MeasurementDeviceId"].ToString())) ? Convert.ToInt32(ViewState["MeasurementDeviceId"]) : 0;
        //    }
        //    set { ViewState["MeasurementDeviceId"] = value.ToString(); }
        //}
        #endregion
        public AddSourceProfile()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presentersourceprofile = new PresenterSourceProfile(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            lblHeader.Text = GetResourceString(_recAddSourceProfile); //"Add Source Profile";
            if (IsPostBack) return;

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            BindRegionDropDown();
            cmbRegion_SelectedIndexChanged(null, null);
            if (Request.QueryString["SourceProfileId"] != null)
            {
                var SourceProfileId = Request.QueryString["SourceProfileId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(SourceProfileId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _SourceProfileId;
                if (Int32.TryParse(id, out _SourceProfileId))
                {
                    lblHeader.Text = GetResourceString(_recEditSourceProfile);//"Edit Source Profile";
                    LoadSourceProfile(_SourceProfileId);
                }
            }
        }

        #endregion

        #region Control events

        protected void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCompanyDropDown();
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (SessionManager.Instance.CurrentUser.Role.RoleId == 4 && cmbCompanyName.SelectedIndex == 0)  //For Region Admin, needs to select Region and Company Name first
            {
                fujiStatusbar.DisplayMessage("Please select Company first.", false);
                return;
            }

            SourceProfile = new SourceProfile();
            SourceProfile.SourceProfileId = (short)Convert.ToInt32(hdnSourceProfileID.Value);
            if (Convert.ToInt32(cmbRegion.SelectedValue) > 0)
                SourceProfile.RegionId = (short)Convert.ToInt32(cmbRegion.SelectedValue);
            if (Convert.ToInt32(cmbCompanyName.SelectedValue) > 0)
                SourceProfile.CompanyId = Convert.ToInt32(cmbCompanyName.SelectedValue);
            SourceProfile.ProfileName = txtSourceProfile.Text.Trim();
            SourceProfile.ProfileDesc = txtProfileDescription.Text;

            //Read source profile from file
            if (fuProfile.PostedFile != null)
            {
                if (fuProfile.PostedFile.ContentLength > 0)
                {
                    HttpPostedFile objHttpPostedFile = fuProfile.PostedFile;
                    int intContentlength = objHttpPostedFile.ContentLength;
                    var bytImage = new Byte[intContentlength];
                    objHttpPostedFile.InputStream.Read(bytImage, 0, intContentlength);
                    SourceProfile.Profile = bytImage;
                }
            }

            SourceProfile.IsUsed = chkIsActive.Checked;
            if ((SessionManager.Instance.CurrentUser.Role.RoleId == 4 && (SourceProfile.RegionId > 0 && SourceProfile.CompanyId > 0)) || SessionManager.Instance.CurrentUser.Role.RoleId == 1) //If Region Admin then need RegionID and CompanyID both, none is mandetory for System Admin
            {
                if (hdnSourceProfileID.Value == "0")        //-------Add Source Profile
                {
                    _presentersourceprofile.InsertSourceProfile();
                    if (IsErrorOccured) return;
                    Response.Redirect("SourceProfileManagement.aspx");
                }
                else                                        //-------Edit Source Profile
                {
                    _presentersourceprofile.UpdateSourceProfile();
                    if (IsErrorOccured) return;
                    Response.Redirect("SourceProfileManagement.aspx");
                }
            }
        }
        #endregion

        #region Method

        private void BindCompanyDropDown()
        {
            cmbCompanyName.Items.Clear();
            if (cmbRegion.SelectedValue != "0")
            {
                RegionId = Convert.ToInt32(cmbRegion.SelectedValue);
                _presentersourceprofile.GetCompaniesByRegion();
                if (!IsErrorOccured)
                {
                    cmbCompanyName.DataSource = CompanyList;
                    cmbCompanyName.DataBind();
                }
            }
            cmbCompanyName.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompanyText), "0"));//-- Select Company --
        }

        private void BindRegionDropDown()
        {
            _presentersourceprofile.RegionList();
            if (!IsErrorOccured)
            {
                cmbRegion.DataSource = RegionList;
                cmbRegion.DataBind();
                cmbRegion.Items.Insert(0, new ListItem(GetResourceString(_recSelectRegionText), "0"));//-- Select Region --
            }
            if (cmbRegion.Items.Count == 2) //---By default give selected region for Region Admin
            {
                cmbRegion.SelectedIndex = 1;
                cmbRegion.Enabled = false;
            }
        }

        public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;
            System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
            long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
            _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
            _FileStream.Close();
            _FileStream.Dispose();
            _BinaryReader.Close();
            return _Buffer;
        }

        private void LoadSourceProfile(int iSourceProfileId)
        {
            SourceProfile = new SourceProfile { SourceProfileId = (short)iSourceProfileId };
            _presentersourceprofile.GetSourceProfile();
            if (!IsErrorOccured)
            {
                cmbRegion.Enabled = false;
                cmbCompanyName.Enabled = false;              
                rfvProfile.ValidationGroup = "a";

                hdnSourceProfileID.Value = Convert.ToString(iSourceProfileId);
                cmbRegion.SelectedValue = Convert.ToString(SourceProfile.Region.RegionId);
                cmbRegion_SelectedIndexChanged(null, null);
                cmbCompanyName.SelectedValue = Convert.ToString(SourceProfile.Company.CompanyId);
                txtSourceProfile.Text = SourceProfile.ProfileName;
                txtProfileDescription.Text = SourceProfile.ProfileDesc;
                chkIsActive.Checked = (SourceProfile.IsUsed == true ? true : false);
            }
        }

        #endregion

        #region IViewSourceProfile Members
        public int SourceProfileId { get; set; }
        public int RegionId { get; set; }
        public int CompanyId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileDesc { get; set; }
        public byte[] Profile { get; set; }
        public bool IsUsed { get; set; }
        public SourceProfile SourceProfile { get; set; }
        public List<SourceProfile> SourceProfiles { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public Dictionary<int, string> RegionList { get; set; }
        #endregion


    }
}
