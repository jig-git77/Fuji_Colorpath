using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Linq;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMetricTargetPresetManagement
    {
        private readonly IViewAddMetricTargetPreset _viewAddMetricTargetPreset;
        private readonly IViewMetricTargetPresetManagement _viewMetricTargetPresetManagement;

        public PresenterMetricTargetPresetManagement(IViewAddMetricTargetPreset viewAddMetricTargetPreset)
        {
            _viewAddMetricTargetPreset = viewAddMetricTargetPreset;
        }

        public PresenterMetricTargetPresetManagement(IViewMetricTargetPresetManagement viewMetricTargetPresetManagement)
        {
            _viewMetricTargetPresetManagement = viewMetricTargetPresetManagement;
        }

        public void GetCompanies()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.Companies = miscDataBusiness.GetCompanyList(_viewAddMetricTargetPreset.CurrentUserId);
        }

        public void RegionList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.RegionList = miscDataBusiness.GetRegionList(_viewAddMetricTargetPreset.CurrentUserId);
        }
        public void GetCompaniesByRegion()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.Companies = miscDataBusiness.GetCompaniesByRegion(_viewAddMetricTargetPreset.RegionId, _viewAddMetricTargetPreset.CurrentUserId);
        }

        public void InsertMetricTargetPreset()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            metricTargetPresetBusiness.InsertMetricTargetPreset(_viewAddMetricTargetPreset.MetricTargetPreset);
        }

        public void UpdateMetricTargetPreset()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            metricTargetPresetBusiness.UpdateMetricTargetPreset(_viewAddMetricTargetPreset.MetricTargetPreset);
        }

        public void DeleteMetricTargetPreset()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewMetricTargetPresetManagement.ErrorOccured;
            metricTargetPresetBusiness.DeleteMetricTargetPreset(_viewMetricTargetPresetManagement.MetricTargetPreset.MetricTargetPresetID);
        }

        public void GetMetricTargetPresets()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewMetricTargetPresetManagement.ErrorOccured;
            _viewMetricTargetPresetManagement.MetricTargetPresetList = metricTargetPresetBusiness.GetMetricTargetPreset(0, _viewMetricTargetPresetManagement.CurrentUserId);
        }

        public void GetMetricTargetPreset()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.MetricTargetPreset = metricTargetPresetBusiness.GetMetricTargetPreset(_viewAddMetricTargetPreset.MetricTargetPreset.MetricTargetPresetID, _viewAddMetricTargetPreset.CurrentUserId).FirstOrDefault();
        }

        //-----Functions for Patch Tolerance level-------//
        public void GetReferenceChartType()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.ReferenceChartType = metricTargetPresetBusiness.GetReferenceChartTypeList(0, 0, 0, 0, _viewAddMetricTargetPreset.InstrumentId, false, 0, 0);
        }

        public void InstrumentTypeList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.InstrumentTypeList = miscDataBusiness.GetInstrumentTypeList();
        }

        public void GetMetricSumList()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.MetricSumList = metricTargetPresetBusiness.GetMetricSumList();
        }

        public void GetMetricTargetPatchPresets()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewMetricTargetPresetManagement.ErrorOccured;
            _viewMetricTargetPresetManagement.MetricTargetPresetList = metricTargetPresetBusiness.GetMetricTargetPatchPreset(0, _viewMetricTargetPresetManagement.CurrentUserId);
        }

        public void GetMetricTargetPatchPreset()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.MetricTargetPreset = metricTargetPresetBusiness.GetMetricTargetPatchPreset(_viewAddMetricTargetPreset.MetricTargetPreset.MetricTargetPresetID, _viewAddMetricTargetPreset.CurrentUserId).FirstOrDefault();
        }

        public void BindChartPatchList()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            _viewAddMetricTargetPreset.ChartPatchList = metricTargetPresetBusiness.BindChartPatchList(_viewAddMetricTargetPreset.ReferenceChartTypeId);
        }

        public void InsertMetricTargetPatchPreset()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            metricTargetPresetBusiness.InsertMetricTargetPatchPreset(_viewAddMetricTargetPreset.MetricTargetPreset);
        }

        public void UpdateMetricTargetPatchPreset()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewAddMetricTargetPreset.ErrorOccured;
            metricTargetPresetBusiness.UpdateMetricTargetPatchPreset(_viewAddMetricTargetPreset.MetricTargetPreset);
        }
    }
}
