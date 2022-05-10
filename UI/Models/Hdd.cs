﻿using System;
using System.Collections.Generic;

#nullable disable

namespace UI.Models
{
    public partial class Hdd
    {
        public Hdd()
        {
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string Hddcode { get; set; }
        public string Hddname { get; set; }
        public byte? HddbrandId { get; set; }
        public int Hddprice { get; set; }
        public short Hddquantity { get; set; }
        public short Hddsize { get; set; }
        public short Hddrpm { get; set; }
        public string Hddtype { get; set; }

        public virtual Brand Hddbrand { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
