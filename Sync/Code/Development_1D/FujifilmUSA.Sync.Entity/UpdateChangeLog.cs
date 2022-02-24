using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Entity
{

    public class UpdateChangeLog
    {

        public int UpdateListID { get; set; }
        public string HeaderLine { get; set; }
        public string SubHeaderLine { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAlert { get; set; }
    }
}
