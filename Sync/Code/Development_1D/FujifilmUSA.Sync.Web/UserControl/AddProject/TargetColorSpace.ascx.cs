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

        private const string _vsPrintStandardType = "vsPrintStandardType";
        private PresenterTargetColorspace _presenterTargetColorspace;
        #endregion
        public TargetColorSpace()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterTargetColorspace = new PresenterTargetColorspace(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkbtnProjectDescription.Visible = IsXmfNewProject;
            if (ViewState[_vsPrintStandardType] == null)
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
            SessionManager.Instance.CurrentProject.PrintStandardType = new Entity.CPSync1D.PrintStandardType()
            {
                PrintStandardTypeID = Convert.ToInt16(cmbPrintStandardType.SelectedValue),
                PrintStandard = cmbPrintStandardType.SelectedItem.Text,
                SourceProfile = new SourceProfile()
                {
                    SourceProfileId = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.SourceProfileId,
                    ProfileName = lblSourceProfileName.Text
                }
            };
            SessionManager.Instance.CurrentProject.SourceProfile = new SourceProfile
            {
                SourceProfileId = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.SourceProfileId,
                ProfileName = lblSourceProfileName.Text
            };

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void cmbPrintStandardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState[_vsPrintStandardType] != null)
            {
                txtProfileDescription.Text = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.ProfileDesc;
                lblSourceProfileName.Text = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.ProfileName;
            }
        }
        #endregion

        #region Method
        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject.PrintStandardType != null)
            {
                cmbPrintStandardType.SelectedValue = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID.ToString();
                cmbPrintStandardType_SelectedIndexChanged(null, null);
                //cmbSourceProfile.Enabled = !(SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0);
            }
        }
        private void BindProfileSource()
        {
            if (ViewState[_vsPrintStandardType] != null) return;

            _presenterTargetColorspace.GetPrintStandardTypeList();
            if (!IsErrorOccured)
            {
                cmbPrintStandardType.DataValueField = "PrintStandardTypeID";
                cmbPrintStandardType.DataTextField = "PrintStandard";
                ViewState[_vsPrintStandardType] = PrintStandardTypeList;
                cmbPrintStandardType.DataSource = (List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType];
                cmbPrintStandardType.DataBind();
                cmbPrintStandardType_SelectedIndexChanged(null, null);
            }
        }

        #endregion

        #region IViewTargetColorspace Members
        public List<Entity.CPSync1D.PrintStandardType> PrintStandardTypeList { get; set; }
        public bool IsSync1DType
        {
            get
            {
                return (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D);
            }
        }
        #endregion



    }
}
