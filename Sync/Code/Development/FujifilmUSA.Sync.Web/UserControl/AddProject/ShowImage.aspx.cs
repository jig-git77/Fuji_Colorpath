using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace FujifilmUSA.Sync.Web.UserControl.AddProject
{
    public partial class ShowImage : BasePage, IViewMeasurementSetting
    {
        #region "Declaration"
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        private string vsIsPostBack = "_IsPostBack";
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] image = null;

            //Get the client Image
            if (Request.QueryString["id"] != null)
            {
                ReferenceChartTypeId = Convert.ToInt32(Request.QueryString["id"]);
               _presenterMeasurementSetting.GetReferenceChartTypeImage();
               image = ReferenceChartTypeImage;
            }

            if (image != null)
                RenderImage(image);
            else
                Response.End();

        }

        #endregion

        private void RenderImage(byte[] image)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(image, 0, image.Length);
            Bitmap bitmap = new Bitmap(stream);
            System.Web.HttpContext.Current.Response.ContentType = "image/gif";
            bitmap.Save(System.Web.HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
        }

        #region IViewMeasurementSetting Members
        public List<Illuminant> Illumin { get; set; }
        public List<ObserverType> Observer { get; set; }
        public int DeviceId { get; set; }
        public Dictionary<int, string> Measurement { get; set; }
        public int MeasurementDeviceId { get; set; }
        public List<ReferenceChartType> ReferenceChartType { get; set; }
        public byte[] ReferenceChartTypeImage { get; set; }
        public int ReferenceChartTypeId { get; set; }
        #endregion

        #region IViewError Members

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}