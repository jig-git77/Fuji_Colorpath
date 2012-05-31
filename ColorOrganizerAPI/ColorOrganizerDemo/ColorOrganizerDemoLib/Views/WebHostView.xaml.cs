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
using System.Windows.Browser;

namespace ColorOrganizerDemoLib.Views
{
    public partial class WebHostView : UserControl
    {
        public WebHostView()
        {
            HtmlPage.RegisterScriptableObject("mySLapp", this);
            InitializeComponent();
        }

        [ScriptableMember]
        public void Message(string eventData)
        {
            lblStatus.Text = string.Format("Message Received: " + eventData);
        }
    }
}
