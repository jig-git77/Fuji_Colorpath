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
using ColorOrganizerDemoLib.ViewModels;

namespace ColorOrganizerDemoLib
{
    public class ViewModelManager
    {
        static ViewModelManager()
        {
            StartIccProfile = new StartICCProfileViewModel();
        }
        
        public static StartICCProfileViewModel StartIccProfile;
    }
}
