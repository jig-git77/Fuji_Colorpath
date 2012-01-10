using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business
{
    public class Common
    {
        public static void ImplementBusinessLogic(Action action, ITaskBase taskBase)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                taskBase.ErrorOccured(taskBase, new ErrorArgs(e.Message));
            }
        }
    }
}
