using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business
{
    public class BusinessFactory<T>
        where T : ITaskBase
    {
        public ITaskBase CreateObject()
        {            
            string className = Utilities.ReadConfigValue(typeof (T).Name);
            var business = (ITaskBase) Activator.CreateInstance(Type.GetType(className));
            return business;
        }
    }
}
