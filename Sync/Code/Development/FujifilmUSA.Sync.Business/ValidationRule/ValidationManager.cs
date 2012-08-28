using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.ValidationRule.Wrapper;

namespace FujifilmUSA.Sync.Business.ValidationRule
{
    public class ValidationManager
    {
        public static void ValidateRule(LogActivity logActivity)
        {
            ValidationContainer.Instance().Process(new ValidationRequest(logActivity));            
        }
    }
}
