﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class CaseVM
    {
        public string CaseCode { get; set; }
        public string CaseName { get; set; }
        public byte? CaseBrandId { get; set; }
        public string BrandName { get; set; }
        public int CasePrice { get; set; }
        public short CaseQuantity { get; set; }
        public string CaseFactorySize { get; set; }
        public byte? CaseRate { get; set; }
        public IEnumerable<byte[]> Image { get; set; }
        public virtual BrandVM CaseBrand { get; set; }

    }
}
