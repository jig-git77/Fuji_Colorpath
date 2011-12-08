using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuItem : System.Web.UI.UserControl
    {
        public LeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }        

        protected void Page_PreRender(object sender, EventArgs e)
        {
            switch (ItemLevel)
            {
                case LeftMenuItemLevel.Home:
                    imgItem.ImageUrl = "../images/home_icon.png";
                    break;
                case LeftMenuItemLevel.Device:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    break;
                case LeftMenuItemLevel.Project:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    break;
            }
            lnkItem.Text = ItemName;           
            
        }

        protected void lnkItem_Click(object sender, EventArgs e)
        {
            switch (this.ItemLevel)
            {
                case LeftMenuItemLevel.Home:
                    SessionManager.Instance.CurrentSyncDevice = null;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentSyncRun = null;
                    Response.Redirect("Index.aspx");
                    break;
                case LeftMenuItemLevel.Device:
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentSyncRun = null;
                    Response.Redirect("ProjectInformation.aspx");
                    break;
                case LeftMenuItemLevel.Project:
                    //do Nothing
                    break;
                case LeftMenuItemLevel.ProjectSetup:
                    SessionManager.Instance.CurrentProject = null;
                    break;
                case LeftMenuItemLevel.SyncRun:
                    break;
                case LeftMenuItemLevel.NewSyncRun:
                    break;
                default:
                    break;
            }             
        }
    }
}