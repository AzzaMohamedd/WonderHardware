using System;
using System.Collections.Generic;

#nullable disable

namespace UI.Models
{
    public partial class Ssd
    {
        public string Ssdcode { get; set; }
        public string Ssdname { get; set; }
        public byte? SsdbrandId { get; set; }
        public int Ssdprice { get; set; }
        public short Ssdquantity { get; set; }
        public byte[] Ssdimage { get; set; }
        public short Ssdsize { get; set; }
        public string Ssdinterface { get; set; }

        public virtual Brand Ssdbrand { get; set; }
    }
}
