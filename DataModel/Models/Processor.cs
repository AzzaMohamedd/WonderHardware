using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Processor
    {
        public string ProCode { get; set; }
        public string ProName { get; set; }
        public byte? ProBrandId { get; set; }
        public int ProPrice { get; set; }
        public short ProQuantity { get; set; }
        public byte ProCores { get; set; }
        public string ProSocket { get; set; }
        public byte ProThreads { get; set; }
        public double ProBaseFreq { get; set; }
        public double ProMaxTurboFreq { get; set; }
        public string ProLithography { get; set; }
        public byte? ProRate { get; set; }

        public virtual Brand ProBrand { get; set; }
    }
}
