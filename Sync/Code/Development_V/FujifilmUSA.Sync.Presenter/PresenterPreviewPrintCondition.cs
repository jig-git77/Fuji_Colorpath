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
            _viewPreviewPrintCondition = viewPreviewPrintCondition;
        }

        public void GetPreviewPrintCondition()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewPreviewPrintCondition.ErrorOccured;
            _viewPreviewPrintCondition.Data = projectBusiness.GetPrintConditionPreviewData(
                _viewPreviewPrintCondition.MeasurementDeviceId.Value,
                _viewPreviewPrintCondition.IlluminantId,
                _viewPreviewPrintCondition.ObserverTypeId,
                _viewPreviewPrintCondition.ReferenceChartTypeId,
                _viewPreviewPrintCondition.DeviceFamilyProfilerOptionId,
                _viewPreviewPrintCondition.V_MeasurementDeviceId.Value,
                _viewPreviewPrintCondition.Opt_ReferenceChartTypeId,
                _viewPreviewPrintCondition.V_ReferenceChartTypeId);
        }
    }
}
