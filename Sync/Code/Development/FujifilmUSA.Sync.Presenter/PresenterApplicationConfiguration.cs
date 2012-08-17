using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterApplicationConfiguration
    {
        private readonly IViewApplicationConfiguration _viewApplicationConfiguration;

        public PresenterApplicationConfiguration(IViewApplicationConfiguration viewApplicationConfiguration)
        {
            this._viewApplicationConfiguration = viewApplicationConfiguration;
        }

        public void FillConfiguration()
        {
            //_viewApplicationConfiguration.IsSingleUserLoginEnabled = Business.ApplicationConfiguration.Instance.;
        }
    }
}
