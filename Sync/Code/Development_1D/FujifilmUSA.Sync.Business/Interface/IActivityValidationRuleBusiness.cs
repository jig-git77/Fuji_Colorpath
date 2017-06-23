using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IActivityValidationRuleBusiness : ITaskBase
    {
        List<ActivityValidation> GetActivityValidationRules();
        List<ActivityValidation> GetActivityValidationRules(LogActivityType activityType);
        int UpdateActivityValidationRules(List<int> validationRuleIds, LogActivityType activityTypeId);
    }
}
