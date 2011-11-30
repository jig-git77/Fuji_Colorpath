using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISourceProfileBusiness : ITaskBase
    {
        Dictionary<int, string> GetSourceProfileList();
    }
}

