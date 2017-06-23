using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Xml.Linq;
using FujifilmUSA.Sync.Business.CustomExceptions;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ActivityValidationRuleBusinessImpl : IActivityValidationRuleBusiness
    {
        public List<ActivityValidation> GetActivityValidationRules()
        {
            var activityValidationRules = new List<ActivityValidation>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRules = DataAccessFactory.Instance.GetDsResult("[dbo].[GetActivityValidationRule]");
                    if (dsRules != null && dsRules.Tables.Count > 0 && dsRules.Tables[0].Rows.Count > 0)
                    {
                        activityValidationRules = dsRules.Tables[0].AsEnumerable().
                            Select(x => new ActivityValidation()
                            {
                                ActivityValidationId = DataReader.ReadInt(x, "ActivityValidationId"),
                                ActivityType = (LogActivityType)DataReader.ReadInt(x, "ActivityTypeId"),
                                ValidationRule = new Entity.ValidationRule()
                                {
                                    ClassName = DataReader.ReadString(x, "ClassName"),
                                    Description = DataReader.ReadString(x, "Description"),
                                    Name = DataReader.ReadString(x, "Name"),
                                    Type = (ValidationRuleType)DataReader.ReadInt(x, "ValidationRuleId"),
                                    ValidationRuleId = DataReader.ReadInt(x, "ValidationRuleId"),
                                    EmailBodyTemplate = DataReader.ReadString(x, "EmailBodyTemplate"),
                                    EmailSubjectTemplate = DataReader.ReadString(x, "EmailSubjectTemplate")
                                }
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetActivityValidationRules"));
            return activityValidationRules;
        }

        public List<ActivityValidation> GetActivityValidationRules(LogActivityType activityType)
        {
            var activityList = new List<ActivityValidation>();
            Common.ImplementBusinessLogic(() =>
                {
                    var dsRules = DataAccessFactory.Instance.GetDsResult("[dbo].[GetActivityValidationRuleByActivityTypeId]", new object[] { (int)activityType });
                    if (dsRules != null && dsRules.Tables.Count > 0 && dsRules.Tables[0].Rows.Count > 0)
                    {
                        activityList = dsRules.Tables[0].AsEnumerable().
                            Select(a => new ActivityValidation()
                            {
                                ActivityValidationId = DataReader.ReadInt(a, "ActivityValidationId"),
                                ActivityType = (LogActivityType)DataReader.ReadInt(a, "ActivityTypeId"),
                                IsRuleOn = DataReader.ReadBoolean(a, "IsRuleOn"),
                                ValidationRule = new Entity.ValidationRule()
                                {
                                    Name = DataReader.ReadString(a, "Name"),
                                    Description = DataReader.ReadString(a, "Description"),
                                    ValidationRuleId = DataReader.ReadInt(a, "ValidationRuleId"),                                    
                                    EmailBodyTemplate = DataReader.ReadString(a, "EmailBodyTemplate"),
                                    EmailSubjectTemplate = DataReader.ReadString(a, "EmailSubjectTemplate")
                                }
                            }).ToList();
                    }

                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetActivityValidationRules"));
            return activityList;
        }

        public int UpdateActivityValidationRules(List<int> validationRuleIds, LogActivityType activityTypeId)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
               () =>
               {
                   var validationRuleIdXML = new XElement("ActivityValidation",
                                                                 validationRuleIds.
                                                                     Select(x =>
                                                                            new XElement("ValidationRule",
                                                                                         new XAttribute("Id", System.Convert.ToInt32(x))
                                                                                )).ToList());

                   returnValue = DataAccessFactory.
                                   Instance.
                                   ExecuteProcedure(
                                       "[dbo].[UpdateActivityValidation]",
                                       new object[]
                                    {
                                       validationRuleIdXML.ToString(),
                                       (int)activityTypeId
                                    });
                   if (returnValue == -1)
                   {
                       //throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateDevice" };
                   }
               },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateActivityValidation"));
            return returnValue;
        }
        public event ErrorHandler ErrorOccured;
    }
}
