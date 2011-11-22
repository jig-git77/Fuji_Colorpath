using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ICompanyBusiness : ITaskBase
    {
        Dictionary<int, string> GetCompanyList();
    }
}
