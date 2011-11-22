using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ProjectBussinessImpl : IProjectBussiness
    {
        #region IProjectBussiness Members

        public List<Project> GetProjectListBySyncDeviceId(int syncDeviceId)
        {
            var objProjectList = new List<Project>();
            //========== Get data from data access=================//
            var dsProjectList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetAllSyncProjectBySyncDeviceID]", new object[] { syncDeviceId });

            if (dsProjectList != null && dsProjectList.Tables.Count > 0 && dsProjectList.Tables[0].Rows.Count > 0)
            {
                objProjectList = dsProjectList.Tables[0].AsEnumerable().
                    Select(x => new Project()
                                    {
                                        ProjectID = DsReader.ReadInt(x,"ProjectID"),
                                        ProjectName = DsReader.ReadString(x,",ProjectName"),
                                        CreatedDate = DsReader.ReadDate(x,"CreatedDate"),
                                        SyncDeviceID = DsReader.ReadInt(x,"SyncDeviceID"),
                                        ΔEType = DsReader.ReadInt(x,"ΔEType"),
                                        DLPID = DsReader.ReadInt(x,"DLPID"),
                                        IsUsed = DsReader.ReadBoolean(x,"IsUsed"),
                                        ReTargetID = DsReader.ReadInt(x,"ReTargetID"),
                                        ProjectTypeID = DsReader.ReadInt(x,"ProjectTypeID"),
                                        OutputTypeID = DsReader.ReadInt(x,"OutPutTypeID"),
                                        ProjectStatus = (Enums.CPSyncRunStatus)DsReader.ReadInt(x,"ProjStatus")
                                    }).ToList();
            }
            return objProjectList;
        }

        #endregion
    }
}
