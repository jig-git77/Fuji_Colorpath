using System;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewBrowserTest : IViewBase
    {
        bool PingResponse { get; set; }
    }
}
