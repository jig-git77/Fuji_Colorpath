using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl.AddProject
{
    public partial class ShowImage : BasePage, IViewMeasurementSetting
    {
        #region "Declaration"
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        public bool IsErrorOccured;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
            IsErrorOccured = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] image = null;

            //Get the client Image
            if (Request.QueryString["id"] != null)
            {
                ReferenceChartTypeId = Convert.ToInt32(Request.QueryString["id"]);
                _presenterMeasurementSetting.GetReferenceChartTypeImage();
                if (!IsErrorOccured)
                {
                    image = ReferenceChartTypeImage;
                }
            }

            if (image != null)
                RenderImage(image);
            else
                Response.End();

        }

        #endregion

        #region Methods
        private static void RenderImage(byte[] image)
        {
            var stream = new MemoryStream();
            stream.Write(image, 0, image.Length);
            var bitmap = new Bitmap(stream);
            System.Web.HttpContext.Current.Response.ContentType = "image/gif";
            bitmap.Save(System.Web.HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
        }
        #endregion

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
            IsErrorOccured = true;
        }

        #endregion
    }
}