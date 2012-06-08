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

namespace ColorOrganizerDemoLib.ViewModels
{
    public class StartICCProfileViewModel : BaseVM
    {
        public StartICCProfileViewModel()
        {
            IsNewIccProfile = true;
            SelectedIccProfile = new IccProfile();
            IccProfiles = new ObservableCollection<IccProfile>()
                              {
                                  new IccProfile() {ProjectId = 1, IccProfileName = "Profile 1"},
                                  new IccProfile() {ProjectId = 2, IccProfileName = "Profile 2"},
                                  new IccProfile() {ProjectId = 3, IccProfileName = "Profile 3"}
                              };
            NextButtonCommand.Executed += (sender, e) => PassToParent(EControl.ChartPrint);
            PreviousButtonCommand.Executed += (sender, e) => PassToParent(EControl.SelectDevice);
        }

        public bool IsNextButtonEnable
        {
            get { return (SelectedIccProfile != null && !string.IsNullOrEmpty(SelectedIccProfile.IccProfileName)); }
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

        public ObservableCollection<IccProfile> IccProfiles { get; set; }

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
    }
}
