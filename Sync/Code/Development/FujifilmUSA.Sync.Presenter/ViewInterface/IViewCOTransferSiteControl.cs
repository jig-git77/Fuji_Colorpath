using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewCOTransferSiteControl : IViewBase
    {
        string SilverlightPostUrl { get; set; }
    }
}
