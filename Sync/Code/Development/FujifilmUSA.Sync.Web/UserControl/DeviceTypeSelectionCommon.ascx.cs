using System;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class DeviceTypeSelectionCommon : System.Web.UI.UserControl, IViewGetMeasurementDeviceType
    {
        public ProfileType Type { get; set; }
        #region Variable Declarations

        private PresenterGetMeasurementDeviceType _presenterGetMeasurementDeviceType;

        public int InstrumentTypeId
        {
            get
            {
                return string.IsNullOrEmpty(cmbDeviceType.SelectedValue)
                           ? 0
                           : Convert.ToInt32(cmbDeviceType.SelectedValue.Split(new char[] { '-' })[0].Trim());
            }
        }

        public string SerialNumber
        {
            get
            {
                return string.IsNullOrEmpty(cmbDeviceType.SelectedValue)
                           ? string.Empty
                           : cmbDeviceType.SelectedValue.Split(new char[] { '-' })[2].Trim();
            }
            set
            {
                if (cmbDeviceType.Items.Cast<ListItem>().Where(x => x.Text.Contains(value)).Count() > 0)
                {
                    cmbDeviceType.Items.Cast<ListItem>().Where(x => x.Text.Contains(value)).First().Selected = true;
                }
            }
        }
        public string ReferenceChartTypeValue
        {
            get { return cmbAlignmentChart.SelectedValue; }
            set { cmbAlignmentChart.SelectedValue = value; }
        }

        public bool ReferenceChartTypeEnable
        {
            get { return cmbAlignmentChart.Enabled; }
            set { cmbAlignmentChart.Enabled = value; }
        }

        public bool DeviceTypeEnable
        {
            get { return cmbDeviceType.Enabled; }
            set { cmbDeviceType.Enabled = value; }
        }

        public Dictionary<string, string> DeviceTypeSource { get; set; }
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _presenterGetMeasurementDeviceType = new PresenterGetMeasurementDeviceType(this);
                SyncDeviceId = Common.SessionManager.Instance.CurrentSyncDevice.SyncDeviceId;
                _presenterGetMeasurementDeviceType.GetMeasurementDeviceListBySyncDeviceId();
                cmbDeviceType.DataSource = MeasurementDevices;
                cmbDeviceType.DataBind();
            }
        }
        #endregion

        #region IViewGetMeasurementDeviceType Members

        public Dictionary<string, string> MeasurementDevices { get; set; }

        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId { get; set; }

        #endregion
    }
}