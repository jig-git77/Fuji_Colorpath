using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IApplicationConfigurationBusiness : ITaskBase
    {
        List<Entity.ApplicationConfiguration> GetApplicationConfiguration();
        int UpdateApplicationConfiguration(List<Entity.ApplicationConfiguration> applicationConfiguration);
    }
}
