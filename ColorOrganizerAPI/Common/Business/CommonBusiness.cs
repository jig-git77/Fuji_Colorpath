using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Service.Common.Business
{
    public class CommonBusiness
    {
        public static string GetRequestIpAddress()
        {
            var ipAddress = string.Empty;
            if (OperationContext.Current != null)
            {
                OperationContext context = OperationContext.Current;
                if (context.IncomingMessageProperties != null)
                {
                    MessageProperties messageProperties = context.IncomingMessageProperties;
                    var endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    if (endpointProperty != null) ipAddress = endpointProperty.Address;
                }
            }
            return ipAddress;
        }
    }
}
