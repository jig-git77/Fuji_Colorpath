using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.CustomExceptions;

namespace FujifilmUSA.Sync.Business
{
    public class Common
    {
        internal static string ProfileDescriptionKey = "Description";

        public static void ImplementBusinessLogic(Action action, BusinessHandlerRequest request)
        {
            try
            {
                action();
            }
            catch (DuplicateException e)
            {
                if (request.Handler != null)
                {
                    request.Handler(
                        request.TaskBase,
                        new ErrorArgs(
                            FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage(e.UserFriendlyMessage)));
                }
                else
                {
                    throw;
                }
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