#region System
using System;
using System.Data;
using System.Web;
using Taskero.Business;
#endregion

namespace TaskeroWeb
{

    public partial class DisplayJobPreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack == false)
            {
                DisplayPreview();

            }

        }


        private void DisplayPreview()
        {
            try
            {
                //========== Gets the information on DeviceID ==========//
                CPMeasurementData objCPMeasurement = new CPMeasurementData();
                objCPMeasurement.CPMeasurementGUID = Convert.ToString(Request.QueryString["CPMeasurementGUID"]);
                DataTable DT = objCPMeasurement.uspGetCPJobPreview();

                if (DT.Rows.Count > 0)
                {
                    byte[] imgbyte = (byte[])DT.Rows[0]["JobImage"];

                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "";
                    Response.AddHeader("content-disposition", "attachment;filename=preview.jpg");
                    Response.BinaryWrite(imgbyte);
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}