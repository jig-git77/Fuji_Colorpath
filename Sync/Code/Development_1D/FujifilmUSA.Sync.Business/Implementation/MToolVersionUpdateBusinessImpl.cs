using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MToolVersionUpdateBusinessImpl : IMToolVersionListBusiness
    {
        #region  IMToolVersionListBusiness Members

        public int InsertMToolUpdateVersion(MToolVersionUpdateList mToolVersion)
        {
            int returnValue = 0;
            Common.
                ImplementBusinessLogic(
                    () =>
                    {
                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CP].[InsertMToolVersionList]",
                                new object[]
                                        {
                                            mToolVersion.OSPlatformID,
                                            mToolVersion.LatestVersion,
                                            mToolVersion.MinReqVersion,
                                            mToolVersion.URLlocation,
                                            mToolVersion.UpdateInfo,
                                            mToolVersion.IsUsed

                                        });
                    },
                    new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertMToolUpdateVersion"));
            return returnValue;
        }

        public int UpdateMToolUpdateVersion(MToolVersionUpdateList mToolVersion)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateMToolVersionList]",
                            new object[]
                                    {
                                            mToolVersion.OSPlatformDetailsID,
                                            mToolVersion.OSPlatformID,
                                            mToolVersion.LatestVersion,
                                            mToolVersion.MinReqVersion,
                                            mToolVersion.URLlocation,
                                            mToolVersion.UpdateInfo,
                                            mToolVersion.IsUsed
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Duplicate_UpdateMToolUpdateVersion"));
            return returnValue;
        }

        public List<MToolVersionUpdateList> GetMToolUpdateVersionList()
        {
            var mtoolversions = new List<MToolVersionUpdateList>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMToolList = DataAccessFactory.Instance.GetDsResult("[CP].[GetMToolVersionList]");
                    if (dsMToolList != null && dsMToolList.Tables.Count > 0 && dsMToolList.Tables[0].Rows.Count > 0)
                    {
                        mtoolversions = dsMToolList.Tables[0].AsEnumerable().
                            Select(x => new MToolVersionUpdateList()
                            {
                                OSPlatformDetailsID = DataReader.ReadShort(x, "OSPlatformDetailsID"),
                                OSPlatformID = DataReader.ReadShort(x, "OSPlatformID"),
                                OSPlatformName = DataReader.ReadString(x, "OSPlatformName"),
                                LatestVersion = DataReader.ReadString(x, "LatestVersion"),
                                MinReqVersion = DataReader.ReadString(x, "MinReqVersion"),
                                URLlocation = DataReader.ReadString(x, "URLlocation"),
                                UpdateInfo = DataReader.ReadString(x, "UpdateInfo"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMToolUpdateVersionList"));
            return mtoolversions;
        }
        public MToolVersionUpdateList GetMToolVersionSelect(short oSPlatformDetailsID)
        {
            var mtoolversions = new MToolVersionUpdateList();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMToolList = DataAccessFactory.Instance.GetDsResult("[CP].[GetMToolVersionSelect]", new object[] { oSPlatformDetailsID});
                    if (dsMToolList != null && dsMToolList.Tables.Count > 0 && dsMToolList.Tables[0].Rows.Count > 0)
                    {
                        mtoolversions = dsMToolList.Tables[0].AsEnumerable().
                            Select(x => new MToolVersionUpdateList()
                            {
                                OSPlatformDetailsID = DataReader.ReadShort(x, "OSPlatformDetailsID"),
                                OSPlatformID = DataReader.ReadShort(x, "OSPlatformID"),
                                OSPlatformName = DataReader.ReadString(x, "OSPlatformName"),
                                LatestVersion = DataReader.ReadString(x, "LatestVersion"),
                                MinReqVersion = DataReader.ReadString(x, "MinReqVersion"),
                                URLlocation = DataReader.ReadString(x, "URLlocation"),
                                UpdateInfo = DataReader.ReadString(x, "UpdateInfo"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                            }).ToList().First();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMToolVersionSelect"));
            return mtoolversions;
        }
        public Dictionary<int, string> GetMToolOSPlatformList()
        {
            var dstOSPlatformList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsOSPlatform = DataAccessFactory.Instance.GetDsResult("[CP].[GetMToolOSPlatform]");
                    if (dsOSPlatform != null && dsOSPlatform.Tables.Count > 0)
                    {
                        dstOSPlatformList = (dsOSPlatform.Tables[0].AsEnumerable().Select(
                            osplatform => new
                            {
                                Value = DataReader.ReadInt(osplatform, "OSPlatformID"),
                                Text = DataReader.ReadString(osplatform, "OSPlatformName")
                            })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMToolOSPlatform"));
            return dstOSPlatformList;
        }
        public bool DeleteMToolVersion(int oSPlatformDetailsID)
        {
            Common.ImplementBusinessLogic(
                () =>
                DataAccessFactory.
                    Instance.
                    ExecuteProcedure(
                        "[CP].[DeleteMToolVersion]",
                        new object[]
                            {
                                oSPlatformDetailsID
                            }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteMToolVersion"));
            return true;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
