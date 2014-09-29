using System;

namespace FujifilmUSA.Sync.Entity
{
    public class ErrorArgs : EventArgs
    {
        public ErrorArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }
    }
}
