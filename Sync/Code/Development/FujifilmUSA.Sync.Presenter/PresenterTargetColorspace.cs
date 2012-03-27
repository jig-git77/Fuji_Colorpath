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

        public void GetSourceProfile()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.SourceProfile = projectBusiness.GetSourceProfileList();
        }

    }
}
