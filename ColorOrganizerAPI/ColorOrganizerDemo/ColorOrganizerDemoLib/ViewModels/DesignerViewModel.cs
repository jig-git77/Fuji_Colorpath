using System.Windows;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class DesignerViewModel
    {
        public enum EControl
        {
            StartICCProfile,
            SelectDevice,
            ChartPrint,
            WebHost
        }

        public DesignerViewModel()
        {
            StartIccProfileViewVisibility = Visibility.Collapsed;
            SelectDeviceViewVisibility = Visibility.Collapsed;
            ChartPrintViewVisibility = Visibility.Collapsed;
            WebHostViewVisibility = Visibility.Collapsed;

            SetCurrentControl(EControl.SelectDevice);
            SelectDevice = new SelectDeviceViewModel();
            StartIccProfile = new StartICCProfileViewModel();
            ChartPrint = new ChartPrintViewModel();
            WebHost = new WebHostViewModel();
        }

        public StartICCProfileViewModel StartIccProfile { get; set; }
        public SelectDeviceViewModel SelectDevice { get; set; }
        public ChartPrintViewModel ChartPrint { get; set; }
        public WebHostViewModel WebHost { get; set; }

        public Visibility StartIccProfileViewVisibility { get; set; }
        public Visibility SelectDeviceViewVisibility { get; set; }
        public Visibility ChartPrintViewVisibility { get; set; }
        public Visibility WebHostViewVisibility { get; set; }

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
                case EControl.ChartPrint:
                    ChartPrintViewVisibility = Visibility.Visible;
                    break;
                case EControl.WebHost:
                    WebHostViewVisibility = Visibility.Visible;
                    break;
            }
        }
    }
}