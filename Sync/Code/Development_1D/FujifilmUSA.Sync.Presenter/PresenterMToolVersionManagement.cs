using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;


namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMToolVersionManagement
    {
        private readonly IViewMToolVersionManagement _viewMToolVersionManagement;
        //private readonly IViewAddBranchManagement _viewAddBranchManagement;

        public PresenterMToolVersionManagement(IViewMToolVersionManagement viewMToolVersionManagement)
        {
            _viewMToolVersionManagement = viewMToolVersionManagement;
        }
        public void GetMToolVersions()
        {
            var mToolVersionBusiness = BusinessFactory<IMToolVersionListBusiness>.CreateObject();
            mToolVersionBusiness.ErrorOccured += _viewMToolVersionManagement.ErrorOccured;
            _viewMToolVersionManagement.MToolVersionList = mToolVersionBusiness.GetMToolUpdateVersionList();
        }
        public void InsertMToolUpdateVersion()
        {
            var mToolVersionBusiness = BusinessFactory<IMToolVersionListBusiness>.CreateObject();
            mToolVersionBusiness.ErrorOccured += _viewMToolVersionManagement.ErrorOccured;
            mToolVersionBusiness.InsertMToolUpdateVersion(_viewMToolVersionManagement.MToolVersion);
        }

        public void UpdateMToolUpdateVersion()
        {
            var mToolVersionBusiness = BusinessFactory<IMToolVersionListBusiness>.CreateObject();
            mToolVersionBusiness.ErrorOccured += _viewMToolVersionManagement.ErrorOccured;
            mToolVersionBusiness.UpdateMToolUpdateVersion(_viewMToolVersionManagement.MToolVersion);
        }
        public void GetMToolVersionsSelect()
        {
            var mToolVersionBusiness = BusinessFactory<IMToolVersionListBusiness>.CreateObject();
            mToolVersionBusiness.ErrorOccured += _viewMToolVersionManagement.ErrorOccured;
            _viewMToolVersionManagement.MToolVersion = mToolVersionBusiness.GetMToolVersionSelect(_viewMToolVersionManagement.MToolVersion.OSPlatformDetailsID);
        }
        public void GetMToolOSPlatform()
        {
            var mToolVersionBusiness = BusinessFactory<IMToolVersionListBusiness>.CreateObject();
            mToolVersionBusiness.ErrorOccured += _viewMToolVersionManagement.ErrorOccured;
            _viewMToolVersionManagement.OSPlatformNameList = mToolVersionBusiness.GetMToolOSPlatformList();
        }
        public bool DeleteMToolVersion()
        {
            var mToolVersionBusiness = BusinessFactory<IMToolVersionListBusiness>.CreateObject();
            mToolVersionBusiness.ErrorOccured += _viewMToolVersionManagement.ErrorOccured;
            return mToolVersionBusiness.DeleteMToolVersion(_viewMToolVersionManagement.MToolVersion.OSPlatformDetailsID);
        }
    }
}
