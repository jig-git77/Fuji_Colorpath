#region System
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Oris.Common
{
    public class Enums
    {
        public enum LanguageType
        {
            English, Japanese, German, Chinese, Hindi, Spanish
        }

        public enum PermissionType
        {
            None,
            AddBranch, AddCompany, AddDevice, AddDongle, AddSupportUser, AddTempRHDevice, AddUser,
            DeleteBranch, DeleteCompany, DeleteDevice, DeleteDongle, DeletePrintTarget, DeleteTempRHDevice, DeleteUser, DiscardCPVMeasurement,
            RemoveSupportUser, ResetDeviceStatus,
            UpdateBranch, UpdateDevice, UpdateDongle, UpdateTempRHDevice, UpdateUser,
            ViewCPVMeasurements, ViewDataMonitor, ViewDeviceStatus, ViewErrorLog, ViewPressData, ViewReports
        }

        public enum RoleType
        {
            None,
            BranchAdmin, CompanyAdmin, CountryAdmin, PrintBuyer, SiteUser, SupportUser, TaskeroAdmin, CompanyAdminWithBrand
        }

        public enum enumYesNo
        {
            No = 0, Yes = 1
        }

        //====  Status  ====
        public enum enumStatus
        {
            Failed = 0, Caution = 1, Passed = 2
        }
        public enum enumJobStatus
        {
            MakeReady = 1,
            Production = 2,
            Complete = 3
        }

        public enum enumDeviceStatus
        {
            None = 0, Green = 1, Yellow = 2, Orange = 3, Red = 4
        }

        // ================= For Taskero Reports ================= \\
        public enum enumMesurmentType
        {
            ColorValue = 1,
            DensityDeviation = 2,
            InkCoverage = 3
        }
        public enum enumReportType
        {
            BranchSummaryReport = 1
            , DeviceReport = 2
            , ShiftVariabilityReport = 3
            , DeviceVariabilityReport = 4
            , ColorDetailReport = 5
            , PatchDetailReport = 6
            , ProductivityReport = 7
            , ReportPerformanceSummary = 8
            , ReportTargetMeasurement = 9
            , ReportJobRunCertificationProcess = 10
            , ReportJobRunCertificationNonProcess = 11
            , ReportPressMakeReady = 12
            , ReportSingleMeasurement = 13
        }

        public enum enumDeviceType
        {
            None = 0, Color = 1, Plate = 2, Monitor = 3, WideFormat = 4, Press = 5
        }


    }
}
