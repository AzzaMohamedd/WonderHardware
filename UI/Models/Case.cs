using System;
using System.Collections.Generic;

#nullable disable

namespace UI.Models
{
    public partial class Case
    {
        public string CaseCode { get; set; }
        public string CaseName { get; set; }
        public byte? CaseBrandId { get; set; }
        public int CasePrice { get; set; }
        public short CaseQuantity { get; set; }
        public byte[] CaseImage { get; set; }
        public string CaseFactorySize { get; set; }

        public virtual Brand CaseBrand { get; set; }
    }
}
