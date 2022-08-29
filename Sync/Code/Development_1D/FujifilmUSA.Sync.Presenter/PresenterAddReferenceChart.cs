using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using System.Linq;
using System.Data;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAddReferenceChart
    {
        private readonly IViewAddReferenceChart _viewAddReferenceChart;
        private readonly IViewReferenceChartManagement _viewReferenceChartManagement;

        public PresenterAddReferenceChart(IViewAddReferenceChart viewAddReferenceChart)
        {
            _viewAddReferenceChart = viewAddReferenceChart;
        }

        public PresenterAddReferenceChart(IViewReferenceChartManagement viewReferenceChartManagement)
        {
            _viewReferenceChartManagement = viewReferenceChartManagement;
        }

        public void GetDeviceFamilyList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddReferenceChart.ErrorOccured;
            _viewAddReferenceChart.DeviceFamilyList = miscDataBusiness.GetDeviceFamilyList();
        }

        public void GetProjectTypeList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddReferenceChart.ErrorOccured;
            _viewAddReferenceChart.ProjectTypeList = miscDataBusiness.GetProjectTypeList();
        }

        public void GetInstrumentTypeList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddReferenceChart.ErrorOccured;
            _viewAddReferenceChart.InstrumentTypeList = miscDataBusiness.GetInstrumentTypeList();
        }

        public void SaveReferenceChart()
        {
            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
            referenceChartBusiness.ErrorOccured += _viewAddReferenceChart.ErrorOccured;
            referenceChartBusiness.CreateReferenceChart(_viewAddReferenceChart.ReferenceChartType);
        }

        public void GetReferenceCharts()
        {
            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
            referenceChartBusiness.ErrorOccured += _viewReferenceChartManagement.ErrorOccured;
            _viewReferenceChartManagement.ReferenceCharts = referenceChartBusiness.GetReferenceCharts(0);
        }

        public void GetReferenceChartDetails()
        {
            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
            referenceChartBusiness.ErrorOccured += _viewAddReferenceChart.ErrorOccured;
            _viewAddReferenceChart.ReferenceChartType = referenceChartBusiness.GetReferenceCharts(_viewAddReferenceChart.ReferenceChartType.ReferenceChartTypeId).FirstOrDefault();
        }

        public void UpdateReferenceChart()
        {
            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
            referenceChartBusiness.ErrorOccured += _viewAddReferenceChart.ErrorOccured;
            referenceChartBusiness.UpdateReferenceChart(_viewAddReferenceChart.ReferenceChartType);
        }
        public DataSet GetMultiColorChannelList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddReferenceChart.ErrorOccured;
            return miscDataBusiness.GetMultiColorChannelList();
        }
    }
}
