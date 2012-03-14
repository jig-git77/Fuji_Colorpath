using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterConfirmSave
    {
        private readonly IViewConfirmSave _viewConfirmSave;
        public PresenterConfirmSave(IViewConfirmSave viewConfirmSave)
        {
            _viewConfirmSave = viewConfirmSave;
        }
        public void OptimizeDlp()
        {
            var task = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            var measurementIds = task.GetMeasurements(_viewConfirmSave.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
            //TODO: remove comment
            //task.OptimizeDeviceLinkProfile(_viewConfirmSave.ProjectId, _viewConfirmSave.OptimizeRunId, measurementIds);
        }
    }
}