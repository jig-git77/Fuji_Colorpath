using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;

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
                if (handler != null)
                {
                    handler(taskBase, new ErrorArgs(e.Message));
                }
                else
                {
                    throw e;
                }
            }
        }
    }
}
