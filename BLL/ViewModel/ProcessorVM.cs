using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ProcessorVM
    {
        public string ProCode { get; set; }
        public string ProName { get; set; }
        public byte? ProBrandId { get; set; }
        public string BrandName { get; set; }
        public int ProPrice { get; set; }
        public short ProQuantity { get; set; }
        public byte ProCores { get; set; }
        public string ProSocket { get; set; }
        public byte ProThreads { get; set; }
        public double ProBaseFreq { get; set; }
        public double ProMaxTurboFreq { get; set; }
        public string ProLithography { get; set; }
        public byte? ProRate { get; set; }
        public IEnumerable<byte[]> Image { get; set; }


        public virtual BrandVM ProBrand { get; set; }
    }
}
