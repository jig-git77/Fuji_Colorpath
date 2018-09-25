using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using System.Data;
using System;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDeviceFamilyProfilerOption
    {
        private readonly IViewDeviceFamilyProfilerOption _viewDeviceFamilyProfilerOption;

        public PresenterDeviceFamilyProfilerOption(IViewDeviceFamilyProfilerOption viewDeviceFamilyProfilerOption)
        {
            _viewDeviceFamilyProfilerOption = viewDeviceFamilyProfilerOption;
        }

        public void GetDeviceFamilyProfilerOption()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.ProfileOption = projectBusiness.GetDeviceFamilyProfilerOptionPreset(_viewDeviceFamilyProfilerOption.DeviceFamilyId,_viewDeviceFamilyProfilerOption.LicenseTypeId);
        }

        public void GetMetricSumTargetValues()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.MetricSumTargetValues = projectBusiness.GetMetricSumTargetValues(_viewDeviceFamilyProfilerOption.MetricTargetPresetId);
        }

        public void GetPrintStandardTVI()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.PrintStandardTVI = projectBusiness.GetPrintStandardTVI(_viewDeviceFamilyProfilerOption.PrintStandardTypeId);
        }

        public void GetPrintStandardDensity()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.ProjectTargetDensity = projectBusiness.GetPrintStandardDensity(_viewDeviceFamilyProfilerOption.PrintStandardTypeId);
        }

        public void GetPrintStandardHueAngle()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.ProjectTargetHueAngle = projectBusiness.GetPrintStandardHueAngle(_viewDeviceFamilyProfilerOption.PrintStandardTypeId);
        }

        public void DeviceListById()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            _viewDeviceFamilyProfilerOption.Devices = deviceBusiness.GetDeviceListById(_viewDeviceFamilyProfilerOption.CompanyID, _viewDeviceFamilyProfilerOption.BranchID,
                                                                          _viewDeviceFamilyProfilerOption.CurrentUserId);
        }

        public void GetReTargetProjectDetails()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewDeviceFamilyProfilerOption.ErrorOccured;
            DataTable dtDetails = projectBusiness.GetRetargetProjectDetails(_viewDeviceFamilyProfilerOption.RetargetProjectID);

            if (dtDetails.Rows.Count > 0)
            {
                _viewDeviceFamilyProfilerOption.RetargetPrintStandardTypeName = Convert.ToString(dtDetails.Rows[0]["PrintStandard"]);
                _viewDeviceFamilyProfilerOption.RetargetMetricTargetPresetName = Convert.ToString(dtDetails.Rows[0]["MetricTargetPresetName"]);
            }
        }

    }
}
