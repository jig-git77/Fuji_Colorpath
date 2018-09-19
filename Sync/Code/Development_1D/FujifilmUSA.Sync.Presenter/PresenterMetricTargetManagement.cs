using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMetricTargetManagement
    {
        private readonly IViewMetricTargetManagement _viewMetricTargetManagement;
        private readonly IViewAddMetricTarget _viewMetricTarget;

        public PresenterMetricTargetManagement(IViewMetricTargetManagement viewMetricTargetManagement)
        {
            _viewMetricTargetManagement = viewMetricTargetManagement;
        }
        public PresenterMetricTargetManagement(IViewAddMetricTarget viewMetricTarget)
        {
            _viewMetricTarget = viewMetricTarget;
        }

        public void GetMetricTargetPresets()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewMetricTargetManagement.ErrorOccured;
            _viewMetricTargetManagement.MetricTargetPresetList = metricTargetPresetBusiness.GetMetricTargetPreset(0, _viewMetricTargetManagement.CurrentUserId);
        }

        public void GetMetricTargetPresetList()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewMetricTarget.ErrorOccured;
            _viewMetricTarget.MetricTargetPresetList = metricTargetPresetBusiness.GetMetricTargetPreset(0, _viewMetricTarget.CurrentUserId);
        }

        public void GetMetricTargetSumList()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewMetricTargetManagement.ErrorOccured;
            _viewMetricTargetManagement.MetricSumTolerancesList = metricTargetPresetBusiness.GetMetricTargetSumList(_viewMetricTargetManagement.MetricTargetPresetID, _viewMetricTargetManagement.CurrentUserId);
        }

        public void GetMetricTargetPreset()
        {
            var metricTargetBusiness = BusinessFactory<IMetricTargetBusiness>.CreateObject();
            metricTargetBusiness.ErrorOccured += _viewMetricTarget.ErrorOccured;
            _viewMetricTarget.MetricTargetPreset = metricTargetBusiness.GetMetricTargetPreset(_viewMetricTarget.MetricTargetPreset.MetricTargetPresetID, _viewMetricTarget.CurrentUserId);
        }

        public void UpdateMetricTarget()
        {
            var metricTargetBusiness = BusinessFactory<IMetricTargetBusiness>.CreateObject();
            metricTargetBusiness.ErrorOccured += _viewMetricTarget.ErrorOccured;
            metricTargetBusiness.UpdateMetricTargetPreset(_viewMetricTarget.MetricTargetPreset.MetricTargetPresetID, _viewMetricTarget.MetricSumList);
        }

        public void GetMetricSums()
        {
            var metricTargetBusiness = BusinessFactory<IMetricTargetBusiness>.CreateObject();
            metricTargetBusiness.ErrorOccured += _viewMetricTarget.ErrorOccured;
            _viewMetricTarget.MetricSums = metricTargetBusiness.GetAllMetricSumList();
        }

        public void SaveMetricSum()
        {
            var metricTargetBusiness = BusinessFactory<IMetricTargetBusiness>.CreateObject();
            metricTargetBusiness.ErrorOccured += _viewMetricTarget.ErrorOccured;
            metricTargetBusiness.SaveMetricSum(_viewMetricTarget.MetricSumName);
        }

        public void GetMetricTargetPresetReferenceCharts()
        {
            var metricTargetBusiness = BusinessFactory<IMetricTargetBusiness>.CreateObject();
            metricTargetBusiness.ErrorOccured += _viewMetricTarget.ErrorOccured;
            _viewMetricTarget.dtPresetRefChart = metricTargetBusiness.GetMetricTargetPresetReferenceCharts(_viewMetricTarget.MetricTargetPreset.MetricTargetPresetID, _viewMetricTarget.CurrentUserId);
        }
    }
}
