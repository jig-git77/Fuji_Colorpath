using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls; 
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;


namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class LeftMenuItem : System.Web.UI.UserControl
    {
        #region Declaration
        public LeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }
        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
           
            switch (ItemLevel)
            {
                case LeftMenuItemLevel.Home:                    
                    imgItem.ImageUrl = "../images/home_icon.png";
                    imgItem.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Home;
                    break;
                case LeftMenuItemLevel.Device:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    imgItem.Font.Bold = SessionManager.Instance.CurrentNavigation == LeftMenuItemLevel.Device;
                    break;
                case LeftMenuItemLevel.Project:
                    imgItem.ImageUrl = "../images/print_icon.png";
                    imgItem.Enabled = false;
                    break;
            }            
            lnkItem.Text =ItemName;              
        }
        #endregion

        #region Control Event
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
                    GetProfileType();
                    break;
            }
        }
        #endregion

        #region Methods
        private void GetProfileType()
        {
            if (SessionManager.Instance.ProfileTypeID.ToString()!=string.Empty)
            {
                switch (SessionManager.Instance.ProfileTypeID)
                { 
                    case ProfileType.Optimization:
                        Response.Redirect("ProjectList.aspx");
                        break;
                    case ProfileType.DestinationProfile:
                        Response.Redirect("DestinationProfileList.aspx");
                        break;
                    case ProfileType.Dlp:
                        Response.Redirect("DeviceLinkProfileList.aspx");
                        break;

                }

            }
        }
        #endregion


    }
}