using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewCompanyManagement : IViewBase
    {
        Company Company { get; set; }
        List<Company> CompanyList { get; set; }
    }
}
