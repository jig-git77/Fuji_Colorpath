using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewUserManagement : IViewBase
    {
        User User { get; set; }
        List<User> UserList { get; set; }
    }
}
