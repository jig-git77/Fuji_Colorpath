using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.ColorOrganizerService.DataContracts;
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
                                            var profileData = projectBusiness.GetLastOptimizedDlp(request.ProjectId);

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
