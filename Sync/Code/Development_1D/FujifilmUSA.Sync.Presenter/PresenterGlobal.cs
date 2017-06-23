using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterGlobal
    {
        public void LogApplicationError(Exception applicationException, bool sendEmail = true)
        {
            LogManager.WriteLog(applicationException, null, sendEmail);
        }
    }
}
