using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class RamVM
    {
        public string RamCode { get; set; }
        public string RamName { get; set; }
        public byte? RamBrandId { get; set; }
        public string BrandName { get; set; }
        public int RamPrice { get; set; }
        public short RamQuantity { get; set; }
        public byte RamSize { get; set; }
        public short RamFrequency { get; set; }
        public string RamType { get; set; }
        public byte Ramkits { get; set; }
        public byte? RamRate { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<byte[]> Image { get; set; }

        public virtual BrandVM RamBrand { get; set; }
    }
}
