using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SourceProfileBusinessImpl : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public Dictionary<int, string> GetSourceProfileList()
        {
           try
            {
                Dictionary<int, string> DstSourceProfileList = new Dictionary<int, string>();
                DataSet DSSourceProfileList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetSourceProfileList]");
                if (DSSourceProfileList != null && DSSourceProfileList.Tables.Count > 0)
                {
                    DstSourceProfileList = (from SourceProfile in DSSourceProfileList.Tables[0].AsEnumerable()
                                      select new
                                      {
                                          Value = DsReader.ReadInt(SourceProfile,"SourceProfileID"),
                                          Text = DsReader.ReadString(SourceProfile,"SourceProfileName")
                                      }).ToDictionary(SourceProfile => SourceProfile.Value, SourceProfile => SourceProfile.Text);
                }
                return DstSourceProfileList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
