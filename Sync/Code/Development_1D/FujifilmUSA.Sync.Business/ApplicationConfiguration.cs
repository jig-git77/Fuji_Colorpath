using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business
{
    public class ApplicationConfiguration : ITaskBase
    {
        #region Variable Declarations
        private static ApplicationConfiguration _applicationConfiguration;
        private string _encryptionKey;
        private string _xmfColorOrganizerEncryptionKey;
        private bool _sendEmailNotificationOnError;
        private string _sendEmailNotificationOnErrorSubject;
        private string _sendEmailNotificationOnErrorTo;
        private string _passwordSetLink;
        private string _passwordReSetLink;
        private string _passwordForceReSetLink;
        private int _optimizeRunLimit;
        private int _measurementsLimit;
        private string _webSiteLink;
        private bool _enableActivityLog;
        private bool _enableSingleUserLogin;
        private int _keepUserAliveInSeconds;
        private string _fujifilmPluginVersion;
        #endregion

        #region Singleton
        
        private ApplicationConfiguration()
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsApplicationConfiguration = DataAccessFactory.Instance.GetDsResult("dbo.GetApplicationConfiguration");
                    if (dsApplicationConfiguration.Tables[0].Rows.Count > 0)
                    {
                        var configurationData = dsApplicationConfiguration.
                            Tables[0].
                            AsEnumerable().
                            Select(x => new
                                            {
                                                Key = DataReader.ReadString(x, "ConfigurationKey"),
                                                Value = DataReader.ReadString(x, "ConfigurationValue")
                                            }).ToList();

                        configurationData.ForEach(x =>
                                                      {
                                                          switch (x.Key.ToUpper())
                                                          {
                                                              case "ENCRYPTIONKEY":
                                                                  _encryptionKey = x.Value;
                                                                  break;
                                                              case "XMFCOLORORGANIZERENCRYPTIONKEY":
                                                                  _xmfColorOrganizerEncryptionKey = x.Value;
                                                                  break;
                                                              case "SENDEMAILNOTIFICATIONONERROR":
                                                                  _sendEmailNotificationOnError = Convert.ToBoolean(x.Value);
                                                                  break;
                                                              case "SENDEMAILNOTIFICATIONONERRORSUBJECT":
                                                                  _sendEmailNotificationOnErrorSubject = x.Value;
                                                                  break;
                                                              case "SENDEMAILNOTIFICATIONONERRORTO":
                                                                  _sendEmailNotificationOnErrorTo = x.Value;
                                                                  break;
                                                              case "PASSWORDSETLINK":
                                                                  _passwordSetLink = x.Value;
                                                                  break;
                                                              case "PASSWORDRESETLINK":
                                                                  _passwordReSetLink = x.Value;
                                                                  break;
                                                              case "PASSWORDFORCERESETLINK":
                                                                  _passwordForceReSetLink = x.Value;
                                                                  break;
                                                              case "OPTIMIZERUNLIMIT":
                                                                  _optimizeRunLimit = Convert.ToInt32(x.Value);
                                                                  break;
                                                              case "MEASUREMENTSLIMIT":
                                                                  _measurementsLimit = Convert.ToInt32(x.Value);
                                                                  break;
                                                              case "WEBSITELINK":
                                                                  _webSiteLink = x.Value;
                                                                  break;
                                                              case "ENABLEACTIVITYLOG":
                                                                  _enableActivityLog = Convert.ToBoolean(x.Value);
                                                                  break;
                                                              case "ENABLESINGLEUSERLOGIN":
                                                                  _enableSingleUserLogin = Convert.ToBoolean(x.Value);
                                                                  break;
                                                              case "KEEPUSERALIVEINSECONDS":
                                                                  _keepUserAliveInSeconds = Convert.ToInt32(x.Value);
                                                                  break;
                                                              case "FUJIFILMPLUGINVERSION":
                                                                  _fujifilmPluginVersion = x.Value;
                                                                  break;
                                                          }
                                                      });
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetApplicationConfigurations"));
        }

        public static ApplicationConfiguration Instance
        {
            get { return _applicationConfiguration ?? (_applicationConfiguration = new ApplicationConfiguration()); }
        }

        public static void ClearInstance()
        {
            lock (_applicationConfiguration)
            {
                _applicationConfiguration = null;
            }

        }
        #endregion

        #region Configuration Properties
        public string EncryptionKey
        {
            get
            {
                return _encryptionKey;
            }
        }
        public string XmfColorOrganizerEncryptionKey
        {
            get
            {
                return _xmfColorOrganizerEncryptionKey;
            }
        }
        public bool SendEmailNotificationOnError
        {
            get
            {
                return _sendEmailNotificationOnError;
            }
        }
        public string SendEmailNotificationOnErrorSubject
        {
            get
            {
                return _sendEmailNotificationOnErrorSubject;
            }
        }
        public string SendEmailNotificationOnErrorTo
        {
            get
            {
                return _sendEmailNotificationOnErrorTo;
            }
        }
        public string PasswordSetLink
        {
            get
            {
                return _passwordSetLink;
            }
        }
        public string PasswordReSetLink
        {
            get
            {
                return _passwordReSetLink;
            }
        }
        public string PasswordForceReSetLink
        {
            get
            {
                return _passwordForceReSetLink;
            }
        }
        public int OptimizeRunLimit
        {
            get
            {
                return _optimizeRunLimit;
            }
        }
        public int MeasurementsLimit
        {
            get
            {
                return _measurementsLimit;
            }
        }
        public string WebSiteLink
        {
            get
            {
                return _webSiteLink;
            }
        }
        public bool EnableActivityLog
        {
            get
            {
                return _enableActivityLog;
            }
        }
        public bool EnableSingleUserLogin
        {
            get
            {
                return _enableSingleUserLogin;
            }
        }
        public int KeepUserAliveInSeconds
        {
            get
            {
                return _keepUserAliveInSeconds;
            }
        }
        public string FujiFilmPluginVersion
        {
            get
            {
                return _fujifilmPluginVersion;
            }
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
