using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterActivityValidationRule
    {
        private readonly IViewActivityValidationRule _viewActivityValidationRule;

        public PresenterActivityValidationRule(IViewActivityValidationRule viewActivityValidationRule)
        {
            _viewActivityValidationRule = viewActivityValidationRule;
        }

        public void GetActivityValidationRules()
        {
            var activityValidationDataBusiness = BusinessFactory<IActivityValidationRuleBusiness>.CreateObject();
            activityValidationDataBusiness.ErrorOccured += _viewActivityValidationRule.ErrorOccured;
            _viewActivityValidationRule.ActivityValidationRule = activityValidationDataBusiness.GetActivityValidationRules(_viewActivityValidationRule.ActivityType);
        }

        public void UpdateActivityValidationRules()
        {
            var activityValidationDataBusiness = BusinessFactory<IActivityValidationRuleBusiness>.CreateObject();
            activityValidationDataBusiness.ErrorOccured += _viewActivityValidationRule.ErrorOccured;
            List<int> validationRuleIds = _viewActivityValidationRule.ActivityValidationRule.Select(c => c.ValidationRule.ValidationRuleId).ToList();            
            activityValidationDataBusiness.UpdateActivityValidationRules(validationRuleIds,_viewActivityValidationRule.ActivityType);
        }

    }
}
