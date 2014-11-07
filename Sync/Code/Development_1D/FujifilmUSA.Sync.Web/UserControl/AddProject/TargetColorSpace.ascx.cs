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
                btn_PaperClear.Visible = false;
                BindProfileSource();
                BindDensityType(); //Load DensityType 
                BindPCSDataType(); //Load PCSDataType              
                if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync1D)
                {
                    trTargetLAB.Visible = false;
                    trProjectType.Visible = false;
                }
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

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                SessionManager.Instance.CurrentProject.ProjectTargetLAB = GetTargetLAB();
                SessionManager.Instance.CurrentProject.ProjectTypeId = Convert.ToInt16(cmbProjectType.SelectedValue);
            }
            else
            { 
                if(SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP)
                    SessionManager.Instance.CurrentProject.ProjectTypeId = (short)Entity.EProjectType.DeviceLinkProfile;
                else if(SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimization)
                    SessionManager.Instance.CurrentProject.ProjectTypeId = (short)Entity.EProjectType.InkOptimization;
            }
            SessionManager.Instance.CurrentProject.DensityType = (DensityTypes)Convert.ToInt16(cmbDensityType.SelectedValue);  //DensityType Selection 
            SessionManager.Instance.CurrentProject.PCSDataTypes = (PCSDataTypes)Convert.ToInt16(cmbPCSDataType.SelectedValue); //PCSDataType Selection 

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
            {
                if (SessionManager.Instance.CurrentProject.ProjectTargetLAB == null || SessionManager.Instance.CurrentProject.ProjectTargetLAB.Count == 0)
                {
                    BindTargetLAB();
                }
                BindPCSDataType();
            }
        }

        protected void chk_IsPaperScaling_CheckedChanged(object sender, EventArgs e)
        {
            Panel_PaperLAB.Enabled = chk_IsPaperScaling.Checked;
        }

        protected void btn_PaperApply_Click(object sender, EventArgs e)
        {
            //Load back data from Listview
            this.ProjectTargetLABList = GetTargetLAB();

            if (this.ProjectTargetLABList != null && this.ProjectTargetLABList.Count > 0)
            {
                CIELab Finallab = new CIELab(0, 0, 0);
                CIEXYZ CurrentPaperxyz = new CIEXYZ(0, 0, 0);
                CIEXYZ NewPaperxyz = new CIEXYZ(0, 0, 0);

                //Create list for Paper Patch
                List<Entity.CPSync1D.ProjectTargetLAB> PaperProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();
                PaperProjectTargetList = this.ProjectTargetLABList.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList();

                //Create list for new lab values
                List<Entity.CPSync1D.ProjectTargetLAB> NewProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();

                //convert new Paper's LAB to XYZ
                NewPaperxyz = ColorMath.LabtoXYZ(Convert.ToDouble(txtPaper_L.Text), Convert.ToDouble(txtPaper_a.Text), Convert.ToDouble(txtPaper_b.Text));

                //convert current Paper's LAB to XYZ
                CurrentPaperxyz = ColorMath.LabtoXYZ(Convert.ToDouble(PaperProjectTargetList[0].TargetL), Convert.ToDouble(PaperProjectTargetList[0].TargetA), Convert.ToDouble(PaperProjectTargetList[0].TargetB));

                for (int i = 0; i < this.ProjectTargetLABList.Count; i++)
                {
                    Finallab = ColorMath.CalcPaperScaleLAB(CurrentPaperxyz.X, CurrentPaperxyz.Y, CurrentPaperxyz.Z, NewPaperxyz.X, NewPaperxyz.Y, NewPaperxyz.Z, Convert.ToDouble(ProjectTargetLABList[i].TargetL), Convert.ToDouble(ProjectTargetLABList[i].TargetA), Convert.ToDouble(ProjectTargetLABList[i].TargetB));
                    NewProjectTargetList.Add(new Entity.CPSync1D.ProjectTargetLAB() { PatchType = this.ProjectTargetLABList[i].PatchType, TargetL = (decimal)Finallab.L, TargetA = (decimal)Finallab.A, TargetB = (decimal)Finallab.B });
                }
                //Assign new ProjectTargetLAB list
                this.ProjectTargetLABList = NewProjectTargetList;
                lvTargetLAB.DataSource = this.ProjectTargetLABList;
                lvTargetLAB.DataBind();
            }
            btn_PaperApply.Visible = false;
            btn_PaperClear.Visible = true;
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
                cmbProjectType_SelectedIndexChanged(null, null);
                if (SessionManager.Instance.CurrentProject.ProjectTargetLAB != null && SessionManager.Instance.CurrentProject.ProjectTargetLAB.Count > 0) //If open the existing project, then load from session
                {
                    chk_IsPaperScaling.Enabled = false; //Disable Paper Scaling when project is already created
                    //Load Target LAB Listview
                    lvTargetLAB.DataSource = SessionManager.Instance.CurrentProject.ProjectTargetLAB;
                    lvTargetLAB.DataBind();
                    //Load Paper LAB values into top Textboxes from List
                    txtPaper_L.Text = SessionManager.Instance.CurrentProject.ProjectTargetLAB.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList()[0].TargetL.ToString();   //Paper L* Value
                    txtPaper_a.Text = SessionManager.Instance.CurrentProject.ProjectTargetLAB.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList()[0].TargetA.ToString();   //Paper a* Value
                    txtPaper_b.Text = SessionManager.Instance.CurrentProject.ProjectTargetLAB.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList()[0].TargetB.ToString();   //Paper b* Value

                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0) //Once project is saved, Project type selection is disable
                    {
                        cmbProjectType.Enabled = false;
                    }
                }
            }
            cmbDensityType.SelectedValue = Convert.ToInt16(SessionManager.Instance.CurrentProject.DensityType).ToString(); //DensityType Selection
            BindPCSDataType();
            cmbPCSDataType.SelectedValue = Convert.ToInt16(SessionManager.Instance.CurrentProject.PCSDataTypes).ToString(); //PCSDataType Selection
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
                Label lblPatch = item.FindControl("lblPatch") as Label;
                if (hdnPatchTypeId != null)
                    itemLAB.PatchType = new PatchType() { PatchTypeId = Convert.ToInt16(hdnPatchTypeId.Value), Type = lblPatch.Text };

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

            if (this.ProjectTargetLABList.Count > 0)
            {
                //Load Paper LAB values into top Textboxes from List
                txtPaper_L.Text = this.ProjectTargetLABList.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList()[0].TargetL.ToString();   //Paper L* Value
                txtPaper_a.Text = this.ProjectTargetLABList.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList()[0].TargetA.ToString();   //Paper a* Value
                txtPaper_b.Text = this.ProjectTargetLABList.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList()[0].TargetB.ToString();   //Paper b* Value
            }
            else   //By Default
            {
                txtPaper_L.Text = "0";   //Paper L* Value
                txtPaper_a.Text = "0";   //Paper a* Value
                txtPaper_b.Text = "0";   //Paper b* Value
            }
        }

        private void BindDensityType()
        {
            if (cmbDensityType.DataSource != null) return;

            cmbDensityType.DataValueField = "Key";
            cmbDensityType.DataTextField = "Value";
            cmbDensityType.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<DensityTypes>();
            cmbDensityType.DataBind();
        }

        private void BindPCSDataType()
        {
            cmbPCSDataType.DataValueField = "Key";
            cmbPCSDataType.DataTextField = "Value";
            cmbPCSDataType.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<PCSDataTypes>();
     
            if (cmbProjectType.SelectedItem.Text == "G7" || SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync1D) //If 1D then G7 type, or for all other Projects 4D,etc...
            {
                cmbPCSDataType.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<PCSDataTypes>().Where(p => p.Value != Common.Utilities.GetEnumDescriptionAttributeValue(PCSDataTypes.Density)).ToDictionary(p => p.Key, p => p.Value);
            }
            else if (cmbProjectType.SelectedItem.Text == "ISO")
            {
                cmbPCSDataType.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<PCSDataTypes>().Where(p => p.Value != Common.Utilities.GetEnumDescriptionAttributeValue(PCSDataTypes.XYZ)).ToDictionary(p => p.Key, p => p.Value);
            }
            cmbPCSDataType.DataBind();
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

        protected void btn_PaperClear_Click(object sender, EventArgs e)
        {
            BindTargetLAB();    //Apply old Target LAB from Main look up table
            btn_PaperApply.Visible = true; 
            btn_PaperClear.Visible = false;
        }      
    }
}
