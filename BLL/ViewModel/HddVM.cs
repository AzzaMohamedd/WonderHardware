using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class HddVM
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

        public virtual BrandVM Hddbrand { get; set; }
    }
}
