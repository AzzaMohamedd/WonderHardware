using BLL.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace DAL
{
    public class Wonder : IWonder
    {
        readonly WonderHardwareContext _wonder;

        public Wonder(WonderHardwareContext wonder)
        {
            _wonder = wonder;
        }
        // Get all Processor
        public IEnumerable<Processor> GetAll()
        {
            return _wonder.Processors.ToList();
        }

        public IEnumerable<BrandVM> GetBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.Join(_wonder.Processors,
                                        brand => brand.BrandId,
                                        processor => processor.ProBrandId,
                                        (brand, processor) => new BrandVM
                                        {
                                            BrandName = brand.BrandName,
                                            BrandNum = _wonder.Processors.Where(brandNum => brandNum.ProBrandId == brand.BrandId).Count()
                                        }

                ).Distinct();
            return brandVMs;
        }
    }
}
