using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
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
            var lstRemovable = new List<string>();
            for (int i = ph.Controls.Count - 1; i >= 0; i--)
            {
                if (ph.Controls[i].GetType().Name.ToLower().Contains("LeftMenuItem".ToLower()))
                {
                    switch (((LeftMenuItem)ph.Controls[i]).ItemLevel)
                    {
                        case LeftMenuItemLevel.Device:
                            if (SessionManager.Instance.CurrentSyncDevice == null)
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
                        case LeftMenuItemLevel.NewSyncRun:
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
                            if (SessionManager.Instance.CurrentProject == null)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case LeftMenuItemLevel.SyncRun:
                            if (SessionManager.Instance.CurrentProject == null)
                                ph.Controls.Remove(ph.Controls[i]);
                            break;
                        case LeftMenuItemLevel.NewSyncRun:
                            if (SessionManager.Instance.CurrentProject == null)
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

            var items = new List<System.Web.UI.UserControl>();
            var ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
            ctrlItem.ItemLevel = LeftMenuItemLevel.Home;
            ctrlItem.ItemName = "Home";
            ph.Controls.Add(ctrlItem);

            if (SessionManager.Instance.CurrentSyncDevice == null) return;
            ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
            ctrlItem.ItemLevel = LeftMenuItemLevel.Device;
            ctrlItem.ItemName = SessionManager.Instance.CurrentSyncDevice.DeviceName;
            ctrlItem.ID = "HomeControlId";
            ph.Controls.Add(ctrlItem);

            if (SessionManager.Instance.CurrentProject == null) return;

            ctrlItem = ((LeftMenuItem)this.LoadControl("~/UserControl/LeftMenuItem.ascx"));
            ctrlItem.ItemLevel = LeftMenuItemLevel.Project;
            ctrlItem.ItemName = SessionManager.Instance.CurrentProject.ProjectName;
            ph.Controls.Add(ctrlItem);

            var ctrlProjItem = ((LeftMenuProjectItem)this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
            ctrlProjItem.ItemLevel = LeftMenuItemLevel.ProjectSetup;
            ctrlProjItem.ItemName = "Project Setup";
            ph.Controls.Add(ctrlProjItem);

            foreach (var item in SessionManager.Instance.CurrentProject.ProjectRuns)
            {
                ctrlProjItem = ((LeftMenuProjectItem)this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
                ctrlProjItem.ItemLevel = LeftMenuItemLevel.SyncRun;
                ctrlProjItem.ItemName = item.SyncRunName;
                ctrlProjItem.CurrentSyncRun = item;
                ph.Controls.Add(ctrlProjItem);
            }

            if (SessionManager.Instance.CurrentProject.ProjectStatus == ECPSyncRunStatus.Closed)
            {
                ctrlProjItem = ((LeftMenuProjectItem)this.LoadControl("~/UserControl/LeftMenuProjectItem.ascx"));
                ctrlProjItem.ItemLevel = LeftMenuItemLevel.NewSyncRun;
                ctrlProjItem.ItemName = "Add New Run";
                ph.Controls.Add(ctrlProjItem);
            }
        }
        #endregion

    }
}
