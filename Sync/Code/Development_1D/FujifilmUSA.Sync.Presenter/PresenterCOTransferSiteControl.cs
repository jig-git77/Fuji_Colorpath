using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterCOTransferSiteControl
    {
        private readonly IViewCOTransferSiteControl _viewCOTransferSiteControl;

        public PresenterCOTransferSiteControl(IViewCOTransferSiteControl viewCoTransferSiteControl)
        {
            _viewCOTransferSiteControl = viewCoTransferSiteControl;
        }

        //public void GetSilverlightPostUrl()
        //{
        //    _viewCOTransferSiteControl.SilverlightPostUrl = ApplicationConfiguration.Instance.SilverlightPostUrl;
        //}
    }
}
