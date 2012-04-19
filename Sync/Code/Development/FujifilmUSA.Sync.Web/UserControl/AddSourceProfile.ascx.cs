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
        //private int MId
        //{
        //    get
        //    {
        //        return (ViewState["MeasurementDeviceId"] != null && !string.IsNullOrEmpty(ViewState["MeasurementDeviceId"].ToString())) ? Convert.ToInt32(ViewState["MeasurementDeviceId"]) : 0;
        //    }
        //    set { ViewState["MeasurementDeviceId"] = value.ToString(); }
        //}
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presentersourceprofile = new PresenterSourceProfile(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            lblHeader.Text = "Add Source Profile";
            if (IsPostBack) return;

            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            BindRegionDropDown();
            cmbRegion_SelectedIndexChanged(null, null);
            if (Request.QueryString["SourceProfileId"] != null)
            {
                lblHeader.Text = "Edit Source Profile";
                LoadSourceProfile(Convert.ToInt32(Request.QueryString["SourceProfileId"]));
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
            SourceProfile = new SourceProfile();
            SourceProfile.SourceProfileId = (short)Convert.ToInt32(hdnSourceProfileID.Value);
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
            if (hdnSourceProfileID.Value == "0")
            {
                _presentersourceprofile.InsertSourceProfile();
                if (IsErrorOccured) return;
                Response.Redirect("SourceProfileManagement.aspx");
            }
            else
            {
                _presentersourceprofile.UpdateSourceProfile();
                if (IsErrorOccured) return;
                Response.Redirect("SourceProfileManagement.aspx");
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
            cmbCompanyName.Items.Insert(0, new ListItem("-- Select Company --", "0"));
        }

        private void BindRegionDropDown()
        {
            _presentersourceprofile.RegionList();
            if (!IsErrorOccured)
            {
                cmbRegion.DataSource = RegionList;
                cmbRegion.DataBind();
                cmbRegion.Items.Insert(0, new ListItem("-- Select Region --", "0"));
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
