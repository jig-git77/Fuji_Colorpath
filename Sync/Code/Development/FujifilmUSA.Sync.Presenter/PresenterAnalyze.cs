using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterAnalyze
    {
        private IViewAnalyze _viewAnalyze;
        public PresenterAnalyze(IViewAnalyze viewAnalyze)
        {
            _viewAnalyze = viewAnalyze;
        }

        public void CreateDestinationAndDlp()
        {
            
        }
    }
}
