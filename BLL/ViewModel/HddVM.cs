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
        public string BrandName { get; set; }
        public int Hddprice { get; set; }
        public short Hddquantity { get; set; }
        public short Hddsize { get; set; }
        public short Hddrpm { get; set; }
        public string Hddtype { get; set; }
        public decimal? Hddrate { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<byte[]> Image { get; set; }

        public virtual BrandVM Hddbrand { get; set; }
    }
}
