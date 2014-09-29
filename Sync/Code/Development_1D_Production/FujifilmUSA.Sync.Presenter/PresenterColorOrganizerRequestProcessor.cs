using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterColorOrganizerRequestProcessor
    {
        private readonly IViewColorOrganizerRequestProcessor _viewColorOrganizerRequestProcessor;

        public PresenterColorOrganizerRequestProcessor(IViewColorOrganizerRequestProcessor viewColorOrganizerRequestProcessor)
        {
            _viewColorOrganizerRequestProcessor = viewColorOrganizerRequestProcessor;
        }

        public void GetLoginInformation()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewColorOrganizerRequestProcessor.ErrorOccured;
            _viewColorOrganizerRequestProcessor.CurrentUser =
                userBusiness.UserLogin(
                    _viewColorOrganizerRequestProcessor.UserName,
                    _viewColorOrganizerRequestProcessor.Password,
                    UserType.XmfColorOrganizerUser);
        }

        public void GetDeviceById()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewColorOrganizerRequestProcessor.ErrorOccured;
            _viewColorOrganizerRequestProcessor.Device =
                deviceBusiness.GetDevice(_viewColorOrganizerRequestProcessor.DeviceId, _viewColorOrganizerRequestProcessor.CurrentUserId).FirstOrDefault();
        }

        public void GetProjectCountByDeviceId()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewColorOrganizerRequestProcessor.ErrorOccured;
            _viewColorOrganizerRequestProcessor.TotalProjects = projectBusiness.GetProjectsByDeviceId(_viewColorOrganizerRequestProcessor.DeviceId,
                                                                                                      _viewColorOrganizerRequestProcessor.LicenseTypeId).Count;
        }

        public void GetTempProjectInfo()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewColorOrganizerRequestProcessor.ErrorOccured;
            _viewColorOrganizerRequestProcessor.TempProjectInfo = projectBusiness.GetTempProjectInfo(_viewColorOrganizerRequestProcessor.TempProjectId);
        }

        public void GetTempValidationInfo()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewColorOrganizerRequestProcessor.ErrorOccured;
            _viewColorOrganizerRequestProcessor.TempValidationInfo = projectBusiness.GetTempValidationInfo(_viewColorOrganizerRequestProcessor.TempValidationInfoId);
        }

        public void GetProjectDetail()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewColorOrganizerRequestProcessor.ErrorOccured;
            _viewColorOrganizerRequestProcessor.Project = projectBusiness.GetProjectDetail(_viewColorOrganizerRequestProcessor.ProjectId);
        }

        public void GetProjectDetailWithVerification()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewColorOrganizerRequestProcessor.ErrorOccured;
            _viewColorOrganizerRequestProcessor.Project = projectBusiness.GetProjectDetail(_viewColorOrganizerRequestProcessor.ProjectId, true);
        }
    }
}
