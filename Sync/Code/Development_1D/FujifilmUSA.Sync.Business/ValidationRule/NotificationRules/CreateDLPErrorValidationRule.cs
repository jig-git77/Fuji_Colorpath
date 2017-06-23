using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.ValidationRule.NotificationRules
{
    public class CreateDLPErrorValidationRule : BaseValidationRule
    {
        public override void Validate()
        {
            string body = string.Format(this.Request.EmailBody, this.Request.CurrentUser.LoginId, this.Request.LogActivity.IpAddress, this.Request.LogActivity.ActivityDate);
            LogManager.SendActivityValidationRuleEmail(this.Request.EmailSubject, body);            
        }
    }
}
