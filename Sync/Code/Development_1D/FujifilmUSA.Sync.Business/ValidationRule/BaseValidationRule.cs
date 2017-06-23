using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.ValidationRule.Wrapper;

namespace FujifilmUSA.Sync.Business.ValidationRule
{
    public abstract class BaseValidationRule
    {
        public string Name { get; set; }
        public string Procedure { get; set; }
        public ValidationRequest Request { get; set; }

        public virtual void Validate()
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    //Do Validation Process
                }, new BusinessHandlerRequest(null, null, string.Empty));
        }
    }
}
