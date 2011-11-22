using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDestinationProfile
    {
        private readonly IViewAddProject _ViewAddProject;
        public PresenterDestinationProfile(IViewAddProject ViewAddProject)
        {
            this._ViewAddProject = ViewAddProject;
        }

        public void GetDestinationProfileList()
        {
            IDestinationProfileBusiness task = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            _ViewAddProject.GetDestinationProfileList = task.GetDestinationProfileList();
        }
    }
}
