using System.Linq;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using System;
using FujifilmUSA.Sync.Entity;

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
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.OptimizeDeviceLinkProfile(_viewConfirmSave.ProjectId, _viewConfirmSave.OptimizeRunId, _viewConfirmSave.MeasurementIds,
                                                          _viewConfirmSave.ProfileDescription, Convert.ToInt32(EOptimizeRunStatus.Closed), DateTime.Now);
        }

        public void GetMeasurementsOnRunId()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.MeasurementIds = measurementBusiness.GetMeasurementsOnRunId(_viewConfirmSave.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
        }

        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.Project = projectBusiness.GetProjectDetail(_viewConfirmSave.ProjectId);
        }

        public void CreateOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.CreateOptimizeRun(_viewConfirmSave.ProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), DateTime.Now);
        }

        public void CertifyOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.CertifyOptimizeRun(_viewConfirmSave.OptimizeRunId, _viewConfirmSave.ProjectId, _viewConfirmSave.MasterRunId, Convert.ToInt32(EOptimizeRunStatus.Certify), DateTime.Now);
        }

        public void Certify1DOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.Certify1DOptimizeRun(_viewConfirmSave.OptimizeRunId, _viewConfirmSave.ProjectId, _viewConfirmSave.MasterRunId, Convert.ToInt32(EOptimizeRunStatus.Certify), DateTime.Now);
        }

        public void GetLastRunStatus()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.LastRunStatus = optimizeRunBusiness.GetLastRunStatus(_viewConfirmSave.OptimizeRunId);
        }

        public void Get1DLastRunStatus()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.LastRunStatus = optimizeRunBusiness.Get1DLastRunStatus(_viewConfirmSave.OptimizeRunId);
        }

        public void Create1DOptimizeRun()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.Create1DOptimizeRun(_viewConfirmSave.ProjectId, Convert.ToInt32(EOptimizeRunStatus.Open), DateTime.Now, 0);
        }

        public void Get1DMeasurementsOnRunId()
        {
            var measurementBusiness = BusinessFactory<IMeasurementBusiness>.CreateObject();
            measurementBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            _viewConfirmSave.MeasurementIds = measurementBusiness.Get1DMeasurementsOnRunId(_viewConfirmSave.OptimizeRunId).Where(x => x.IsUsed).Select(x => x.MeasurementId).ToList();
        }

        public void CreateG7AlignmentCurve()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.CreateG7AlignmentCurve(
                _viewConfirmSave.ProjectId,
                _viewConfirmSave.OptimizeRunId,
                _viewConfirmSave.MeasurementIds,
                true,                
                Convert.ToInt32(EOptimizeRunStatus.Closed),
                DateTime.Now,
                _viewConfirmSave.Is1DMeasurementSmoothing);

        }

        public void CreateISOAlignmentCurve()
        {
            var optimizeRunBusiness = BusinessFactory<IOptimizeRunBusiness>.CreateObject();
            optimizeRunBusiness.ErrorOccured += _viewConfirmSave.ErrorOccured;
            optimizeRunBusiness.CreateISOAlignmentCurve(
                _viewConfirmSave.ProjectId,
                 _viewConfirmSave.OptimizeRunId,
                _viewConfirmSave.MeasurementIds,
                true,                
                Convert.ToInt32(EOptimizeRunStatus.Closed),
                DateTime.Now,
                _viewConfirmSave.Is1DMeasurementSmoothing);

        }
    }
}