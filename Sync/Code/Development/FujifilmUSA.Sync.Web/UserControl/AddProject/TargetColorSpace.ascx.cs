using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class TargetColorSpace : System.Web.UI.UserControl, IViewTargetColorspace
    {

        #region "Declaration"
        private string _vsSourceProfile = "vsSourceProfile";
        //private PresenterProjectList _presenterProjectList;
        private PresenterTargetColorspace _presenterTargetColorspace;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterTargetColorspace = new PresenterTargetColorspace(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            {
                BindProfileSource();
            }
        }
        #endregion

        #region Control Events
        protected void lnkbtnTargetColorSpace_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.SourceProfile = new SourceProfile
            {
                SourceProfileId = (short)Convert.ToInt32(cmbSourceProfile.SelectedValue),
            };

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }
        #endregion

        #region Method

        private void BindProfileSource()
        {
            if (ViewState[_vsSourceProfile] == null)
            {
                _presenterTargetColorspace.GetSourceProfile();
                cmbSourceProfile.DataValueField = "SourceProfileID";
                cmbSourceProfile.DataTextField = "ProfileName";
                ViewState[_vsSourceProfile] = SourceProfile;
                cmbSourceProfile.DataSource = (List<SourceProfile>)ViewState[_vsSourceProfile];
                cmbSourceProfile.DataBind();
                cmbSourceProfile_SelectedIndexChanged(null, null);
            }
        }

        #endregion

        #region IViewTargetColorspace Members
        public List<SourceProfile> SourceProfile { get; set; }
        #endregion

        protected void cmbSourceProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState[_vsSourceProfile] != null)
            {
                txtProfileDescription.Text = ((List<SourceProfile>)ViewState[_vsSourceProfile]).Single(x => x.SourceProfileId.ToString() == cmbSourceProfile.SelectedValue).ProfileDesc;
            }
        }
    }
}