using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterBrowserTest
    {
        private readonly IViewBrowserTest _viewProfilerService;

        public PresenterBrowserTest(IViewBrowserTest viewProfilerService)
        {
            _viewProfilerService = viewProfilerService;
        }

        public void Ping()
        {
            var miscBusiness = BusinessFactory<IMiscBusiness>.CreateObject();
            miscBusiness.ErrorOccured += _viewProfilerService.ErrorOccured;
            _viewProfilerService.PingResponse = miscBusiness.Ping();
        }
    }
}
