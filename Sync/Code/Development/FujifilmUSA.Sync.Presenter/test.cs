using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter
{
    class test
    {
        public void test1()
        {
            Business.BusinessFactory bf = new Business.BusinessFactory();
            var inst = bf.GetInstance().UserLogin("test", "test");
        }
    }
}
