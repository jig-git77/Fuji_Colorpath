using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProfileManagement : IViewBase
    {
        int LoginId { get; set; }
        string Password { get; set; }
        int SecurityQuestionId { get; set; }
        string SecurityAnswer { get; set; }
        Dictionary<int, string> SecurityQuestions { get; set; }
    }
}
