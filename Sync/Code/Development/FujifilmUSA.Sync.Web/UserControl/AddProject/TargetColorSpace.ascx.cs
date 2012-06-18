using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class TargetColorSpace : BaseControl, IViewTargetColorspace
    {
        #region "Declaration"
        public bool IsXmfNewProject
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser) &&
                        SessionManager.Instance.CurrentProject.ProjectId == 0);
            }
        }

        private const string _vsSourceProfile = "vsSourceProfile";
        private PresenterTargetColorspace _presenterTargetColorspace;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterTargetColorspace = new PresenterTargetColorspace(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkbtnProjectDescription.Visible = IsXmfNewProject;
            if (ViewState[_vsSourceProfile] == null)
            {
                BindProfileSource();
                LoadProjectInfoFromSession();
            }
        }
        #endregion

        #region Control Events
        protected void lnkbtnProjectDescription_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;
            Response.Redirect(string.Format("~/COAddProject.aspx?id={0}", Guid.NewGuid()));
        }

        protected void lnkbtnTargetColorSpace_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.SourceProfile = new SourceProfile
            {
                SourceProfileId = Convert.ToInt16(cmbSourceProfile.SelectedValue),
                ProfileName = cmbSourceProfile.SelectedItem.Text,
            };

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void cmbSourceProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState[_vsSourceProfile] != null)
            {
                txtProfileDescription.Text = ((List<SourceProfile>)ViewState[_vsSourceProfile]).Single(x => x.SourceProfileId.ToString() == cmbSourceProfile.SelectedValue).ProfileDesc;
            }
        }
        #endregion

        #region Method
        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject.SourceProfile != null)
            {
                cmbSourceProfile.SelectedValue = SessionManager.Instance.CurrentProject.SourceProfile.SourceProfileId.ToString();
                cmbSourceProfile_SelectedIndexChanged(null, null);
                //cmbSourceProfile.Enabled = !(SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0);
            }
        }
        private void BindProfileSource()
        {
            if (ViewState[_vsSourceProfile] != null) return;

            _presenterTargetColorspace.GetSourceProfile();
            if (!IsErrorOccured)
            {
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
    }
}
