#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
#endregion

namespace Fuji.Common
{
    public class Sessions
    {
        #region Constructor
        public Sessions()
        { }
        #endregion

        #region Properties
        public static int UserID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserID"] == null) { System.Web.HttpContext.Current.Session["UserID"] = 0; }
                return (Int32)System.Web.HttpContext.Current.Session["UserID"];
            }
            set
            { System.Web.HttpContext.Current.Session["UserID"] = value; }
        }
        public static string Username
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Username"] == null) { System.Web.HttpContext.Current.Session["Username"] = ""; }
                return System.Web.HttpContext.Current.Session["Username"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["Username"] = value; }
        }
        public static string Email
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Email"] == null) { System.Web.HttpContext.Current.Session["Email"] = ""; }
                return System.Web.HttpContext.Current.Session["Email"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["Email"] = value; }
        }
        public static string FirstName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["FirstName"] == null) { System.Web.HttpContext.Current.Session["FirstName"] = ""; }
                return System.Web.HttpContext.Current.Session["FirstName"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["FirstName"] = value; }
        }
        public static string LastName
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["LastName"] == null) { System.Web.HttpContext.Current.Session["LastName"] = ""; }
                return System.Web.HttpContext.Current.Session["LastName"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["LastName"] = value; }
        }
        public static bool IsLoggedIn
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["IsLoggedIn"] == null) { System.Web.HttpContext.Current.Session["IsLoggedIn"] = false; }
                return (bool)System.Web.HttpContext.Current.Session["IsLoggedIn"];
            }
            set
            { System.Web.HttpContext.Current.Session["IsLoggedIn"] = value; }
        }
        public static int CompanyID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["CompanyID"] == null) { System.Web.HttpContext.Current.Session["CompanyID"] = 0; }
                return (Int32)System.Web.HttpContext.Current.Session["CompanyID"];
            }
            set
            { System.Web.HttpContext.Current.Session["CompanyID"] = value; }
        }
        public static int BrandID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["BrandID"] == null) { System.Web.HttpContext.Current.Session["BrandID"] = 0; }
                return (Int32)System.Web.HttpContext.Current.Session["BrandID"];
            }
            set
            { System.Web.HttpContext.Current.Session["BrandID"] = value; }
        }
        public static string CompanyIDs
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["CompanyIDs"] == null) { System.Web.HttpContext.Current.Session["CompanyIDs"] = ""; }
                return System.Web.HttpContext.Current.Session["CompanyIDs"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["CompanyIDs"] = value; }
        }
        public static string BranchIDs
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["BranchIDs"] == null) { System.Web.HttpContext.Current.Session["BranchIDs"] = ""; }
                return System.Web.HttpContext.Current.Session["BranchIDs"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["BranchIDs"] = value; }
        }
        public static string DongleIDs
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["DongleIDs"] == null) { System.Web.HttpContext.Current.Session["DongleIDs"] = ""; }
                return System.Web.HttpContext.Current.Session["DongleIDs"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["DongleIDs"] = value; }
        }
        public static string ReportIDs
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ReportIDs"] == null) { System.Web.HttpContext.Current.Session["ReportIDs"] = ""; }
                return System.Web.HttpContext.Current.Session["ReportIDs"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["ReportIDs"] = value; }
        }
        public static string Css
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Css"] == null ) { System.Web.HttpContext.Current.Session["Css"] = ""; }
                return System.Web.HttpContext.Current.Session["Css"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["Css"] = value; }
        }
        public static string HeaderLogo
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["HeaderLogo"] == null) { System.Web.HttpContext.Current.Session["HeaderLogo"] = "logo.gif"; }
                return System.Web.HttpContext.Current.Session["HeaderLogo"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["HeaderLogo"] = value; }
        }
        public static string LargeImage
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["LargeImage"] == null) { System.Web.HttpContext.Current.Session["LargeImage"] = "taskero-uni-logo-big.gif"; }
                return System.Web.HttpContext.Current.Session["LargeImage"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["LargeImage"] = value; }
        }

        public static bool SiteLicenseAgreed
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["SiteLicenseAgreed"] == null) { System.Web.HttpContext.Current.Session["SiteLicenseAgreed"] = false; }
                return (bool)System.Web.HttpContext.Current.Session["SiteLicenseAgreed"];
            }
            set
            { System.Web.HttpContext.Current.Session["SiteLicenseAgreed"] = value; }
        }

        #endregion

        #region Roles
        public static int RoleID
        {
            get
            {
                //========== 0 IS NONE ==========/
                if (System.Web.HttpContext.Current.Session["RoleID"] == null) { System.Web.HttpContext.Current.Session["RoleID"] = 0; }
                return (Int32)System.Web.HttpContext.Current.Session["RoleID"];
            }
            set
            { System.Web.HttpContext.Current.Session["RoleID"] = value; }
        }
        public static int RoleLevel
        {
            get
            {
                int RT = 0;

                switch (RoleID)
                {
                    case 1:
                        RT = 1;
                        break;
                    case 2:
                        RT = 2;
                        break;
                    case 3:
                        RT = 3;
                        break;
                    case 4:
                        RT = 4;
                        break;
                    case 5:
                        RT = 5;
                        break;
                    case 6:
                        RT = 6;
                        break;
                    case 7:
                        RT = 7;
                        break;
                    case 8:
                        RT = 3;
                        break;
                    default:
                        RT = 0;
                        break;
                }

                return RT;
            }
            set { }
        }
        public static Enums.RoleType RoleDesc
        {
            get
            {
                Enums.RoleType RT = Enums.RoleType.None;

                switch (RoleID)
                {
                    case 1:
                        RT = Enums.RoleType.TaskeroAdmin;
                        break;
                    case 2:
                        RT = Enums.RoleType.CountryAdmin;
                        break;
                    case 3:
                        RT = Enums.RoleType.CompanyAdmin;
                        break;
                    case 4:
                        RT = Enums.RoleType.BranchAdmin;
                        break;
                    case 5:
                        RT = Enums.RoleType.PrintBuyer;
                        break;
                    case 6:
                        RT = Enums.RoleType.SupportUser;
                        break;
                    case 7:
                        RT = Enums.RoleType.SiteUser;
                        break;
                    case 8:
                        RT = Enums.RoleType.CompanyAdminWithBrand;
                        break;
                    default:
                        RT = Enums.RoleType.None;
                        break;
                }

                return RT;
            }
            set { }
        }
        #endregion

        #region Permissions
        public static int PermissionID
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["PermissionID"] == null) { System.Web.HttpContext.Current.Session["PermissionID"] = 0; }
                return (Int32)System.Web.HttpContext.Current.Session["PermissionID"];
            }
            set
            { System.Web.HttpContext.Current.Session["PermissionID"] = value; }
        }
        public static Enums.PermissionType PermissionDesc
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["PermissionDesc"] == null) { System.Web.HttpContext.Current.Session["PermissionDesc"] = Enums.PermissionType.None; }
                return (Enums.PermissionType)System.Web.HttpContext.Current.Session["PermissionDesc"];
            }
            set
            { System.Web.HttpContext.Current.Session["PermissionDesc"] = value; }
        }
        public static bool View_Jobs_Tab { get { if (System.Web.HttpContext.Current.Session["_View_Jobs_Tab"] == null) { System.Web.HttpContext.Current.Session["_View_Jobs_Tab"] = false; } return (bool)System.Web.HttpContext.Current.Session["_View_Jobs_Tab"]; } set { System.Web.HttpContext.Current.Session["_View_Jobs_Tab"] = value; } }
        public static bool Discard_CPV_Measurement { get { if (System.Web.HttpContext.Current.Session["_Discard_CPV_Measurement"] == null) { System.Web.HttpContext.Current.Session["_Discard_CPV_Measurement"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Discard_CPV_Measurement"]; } set { System.Web.HttpContext.Current.Session["_Discard_CPV_Measurement"] = value; } }
        public static bool View_Device_Status { get { if (System.Web.HttpContext.Current.Session["_View_Device_Status"] == null) { System.Web.HttpContext.Current.Session["_View_Device_Status"] = false; } return (bool)System.Web.HttpContext.Current.Session["_View_Device_Status"]; } set { System.Web.HttpContext.Current.Session["_View_Device_Status"] = value; } }
        public static bool Reset_Device_Status { get { if (System.Web.HttpContext.Current.Session["_Reset_Device_Status"] == null) { System.Web.HttpContext.Current.Session["_Reset_Device_Status"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Reset_Device_Status"]; } set { System.Web.HttpContext.Current.Session["_Reset_Device_Status"] = value; } }
        public static bool View_Reports { get { if (System.Web.HttpContext.Current.Session["_View_Reports"] == null) { System.Web.HttpContext.Current.Session["_View_Reports"] = false; } return (bool)System.Web.HttpContext.Current.Session["_View_Reports"]; } set { System.Web.HttpContext.Current.Session["_View_Reports"] = value; } }
        public static bool Add_Dongle { get { if (System.Web.HttpContext.Current.Session["_Add_Dongle"] == null) { System.Web.HttpContext.Current.Session["_Add_Dongle"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Add_Dongle"]; } set { System.Web.HttpContext.Current.Session["_Add_Dongle"] = value; } }
        public static bool Update_Dongle { get { if (System.Web.HttpContext.Current.Session["_Update_Dongle"] == null) { System.Web.HttpContext.Current.Session["_Update_Dongle"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Update_Dongle"]; } set { System.Web.HttpContext.Current.Session["_Update_Dongle"] = value; } }
        public static bool Delete_Dongle { get { if (System.Web.HttpContext.Current.Session["_Delete_Dongle"] == null) { System.Web.HttpContext.Current.Session["_Delete_Dongle"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Delete_Dongle"]; } set { System.Web.HttpContext.Current.Session["_Delete_Dongle"] = value; } }
        public static bool Add_Company { get { if (System.Web.HttpContext.Current.Session["_Add_Company"] == null) { System.Web.HttpContext.Current.Session["_Add_Company"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Add_Company"]; } set { System.Web.HttpContext.Current.Session["_Add_Company"] = value; } }
        public static bool Delete_Company { get { if (System.Web.HttpContext.Current.Session["_Delete_Company"] == null) { System.Web.HttpContext.Current.Session["_Delete_Company"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Delete_Company"]; } set { System.Web.HttpContext.Current.Session["_Delete_Company"] = value; } }
        public static bool Add_Branch { get { if (System.Web.HttpContext.Current.Session["_Add_Branch"] == null) { System.Web.HttpContext.Current.Session["_Add_Branch"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Add_Branch"]; } set { System.Web.HttpContext.Current.Session["_Add_Branch"] = value; } }
        public static bool Update_Branch { get { if (System.Web.HttpContext.Current.Session["_Update_Branch"] == null) { System.Web.HttpContext.Current.Session["_Update_Branch"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Update_Branch"]; } set { System.Web.HttpContext.Current.Session["_Update_Branch"] = value; } }
        public static bool Delete_Branch { get { if (System.Web.HttpContext.Current.Session["_Delete_Branch"] == null) { System.Web.HttpContext.Current.Session["_Delete_Branch"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Delete_Branch"]; } set { System.Web.HttpContext.Current.Session["_Delete_Branch"] = value; } }
        public static bool Add_Device { get { if (System.Web.HttpContext.Current.Session["_Add_Device"] == null) { System.Web.HttpContext.Current.Session["_Add_Device"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Add_Device"]; } set { System.Web.HttpContext.Current.Session["_Add_Device"] = value; } }
        public static bool Update_Device { get { if (System.Web.HttpContext.Current.Session["_Update_Device"] == null) { System.Web.HttpContext.Current.Session["_Update_Device"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Update_Device"]; } set { System.Web.HttpContext.Current.Session["_Update_Device"] = value; } }
        public static bool Delete_Device { get { if (System.Web.HttpContext.Current.Session["_Delete_Device"] == null) { System.Web.HttpContext.Current.Session["_Delete_Device"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Delete_Device"]; } set { System.Web.HttpContext.Current.Session["_Delete_Device"] = value; } }
        public static bool Add_Temp_RH_Device { get { if (System.Web.HttpContext.Current.Session["_Add_Temp_RH_Device"] == null) { System.Web.HttpContext.Current.Session["_Add_Temp_RH_Device"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Add_Temp_RH_Device"]; } set { System.Web.HttpContext.Current.Session["_Add_Temp_RH_Device"] = value; } }
        public static bool Update_Temp_RH_Device { get { if (System.Web.HttpContext.Current.Session["_Update_Temp_RH_Device"] == null) { System.Web.HttpContext.Current.Session["_Update_Temp_RH_Device"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Update_Temp_RH_Device"]; } set { System.Web.HttpContext.Current.Session["_Update_Temp_RH_Device"] = value; } }
        public static bool Delete_Temp_RH_Device { get { if (System.Web.HttpContext.Current.Session["_Delete_Temp_RH_Device"] == null) { System.Web.HttpContext.Current.Session["_Delete_Temp_RH_Device"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Delete_Temp_RH_Device"]; } set { System.Web.HttpContext.Current.Session["_Delete_Temp_RH_Device"] = value; } }
        public static bool Add_User { get { if (System.Web.HttpContext.Current.Session["_Add_User"] == null) { System.Web.HttpContext.Current.Session["_Add_User"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Add_User"]; } set { System.Web.HttpContext.Current.Session["_Add_User"] = value; } }
        public static bool Update_User { get { if (System.Web.HttpContext.Current.Session["_Update_User"] == null) { System.Web.HttpContext.Current.Session["_Update_User"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Update_User"]; } set { System.Web.HttpContext.Current.Session["_Update_User"] = value; } }
        public static bool Delete_User { get { if (System.Web.HttpContext.Current.Session["_Delete_User"] == null) { System.Web.HttpContext.Current.Session["_Delete_User"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Delete_User"]; } set { System.Web.HttpContext.Current.Session["_Delete_User"] = value; } }
        public static bool Delete_Print_Target { get { if (System.Web.HttpContext.Current.Session["_Delete_Print_Target"] == null) { System.Web.HttpContext.Current.Session["_Delete_Print_Target"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Delete_Print_Target"]; } set { System.Web.HttpContext.Current.Session["_Delete_Print_Target"] = value; } }
        public static bool View_Error_Log { get { if (System.Web.HttpContext.Current.Session["_View_Error_Log"] == null) { System.Web.HttpContext.Current.Session["_View_Error_Log"] = false; } return (bool)System.Web.HttpContext.Current.Session["_View_Error_Log"]; } set { System.Web.HttpContext.Current.Session["_View_Error_Log"] = value; } }
        public static bool View_Data_Monitor { get { if (System.Web.HttpContext.Current.Session["_View_Data_Monitor"] == null) { System.Web.HttpContext.Current.Session["_View_Data_Monitor"] = false; } return (bool)System.Web.HttpContext.Current.Session["_View_Data_Monitor"]; } set { System.Web.HttpContext.Current.Session["_View_Data_Monitor"] = value; } }
        public static bool Add_Support_User { get { if (System.Web.HttpContext.Current.Session["_Add_Support_User"] == null) { System.Web.HttpContext.Current.Session["_Add_Support_User"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Add_Support_User"]; } set { System.Web.HttpContext.Current.Session["_Add_Support_User"] = value; } }
        public static bool Remove_Support_User { get { if (System.Web.HttpContext.Current.Session["_Remove_Support_User"] == null) { System.Web.HttpContext.Current.Session["_Remove_Support_User"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Remove_Support_User"]; } set { System.Web.HttpContext.Current.Session["_Remove_Support_User"] = value; } }
        public static bool View_Press_Data { get { if (System.Web.HttpContext.Current.Session["_View_Press_Data"] == null) { System.Web.HttpContext.Current.Session["_View_Press_Data"] = false; } return (bool)System.Web.HttpContext.Current.Session["_View_Press_Data"]; } set { System.Web.HttpContext.Current.Session["_View_Press_Data"] = value; } }
        public static bool View_Devices_Tab { get { if (System.Web.HttpContext.Current.Session["_View_Devices_Tab"] == null) { System.Web.HttpContext.Current.Session["_View_Devices_Tab"] = false; } return (bool)System.Web.HttpContext.Current.Session["_View_Devices_Tab"]; } set { System.Web.HttpContext.Current.Session["_View_Devices_Tab"] = value; } }
        public static bool Access_Branding { get { if (System.Web.HttpContext.Current.Session["_Access_Branding"] == null) { System.Web.HttpContext.Current.Session["_Access_Branding"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Access_Branding"]; } set { System.Web.HttpContext.Current.Session["_Access_Branding"] = value; } }
        public static bool Access_Video {get { if (System.Web.HttpContext.Current.Session["_Access_Video"] == null) { System.Web.HttpContext.Current.Session["_Access_Video"] = false; } return (bool)System.Web.HttpContext.Current.Session["_Access_Video"]; } set { System.Web.HttpContext.Current.Session["_Access_Video"] = value; } }
        public static bool Access_VideoView{get{if (System.Web.HttpContext.Current.Session["_Access_VideoView"] == null){System.Web.HttpContext.Current.Session["_Access_VideoView"] = false;}return (bool)System.Web.HttpContext.Current.Session["_Access_VideoView"];}set{System.Web.HttpContext.Current.Session["_Access_VideoView"] = value;}}
        public static bool Access_SyncView
        {   get{if (System.Web.HttpContext.Current.Session["_Access_SyncView"] == null){System.Web.HttpContext.Current.Session["_Access_SyncView"] = false;}
                return (bool)System.Web.HttpContext.Current.Session["_Access_SyncView"];}
            set { System.Web.HttpContext.Current.Session["_Access_SyncView"] = value; }
        }
        #endregion

        #region Language
        public static string Language
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Language"] == null) { System.Web.HttpContext.Current.Session["Language"] = "en-US"; }
                return System.Web.HttpContext.Current.Session["Language"].ToString();
            }
            set
            { System.Web.HttpContext.Current.Session["Language"] = value; }
        }
        public static Enums.LanguageType LanguageName
        {
            get
            {
                Enums.LanguageType LT = Enums.LanguageType.English;

                switch (Language)
                {
                    case "en-US":
                        LT = Enums.LanguageType.English;
                        break;
                    case "ja-JP":
                        LT = Enums.LanguageType.Japanese;
                        break;
                    case "de-DE":
                        LT = Enums.LanguageType.German;
                        break;
                    case "zh-HK":
                        LT = Enums.LanguageType.Chinese;
                        break;
                    case "hi-IN":
                        LT = Enums.LanguageType.Hindi;
                        break;
                    case "es-ES":
                        LT = Enums.LanguageType.Spanish;
                        break;
                    default:
                        LT = Enums.LanguageType.English;
                        break;
                }

                return LT;
            }
            set { }
        }
        #endregion

        #region Set Custom Sessions
        public static void SetSessions(string Key, object Value) { System.Web.HttpContext.Current.Session[Key] = Value; }
        public static object GetSession(string Key) { return System.Web.HttpContext.Current.Session[Key]; }
        #endregion

        #region Session Functions
        public static void SetLoginSessions(DataSet DSet)
        {
            #region Is Logged In
            Sessions.IsLoggedIn = true;
            #endregion

            #region Table 1 : Taskero User
            Sessions.UserID = Convert.ToInt32(DSet.Tables[0].Rows[0]["TaskeroUserID"]);
            Sessions.Username = DSet.Tables[0].Rows[0]["LoginID"].ToString();
            Sessions.LastName = DSet.Tables[0].Rows[0]["LastName"].ToString();
            Sessions.FirstName = DSet.Tables[0].Rows[0]["FirstName"].ToString();
            Sessions.Email = DSet.Tables[0].Rows[0]["Email"].ToString();
            if(DSet.Tables[0].Rows[0]["AccessVideoView"] != null && Convert.ToInt32(DSet.Tables[0].Rows[0]["AccessVideoView"]) > 0)
                Sessions.Access_VideoView =  true;
            else
                Sessions.Access_VideoView = false;

            if (Convert.ToBoolean(DSet.Tables[0].Rows[0]["SiteLicenseAgreement"]) == true || Fuji.Common.Sessions.RoleDesc == Fuji.Common.Enums.RoleType.TaskeroAdmin)
                Sessions.SiteLicenseAgreed = true;
            else
                Sessions.SiteLicenseAgreed = false;
            
            #endregion

            #region Table 1 : Role
            Sessions.RoleID = Convert.ToInt32(DSet.Tables[0].Rows[0]["RoleID"]);
            #endregion

            #region Table 2 Or 4 : Permission
            if (Sessions.RoleID == 1) //========== If taskero Admin all rights assigned ==========//
            {
                Sessions.View_Jobs_Tab = true;
                Sessions.Discard_CPV_Measurement = true;
                Sessions.View_Device_Status = true;
                Sessions.Reset_Device_Status = true;
                Sessions.View_Reports = true;
                Sessions.Add_Dongle = true;
                Sessions.Update_Dongle = true;
                Sessions.Delete_Dongle = true;
                Sessions.Add_Company = true;
                Sessions.Delete_Company = true;
                Sessions.Add_Branch = true;
                Sessions.Update_Branch = true;
                Sessions.Delete_Branch = true;
                Sessions.Add_Device = true;
                Sessions.Update_Device = true;
                Sessions.Delete_Device = true;
                Sessions.Add_Temp_RH_Device = true;
                Sessions.Update_Temp_RH_Device = true;
                Sessions.Delete_Temp_RH_Device = true;
                Sessions.Add_User = true;
                Sessions.Update_User = true;
                Sessions.Delete_User = true;
                Sessions.Delete_Print_Target = true;
                Sessions.View_Error_Log = true;
                Sessions.View_Data_Monitor = true;
                Sessions.Add_Support_User = true;
                Sessions.Remove_Support_User = true;
                Sessions.View_Press_Data = true;
                Sessions.View_Devices_Tab = true;
                Sessions.Access_Branding = true;
                Sessions.Access_Video = true;
                Sessions.Access_SyncView = true;
                
            }
            else //========== If not taskero Admin check and assign all rights ==========//
            {
                int TableID = 0;
                if (Sessions.RoleID == 0)
                { TableID = 3; }// Take UserPermission
                else
                { TableID = 2; }// Take RolePermission

                #region Permission Checking
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 1"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.View_Jobs_Tab = true; } else { Sessions.View_Jobs_Tab = false; } //Permission : View Jobs Tab
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 2"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Discard_CPV_Measurement = true; } else { Sessions.Discard_CPV_Measurement = false; } //Permission : Discard CPV Measurement
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 3"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.View_Device_Status = true; } else { Sessions.View_Device_Status = false; } //Permission : View Device Status
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 4"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Reset_Device_Status = true; } else { Sessions.Reset_Device_Status = false; } //Permission : Reset Device Status
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 5"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.View_Reports = true; } else { Sessions.View_Reports = false; } //Permission : View Reports
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 6"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Add_Dongle = true; } else { Sessions.Add_Dongle = false; } //Permission : Add Dongle
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 7"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Update_Dongle = true; } else { Sessions.Update_Dongle = false; } //Permission : Update Dongle
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 8"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Delete_Dongle = true; } else { Sessions.Delete_Dongle = false; } //Permission : Delete Dongle
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 9"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Add_Company = true; } else { Sessions.Add_Company = false; } //Permission : Add Company
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 10"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Delete_Company = true; } else { Sessions.Delete_Company = false; } //Permission : Delete Company
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 11"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Add_Branch = true; } else { Sessions.Add_Branch = false; } //Permission : Add Branch
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 12"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Update_Branch = true; } else { Sessions.Update_Branch = false; } //Permission : Update Branch
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 13"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Delete_Branch = true; } else { Sessions.Delete_Branch = false; } //Permission : Delete Branch
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 14"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Add_Device = true; } else { Sessions.Add_Device = false; } //Permission : Add Device
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 15"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Update_Device = true; } else { Sessions.Update_Device = false; } //Permission : Update Device
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 16"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Delete_Device = true; } else { Sessions.Delete_Device = false; } //Permission : Delete Device
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 17"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Add_Temp_RH_Device = true; } else { Sessions.Add_Temp_RH_Device = false; } //Permission : Add Temp RH Device
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 18"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Update_Temp_RH_Device = true; } else { Sessions.Update_Temp_RH_Device = false; } //Permission : Update Temp RH Device
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 19"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Delete_Temp_RH_Device = true; } else { Sessions.Delete_Temp_RH_Device = false; } //Permission : Delete Temp RH Device
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 20"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Add_User = true; } else { Sessions.Add_User = false; } //Permission : Add User
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 21"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Update_User = true; } else { Sessions.Update_User = false; } //Permission : Update User
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 22"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Delete_User = true; } else { Sessions.Delete_User = false; } //Permission : Delete User
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 23"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Delete_Print_Target = true; } else { Sessions.Delete_Print_Target = false; } //Permission : Delete Print Target
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 24"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.View_Error_Log = true; } else { Sessions.View_Error_Log = false; } //Permission : View Error Log
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 25"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.View_Data_Monitor = true; } else { Sessions.View_Data_Monitor = false; } //Permission : View Data Monitor
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 26"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Add_Support_User = true; } else { Sessions.Add_Support_User = false; } //Permission : Add Support User
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 27"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Remove_Support_User = true; } else { Sessions.Remove_Support_User = false; } //Permission : Remove Support User
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 28"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.View_Press_Data = true; } else { Sessions.View_Press_Data = false; } //Permission : View Press Data
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 29"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.View_Devices_Tab = true; } else { Sessions.View_Devices_Tab = false; } //Permission : View Devices Tab
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 30"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Access_Branding = true; } else { Sessions.Access_Branding = false; } //Permission : Access Branding                
                DSet.Tables[TableID].DefaultView.RowFilter = "PermissionID = 31"; if (DSet.Tables[TableID].DefaultView.Count > 0) { Sessions.Access_Video = true; } else {Sessions.Access_Video = false; } //Permission : Access Video                
                                
                #endregion
            }
            #endregion

            #region Table 4 : Company ID
            if (DSet.Tables[4].Rows.Count > 0)
            { Sessions.CompanyID = Convert.ToInt32(DSet.Tables[4].Rows[0]["CompanyId"]); }
            string strCompanyID = "";
            for (int i = 0; i < DSet.Tables[4].Rows.Count; i = i + 1)
            {
                if (strCompanyID == "")
                { strCompanyID = DSet.Tables[4].Rows[i]["CompanyId"].ToString(); }
                else
                { strCompanyID = strCompanyID + " , " + DSet.Tables[4].Rows[i]["CompanyId"].ToString(); }
            }
            Sessions.CompanyIDs = strCompanyID;
            #endregion

            #region Table 5 : Branch ID
            string strBranchID = "";
            for (int i = 0; i < DSet.Tables[5].Rows.Count; i = i + 1)
            {
                if (strBranchID == "")
                { strBranchID = DSet.Tables[5].Rows[i]["BranchID"].ToString(); }
                else
                { strBranchID = strBranchID + " , " + DSet.Tables[5].Rows[i]["BranchID"].ToString(); }
            }
            Sessions.BranchIDs = strBranchID;
            #endregion

            #region Table 6 : Dongle ID
            string strDongleID = "";
            for (int i = 0; i < DSet.Tables[6].Rows.Count; i = i + 1)
            {
                if (strDongleID == "")
                { strDongleID = DSet.Tables[6].Rows[i]["CPDongleID"].ToString(); }
                else
                { strDongleID = strBranchID + " , " + DSet.Tables[6].Rows[i]["CPDongleID"].ToString(); }
            }
            Sessions.DongleIDs = strDongleID;
            #endregion

            #region Table 7 : CSS AND Image FileName
            if (DSet.Tables[7].Rows.Count > 0)
            {
                Sessions.Css = DSet.Tables[7].Rows[0]["Css"].ToString();
                Sessions.HeaderLogo = DSet.Tables[7].Rows[0]["HeaderLogo"].ToString();
                Sessions.LargeImage = DSet.Tables[7].Rows[0]["LargeImage"].ToString();
            }
            else
            {
                Sessions.Css = "";
                Sessions.HeaderLogo = "Logo.gif";
                Sessions.LargeImage = "colorpath_logo_home.jpg";
            }
            #endregion

            // === Check For Sync Permission for particular User === //
            // === By Chirag Shah(2 May, 2010) === //
            #region Table 8 : Check For Sync Permission for particular User : By Chirag Shah(2 May, 2010)
                if (DSet.Tables[8] != null && DSet.Tables[8].Rows.Count > 0 && Convert.ToBoolean(DSet.Tables[8].Rows[0]["IsProcessorAllow"]))
                    Sessions.Access_SyncView = true;
                else
                    Sessions.Access_SyncView = false;
            #endregion

            #region Create a DataTable for Access Information
            DataTable TabAccess = new DataTable();
            TabAccess.Columns.Add("ID");
            TabAccess.Columns.Add("Desc");
            TabAccess.Columns.Add("Page");
            TabAccess.Columns.Add("Image");
            TabAccess.Columns.Add("SmallImage");
            DataRow ROW;

            #region Check Company Access
            if ((Fuji.Common.Sessions.Add_Company == true) || Fuji.Common.Sessions.Delete_Company == true)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "A";
                ROW["Desc"] = "Company Management";
                ROW["Page"] = "MasterCompany.aspx";
                ROW["Image"] = "Images/company-manage-icon.png";
                ROW["SmallImage"] = "images/company-manage-icon-s.gif";
                TabAccess.Rows.Add(ROW);
            }

            #endregion

            #region Check Branch Access
            if ((Fuji.Common.Sessions.Add_Branch == true) || Fuji.Common.Sessions.Update_Branch == true || Fuji.Common.Sessions.Delete_Branch == true || Fuji.Common.Sessions.Add_Support_User == true)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "B";
                ROW["Desc"] = "Branch Management";
                ROW["Page"] = "BranchManagement.aspx";
                ROW["Image"] = "Images/branch-manage-icon.png";
                ROW["SmallImage"] = "images/branch-manage-icon-s.gif";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check User Access
            if ((Fuji.Common.Sessions.Add_User == true) || Fuji.Common.Sessions.Update_User == true || Fuji.Common.Sessions.Delete_User == true)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "C";
                ROW["Desc"] = "User Management";
                ROW["Page"] = "UserManagement.aspx";
                ROW["Image"] = "images/user-manage-icon.png";
                ROW["SmallImage"] = "images/user-manage-icon-s.gif";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check Role Access
            //if (Fuji.Common.Sessions.RoleID < 1 || Fuji.Common.Sessions.RoleID != 0)
            //{
            //    ROW = TabAccess.NewRow();
            //    ROW["ID"] = "D";
            //    ROW["Desc"] = "Role Management";
            //    ROW["Page"] = "RoleManagement.aspx";
            //    ROW["Image"] = "images/role-manage-icon.png";
            //    ROW["SmallImage"] = "images/role-manage-icon-s.gif";
            //    TabAccess.Rows.Add(ROW);
            //}
            #endregion

            #region Check Device Access
            if ((Fuji.Common.Sessions.Add_Device == true) || Fuji.Common.Sessions.Update_Device == true || Fuji.Common.Sessions.Delete_Device == true)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "E";
                ROW["Desc"] = "Device Management";
                ROW["Page"] = "DeviceManagement.aspx";
                ROW["Image"] = "Images/device-manage-icon.png";
                ROW["SmallImage"] = "images/device-manage-icon-s.gif";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check Dongle Access
            if ((Fuji.Common.Sessions.Add_Dongle == true) || Fuji.Common.Sessions.Update_Dongle == true || Fuji.Common.Sessions.Delete_Dongle == true)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "F";
                ROW["Desc"] = "Dongle Management";
                ROW["Page"] = "DongleManagement.aspx";
                ROW["Image"] = "Images/dongle-manage.png";
                ROW["SmallImage"] = "Images/dongle-small-icon.gif";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check RH Device Access
            if ((Fuji.Common.Sessions.Add_Temp_RH_Device == true) || Fuji.Common.Sessions.Update_Temp_RH_Device == true || Fuji.Common.Sessions.Delete_Temp_RH_Device == true)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "G";
                ROW["Desc"] = "Temp RH Device Management";
                ROW["Page"] = "TempRHDeviceManagement.aspx";
                ROW["Image"] = "Images/TempRHDevice-icon.png";
                ROW["SmallImage"] = "Images/TempRHDevice-small-icon.gif";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check Delete Measurement
            if (Fuji.Common.Sessions.RoleDesc == Fuji.Common.Enums.RoleType.TaskeroAdmin)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "I";
                ROW["Desc"] = "Delete Measurement";
                ROW["Page"] = "ManageMeasurement.aspx";
                ROW["Image"] = "Images/Delete-Measurement-icon.png";
                ROW["SmallImage"] = "images/delete-measurement-icon.gif";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check Add/Delete Support User
            if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin || Fuji.Common.Sessions.RoleDesc == Enums.RoleType.CountryAdmin)
            {
                if (Fuji.Common.Sessions.Add_Support_User == true || Fuji.Common.Sessions.Remove_Support_User == true)
                {
                    ROW = TabAccess.NewRow();
                    ROW["ID"] = "J";
                    ROW["Desc"] = "Support Management";
                    ROW["Page"] = "SupportManagement.aspx";
                    ROW["Image"] = "Images/SupportManagement-icon.png";
                    ROW["SmallImage"] = "images/SupportManagement-icon-s.png";
                    TabAccess.Rows.Add(ROW);
                }
            }
            #endregion

            #region Check Brand
            if (Fuji.Common.Sessions.Access_Branding == true )
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "H";
                ROW["Desc"] = "Brand Management";
                ROW["Page"] = "BrandingName.aspx";
                ROW["Image"] = "Images/brand-manage-icon.png";
                ROW["SmallImage"] = "images/brand-manage-icon.gif";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check Video File Management
            if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin || Fuji.Common.Sessions.Access_Video == true)
            {
                ROW = TabAccess.NewRow();
                ROW["ID"] = "K";
                ROW["Desc"] = "Video Management";
                ROW["Page"] = "VideoManagement.aspx";
                ROW["Image"] = "Images/video-player-icon.png";
                ROW["SmallImage"] = "images/video-player-icon-small.png";
                TabAccess.Rows.Add(ROW);
            }
            #endregion

            #region Check Sync Management

            if (System.Web.HttpContext.Current.Session["Username"].ToString().ToUpper() == "MWINN" || System.Web.HttpContext.Current.Session["Username"].ToString().ToUpper() == "TASKEROWEBADMIN")
            {
            
                if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin || Fuji.Common.Sessions.Access_SyncView == true)
                {
                    ROW = TabAccess.NewRow();
                    ROW["ID"] = "L";
                    ROW["Desc"] = "Sync Device Management";
                    ROW["Page"] = "SyncManagement.aspx";
                    ROW["Image"] = "Images/syncdevicemgt.png"; 
                    ROW["SmallImage"] = "images/syncdevicemgt_small.png"; 
                    TabAccess.Rows.Add(ROW);
                }

            }
            #endregion
          

            #region Download Mangement
            ROW = TabAccess.NewRow();
            ROW["ID"] = "M";
            ROW["Desc"] = "Download";
            ROW["Page"] = "AdminDownload.aspx";
            ROW["Image"] = "Images/download-icon.png";
            ROW["SmallImage"] = "images/download-icon-small.png";
            TabAccess.Rows.Add(ROW);
            #endregion

            #region Check Sync Measurement Device

            if (System.Web.HttpContext.Current.Session["Username"].ToString().ToUpper() == "MWINN" || System.Web.HttpContext.Current.Session["Username"].ToString().ToUpper() == "TASKEROWEBADMIN")
            {

                if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin || Fuji.Common.Sessions.Access_SyncView == true)
                {
                    ROW = TabAccess.NewRow();
                    ROW["ID"] = "N";
                    ROW["Desc"] = "Measurement Device";
                    ROW["Page"] = "SyncMeasurementDevice.aspx";
                    ROW["Image"] = "Images/syncdevicemgt.png";
                    ROW["SmallImage"] = "images/syncdevicemgt_small.png";
                    TabAccess.Rows.Add(ROW);
                }

            }
            #endregion

            SetSessions("AccessData", TabAccess);
            #endregion
        }
        public static void ClearLoginSession()
        {
            #region Is Logged In
            Sessions.IsLoggedIn = false;
            #endregion

            #region Table 1 : Taskero User
            Sessions.UserID = 0;
            Sessions.Username = "";
            Sessions.LastName = "";
            Sessions.FirstName = "";
            Sessions.Email = "";
            #endregion

            #region Role
            Sessions.RoleID = 0;
            #endregion

            #region Permission
            Sessions.View_Jobs_Tab = false;
            Sessions.Discard_CPV_Measurement = false;
            Sessions.View_Device_Status = false;
            Sessions.Reset_Device_Status = false;
            Sessions.View_Reports = false;
            Sessions.Add_Dongle = false;
            Sessions.Update_Dongle = false;
            Sessions.Delete_Dongle = false;
            Sessions.Add_Company = false;
            Sessions.Delete_Company = false;
            Sessions.Add_Branch = false;
            Sessions.Update_Branch = false;
            Sessions.Delete_Branch = false;
            Sessions.Add_Device = false;
            Sessions.Update_Device = false;
            Sessions.Delete_Device = false;
            Sessions.Add_Temp_RH_Device = false;
            Sessions.Update_Temp_RH_Device = false;
            Sessions.Delete_Temp_RH_Device = false;
            Sessions.Add_User = false;
            Sessions.Update_User = false;
            Sessions.Delete_User = false;
            Sessions.Delete_Print_Target = false;
            Sessions.View_Error_Log = false;
            Sessions.View_Data_Monitor = false;
            Sessions.Add_Support_User = false;
            Sessions.Remove_Support_User = false;
            Sessions.View_Press_Data = false;
            Sessions.View_Devices_Tab = false;
            #endregion

            #region Table 5 : Company ID
            Sessions.CompanyID = 0;
            #endregion

            #region Table 6 : Branch ID
            Sessions.BranchIDs = "";
            #endregion

            #region Table 7 : Dongle ID
            Sessions.DongleIDs = "";
            #endregion
        }
        #endregion

        #region Check User Logins and Send it to default
        public static void CheckUserLogin()
        {
            if (Sessions.IsLoggedIn == false) { Common.CommonFunctions.PageRedirect("Default", ""); }
        }
        public static void CheckUserLoginModelDialog()
        {
            if (Sessions.IsLoggedIn == false)
            {
                //========== Script to Send to Main Screen ==========//
                string _Script = "<script language=\"javascript\">window.parent.window.location.href = \"default.aspx\";</script>";

                //========== Write To Execute ==========//
                System.Web.HttpContext.Current.Response.Write(_Script);
            }
        }
        public static void CheckUserLoginPopUp()
        {
            if (Sessions.IsLoggedIn == false)
            {
                //========== Script to Send to Main Screen ==========//
                string _Script = "<script language=\"javascript\">window.opener.window.location.href = \"default.aspx\";window.close();</script>";

                //========== Write To Execute ==========//
                System.Web.HttpContext.Current.Response.Write(_Script);
            }
        }
        public static void CheckUserLoginPopUpRefresh()
        {
            if (Sessions.IsLoggedIn == false)
            {
                //========== Script to Send to Main Screen ==========//
                string _Script = "<script language=\"javascript\">window.opener.window.parent.window.location.href = \"default.aspx\";window.close();</script>";

                //========== Write To Execute ==========//
                System.Web.HttpContext.Current.Response.Write(_Script);
            }
        }
        #endregion

        #region Check User Login And Send it to Login Page For Mabile Application
        public static void CheckUserLoginForMobi()
        {
            if (Sessions.IsLoggedIn == false) { Common.CommonFunctions.PageRedirect("Login", ""); }
        }
        #endregion

    }
}
