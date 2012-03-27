using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterLicenseManagement 
    {
        private readonly IViewLicenseManagement _viewLicenseManagement;
        private readonly IViewAddLicenseManagement _viewAddLicenseManagement;
        private readonly IViewAddLicenseTypeManagement _viewAddLicenseTypeManagement;

        public PresenterLicenseManagement(IViewLicenseManagement viewLicenseManagement)
        {
            this._viewLicenseManagement = viewLicenseManagement;
        }
        public PresenterLicenseManagement(IViewAddLicenseManagement viewAddLicenseManagement)
        {
            this._viewAddLicenseManagement = viewAddLicenseManagement;
        }
        public PresenterLicenseManagement(IViewAddLicenseTypeManagement viewAddLicenseTypeManagement)
        {
            this._viewAddLicenseTypeManagement = viewAddLicenseTypeManagement;
        }


        public void GetLicense()
        {
            var task = BusinessFactory<ILicenseBusiness>.CreateObject();
            _viewLicenseManagement.LicenseList = task.GetLicense();
        }

        public void InsertLicense()
        {
            var task = BusinessFactory<ILicenseBusiness>.CreateObject();
            task.InsertLicense(_viewAddLicenseManagement.License);
        }
        public void InsertLicenseType()
        {
            var task = BusinessFactory<ILicenseBusiness>.CreateObject();
            task.InsertLicenseType(_viewAddLicenseTypeManagement.LicenseType);
        }

        public void UpdateLicense()
        {
            var task = BusinessFactory<ILicenseBusiness>.CreateObject();
            task.UpdateLicense(_viewAddLicenseManagement.License);
        }

        public void DeleteLicense()
        {
            var task = BusinessFactory<ILicenseBusiness>.CreateObject();
            task.DeleteLicense(_viewLicenseManagement.License.LicenseId);
        }

        public void GetCompanies()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddLicenseManagement.Compnaies = task.CompanyList();
        }
        public void GetBranches()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddLicenseManagement.Branches = task.BranchList(_viewAddLicenseManagement.CompanyId);
        }
        public void GetLicenseType()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddLicenseManagement.LicenseTypes = task.LicenseTypeList();
        }

    }
}
