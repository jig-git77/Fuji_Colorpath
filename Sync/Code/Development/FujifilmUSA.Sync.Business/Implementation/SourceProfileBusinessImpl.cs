using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SourceProfileBusinessImpl : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public Dictionary<int, string> GetSourceProfileList()
        {
            var dstSourceProfileList = new Dictionary<int, string>();
            var dsSourceProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSourceProfileList]");
            if (dsSourceProfileList != null && dsSourceProfileList.Tables.Count > 0)
            {
                dstSourceProfileList = (from sourceProfile in dsSourceProfileList.Tables[0].AsEnumerable()
                                        select new
                                                   {
                                                       Value = DataReader.ReadInt(sourceProfile, "SourceProfileID"),
                                                       Text = DataReader.ReadString(sourceProfile, "SourceProfileName"),
                                                   }).ToDictionary(sourceProfile => sourceProfile.Value, sourceProfile => sourceProfile.Text);
            }
            return dstSourceProfileList;
        }

        #endregion
    }
}
