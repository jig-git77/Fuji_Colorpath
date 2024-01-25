using System.ServiceModel;
using TaskeroWeb.Models.Service.MessageContracts;

namespace TaskeroWeb.Models.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectManagementService" in both code and config file together.
    [ServiceContract]
    public interface IProjectManagementService
    {
        [OperationContract]
        GetDeviceListResponse GetDeviceList(GetDeviceListRequest request);

        [OperationContract]
        AddProjectResponse AddProject(AddProjectRequest request);

        [OperationContract]
        GetProjectDetailResponse GetProjectDetail(GetProjectDetailRequest request);

        [OperationContract]
        GetProjectsResponse GetProjects(GetProjectsRequest request);
    }
}
