using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Windows;
using ServiceDemo.Sync4DService;
using Service.Common.Encryption;

namespace ServiceDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Methods { get; set; }
        public Dictionary<int, string> ProfileTypes { get; set; }
        public string SelectedMethod { get; set; }
        private string _userName;
        private string _userPassword;
        private string _securityToken;

        public MainWindow()
        {
            Methods = new List<String>() { "Get Device List", "Get ReferenceChart List", "Get Profile", "Get Project Information", "Store Temp Project Information" };

            ProfileTypes = new Dictionary<int, string>()
                               {
                                   { Convert.ToInt32(EProfileType.None), EProfileType.None.ToString() },
                                   { Convert.ToInt32(EProfileType.SourceProfile), EProfileType.SourceProfile.ToString() },
                                   { Convert.ToInt32(EProfileType.DestinationPRofile), EProfileType.DestinationPRofile.ToString() },
                                   { Convert.ToInt32(EProfileType.DeviceLinkProfile), EProfileType.DeviceLinkProfile.ToString() },
                               };
            InitializeComponent();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            _securityToken = ConfigurationManager.AppSettings["SecurityToken"];

            this.DataContext = this;
        }

        private void btnCallService_Click(object sender, RoutedEventArgs e)
        {
            _userName = txtUserName.Text;
            _userPassword = CryptHelper.EncryptString(txtPassword.Password, ConfigurationManager.AppSettings["XmfColorOrganizerEncryptionKey"]);            
            using (var client = new Sync4DServiceContractClient())
            {
                try
                {
                    switch (SelectedMethod)
                    {
                        case "Get Device List":

                            #region GetDeviceList

                            var getDeviceListresponse = client.GetDeviceList(new GetDeviceListRequest()
                                                                                 {
                                                                                     UserName = _userName,
                                                                                     UserPassword = _userPassword,
                                                                                     SecurityToken = _securityToken
                                                                                 });
                            dgResult.ItemsSource = getDeviceListresponse.Devices;
                            lblStatus.Text = String.Format("Status: {0} with count: {1}", getDeviceListresponse.Message.Message, getDeviceListresponse.Count);
                            #endregion

                            break;
                        case "Get ReferenceChart List":

                            #region GetReferenceChartList

                            var getRefChartListresponse = client.GetReferenceChartList(new GetReferenceChartListRequest()
                            {
                                UserName = _userName,
                                UserPassword = _userPassword,
                                SecurityToken = _securityToken
                            });
                            dgResult.ItemsSource = getRefChartListresponse.ReferenceChartList;
                            lblStatus.Text = String.Format("Status: {0} with count: {1}", getRefChartListresponse.Message.Message, getRefChartListresponse.Count);

                            #endregion

                            break;
                        case "Get Profile":

                            #region GetProfile

                            var getProfileresponse = client.GetProfile(new GetProfileRequest()
                            {
                                UserName = _userName,
                                UserPassword = _userPassword,
                                SecurityToken = _securityToken,
                                ProjectId = Convert.ToInt32(txtProjectId.Text),
                                ProfileType =
                                    (EProfileType)Enum.Parse(typeof(EProfileType), cmbProfileType.SelectedValue.ToString())
                            });
                            dgResult.ItemsSource = new List<Profile>() { getProfileresponse.Profile };
                            lblStatus.Text = String.Format("Status: {0} with count: {1}", getProfileresponse.Message.Message, getProfileresponse.Count);
                            #endregion

                            break;
                        case "Get Project Information":

                            #region GetProjectInformation

                            var getProjInformationresponse = client.GetProjectInformation(new GetProjectInformationRequest()
                            {
                                UserName = _userName,
                                UserPassword = _userPassword,
                                SecurityToken = _securityToken,
                                ProjectId = Convert.ToInt32(txtProjectId.Text)
                            });
                        
                            dgResult.ItemsSource = new List<ProjectInformation>() { getProjInformationresponse.ProjectInformation };
                            lblStatus.Text = String.Format("Status: {0} with count: {1}", getProjInformationresponse.Message.Message, getProjInformationresponse.Count);
                            #endregion

                            break;
                        case "Store Temp Project Information":

                            #region GetProjectInformation

                            var storeTempProjectInformationresponse =
                                client.StoreTempProjectInformation(new StoreTempProjectInformationRequest()
                                {
                                    UserName = _userName,
                                    UserPassword = _userPassword,
                                    SecurityToken = _securityToken,
                                    ProjectInfoId =
                                        string.IsNullOrEmpty(txtProjectTempId.Text)
                                            ? Guid.Empty
                                            : Guid.Parse(txtProjectTempId.Text.Trim()),
                                    ProjectName = txtProjectName.Text,
                                    Media = txtMedia.Text,
                                    Colorant = txtColorant.Text,
                                    RipSettings = txtRipSettings.Text
                                });
                            dgResult.ItemsSource = new List<string>() { storeTempProjectInformationresponse.ProjectInfoId.ToString() };
                            txtProjectTempId.Text = storeTempProjectInformationresponse.ProjectInfoId.ToString();
                            lblStatus.Text = String.Format("Status: {0} with count: {1}", storeTempProjectInformationresponse.Message.Message, storeTempProjectInformationresponse.Count);
                            #endregion

                            break;
                    }
                    if (SelectedMethod != "Store Temp Project Information")
                        dgResult.Columns[0].Visibility = Visibility.Hidden;
                }
                catch (FaultException<ServiceFault> exception)
                {
                    lblStatus.Text = String.Format("ErrorCode:{0}, ErrorMessage:{1}", exception.Detail.FaultErrorCode, exception.Detail.FaultMessage);
                }
            }
        }
                
        private void btnCallAllServices_Click(object sender, RoutedEventArgs e)
        {
            List<ServiceMessages> methodCallResult = new List<ServiceMessages>();
            _userName = txtUserName.Text;
            _userPassword = CryptHelper.EncryptString(txtPassword.Password, ConfigurationManager.AppSettings["XmfColorOrganizerEncryptionKey"]);
            _securityToken = ConfigurationManager.AppSettings["SecurityToken"];
            succeeded = 0;
            failed = 0;
            using (var client = new Sync4DServiceContractClient())
            {

                try
                {
                    #region GetDeviceList

                    var getDeviceListresponse = client.GetDeviceList(new GetDeviceListRequest()
                    {
                        UserName = _userName,
                        UserPassword = _userPassword,
                        SecurityToken = _securityToken
                    });
                    methodCallResult.Add(new ServiceMessages(){ Value = PrepareMessage("Get Device List", false, getDeviceListresponse.Count)});
                    //methodCallResult.Add(string.Format("Get Device List - {0} - {1}", getDeviceListresponse.Message.Message, getDeviceListresponse.Count));
                    succeeded++;
                    #endregion
                }
                catch (FaultException<ServiceFault> exception)
                {
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareErrorMessage(exception, "Get Device List") });
                }

                try
                {
                    #region GetReferenceChartList

                    var getRefChartListresponse = client.GetReferenceChartList(new GetReferenceChartListRequest()
                    {
                        UserName = _userName,
                        UserPassword = _userPassword,
                        SecurityToken = _securityToken
                    });
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareMessage("Get ReferenceChart List", false, getRefChartListresponse.Count) });
                    //methodCallResult.Add(string.Format("Get ReferenceChart List - {0} - {1} ", getRefChartListresponse.Message.Message, getRefChartListresponse.Count));
                    succeeded++;
                    #endregion
                }
                catch (FaultException<ServiceFault> exception)
                {
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareErrorMessage(exception, "Get ReferenceChart List") });
                }

                try
                {
                    #region GetProfile

                    var getProfileresponse = client.GetProfile(new GetProfileRequest()
                    {
                        UserName = _userName,
                        UserPassword = _userPassword,
                        SecurityToken = _securityToken,
                        ProjectId = Convert.ToInt32(string.IsNullOrEmpty(txtProjectId.Text.Trim()) ? "0" : txtProjectId.Text.Trim()),
                        ProfileType =
                            (EProfileType)Enum.Parse(typeof(EProfileType), cmbProfileType.SelectedValue == null ? "0" : cmbProfileType.SelectedValue.ToString())
                    });
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareMessage("Get Profile", false, getProfileresponse.Count) });
                    //methodCallResult.Add(string.Format("Get Profile - {0} - {1}", getProfileresponse.Message.Message, getProfileresponse.Count));
                    succeeded++;
                    #endregion
                }
                catch (FaultException<ServiceFault> exception)
                {
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareErrorMessage(exception, "Get Profile") });
                }

                try
                {
                    #region GetProjectInformation

                    var getProjInformationresponse = client.GetProjectInformation(new GetProjectInformationRequest()
                    {
                        UserName = _userName,
                        UserPassword = _userPassword,
                        SecurityToken = _securityToken,
                        ProjectId = Convert.ToInt32(string.IsNullOrEmpty(txtProjectId.Text.Trim()) ? "0" : txtProjectId.Text.Trim())
                    });
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareMessage("Get Project Information", false, getProjInformationresponse.Count) });
                    //methodCallResult.Add(string.Format("Get Project Information - {0} - {1}", getProjInformationresponse.Message.Message, getProjInformationresponse.Count));
                    succeeded++;
                    #endregion
                }
                catch (FaultException<ServiceFault> exception)
                {
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareErrorMessage(exception, "Get Project Information") });
                }

                try
                {
                    #region GetProjectInformation

                    var storeTempProjectInformationresponse =
                        client.StoreTempProjectInformation(new StoreTempProjectInformationRequest()
                        {
                            UserName = _userName,
                            UserPassword = _userPassword,
                            SecurityToken = _securityToken,
                            ProjectInfoId =
                                string.IsNullOrEmpty(txtProjectTempId.Text)
                                    ? Guid.Empty
                                    : Guid.Parse(txtProjectTempId.Text.Trim()),
                            ProjectName = txtProjectName.Text,
                            Media = txtMedia.Text,
                            Colorant = txtColorant.Text,
                            RipSettings = txtRipSettings.Text
                        });
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareMessage("Store Temp Project Information", false, storeTempProjectInformationresponse.Count) });
                    //methodCallResult.Add(string.Format("Store Temp Project Information - {0} - {1}", storeTempProjectInformationresponse.Message.Message, storeTempProjectInformationresponse.Count));
                    succeeded++;
                    #endregion
                }
                catch (FaultException<ServiceFault> exception)
                {
                    methodCallResult.Add(new ServiceMessages() { Value = PrepareErrorMessage(exception, "Store Temp Project Information") });
                }



                dgResult.ItemsSource = methodCallResult;          
                lblStatus.Text = String.Format("  =========== Service Calls: {0} succeeded, {1} failed ============", succeeded, failed);
            }

        }

        private int succeeded = 0;
        private int failed = 0;

        private string PrepareMessage(string methodName, bool isError, long count, string message = "")
        {
            //MethodName - Status:{Succeeded/failed}, Count={count}, ErrorCode/Message={code/description}
            return string.Format("{0} - Status: {1}, Count: {2}{3}"
                , methodName
                , !isError ? "Succeeded" : "Failed"
                , count
                , string.IsNullOrEmpty(message) ? string.Empty : ", " + message);
        }
       
        private string PrepareErrorMessage(FaultException<ServiceFault> exception, string methodName)
        {
            string result= string.Empty;
            switch (exception.Detail.FaultErrorCode)
            {
                case ErrorCode.DataNotFound:
                    succeeded++;
                    result = PrepareMessage(methodName,false,0);
                    break;

                case ErrorCode.None:
                case ErrorCode.UserNotFound:
                case ErrorCode.InValidCredential:
                case ErrorCode.BadRequest:
                case ErrorCode.UnKnown:
                case ErrorCode.Internal:
                    failed++;
                    result = PrepareMessage(methodName, true, 0, string.Format("ErrorCode/Message: {0}/{1}", exception.Detail.FaultErrorCode, exception.Detail.FaultMessage));                   
                    break;
            }
            return result;
        }
    }

    internal class ServiceMessages
    {
        public string Value { get; set; }
    }
}
