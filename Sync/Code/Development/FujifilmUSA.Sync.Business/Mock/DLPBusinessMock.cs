using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DLPBusinessMock : IDLPBusiness
    {
        #region IDLPBusiness Members

        public List<DLP> GetDLPList()
        {
            return new List<DLP>();
        }

        #endregion
    }
}
