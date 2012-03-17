using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterPreviewPrintCondition
    {
        private readonly IViewPreviewPrintCondition _viewPreviewPrintCondition;

        public PresenterPreviewPrintCondition(IViewPreviewPrintCondition viewPreviewPrintCondition)
        {
            this._viewPreviewPrintCondition = viewPreviewPrintCondition;
        }

        public void GetPreviewPrintCondition()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewPreviewPrintCondition.Data = task.GetPrintConditionPreviewData(
                                _viewPreviewPrintCondition.MeasurementDeviceId.Value,
                                _viewPreviewPrintCondition.IlluminantID,
                                _viewPreviewPrintCondition.ObserverTypeID,
                                _viewPreviewPrintCondition.ReferenceChartTypeId,
                                _viewPreviewPrintCondition.DeviceFamilyProfilerOptionID);
        }
    }
}
