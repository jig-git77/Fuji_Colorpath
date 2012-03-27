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
    public partial class AddSourceProfile : System.Web.UI.UserControl, IViewSourceProfile
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
            AdminStatusbar1.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SessionManager.CheckLogin();


            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");

                BindCompanyDropDown();
                BindRegionDropDown();
                if (Request.QueryString["SourceProfileId"] != null)
                {
                    LoadSourceProfile(Convert.ToInt32(Request.QueryString["SourceProfileId"]));
                }
            }
        }

        #endregion

        #region Control events

        protected void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbBranch.Items.Clear();
            //if (cmbCompanyName.SelectedIndex != 0)
            //{
            //    BindBranchDropDown(int.Parse(cmbCompanyName.SelectedValue));
            //}
            //else
            //    cmbBranch.Items.Insert(0, new ListItem("-- Select --", "0"));

        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbLicenseType.Items.Clear();
            //if (cmbBranch.SelectedIndex != 0)
            //{
            //    BindLicenseDropDown(int.Parse(cmbBranch.SelectedValue));
            //}
            //else
            //    cmbLicenseType.Items.Insert(0, new ListItem("-- Select --", "0"));

        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SourceProfile = new SourceProfile();
            SourceProfile.SourceProfileId = (short)Convert.ToInt32(hdnSourceProfileID.Value);
            SourceProfile.CompanyId = Convert.ToInt32(cmbCompanyName.SelectedValue);
            SourceProfile.RegionId = (short)Convert.ToInt32(cmbRegion.SelectedValue);
            SourceProfile.ProfileName = txtSourceProfile.Text.Trim();
            SourceProfile.ProfileDesc = txtProfileDescription.Text;
            //if (fuProfile.HasFile)
            //{
                //string strFileName = System.IO.Path.GetFullPath(fuProfile.PostedFile.FileName);
                //string strFileName = Server.MapPath(fuProfile.FileName);
                //HttpPostedFile sourcefile = Request.Files;
                //Byte[] data = new Byte[sourcefile.ContentLength];
            //}
            
            //SourceProfile.Profile= fuProfile.FileBytes;
            //SourceProfile.Profile = File.ReadAllBytes(fuProfile.
            
            HttpPostedFile objHttpPostedFile = fuProfile.PostedFile;
            int ContentLength = objHttpPostedFile.ContentLength;
            SourceProfile.Profile = new byte[ContentLength];
            SourceProfile.IsUsed = chkIsActive.Checked;
            if (hdnSourceProfileID.Value == "0")
            {
                _presentersourceprofile.InsertSourceProfile();
                Response.Redirect("SourceProfileManagement.aspx");
            }
            else
            {
                _presentersourceprofile.UpdateSourceProfile();
                Response.Redirect("SourceProfileManagement.aspx");
            }

        }

        #endregion

        #region Method

        private void BindCompanyDropDown()
        {
            _presentersourceprofile.CompanyList();
            cmbCompanyName.DataSource = CompanyList;
            cmbCompanyName.DataBind();
            cmbCompanyName.Items.Insert(0, new ListItem("-- Select Company --", "0"));
        }

        private void BindRegionDropDown()
        {
            _presentersourceprofile.RegionList();
            cmbRegion.DataSource = RegionList;
            cmbRegion.DataBind();
            cmbRegion.Items.Insert(0, new ListItem("-- Select Region --", "0"));
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
            SourceProfile = new SourceProfile();
            SourceProfile.SourceProfileId = (short)iSourceProfileId;
            _presentersourceprofile.GetSourceProfile();
            hdnSourceProfileID.Value = Convert.ToString(iSourceProfileId);
            cmbCompanyName.SelectedValue = Convert.ToString(SourceProfile.Company.CompanyId);
            cmbRegion.SelectedValue = Convert.ToString(SourceProfile.Region.RegionId);
            txtSourceProfile.Text = SourceProfile.ProfileName;
            txtProfileDescription.Text = SourceProfile.ProfileDesc;
            chkIsActive.Checked = (SourceProfile.IsUsed == true ? true : false);
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

        #region IViewError Members

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}