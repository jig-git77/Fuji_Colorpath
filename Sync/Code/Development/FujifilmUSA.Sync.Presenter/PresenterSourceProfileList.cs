using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSourceProfileList 
    {
        private readonly IViewSourceProfiles _viewSourceProfiles;

        public PresenterSourceProfileList(IViewSourceProfiles viewSourceProfiles)
        {
            this._viewSourceProfiles = viewSourceProfiles;
        }
        public void GetSourceProfileList()
        {
            var task = (ISourceProfileBusiness)new BusinessFactory<ISourceProfileBusiness>().CreateObject();
            _viewSourceProfiles.GetSourceProfiles = task.GetSourceProfiles();
        }
    }
}
