using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class TargetColorSpace : System.Web.UI.UserControl, IViewProjectList
    {

        #region "Declaration"
        private PresenterProjectList _presenterProjectList;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterProjectList = new PresenterProjectList(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindProfileSource();
           
        }
        #endregion

        #region Control Events
        protected void lnkbtnTargetColorSpace_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.SourceProfile = new FujifilmUSA.Sync.Entity.SourceProfile
            {
               SourceProfileId= (short)Convert.ToInt32(cmbSourceProfile.SelectedValue),
               ProfileDesc=txtProfileDescription.Text

            };

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();            
        }
        #endregion

        #region Method

        private void BindProfileSource()
        {
            _presenterProjectList.GetSourceProfile();
            cmbSourceProfile.DataSource = SourceProfile;
            cmbSourceProfile.DataBind();
        }

        #endregion

        #region IViewProjectList Members
        public List<Project> Projects { get; set; }
        public Project Project { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> SourceProfile { get; set; }
        public Dictionary<int, string> ProfilerOptionsPreset { get; set; }
        public int ProjectId { get; set; }
        #endregion
    }
}