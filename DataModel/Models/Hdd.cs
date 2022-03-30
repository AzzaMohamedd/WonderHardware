﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Hdd
    {
        public string Hddcode { get; set; }
        public string Hddname { get; set; }
        public byte? HddbrandId { get; set; }
        public int Hddprice { get; set; }
        public short Hddquantity { get; set; }
        public short Hddsize { get; set; }
        public short Hddrpm { get; set; }
        public string Hddtype { get; set; }
        public byte? Hddrate { get; set; }

        public virtual Brand Hddbrand { get; set; }
    }
}
