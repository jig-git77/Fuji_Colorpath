using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class Activity
    {
        //public int RegionID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string UserName { get; set; }
        public int PrintCondCreate { get; set; }
        public int PrintCondDelete { get; set; }
        public int UploadMeasure { get; set; }
        public int CreateDLP { get; set; }
        public int OptDLP { get; set; }
        public int NoOfLogins { get; set; }
    }
}
