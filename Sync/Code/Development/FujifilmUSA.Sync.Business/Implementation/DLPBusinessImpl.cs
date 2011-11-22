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
    public class DLPBusinessImpl :IDLPBusiness
    {
        #region IDLPBusiness Members

        public List<DLP> GetDLPList()
        {
            try
            {
                List<DLP> ObjDLPList = new List<DLP>();
                //========== Get data from data access=================//
                DataSet DSDLPList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetDLPList]");

                if (DSDLPList != null && DSDLPList.Tables.Count > 0 && DSDLPList.Tables[0].Rows.Count > 0)
                {
                    ObjDLPList = DSDLPList.Tables[0].AsEnumerable().
                    Select(x => new DLP()
                    {

                        DLPID = DsReader.ReadInt(x,"DLPID"),
                        DLPName = DsReader.ReadString(x,"DLPName"),
                        Profile = DsReader.ReadString(x,"Profile"),
                        IsUse = DsReader.ReadBoolean(x,"IsUse"),
                        DLPSourceProfile = new SourceProfile{ SourceProfileID = DsReader.ReadInt(x,"SourceProfileID"), SourceProfileName = DsReader.ReadString(x,"SourceProfileName")},
                        DLPDestinationProfile = new DestinationProfile{ DestinationProfileID = DsReader.ReadInt(x,"DestinationProfileID"), DestinationProfileName = DsReader.ReadString(x,"DestinationProfileName")}                        
                        
                    }).ToList();
                }
                return ObjDLPList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
