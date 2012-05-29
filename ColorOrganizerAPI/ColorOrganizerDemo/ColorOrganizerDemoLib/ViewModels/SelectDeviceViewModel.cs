using ColorOrganizerDemoLib.Sync4DService;
using System.Collections.Generic;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class SelectDeviceViewModel
    {
        private Device _selectedDevice;
        public SelectDeviceViewModel()
        {
            DeviceList = new List<Device>();
            FillDeviceList();
        }

        public Device SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
            }
        }

        private void FillDeviceList()
        {
            DeviceList = new List<Device>()
                             {
                                 new Device() {DeviceId = 1, DeviceName = "Test Device 1"},
                                 new Device() {DeviceId = 2, DeviceName = "Test Device 2"},
                                 new Device() {DeviceId = 3, DeviceName = "Test Device 3"},
                                 new Device() {DeviceId = 4, DeviceName = "Test Device 4"},
                                 new Device() {DeviceId = 5, DeviceName = "Test Device 5"},
                             };
        }

        public List<Device> DeviceList { get; set; }
    }
}
