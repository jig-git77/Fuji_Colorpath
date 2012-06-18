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
        #region Variable Declarations

        private readonly PresenterColorOrganizerRequestProcessor _presenterColorOrganizerRequestProcessor;
        private const string _qsActionKey = "action";
        private const string _qsactionValueAdd = "add";
        private const string _qsactionValueEdit = "edit";
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
                     Equals(_qsactionValueEdit, StringComparison.CurrentCultureIgnoreCase))
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
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        private bool CommonProcess()
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
                        HttpContext.Current.Response.Redirect("ProjectMaxLimit.aspx");
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