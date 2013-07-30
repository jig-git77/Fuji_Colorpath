using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.Business;
using Service.Common.FaultContracts;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static void ErrorOccured(object sender, Sync.Entity.ErrorArgs e)
        {
            ExceptionHandling.RaiseException(e.ErrorMessage, ErrorCode.Internal);
        }
    }
}
