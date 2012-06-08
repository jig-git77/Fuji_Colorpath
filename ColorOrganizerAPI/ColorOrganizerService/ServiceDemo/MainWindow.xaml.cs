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
        public string SelectedMethod { get; set; }
        private string _userName;
        private string _userPassword;
        private string _securityToken;

        public MainWindow()
        {
            Methods = new List<String>() {"Get Device List", "Get ReferenceChart List", "Get Profile", "Get Project Information", "Store Temp Project Information"};
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
                                                                                     //SecurityToken = _securityToken
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
                                ProjectId = Convert.ToInt32(txtProjectId.Text)
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
    }
}
