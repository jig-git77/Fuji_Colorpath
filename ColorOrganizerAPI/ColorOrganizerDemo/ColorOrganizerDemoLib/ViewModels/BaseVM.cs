using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class BaseVM : ViewModelBase
    {
        public BaseVM()
        {
            NextButtonCommand = new DelegateCommand();
            PreviousButtonCommand = new DelegateCommand();
            CurrentStatus = Visibility.Collapsed;
        }

        public DesignerViewModel Parent { get; set; }
        public DelegateCommand NextButtonCommand { get; set; }
        public DelegateCommand PreviousButtonCommand { get; set; }

        public Common.PassCommandToParent PassToParent { get; set; }
        
        private Visibility _currentStatus;
        public Visibility CurrentStatus
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                _currentStatus = value;
                RaisePropertyChanged("CurrentStatus");
            }
        }


    }
}
