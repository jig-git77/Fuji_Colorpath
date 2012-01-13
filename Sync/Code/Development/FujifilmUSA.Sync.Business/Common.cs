using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business
{
    public class Common
    {
        public static void ImplementBusinessLogic(Action action, ITaskBase taskBase, ErrorHandler handler)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                LogManager.WriteLog(e.Message);
                
                if (handler != null)
                {
                    handler(taskBase, new ErrorArgs(e.Message));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
