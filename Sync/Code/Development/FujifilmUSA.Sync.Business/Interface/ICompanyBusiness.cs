using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ICompanyBusiness : ITaskBase
    {
        Dictionary<int, string> GetCompanyList();
    }
}
