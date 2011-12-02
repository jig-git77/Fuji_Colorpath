using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdDLP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            dynamic data = new[] {
                new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
                new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
                    new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"},
                        new { Source = "123456", Paper = "paper3",PrintVariables="ass",Separations="asasasas"}
            };
            grdDLP.DataSource = data;
        }
    }
}