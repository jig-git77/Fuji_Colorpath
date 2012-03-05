using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public class ErrorArgs : EventArgs
    {
        public ErrorArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }
    }

    public delegate void ErrorHandler(object sender, ErrorArgs e);

    public interface ITaskBase
    {
        event ErrorHandler ErrorOccured;
    }
}
