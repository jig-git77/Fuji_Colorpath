using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<Project> GetProjectListBySyncDeviceID(int SyncDeviceID)
        {
            try
            {
                List<Project> ObjProjectList = new List<Project>();
                //========== Get data from data access=================//
                DataSet DSProjectList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetAllSyncProjectBySyncDeviceID]", new object[] { SyncDeviceID });

                if (DSProjectList != null && DSProjectList.Tables.Count > 0 && DSProjectList.Tables[0].Rows.Count > 0)
                {
                    ObjProjectList = DSProjectList.Tables[0].AsEnumerable().
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
                return ObjProjectList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
