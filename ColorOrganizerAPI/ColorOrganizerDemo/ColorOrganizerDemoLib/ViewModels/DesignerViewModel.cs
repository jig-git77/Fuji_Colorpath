using System.Windows;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class DesignerViewModel
    {
        public DesignerViewModel()
        {
            SelectDevice = new SelectDeviceViewModel()
                               {
                                   PassToParent = GetAtParent,
                                   Parent = this
                               };
            StartIccProfile = new StartICCProfileViewModel()
                                  {
                                      PassToParent = GetAtParent,
                                      Parent = this
                                  };
            ChartPrint = new ChartPrintViewModel()
                             {
                                 PassToParent = GetAtParent,
                                 Parent = this
                             };
            WebHost = new WebHostViewModel()
                          {
                              PassToParent = GetAtParent,
                              Parent = this
                          };

            SetCurrentControl(EControl.SelectDevice);

            //Silverlight.Extensions.MVVM.Bindings.PropertyNotificationBinding.Create(
            //    () => SelectDevice.SelectedDevice,
            //    () => ChartPrint.SelectedDevice);

            //Silverlight.Extensions.MVVM.Bindings.PropertyNotificationBinding.Create(
            //    () => StartIccProfile.SelectedIccProfile,
            //    () => ChartPrint.SelectedIccProfile);
        }

        public void GetAtParent(EControl command)
        {
            StartIccProfile.CurrentStatus = SelectDevice.CurrentStatus = ChartPrint.CurrentStatus = WebHost.CurrentStatus = Visibility.Collapsed;
            SetCurrentControl(command);
        }

        public StartICCProfileViewModel StartIccProfile { get; set; }
        public SelectDeviceViewModel SelectDevice { get; set; }
        public ChartPrintViewModel ChartPrint { get; set; }
        public WebHostViewModel WebHost { get; set; }

        private void SetCurrentControl(EControl control)
        {
            switch (control)
            {
                case EControl.StartICCProfile:
                    StartIccProfile.CurrentStatus = Visibility.Visible;
                    break;
                case EControl.SelectDevice:
                    SelectDevice.CurrentStatus = Visibility.Visible;
                    break;
                case EControl.ChartPrint:
                    ChartPrint.CurrentStatus = Visibility.Visible;
                    break;
                case EControl.WebHost:
                    WebHost.CurrentStatus = Visibility.Visible;
                    break;
            }
        }
    }
}