using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMeasurement
    {
        private readonly IViewMeasurement _viewMeasurement;

        public PresenterMeasurement(IViewMeasurement viewMeasurement)
        {
            _viewMeasurement = viewMeasurement;
        }

        public void GetMeasurements()
        {
            var task = (ISyncRunBusiness)new BusinessFactory<ISyncRunBusiness>().CreateObject();
            _viewMeasurement.Measurements = task.GetMeasurement(_viewMeasurement.SyncRunId);
        }
    }
}
