using System;
using System.Collections.Generic;

#nullable disable

namespace UI.Models
{
    public partial class Sale
    {
        public int? UserId { get; set; }
        public string ProductCode { get; set; }
        public DateTime DateAndTime { get; set; }
        public byte ProductQuantity { get; set; }
        public int TotalPrice { get; set; }
        public string Address { get; set; }

        public virtual Hdd ProductCode1 { get; set; }
        public virtual Motherboard ProductCode2 { get; set; }
        public virtual Processor ProductCode3 { get; set; }
        public virtual PowerSupply ProductCode4 { get; set; }
        public virtual Ram ProductCode5 { get; set; }
        public virtual Ssd ProductCode6 { get; set; }
        public virtual GraphicsCard ProductCode7 { get; set; }
        public virtual Case ProductCodeNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
