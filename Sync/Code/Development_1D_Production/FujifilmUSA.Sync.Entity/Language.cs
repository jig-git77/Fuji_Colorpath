using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class Language
    {
        public int LanguageId { get; set; }

        public string EnglishName { get; set; }

        public string DisplayText { get; set; }

        public string CultureCode { get; set; }

        public bool IsDefault { get; set; }

        public bool IsDeleted { get; set; }
    }
}
