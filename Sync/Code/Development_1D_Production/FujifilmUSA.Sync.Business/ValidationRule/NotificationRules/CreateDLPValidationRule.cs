using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.ValidationRule.NotificationRules
{
    public class CreateDLPValidationRule : BaseValidationRule
    {
        public override void Validate()
        {
            LogActivity logActivity = this.Request.LogActivity;
            logActivity.ActivityDateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            logActivity.ActivityDateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);


            List<LogActivity> logActivityList = LogActivityManager.GetLogActivity(logActivity);
            if (logActivityList.Count >= 5)
            {
                //Send alert here
                string body = string.Format(this.Request.EmailBody, 4, this.Request.CurrentUser.LoginId, this.Request.LogActivity.IpAddress, this.Request.LogActivity.ActivityDate);
                LogManager.SendActivityValidationRuleEmail(this.Request.EmailSubject, body);
            }            
        }
    }
}
