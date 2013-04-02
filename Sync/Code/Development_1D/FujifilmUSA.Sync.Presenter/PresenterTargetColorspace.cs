using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterTargetColorspace
    {
        private readonly IViewTargetColorspace _viewTargetColorspace;

        public PresenterTargetColorspace(IViewTargetColorspace viewTargetColorspace)
        {
            _viewTargetColorspace = viewTargetColorspace;
        }

        public void GetPrintStandardTypeList()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.PrintStandardTypeList = projectBusiness.GetPrintStandardTypeList(_viewTargetColorspace.CurrentUserId, _viewTargetColorspace.IsSync1DType);
        }

    }
}
