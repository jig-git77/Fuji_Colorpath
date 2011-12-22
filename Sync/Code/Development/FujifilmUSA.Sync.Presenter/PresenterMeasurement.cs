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

        public void GetMeasurementsbyProfileType()
        {
            var task = (IProfileBusiness)new BusinessFactory<IProfileBusiness>().CreateObject();
            _viewMeasurement.Measurements = task.GetMeasurement(_viewMeasurement.ProfileId,_viewMeasurement.TypeId);
        }

        public void DeleteMeasurement()
        {
            var task = (ISyncRunBusiness)new BusinessFactory<ISyncRunBusiness>().CreateObject();
            _viewMeasurement.MeasurementDeleted = task.DeleteMeasurement(_viewMeasurement.MeasurementId);
        }
    }
}
