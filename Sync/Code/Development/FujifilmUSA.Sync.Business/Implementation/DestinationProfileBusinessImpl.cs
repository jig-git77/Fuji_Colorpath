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
    public class DestinationProfileBusinessImpl : IDestinationProfileBusiness
    {

        #region IDestinationProfileBusiness Members

        public Dictionary<int, string> GetDestinationProfileList()
        {
            try
            {
                Dictionary<int, string> DstDestinationProfileList = new Dictionary<int, string>();
                DataSet DSDestinationProfileList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetDestinationProfileList]");
                if (DSDestinationProfileList != null && DSDestinationProfileList.Tables.Count > 0)
                {
                    DstDestinationProfileList = (from DestinationProfile in DSDestinationProfileList.Tables[0].AsEnumerable()
                                            select new
                                            {
                                                Value = DsReader.ReadInt(DestinationProfile, "DestinationProfileID"),
                                                Text = DsReader.ReadString(DestinationProfile, "DestinationProfileName")
                                            }).ToDictionary(DestinationProfile => DestinationProfile.Value, DestinationProfile => DestinationProfile.Text);
                }
                return DstDestinationProfileList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
