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
    public class DLPBusinessImpl :IDLPBusiness
    {
        #region IDLPBusiness Members

        public List<DLP> GetDLPList()
        {
            var objDlpList = new List<DLP>();
            //========== Get data from data access=================//
            var dsdlpList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetDLPList]");

            if (dsdlpList != null && dsdlpList.Tables.Count > 0 && dsdlpList.Tables[0].Rows.Count > 0)
            {
                objDlpList = dsdlpList.Tables[0].AsEnumerable().
                    Select(x => new DLP()
                                    {

                                        DLPId = DsReader.ReadInt(x,"DLPID"),
                                        DLPName = DsReader.ReadString(x,"DLPName"),
                                        Profile = DsReader.ReadString(x,"Profile"),
                                        IsUse = DsReader.ReadBoolean(x,"IsUse"),
                                        DLPSourceProfile = new SourceProfile{ SourceProfileId = DsReader.ReadInt(x,"SourceProfileID"), SourceProfileName = DsReader.ReadString(x,"SourceProfileName")},
                                        DLPDestinationProfile = new DestinationProfile{ DestinationProfileId = DsReader.ReadInt(x,"DestinationProfileID"), DestinationProfileName = DsReader.ReadString(x,"DestinationProfileName")}                        
                        
                                    }).ToList();
            }
            return objDlpList;
        }

        #endregion
    }
}
