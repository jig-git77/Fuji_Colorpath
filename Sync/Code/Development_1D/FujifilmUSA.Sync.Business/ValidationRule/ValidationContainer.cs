using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.ValidationRule.Wrapper;
using FujifilmUSA.Sync.Business.Implementation;
using System.Web;
using FujifilmUSA.Sync.Business.Interface;
namespace FujifilmUSA.Sync.Business.ValidationRule
{
    delegate void DelegateProcess(List<BaseValidationRule> rules);

    internal class ValidationContainer : ITaskBase
    {
        private static ValidationContainer _instance;

        private readonly List<ActivityValidation> _ValidationRules;

        DelegateProcess ProcessRule;

        private ValidationContainer()
        {
            var validationRuleBusiness = BusinessFactory<IActivityValidationRuleBusiness>.CreateObject();
            _ValidationRules = validationRuleBusiness.GetActivityValidationRules();
            this.ErrorOccured += new ErrorHandler(ValidationContainer_ErrorOccured);
        }

        void ValidationContainer_ErrorOccured(object sender, ErrorArgs e)
        {
            //Do Nothing...
        }

        public static ValidationContainer Instance()
        {
            if (_instance == null)
            {
                _instance = new ValidationContainer();
            }
            return _instance;
        }

        internal void Process(ValidationRequest request)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    List<BaseValidationRule> _Rules = new List<BaseValidationRule>();

                    ProcessRule = DoProcess;

                    if (_ValidationRules.Where(x => x.ActivityType == request.LogActivity.ActivityType).Count() > 0)
                    {
                        _ValidationRules.
                            Where(x => x.ActivityType == request.LogActivity.ActivityType).
                            ToList().
                            ForEach(rule =>
                            {
                                var objRule = CreateObject(rule.ValidationRule.ClassName);
                                objRule.Request = request;
                                objRule.Request.EmailBody = rule.ValidationRule.EmailBodyTemplate;
                                objRule.Request.EmailSubject= rule.ValidationRule.EmailSubjectTemplate;
                                
                                if (objRule != null)
                                    _Rules.Add(objRule);
                            });
                    }
                    if (_Rules.Count > 0)
                        ProcessRule.BeginInvoke(_Rules, null, null);
                }, new BusinessHandlerRequest(this, ErrorOccured, string.Empty));
        }

        private void DoProcess(List<BaseValidationRule> rules)
        {
            rules.ForEach(r => r.Validate());
        }

        private BaseValidationRule CreateObject(string className)
        {
            var classObject = Activator.CreateInstance(Type.GetType(className));
            if (classObject != null)
                return (BaseValidationRule)classObject;
            return null;
        }

        public event ErrorHandler ErrorOccured;
    }
}
