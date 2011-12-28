using System;
using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;


namespace FujifilmUSA.Sync.Business.Implementation
{
    #region IProfilerOptionBusiness Members
    public class ProfilerOptionBusinessImpl : IProfilerOptionBusiness
    {
        public Dictionary<int, string> GetProflierOption()
        {
            var ProfilerOptionList = new Dictionary<int, string>();
            var dsProfilerOptionDetail = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetProfilerOption]");
            if (dsProfilerOptionDetail != null && dsProfilerOptionDetail.Tables.Count > 0)
            {
                ProfilerOptionList = (dsProfilerOptionDetail.Tables[0].AsEnumerable().Select(
                    ProfilerOption => new
                    {
                        Value = DataReader.ReadInt(ProfilerOption, "ProfilerOptionID"),
                        Text = DataReader.ReadString(ProfilerOption, "ProfilerOptionDesc")
                    })).ToDictionary(ProfilerOption => ProfilerOption.Value, ProfilerOption => ProfilerOption.Text);
            }
            return ProfilerOptionList;
        }
    }
    #endregion
}
