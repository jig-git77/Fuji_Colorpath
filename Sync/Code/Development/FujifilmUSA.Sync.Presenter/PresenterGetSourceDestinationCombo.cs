using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterGetSourceDestinationCombo
    {
        private readonly IViewGetSourceDestinationCombo _viewGetSourceDestinationCombo;
        public PresenterGetSourceDestinationCombo(IViewGetSourceDestinationCombo viewGetSourceDestinationCombo)
        {
            this._viewGetSourceDestinationCombo = viewGetSourceDestinationCombo;
        }

        public void GetSourceProfileList()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            task.ErrorOccured += _viewGetSourceDestinationCombo.ErrorOccured;
            _viewGetSourceDestinationCombo.GetSourceProfiles = task.GetSourceProfileList();
        }

        public void GetDestinationProfiles()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            task.ErrorOccured += _viewGetSourceDestinationCombo.ErrorOccured;  
            _viewGetSourceDestinationCombo.GetDestinationProfiles = task.GetDestinationProfiles(_viewGetSourceDestinationCombo.SyncDeviceId);
        }
    }
}
