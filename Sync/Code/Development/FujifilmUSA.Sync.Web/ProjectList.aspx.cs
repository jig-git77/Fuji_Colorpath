using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Device;
            if (!Page.IsPostBack)
            {                
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                //Response.Cache.SetNoStore();  
            }
            
        }
    }
}