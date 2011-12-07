using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {                      
            if (!IsPostBack)
            {
                CreateLeftMenu();
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
                        sb.Append("<li class='active'><a>Measure</a></li>");
                        sb.Append("<li><a>Analyze</a></li>");
                        sb.Append("<li><a>Expert</a></li>");
                        sb.Append("</ul>");
                        sb.Append("</li>");                       
                    }
                    sb.Append("</ul");                                                            
                }               
            }
            sb.Append("<div class='clear'></div>");         
            divLeftmenu.InnerHtml = sb.ToString();
        }
    }
}