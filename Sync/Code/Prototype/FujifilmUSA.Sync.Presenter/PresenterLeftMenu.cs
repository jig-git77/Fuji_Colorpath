using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

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
            task.ErrorOccured += _viewLeftMenu.ErrorOccured;  
            task.CreateSyncRun(_viewLeftMenu.Project.ProjectId, Convert.ToInt32(_viewLeftMenu.Project.ProjectStatus), _viewLeftMenu.CreatedDateTime);
            _viewLeftMenu.Project = task.GetProject(_viewLeftMenu.Project.ProjectId);
        }
    }
}
