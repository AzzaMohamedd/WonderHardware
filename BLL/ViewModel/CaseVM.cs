using System;
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
        public decimal? CaseRate { get; set; }
        public List<RateVM> RateCount { get; set; }
        public List<ReviewVM> Reviews { get; set; }
        public IEnumerable<byte[]> Image { get; set; }
        public virtual BrandVM CaseBrand { get; set; }

        //reviews pagination
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }

    }
}
