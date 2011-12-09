using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMeasurement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            if(!IsPostBack)
            {
                hdProjectID.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                hdSyncRunID.Value = "1";
                hdMeasurementTypeID.Value = "5";
            }
        }

        protected void grdRecentUploadList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            dynamic data = new[] {
                new { MeasurementName = "123456", UploadDateTime = DateTime.Now},
                 new { MeasurementName = "654231", UploadDateTime = DateTime.Now.AddMinutes(10)}
                
            };
            grdRecentUploadList.DataSource = data;
        }
    }
}