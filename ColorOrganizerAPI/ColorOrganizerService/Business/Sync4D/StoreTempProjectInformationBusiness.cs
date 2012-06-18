using FujifilmUSA.ColorOrganizerService.MessageContracts;
using Service.Common.Business;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static StoreTempProjectInformationResponse StoreTempProjectInformation(StoreTempProjectInformationRequest request)
        {
            var response = new StoreTempProjectInformationResponse();
            if (request == null)
            {
                request = new StoreTempProjectInformationRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                            {
                                                var userBusiness = new UserBusiness();
                                                userBusiness.UserLogin(request.UserName, request.UserPassword);

                                                var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                                                projectBusiness.ErrorOccured += ErrorOccured;
                                                response.ProjectInfoId = projectBusiness.StoreTempararyProjectInformation(
                                                    request.ProjectName,
                                                    request.Media,
                                                    request.Colorant,
                                                    request.RipSettings,
                                                    request.MeasurementDeviceTypeId,
                                                    request.ChartId,
                                                    request.ProjectId,
                                                    request.ProjectInfoId);

                                                response.Count = 1;
                                                response.Message = Common.GetSuccessMessage();
                                            }, "Store Temp Project Information");
            response.CalculateAndLogRequestTime("StoreTempProjectInformation");
            return response;
        }
    }
}
