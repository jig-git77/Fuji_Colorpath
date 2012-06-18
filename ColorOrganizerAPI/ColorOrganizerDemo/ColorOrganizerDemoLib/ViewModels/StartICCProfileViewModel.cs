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
        }

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
    }
}
