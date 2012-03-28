using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business
{
    public class Common
    {
        public static void ImplementBusinessLogic(Action action, BusinessHandlerRequest request)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                LogManager.WriteLog(e, request);
                if (request.Handler != null)
                {
                    request.Handler(
                        request.TaskBase,
                        new ErrorArgs(
                            FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage(request.UserFriendlyMessage)));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
