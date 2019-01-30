using System;
using System.Collections.Generic;
using FujifilmUSA.ColorOrganizerService.DataContracts;
using FujifilmUSA.ColorOrganizerService.DataContracts.Enums;
using FujifilmUSA.ColorOrganizerService.MessageContracts;
using Service.Common.Business;
using Service.Common.FaultContracts;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static GetReferenceChartListResponse GetReferenceChartList(GetReferenceChartListRequest request)
        {
            var response = new GetReferenceChartListResponse
            {
                ReferenceChartList = new List<ReferenceChart>()
            };
            if (request == null)
            {
                request = new GetReferenceChartListRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                        {
                                            var userBusiness = new UserBusiness();
                                            userBusiness.UserLogin(request.UserName, request.UserPassword);

                                            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
                                            referenceChartBusiness.ErrorOccured += ErrorOccured;
                                            var referenceCharts = referenceChartBusiness.GetReferenceChartsForXmf();

                                            if (referenceCharts.Count > 0)
                                            {
                                                referenceCharts.ForEach(x =>
                                                                        response.
                                                                            ReferenceChartList.
                                                                            Add(new ReferenceChart()
                                                                                    {
                                                                                        ChartId = x.ReferenceChartTypeId,
                                                                                        ChartName = x.ChartName,
                                                                                        NumOfCols = x.NumOfCols,
                                                                                        NumOfRows = x.NumOfRows,
                                                                                        TotalPatches = x.TotalPatches,
                                                                                        ProjectType = new ProjectType()
                                                                                                          {
                                                                                                              ProjectTypeId = x.ProjectType.ProjectTypeId,
                                                                                                              ProjectTypeName = x.ProjectType.ProjectTypeName
                                                                                                          },
                                                                                        MeasurementDevice =
                                                                                            (EMeasurementDevice)
                                                                                            Enum.Parse(typeof (EMeasurementDevice),
                                                                                                       x.InstrumentType.InstrumentTypeId.ToString()),
                                                                                        ChartType = (x.ChartDisplayType > 0)
                                                                                                        ? (EReferenceChartType)
                                                                                                          Enum.Parse(typeof (EReferenceChartType),
                                                                                                                     x.ChartDisplayType.ToString())
                                                                                                        : EReferenceChartType.None,
                                                                                    }));
                                                response.Count = response.ReferenceChartList.Count;
                                                response.Message = Common.GetSuccessMessage();
                                            }
                                            else
                                            {
                                                ExceptionHandling.RaiseException("Charts not found", ErrorCode.DataNotFound);
                                            }
                                        }, "Get Reference Chart List");
            response.CalculateAndLogRequestTime("GetReferenceChartList");
            return response;
        }
    }
}
