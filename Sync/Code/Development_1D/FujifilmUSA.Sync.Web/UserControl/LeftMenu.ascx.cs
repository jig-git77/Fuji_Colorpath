using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenu : BaseControl
    {
        #region Constructor
        public LeftMenu()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        }
        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateLeftMenuList();
        }
        public void Page_PreRender(Object o, EventArgs e)
        {
            for (int i = ph.Controls.Count - 1; i >= 0; i--)
            {
                if (ph.Controls[i].GetType().Name.ToLower().Contains("LeftMenuItem".ToLower()))
                {
                    switch (((LeftMenuItem)ph.Controls[i]).ItemLevel)
                    {
                        case ELeftMenuItemLevel.Device:
                            if (SessionManager.Instance.CurrentDevice == null)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case ELeftMenuItemLevel.Project:
                            if (SessionManager.Instance.CurrentProject == null)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case ELeftMenuItemLevel.OptimizeRun:
                            if (SessionManager.Instance.CurrentProject == null)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        default:
                            break;
                    }
                }
                else if (ph.Controls[i].GetType().Name.ToLower().Contains("LeftMenuProjectItem".ToLower()))
                {
                    switch (((LeftMenuProjectItem)ph.Controls[i]).ItemLevel)
                    {
                        case ELeftMenuItemLevel.ProjectSetup:
                            if (SessionManager.Instance.CurrentProject == null ||
                                SessionManager.Instance.CurrentProject.CurrentDisplayStatus != ECurrentProjectStatus.New)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case ELeftMenuItemLevel.Validation:
                        case ELeftMenuItemLevel.OptimizeRun:
                            if (SessionManager.Instance.CurrentProject == null ||
                                SessionManager.Instance.CurrentProject.CurrentDisplayStatus == ECurrentProjectStatus.New)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        #endregion

        #region "Methods"
        public void CreateLeftMenuList()
        {
            ph.Controls.Clear();

            var ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
            ctrlItem.ItemLevel = ELeftMenuItemLevel.Home;
            if (SessionManager.Instance.CurrentBranchName != null)
            {
                ctrlItem.ItemName = SessionManager.Instance.CurrentBranchName;
            }
            else if (SessionManager.Instance.CurrentUser != null && SessionManager.Instance.CurrentUser.UserBranches != null && SessionManager.Instance.CurrentUser.UserBranches.Count == 1)
            {
                ctrlItem.ItemName = SessionManager.Instance.CurrentUser.UserBranches[0].Branch.BranchName;
                SessionManager.Instance.CurrentBranchName = SessionManager.Instance.CurrentUser.UserBranches[0].Branch.BranchName;
            }
            else if (SessionManager.Instance.CurrentUser != null && SessionManager.Instance.CurrentUser.UserCompany != null && SessionManager.Instance.CurrentUser.UserCompany.CompanyId > 0)
            {
                ctrlItem.ItemName = SessionManager.Instance.CurrentUser.UserCompany.Company.CompanyName;
            }
            else
            {
                ctrlItem.ItemName = GetResourceString("homeText");//"Home"
            }
            ctrlItem.ID = "_Home";
            ph.Controls.Add(ctrlItem);

            if (SessionManager.Instance.CurrentDevice == null) return;
            ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
            ctrlItem.ItemLevel = ELeftMenuItemLevel.Device;
            ctrlItem.ItemName = SessionManager.Instance.CurrentDevice.DeviceName;
            ctrlItem.ID = "_Device";
            ph.Controls.Add(ctrlItem);

            if (SessionManager.Instance.CurrentProject != null
                && (SessionManager.Instance.CurrentProject.CurrentDisplayStatus != ECurrentProjectStatus.New
                || (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count() == 1)))
            {
                ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
                ctrlItem.ItemLevel = ELeftMenuItemLevel.Operation; //Operation will also navigate to Device level
                ctrlItem.ItemName = Utilities.GetEnumDescriptionAttributeValue(SessionManager.Instance.CurrentProject.LicenseType);
                ctrlItem.ID = "_Operation";
                ph.Controls.Add(ctrlItem);
            }
            if (SessionManager.Instance.CurrentProject == null) return;


            switch (SessionManager.Instance.CurrentProject.CurrentDisplayStatus)
            {
                case ECurrentProjectStatus.New:
                    ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
                    ctrlItem.ItemLevel = ELeftMenuItemLevel.ProjectSetup;
                    ctrlItem.ItemName = SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary ? //if current project is spotlibrary
                        (string.IsNullOrEmpty(SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName) // name from the spotproject
                                            ? GetResourceString("newPrintConditionText")
                                            : SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName) :
                        (string.IsNullOrEmpty(SessionManager.Instance.CurrentProject.ProjectName)               // else name from the parent project
                                            ? GetResourceString("newPrintConditionText")
                                            : SessionManager.Instance.CurrentProject.ProjectName);
                    ph.Controls.Add(ctrlItem);
                    ph.Controls.Add(this.LoadControl("~/UserControl/LeftMenuProjectSetting.ascx"));
                    break;
                case ECurrentProjectStatus.Edit:
                    ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
                    ctrlItem.ItemLevel = ELeftMenuItemLevel.Project;
                    ctrlItem.ItemName = SessionManager.Instance.CurrentProject.ProjectName;
                    ph.Controls.Add(ctrlItem);

                    ph.Controls.Add(this.LoadControl("~/UserControl/LeftMenuProjectSetting.ascx"));

                    if (SessionManager.Instance.CurrentProject.OptimizeRuns == null) return;
                    foreach (var item in SessionManager.Instance.CurrentProject.OptimizeRuns)
                    {
                        if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation ||
                            item.OptimizeRunId != SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                        {
                            var ctrlProjItem = ((LeftMenuProjectItem)this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
                            ctrlProjItem.ItemLevel = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                                                         ? ELeftMenuItemLevel.Validation
                                                         : ELeftMenuItemLevel.OptimizeRun;
                            ctrlProjItem.ItemName = item.RunName;
                            ctrlProjItem.CurrentRun = item;
                            ph.Controls.Add(ctrlProjItem);
                        }
                    }
                    break;
                case ECurrentProjectStatus.Run:
                    ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
                    ctrlItem.ItemLevel = ELeftMenuItemLevel.Project;
                    //ctrlItem.ItemName = SessionManager.Instance.CurrentProject.ProjectName;
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)//if current project is spot then optimizatoin run should be spot object
                    {
                        ctrlItem.ItemName = SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName;
                        ph.Controls.Add(ctrlItem);

                        if (SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns == null) return;
                        foreach (var item in SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns)
                        {
                            if (item.OptimizeRunId != SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.First().OptimizeRunId ||
                                (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation))
                            {
                                var ctrlProjItem = ((LeftMenuProjectItem)this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
                                ctrlProjItem.ItemLevel = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                                                             ? ELeftMenuItemLevel.Validation
                                                             : ELeftMenuItemLevel.OptimizeRun;
                                ctrlProjItem.ItemName = item.RunName;
                                ctrlProjItem.CurrentRun = item;
                                ph.Controls.Add(ctrlProjItem);
                            }
                        }
                    }
                    else
                    {
                        ctrlItem.ItemName = SessionManager.Instance.CurrentProject.ProjectName;
                        ph.Controls.Add(ctrlItem);

                        if (SessionManager.Instance.CurrentProject.OptimizeRuns == null) return;
                        foreach (var item in SessionManager.Instance.CurrentProject.OptimizeRuns)
                        {
                            if (item.OptimizeRunId != SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId ||
                                (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation))
                            {
                                var ctrlProjItem = ((LeftMenuProjectItem)this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
                                ctrlProjItem.ItemLevel = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                                                             ? ELeftMenuItemLevel.Validation
                                                             : ELeftMenuItemLevel.OptimizeRun;
                                ctrlProjItem.ItemName = item.RunName;
                                ctrlProjItem.CurrentRun = item;
                                ph.Controls.Add(ctrlProjItem);
                            }
                        }
                    }

                    //ph.Controls.Add(ctrlItem);

                    //if (SessionManager.Instance.CurrentProject.OptimizeRuns == null) return;
                    //foreach (var item in SessionManager.Instance.CurrentProject.OptimizeRuns)
                    //{
                    //    if (item.OptimizeRunId != SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId ||
                    //        (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation))
                    //    {
                    //        var ctrlProjItem = ((LeftMenuProjectItem)this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
                    //        ctrlProjItem.ItemLevel = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                    //                                     ? ELeftMenuItemLevel.Validation
                    //                                     : ELeftMenuItemLevel.OptimizeRun;
                    //        ctrlProjItem.ItemName = item.RunName;
                    //        ctrlProjItem.CurrentRun = item;
                    //        ph.Controls.Add(ctrlProjItem);
                    //    }
                    //}
                    break;
            }
        }
        #endregion

    }
}
