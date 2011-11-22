using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterProject
    {
        private readonly IViewProjectList _ViewProjectList;

        public PresenterProject(IViewProjectList ViewProjectList)
        {
            _ViewProjectList = ViewProjectList;
        }

        public void GetProjectListBySyncDeviceID()
        {
            IProjectBussiness task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            _ViewProjectList.GetProjectListBySyncDeviceID = task.GetProjectListBySyncDeviceID(_ViewProjectList.SyncDeviceID);
        }
    }
}
