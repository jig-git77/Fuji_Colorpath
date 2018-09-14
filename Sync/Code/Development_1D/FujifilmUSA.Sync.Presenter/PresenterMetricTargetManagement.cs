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
        private readonly IViewMetricTargetPresetManagement _viewMetricTargetPresetManagement;

        public PresenterMetricTargetManagement(IViewMetricTargetManagement viewMetricTargetManagement)
        {
            _viewMetricTargetManagement = viewMetricTargetManagement;
        }
        public PresenterMetricTargetManagement(IViewMetricTargetPresetManagement viewMetricTargetPresetManagement)
        {
            _viewMetricTargetPresetManagement = viewMetricTargetPresetManagement;
        }

        public void GetMetricTargetPresets()
        {
            var metricTargetPresetBusiness = BusinessFactory<IMetricTargetPresetBusiness>.CreateObject();
            metricTargetPresetBusiness.ErrorOccured += _viewMetricTargetPresetManagement.ErrorOccured;
            _viewMetricTargetPresetManagement.MetricTargetPresetList = metricTargetPresetBusiness.GetMetricTargetPreset(0, _viewMetricTargetPresetManagement.CurrentUserId);
        }

        //public void GetMetricTargetPresets()
        //{
        //    var metricTargetPresetBusiness = BusinessFactory<IMetricTargetBusiness>.CreateObject();
        //    metricTargetPresetBusiness.ErrorOccured += _viewMetricTargetManagement.ErrorOccured;
        //    _viewMetricTargetManagement.MetricTargetPresetList = metricTargetPresetBusiness.GetMetricTargetList(_viewMetricTargetManagement.CurrentUserId);
        //}
    }
}
