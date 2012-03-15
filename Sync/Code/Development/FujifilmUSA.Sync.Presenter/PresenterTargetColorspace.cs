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
            this._viewTargetColorspace = viewTargetColorspace;
        }

        public void GetSourceProfile()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewTargetColorspace.SourceProfile = task.GetSourceProfileList();
        }

    }
}
