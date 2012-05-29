using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class DesignerViewModel
    {
        public enum EControl
        {
            StartICCProfile,
            SelectDevice
        }

        public DesignerViewModel()
        {
            StartIccProfileViewVisibility = Visibility.Collapsed;
            SelectDeviceViewVisibility = Visibility.Collapsed;
            SetCurrentControl(EControl.SelectDevice);
            SelectDevice = new SelectDeviceViewModel();
            StartIccProfile = new StartICCProfileViewModel();
        }

        public StartICCProfileViewModel StartIccProfile { get; set; }
        public SelectDeviceViewModel SelectDevice { get; set; }

        public Visibility StartIccProfileViewVisibility { get; set; }
        public Visibility SelectDeviceViewVisibility { get; set; }

        private void SetCurrentControl(EControl control)
        {

            switch (control)
            {
                case EControl.StartICCProfile:
                    StartIccProfileViewVisibility = Visibility.Visible;
                    break;
                case EControl.SelectDevice:
                    SelectDeviceViewVisibility = Visibility.Visible;
                    break;
            }
        }
    }
}