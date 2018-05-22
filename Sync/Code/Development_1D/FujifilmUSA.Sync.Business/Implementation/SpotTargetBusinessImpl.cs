using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity.CPSpot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SpotTargetBusinessImpl : ISpotTargetBusiness
    {
        public List<SpotTarget> GetSpotTargetList(int UserID, int CurrentUserID)
        {
            var objSpotTargets = new List<SpotTarget>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSpots = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "[CPSpot].[GetSpotTargets]",
                            new object[]
                                    {
                                        UserID,CurrentUserID
                                    });
                    if (dsSpots != null && dsSpots.Tables.Count > 0 && dsSpots.Tables[0].Rows.Count > 0)
                    {
                        objSpotTargets = dsSpots.Tables[0].AsEnumerable().
                            Select(x =>
                                   new SpotTarget()
                                   {
                                       SpotTargetID = DataReader.ReadInt(x, "SpotTargetID"),
                                       SpotTargetName = DataReader.ReadString(x, "SpotTargetName"),
                                       Company = new Entity.Company()
                                       {
                                           CompanyId= DataReader.ReadInt(x, "CompanyID"),
                                           CompanyName= DataReader.ReadString(x, "CompanyName"),
                                       },
                                       IsUsed = DataReader.ReadBoolean(x, "IsUsed")                                       
                                   }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotTargetList"));
            return objSpotTargets;
        }


        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
