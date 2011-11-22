using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Login : System.Web.UI.UserControl, IViewTaskeroUser
    {
        #region "Declaration"
        private PresenterTaskeroUser _presentertaskerouser;        
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presentertaskerouser = new PresenterTaskeroUser(this);            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = "MWinn";
            Password = "fuji";
            _presentertaskerouser.TaskeroUserLogin();

        }
        #endregion

        #region IViewTaskeroUser Members
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AllowedIp { get; set; }                
        public Entity.TaskeroUser GetTaskeroUser { get; set; }
        #endregion
    }
}