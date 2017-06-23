using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddUserManagement : IViewBase
    {
        User UserInformation { get; set; }
        int CompanyId { get; set; }
        Dictionary<int, string> Companies { get; set; }
        Dictionary<int, string> Branches { get; set; }
        Dictionary<int, string> Regions { get; set; }
        Dictionary<int, string> UserRoleList { get; set; }
        int RegionId { get; set; }
        UserAuthenticationUpdateType UpdateType { get; set; }
        List<Language> LanguageList { get; set; }
    }
}
