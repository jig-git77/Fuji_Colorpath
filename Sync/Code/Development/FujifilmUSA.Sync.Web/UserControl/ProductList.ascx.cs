using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProductList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void grdProjectList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            dynamic data = new[] {
                new { ID = 1, ProjectName = "Test 1"},
                 new { ID = 11, ProjectName = "Test 12"},
                  new { ID = 1, ProjectName = "Test 1"},
                 new { ID = 11, ProjectName = "Test 12"},
                  new { ID = 1, ProjectName = "Test 1"},
                 new { ID = 11, ProjectName = "Test 12"}
                  
               
            };
            grdProjectList.DataSource = data;
        }
    }
}