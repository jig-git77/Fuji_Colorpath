using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity.CPSpot;

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
        private const string _vsSpotTarget = "vsSpotTarget";
        private const string _recSelect = "selectText";
        private const string _recNoCertifiedSpotCondition = "noCertifiedSpotCondition";
        private const string _recTargetPrintConditionNotAvailableMessage = "TargetPrintConditionNotAvailableMessage";
        

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
                BindTargetPrintCategory();
                BindProfileSource();
                BindPCSDataType(); //Load PCSDataType 

                PanelProjectType.GroupingText = "<b>" + GetResourceString("PanelProjectType") + "</b>";
                if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync1D)
                {
                    trTargetLAB.Visible = false;
                    trProjectType.Visible = false;
                }
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP)
                {
                    trPaperScaling_4D.Visible = true;
                }
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
                {
                    BindSpotTarget(); //Load Spot Target List
                    BindBranches();
                    trListHeader.Visible = false;
                    trTargetLAB.Visible = false;
                    trSpotTarget.Visible = true;
                    trMeasurementMode.Visible = false;
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

                //------If 1D project then pass NewPaperScaling LAB values--------//
                SessionManager.Instance.CurrentProject.NewPaper_White_L = Convert.ToDecimal(txtPaper_L.Text);
                SessionManager.Instance.CurrentProject.NewPaper_White_a = Convert.ToDecimal(txtPaper_a.Text);
                SessionManager.Instance.CurrentProject.NewPaper_White_b = Convert.ToDecimal(txtPaper_b.Text);
                SessionManager.Instance.CurrentProject.NewPaper_Black_L = Convert.ToDecimal(txtPaper_Black_L.Text);
                SessionManager.Instance.CurrentProject.NewPaper_Black_a = Convert.ToDecimal(txtPaper_Black_a.Text);
                SessionManager.Instance.CurrentProject.NewPaper_Black_b = Convert.ToDecimal(txtPaper_Black_b.Text);
            }
            else
            {
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP)
                {
                    SessionManager.Instance.CurrentProject.ProjectTypeId = (short)Entity.EProjectType.DeviceLinkProfile;
                    //------If 4D project then pass NewPaperScaling LAB values--------//
                    SessionManager.Instance.CurrentProject.EnabledPaperScaling = chk_IsPaperScaling4D.Checked;
                    SessionManager.Instance.CurrentProject.NewPaper_White_L = Convert.ToDecimal(txtPaper_L_4D_White.Text);
                    SessionManager.Instance.CurrentProject.NewPaper_White_a = Convert.ToDecimal(txtPaper_a_4D_White.Text);
                    SessionManager.Instance.CurrentProject.NewPaper_White_b = Convert.ToDecimal(txtPaper_b_4D_White.Text);
                    SessionManager.Instance.CurrentProject.NewPaper_Black_L = Convert.ToDecimal(txtPaper_L_4D_Black.Text);
                    SessionManager.Instance.CurrentProject.NewPaper_Black_a = Convert.ToDecimal(txtPaper_a_4D_Black.Text);
                    SessionManager.Instance.CurrentProject.NewPaper_Black_b = Convert.ToDecimal(txtPaper_b_4D_Black.Text);
                }
                else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimization)
                    SessionManager.Instance.CurrentProject.ProjectTypeId = (short)Entity.EProjectType.InkOptimization;
            }
            SessionManager.Instance.CurrentProject.PCSDataTypes = (PCSDataTypes)Convert.ToInt16(cmbPCSDataType.SelectedValue); //PCSDataType Selection 

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;

            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void cmbPrintStandardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<Entity.CPSync1D.PrintStandardType> listProfileCondition = (List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType];

            //if (ViewState[_vsPrintStandardType] != null && listProfileCondition.Count > 0)
            //{                
            //        txtProfileDescription.Text = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.ProfileDesc;
            //        lblSourceProfileName.Text = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.ProfileName;
            //}
            //else
            //{
            //    txtProfileDescription.Text = lblSourceProfileName.Text = GetResourceString(_recTargetPrintConditionNotAvailableMessage);                                 
            //}

            if (!string.IsNullOrEmpty(cmbPrintStandardType.SelectedValue))
            {
                if (ViewState[_vsPrintStandardType] != null)
                {
                    txtProfileDescription.Text = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.ProfileDesc;
                    lblSourceProfileName.Text = ((List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType]).Single(x => x.PrintStandardTypeID.ToString() == cmbPrintStandardType.SelectedValue).SourceProfile.ProfileName;
                }
            }
            else
            {
                txtProfileDescription.Text = lblSourceProfileName.Text = GetResourceString(_recTargetPrintConditionNotAvailableMessage);
            }
            
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                cmbProjectType_SelectedIndexChanged(null, null);
        }

        protected void cmbProjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                if (ViewState[_vsPrintStandardType] != null)
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
                CIEXYZ NewMinxyz = new CIEXYZ(0, 0, 0);


                //Create list for Paper Patch
                List<Entity.CPSync1D.ProjectTargetLAB> PaperProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();
                PaperProjectTargetList = this.ProjectTargetLABList.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList();

                //Create list for new lab values
                List<Entity.CPSync1D.ProjectTargetLAB> NewProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();

                //convert new Paper's LAB to XYZ
                NewPaperxyz = ColorMath.LabtoXYZ(Convert.ToDouble(txtPaper_L.Text), Convert.ToDouble(txtPaper_a.Text), Convert.ToDouble(txtPaper_b.Text));
                NewMinxyz = ColorMath.LabtoXYZ(Convert.ToDouble(txtPaper_Black_L.Text), Convert.ToDouble(txtPaper_Black_a.Text), Convert.ToDouble(txtPaper_Black_b.Text));

                //convert current Paper's LAB to XYZ
                CurrentPaperxyz = ColorMath.LabtoXYZ(Convert.ToDouble(PaperProjectTargetList[0].TargetL), Convert.ToDouble(PaperProjectTargetList[0].TargetA), Convert.ToDouble(PaperProjectTargetList[0].TargetB));

                for (int i = 0; i < this.ProjectTargetLABList.Count; i++)
                {
                    Finallab = ColorMath.CalcPaperScaleLAB(CurrentPaperxyz.X, CurrentPaperxyz.Y, CurrentPaperxyz.Z, NewPaperxyz.X, NewPaperxyz.Y, NewPaperxyz.Z, NewMinxyz.X, NewMinxyz.Y, NewMinxyz.Z, Convert.ToDouble(ProjectTargetLABList[i].TargetL), Convert.ToDouble(ProjectTargetLABList[i].TargetA), Convert.ToDouble(ProjectTargetLABList[i].TargetB));
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

                    if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0) //Once project is saved, Project type selection is disable
                    {
                        cmbProjectType.Enabled = false;
                    }
                }
                txtPaper_Black_L.Text = SessionManager.Instance.CurrentProject.NewPaper_Black_L.ToString();
                txtPaper_Black_a.Text = SessionManager.Instance.CurrentProject.NewPaper_Black_a.ToString();
                txtPaper_Black_b.Text = SessionManager.Instance.CurrentProject.NewPaper_Black_b.ToString();
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP) //-----If it is projecttype SYnc4D, then display New Paper scaling L,a,b values---//
            {
                chk_IsPaperScaling4D.Checked = SessionManager.Instance.CurrentProject.EnabledPaperScaling;
                if (chk_IsPaperScaling4D.Checked)
                {
                    Panel_PaperLAB4D_White.Enabled = true;
                    Panel_PaperLAB4D_Black.Enabled = true;
                }
                txtPaper_L_4D_White.Text = SessionManager.Instance.CurrentProject.NewPaper_White_L.ToString();
                txtPaper_a_4D_White.Text = SessionManager.Instance.CurrentProject.NewPaper_White_a.ToString();
                txtPaper_b_4D_White.Text = SessionManager.Instance.CurrentProject.NewPaper_White_b.ToString();
                txtPaper_L_4D_Black.Text = SessionManager.Instance.CurrentProject.NewPaper_Black_L.ToString();
                txtPaper_a_4D_Black.Text = SessionManager.Instance.CurrentProject.NewPaper_Black_a.ToString();
                txtPaper_b_4D_Black.Text = SessionManager.Instance.CurrentProject.NewPaper_Black_b.ToString();

                if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0) //Once project is saved, Paper Scaling L,a,b selection is disable
                {
                    chk_IsPaperScaling4D.Enabled = false;
                    Panel_PaperLAB4D_White.Enabled = false;
                    Panel_PaperLAB4D_Black.Enabled = false;
                }
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                if (SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget != null)
                {
                    cmbSpotTargetList.SelectedValue = Convert.ToString(SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget.SpotTargetID);
                    cmbSpotTargetList_SelectedIndexChanged(null, null);
                    cmbSpotTargetList.Enabled = false;

                    if (SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget.SpotTargetID == 1) //-----If Custom made re target spot project, then set original base spot project from where we re-targeted
                    {
                        cmbBranch.SelectedValue = Convert.ToString(SessionManager.Instance.CurrentProject.ReTargetProjectBranchID);
                        cmbBranch_SelectedIndexChanged(null, null);
                        cmbDevice.SelectedValue = Convert.ToString(SessionManager.Instance.CurrentProject.ReTargetProjectDeviceID);
                        cmbDevice_SelectedIndexChanged(null, null);
                        ddlReTargetProject.SelectedValue = Convert.ToString(SessionManager.Instance.CurrentProject.SpotTargetProject.ReTargetSpotProjectID);
                        ddlReTargetProject_SelectedIndexChanged(null, null);
                        cmbBranch.Enabled = false;
                        cmbDevice.Enabled = false;
                        ddlReTargetProject.Enabled = false;
                    }

                }
            }
            BindPCSDataType();
            cmbPCSDataType.SelectedValue = Convert.ToInt16(SessionManager.Instance.CurrentProject.PCSDataTypes).ToString(); //PCSDataType Selection

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                lnkbtNext.Visible = true;
                lnkbtnTargetColorSpace.Visible = false;
            }
            else
            {
                lnkbtNext.Visible = false;
            }
        }

        private void BindProfileSource()
        {
            TargetPrintCategory = Convert.ToInt32(cmbTargetPrintCategory.SelectedValue);
            // if (ViewState[_vsPrintStandardType] != null) return;

            _presenterTargetColorspace.GetPrintStandardTypeList();
            if (!IsErrorOccured)
            {
                if(PrintStandardTypeList!=null && PrintStandardTypeList.Count>0)
                {
                    cmbPrintStandardType.DataValueField = "PrintStandardTypeID";
                    cmbPrintStandardType.DataTextField = "PrintStandard";
                    ViewState[_vsPrintStandardType] = PrintStandardTypeList;
                    cmbPrintStandardType.DataSource = (List<Entity.CPSync1D.PrintStandardType>)ViewState[_vsPrintStandardType];
                    cmbPrintStandardType.DataBind();
                }
                else
                {
                    cmbPrintStandardType.Items.Clear();
                    cmbPrintStandardType.Items.Insert(0, new ListItem(GetResourceString(_recTargetPrintConditionNotAvailableMessage), string.Empty));
                }
                
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
                txtPaper_Black_L.Text = "0";
                txtPaper_Black_a.Text = "0";
                txtPaper_Black_b.Text = "0";
            }
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
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) //Bydefault spectral selection for Sync1D projects only
            {
                cmbPCSDataType.SelectedIndex = 1; //Bydefault spectral selection
            }
        }

        private void BindSpotTarget()
        {
            if (ViewState[_vsSpotTarget] != null) return;

            _presenterTargetColorspace.GetSpotTargetList();
            if (!IsErrorOccured)
            {
                cmbSpotTargetList.DataValueField = "SpotTargetID";
                cmbSpotTargetList.DataTextField = "SpotTargetName";
                ViewState[_vsSpotTarget] = SpotTargetList;
                cmbSpotTargetList.DataSource = (List<Entity.CPSpot.SpotTarget>)ViewState[_vsSpotTarget];
                cmbSpotTargetList.DataBind();
                cmbSpotTargetList_SelectedIndexChanged(null, null);
            }
        }

        private void BindTargetPrintCategory()
        {
            cmbTargetPrintCategory.DataValueField = "Key";
            cmbTargetPrintCategory.DataTextField = "Value";
            cmbTargetPrintCategory.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<ESourceProfileCategory>();
            cmbTargetPrintCategory.DataBind();
            cmbTargetPrintCategory.SelectedIndex = 0;

            //disable for all projects except 4d and spot
            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.DLP 
                && SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync4DCertification
                && SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary
                && SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.InkOptimization
                && SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.InkOptimizationSource)
                cmbTargetPrintCategory.Items.FindByValue("4").Enabled = false;
        }

        private void BindBranches()
        {
            CompanyID = SessionManager.Instance.SelectedCompany;
            _presenterTargetColorspace.GetBranchByCompany();
            if (!IsErrorOccured)
            {
                cmbBranch.DataValueField = "Key";
                cmbBranch.DataTextField = "Value";
                cmbBranch.DataSource = Branches;
                cmbBranch.DataBind();
                cmbBranch_SelectedIndexChanged(null, null);
            }
        }

        private void BindCertifiedSpotProjects()
        {
            CompanyID = SessionManager.Instance.SelectedCompany;
            BranchID = Convert.ToInt32(cmbBranch.SelectedValue.ToString());
            DeviceID = Convert.ToInt32(cmbDevice.SelectedValue.ToString());
            _presenterTargetColorspace.GetSpotReTargetProjectList();
            if (!IsErrorOccured)
            {
                if (ReTargetSpotProjectList.Count > 0)
                {
                    ddlReTargetProject.DataValueField = "Key";
                    ddlReTargetProject.DataTextField = "Value";
                    ddlReTargetProject.DataSource = ReTargetSpotProjectList;
                    ddlReTargetProject.DataBind();
                    ddlReTargetProject.Items.Insert(0, new ListItem(GetResourceString(_recSelect), string.Empty));
                }
                else
                {
                    ddlReTargetProject.Items.Clear();
                    ddlReTargetProject.Items.Insert(0, new ListItem(GetResourceString(_recNoCertifiedSpotCondition), string.Empty));
                }
            }
            divRetarget.Visible = true;
        }

        private void BindDevices()
        {
            CompanyID = SessionManager.Instance.SelectedCompany;
            BranchID = Convert.ToInt32(cmbBranch.SelectedValue.ToString());
            _presenterTargetColorspace.DeviceListForRetargetSpotProject();
            if (!IsErrorOccured)
            {
                cmbDevice.DataValueField = "DeviceId";
                cmbDevice.DataTextField = "DeviceName";
                cmbDevice.DataSource = Devices;
                cmbDevice.DataBind();
                cmbDevice.Items.Insert(0, new ListItem(GetResourceString(_recSelect), string.Empty));
            }
            divDevice.Visible = true;
        }

        private void BindRetargetSpotProjectDetails()
        {
            ReTargetID = Convert.ToInt32(ddlReTargetProject.SelectedValue.ToString());
            _presenterTargetColorspace.GetSpotReTargetProjectDetails();
            if (!IsErrorOccured)
            {
                lblParentProjectName.Text = RetargetSpotProject.ProjectName;
                lblLibraryName.Text = RetargetSpotProject.SpotTarget.SpotTargetName;
                lblRetargetDescription.Text = RetargetSpotProject.SpotTarget.Description;
            }
            divRetargetDetails.Visible = true;
        }


        protected void btn_PaperClear_Click(object sender, EventArgs e)
        {
            BindTargetLAB();    //Apply old Target LAB from Main look up table
            btn_PaperApply.Visible = true;
            btn_PaperClear.Visible = false;
        }

        protected void cmbSpotTargetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState[_vsSpotTarget] != null)
            {
                txtDescription.Text = ((List<Entity.CPSpot.SpotTarget>)ViewState[_vsSpotTarget]).Single(x => x.SpotTargetID.ToString() == cmbSpotTargetList.SelectedValue).Description;
                lblMeasMode.Text = "M" + ((List<Entity.CPSpot.SpotTarget>)ViewState[_vsSpotTarget]).Single(x => x.SpotTargetID.ToString() == cmbSpotTargetList.SelectedValue).MeasurementMode;
            }

            //for retarget spot project selection panel
            if (cmbSpotTargetList.SelectedValue.ToString() == "1") //-----Set no. 1 record for Custom made re target spot project
            {
                divReTargetPanel.Visible = true;
                PanelOptionalRetargetSpotProject.Visible = true;
                BindBranches();
            }
            else
            {
                divReTargetPanel.Visible = false;
                divDevice.Visible = false;
                divRetarget.Visible = false;
                divRetargetDetails.Visible = false;
                PanelOptionalRetargetSpotProject.Visible = false;
            }
        }

        protected void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBranch.SelectedValue.ToString() != "")
                BindDevices();
            else
                divDevice.Visible = false;

            divRetarget.Visible = false;
            divRetargetDetails.Visible = false;
        }

        protected void cmbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDevice.SelectedValue.ToString() != "")
                BindCertifiedSpotProjects();
            else
                divRetarget.Visible = false;


            divRetargetDetails.Visible = false;
        }

        protected void ddlReTargetProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReTargetProject.SelectedValue.ToString() != "")
                BindRetargetSpotProjectDetails();
            else
                divRetargetDetails.Visible = false;
        }

        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //Skip Profiler Options page for Spot Target project type
            {
                if (SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget == null)
                {
                    SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget = new Entity.CPSpot.SpotTarget()
                    {
                        SpotTargetID = ((List<Entity.CPSpot.SpotTarget>)ViewState[_vsSpotTarget]).Single(x => x.SpotTargetID.ToString() == cmbSpotTargetList.SelectedValue).SpotTargetID,
                        SpotTargetName = ((List<Entity.CPSpot.SpotTarget>)ViewState[_vsSpotTarget]).Single(x => x.SpotTargetID.ToString() == cmbSpotTargetList.SelectedValue).SpotTargetName
                    };
                    SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectId = SessionManager.Instance.CurrentProject.SpotTargetMasterProjectID;
                    if (cmbSpotTargetList.SelectedValue.ToString() == "1") //-----Set no. 1 record for Custom made re target spot project
                    {
                        SessionManager.Instance.CurrentProject.SpotTargetProject.ReTargetSpotProjectID = Convert.ToInt32(ddlReTargetProject.SelectedValue.ToString());
                        SessionManager.Instance.CurrentProject.ReTargetProjectBranchID = Convert.ToInt32(cmbBranch.SelectedValue.ToString());
                        SessionManager.Instance.CurrentProject.ReTargetProjectDeviceID = Convert.ToInt32(cmbDevice.SelectedValue.ToString());
                    }

                }
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            }
            else
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
            }
             ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();

        }

        protected void cmbTargetPrintCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProfileSource();
        }

        protected void chk_IsPaperScaling4D_CheckedChanged(object sender, EventArgs e)
        {
            Panel_PaperLAB4D_White.Enabled = Panel_PaperLAB4D_Black.Enabled = chk_IsPaperScaling4D.Checked;
            if (!chk_IsPaperScaling4D.Checked)
            {
                txtPaper_L_4D_White.Text = "0";
                txtPaper_a_4D_White.Text = "0";
                txtPaper_b_4D_White.Text = "0";
                txtPaper_L_4D_Black.Text = "0";
                txtPaper_a_4D_Black.Text = "0";
                txtPaper_b_4D_Black.Text = "0";
            }
        }

        #endregion

        #region IViewTargetColorspace Members
        public List<Entity.CPSync1D.PrintStandardType> PrintStandardTypeList { get; set; }
        public List<Entity.CPSync1D.ProjectTargetLAB> ProjectTargetLABList { get; set; }
        public List<Entity.CPSpot.SpotTarget> SpotTargetList { get; set; }
        public int PrintstandardTypeId { get; set; }
        public int ProjectTypeId { get; set; }
        public Dictionary<int, string> Branches { get; set; }
        public bool IsSync1DType
        {
            get
            {
                return (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D);
            }
        }
        public bool IsSpotTargetType
        {
            get
            {
                return (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary);
            }
        }
        public int TargetPrintCategory { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public int DeviceID { get; set; }
        public int ReTargetID { get; set; }
        public Dictionary<int, string> ReTargetSpotProjectList { get; set; }
        public List<Device> Devices { get; set; }
        public SpotProjectLibrary RetargetSpotProject { get; set; }
        #endregion

    }
}
