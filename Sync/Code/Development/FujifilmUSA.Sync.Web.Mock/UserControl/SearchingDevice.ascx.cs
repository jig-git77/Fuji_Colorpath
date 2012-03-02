using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.Mock.UserControl
{
    public partial class SearchingDevice : System.Web.UI.UserControl
    {
        public event EventHandler MySearching;

        public string DeviceName
        {
            get
            {
                return txtDeviceName.Text;
            }
        }

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            string postBack = " function SearchEnterKeyPress(event) { if (e.keyCode == 13)  " + string.Format("__doPostBack('{0}','')", lbtnSearchDevice.ClientID) + "  }";
            txtDeviceName.Attributes.Add("OnKeyPress", postBack);
        }
        
        protected void lbtnSearchDevice_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Control events

        #endregion
      
    }

   

}

