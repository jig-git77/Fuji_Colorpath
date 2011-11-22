using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterCompany
    {
        
        private readonly IViewSyncDevice _ViewSyncDevice;

        public PresenterCompany(IViewSyncDevice ViewSyncDevice)
        {
            this._ViewSyncDevice = ViewSyncDevice;
        }
        public void GetCompanyList()
        {
            ICompanyBusiness task = (ICompanyBusiness)new BusinessFactory<ICompanyBusiness>().CreateObject();
            _ViewSyncDevice.GetCompanyList = task.GetCompanyList();
        }
    }
}
