using System.Linq;
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
            _viewCompanyManagement = viewCompanyManagement;
        }
        public PresenterCompanyManagement(IViewAddCompanyManagement viewAddCompanyManagement)
        {
            _viewAddCompanyManagement = viewAddCompanyManagement;
        }

        public void GetCompany()
        {
            var companyBusiness = BusinessFactory<ICompanyBusiness>.CreateObject();
            companyBusiness.ErrorOccured += _viewAddCompanyManagement.ErrorOccured;
            _viewAddCompanyManagement.Company = companyBusiness.GetCompany(_viewAddCompanyManagement.Company.CompanyId).FirstOrDefault();
        }

        public void GetCompanies()
        {
            var companyBusiness = BusinessFactory<ICompanyBusiness>.CreateObject();
            companyBusiness.ErrorOccured += _viewCompanyManagement.ErrorOccured;
            _viewCompanyManagement.CompanyList = companyBusiness.GetCompany(0);
        }

        public void InsertCompany()
        {
            var companyBusiness = BusinessFactory<ICompanyBusiness>.CreateObject();
            companyBusiness.ErrorOccured += _viewAddCompanyManagement.ErrorOccured;
            companyBusiness.InsertCompany(_viewAddCompanyManagement.Company);
        }

        public void UpdateCompany()
        {
            var companyBusiness = BusinessFactory<ICompanyBusiness>.CreateObject();
            companyBusiness.ErrorOccured += _viewAddCompanyManagement.ErrorOccured;
            companyBusiness.UpdateCompany(_viewAddCompanyManagement.Company);
        }

        public void DeleteCompany()
        {
            var companyBusiness = BusinessFactory<ICompanyBusiness>.CreateObject();
            companyBusiness.ErrorOccured += _viewCompanyManagement.ErrorOccured;
            companyBusiness.DeleteCompany(_viewCompanyManagement.Company.CompanyId);
        }

        public void GetRegions()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddCompanyManagement.ErrorOccured;
            _viewAddCompanyManagement.RegionList = miscDataBusiness.GetRegionList();
        }

    }
}
