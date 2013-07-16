using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterApplicationConfiguration
    {
        private readonly IViewApplicationConfiguration _viewApplicationConfiguration;       

        public PresenterApplicationConfiguration(IViewApplicationConfiguration viewApplicationConfiguration)
        {
            this._viewApplicationConfiguration = viewApplicationConfiguration;
        }

        public void FillConfiguration()
        {
            _viewApplicationConfiguration.IsSingleUserLoginEnabled = Business.ApplicationConfiguration.Instance.EnableSingleUserLogin;
            _viewApplicationConfiguration.KeepUserAliveInSeconds = Business.ApplicationConfiguration.Instance.KeepUserAliveInSeconds;
        }

        public void GetApplicationConfiguration()
        {
            var applicationConfigurationBusiness = BusinessFactory<IApplicationConfigurationBusiness>.CreateObject();
            _viewApplicationConfiguration.ApplicationConfigurationList = applicationConfigurationBusiness.GetApplicationConfiguration();
        }

        public void UpdateApplicationConfiguration()
        { 
            var applicationConfigurationBusiness = BusinessFactory<IApplicationConfigurationBusiness>.CreateObject();
            applicationConfigurationBusiness.ErrorOccured += _viewApplicationConfiguration.ErrorOccured;
            int returnValue = applicationConfigurationBusiness.UpdateApplicationConfiguration(_viewApplicationConfiguration.ApplicationConfigurationList);
            if (returnValue != -1)
            { 
                //clear instance of application configuration
                ApplicationConfiguration.ClearInstance();
            }
        }
    }
}
