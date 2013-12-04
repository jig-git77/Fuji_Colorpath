using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.ColorOrganizerService.MessageContracts;
using Service.Common.Business;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static StoreTempValidationInformationResponse StoreTempValidationInformation(StoreTempValidationInformationRequest request)
        {
            var response = new StoreTempValidationInformationResponse();
            if (request == null)
            {
                request = new StoreTempValidationInformationRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                        {
                                            var userBusiness = new UserBusiness();
                                            userBusiness.UserLogin(request.UserName, request.UserPassword);

                                            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                                            projectBusiness.ErrorOccured += ErrorOccured;
                                            response.ValidationInfoId = projectBusiness.StoreTemporaryValidationInformation(
                                                request.JobName,
                                                 request.Customer,
                                                 request.PageOrSurface,
                                                 request.JDFJobId,
                                                 request.ProjectId,
                                                 request.TempValidationId,
                                                 request.SilverlightPostUrl);

                                            response.Count = 1;
                                            response.Message = Common.GetSuccessMessage();
                                        }, "Store Temp Validation Information");
            response.CalculateAndLogRequestTime("StoreTempValidationInformation");

            return response;
        }
    }
}
