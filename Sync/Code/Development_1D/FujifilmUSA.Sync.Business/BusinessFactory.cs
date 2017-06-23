using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business
{
    public class BusinessFactory<T>
        where T : ITaskBase
    {
        public static T CreateObject()
        {
            string className = Utilities.ReadConfigValue(typeof(T).Name);
            var business = Activator.CreateInstance(Type.GetType(className));
            return (T)business;
        }
    }
}
