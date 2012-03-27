using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewTargetColorspace : IViewError
    {
        List<SourceProfile> SourceProfile { get; set; }
    }
}
