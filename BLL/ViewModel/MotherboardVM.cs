using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class MotherboardVM
    {
        public string MotherCode { get; set; }
        public string MotherName { get; set; }
        public byte? MotherBrandId { get; set; }
        public string BrandName { get; set; }
        public int MotherPrice { get; set; }
        public short MotherQuantity { get; set; }
        public string MotherSocket { get; set; }
        public decimal MotherRate { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<byte[]> Image { get; set; }

        public virtual BrandVM MotherBrand { get; set; }
    }
}
