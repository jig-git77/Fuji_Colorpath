using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.ValidationRule.Wrapper;

namespace FujifilmUSA.Sync.Business.ValidationRule.NotificationRules
{
    public class LoggedInValidationRule : BaseValidationRule
    {
        public override void Validate()
        {
            //Here comes business logic for processing rule 
            //System.IO.File.AppendAllText(@"d:\c.txt", "rule"); 

        }
    }
}
