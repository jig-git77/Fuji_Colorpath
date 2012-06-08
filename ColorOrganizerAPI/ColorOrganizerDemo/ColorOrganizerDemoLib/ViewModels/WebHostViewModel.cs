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
    public class WebHostViewModel : BaseVM
    {
        public WebHostViewModel()
        {
            PreviousButtonCommand.Executed += (sender, e) => PassToParent(EControl.ChartPrint);
        }
    }
}
