using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterCompanyManagement 
    {
        private readonly IViewCompanyManagement _viewCompanyManagement;
        private readonly IViewAddCompanyManagement _viewAddCompanyManagement;

        public PresenterCompanyManagement(IViewCompanyManagement viewCompanyManagement)
        {
            this._viewCompanyManagement = viewCompanyManagement;
        }
        public PresenterCompanyManagement(IViewAddCompanyManagement viewAddCompanyManagement)
        {
            this._viewAddCompanyManagement = viewAddCompanyManagement;
        }

        public void GetCompanies()
        {
            var task = BusinessFactory<ICompanyBusiness>.CreateObject();
            _viewCompanyManagement.CompanyList = task.GetCompanies();
        }

        public void InsertCompany()
        {
            var task = BusinessFactory<ICompanyBusiness>.CreateObject();
            task.InsertCompany(_viewAddCompanyManagement.Company);
        }

        public void UpdateCompnay()
        {
            var task = BusinessFactory<ICompanyBusiness>.CreateObject();
            task.UpdateCompnay(_viewAddCompanyManagement.Company);
        }

        public void DeleteCompany()
        {
            var task = BusinessFactory<ICompanyBusiness>.CreateObject();
            task.DeleteCompany(_viewCompanyManagement.Company.CompanyId);
        }

        public void GetRegions()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddCompanyManagement.RegionList = task.RegionList();
        }

    }
}
