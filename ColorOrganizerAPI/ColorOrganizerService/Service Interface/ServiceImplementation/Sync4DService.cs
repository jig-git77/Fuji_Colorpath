using System.ServiceModel.Activation;
using FujifilmUSA.ColorOrganizerService.ServiceContracts;
using FujifilmUSA.ColorOrganizerService.MessageContracts;
using FujifilmUSA.ColorOrganizerService.Business;

namespace FujifilmUSA.ColorOrganizerService.ServiceImplementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Sync4DService : ISync4DServiceContract
    {
        #region ISync4DServiceContract Members

        public bool Ping()
        {
            return true;
        }

        public GetDeviceListResponse GetDeviceList(GetDeviceListRequest request)
        {
            return Sync4DBusiness.GetDeviceList(request);
        }

        public GetReferenceChartListResponse GetReferenceChartList(GetReferenceChartListRequest request)
        {
            return Sync4DBusiness.GetReferenceChartList(request);
        }

        public GetProjectInformationResponse GetProjectInformation(GetProjectInformationRequest request)
        {
            return Sync4DBusiness.GetProjectInformation(request);
        }
        
        public GetProfileResponse GetProfile(GetProfileRequest request)
        {
            return Sync4DBusiness.GetProfile(request);
        }

        public StoreTempProjectInformationResponse StoreTempProjectInformation(StoreTempProjectInformationRequest request)
        {
            return Sync4DBusiness.StoreTempProjectInformation(request);
        }

        public CheckUserCredentialsResponse CheckUserCredentials(CheckUserCredentialsRequest request)
        {
            return Sync4DBusiness.CheckUserCredentials(request);
        }

        #endregion
    }
}