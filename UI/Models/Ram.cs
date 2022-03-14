using System;
using System.Collections.Generic;

#nullable disable

namespace UI.Models
{
    public partial class Ram
    {
        public string RamCode { get; set; }
        public string RamName { get; set; }
        public byte? RamBrandId { get; set; }
        public int RamPrice { get; set; }
        public short RamQuantity { get; set; }
        public byte[] RamImage { get; set; }
        public byte RamSize { get; set; }
        public short RamFrequency { get; set; }
        public string RamType { get; set; }
        public byte Ramkits { get; set; }

        public virtual Brand RamBrand { get; set; }
    }
}
