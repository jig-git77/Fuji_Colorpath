using System.Collections.ObjectModel;
using System.Windows;
using ColorOrganizerDemoLib.Sync4DService;
using System;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class SelectDeviceViewModel : BaseVM
    {
        public SelectDeviceViewModel()
        {
            NextButtonCommand.Executed += (sender, e) => PassToParent(EControl.StartICCProfile);
            FillDeviceList();
        }

        
        public bool IsNextButtonEnable
        {
            get
            {
                return (SelectedDevice != null);
            }
        }

        private Device _selectedDevice;
        public Device SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
                RaisePropertyChanged("IsNextButtonEnable");
                RaisePropertyChanged("SelectedDevice");
            }
        }

        
        public ObservableCollection<Device> DeviceList { get; set; }

        private void FillDeviceList()
        {
            DeviceList = new ObservableCollection<Device>();
            var client = new Sync4DServiceContractClient("BasicHttpBinding_ISync4DServiceContract");
            client.GetDeviceListCompleted += delegate(object sender, GetDeviceListCompletedEventArgs e)
                                                 {
                                                     if (e.Error == null)
                                                     {
                                                         if (e.Result != null)
                                                         {
                                                             if (e.Result.Devices.Count > 0)
                                                             {
                                                                 foreach (var device in e.Result.Devices)
                                                                 {
                                                                     DeviceList.Add(device);
                                                                 }
                                                             }
                                                             else
                                                             {
                                                                 DeviceList.Add(new Device() { DeviceId = 0, DeviceName = "No device found" });
                                                             }
                                                         }
                                                     }
                                                     else
                                                     {
                                                         //e.Error
                                                     }
                                                 };
            client.GetDeviceListAsync(new GetDeviceListRequest()
                                          {
                                              SecurityToken = Common.SecurityToken,
                                              UserName = Common.UserName,
                                              UserPassword = Common.GetEncryptPassword()
                                          });

            //DeviceList = new ObservableCollection<Device>()
            //                 {
            //                     new Device() {DeviceId = 1, DeviceName = "Test Device 1"},
            //                     new Device() {DeviceId = 2, DeviceName = "Test Device 2"},
            //                     new Device() {DeviceId = 3, DeviceName = "Test Device 3"},
            //                     new Device() {DeviceId = 4, DeviceName = "Test Device 4"},
            //                     new Device() {DeviceId = 5, DeviceName = "Test Device 5"},
            //                 };
        }
    }
}
