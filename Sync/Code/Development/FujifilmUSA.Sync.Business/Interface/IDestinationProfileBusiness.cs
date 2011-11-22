using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDestinationProfileBusiness : ITaskBase
    {
        Dictionary<int, string> GetDestinationProfileList();
    }
    
    
}
