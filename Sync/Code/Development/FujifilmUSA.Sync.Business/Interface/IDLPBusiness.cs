using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IDLPBusiness : ITaskBase
    {
        List<DLP> GetDLPList();
    }
}
