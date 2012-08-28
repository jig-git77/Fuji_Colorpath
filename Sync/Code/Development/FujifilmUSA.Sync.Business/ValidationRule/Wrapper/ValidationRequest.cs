using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.ValidationRule.Wrapper
{
    public class ValidationRequest
    {
        private readonly LogActivity _logActivity;
        public ValidationRequest(LogActivity logActivity)
        {
            _logActivity = logActivity;
        }
        //Different properties that holds the values
        public LogActivity LogActivity
        {
            get
            {
                return _logActivity;
            }
        }

    }
}
