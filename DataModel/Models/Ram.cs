using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Ram
    {
        public Ram()
        {
            Sales = new HashSet<Sale>();
        }

        public string RamCode { get; set; }
        public string RamName { get; set; }
        public byte? RamBrandId { get; set; }
        public int RamPrice { get; set; }
        public short RamQuantity { get; set; }
        public byte RamSize { get; set; }
        public short RamFrequency { get; set; }
        public string RamType { get; set; }
        public byte Ramkits { get; set; }
        public byte? RamRate { get; set; }

        public virtual Brand RamBrand { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
