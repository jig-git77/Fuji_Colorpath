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
                if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync1D)
                {
                    trTargetLAB.Visible = false;
                    trProjectType.Visible = false;
                }
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

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                SessionManager.Instance.CurrentProject.ProjectTargetLAB = GetTargetLAB();
                SessionManager.Instance.CurrentProject.ProjectTypeId = Convert.ToInt16(cmbProjectType.SelectedValue);               
            }

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
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                 cmbProjectType_SelectedIndexChanged(null, null);
        }

        protected void cmbProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                BindTargetLAB();
        }
        #endregion

        #region Method
        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject.PrintStandardType != null)
            {
                cmbPrintStandardType.SelectedValue = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID.ToString();
                cmbPrintStandardType_SelectedIndexChanged(null, null);
            }
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                cmbProjectType.SelectedValue = SessionManager.Instance.CurrentProject.ProjectTypeId.ToString();
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

        private List<Entity.CPSync1D.ProjectTargetLAB> GetTargetLAB()
        {
            List<Entity.CPSync1D.ProjectTargetLAB> targetLAB = new List<Entity.CPSync1D.ProjectTargetLAB>();
            Entity.CPSync1D.ProjectTargetLAB itemLAB = new Entity.CPSync1D.ProjectTargetLAB();
            foreach (ListViewDataItem item in lvTargetLAB.Items)
            {
                itemLAB = new Entity.CPSync1D.ProjectTargetLAB();
                TextBox txtPatchTargetL = item.FindControl("txtPatchTargetL") as TextBox;
                if (txtPatchTargetL != null)
                    itemLAB.TargetL = Convert.ToDecimal(txtPatchTargetL.Text);

                TextBox txtPatchTargetA = item.FindControl("txtPatchTargetA") as TextBox;
                if (txtPatchTargetA != null)
                    itemLAB.TargetA = Convert.ToDecimal(txtPatchTargetA.Text);

                TextBox txtPatchTargetB = item.FindControl("txtPatchTargetB") as TextBox;
                if (txtPatchTargetB != null)
                    itemLAB.TargetB = Convert.ToDecimal(txtPatchTargetB.Text);

                HiddenField hdnPatchTypeId = item.FindControl("hdnPatchTypeId") as HiddenField;
                if (hdnPatchTypeId != null)
                    itemLAB.PatchType = new PatchType() { PatchTypeId = Convert.ToInt16(hdnPatchTypeId.Value) };

                targetLAB.Add(itemLAB);
            }
            return targetLAB;
        }

        private void BindTargetLAB()
        {
            trTargetLAB.Visible = true;
            this.PrintstandardTypeId = Convert.ToInt16(cmbPrintStandardType.SelectedValue);
            this.ProjectTypeId = Convert.ToInt16(cmbProjectType.SelectedValue);
            _presenterTargetColorspace.GetTargetLABList();
            lvTargetLAB.DataSource = this.ProjectTargetLABList;
            lvTargetLAB.DataBind();
        }

        #endregion

        #region IViewTargetColorspace Members
        public List<Entity.CPSync1D.PrintStandardType> PrintStandardTypeList { get; set; }
        public List<Entity.CPSync1D.ProjectTargetLAB> ProjectTargetLABList { get; set; }
        public int PrintstandardTypeId { get; set; }
        public int ProjectTypeId { get; set; }
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
