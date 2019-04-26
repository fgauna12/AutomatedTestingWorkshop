using System;

namespace KetoPal.Core.Models
{
    public class CarbConsumption
    {
        public DateTimeOffset ConsumedOn { get; set; }
        public double Amount { get; set; }
    }
}