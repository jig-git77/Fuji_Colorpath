using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Entity;
using System;
using System.Linq;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterLeftMenuProjectItem
    {
        private readonly IViewLeftMenuProjectItem _viewLeftMenuProjectItem;
        public PresenterLeftMenuProjectItem(IViewLeftMenuProjectItem viewLeftMenuProjectItem)
        {
            _viewLeftMenuProjectItem = viewLeftMenuProjectItem;
        }

        public void CreateOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRun.CreateOptimizeRun(
                _viewLeftMenuProjectItem.ProjectId,
                Convert.ToInt32(EOptimizeRunStatus.Open),               
                DateTime.Now);
        }
        public void DeleteOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRun.DeleteOptimizeRun(_viewLeftMenuProjectItem.OptimizeRunID, _viewLeftMenuProjectItem.ProjectId, _viewLeftMenuProjectItem.MasterRunID, _viewLeftMenuProjectItem.RunStatus);
        }
        public void Delete1DOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRun.Delete1DOptimizeRun(_viewLeftMenuProjectItem.OptimizeRunID, _viewLeftMenuProjectItem.ProjectId, _viewLeftMenuProjectItem.MasterRunID, _viewLeftMenuProjectItem.RunStatus);
        }
        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            _viewLeftMenuProjectItem.Project = projectBusiness.GetProjectDetail(_viewLeftMenuProjectItem.ProjectId);
        }
        public void DeleteOptimizeRunDLP()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRun.DeleteOptimizeRunDLP(_viewLeftMenuProjectItem.OptimizeRunID, _viewLeftMenuProjectItem.ProjectId);
        }
        public void Delete1DOptimizeRunDLP()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRun.Delete1DOptimizeRunDLP(_viewLeftMenuProjectItem.OptimizeRunID, _viewLeftMenuProjectItem.ProjectId);
        }
        public void Create1DOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRun.Create1DOptimizeRun(
                _viewLeftMenuProjectItem.ProjectId,
                Convert.ToInt32(EOptimizeRunStatus.Open),
                DateTime.Now,
                1);
        }
        public void DeleteSpotOptimizeRun()
        {
            var optimizeRun = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRun.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRun.DeleteSpotOptimizeRun(_viewLeftMenuProjectItem.OptimizeRunID, _viewLeftMenuProjectItem.SpotProjectId, _viewLeftMenuProjectItem.MasterRunID, _viewLeftMenuProjectItem.RunStatus);
        }
        public void GetProjectDetail_SpotProject()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            _viewLeftMenuProjectItem.Project = projectBusiness.GetProjectDetail_SpotProject(_viewLeftMenuProjectItem.SpotProjectId);
        }
        public void CreateOptimizeRun_SpotProject()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewLeftMenuProjectItem.ErrorOccured;
            optimizeRunBusiness.CreateSpotProjectOptimizeRun(_viewLeftMenuProjectItem.SpotProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), DateTime.Now);
        }
    }
}
