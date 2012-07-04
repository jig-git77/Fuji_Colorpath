using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Web.Security;

namespace FujifilmUSA.Sync.Web.ColorOrganizer
{
    public class ColorOrganizerRequestProcessor : IViewColorOrganizerRequestProcessor
    {
        internal enum Action
        {
            None = 0,
            Add = 1,
            Edit = 2,
            ViewLastAnalyze = 3
        }

        #region Variable Declarations

        private readonly PresenterColorOrganizerRequestProcessor _presenterColorOrganizerRequestProcessor;
        private const string _qsActionKey = "action";
        private const string _qsactionValueAdd = "add";
        private const string _qsactionValueEdit = "edit";
        private const string _qsactionValueView = "viewLastAnalyze";
        private const string _qsUserNameKey = "uid";
        private const string _qsPasswordKey = "pwd";
        private const string _qsDeviceIdKey = "deviceid";
        private const string _qsProjectInfoIdKey = "projinfoid";
        private const string _qsProjectIdKey = "projectid";
        private const string _qsChartIdKey = "chartid";

        private bool IsErrorOccured { get; set; }

        #endregion

        #region Ctor

        public ColorOrganizerRequestProcessor()
        {
            _presenterColorOrganizerRequestProcessor = new PresenterColorOrganizerRequestProcessor(this);
        }
        #endregion

        #region Public Methods
        public bool ProcessRequest()
        {
            if (HttpContext.Current.Request.QueryString[_qsActionKey] == null)
                return false;

            if (!HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsactionValueAdd, StringComparison.CurrentCultureIgnoreCase) &&
                !HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsactionValueEdit, StringComparison.CurrentCultureIgnoreCase) &&
                !HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsactionValueView, StringComparison.CurrentCultureIgnoreCase))
                return false;

            if (HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsactionValueAdd, StringComparison.CurrentCultureIgnoreCase))
            {
                return ProcessAddRequest();
            }
            else if (HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsactionValueEdit, StringComparison.CurrentCultureIgnoreCase))
            {
                return ProcessEditRequest();
            }
            else if (HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsactionValueView, StringComparison.CurrentCultureIgnoreCase))
            {
                return ProcessViewLastAnalyzeRequest();
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        //private bool AuthenticateUserCredentials()
        //{
        //    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsUserNameKey]) ||
        //         string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsPasswordKey]) ||
        //         string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsDeviceIdKey]))
        //        return false;


        //    UserName = HttpContext.Current.Request.QueryString[_qsUserNameKey];
        //    Password = HttpContext.Current.Request.QueryString[_qsPasswordKey].Replace(' ', '+');
        //    _presenterColorOrganizerRequestProcessor.GetLoginInformation();
        //    if (CurrentUser != null && CurrentUser.UserId > 0)
        //    {
        //        SessionManager.Instance.CurrentUser = CurrentUser;
        //        FormsAuthentication.RedirectFromLoginPage(UserName, false);

        //        DeviceId = Convert.ToInt32(HttpContext.Current.Request.QueryString[_qsDeviceIdKey]);
        //        _presenterColorOrganizerRequestProcessor.GetDeviceById();
        //        if (!IsErrorOccured)
        //        {
        //            SessionManager.Instance.CurrentDevice = Device;
        //            SessionManager.Instance.CurrentOperation = ELicenseType.DLP;
        //            SessionManager.Instance.CurrentProjLimit = SessionManager.Instance.CurrentDevice.LicenseTypeProject.Single(x => x.Key == ELicenseType.DLP).Value;
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        private bool CommonProcess()
        {
            //Validate user credentials
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsUserNameKey]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsPasswordKey]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsDeviceIdKey]))
                return false;

            //Set login user
            UserName = HttpContext.Current.Request.QueryString[_qsUserNameKey];
            Password = HttpContext.Current.Request.QueryString[_qsPasswordKey].Replace(' ', '+');
            _presenterColorOrganizerRequestProcessor.GetLoginInformation();
            if (CurrentUser != null && CurrentUser.UserId > 0)
            {
                SessionManager.Instance.CurrentUser = CurrentUser;
                FormsAuthentication.RedirectFromLoginPage(UserName, false);

                //Get Current Device info
                DeviceId = Convert.ToInt32(HttpContext.Current.Request.QueryString[_qsDeviceIdKey]);
                _presenterColorOrganizerRequestProcessor.GetDeviceById();
                if (!IsErrorOccured)
                {
                    //Set device to session
                    SessionManager.Instance.CurrentDevice = Device;
                    SessionManager.Instance.CurrentOperation = ELicenseType.DLP;
                    SessionManager.Instance.CurrentProjLimit = SessionManager.Instance.CurrentDevice.LicenseTypeProject.Single(x => x.Key == ELicenseType.DLP).Value;

                    //Convert action to enum
                    Action action = 0;
                    action = (Action)Enum.Parse(typeof(Action), HttpContext.Current.Request.QueryString[_qsActionKey].ToString().Trim().ToLower(), true);
                    switch (action)
                    {
                        case Action.Add:
                        case Action.Edit:
                            //Project info id is used in Add or Edit only
                            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsProjectInfoIdKey]))
                                return false;
                            TempProjectId = Guid.Parse(HttpContext.Current.Request.QueryString[_qsProjectInfoIdKey]);
                            _presenterColorOrganizerRequestProcessor.GetTempProjectInfo();
                            SessionManager.Instance.CurrentColorOrgRequest = TempProjectInfo;
                            break;
                        case Action.ViewLastAnalyze:
                            break;
                        default:
                            break;
                    }
                    //Return true if no error
                    if (!IsErrorOccured)
                        return true;
                }
            }
            return false;
        }

        private bool CommonProcess(string t)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsUserNameKey]) ||
                 string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsPasswordKey]) ||
                 string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsDeviceIdKey]) ||
                string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsProjectInfoIdKey]))
                return false;

            #region Setting Login User
            UserName = HttpContext.Current.Request.QueryString[_qsUserNameKey];
            Password = HttpContext.Current.Request.QueryString[_qsPasswordKey].Replace(' ', '+');
            _presenterColorOrganizerRequestProcessor.GetLoginInformation();
            if (CurrentUser != null && CurrentUser.UserId > 0)
            {
                SessionManager.Instance.CurrentUser = CurrentUser;
                FormsAuthentication.RedirectFromLoginPage(UserName, false);

            #endregion

                #region Setting Current Device

                DeviceId = Convert.ToInt32(HttpContext.Current.Request.QueryString[_qsDeviceIdKey]);
                _presenterColorOrganizerRequestProcessor.GetDeviceById();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentDevice = Device;

                #endregion

                    #region Setting Current Operation - Static to DLP

                    SessionManager.Instance.CurrentOperation = ELicenseType.DLP;
                    SessionManager.Instance.CurrentProjLimit =
                        SessionManager.Instance.CurrentDevice.LicenseTypeProject.Single(x => x.Key == ELicenseType.DLP).Value;

                    #endregion

                    #region Setting TempProjectInfo to Session
                    TempProjectId = Guid.Parse(HttpContext.Current.Request.QueryString[_qsProjectInfoIdKey]);
                    _presenterColorOrganizerRequestProcessor.GetTempProjectInfo();
                    SessionManager.Instance.CurrentColorOrgRequest = TempProjectInfo;

                    #endregion

                    return true;
                }
            }
            return false;
        }

        private bool ProcessAddRequest()
        {
            if (CommonProcess())
            {
                #region Setting New Project Information

                if (SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject))
                {
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;

                    DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
                    LicenseTypeId = Convert.ToInt32(SessionManager.Instance.CurrentOperation);
                    _presenterColorOrganizerRequestProcessor.GetProjectCountByDeviceId();

                    if (TotalProjects >= SessionManager.Instance.CurrentProjLimit)
                    {
                        //--------------Project Limit reach, so can not create more projects under this Device now-------//  
                        HttpContext.Current.Response.Redirect("COProjectMaxLimit.aspx");
                        return false;
                    }
                    else
                    {
                        //------------Create new Project under this Device-----------------//
                        SessionManager.Instance.CurrentProject = new Project()
                                                                     {
                                                                         DeviceId = SessionManager.Instance.CurrentDevice.DeviceId,
                                                                         IsUsed = true,
                                                                         CurrentDisplayStatus = ECurrentProjectStatus.New,
                                                                     };
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;

                        if (!IsErrorOccured)
                        {
                            SessionManager.Instance.CurrentProject.ProjectName = TempProjectInfo.ProjectName;
                            SessionManager.Instance.CurrentProject.PrintCondition = new PrintCondition()
                                                                                        {
                                                                                            Media = TempProjectInfo.Media,
                                                                                            Colorant = TempProjectInfo.Colorant,
                                                                                            RipSettings = TempProjectInfo.RipSettings
                                                                                        };
                            SessionManager.Instance.CurrentProject.MeasurementSetting =
                                new MeasurementSetting()
                                    {
                                        ReferenceChartType =
                                            new KeyValuePair<short, string>(
                                            Convert.ToInt16(TempProjectInfo.ChartId),
                                            string.Empty)
                                    };
                            HttpContext.Current.Response.Redirect(string.Format("COAddProject.aspx?id={0}", Guid.NewGuid()));
                        }
                    }
                }

                #endregion
            }
            return false;
        }

        private bool ProcessEditRequest()
        {
            if (CommonProcess())
            {
                #region Setting Existing Project Information
                if (SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ModifyProject))
                {
                    ProjectId = TempProjectInfo.ProjectId;
                    _presenterColorOrganizerRequestProcessor.GetProjectDetail();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentProject.MeasurementSetting =
                            new MeasurementSetting()
                                {
                                    ReferenceChartType =
                                        new KeyValuePair<short, string>(
                                        Convert.ToInt16(TempProjectInfo.ChartId),
                                        string.Empty)
                                };
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
                        HttpContext.Current.Response.Redirect(string.Format("COAddProject.aspx?id={0}", Guid.NewGuid()));
                    }
                }

                #endregion
            }
            return false;
        }

        private bool ProcessViewLastAnalyzeRequest()
        {
            if (CommonProcess())
            {
                #region Setting Existing Project Information
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsProjectIdKey]))
                {
                    ProjectId = Convert.ToInt32(HttpContext.Current.Request.QueryString[_qsProjectIdKey]);
                    _presenterColorOrganizerRequestProcessor.GetProjectDetail();
                    
                    if (Project.Status == ERefChartDisplayType.Open)
                    {
                        //Redirect to other page                        
                        HttpContext.Current.Response.Redirect("COViewOpenProject.aspx");
                    }
                    else
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        //Redirect to COAnalyze page
                        HttpContext.Current.Response.Redirect("COAnalyze.aspx");
                    }
                }
                #endregion
            }
            return false;
        }

        #endregion

        #region IViewColorOrganizerRequestProcessor Members

        public string UserName { get; set; }

        public string Password { get; set; }

        public User CurrentUser { get; set; }

        public Device Device { get; set; }

        public int DeviceId { get; set; }

        public int CurrentUserId { get; set; }

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            IsErrorOccured = true;
            throw new NotImplementedException();
        }

        public Guid TempProjectId { get; set; }

        public TempProjectInfo TempProjectInfo { get; set; }

        public int TotalProjects { get; set; }

        public int LicenseTypeId { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        #endregion
    }
}