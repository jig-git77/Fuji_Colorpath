using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Fuji.Business;
using Fuji.Common;
using System.Data;
using System.Data.SqlClient;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class ProjectManagementService : IProjectManagementService
{
    public AddProjectResponse AddProject(AddProjectRequest request)
    {
        var response = new AddProjectResponse();
        var syncPressRun = new CPSyncPressRunData
                                    {
                                        ProjectName = request.ProjectName,
                                        PressID = request.DeviceId,
                                        Status = Convert.ToInt16(Enums.CPSyncPressRunStatus.Open),
                                        ΔEType = 1,
                                        StartDateTime = DateTime.Now,
                                        UseDLP = true,
                                        ProjectTypeID = request.ProjectType,
                                        OutputTypeID = request.OutputType
                                    };
        var dr = syncPressRun.CheckProjectName();
        if (dr.HasRows)
        {
            response.Message = "Project with similar name already exists for this press. Change project name";
            response.Severity = MessageSeverity.Error;
        }
        else
        {
            var data = syncPressRun.uspInsertCPSyncProject();
            response.ProjectId = data.Key;
            response.Message = "Project has been added successfully";
            response.Severity = MessageSeverity.Information;
        }
        return response;
    }

    public GetProjectDetailResponse GetProjectDetail(GetProjectDetailRequest request)
    {
        var response = new GetProjectDetailResponse();
        var pressRun = new CPSyncPressRunData();
        var dtPressRun = pressRun.uspGetAllSyncPressRunByProjectID(request.ProjectID);
        var dvPressRun = dtPressRun.DefaultView;
        dvPressRun.RowFilter = "Status=3";
        if (dvPressRun.Count == 1)
        {
            pressRun.PressRunID = Convert.ToInt16(dvPressRun[0]["PressRunID"]);
            var dr = pressRun.uspGetICCProfile();
            if (dr != null && dr.HasRows)
            {
                dr.Read();
                if (dr["ICCProfile"] != DBNull.Value)
                {
                    response.Profile = (byte[]) dr["ICCProfile"];
                }
                else
                {
                    response.Message = "Profile not found";
                    response.Severity = MessageSeverity.Error;
                }
            }
            else
            {
                response.Message = "Profile not found";
                response.Severity = MessageSeverity.Error;
            }
        }
        else
        {
            response.Message = "Project is not certified";
            response.Severity = MessageSeverity.Error;
        }
        return response;
    }

    public GetProjectsResponse GetProjects(GetProjectsRequest request)
    {
        var response = new GetProjectsResponse()
                           {
                               Projects = new List<ProjectInfo>()
                           };
        var dtProject = new CPSyncPressRunData().uspGetAllSyncProjectByPressID(request.DeviceId);
        var dvProject = dtProject.DefaultView;
        dvProject.RowFilter = "ProjStatusID=3";
        if (dvProject.Count > 0)
        {
            foreach (DataRowView dr in dvProject)
            {
                response.Projects.Add(new ProjectInfo()
                                          {
                                              DeviceId = Convert.ToInt32(dr["PressID"]),
                                              ProjectId = Convert.ToInt32(dr["ProjectID"]),
                                              ProjectName = dr["ProjectName"].ToString(),
                                              ΔEType = Convert.ToInt32(dr["ΔEType"])
                                          });
            }
        }
        else
        {
            response.Message = "No certified projects found";
            response.Severity = MessageSeverity.Warning;
        }
        return response;
    }

    public GetDeviceListResponse GetDeviceList(GetDeviceListRequest request)
    {
        var response = new GetDeviceListResponse()
                           {
                               Devices = new List<DeviceInfo>()
                           };
        var dtPress = new CPSyncPressData().uspGetPressOnDongleName(request.DongleName);
        if (dtPress.Rows.Count > 0)
        {
            foreach (DataRow dr in dtPress.Rows)
            {
                response.Devices.Add(new DeviceInfo()
                                         {
                                             DeviceId = Convert.ToInt32(dr["PressID"]),
                                             DeviceName = dr["PressName"].ToString(),
                                             DongleId = Convert.ToInt32(dr["CPDongleID"])
                                         });
            }
        }
        else
        {
            response.Message = "No Device found";
            response.Severity = MessageSeverity.Warning;
        }
        return response;
    }
}