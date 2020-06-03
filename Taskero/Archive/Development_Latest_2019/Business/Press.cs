#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public class Press
    {
        public int JobID { get; set; }

        public string DongleID{ get; set; }

        public int SheetID{ get; set; }

        public int SheetNumber { get; set; }

        public string ColorID{ get; set; }
    }
}
