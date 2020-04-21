using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.DataContracts;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    internal class Common
    {
        public static ResponseMessage GetSuccessMessage()
        {
            return new ResponseMessage()
                       {
                           Message = "Success",
                           Severity = MessageSeverity.Information
                       };
        }
    }
}
