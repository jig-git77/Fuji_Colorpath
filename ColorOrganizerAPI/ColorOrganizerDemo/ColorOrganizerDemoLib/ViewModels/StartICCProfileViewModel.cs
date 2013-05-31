using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using ColorOrganizerDemoLib.Models;
using System.Collections.ObjectModel;
using ColorOrganizerDemoLib.Sync4DService;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class StartICCProfileViewModel : BaseVM
    {
        public StartICCProfileViewModel()
        {
            IsNewIccProfile = true;
            SelectedIccProfile = new IccProfile();
            NextButtonCommand.Executed += (sender, e) => PassToParent(EControl.ChartPrint);
            PreviousButtonCommand.Executed += (sender, e) => PassToParent(EControl.SelectDevice);

            LastAnalyzeButtonCommand = new DelegateCommand();
            LastAnalyzeButtonCommand.Executed += (sender, e) => RedirectToLastAnalyse();

            LastValidationAnalyzeButtonCommand = new DelegateCommand();
            LastValidationAnalyzeButtonCommand.Executed += (sender, e) => RedirectToLastValidationAnalyse();

            ValidationMeasureCommand = new DelegateCommand();
            ValidationMeasureCommand.Executed += (sender, e) => RedirectToValidation();

        }

        private void RedirectToValidation()
        {
            //Store temporary validation information here
            //Redirect to website page
            if (!IsNewIccProfile)
                InsertValidationInformationAndRedirect();
        }

        private void RedirectToLastAnalyse()
        {
            this.Parent.WebHost.RedirectUrl =
                            String.Format(
                                "{0}/COProcessRequest.ashx?action={1}&uid={2}&pwd={3}&projectid={4}&deviceid={5}",
                                Common.FujiWebSite,
                                "viewLastAnalyze",
                                Common.UserName,
                                Common.GetEncryptPassword(),
                                this.Parent.StartIccProfile.SelectedIccProfile.ProjectId,
                                this.Parent.SelectDevice.SelectedDevice.DeviceId);

            PassToParent(EControl.WebHost);
        }

        private void RedirectToLastValidationAnalyse()
        {
            this.Parent.WebHost.RedirectUrl =
                            String.Format(
                                "{0}/COProcessRequest.ashx?action={1}&uid={2}&pwd={3}&projectid={4}&deviceid={5}",
                                Common.FujiWebSite,
                                "viewLastValidationAnalyze",
                                Common.UserName,
                                Common.GetEncryptPassword(),
                                this.Parent.StartIccProfile.SelectedIccProfile.ProjectId,
                                this.Parent.SelectDevice.SelectedDevice.DeviceId);

            PassToParent(EControl.WebHost);
        }
        

        private void InsertValidationInformationAndRedirect()
        {
            var client = new Sync4DServiceContractClient("BasicHttpBinding_ISync4DServiceContract");
            client.StoreTempValidationInformationCompleted += delegate(object sender, StoreTempValidationInformationCompletedEventArgs e)
            {
                if (e.Error == null)
                {
                    if (e.Result != null)
                    {
                        Common.ValidationInfoId = e.Result.ValidationInfoId;

                        this.Parent.WebHost.RedirectUrl =
                           String.Format(
                               "{0}/COProcessRequest.ashx?action={1}&uid={2}&pwd={3}&validationinfoid={4}&deviceid={5}",
                               Common.FujiWebSite,
                               "validation",
                               Common.UserName,
                               Common.GetEncryptPassword(),
                               Common.ValidationInfoId,
                                this.Parent.SelectDevice.SelectedDevice.DeviceId);

                        PassToParent(EControl.WebHost);
                    }
                }
                else
                {
                    //e.Error
                }
            };

            var domainUrl = string.Empty;
            if (System.Windows.Browser.HtmlPage.Document != null)
            {
                var urlParts = System.Windows.Browser.HtmlPage.Document.DocumentUri.AbsoluteUri.Split('/');
                domainUrl = string.Join("/", urlParts, 0, urlParts.Length - 1);
            }

            client.StoreTempValidationInformationAsync(new StoreTempValidationInformationRequest()
            {
                SecurityToken = Common.SecurityToken,
                UserName = Common.UserName,
                UserPassword = Common.GetEncryptPassword(),
                JobName = JobName,
                Customer = Customer,
                JDFJobId = JDFJobId,
                PageOrSurface = PageOrSurface,
                ProjectId = this.Parent.StartIccProfile.SelectedIccProfile.ProjectId,
                SilverlightPostUrl = domainUrl
            });
        }

        public DelegateCommand ValidationMeasureCommand { get; set; }
        public DelegateCommand LastAnalyzeButtonCommand { get; set; }
        public DelegateCommand LastValidationAnalyzeButtonCommand { get; set; }

        public bool IsNextButtonEnable
        {
            get { return (SelectedIccProfile != null && (!string.IsNullOrEmpty(SelectedIccProfile.IccProfileName) || SelectedIccProfile.ProjectId > 0)); }
        }

        private IccProfile _selectedIccProfile;
        public IccProfile SelectedIccProfile
        {
            get { return _selectedIccProfile; }
            set
            {
                _selectedIccProfile = value;
                RaisePropertyChanged("SelectedIccProfile");
                RaisePropertyChanged("IsNextButtonEnable");
            }
        }

        private string _existingProjectId;
        public string ExistingProjectId
        {
            get { return _existingProjectId; }
            set
            {
                _existingProjectId = value;
                SelectedIccProfile = new IccProfile()
                                         {
                                             ProjectId = Convert.ToInt32(value)
                                         };
            }
        }

        private string _newProfileName;
        public string NewProfileName
        {
            get { return _newProfileName; }
            set
            {
                _newProfileName = value;
                SelectedIccProfile = new IccProfile()
                                         {
                                             IccProfileName = value
                                         };
            }
        }

        private bool _isNewIccProfile;
        public bool IsNewIccProfile
        {
            get
            {
                return _isNewIccProfile;
            }
            set
            {
                _isNewIccProfile = value;
                RaisePropertyChanged("IsNewIccProfile");
            }
        }

        private string _jobName;
        public string JobName
        {
            get
            {
                return _jobName;
            }
            set
            {
                _jobName = value;
                RaisePropertyChanged("JobName");
            }
        }

        private string _customer;
        public string Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                RaisePropertyChanged("Customer");
            }
        }

        private string _pageOrSurface;
        public string PageOrSurface
        {
            get
            {
                return _pageOrSurface;
            }
            set
            {
                _pageOrSurface = value;
                RaisePropertyChanged("PageOrSurface");
            }
        }

        private string _jdfJobId;
        public string JDFJobId
        {
            get
            {
                return _jdfJobId;
            }
            set
            {
                _jdfJobId = value;
                RaisePropertyChanged("JDFJobId");
            }
        }
    }
}
