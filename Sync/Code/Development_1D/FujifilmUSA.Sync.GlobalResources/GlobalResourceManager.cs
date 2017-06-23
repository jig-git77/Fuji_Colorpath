using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
namespace FujifilmUSA.Sync.GlobalResources
{    
    /// <summary>
    /// This class is used to access the global resource from FujifilmUSA.Sync.GlobalResources assembly
    /// </summary>
    /// <createdby>Chirag Vidani</createdby>
    /// <createddate>09 July 2012</createddate>
    public class GlobalResourceManager
    {
        private static readonly GlobalResourceManager _instance = new GlobalResourceManager();
        private readonly ResourceManager resourceManager;

        private GlobalResourceManager()
        {            
            resourceManager = new System.Resources.ResourceManager("FujifilmUSA.Sync.GlobalResources.Resources", this.GetType().Assembly);
        }

        public static GlobalResourceManager Instance
        {
            get
            {
                return new GlobalResourceManager();
            }
        }

        /// <summary>
        /// This method is used to get the localized value from the global resource file based to key provided in argument
        /// </summary>
        /// <param name="key">Accepts the key value assigned in the resource (.resx file)</param>
        /// <returns>Returns the localized value of key passed in parameter</returns>
        public string GetGlobalResourceString(string key)
        {
            if (resourceManager != null)
            {
                return resourceManager.GetString(key);
            }
            return key;
        }
    }
}
