using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewApplicationConfiguration : IViewBase
    {
        bool IsSingleUserLoginEnabled { get; set; }
        int KeepUserAliveInSeconds { get; set; }
    }
}
