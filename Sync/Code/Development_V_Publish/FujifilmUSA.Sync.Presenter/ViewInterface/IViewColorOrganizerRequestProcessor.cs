using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSyncV;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewColorOrganizerRequestProcessor : IViewBase
    {
        string UserName { get; set; }
        string Password { get; set; }
        User CurrentUser { get; set; }

        Device Device { get; set; }
        int DeviceId { get; set; }

        int TotalProjects { get; set; }
        int LicenseTypeId { get; set; }

        Guid TempProjectId { get; set; }
        TempProjectInfo TempProjectInfo { get; set; }

        int ProjectId { get; set; }
        Project Project { get; set; }

        Guid TempValidationInfoId { get; set; }
        TempValidationInfo TempValidationInfo { get; set; }
    }
}
