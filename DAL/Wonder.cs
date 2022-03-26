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
        WonderHardwareContext db = new WonderHardwareContext();
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

        public ProcessorVM ProcessorDetails(string code)
        {
            ProcessorVM obj = new ProcessorVM();
            var processor = db.Processors.Where(x => x.ProCode == code).FirstOrDefault();
            obj.ProCode = processor.ProCode;
            obj.ProName = processor.ProName;
            obj.ProBrandId = processor.ProBrandId;
            obj.ProPrice = processor.ProPrice;
            obj.ProQuantity = processor.ProQuantity;
            obj.ProCores = processor.ProCores;
            obj.ProSocket = processor.ProSocket;
            obj.ProThreads = processor.ProThreads;
            obj.ProBaseFreq = processor.ProBaseFreq;
            obj.ProMaxTurboFreq = processor.ProMaxTurboFreq;
            obj.ProLithography = processor.ProLithography;
            obj.ProRate = processor.ProRate;
            return obj;
        }
    }
}
