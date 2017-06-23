using System.Web;
namespace FujifilmUSA.Sync.Common
{
    public class ResourceManager
    {
        /// <summary>
        /// This method will be returning the message from the resource file.
        /// </summary>
        /// <param name="keyName">Provide the resource key</param>
        /// <returns>Resource message of the key provided. If no resource message or key is available, it will return no message available</returns>
        public static string GetResourceMessage(string keyName)
        {
            var message = FujifilmUSA.Sync.GlobalResources.GlobalResourceManager.Instance.GetGlobalResourceString(keyName);
            return message == null ? "No resouce message available" : message.ToString();
        }
    }
}
