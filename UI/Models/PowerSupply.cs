﻿using System;
using System.Collections.Generic;

#nullable disable

namespace UI.Models
{
    public partial class PowerSupply
    {
        public PowerSupply()
        {
            Sales = new HashSet<Sale>();
        }

        public string Psucode { get; set; }
        public string Psuname { get; set; }
        public byte PsubrandId { get; set; }
        public int Psuprice { get; set; }
        public short Psuquantity { get; set; }
        public short Psuwatt { get; set; }
        public string Psucertificate { get; set; }
        public byte? Psurate { get; set; }

        public virtual Brand Psubrand { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}