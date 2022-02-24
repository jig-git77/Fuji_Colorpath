using System.Collections.Generic;
using System.Data;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewUpdateChangeLog : IViewBase
    {
        List<Entity.UpdateChangeLog> UpdateChangeLogList { get; set; }
        DataSet resultData { get; set; }
        Entity.UpdateChangeLog objUpdateChangeLog { get; set; }

    }
}
