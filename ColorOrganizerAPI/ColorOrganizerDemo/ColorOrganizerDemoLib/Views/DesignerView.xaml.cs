using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ColorOrganizerDemoLib.ViewModels;

namespace ColorOrganizerDemoLib.Views
{
    public partial class DesignerView : UserControl
    {
        private Dictionary<string, string> _initParameters;
        public Dictionary<string, string> InitParameters
        {
            get { return _initParameters; }
            set
            {
                _initParameters = value;
                Common.FujiWebSite = _initParameters.Single(x => x.Key.Equals("FujiUSAWebSource")).Value;
            }
        }

        public DesignerView()
        {
            InitializeComponent();
            DataContext = new DesignerViewModel();
        }
    }
}
