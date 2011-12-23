using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterLeftMenu
    {
        private readonly IViewLeftMenu _viewLeftMenu;

        public PresenterLeftMenu(IViewLeftMenu viewLeftMenu)
        {
            _viewLeftMenu = viewLeftMenu;
        }

        public void AddProjectSyncRun()
        {
            var task = (IProjectBussiness)new BusinessFactory<IProjectBussiness>().CreateObject();
            task.CreateSyncRun(_viewLeftMenu.ProjectId, Convert.ToInt32(_viewLeftMenu.Status), _viewLeftMenu.CreatedDateTime);

            _viewLeftMenu.CurrentProject = task.GetProject(_viewLeftMenu.ProjectId);
        }
    }
}
