using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSyncV;
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
            ViewLastAnalyze = 3,
            ViewLastValidationAnalyze = 4,
            Validation = 5
        }

        #region Variable Declarations

        private readonly PresenterColorOrganizerRequestProcessor _presenterColorOrganizerRequestProcessor;
        private const string _qsActionKey = "action";
        private const string _qsactionValueAdd = "add";
        private const string _qsactionValueEdit = "edit";
        private const string _qsactionValueView = "viewLastAnalyze";
        private const string _qsactionValueValidationView = "viewLastValidationAnalyze";
        private const string _qsUserNameKey = "uid";
        private const string _qsPasswordKey = "pwd";
        private const string _qsDeviceIdKey = "deviceid";
        private const string _qsProjectInfoIdKey = "projinfoid";
        private const string _qsProjectIdKey = "projectid";
        private const string _qsChartIdKey = "chartid";
        private const string _qsValidation = "validation";
        private const string _qsValidationInfoId = "validationinfoid";
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
                     Equals(_qsactionValueView, StringComparison.CurrentCultureIgnoreCase) &&
                !HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsactionValueValidationView, StringComparison.CurrentCultureIgnoreCase) &&
                !HttpContext.Current.Request.QueryString[_qsActionKey].
                     Equals(_qsValidation, StringComparison.CurrentCultureIgnoreCase))
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
            else if (HttpContext.Current.Request.QueryString[_qsActionKey].
            Equals(_qsactionValueValidationView, StringComparison.CurrentCultureIgnoreCase))
            {
                return ProcessViewLastValidationAnalyzeRequest();
            }
            else if (HttpContext.Current.Request.QueryString[_qsActionKey].
                Equals(_qsValidation, StringComparison.CurrentCultureIgnoreCase))
            {
                return ProcessValidationRequest();
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

            //Reset SubDLPOperation Session
            SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;

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
                    //SessionManager.Instance.CurrentProjLimit = SessionManager.Instance.CurrentDevice.LicenseTypeProject.Single(x => x.Key == ELicenseType.DLP).Value;

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
                        case Action.ViewLastValidationAnalyze:
                            break;
                        case Action.Validation:
                            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsValidationInfoId]))
                                return false;
                            TempValidationInfoId = Guid.Parse(HttpContext.Current.Request.QueryString[_qsValidationInfoId]);
                            _presenterColorOrganizerRequestProcessor.GetTempValidationInfo();

                            //Validate provided data here
                            if (TempValidationInfo.JobName.Length < 4)
                            {
                                Entity.COErrorMessage errorMessage = new Entity.COErrorMessage();
                                errorMessage.Header = "";
                                errorMessage.Message = FujifilmUSA.Sync.GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("JobName_LengthError");
                                SessionManager.Instance.COErrorMessage = errorMessage;
                                HttpContext.Current.Response.Redirect(string.Format("COErrorMessage.aspx?id={0}", Guid.NewGuid()));
                            }

                            SessionManager.Instance.CurrentValidationRequest = TempValidationInfo;
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

        private bool ProcessAddRequest()
        {
            if (CommonProcess())
            {
                #region Setting New Project Information

                if (SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject))
                {
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Description;

                    DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
                    LicenseTypeId = Convert.ToInt32(ELicenseType.DLP);
                    _presenterColorOrganizerRequestProcessor.GetProjectCountByDeviceId();

                    int maxProjectLimit = SessionManager.Instance.CurrentDevice.LicenseTypeProject.Single(x => x.Key == ELicenseType.DLP).Value;
                    if (TotalProjects >= maxProjectLimit)
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

                        SessionManager.Instance.CurrentProject.ProjectName = TempProjectInfo.ProjectName;
                        SessionManager.Instance.CurrentProject.PrintCondition = new PrintCondition()
                                                                                    {
                                                                                        Media = TempProjectInfo.Media,
                                                                                        Colorant = TempProjectInfo.Colorant,
                                                                                        RipSettings = TempProjectInfo.RipSettings
                                                                                    };
                        SessionManager.Instance.CurrentProject.LicenseType = ELicenseType.DLP;
                        SessionManager.Instance.CurrentProject.MeasurementSetting =
                            new MeasurementSetting()
                                {
                                    ReferenceChartType =
                                        new KeyValuePair<short, string>(
                                        Convert.ToInt16(TempProjectInfo.ChartId),
                                        string.Empty),
                                    OptReferenceChartType =
                                        new KeyValuePair<short, string>(
                                        Convert.ToInt16(TempProjectInfo.ChartId),
                                        string.Empty)
                                };
                        HttpContext.Current.Response.Redirect(string.Format("COAddProject.aspx?id={0}", Guid.NewGuid()));
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

                        if (!(SessionManager.Instance.CurrentOptimizeRun != null && SessionManager.Instance.CurrentOptimizeRun.HasProfile))
                        {
                            //This will be called when no DLP is created
                            SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType =
                                            new KeyValuePair<short, string>(Convert.ToInt16(TempProjectInfo.ChartId), string.Empty);
                        }
                        //This will be set every time
                        SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType =
                                           new KeyValuePair<short, string>(Convert.ToInt16(TempProjectInfo.ChartId), string.Empty);

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

                    if (Project.Status == EOptimizeRunStatus.Open)
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

        private bool ProcessViewLastValidationAnalyzeRequest()
        {
            if (CommonProcess())
            {
                #region Setting Existing Project Information
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_qsProjectIdKey]))
                {
                    ProjectId = Convert.ToInt32(HttpContext.Current.Request.QueryString[_qsProjectIdKey]);
                    _presenterColorOrganizerRequestProcessor.GetProjectDetailWithVerification();

                    //SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                    if (Project.Status != EOptimizeRunStatus.Open && Project.Status != EOptimizeRunStatus.Closed)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        //Set validation as sub operation type
                        SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.Validation;
                        if (SessionManager.Instance.CurrentOptimizeRun.HasMeasurements)
                        {
                            //Redirect to analyse page
                            HttpContext.Current.Response.Redirect(string.Format("COAnalyze.aspx?id={0}", Guid.NewGuid()));
                        }
                        else
                        {
                            Entity.COErrorMessage errorMessage = new Entity.COErrorMessage();
                            errorMessage.Header = "";
                            errorMessage.Message = GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("Validation_NoMeasurements");
                            SessionManager.Instance.COErrorMessage = errorMessage;
                            HttpContext.Current.Response.Redirect(string.Format("COErrorMessage.aspx?id={0}", Guid.NewGuid()));
                        }
                    }
                    else if (Project.Status == EOptimizeRunStatus.Open)
                    {
                        Entity.COErrorMessage errorMessage = new Entity.COErrorMessage();
                        errorMessage.Header = "";
                        errorMessage.Message = GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("Validation_ProjectOpenError");
                        SessionManager.Instance.COErrorMessage = errorMessage;
                        HttpContext.Current.Response.Redirect(string.Format("COErrorMessage.aspx?id={0}", Guid.NewGuid()));
                    }
                    else if (Project.Status == EOptimizeRunStatus.Closed)
                    {
                        Entity.COErrorMessage errorMessage = new Entity.COErrorMessage();
                        errorMessage.Header = "";
                        errorMessage.Message = GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("Validation_ProjectClosedError");
                        SessionManager.Instance.COErrorMessage = errorMessage;
                        HttpContext.Current.Response.Redirect(string.Format("COErrorMessage.aspx?id={0}", Guid.NewGuid()));
                    }
                    
                }
                #endregion
            }
            return false;
        }

        private bool ProcessValidationRequest()
        {
            if (CommonProcess())
            {
                #region Setting Existing Project Information
                if (SessionManager.Instance.CurrentValidationRequest.ProjectId > 0)
                {
                    ProjectId = SessionManager.Instance.CurrentValidationRequest.ProjectId;
                    _presenterColorOrganizerRequestProcessor.GetProjectDetail();
                    if (Project.Status != EOptimizeRunStatus.Open && Project.Status != EOptimizeRunStatus.Closed)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        //Set validation as sub operation type
                        SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.Validation;

                        //Redirect to measurment page
                        HttpContext.Current.Response.Redirect(string.Format("COMeasurement.aspx?id={0}&projectid={1}", Guid.NewGuid(), SessionManager.Instance.CurrentProject.ProjectId));
                    }
                    else if (Project.Status == EOptimizeRunStatus.Open)
                    {
                        Entity.COErrorMessage errorMessage = new Entity.COErrorMessage();
                        errorMessage.Header = "";
                        errorMessage.Message = GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("Validation_ProjectOpenError");
                        SessionManager.Instance.COErrorMessage = errorMessage;
                        HttpContext.Current.Response.Redirect(string.Format("COErrorMessage.aspx?id={0}", Guid.NewGuid()));
                    }
                    else if (Project.Status == EOptimizeRunStatus.Closed)
                    {
                        Entity.COErrorMessage errorMessage = new Entity.COErrorMessage();
                        errorMessage.Header = "";
                        errorMessage.Message = GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString("Validation_ProjectClosedError");
                        SessionManager.Instance.COErrorMessage = errorMessage;
                        HttpContext.Current.Response.Redirect(string.Format("COErrorMessage.aspx?id={0}", Guid.NewGuid()));
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

        public Guid TempValidationInfoId { get; set; }

        public TempValidationInfo TempValidationInfo { get; set; }

        #endregion
    }
}