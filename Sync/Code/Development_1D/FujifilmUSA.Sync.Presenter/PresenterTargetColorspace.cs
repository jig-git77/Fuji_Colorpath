using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterTargetColorspace
    {
        private readonly IViewTargetColorspace _viewTargetColorspace;

        public PresenterTargetColorspace(IViewTargetColorspace viewTargetColorspace)
        {
            _viewTargetColorspace = viewTargetColorspace;
        }

        public void GetPrintStandardTypeList()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.PrintStandardTypeList = projectBusiness.GetPrintStandardTypeList(_viewTargetColorspace.CurrentUserId, _viewTargetColorspace.IsSync1DType, _viewTargetColorspace.TargetPrintCategory);
        }
        public void GetTargetLABList()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.ProjectTargetLABList = projectBusiness.GetProjectTargetLAB(_viewTargetColorspace.PrintstandardTypeId, _viewTargetColorspace.ProjectTypeId);
        }
        public void GetSpotTargetList()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.SpotTargetList = projectBusiness.GetSpotTargetList(_viewTargetColorspace.CurrentUserId, _viewTargetColorspace.IsSpotTargetType);
        }

        public void GetSpotReTargetProjectList()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.ReTargetSpotProjectList = projectBusiness.GetReTargetSpotProjectList(_viewTargetColorspace.CompanyID, _viewTargetColorspace.BranchID, _viewTargetColorspace.DeviceID);
        }

        public void GetBranchByCompany()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.Branches = miscDataBusiness.GetBranchList(_viewTargetColorspace.CompanyID, _viewTargetColorspace.CurrentUserId);
        }

        public void DeviceListForRetargetSpotProject()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.Devices = deviceBusiness.GetDeviceListForRetargetSpotProject(_viewTargetColorspace.CompanyID, _viewTargetColorspace.BranchID,
                                                                          _viewTargetColorspace.CurrentUserId);
        }

        public void GetSpotReTargetProjectDetails()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewTargetColorspace.ErrorOccured;
            _viewTargetColorspace.RetargetSpotProject = projectBusiness.GetRetargetSpotProjectDetails(_viewTargetColorspace.ReTargetID);
        }
    }
}
