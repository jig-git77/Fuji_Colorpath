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
        protected void Page_Load(object sender, EventArgs e)
        {
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
        public void CreateLeftMenu()
        {
            string[] currentURLList = Request.Url.ToString().Split(new char[] { '/' });
            string currentPage = currentURLList[currentURLList.Length - 1].Trim();

            if (currentPage == "Index.aspx")
            {
                SessionManager.Instance.CurrentSyncDevice = null;
                SessionManager.Instance.CurrentProject = null;
            }

            if (currentPage == "ProjectList.aspx")
            {
                SessionManager.Instance.CurrentProject = null;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul id='menu'><li>");
            sb.Append("<a href='Index.aspx'><span class='image'><img src='images/home_icon.png' alt=''></span><span class='text'>Home</span></a></li>");
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                sb.Append("<li>");
                sb.Append("<a href='ProjectList.aspx'><span class='image'><img src='images/print_icon.png' alt=''></span><span class='text'>");
                sb.Append(SessionManager.Instance.CurrentSyncDevice.DeviceName);
                sb.Append("</span></a>");
                sb.Append("</li>");
                if (currentPage == "AddProject.aspx")
                {
                    sb.Append("<ul id='menu2'><li><span>New Project</span></li></ul>");
                    sb.Append("<ul id='menu2'><li><span>Project SetUp</span></li></ul>");
                }
                if (SessionManager.Instance.CurrentProject != null)
                {
                    sb.Append("<li>");
                    sb.Append("<a href='#'><span class='image'><img src='images/print_icon.png' alt=''></span><span class='text'>");
                    sb.Append(SessionManager.Instance.CurrentProject.ProjectName);
                    sb.Append("</span></a>");
                    sb.Append("<div class='clear'></div>");
                    sb.Append("</li>");
                    sb.Append("</ul>");
                    sb.Append("<ul id='menu2'>");
                    sb.Append("<li><span>Project SetUp</span></li>");
                    foreach (var item in SessionManager.Instance.CurrentProject.ProjectRuns)
                    {
                        sb.Append("<li><span>");
                        sb.Append(item.SyncRunName);
                        sb.Append("</span>");
                        sb.Append("<ul class='submenu'>");
                        sb.Append("<li class='active'><a href='AddMeasurement.aspx'>Measure</a></li>");
                        sb.Append("<li><a>Analyze</a></li>");
                        sb.Append("<li><a href='SyncRunExport.aspx'>Expert</a></li>");
                        sb.Append("</ul>");
                        sb.Append("</li>");

                        if (SessionManager.Instance.CurrentProject.ProjectRuns.Last().Status == 0)
                        {
                            sb.Append("<li><a>New Project</a></li>");
                        }
                    }
                    sb.Append("</ul");
                }
            }
            sb.Append("<div class='clear'></div>");
            divLeftmenu.InnerHtml = sb.ToString();
        }

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
    }
}
