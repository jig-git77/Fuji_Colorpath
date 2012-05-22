using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Linq;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterLicenseManagement 
    {
        private readonly IViewLicenseManagement _viewLicenseManagement;
        private readonly IViewAddLicenseManagement _viewAddLicenseManagement;
        private readonly IViewAddLicenseTypeManagement _viewAddLicenseTypeManagement;

        public PresenterLicenseManagement(IViewLicenseManagement viewLicenseManagement)
        {
            _viewLicenseManagement = viewLicenseManagement;
        }
        public PresenterLicenseManagement(IViewAddLicenseManagement viewAddLicenseManagement)
        {
            _viewAddLicenseManagement = viewAddLicenseManagement;
        }
        public PresenterLicenseManagement(IViewAddLicenseTypeManagement viewAddLicenseTypeManagement)
        {
            _viewAddLicenseTypeManagement = viewAddLicenseTypeManagement;
        }

        public void GetLicense()
        {
            var licenseBusiness = BusinessFactory<ILicenseBusiness>.CreateObject();
            licenseBusiness.ErrorOccured += _viewLicenseManagement.ErrorOccured;
            _viewLicenseManagement.LicenseList = licenseBusiness.GetLicense(0, _viewLicenseManagement.CurrentUserId);
        }

        public void GetLicens()
        {
            var licenseBusiness = BusinessFactory<ILicenseBusiness>.CreateObject();
            licenseBusiness.ErrorOccured += _viewAddLicenseManagement.ErrorOccured;
            _viewAddLicenseManagement.License = licenseBusiness.GetLicense(_viewAddLicenseManagement.License.LicenseId, _viewAddLicenseManagement.CurrentUserId).FirstOrDefault();
        }

        public void InsertLicense()
        {
            var licenseBusiness = BusinessFactory<ILicenseBusiness>.CreateObject();
            licenseBusiness.ErrorOccured += _viewAddLicenseManagement.ErrorOccured;
            licenseBusiness.InsertLicense(_viewAddLicenseManagement.License);
        }
        public void InsertLicenseType()
        {
            var licenseBusiness = BusinessFactory<ILicenseBusiness>.CreateObject();
            licenseBusiness.ErrorOccured += _viewAddLicenseTypeManagement.ErrorOccured;
            licenseBusiness.InsertLicenseType(_viewAddLicenseTypeManagement.LicenseType);
        }

        public void UpdateLicense()
        {
            var licenseBusiness = BusinessFactory<ILicenseBusiness>.CreateObject();
            licenseBusiness.ErrorOccured += _viewAddLicenseManagement.ErrorOccured;
            licenseBusiness.UpdateLicense(_viewAddLicenseManagement.License);
        }

        public void DeleteLicense()
        {
            var licenseBusiness = BusinessFactory<ILicenseBusiness>.CreateObject();
            licenseBusiness.ErrorOccured += _viewLicenseManagement.ErrorOccured;
            licenseBusiness.DeleteLicense(_viewLicenseManagement.License.LicenseId);
        }

        public void GetCompanies()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddLicenseManagement.ErrorOccured;
            _viewAddLicenseManagement.Compnaies = miscDataBusiness.GetCompanyList(_viewAddLicenseManagement.CurrentUserId);
        }
        public void GetBranches()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddLicenseManagement.ErrorOccured;
            _viewAddLicenseManagement.Branches = miscDataBusiness.GetBranchList(_viewAddLicenseManagement.CompanyId, _viewAddLicenseManagement.CurrentUserId);
        }
    }
}