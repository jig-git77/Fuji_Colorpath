using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class ProjectList : System.Web.UI.UserControl
    {
        #region "Declaration"
        public string ProjectList_DeleteProject_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("ProjectList_DeleteProject_Confirmation");
            }
        }
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionManager.CheckLogin();
            SessionManager.Instance.ProfileTypeID = ProfileType.Optimization;
            if (!IsPostBack)
            {
                BindProject();
            }
        }
        #endregion

      
        #region Control events
        protected void lbtnCreateNewPrintCondition_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProject.aspx?Stage=1");
        }
        protected void rgProjectList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string arg = e.CommandArgument as string;
            }
            if (e.CommandName == "Delete")
            {
                string arg = e.CommandArgument as string;
            }
        }
        protected void rgProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgProjectList.DataSource = GetProjectList();
        }
        protected void rgProjectList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            var project = e.Item.DataItem as MockProject;
            if (e.Item.ItemType == GridItemType.FilteringItem)
            {
                //if (e.Item.FindControl("RadComboBoxStatus") != null)
                //    BindSearchSyncRunStatus(e.Item.FindControl("RadComboBoxStatus") as RadComboBox);
                //if (e.Item.FindControl("RadComboBoxPrintStandard") != null)
                //    BindSearchPrintStandardType(e.Item.FindControl("RadComboBoxPrintStandard") as RadComboBox);
            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                var btnSelect = e.Item.FindControl("imgbtnSelect") as ImageButton;
                var imgStatus = e.Item.FindControl("imgStatus") as Image;
                if (imgStatus != null)
                {
                    switch (project.ProjectStatus)
                    {
                        case ECPSyncRunStatus.Open:
                            if (btnSelect != null)
                                btnSelect.Attributes.Add("onclick", "javascript: return ShowOpenPrintConditionWindow('" + project.ProjectName + "')");
//                                btnSelect.Attributes.Add("onclick", "javascript: return OpenInformationBox('" + ResolveUrl("~/PrintCondition.aspx?ModelControlId=1&ProjectName=" + project.ProjectName) + "')");
                            imgStatus.ImageUrl = ResolveUrl("../Images/Project_Open.png");
                            imgStatus.ToolTip = ECPSyncRunStatus.Open.ToString();
                            imgStatus.AlternateText = ECPSyncRunStatus.Open.ToString();
                            break;
                        case ECPSyncRunStatus.Certify:
                            if (btnSelect != null)
                                btnSelect.Attributes.Add("onclick", "javascript: return false;");
                            imgStatus.ImageUrl = ResolveUrl("../Images/Project_Certify.png");
                            imgStatus.ToolTip = ECPSyncRunStatus.Certify.ToString();
                            imgStatus.AlternateText = ECPSyncRunStatus.Certify.ToString();
                            break;
                        case ECPSyncRunStatus.Closed:
                            if (btnSelect != null)
                                  btnSelect.Attributes.Add("onclick", "javascript: return ShowClosePrintConditionWindow('" + project.ProjectName + "')");
                            //    btnSelect.Attributes.Add("onclick", "javascript: return OpenInformationBox('" + ResolveUrl("~/PrintCondition.aspx?ModelControlId=2&ProjectName=" + project.ProjectName) + "')");
                            imgStatus.ImageUrl = ResolveUrl("../Images/Project_Close.png");
                            imgStatus.ToolTip = ECPSyncRunStatus.Closed.ToString();
                            imgStatus.AlternateText = ECPSyncRunStatus.Closed.ToString();
                            break;
                        case ECPSyncRunStatus.NotUsed:
                            if (btnSelect != null)
                                btnSelect.Attributes.Add("onclick", "javascript: return false;");
                            imgStatus.ImageUrl = ResolveUrl("../Images/no_image.png");
                            break;
                    }
                }
                var lblStandard = e.Item.FindControl("lblStandard") as Label;
                if (lblStandard != null)
                    lblStandard.Text = project.PrintStandardType.ToString();
            }
        }
        #endregion

        #region Method
        private void BindSearchSyncRunStatus(RadComboBox SyncRunStatus)
        {
            Dictionary<int, string> dic = Utilities.GetEnumDropdown<ECPSyncRunStatus>();
            SyncRunStatus.DataSource = dic;
            SyncRunStatus.DataTextField = "Value";
            SyncRunStatus.DataValueField = "Key";
            SyncRunStatus.DataBind();
        }
        private void BindSearchPrintStandardType(RadComboBox cmbSearchPrintStandardType)
        {
            cmbSearchPrintStandardType.DataSource = Utilities.GetEnumDropdown<EPrintStandardType>();
            cmbSearchPrintStandardType.DataTextField = "Value";
            cmbSearchPrintStandardType.DataValueField = "Key";
            cmbSearchPrintStandardType.DataBind();
        }
        protected DateTime? CreatedDateDate(GridItem item)
        {
            if (item.OwnerTableView.GetColumn("CreatedDate").CurrentFilterValue == string.Empty)
            {
                return new DateTime?();
            }
            else
            {
                var dateValue = item.OwnerTableView.GetColumn("CreatedDate").CurrentFilterValue.Split(' ')[0];
                return DateTime.Parse(dateValue);
            }
        }
        private void BindProject()
        {
            List<MockProject> lst = new List<MockProject>();
            lst = GetProjectList();
            rgProjectList.DataSource = lst;
            rgProjectList.DataBind();
        }
        #endregion

        #region Error Handling Method
        //public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        //{
        //    ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        //}
        #endregion

        private List<MockProject> GetProjectList()
        {
            List<MockProject> projectList = new List<MockProject>();
            projectList.Add(new MockProject() { ProjectId = 78, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "OptimizeXeroxDlpCustom OptimizeXeroxDlpCustom", Media = "Media 1", Colorant = "Colorant 1", CreatedDate = Convert.ToDateTime("2012-01-16 17:12:23.050"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 3 });
            projectList.Add(new MockProject() { ProjectId = 79, ProjectStatus = ECPSyncRunStatus.Certify, PrintStandardType = EPrintStandardType.PSO_12647_2, ProjectName = "Edit Testing", Media = "Media 2", Colorant = "Colorant 2", CreatedDate = Convert.ToDateTime("2012-01-17 10:52:04.170"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 80, ProjectStatus = ECPSyncRunStatus.Closed, PrintStandardType = EPrintStandardType.SWOP_3, ProjectName = "Edit Test2", Media = "Media 3", Colorant = "Colorant 3", CreatedDate = Convert.ToDateTime("2012-01-18 12:49:18.577"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 81, ProjectStatus = ECPSyncRunStatus.NotUsed, PrintStandardType = EPrintStandardType.SWOP_5, ProjectName = "EditProjectTest", Media = "Media 4", Colorant = "Colorant 4", CreatedDate = Convert.ToDateTime("2012-02-01 12:03:51.460"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 82, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "tREDST", Media = "Media 5", Colorant = "Colorant 5", CreatedDate = Convert.ToDateTime("2012-02-23 13:45:59.590"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 78, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "OptimizeXeroxDlpCustom OptimizeXeroxDlpCustom", Media = "Media 1", Colorant = "Colorant 1", CreatedDate = Convert.ToDateTime("2012-01-16 17:12:23.050"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 3 });
            projectList.Add(new MockProject() { ProjectId = 79, ProjectStatus = ECPSyncRunStatus.Certify, PrintStandardType = EPrintStandardType.PSO_12647_2, ProjectName = "Edit Testing", Media = "Media 2", Colorant = "Colorant 2", CreatedDate = Convert.ToDateTime("2012-01-30 10:52:04.170"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 80, ProjectStatus = ECPSyncRunStatus.Closed, PrintStandardType = EPrintStandardType.SWOP_3, ProjectName = "Edit Test2", Media = "Media 3", Colorant = "Colorant 3", CreatedDate = Convert.ToDateTime("2012-01-30 12:49:18.577"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 81, ProjectStatus = ECPSyncRunStatus.NotUsed, PrintStandardType = EPrintStandardType.SWOP_5, ProjectName = "EditProjectTest", Media = "Media 4", Colorant = "Colorant 4", CreatedDate = Convert.ToDateTime("2012-02-01 12:03:51.460"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 82, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "tREDST", Media = "Media 5", Colorant = "Colorant 5", CreatedDate = Convert.ToDateTime("2012-02-23 13:45:59.590"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 78, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "OptimizeXeroxDlpCustom OptimizeXeroxDlpCustom", Media = "Media 1", Colorant = "Colorant 1", CreatedDate = Convert.ToDateTime("2012-01-16 17:12:23.050"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 3 });
            projectList.Add(new MockProject() { ProjectId = 79, ProjectStatus = ECPSyncRunStatus.Certify, PrintStandardType = EPrintStandardType.PSO_12647_2, ProjectName = "Edit Testing", Media = "Media 2", Colorant = "Colorant 2", CreatedDate = Convert.ToDateTime("2012-01-30 10:52:04.170"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 80, ProjectStatus = ECPSyncRunStatus.Closed, PrintStandardType = EPrintStandardType.SWOP_3, ProjectName = "Edit Test2", Media = "Media 3", Colorant = "Colorant 3", CreatedDate = Convert.ToDateTime("2012-01-30 12:49:18.577"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 81, ProjectStatus = ECPSyncRunStatus.NotUsed, PrintStandardType = EPrintStandardType.SWOP_5, ProjectName = "EditProjectTest", Media = "Media 4", Colorant = "Colorant 4", CreatedDate = Convert.ToDateTime("2012-02-01 12:03:51.460"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 82, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "tREDST", Media = "Media 5", Colorant = "Colorant 5", CreatedDate = Convert.ToDateTime("2012-02-23 13:45:59.590"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 78, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "OptimizeXeroxDlpCustom OptimizeXeroxDlpCustom", Media = "Media 1", Colorant = "Colorant 1", CreatedDate = Convert.ToDateTime("2012-01-16 17:12:23.050"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 3 });
            projectList.Add(new MockProject() { ProjectId = 79, ProjectStatus = ECPSyncRunStatus.Certify, PrintStandardType = EPrintStandardType.PSO_12647_2, ProjectName = "Edit Testing", Media = "Media 2", Colorant = "Colorant 2", CreatedDate = Convert.ToDateTime("2012-01-30 10:52:04.170"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 80, ProjectStatus = ECPSyncRunStatus.Closed, PrintStandardType = EPrintStandardType.SWOP_3, ProjectName = "Edit Test2", Media = "Media 3", Colorant = "Colorant 3", CreatedDate = Convert.ToDateTime("2012-01-30 12:49:18.577"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 81, ProjectStatus = ECPSyncRunStatus.NotUsed, PrintStandardType = EPrintStandardType.SWOP_5, ProjectName = "EditProjectTest", Media = "Media 4", Colorant = "Colorant 4", CreatedDate = Convert.ToDateTime("2012-02-01 12:03:51.460"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 82, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "tREDST", Media = "Media 5", Colorant = "Colorant 5", CreatedDate = Convert.ToDateTime("2012-02-23 13:45:59.590"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 78, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "OptimizeXeroxDlpCustom OptimizeXeroxDlpCustom", Media = "Media 1", Colorant = "Colorant 1", CreatedDate = Convert.ToDateTime("2012-01-16 17:12:23.050"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 3 });
            projectList.Add(new MockProject() { ProjectId = 79, ProjectStatus = ECPSyncRunStatus.Certify, PrintStandardType = EPrintStandardType.PSO_12647_2, ProjectName = "Edit Testing", Media = "Media 2", Colorant = "Colorant 2", CreatedDate = Convert.ToDateTime("2012-01-30 10:52:04.170"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 80, ProjectStatus = ECPSyncRunStatus.Closed, PrintStandardType = EPrintStandardType.SWOP_3, ProjectName = "Edit Test2", Media = "Media 3", Colorant = "Colorant 3", CreatedDate = Convert.ToDateTime("2012-01-30 12:49:18.577"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 81, ProjectStatus = ECPSyncRunStatus.NotUsed, PrintStandardType = EPrintStandardType.SWOP_5, ProjectName = "EditProjectTest", Media = "Media 4", Colorant = "Colorant 4", CreatedDate = Convert.ToDateTime("2012-02-01 12:03:51.460"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 82, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "tREDST", Media = "Media 5", Colorant = "Colorant 5", CreatedDate = Convert.ToDateTime("2012-02-23 13:45:59.590"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 78, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "OptimizeXeroxDlpCustom OptimizeXeroxDlpCustom", Media = "Media 1", Colorant = "Colorant 1", CreatedDate = Convert.ToDateTime("2012-01-16 17:12:23.050"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 3 });
            projectList.Add(new MockProject() { ProjectId = 79, ProjectStatus = ECPSyncRunStatus.Certify, PrintStandardType = EPrintStandardType.PSO_12647_2, ProjectName = "Edit Testing", Media = "Media 2", Colorant = "Colorant 2", CreatedDate = Convert.ToDateTime("2012-01-30 10:52:04.170"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 80, ProjectStatus = ECPSyncRunStatus.Closed, PrintStandardType = EPrintStandardType.SWOP_3, ProjectName = "Edit Test2", Media = "Media 3", Colorant = "Colorant 3", CreatedDate = Convert.ToDateTime("2012-01-30 12:49:18.577"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 81, ProjectStatus = ECPSyncRunStatus.NotUsed, PrintStandardType = EPrintStandardType.SWOP_5, ProjectName = "EditProjectTest", Media = "Media 4", Colorant = "Colorant 4", CreatedDate = Convert.ToDateTime("2012-02-01 12:03:51.460"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 17, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
            projectList.Add(new MockProject() { ProjectId = 82, ProjectStatus = ECPSyncRunStatus.Open, PrintStandardType = EPrintStandardType.GRACoL, ProjectName = "tREDST", Media = "Media 5", Colorant = "Colorant 5", CreatedDate = Convert.ToDateTime("2012-02-23 13:45:59.590"), SyncDeviceId = 6, ΔEType = 1, DeviceLinkProfileId = 4, IsUsed = true, UseDLP = true, ProfileOptionId = 1 });
 
            return projectList;
        }

        
        //*******************************************************************//
        //#region IViewProjectList Members
        //public List<Project> Projects { get; set;}
        //#endregion

        //#region IViewBaseProject Members
        //public Project Project{ get; set;}
        //#endregion
    }

    public class MockProject
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SyncDeviceId { get; set; }
        public int ΔEType { get; set; }
        public int DeviceLinkProfileId { get; set; }
        public Boolean IsUsed { get; set; }
        public Boolean UseDLP { get; set; }
        public int ReTargetId { get; set; }

        public int ProfileOptionId { get; set; }
        //public int ProjectTypeId { get; set; }
        //public int OutputTypeId { get; set; }

        public DateTime StartDate { get; set; }
        public int ReferenceChartTypeId { get; set; }
        public List<SyncRun> ProjectRuns { get; set; }
        public ECPSyncRunStatus ProjectStatus { get; set; }

        //Add For DataFiled of RadGrid
        public int ProjectStatusId { get { return Convert.ToInt32(ProjectStatus); } }

        //Add Standar Type 
        public int PrintStandardTypeId { get { return Convert.ToInt32(PrintStandardType); } }
        public EPrintStandardType PrintStandardType { get; set; }


        public string Media { get; set; }
        public string Colorant { get; set; }


    }
}