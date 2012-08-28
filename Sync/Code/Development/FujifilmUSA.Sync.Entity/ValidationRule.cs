using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ValidationRule
    {
       public int ValidationRuleId { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public string ClassName { get; set; }
       public ValidationRuleType Type { get; set; }
    }
}
