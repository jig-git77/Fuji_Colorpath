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
        public static GetProfileResponse GetProfile(GetProfileRequest request)
        {
            var response = new GetProfileResponse
                               {
                                   Profile = new Profile()
                               };
            if (request == null)
            {
                request = new GetProfileRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                            {
                                                var userBusiness = new UserBusiness();
                                                userBusiness.UserLogin(request.UserName, request.UserPassword);

                                                var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                                                projectBusiness.ErrorOccured += ErrorOccured;
                                                byte[] profileData = null;
                                                switch (request.ProfileType)
                                                {
                                                    case EProfileType.SourceProfile:
                                                        profileData = projectBusiness.GetSourceProfile(request.ProjectId);
                                                        break;
                                                    case EProfileType.DestinationPRofile:
                                                        profileData = projectBusiness.GetDestinationProfile(request.ProjectId);
                                                        break;
                                                    case EProfileType.DeviceLinkProfile:
                                                        profileData = projectBusiness.GetLastOptimizedDlp(request.ProjectId);
                                                        break;
                                                }

                                                if (profileData != null && profileData.Length > 0)
                                                {
                                                    response.Profile.ProfileData = profileData;
                                                    response.Count = 1;
                                                    response.Message = Common.GetSuccessMessage();
                                                }
                                                else
                                                {
                                                    ExceptionHandling.RaiseException("profile not found", ErrorCode.DataNotFound);
                                                }

                                            }, "Get Profile");
            response.CalculateAndLogRequestTime("GetProfile");
            return response;
        }
    }
}
