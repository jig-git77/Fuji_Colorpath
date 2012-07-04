using FujifilmUSA.ColorOrganizerService.MessageContracts;
using Service.Common.Business;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static CheckUserCredentialsResponse CheckUserCredentials(CheckUserCredentialsRequest request)
        {
            var response = new CheckUserCredentialsResponse();

            if (request == null)
            {
                request = new CheckUserCredentialsRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                        {
                                            var userBusiness = new UserBusiness();
                                            userBusiness.UserLogin(request.UserName, request.UserPassword);

                                            response.IsValid = (userBusiness.CurrentUser != null && userBusiness.CurrentUser.UserId > 0);
                                            if(response.IsValid)
                                            {
                                                response.Message = Common.GetSuccessMessage();
                                                response.Count = 1;
                                            }
                                            else
                                            {
                                                response.Count = 0;
                                            }
                                        }, "Check User Credentials");
            response.CalculateAndLogRequestTime("CheckUserCredentials");
            return response;
        }
    }
}
