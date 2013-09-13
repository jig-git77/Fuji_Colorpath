using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ActivityValidation
    {
        public int ActivityValidationId { get; set; }
        public LogActivityType ActivityType { get; set; }
        public ValidationRule ValidationRule { get; set; }
        public bool IsRuleOn { get; set; }
    }
}
