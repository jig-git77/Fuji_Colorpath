using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSourceProfile
    {
        private readonly IViewAddProject _ViewAddProject;
        public PresenterSourceProfile(IViewAddProject ViewAddProject)
        {
            this._ViewAddProject = ViewAddProject;
        }

        public void GetSourceProfileList()
        {
            ISourceProfileBusiness task = (ISourceProfileBusiness)new BusinessFactory<ISourceProfileBusiness>().CreateObject();
            _ViewAddProject.GetSourceProfileList = task.GetSourceProfileList();
        }
    }
}
