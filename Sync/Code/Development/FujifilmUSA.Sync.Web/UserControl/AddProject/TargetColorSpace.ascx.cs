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
            if (!IsPostBack)
            {
                BindProfileSource();
            }
        }
        #endregion


        #region Control Events
        protected void lnkbtnTargetColorSpace_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.ProfilerOption;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();            
        }
        #endregion


        #region Method

        private void BindProfileSource()
        {
            _presenterProjectList.GetSourceProfile();
            cmbSourceProfile.DataSource = SourceProfile;
            cmbSourceProfile.DataBind();
            cmbSourceProfile.Items.Insert(0, new ListItem("-- Select --", "0"));

            //var dic = new Dictionary<int,string> {{1, "Gracol v02"}, {2, "Swop"}};
            //cmbSourceProfile.DataSource = dic;
            //cmbSourceProfile.DataTextField = "Value";
            //cmbSourceProfile.DataValueField = "Key";
            //cmbSourceProfile.DataBind();
        }

        #endregion

        #region IViewProjectList Members
        public List<Project> Projects { get; set; }
        public Project Project { get; set; }
        public int CompnayId { get; set; }
        public Dictionary<int, string> SourceProfile { get; set; }
        #endregion


      
    }
}