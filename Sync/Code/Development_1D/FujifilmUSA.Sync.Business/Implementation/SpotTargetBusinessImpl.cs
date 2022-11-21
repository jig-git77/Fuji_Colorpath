using FujifilmUSA.Sync.Business.CustomExceptions;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSpot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SpotTargetBusinessImpl : ISpotTargetBusiness
    {
        #region ISpotTargetBusiness Members
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
                                           CompanyId = DataReader.ReadInt(x, "CompanyID"),
                                           CompanyName = DataReader.ReadString(x, "CompanyName"),
                                       },
                                       IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                   }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotTargetList"));
            return objSpotTargets;
        }

        public int CreateSpotTarget(SpotTarget spotTarget)
        {
            var returnValue = 0;
            Common.ImplementBusinessLogic(
                 () =>
                 {
                     var spotLabData = new XElement("SpotTargetLABData",
                                                    spotTarget.spotTargetLab.
                                                    Select(x =>
                                                        new XElement("SpotTargetLAB",
                                                                 new XAttribute("SpotID", x.SpotID),
                                                                 new XAttribute("SpotName", x.SpotName),
                                                                 new XAttribute("Target_L", x.Target_L),
                                                                 new XAttribute("Target_a", x.Target_a),
                                                                 new XAttribute("Target_b", x.Target_b)
                                                    //new XAttribute("HTMLColor", x.HTMLColor)
                                                    )).ToList());

                     returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure("[CPSpot].[CreateSpotTarget]", new object[]
                                                                                {
                                                                                    spotTarget.Company.CompanyId,
                                                                                    spotTarget.SpotTargetName,
                                                                                    spotTarget.Description,
                                                                                    spotTarget.IsUsed,
                                                                                    spotTarget.spotTargetSetting.ChartName,
                                                                                    spotTarget.spotTargetSetting.NumOfRows,
                                                                                    spotTarget.spotTargetSetting.NumOfCols,
                                                                                    spotTarget.spotTargetSetting.ChartSizeX,
                                                                                    spotTarget.spotTargetSetting.ChartSizeY,
                                                                                    spotTarget.spotTargetSetting.PatchSizeX,
                                                                                    spotTarget.spotTargetSetting.PatchSizeY,
                                                                                    spotTarget.spotTargetSetting.FirstSample,
                                                                                    spotTarget.spotTargetSetting.ReadDirection,
                                                                                    spotTarget.spotTargetSetting.ReadMode,
                                                                                    spotTarget.spotTargetSetting.Pages,
                                                                                    spotTarget.spotTargetSetting.RowsPerPage,
                                                                                    spotTarget.spotTargetSetting.ChartBorder,
                                                                                    spotTarget.spotTargetSetting.Vgap,
                                                                                    spotTarget.spotTargetSetting.Hgap,
                                                                                    spotTarget.spotTargetSetting.GapLeval,
                                                                                    spotTarget.spotTargetSetting.PreviousL,
                                                                                    spotTarget.spotTargetSetting.ChartRes,
                                                                                    spotTarget.IsCustom,
                                                                                    spotLabData.ToString()
                                                                                });
                 },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_CreateSpotTarget"));
            return returnValue;
        }

        public SpotTarget GetSpotTarget(int SpotTargetID)
        {
            var spotTarget = new SpotTarget();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSpotTarget = DataAccessFactory.Instance.GetDsResult("[CPSpot].[GetSpotTarget]", new object[] { SpotTargetID });
                    if (dsSpotTarget != null && dsSpotTarget.Tables.Count > 0 && dsSpotTarget.Tables[0].Rows.Count > 0)
                    {
                        spotTarget = dsSpotTarget.Tables[0].AsEnumerable().
                            Select(x => new SpotTarget()
                            {
                                SpotTargetID = DataReader.ReadInt(x, "SpotTargetID"),
                                SpotTargetName = DataReader.ReadString(x, "SpotTargetName"),
                                Company = new Entity.Company()
                                {
                                    CompanyId = DataReader.ReadInt(x, "CompanyID")
                                },
                                Description = DataReader.ReadString(x, "Description"),
                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                spotTargetSetting = new Entity.SpotProjectChartSetting()
                                {
                                    ChartName = DataReader.ReadString(x, "ChartName"),
                                    NumOfRows = DataReader.ReadInt(x, "NumOfRows"),
                                    NumOfCols = DataReader.ReadInt(x, "NumOfCols"),
                                    ChartSizeX = DataReader.ReadDecimal(x, "ChartSizeX"),
                                    ChartSizeY = DataReader.ReadDecimal(x, "ChartSizeY"),
                                    PatchSizeX = DataReader.ReadDecimal(x, "PatchSizeX"),
                                    PatchSizeY = DataReader.ReadDecimal(x, "PatchSizeY"),
                                    FirstSample = DataReader.ReadString(x, "FirstSample"),
                                    ReadDirection = DataReader.ReadString(x, "ReadDirection"),
                                    ReadMode = DataReader.ReadString(x, "ReadMode"),
                                    Pages = DataReader.ReadInt(x, "Pages"),
                                    RowsPerPage = DataReader.ReadInt(x, "RowsPerPage"),
                                    ChartBorder = DataReader.ReadInt(x, "ChartBorder"),
                                    Vgap = DataReader.ReadInt(x, "Vgap"),
                                    Hgap = DataReader.ReadInt(x, "Hgap"),
                                    GapLeval = DataReader.ReadInt(x, "GapLeval"),
                                    PreviousL = DataReader.ReadInt(x, "PreviousL"),
                                    ChartRes = DataReader.ReadInt(x, "ChartRes")
                                }
                            }).First();

                        if (dsSpotTarget.Tables[1].Rows.Count > 0)
                        {
                            spotTarget.spotTargetLab = dsSpotTarget.Tables[1].AsEnumerable().
                            Select(x => new SpotTargetLAB()
                            {
                                SpotID = DataReader.ReadShort(x, "SpotID"),
                                SpotName = DataReader.ReadString(x, "SpotName"),
                                Target_a = DataReader.ReadDecimal(x, "Target_a*"),
                                Target_b = DataReader.ReadDecimal(x, "Target_b*"),
                                Target_L = DataReader.ReadDecimal(x, "Target_L*"),
                                HTMLColor = DataReader.ReadString(x, "HTMLColor")
                            }).ToList();
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetReferenceCharts"));
            return spotTarget;
        }

        public int UpdateSpotTarget(SpotTarget spotTarget)
        {
            var returnValue = 0;
            Common.ImplementBusinessLogic(
                 () =>
                 {
                     string strspotLabData = "";

                     if (spotTarget.spotTargetLab!= null && spotTarget.spotTargetLab.Count > 0)
                     {
                         XElement spotLabData = new XElement("SpotTargetLABData",
                                              spotTarget.spotTargetLab.
                                              Select(x =>
                                                  new XElement("SpotTargetLAB",
                                                           new XAttribute("SpotID", x.SpotID),
                                                           new XAttribute("SpotName", x.SpotName),
                                                           new XAttribute("Target_L", x.Target_L),
                                                           new XAttribute("Target_a", x.Target_a),
                                                           new XAttribute("Target_b", x.Target_b)
                                              //new XAttribute("HTMLColor", x.HTMLColor)
                                              )).ToList());
                         strspotLabData = spotLabData.ToString();
                     }

                     returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CPSpot].[UpdateSpotTarget]", new object[]
                                                                                {
                                                                                    spotTarget.SpotTargetID,
                                                                                    spotTarget.Company.CompanyId,
                                                                                    spotTarget.SpotTargetName,
                                                                                    spotTarget.Description,
                                                                                    spotTarget.IsUsed,
                                                                                    spotTarget.spotTargetSetting.ChartName,
                                                                                    spotTarget.spotTargetSetting.NumOfRows,
                                                                                    spotTarget.spotTargetSetting.NumOfCols,
                                                                                    spotTarget.spotTargetSetting.ChartSizeX,
                                                                                    spotTarget.spotTargetSetting.ChartSizeY,
                                                                                    spotTarget.spotTargetSetting.PatchSizeX,
                                                                                    spotTarget.spotTargetSetting.PatchSizeY,
                                                                                    spotTarget.spotTargetSetting.FirstSample,
                                                                                    spotTarget.spotTargetSetting.ReadDirection,
                                                                                    spotTarget.spotTargetSetting.ReadMode,
                                                                                    spotTarget.spotTargetSetting.Pages,
                                                                                    spotTarget.spotTargetSetting.RowsPerPage,
                                                                                    spotTarget.spotTargetSetting.ChartBorder,
                                                                                    spotTarget.spotTargetSetting.Vgap,
                                                                                    spotTarget.spotTargetSetting.Hgap,
                                                                                    spotTarget.spotTargetSetting.GapLeval,
                                                                                    spotTarget.spotTargetSetting.PreviousL,
                                                                                    spotTarget.spotTargetSetting.ChartRes,
                                                                                    strspotLabData
                                                                                });

                 },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateSpotTarget"));
            return returnValue;
        }


        public int DeleteSpotTargetList(List<string> lstDeleteSpotTargetID)
        {
            string strSpotIdList = "";
            var outParam = new Parameters() { ParameterName = "@isDeleted", Type = DbType.Int32 };

            if (lstDeleteSpotTargetID != null && lstDeleteSpotTargetID.Count > 0)
            {
                XElement spotLabData = new XElement("SpotTargetIdData",
                                   lstDeleteSpotTargetID.
                                     Select(x => new XElement("SpotTarget", new XAttribute("SpotTargetId", x.ToString())
                                     )).ToList());
                strSpotIdList = spotLabData.ToString();
            }

            Common.ImplementBusinessLogic(
                () =>
                    DataAccessFactory.Instance.ExecuteProcedure("CPSpot.DeleteSpotTargetList",
                                          new List<Parameters>()
                                             {
                                                 new Parameters()
                                                     {ParameterName = "@SpotTargetList", Size = 4000, Type = DbType.Xml, Value = strSpotIdList},
                                             },
                                         new List<Parameters>()
                                             {
                                                 outParam
                                             }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteSpotTarget"));
            return Convert.ToInt32(outParam.Value);
        }


        public int DeleteSpotTarget(int spotTargetId)
        {
            var outParam = new Parameters() { ParameterName = "@isDeleted", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                //DataAccessFactory.
                //          Instance.
                //          ExecuteProcedure(
                //              "[CPSpot].[DeleteSpotTarget]",
                //              new object[]
                //                  {
                //                      spotTargetId
                //                  })
                                  DataAccessFactory.
                        Instance.
                        ExecuteProcedure("CPSpot.DeleteSpotTarget",
                                         new List<Parameters>()
                                             {
                                                 new Parameters()
                                                     {ParameterName = "@SpotTargetID", Size = 50, Type = DbType.String, Value = spotTargetId},
                                             },
                                         new List<Parameters>()
                                             {
                                                 outParam
                                             }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteSpotTarget"));
            return Convert.ToInt32(outParam.Value);
        }

        /// <summary>
        /// GetSpotTargetList_CustomBCO
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CurrentUserID"></param>
        /// <returns></returns>
        public List<SpotTarget> GetSpotTargetList_CustomBCO(int UserID, int CurrentUserID)
        {
            var objSpotTargets = new List<SpotTarget>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSpots = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "[CPSpot].[GetSpotTargets_CustomBCO]",
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
                                       Description = DataReader.ReadString(x, "Description"),
                                       IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                       NumberOfColors = DataReader.ReadInt(x, "NumberOfColors"),
                                       Company = new Entity.Company()
                                       {
                                           CompanyId = DataReader.ReadInt(x, "CompanyID"),
                                           CompanyName = DataReader.ReadString(x, "CompanyName"),
                                       },
                                   }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSpotTargetList"));
            return objSpotTargets;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
