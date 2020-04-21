using System.ServiceModel;
using Service.Common.FaultContracts;
using FujifilmUSA.ColorOrganizerService.MessageContracts;

namespace FujifilmUSA.ColorOrganizerService.ServiceContracts
{
    [ServiceContract]
    public interface ISync4DServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        bool Ping();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        GetDeviceListResponse GetDeviceList(GetDeviceListRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        GetReferenceChartListResponse GetReferenceChartList(GetReferenceChartListRequest request);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        GetReferenceChartDataResponse GetReferenceChartData(GetReferenceChartDataRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        GetProjectInformationResponse GetProjectInformation(GetProjectInformationRequest request);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        GetProfileResponse GetProfile(GetProfileRequest request);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        StoreTempProjectInformationResponse StoreTempProjectInformation(StoreTempProjectInformationRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        CheckUserCredentialsResponse CheckUserCredentials(CheckUserCredentialsRequest request);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        StoreTempValidationInformationResponse StoreTempValidationInformation(StoreTempValidationInformationRequest request);
    }
}