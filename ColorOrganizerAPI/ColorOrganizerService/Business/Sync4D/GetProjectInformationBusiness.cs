using FujifilmUSA.ColorOrganizerService.DataContracts;
using FujifilmUSA.ColorOrganizerService.MessageContracts;
using Service.Common.Business;
using Service.Common.FaultContracts;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using System;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static GetProjectInformationResponse GetProjectInformation(GetProjectInformationRequest request)
        {
            var response = new GetProjectInformationResponse
            {
                ProjectInformation = new ProjectInformation()
            };
            if (request == null)
            {
                request = new GetProjectInformationRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                        {
                                            var userBusiness = new UserBusiness();
                                            userBusiness.UserLogin(request.UserName, request.UserPassword);

                                            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
                                            projectBusiness.ErrorOccured += ErrorOccured;
                                            var projectInfo = projectBusiness.GetProjectDetail(request.ProjectId);

                                            if (projectInfo != null && projectInfo.ProjectId > 0)
                                            {
                                                response.ProjectInformation.ProjectId = projectInfo.ProjectId;
                                                response.ProjectInformation.ProjectName = projectInfo.ProjectName;
                                                response.ProjectInformation.OptimizeRunCount = projectInfo.OptimizeRuns.Count;
                                                response.ProjectInformation.CreatedDate = projectInfo.CreateDate;
                                                response.ProjectInformation.Media = projectInfo.PrintCondition.Media;
                                                response.ProjectInformation.Colorant = projectInfo.PrintCondition.Colorant;
                                                response.ProjectInformation.RipSettings = projectInfo.PrintCondition.RipSettings;
                                                response.ProjectInformation.DeviceId = projectInfo.DeviceId;
                                                response.ProjectInformation.ChartId = projectInfo.MeasurementSetting.ReferenceChartType.Key;
                                                response.ProjectInformation.Status = (ProjectStatus)Enum.Parse(typeof(ProjectStatus), projectInfo.Status.ToString());
                                                response.ProjectInformation.SourceProfileId = projectInfo.SourceProfile.SourceProfileId;
                                                response.ProjectInformation.SourceProfileName = projectInfo.SourceProfile.ProfileName;
                                                response.Count = 1;
                                                response.Message = Common.GetSuccessMessage();
                                            }
                                            else
                                            {
                                                ExceptionHandling.RaiseException("project not found", ErrorCode.DataNotFound);
                                            }

                                        }, "Get Project Information");
            response.CalculateAndLogRequestTime("GetProjectInformation");
            return response;
        }
    }
}
