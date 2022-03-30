using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Motherboard
    {
        public string MotherCode { get; set; }
        public string MotherName { get; set; }
        public byte? MotherBrandId { get; set; }
        public int MotherPrice { get; set; }
        public short MotherQuantity { get; set; }
        public string MotherSocket { get; set; }
        public byte? MotherRate { get; set; }

        public virtual Brand MotherBrand { get; set; }
    }
}
