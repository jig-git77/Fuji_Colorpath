using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterOpenPrintCondition
    {
        private readonly IViewOpenPrintCondition _viewOpenPrintCondition;

        public PresenterOpenPrintCondition(IViewOpenPrintCondition viewOpenPrintCondition)
        {
            _viewOpenPrintCondition = viewOpenPrintCondition;
        }

        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewOpenPrintCondition.ErrorOccured;
            _viewOpenPrintCondition.Project = projectBusiness.GetProjectDetail(_viewOpenPrintCondition.ProjectId);
        }
    }
}
