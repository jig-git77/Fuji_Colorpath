using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterOperationSelection
    {
        private readonly IViewOperationSelection _viewOperationSelection;

        public PresenterOperationSelection(IViewOperationSelection viewOperationSelection)
        {
            _viewOperationSelection = viewOperationSelection;
        }

        public void GetDeviceById()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewOperationSelection.ErrorOccured;
            _viewOperationSelection.Device = deviceBusiness.GetDevice(_viewOperationSelection.DeviceId, _viewOperationSelection.CurrentUserId).FirstOrDefault();
        }
    }
}
