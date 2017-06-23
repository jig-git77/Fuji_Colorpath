using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Business
{
    public delegate void ErrorHandler(object sender, ErrorArgs e);

    public class BusinessHandlerRequest
    {
        public BusinessHandlerRequest(ITaskBase taskBase, ErrorHandler handler, string userFriendlyMessage)
        {
            TaskBase = taskBase;
            Handler = handler;
            UserFriendlyMessage = userFriendlyMessage;
        }

        public ITaskBase TaskBase { get; set; }
        public ErrorHandler Handler { get; set; }
        public string UserFriendlyMessage { get; set; }
    }
}
