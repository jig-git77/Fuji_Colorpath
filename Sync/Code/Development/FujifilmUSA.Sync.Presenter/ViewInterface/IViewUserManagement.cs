using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewUserManagement : IViewError
    {
        User User { get; set; }
        List<User> UserList { get; set; }
    }
}
