using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class PowerSupplyVM
    {
        public string Psucode { get; set; }
        public string Psuname { get; set; }
        public byte PsubrandId { get; set; }
        public int Psuprice { get; set; }
        public short Psuquantity { get; set; }
        public short Psuwatt { get; set; }
        public string Psucertificate { get; set; }
        public byte? Psurate { get; set; }

        public virtual BrandVM Psubrand { get; set; }
    }
}
