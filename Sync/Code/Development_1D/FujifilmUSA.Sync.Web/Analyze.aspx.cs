using System;
namespace FujifilmUSA.Sync.Web
{
    public partial class Analyze : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [System.Web.Services.WebMethod]
        public static void Set1DMeasurementSmoothingSessionValue(string value)
        {
            Common.SessionManager.Instance.CurrentProject.Is1DMeasurementSmoothing = Convert.ToBoolean(value);
        }
    }
}