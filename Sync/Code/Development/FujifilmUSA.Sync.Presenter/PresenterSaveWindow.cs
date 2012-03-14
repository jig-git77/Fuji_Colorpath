using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSaveWindow
    {
        private IViewSaveWindow _viewSaveWindow;

        public PresenterSaveWindow(IViewSaveWindow viewSaveWindow)
        {
            _viewSaveWindow = viewSaveWindow;
        }

        public void SaveProject()
        {
            
        }
    }
}
