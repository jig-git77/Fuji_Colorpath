using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ApplicationConfiguration
    {
       public int ApplicationConfigurationId { get; set; }
       public string ConfigurationKey { get; set; }
       public string ConfigurationValue { get; set; }
    }
}
