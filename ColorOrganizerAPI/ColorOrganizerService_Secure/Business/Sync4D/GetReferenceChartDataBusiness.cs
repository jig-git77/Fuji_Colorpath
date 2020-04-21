using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.ColorOrganizerService.MessageContracts;
using FujifilmUSA.ColorOrganizerService.DataContracts;
using Service.Common.Business;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using Service.Common.FaultContracts;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static GetReferenceChartDataResponse GetReferenceChartData(GetReferenceChartDataRequest request)
        {
            var response = new GetReferenceChartDataResponse
            {
                ReferenceChart = new ReferenceChart()
            };
            if (request == null)
            {
                request = new GetReferenceChartDataRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                        {
                                            var userBusiness = new UserBusiness();
                                            userBusiness.UserLogin(request.UserName, request.UserPassword);

                                            if(request.ChartId ==0)
                                            {
                                                ExceptionHandling.RaiseException("Chartid can't be zero", ErrorCode.BadRequest);
                                            }
                                            var referenceChartBusiness = BusinessFactory<IReferenceChartBusiness>.CreateObject();
                                            referenceChartBusiness.ErrorOccured += ErrorOccured;
                                            var referenceChart = referenceChartBusiness.GetReferenceChartDataForXmf(request.ChartId);

                                            if (referenceChart.ReferenceChartTypeId > 0)
                                            {
                                                response.ReferenceChart.ChartId = referenceChart.ReferenceChartTypeId;
                                                response.ReferenceChart.ChartPreview = referenceChart.ChartPreview;
                                                response.ReferenceChart.OriginalChart = referenceChart.OriginalChart;
                                                response.Count = 1;
                                                response.Message = Common.GetSuccessMessage();
                                            }
                                            else
                                            {
                                                ExceptionHandling.RaiseException("Chart not found", ErrorCode.DataNotFound);
                                            }
                                        }, "Get Reference Chart Data");
            response.CalculateAndLogRequestTime("GetReferenceChartData");
            return response;
        }
    }
}
