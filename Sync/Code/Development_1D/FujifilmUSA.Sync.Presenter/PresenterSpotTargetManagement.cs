using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSpotTargetManagement
    {
        private readonly IViewSpotTargetManagement _viewSpotTargetManagement;
        private readonly IViewAddSpotTarget _viewAddSpotTarget;

        public PresenterSpotTargetManagement(IViewSpotTargetManagement viewSpotManagement)
        {
            _viewSpotTargetManagement = viewSpotManagement;
        }

        public PresenterSpotTargetManagement(IViewAddSpotTarget viewAddSpotTarget)
        {
            _viewAddSpotTarget = viewAddSpotTarget;
        }
        public void GetSpotTargets()
        {
            var spotBusiness = BusinessFactory<ISpotTargetBusiness>.CreateObject();
            spotBusiness.ErrorOccured += _viewSpotTargetManagement.ErrorOccured;
            _viewSpotTargetManagement.SpotTargetList = spotBusiness.GetSpotTargetList(0, _viewSpotTargetManagement.CurrentUserId);
        }
        public void GetCompanies()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddSpotTarget.ErrorOccured;
            _viewAddSpotTarget.Compnaies = miscDataBusiness.GetCompanyList(_viewAddSpotTarget.CurrentUserId);
        }
    }
}
