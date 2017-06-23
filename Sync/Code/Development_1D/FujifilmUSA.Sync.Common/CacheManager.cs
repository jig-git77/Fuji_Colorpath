using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;
using System.Web;
using System.Web.Caching;

namespace FujifilmUSA.Sync.Common
{
    public class CacheManager
    {
        #region Private Variables
        private const string _activeUsers = "activeusers";
        #endregion

        #region Singleton
        private CacheManager() { }

        private static readonly CacheManager _instance = new CacheManager();

        public static CacheManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region Cache
        public List<User> ActiveUsers
        {
            get
            {
                lock (Instance)
                {
                    Cache cache = HttpContext.Current.Cache;
                    if (cache.Get(_activeUsers) != null)
                        return (List<User>)cache.Get(_activeUsers);
                    else
                        return new List<User>();
                }
            }
            set
            {
                lock (Instance)
                {
                    Cache cache = HttpContext.Current.Cache;
                    cache.Insert(_activeUsers, value, null, DateTime.Now.AddHours(2), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
        }
        #endregion
    }
}
