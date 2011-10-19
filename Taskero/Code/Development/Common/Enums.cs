#region System
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Fuji.Common
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
            ,DeviceReport = 2
            ,ShiftVariabilityReport = 3
            ,DeviceVariabilityReport = 4
            ,ColorDetailReport = 5
            ,PatchDetailReport = 6
            ,ProductivityReport = 7
            ,ReportPerformanceSummary = 8
            ,ReportTargetMeasurement = 9
            ,ReportJobRunCertificationProcess = 10
            ,ReportJobRunCertificationNonProcess = 11
            ,ReportPressMakeReady = 12
            ,ReportSingleMeasurement = 13
            ,CPSPrintStandardsReport = 14
            ,CPSPrintStandardsReportISO = 15
        }

        public enum enumDeviceType
        { 
            None = 0, Color = 1, Plate = 2, Monitor = 3, WideFormat = 4, Press = 5
        }

        public enum CPSyncPressRunStatus
        {
            Open = 1,
            Closed = 2,
            Certify = 3,
            NotUsed = 4
        }

        public enum DongleType
        {
            CPV = 1,
            ITX = 2,
            Sync = 3,
            EFI = 4,
            CTW =5,
            ColorGATE = 6
        }

        public enum CPSyncPatchType
        {
            Cyan = 1,
            Magenta = 2,
            Yellow = 3,
            Black = 4,
            Overprint_Red = 5,
            Overprint_Green = 6,
            Overprint_Blue = 7,
            Paper = 8,
            Triplet_Solid_Gray = 9,
            Gray_C_M_Y = 10,
            Other = 11,
            Cyan_Tint = 12,
            Magenta_Tint = 13,
            Yellow_Tint = 14,
            Black_Tint = 15,
            Cyan_Quarter_Tone = 16,
            Cyan_Half_Tone = 17,
            Cyan_Three_Quarter_Tone = 18,
            Magenta_Quarter_Tone = 19,
            Magenta_Half_Tone = 20,
            Magenta_Three_Quarter_Tone = 21,
            Yellow_Quarter_Tone = 22,
            Yellow_Half_Tone = 23,
            Yellow_Three_Quarter_Tone = 24,
            Black_Quarter_Tone = 25,
            Black_Half_Tone = 26,
            Black_Three_Quarter_Tone = 27,
            Gray_25 = 28,
            Gray_50 = 29,
            Gray_75 = 30
        }

        public enum CPSyncColorChar
        {
            Density = 1,
            DeltaE = 2,
            DeltaE_2000 = 3,
            DeltaF = 4,
            L = 5,
            a = 6,
            b = 7,
            DotGain_TVI = 9,
            Gray_L_Target_G7 = 10,
            Gray_a_Target_G7 = 11,
            Gray_b_Target_G7 = 12,
            G7_NPDC = 13,
            DeltaH = 14,
            DotGain_TVI_Spread = 15
        }

        public enum ProjectType
        {
            G7 = 1,
            ISO = 2,
           
        }

        public enum OutputType
        {
            Offset = 1,
            Screen = 2,
            Wideformat = 3
        }    
       
        
        public enum PrintStandardType
        {
            GRACoL = 1,
            SWOP_5 = 2,
            SWOP_3 = 3,
            PSO_12647_2 = 4
        }

        //public enum SyncMeasurementDeviceType
        //{
        //    i1 = 1,
        //    iSis = 3
        //}
    }
}
