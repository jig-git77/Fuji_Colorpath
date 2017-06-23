using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterProfileManagement
    {
        private readonly IViewProfileManagement _viewProfileManagement;

        public PresenterProfileManagement(IViewProfileManagement viewProfileManagement)
        {
            _viewProfileManagement = viewProfileManagement;
        }

        public void GetSecurityQuestionList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewProfileManagement.ErrorOccured;
            _viewProfileManagement.SecurityQuestions = miscDataBusiness.GetSecurityQuestions();
        }

        public void UpdatePassword()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewProfileManagement.ErrorOccured;
            userBusiness.UpdatePassword(_viewProfileManagement.LoginId, _viewProfileManagement.Password);
        }

        public void UpdateSecurityInformation()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewProfileManagement.ErrorOccured;
            userBusiness.UpdateSecurityInformation(_viewProfileManagement.LoginId, _viewProfileManagement.SecurityQuestionId, _viewProfileManagement.SecurityAnswer);
        }

        public void UpdateLanguage()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewProfileManagement.ErrorOccured;
            userBusiness.UpdateLanguage(_viewProfileManagement.LoginId, _viewProfileManagement.LanguageCultureCode);
        }

        public void GetLanguages()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewProfileManagement.ErrorOccured;
            _viewProfileManagement.LanguageList = userBusiness.GetLanguages();
        }

        public void UpdateUserDeviceInfo()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewProfileManagement.ErrorOccured;
            userBusiness.UpdateUserDeviceInfo(_viewProfileManagement.LoginId, _viewProfileManagement.MeasurementDeviceID, _viewProfileManagement.DensityType, _viewProfileManagement.UVMode);
        }

        public void GetMeasurementDevice()
        {
            var userBusiness = BusinessFactory<IUserBusiness>.CreateObject();
            userBusiness.ErrorOccured += _viewProfileManagement.ErrorOccured;
            _viewProfileManagement.MeasurementDevices = userBusiness.GetMeasurementDeviceList(_viewProfileManagement.LoginId, 0);
        }
    }
}
