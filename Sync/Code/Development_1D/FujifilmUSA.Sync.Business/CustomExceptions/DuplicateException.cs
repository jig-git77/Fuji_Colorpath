using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.CustomExceptions
{
    internal class DuplicateException : Exception
    {
        public string UserFriendlyMessage { get; set; }
    }
}
