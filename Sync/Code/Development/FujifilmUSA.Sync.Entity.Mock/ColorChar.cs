using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class ColorChar
    {
        public byte ColorCharId { get; set; }
        public string ColorCharacteristics { get; set; }
        public bool IsUsed { get; set; }
        public List<MeasuredColorChar> MeasuredColorChars { get; set; }
    }
}