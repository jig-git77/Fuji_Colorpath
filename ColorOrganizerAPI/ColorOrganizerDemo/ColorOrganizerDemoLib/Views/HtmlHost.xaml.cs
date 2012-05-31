using System.ComponentModel;
using System.Windows;
using System.Windows.Browser;

namespace ColorOrganizerDemoLib.Views
{
    public partial class HtmlHost
    {
        private const string IFRAME =
            @"<iframe height=""100%"" width=""100%"" marginheight=""1"" marginwidth=""1"" src=""{0}""></iframe>";

        private const string ATTR_INNER_HTML = "innerHTML";
        private const string ATTR_LEFT = "left";
        private const string ATTR_TOP = "top";
        private const string ATTR_WIDTH = "width";
        private const string ATTR_HEIGHT = "height";
        private const string ATTR_VISIBILITY = "visibility";
        private const string VISIBLE = "visible";
        private const string HIDDEN = "hidden";
        private const string PX = "{0}px";
        private HtmlElement _div;

        private double _width, _height;

        public static DependencyProperty HostDivProperty = DependencyProperty.Register(
            "HostDiv",
            typeof(string),
            typeof(HtmlHost),
            null);

        public string HostDiv
        {
            get { return GetValue(HostDivProperty).ToString(); }

            set
            {
                SetValue(HostDivProperty, value);
                if (!DesignerProperties.IsInDesignTool)
                {
                    _div = HtmlPage.Document.GetElementById(value);
                }
            }
        }

        public static DependencyProperty UrlProperty = DependencyProperty.Register(
            "Url",
            typeof(string),
            typeof(HtmlHost),
            null);

        public string Url
        {
            get { return GetValue(UrlProperty).ToString(); }

            set
            {
                SetValue(UrlProperty, value);
                if (!DesignerProperties.IsInDesignTool)
                {
                    _div.SetProperty(ATTR_INNER_HTML, string.Format(IFRAME, value));
                }
            }
        }

        public void Show()
        {
            _div.RemoveStyleAttribute(ATTR_VISIBILITY);
            _div.SetStyleAttribute(ATTR_VISIBILITY, VISIBLE);
            //var tt = _div.Dispatcher;


            //WebRequest.RegisterPrefix("http://fabrikam.com/service", WebRequestCreator.ClientHttp);
            Application.Current.Host.Content.Resized += Content_Resized;
            HostGrid.SizeChanged += LayoutRoot_SizeChanged;
            _Resize();
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _width = e.NewSize.Width;
            _height = e.NewSize.Height;
        }

        private void Content_Resized(object sender, System.EventArgs e)
        {
            _Resize();
        }

        public void Hide()
        {
            _div.RemoveStyleAttribute(ATTR_VISIBILITY);
            _div.SetStyleAttribute(ATTR_VISIBILITY, HIDDEN);
            Application.Current.Host.Content.Resized -= Content_Resized;
            HostGrid.SizeChanged -= LayoutRoot_SizeChanged;
        }

        private void _Resize()
        {
            var gt = HostGrid.TransformToVisual(Application.Current.RootVisual);
            var offset = gt.Transform(new Point(0, 0));
            _div.RemoveStyleAttribute(ATTR_LEFT);
            _div.RemoveStyleAttribute(ATTR_TOP);
            _div.RemoveStyleAttribute(ATTR_WIDTH);
            _div.RemoveStyleAttribute(ATTR_HEIGHT);

            _div.SetStyleAttribute(ATTR_LEFT, string.Format(PX, offset.X));
            _div.SetStyleAttribute(ATTR_TOP, string.Format(PX, offset.Y));
            _div.SetStyleAttribute(ATTR_WIDTH, string.Format(PX, _width));
            _div.SetStyleAttribute(ATTR_HEIGHT,
                                   string.Format(PX, _height));
        }

        public HtmlHost()
        {
            InitializeComponent();
            if (DesignerProperties.IsInDesignTool)
            {
                return;
            }
            Loaded += (o, e) =>
                          {
                              _width = Width;
                              _height = Height;
                              if (_div != null)
                              {
                                  Show();
                              }
                          };
        }
    }
}