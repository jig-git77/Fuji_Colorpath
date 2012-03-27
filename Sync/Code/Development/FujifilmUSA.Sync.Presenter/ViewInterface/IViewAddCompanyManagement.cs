using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddCompanyManagement : IViewError
    {
        Company Company { get; set; }
        Dictionary<int, string> RegionList { get; set; }
    }
}
