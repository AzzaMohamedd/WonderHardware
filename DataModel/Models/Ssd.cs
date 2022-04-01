using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Ssd
    {
        public Ssd()
        {
            Sales = new HashSet<Sale>();
        }

        public string Ssdcode { get; set; }
        public string Ssdname { get; set; }
        public byte? SsdbrandId { get; set; }
        public int Ssdprice { get; set; }
        public short Ssdquantity { get; set; }
        public short Ssdsize { get; set; }
        public string Ssdinterface { get; set; }
        public byte? Ssdrate { get; set; }

        public virtual Brand Ssdbrand { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
