using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class Case
    {
        public Case()
        {
            Sales = new HashSet<Sale>();
        }

        public string CaseCode { get; set; }
        public string CaseName { get; set; }
        public byte? CaseBrandId { get; set; }
        public int CasePrice { get; set; }
        public short CaseQuantity { get; set; }
        public string CaseFactorySize { get; set; }
        public byte? CaseRate { get; set; }

        public virtual Brand CaseBrand { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
