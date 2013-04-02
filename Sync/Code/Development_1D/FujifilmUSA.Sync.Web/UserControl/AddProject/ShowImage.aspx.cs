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
        private const string NoImageUrl = "Images/no_chart_preview.png";
        //private const string NoPreviewImageUrl = @"Images/chart_preview.png";
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
            if (Request.QueryString["id"] == null)
            {
                Response.End();
                return;
            }

            ReferenceChartTypeId = Convert.ToInt16(Request.QueryString["id"]);
            _presenterMeasurementSetting.GetReferenceChartTypeImage();
            if (!IsErrorOccured)
            {
                image = ReferenceChartTypeImage;
            }

            if (image != null)
            {
                if (Request.QueryString["Preview"] == null)
                {
                    RenderImage(image);
                }
                else
                {
                    RenderPreviewImage(image);
                }
            }
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
            System.Web.HttpContext.Current.Response.ContentType = "image/jpg";
            bitmap.Save(System.Web.HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
        }

        private static void RenderPreviewImage(byte[] image)
        {
            var stream = new MemoryStream();
            stream.Write(image, 0, image.Length);
            var bitmap = new Bitmap(stream);
            
            Image ImageResize = (Image)bitmap;
            SetImageSize(ImageResize);
        }

        static void SetImageSize(Image ImageResize)
        {
            double aw = 300;
            double ah = 245;
            double iw = ImageResize.Width;
            double ih = ImageResize.Height;

            double rw = aw / iw;
            double rh = ah / ih;
            double ratio;
            if (rw < rh)
            {
                ratio = rw;
            }
            else
            {
                ratio = rh;
            }
            int nw = Convert.ToInt32(iw * ratio);
            int nh = Convert.ToInt32(ih * ratio);                  

            System.Drawing.Bitmap resizedImage = new System.Drawing.Bitmap(nw, nh);
            System.Drawing.Graphics.FromImage((System.Drawing.Image)resizedImage).DrawImage(ImageResize, 0, 0, nw, nh);

            System.Web.HttpContext.Current.Response.ContentType = "image/jpg";
            resizedImage.Save(System.Web.HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);           
        }


        //public void DownloadImageFromUrl(string imageUrl)
        //{
        //    Image image = null;
        //    var webRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
        //    webRequest.AllowWriteStreamBuffering = true;
        //    webRequest.Timeout = 30000;
        //    var webResponse = webRequest.GetResponse();
        //    var stream = webResponse.GetResponseStream();
        //    if (stream != null) image = Image.FromStream(stream);
        //    webResponse.Close();
        //    if (image != null)
        //    {
        //        image.Save(System.Web.HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
        //    }
        //}

        #endregion

        #region IViewMeasurementSetting Members
        public List<Illuminant> Illumin { get; set; }
        public List<ObserverType> Observer { get; set; }
        public int DeviceId { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
        public int MeasurementDeviceId { get; set; }
        public List<ReferenceChartType> ReferenceChartType { get; set; }
        public byte[] ReferenceChartTypeImage { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ChartDisplayType { get; set; }
        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPreset { get; set; }
        public List<Entity.CPSync1D.ProjectTargetLAB> ProjectTargetLABList { get; set; }
        public int PrintstandardTypeId { get; set; }
        public int MetricTargetPresetID { get; set; }
        public ELicenseType LicenseType { get; set; }
        #endregion

        #region IViewBase Members

        public int CurrentUserId { get; set; }

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            IsErrorOccured = true;
        }

        #endregion        
    
    }
}
