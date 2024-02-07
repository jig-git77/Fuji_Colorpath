#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Taskero.DataAccess;
#endregion

namespace Taskero.Business
{
    public abstract class Processor
    {
        public int TaskeroUserID { get; set; }
        public int RoleID { get; set; }
        public int DongleID { get; set; }
        public string ZProcIDs { get; set; }
        public int ZProcID { get; set; }
    }
}
