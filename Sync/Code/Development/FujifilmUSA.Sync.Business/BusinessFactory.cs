using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Mock;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business
{
    public class BusinessFactory<T>
        where T : ITaskBase
    {
        public ITaskBase CreateObject()
        {
            string className = Utilities.ReadConfigValue(typeof (T).Name);
            string assemblyName = Utilities.ReadConfigValue("AssemblyName");

            var task = (ITaskBase) Activator.CreateInstance(Type.GetType(className));

            return task;
        }
    }
}
