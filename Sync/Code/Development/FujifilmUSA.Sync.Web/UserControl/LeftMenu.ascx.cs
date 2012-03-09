using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
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
                        case LeftMenuItemLevel.Device:
                            if (SessionManager.Instance.CurrentDevice == null)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case LeftMenuItemLevel.Project:
                            if (SessionManager.Instance.CurrentProject == null)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case LeftMenuItemLevel.SyncRun:
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
                        case LeftMenuItemLevel.ProjectSetup:
                            if (SessionManager.Instance.CurrentProject == null || !SessionManager.Instance.CurrentProject.IsNewlyCreated)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case LeftMenuItemLevel.SyncRun:
                            if (SessionManager.Instance.CurrentProject == null || SessionManager.Instance.CurrentProject.IsNewlyCreated)
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
            ctrlItem.ItemLevel = LeftMenuItemLevel.Home;
            ctrlItem.ItemName = "Home";
            ph.Controls.Add(ctrlItem);

            if (SessionManager.Instance.CurrentDevice == null) return;
            ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
            ctrlItem.ItemLevel = LeftMenuItemLevel.Device;
            ctrlItem.ItemName = SessionManager.Instance.CurrentDevice.DeviceName;
            ctrlItem.ID = "HomeControlId";
            ph.Controls.Add(ctrlItem);

            if (SessionManager.Instance.CurrentProject == null) return;
            ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
            ctrlItem.ItemLevel = LeftMenuItemLevel.Project;
            ctrlItem.ItemName = SessionManager.Instance.CurrentProject.ProjectName;
            ph.Controls.Add(ctrlItem);

            if (SessionManager.Instance.CurrentProject.IsNewlyCreated)
            {
                ph.Controls.Add(this.LoadControl("~/UserControl/LeftMenuProjectSetting.ascx"));
            }
            else
            {
                foreach (var item in SessionManager.Instance.CurrentProject.OptimizeRuns)
                {
                    var ctrlProjItem = ((LeftMenuProjectItem) this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
                    ctrlProjItem.ItemLevel = LeftMenuItemLevel.SyncRun;
                    ctrlProjItem.ItemName = item.RunName;
                    ctrlProjItem.CurrentSyncRun = item;
                    ph.Controls.Add(ctrlProjItem);
                }
            }
        }
        #endregion

    }
}
