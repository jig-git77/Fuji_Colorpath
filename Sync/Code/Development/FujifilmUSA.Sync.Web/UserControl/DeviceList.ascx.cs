using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class DeviceList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void lvDeviceList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            dynamic data = new[] {
                new { ID = 1, ID2 = "Test 1"},
                 new { ID = 11, ID2 = "Test 12"},
                  new { ID = 1, ID2 = "Test 1"},
                 new { ID = 11, ID2 = "Test 12"},
                  new { ID = 1, ID2 = "Test 1"},
                 new { ID = 11, ID2 = "Test 12"}
                  
               
            };
            lvDeviceList.DataSource = data;
        }
    }
}