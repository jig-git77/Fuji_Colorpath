using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
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
            object message = HttpContext.GetGlobalResourceObject("ResourceMessages", keyName);
            if (message == null)
                return "No resouce message available";
            else
                return message.ToString();            
        }
    }
}
