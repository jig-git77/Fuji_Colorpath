using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class CmbDeviceType : System.Web.UI.UserControl, IViewAddProject
    {
        #region "Declaration"
        private PresenterAddProject _presenteraddproject;

        public ListItem DeviceType_SelectedItem
        {
            get { return cmbDeviceType.SelectedItem; }
        }

       public string DeviceType_Value
       {
                get { return cmbDeviceType.SelectedValue; }
                set { cmbDeviceType.SelectedValue = value; }
       }

       public string AlignmentChart_Value
       {
            get{ return cmbAlignmentChart.SelectedValue;}
            set { cmbAlignmentChart.SelectedValue = value; }
       }

       public bool AlignmentChart_Enable
       {
           get { return cmbAlignmentChart.Enabled ; }
           set { cmbAlignmentChart.Enabled = value; }
       }

       public bool DeviceType_Enable
       {
           get { return cmbDeviceType.Enabled; }
           set { cmbDeviceType.Enabled = value; }
       }

       public string CmbAlignmentChartValue
       {
           get { return cmbAlignmentChart.SelectedValue; }
           set { cmbAlignmentChart.SelectedValue = value; }
       }

       public string SetCmbDeviceTypeBySerialNumber
       {
           set  {
                       foreach (ListItem item in cmbDeviceType.Items)
                       {
                           if (item.Text.Contains(value))
                           {
                               item.Selected = true;
                           }
                           else
                           {
                               item.Selected = false;
                           }
                       }
                }
       }


      

        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenteraddproject = new PresenterAddProject(this);
            
            if (!Page.IsPostBack)
            {               
                BindMeasurementDeviceType();
                BindReferenceChartType();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        } 

        #endregion

        #region Methods
        private void BindMeasurementDeviceType()
        {
            if (SessionManager.Instance.CurrentSyncDevice != null)
            {
                this.SyncDeviceId = SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
                _presenteraddproject.GetMeasurementDeviceListBySyncDeviceId();
                cmbDeviceType.DataSource = GetMeasurementDeviceListBySyncDeviceId;
                cmbDeviceType.DataBind();
            }

        }
        private void BindReferenceChartType()
        {
            //Split device type and take the 2nd position number as MeasurementDeviceId
            this.MeasurementDeviceId = int.Parse(cmbDeviceType.SelectedValue.Split(new char[] { '-' })[1].Trim());
            _presenteraddproject.GetReferenceChartTypeListByMeasurementDeviceId();
            cmbAlignmentChart.DataSource = GetReferenceChartTypeListByMeasurementDeviceId;
            cmbAlignmentChart.DataBind();
        }
        #endregion

        #region IViewAddProject Members
        public Dictionary<int, string> GetSourceProfileList { get; set; }
        public Dictionary<int, string> GetDestinationProfiles { get; set; }
        public int SourceProfileId { get; set; }
        public int DestinationProfileId { get; set; }
        public List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
        public int SyncDeviceId { get; set; }
        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId { get; set; }
        public int MeasurementDeviceId { get; set; }
        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId { get; set; }
        public Project ObjProject { get; set; }
        public MeasurementSetting ObjMeasurementSetting { get; set; }
        public Project InsertProjectAndMeasurementSetting { get; set; }
        #endregion

       
    }
}