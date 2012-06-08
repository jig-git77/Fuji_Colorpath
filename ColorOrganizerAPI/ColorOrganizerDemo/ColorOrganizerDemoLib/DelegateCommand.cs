using System;
using System.Windows.Input;

namespace ColorOrganizerDemoLib
{
    public delegate void DelegateCommandExecutedEventHandler(Object sender, DelegateCommandExecutedEventArgs e);

    public class DelegateCommandExecutedEventArgs : EventArgs
    {
        public DelegateCommand Command { get; set; }
        public Object Parameter { get; set; }

        public DelegateCommandExecutedEventArgs(DelegateCommand command, Object parameter)
        {
            Command = command;
            Parameter = parameter;
        }
    }

    public class DelegateCommand : ICommand
    {

        private Boolean _canExecuteCore;
        public Boolean CanExecuteCore
        {
            get { return _canExecuteCore; }
            set { _canExecuteCore = value; OnCanExecuteChanged(); }
        }

        public event EventHandler CanExecuteChanged;

        protected void OnCanExecuteChanged()
        {
            EventHandler temp = CanExecuteChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public event DelegateCommandExecutedEventHandler Executed;

        protected void OnExecuted(DelegateCommand command, Object parameter)
        {
            DelegateCommandExecutedEventHandler temp = Executed;
            if (temp != null)
            {
                temp(this, new DelegateCommandExecutedEventArgs(command, parameter));
            }
        }

        public DelegateCommand()
        {
            CanExecuteCore = true;
        }

        public Boolean CanExecute(object parameter)
        {
            return CanExecuteCore;
        }

        public void Execute(object parameter)
        {
            OnExecuted(this, parameter);
        }
    }
}
