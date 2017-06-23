using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewGlobalActivation : IViewBase
    {
        User User { get; set; }
        Dictionary<int, string> SecurityQuestions { get; set; }
        UserAuthenticationUpdateType UpdateType { get; set; }
    }
}
