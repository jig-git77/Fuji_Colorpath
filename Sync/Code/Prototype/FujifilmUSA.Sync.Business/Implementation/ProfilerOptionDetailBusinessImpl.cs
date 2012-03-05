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
    public class ProfilerOptionDetailBusinessImpl : IProfilerOptionDetailBusiness
    {
        #region IProfilerOptionDetailBusiness Members

        public List<ProfilerOptionDetail> GetProfilerOptionDetails(int profileroptionId, ProfileType profilerType)
        {
            var profileroptionDetailList = new List<ProfilerOptionDetail>();
            Common.ImplementBusinessLogic(() =>
            {
                var dsprofileroptionDetail = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetProfilerOptionDetailByProfilerID]", new object[] { profileroptionId, profilerType });

                if (dsprofileroptionDetail != null && dsprofileroptionDetail.Tables.Count > 0 && dsprofileroptionDetail.Tables[0].Rows.Count > 0)
                {
                    profileroptionDetailList = dsprofileroptionDetail.Tables[0].AsEnumerable().Select(x => new ProfilerOptionDetail()
                    {
                        ProfilerOptionDetailId = DataReader.ReadInt(x, "ProfilerOptionDetailID"),
                        ProfilerOptionId = DataReader.ReadInt(x, "ProfilerOptionID"),
                        Key = DataReader.ReadString(x, "Key"),
                        Value = DataReader.ReadString(x, "Value"),
                        ProfilerType = (ProfileType)Enum.Parse(typeof(ProfileType), DataReader.ReadInt(x, "ProfilerType").ToString(), true)
                    }).ToList();
                }
            }, this, ErrorOccured);
            return profileroptionDetailList;

        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
