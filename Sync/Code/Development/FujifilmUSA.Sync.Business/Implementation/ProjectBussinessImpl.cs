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
                                        ProjectId = DsReader.ReadInt(x,"ProjectID"),
                                        ProjectName = DsReader.ReadString(x,",ProjectName"),
                                        CreatedDate = DsReader.ReadDate(x,"CreatedDate"),
                                        SyncDeviceId = DsReader.ReadInt(x,"SyncDeviceID"),
                                        ΔEType = DsReader.ReadInt(x,"ΔEType"),
                                        DLPId = DsReader.ReadInt(x,"DLPID"),
                                        IsUsed = DsReader.ReadBoolean(x,"IsUsed"),
                                        ReTargetId = DsReader.ReadInt(x,"ReTargetID"),
                                        ProjectTypeId = DsReader.ReadInt(x,"ProjectTypeID"),
                                        OutputTypeId = DsReader.ReadInt(x,"OutPutTypeID"),
                                        ProjectStatus = (Enums.CPSyncRunStatus)DsReader.ReadInt(x,"ProjStatus")
                                    }).ToList();
            }
            return objProjectList;
        }

        #endregion
    }
}
