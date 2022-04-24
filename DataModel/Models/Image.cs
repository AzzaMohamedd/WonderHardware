using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Image
    {
        public string ProductCode { get; set; }
        public byte[] ProductImage { get; set; }

        public virtual Hdd ProductCode1 { get; set; }
        public virtual Motherboard ProductCode2 { get; set; }
        public virtual Processor ProductCode3 { get; set; }
        public virtual PowerSupply ProductCode4 { get; set; }
        public virtual Ram ProductCode5 { get; set; }
        public virtual Ssd ProductCode6 { get; set; }
        public virtual GraphicsCard ProductCode7 { get; set; }
        public virtual Case ProductCodeNavigation { get; set; }
    }
}