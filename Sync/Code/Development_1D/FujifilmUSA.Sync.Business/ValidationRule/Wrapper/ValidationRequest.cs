using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.ValidationRule.Wrapper
{
    public class ValidationRequest
    {
        public LogActivity LogActivity
        {
            get;
            private set;
        }
        public User CurrentUser
        {
            get;
            private set;
        }

        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }

        public ValidationRequest(LogActivity logActivity,User currentUser)
        {
            LogActivity = logActivity;
            CurrentUser = currentUser;
        }
    }
}
