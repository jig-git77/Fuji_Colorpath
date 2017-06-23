using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class COErrorMessage
    {
        public COErrorMessage()
        {
            Header = "";
            Message = "";
        }
        public string Header { get; set; }
        public string Message { get; set; }
    }
}
