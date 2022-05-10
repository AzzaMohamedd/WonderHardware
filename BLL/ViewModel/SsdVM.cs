using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class SsdVM
    {
        public string Ssdcode { get; set; }
        public string Ssdname { get; set; }
        public byte? SsdbrandId { get; set; }
        public string BrandName { get; set; }
        public int Ssdprice { get; set; }
        public short Ssdquantity { get; set; }
        public short Ssdsize { get; set; }
        public string Ssdinterface { get; set; }
        public decimal? Ssdrate { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<byte[]> Image { get; set; }

        public virtual BrandVM Ssdbrand { get; set; }
    }
}
