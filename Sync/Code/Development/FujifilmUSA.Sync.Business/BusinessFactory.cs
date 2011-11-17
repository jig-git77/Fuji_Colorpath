using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Mock;

namespace FujifilmUSA.Sync.Business
{
    public class BusinessFactory
    {
        public ITaskeroUserBusiness GetInstance()
        {
            return new TaskeroUserBusinessMock();
        }
    }
}
