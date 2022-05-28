using BLL.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;
using System.Diagnostics;
using System.Linq.Dynamic.Core;

namespace DAL
{
    public class Wonder : IWonder
    {
        readonly WonderHardwareContext _wonder;

        public Wonder(WonderHardwareContext wonder)
        {
            _wonder = wonder;
        }


        #region Processor


        public List<ProcessorVM> GetAllProcessors()
        {
            List<Processor> Processor = _wonder.Processors.Where(x => x.IsAvailable == true).ToList();
            List<ProcessorVM> PR = new List<ProcessorVM>();
            foreach (var item in Processor)
            {
                ProcessorVM obj = new ProcessorVM();
                obj.ProCode = item.ProCode;
                obj.ProName = item.ProName;
                obj.ProBrandId = item.ProBrandId;
                obj.BrandName = item.ProBrand.BrandName;
                obj.ProPrice = item.ProPrice;
                obj.ProQuantity = item.ProQuantity;
                obj.ProCores = item.ProCores;
                obj.ProSocket = item.ProSocket;
                obj.ProThreads = item.ProThreads;
                obj.ProBaseFreq = item.ProBaseFreq;
                obj.ProMaxTurboFreq = item.ProMaxTurboFreq;
                obj.ProLithography = item.ProLithography;
                obj.ProRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.ProCode == item.ProCode && x.Rate != 0 && x.IsAvailable == true).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.ProRate = Rates.Sum() / Rates.Count();
                }
                PR.Add(obj);
            }
            return PR;
        }
        public IEnumerable<ProcessorVM> ProcessorPaginations(int PNum, int SNum)
        {

            var PvMs = GetAllProcessors();
            var Data = PvMs.Skip((PNum * SNum) - SNum).Take(SNum);
            return Data;
        }
        public IEnumerable<BrandVM> GetProcessorBrandNamesAndNumbers()
        {
            IList<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllProcessors(),
                                        brand => brand.BrandId,
                                        processor => processor.ProBrandId,
                                        (brand, processor) => new BrandVM
                                        {
                                            BrandName = brand.BrandName,
                                            BrandNum = _wonder.Processors.Where(brandNum => brandNum.ProBrandId == brand.BrandId).Count()
                                        }

                ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<ProcessorVM> GetProcessorProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id)
        {
            IList<ProcessorVM> processors = null;
            if (Id == 1)
            {
                processors = processorVMs.OrderByDescending(PVM => PVM.ProPrice).ToList();
            }
            else if (Id == 2)
            {
                processors = processorVMs.OrderBy(PVM => PVM.ProPrice).ToList();
            }
            else
            {
                processors = processorVMs.ToList();
            }
            return processors;
        }
        public IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max)
        {
            IEnumerable<ProcessorVM> Data = from Pro in GetAllProcessors()
                                            join brand in BName
                                            on Pro.BrandName.Trim() equals brand
                                            select new ProcessorVM { ProName = Pro.ProName, ProPrice = Pro.ProPrice };
            if (min == 0 && max == 0)
            {

                return GetProcessorProductsByPrice(Data, id).Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            }

            return GetProcessorProductsByPrice(Data, id).Where(pro => pro.ProPrice >= min && pro.ProPrice <= max).Skip((PNumber * SNumber) - SNumber).Take(SNumber);
        }
        public IEnumerable<ProcessorVM> ProcessorPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<ProcessorVM> processors
                                = GetAllProcessors().
                                 Where(processor => processor.ProPrice >= min && processor.ProPrice <= max).Skip((PSize * NPage) - PSize).Take(PSize);
            return processors;
        }

        public IEnumerable<ProcessorVM> ProcessorPaginByBrand(int PNum, int SNum, string[] BName)
        {
            var Products = GetAllProcessors().Skip((PNum * SNum) - SNum).Take(SNum);
            IEnumerable<ProcessorVM> Data = from Pro in Products
                                            join brand in BName
                       on Pro.BrandName.Trim() equals brand
                                            select new ProcessorVM { ProName = Pro.ProName, ProPrice = Pro.ProPrice };
            return Data.Distinct();
        }
        public IEnumerable<ProcessorVM> ProcessorPriceBrand(int PageNumber, int PageSize, int Id, string[] BName)
        {
            IEnumerable<ProcessorVM> Data = from Pro in GetAllProcessors()
                                            join brand in BName
                       on Pro.BrandName.Trim() equals brand
                                            select Pro;

            var get = Data.Skip((PageNumber * PageSize) - PageSize).Take(PageSize);
            IEnumerable<ProcessorVM> Products = null;
            if (Id == 1)
            {
                Products = get.OrderByDescending(PVM => PVM.ProPrice).ToList();
            }
            else
            {
                Products = get.OrderBy(PVM => PVM.ProPrice).ToList();
            }
            return Products;


        }
        public IEnumerable<ProcessorVM> GetProcessorDependentOnSort(int id)
        {
            if (id == 0)
            {
                return GetAllProcessors().ToList();
            }
            return GetProcessorProductsByPrice(GetAllProcessors(), id);
        }
        public IEnumerable<ProcessorVM> GetProcessorPriceDependentOnBrand(int min, int max, int sort)
        {
            IEnumerable<ProcessorVM> processors = null;
            if (min == 0 && max == 0)
            {

                processors = GetProcessorDependentOnSort(sort).ToList();
            }
            else
            {

                processors = GetAllProcessors().Where(processor => processor.ProPrice >= min && processor.ProPrice <= max);
            }
            return GetProcessorProductsByPrice(processors, sort);
        }
        #endregion


        #region MotherBoard

        public List<MotherboardVM> GetAllMotherboard()
        {
            List<Motherboard> Motherboard = _wonder.Motherboards.Where(x => x.IsAvailable == true).ToList();
            List<MotherboardVM> MB = new List<MotherboardVM>();
            foreach (var item in Motherboard)
            {
                MotherboardVM obj = new MotherboardVM();
                obj.MotherCode = item.MotherCode;
                obj.MotherName = item.MotherName;
                obj.MotherBrandId = item.MotherBrandId;
                obj.BrandName = item.MotherBrand.BrandName;
                obj.MotherPrice = item.MotherPrice;
                obj.MotherQuantity = item.MotherQuantity;
                obj.MotherSocket = item.MotherSocket;
                obj.MotherRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.MotherCode == item.MotherCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.MotherRate = Rates.Sum() / Rates.Count();
                }
                MB.Add(obj);
            }
            return MB;
        }

        public IEnumerable<MotherboardVM> MotherboardPaginations(int PNum, int SNum)
        {

            var PvMs = GetAllMotherboard();
            var Data = PvMs.Skip((PNum * SNum) - SNum).Take(SNum);
            return Data;
        }
        public IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers()
        {
            IList<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllMotherboard(),
                                        brand => brand.BrandId,
                                        month => month.MotherBrandId,
                                        (brand, month) => new BrandVM
                                        {
                                            BrandName = brand.BrandName,
                                            BrandNum = _wonder.Motherboards.Where(brandNum => brandNum.MotherBrandId == brand.BrandId).Count()
                                        }

                ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> motherboardVMs, int Id)
        {
            IList<MotherboardVM> motherboards = null;
            if (Id == 1)
            {
                motherboards = motherboardVMs.OrderByDescending(PVM => PVM.MotherPrice).ToList();
            }
            else if (Id == 2)
            {
                motherboards = motherboardVMs.OrderBy(PVM => PVM.MotherPrice).ToList();
            }
            else
            {
                motherboards = motherboardVMs.ToList();
            }
            return motherboards;
        }
        public IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max)
        {
            IEnumerable<MotherboardVM> Data = from moth in GetAllMotherboard()
                                              join brand in BName
                                              on moth.BrandName.Trim() equals brand
                                              select new MotherboardVM { MotherName = moth.MotherName, MotherPrice = moth.MotherPrice };
            if (min == 0 && max == 0)
            {

                return GetMotherboardProductsByPrice(Data, id).Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            }

            return GetMotherboardProductsByPrice(Data, id).Where(moth => moth.MotherPrice >= min && moth.MotherPrice <= max).Skip((PNumber * SNumber) - SNumber).Take(SNumber);
        }
        public IEnumerable<MotherboardVM> MotherboardPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<MotherboardVM> motherboards
                                = GetAllMotherboard().
                                 Where(motherboard => motherboard.MotherPrice >= min && motherboard.MotherPrice <= max).Skip((PSize * NPage) - PSize).Take(PSize);
            return motherboards;
        }

        public IEnumerable<MotherboardVM> MotherboardPaginByBrand(int PNum, int SNum, string[] BName)
        {
            var Products = GetAllMotherboard().Skip((PNum * SNum) - SNum).Take(SNum);
            IEnumerable<MotherboardVM> Data = from moth in Products
                                              join brand in BName
                         on moth.BrandName.Trim() equals brand
                                              select new MotherboardVM { MotherPrice = moth.MotherPrice, MotherName = moth.MotherName };
            return Data.Distinct();
        }
        public IEnumerable<MotherboardVM> MotherboardPriceBrand(int PageNumber, int PageSize, int Id, string[] BName)
        {
            IEnumerable<MotherboardVM> Data = from moth in GetAllMotherboard()
                                              join brand in BName
                         on moth.BrandName.Trim() equals brand
                                              select moth;

            var get = Data.Skip((PageNumber * PageSize) - PageSize).Take(PageSize);
            IEnumerable<MotherboardVM> Products = null;
            if (Id == 1)
            {
                Products = get.OrderByDescending(PVM => PVM.MotherPrice).ToList();
            }
            else
            {
                Products = get.OrderBy(PVM => PVM.MotherPrice).ToList();
            }
            return Products;
        }
        public IEnumerable<MotherboardVM> GetMotherboardDependentOnSort(int id)
        {
            if (id == 0)
            {
                return GetAllMotherboard().ToList();
            }
            return GetMotherboardProductsByPrice(GetAllMotherboard(), id);
        }
        public IEnumerable<MotherboardVM> GetMotherboardPriceDependentOnBrand(int min, int max, int sort)
        {
            IEnumerable<MotherboardVM> motherboards = null;
            if (min == 0 && max == 0)
            {

                motherboards = GetMotherboardDependentOnSort(sort).ToList();
            }
            else
            {

                motherboards = GetAllMotherboard().Where(moth => moth.MotherPrice >= min && moth.MotherPrice <= max);
            }
            return GetMotherboardProductsByPrice(motherboards, sort);
        }

        #endregion


        #region HDD

        public List<HddVM> GetAllHDD()
        {
            List<Hdd> HDD = _wonder.Hdds.Where(x => x.IsAvailable == true).ToList();
            List<HddVM> HD = new List<HddVM>();
            foreach (var item in HDD)
            {
                HddVM obj = new HddVM();
                obj.Hddcode = item.Hddcode;
                obj.Hddname = item.Hddname;
                obj.HddbrandId = item.HddbrandId;
                obj.BrandName = item.Hddbrand.BrandName;
                obj.Hddprice = item.Hddprice;
                obj.Hddquantity = item.Hddquantity;
                obj.Hddsize = item.Hddsize;
                obj.Hddrpm = item.Hddrpm;
                obj.Hddtype = item.Hddtype;
                obj.Hddrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Hddcode == item.Hddcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Hddrate = Rates.Sum() / Rates.Count();
                }
                HD.Add(obj);
            }
            return HD;
        }


        public IEnumerable<HddVM> HDDPaginations(int PNum, int SNum)
        {

            var PvMs = GetAllHDD();
            var Data = PvMs.Skip((PNum * SNum) - SNum).Take(SNum);
            return Data;
        }
        public IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers()
        {
            IList<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllHDD(),
                                        brand => brand.BrandId,
                                        hdd => hdd.HddbrandId,
                                        (brand, hdd) => new BrandVM
                                        {
                                            BrandName = brand.BrandName,
                                            BrandNum = _wonder.Hdds.Where(brandNum => brandNum.HddbrandId == brand.BrandId).Count()
                                        }

                ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }
        public IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> hddVMs, int Id)
        {
            IList<HddVM> hdds = null;
            if (Id == 1)
            {
                hdds = hddVMs.OrderByDescending(PVM => PVM.Hddprice).ToList();
            }
            else if (Id == 2)
            {
                hdds = hddVMs.OrderBy(PVM => PVM.Hddprice).ToList();
            }
            else
            {
                hdds = hddVMs.ToList();
            }
            return hdds;
        }
        public IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int PNumber, int SNumber, int id, int min, int max)
        {
            IEnumerable<HddVM> Data = from hdd in GetAllHDD()
                                      join brand in BName
                                      on hdd.BrandName.Trim() equals brand
                                      select new HddVM { Hddname = hdd.Hddname, Hddprice = hdd.Hddprice };
            if (min == 0 && max == 0)
            {

                return GetHDDProductsByPrice(Data, id).Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            }

            return GetHDDProductsByPrice(Data, id).Where(hdd => hdd.Hddprice >= min && hdd.Hddprice <= max).Skip((PNumber * SNumber) - SNumber).Take(SNumber);
        }
        public IEnumerable<HddVM> HDDPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<HddVM> hdds
                                = GetAllHDD().Where(hdd => hdd.Hddprice >= min && hdd.Hddprice <= max);
            return hdds.Skip((PSize * NPage) - PSize).Take(PSize);
        }

        public IEnumerable<HddVM> HDDPaginByBrand(int PNum, int SNum, string[] BName)
        {
            var Products = GetAllHDD().Skip((PNum * SNum) - SNum).Take(SNum);
            IEnumerable<HddVM> Data = from hdd in Products
                                      join brand in BName
                 on hdd.BrandName.Trim() equals brand
                                      select new HddVM { Hddname = hdd.Hddname, Hddprice = hdd.Hddprice };
            return Data.Distinct();
        }
        public IEnumerable<HddVM> HDDPriceBrand(int PageNumber, int PageSize, int Id, string[] BName)
        {
            IEnumerable<HddVM> Data = from hdd in GetAllHDD()
                                            join brand in BName
                       on hdd.BrandName.Trim() equals brand
                                            select hdd;

            var get = Data.Skip((PageNumber * PageSize) - PageSize).Take(PageSize);
            IEnumerable<HddVM> Products = null;
            if (Id == 1)
            {
                Products = get.OrderByDescending(PVM => PVM.Hddprice).ToList();
            }
            else
            {
                Products = get.OrderBy(PVM => PVM.Hddprice).ToList();
            }
            return Products;

        }
        public IEnumerable<HddVM> GetHDDDependentOnSort(int id)
        {
            if (id == 0)
            {
                return GetAllHDD().ToList();
            }
            return GetHDDProductsByPrice(GetAllHDD(), id);
        }
        public IEnumerable<HddVM> GetHDDPriceDependentOnBrand(int min, int max, int sort)
        {
            IEnumerable<HddVM> hdds = null;
            if (min == 0 && max == 0)
            {

                hdds = GetHDDDependentOnSort(sort).ToList();
            }
            else
            {

                hdds = GetAllHDD().Where(hdd => hdd.Hddprice >= min && hdd.Hddprice <= max);
            }
            return GetHDDProductsByPrice(hdds, sort);
        }
        #endregion


        #region RAM

        public List<RamVM> GetAllRAM()
        {
            List<Ram> Ram = _wonder.Rams.Where(x => x.IsAvailable == true).ToList();
            List<RamVM> RM = new List<RamVM>();
            foreach (var item in Ram)
            {
                RamVM obj = new RamVM();
                obj.RamCode = item.RamCode;
                obj.RamName = item.RamName;
                obj.RamBrandId = item.RamBrandId;
                obj.BrandName = item.RamBrand.BrandName;
                obj.RamPrice = item.RamPrice;
                obj.RamQuantity = item.RamQuantity;
                obj.RamSize = item.RamSize;
                obj.RamFrequency = item.RamFrequency;
                obj.RamType = item.RamType;
                obj.Ramkits = item.Ramkits;
                obj.RamRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.RamCode == item.RamCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.RamRate = Rates.Sum() / Rates.Count();
                }
                RM.Add(obj);
            }
            return RM;
        }

        public IEnumerable<RamVM> RAMPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<RamVM> RVMs = GetAllRAM().Skip(Startfromthisrecord).Take(SNum);
            return RVMs;
        }

        public IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllRAM(),
                                       brand => brand.BrandId,
                                       RAM => RAM.RamBrandId,
                                       (brand, RAM) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllRAM().Where(brandNum => brandNum.RamBrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id)
        {
            IList<RamVM> ram = null;
            if (Id == 1)
            {
                ram = RamVMs.OrderByDescending(RVM => RVM.RamPrice).ToList();
            }
            else
            {
                ram = RamVMs.OrderBy(RVM => RVM.RamPrice).ToList();
            }
            return ram;
        }

        public IEnumerable<RamVM> GetRAMProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllRAM().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<RamVM> Data = from ram in Products
                                      join brand in BName
                         on ram.BrandName equals brand
                                      select new RamVM { RamName = ram.RamName, RamPrice = ram.RamPrice };
            return Data.Distinct();
        }

        public IEnumerable<RamVM> RAMPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<RamVM> ramVMs
                                = GetAllRAM().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(ram => ram.RamPrice >= min && ram.RamPrice <= max)
                                 .Select(ramvm => new RamVM
                                 {
                                     RamPrice = ramvm.RamPrice,
                                     RamName = ramvm.RamName
                                 });
            return ramVMs;
        }
        #endregion


        #region SSD

        public List<SsdVM> GetAllSSD()
        {
            List<Ssd> Ssd = _wonder.Ssds.Where(x => x.IsAvailable == true).ToList();
            List<SsdVM> SD = new List<SsdVM>();
            foreach (var item in Ssd)
            {
                SsdVM obj = new SsdVM();
                obj.Ssdcode = item.Ssdcode;
                obj.Ssdname = item.Ssdname;
                obj.SsdbrandId = item.SsdbrandId;
                obj.BrandName = item.Ssdbrand.BrandName;
                obj.Ssdprice = item.Ssdprice;
                obj.Ssdquantity = item.Ssdquantity;
                obj.Ssdsize = item.Ssdsize;
                obj.Ssdinterface = item.Ssdinterface;
                obj.Ssdrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Ssdcode == item.Ssdcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Ssdrate = Rates.Sum() / Rates.Count();
                }
                SD.Add(obj);
            }
            return SD;
        }

        public IEnumerable<SsdVM> SSDPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<SsdVM> SVMs = GetAllSSD().Skip(Startfromthisrecord).Take(SNum);
            return SVMs;
        }

        public IEnumerable<BrandVM> GetSSDBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllSSD(),
                                       brand => brand.BrandId,
                                       ssd => ssd.SsdbrandId,
                                       (brand, ssd) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllSSD().Where(brandNum => brandNum.SsdbrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<SsdVM> GetSSDProductsByPrice(IEnumerable<SsdVM> ssdVMs, int Id)
        {
            IList<SsdVM> ssd = null;
            if (Id == 1)
            {
                ssd = ssdVMs.OrderByDescending(ssdvm => ssdvm.Ssdprice).ToList();
            }
            else
            {
                ssd = ssdVMs.OrderBy(ssdvm => ssdvm.Ssdprice).ToList();
            }
            return ssd;
        }

        public IEnumerable<SsdVM> GetSSDProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllSSD().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<SsdVM> Data = from ssd in Products
                                      join brand in BName
                         on ssd.BrandName.Trim() equals brand
                                      select new SsdVM { Ssdname = ssd.Ssdname, Ssdprice = ssd.Ssdprice };
            return Data.Distinct();
        }

        public IEnumerable<SsdVM> SSDPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<SsdVM> ssdVMs
                                = GetAllSSD().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(ssd => ssd.Ssdprice >= min && ssd.Ssdprice <= max)
                                 .Select(ssdvm => new SsdVM
                                 {
                                     Ssdprice = ssdvm.Ssdprice,
                                     Ssdname = ssdvm.Ssdname
                                 });
            return ssdVMs;
        }
        #endregion


        #region Graphics Card

        public List<GraphicsCardVM> GetAllCard()
        {
            List<GraphicsCard> GraphicsCard = _wonder.GraphicsCards.Where(x => x.IsAvailable == true).ToList();
            List<GraphicsCardVM> GC = new List<GraphicsCardVM>();
            foreach (var item in GraphicsCard)
            {
                GraphicsCardVM obj = new GraphicsCardVM();
                obj.Vgacode = item.Vgacode;
                obj.Vganame = item.Vganame;
                obj.VgabrandId = item.VgabrandId;
                obj.BrandName = item.Vgabrand.BrandName;
                obj.Vgaprice = item.Vgaprice;
                obj.Vgaquantity = item.Vgaquantity;
                obj.Vram = item.Vram;
                obj.IntermediateBrandId = item.IntermediateBrandId;
                obj.Vgarate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Vgacode == item.Vgacode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Vgarate = Rates.Sum() / Rates.Count();
                }
                GC.Add(obj);
            }
            return GC;
        }

        public IEnumerable<GraphicsCardVM> CardPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<GraphicsCardVM> CardVMs = GetAllCard().Skip(Startfromthisrecord).Take(SNum);
            return CardVMs;
        }

        public IEnumerable<BrandVM> GetCardVMBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllCard(),
                                       brand => brand.BrandId,
                                      Card => Card.VgabrandId,
                                       (brand, ssd) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllCard().Where(brandNum => brandNum.VgabrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<GraphicsCardVM> GetCardVMProductsByPrice(IEnumerable<GraphicsCardVM> CardVMVMs, int Id)
        {
            IList<GraphicsCardVM> CardVM = null;
            if (Id == 1)
            {
                CardVM = CardVMVMs.OrderByDescending(cardvm => cardvm.Vgaprice).ToList();
            }
            else
            {
                CardVM = CardVMVMs.OrderBy(cardvm => cardvm.Vgaprice).ToList();
            }
            return CardVM;
        }

        public IEnumerable<GraphicsCardVM> GetCardProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllCard().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<GraphicsCardVM> Data = from card in Products
                                               join brand in BName
                                  on card.BrandName.Trim() equals brand
                                               select new GraphicsCardVM { Vganame = card.Vganame, Vgaprice = card.Vgaprice };
            return Data.Distinct();
        }

        public IEnumerable<GraphicsCardVM> CardPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<GraphicsCardVM> CardVMs
                                = GetAllCard().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(card => card.Vgaprice >= min && card.Vgaprice <= max).Select(cardVm => new GraphicsCardVM()
                                 {
                                     Vgaprice = cardVm.Vgaprice,
                                     Vganame = cardVm.Vganame
                                 });
            return CardVMs;
        }
        #endregion


        #region Case

        public List<CaseVM> GetAllCase(string deleteddata = null)
        {
            List<Case> Case = new List<Case>();
            if (deleteddata == null)
                Case = _wonder.Cases.Where(x => x.IsAvailable == true).ToList();
            else
                Case = _wonder.Cases.Where(x => x.IsAvailable == false).ToList();

            List<CaseVM> CA = new List<CaseVM>();
            foreach (var item in Case)
            {
                CaseVM obj = new CaseVM();
                obj.CaseCode = item.CaseCode;
                obj.CaseName = item.CaseName;
                obj.CaseBrandId = item.CaseBrandId;
                obj.BrandName = item.CaseBrand.BrandName;
                obj.CasePrice = item.CasePrice;
                obj.CaseQuantity = item.CaseQuantity;
                obj.CaseFactorySize = item.CaseFactorySize;
                obj.IsAvailable = item.IsAvailable;
                obj.CaseRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.CaseCode == item.CaseCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.CaseRate = Rates.Sum() / Rates.Count();
                }
                CA.Add(obj);
            }
            return CA;
        }

        public IEnumerable<CaseVM> CasePaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<CaseVM> CaseVMs = GetAllCase().Skip(Startfromthisrecord).Take(SNum);
            return CaseVMs;
        }

        public IEnumerable<BrandVM> GetCaseVMBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllCase(),
                                       brand => brand.BrandId,
                                       Case => Case.CaseBrandId,
                                       (brand, cAse) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllCase().Where(brandNum => brandNum.CaseBrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<CaseVM> GetCaseVMProductsByPrice(IEnumerable<CaseVM> caseVMs, int Id)
        {
            IList<CaseVM> CaseVM = null;
            if (Id == 1)
            {
                CaseVM = caseVMs.OrderByDescending(Case => Case.CasePrice).ToList();
            }
            else
            {
                CaseVM = caseVMs.OrderBy(Case => Case.CasePrice).ToList();
            }
            return CaseVM;
        }

        public IEnumerable<CaseVM> GetCaseProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllCase().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<CaseVM> Data = from Cs in Products
                                       join brand in BName
                          on Cs.BrandName.Trim() equals brand
                                       select new CaseVM { CaseName = Cs.CaseName, CasePrice = Cs.CasePrice };
            return Data.Distinct();
        }

        public IEnumerable<CaseVM> CasePrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<CaseVM> CaseVMs
                                = GetAllCase().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(c => c.CasePrice >= min && c.CasePrice <= max)
                                 .Select(cvm => new CaseVM
                                 {
                                     CasePrice = cvm.CasePrice,
                                     CaseName = cvm.CaseName
                                 });
            return CaseVMs;
        }
        #endregion


        #region PowerSuply

        public List<PowerSupplyVM> GetAllPowerSuply()
        {
            List<PowerSupply> PowerSupply = _wonder.PowerSupplies.Where(x => x.IsAvailable == true).ToList();
            List<PowerSupplyVM> PS = new List<PowerSupplyVM>();
            foreach (var item in PowerSupply)
            {
                PowerSupplyVM obj = new PowerSupplyVM();
                obj.Psucode = item.Psucode;
                obj.Psuname = item.Psuname;
                obj.PsubrandId = item.PsubrandId;
                obj.BrandName = item.Psubrand.BrandName;
                obj.Psuprice = item.Psuprice;
                obj.Psuquantity = item.Psuquantity;
                obj.Psuwatt = item.Psuwatt;
                obj.Psucertificate = item.Psucertificate;
                obj.Psurate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Psucode == item.Psucode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Psurate = Rates.Sum() / Rates.Count();
                }
                PS.Add(obj);
            }
            return PS;
        }

        public IEnumerable<PowerSupplyVM> PowerSuplyPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<PowerSupplyVM> PowerSuplyVMs = GetAllPowerSuply().Skip(Startfromthisrecord).Take(SNum);
            return PowerSuplyVMs;
        }

        public IEnumerable<BrandVM> GetPowerSupplyBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllPowerSuply(),
                                       brand => brand.BrandId,
                                       PowerSupply => PowerSupply.PsubrandId,
                                       (brand, PowerSupply) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllPowerSuply().Where(brandNum => brandNum.PsubrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<PowerSupplyVM> GetPowerSupplyProductsByPrice(IEnumerable<PowerSupplyVM> PowerSupplyVMs, int Id)
        {
            IList<PowerSupplyVM> PowerSupplyVM = null;
            if (Id == 1)
            {
                PowerSupplyVM = PowerSupplyVMs.OrderByDescending(PSVM => PSVM.Psuprice).ToList();
            }
            else
            {
                PowerSupplyVM = PowerSupplyVMs.OrderBy(PSVM => PSVM.Psuprice).ToList();
            }
            return PowerSupplyVM;
        }

        public IEnumerable<PowerSupplyVM> GetPowerSupplyVMsProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllPowerSuply().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<PowerSupplyVM> Data = from PS in Products
                                              join brand in BName
                                 on PS.BrandName equals brand
                                              select new PowerSupplyVM { Psuname = PS.Psuname, Psuprice = PS.Psuprice };
            return Data.Distinct();
        }

        public IEnumerable<PowerSupplyVM> PSPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<PowerSupplyVM> PSVMs
                                = GetAllPowerSuply().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(ps => ps.Psuprice >= min && ps.Psuprice <= max)
                                 .Select(psvm => new PowerSupplyVM
                                 {
                                     Psuname = psvm.Psuname,
                                     Psuprice = psvm.Psuprice
                                 });
            return PSVMs;
        }
        #endregion


        #region NewProduct

        public List<CaseVM> GetNewCase(int userid)
        {
            List<Case> Case = _wonder.Cases.Where(x => x.IsAvailable == true).OrderByDescending(p => p.CaseCode).Take(5).ToList();
            List<CaseVM> CA = new List<CaseVM>();
            foreach (var item in Case)
            {
                CaseVM obj = new CaseVM();
                obj.CaseCode = item.CaseCode;
                obj.CaseName = item.CaseName;
                obj.CaseBrandId = item.CaseBrandId;
                obj.BrandName = item.CaseBrand.BrandName;
                obj.CasePrice = item.CasePrice;
                obj.CaseQuantity = item.CaseQuantity;
                obj.CaseFactorySize = item.CaseFactorySize;
                obj.WishList =  _wonder.WishLists.Any(x => x.CaseCode == item.CaseCode && x.UserId == userid && x.IsAdded==true);
                obj.CaseRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.CaseCode == item.CaseCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.CaseRate = Rates.Sum() / Rates.Count();
                }
                CA.Add(obj);
            }
            return CA;
        }

        public List<HddVM> GetNewHDD()
        {
            List<Hdd> HDD = _wonder.Hdds.Where(x => x.IsAvailable == true).OrderByDescending(p => p.Hddcode).Take(5).ToList();
            List<HddVM> HD = new List<HddVM>();
            foreach (var Hdd in HDD)
            {
                HddVM obj = new HddVM();
                obj.Hddcode = Hdd.Hddcode;
                obj.Hddname = Hdd.Hddname;
                obj.HddbrandId = Hdd.HddbrandId;
                obj.BrandName = Hdd.Hddbrand.BrandName;
                obj.Hddprice = Hdd.Hddprice;
                obj.Hddquantity = Hdd.Hddquantity;
                obj.Hddsize = Hdd.Hddsize;
                obj.Hddrpm = Hdd.Hddrpm;
                obj.Hddtype = Hdd.Hddtype;
                obj.Hddrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Hddcode == Hdd.Hddcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Hddrate = Rates.Sum() / Rates.Count();
                }
                HD.Add(obj);
            }
            return HD;
        }

        public List<MotherboardVM> GetNewMotherBoards()
        {
            List<Motherboard> Motherboard = _wonder.Motherboards.Where(x => x.IsAvailable == true).OrderByDescending(p => p.MotherCode).Take(5).ToList();
            List<MotherboardVM> MB = new List<MotherboardVM>();
            foreach (var item in Motherboard)
            {
                MotherboardVM obj = new MotherboardVM();
                obj.MotherCode = item.MotherCode;
                obj.MotherName = item.MotherName;
                obj.MotherBrandId = item.MotherBrandId;
                obj.BrandName = item.MotherBrand.BrandName;
                obj.MotherPrice = item.MotherPrice;
                obj.MotherQuantity = item.MotherQuantity;
                obj.MotherSocket = item.MotherSocket;
                obj.MotherRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.MotherCode == item.MotherCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.MotherRate = Rates.Sum() / Rates.Count();
                }
                MB.Add(obj);
            }
            return MB;
        }

        public List<ProcessorVM> GetNewProcessors()
        {
            List<Processor> Processor = _wonder.Processors.Where(x => x.IsAvailable == true).OrderByDescending(p => p.ProCode).Take(5).ToList();
            List<ProcessorVM> PR = new List<ProcessorVM>();
            foreach (var processor in Processor)
            {
                ProcessorVM obj = new ProcessorVM();
                obj.ProCode = processor.ProCode;
                obj.ProName = processor.ProName;
                obj.ProBrandId = processor.ProBrandId;
                obj.BrandName = processor.ProBrand.BrandName;
                obj.ProPrice = processor.ProPrice;
                obj.ProQuantity = processor.ProQuantity;
                obj.ProCores = processor.ProCores;
                obj.ProSocket = processor.ProSocket;
                obj.ProThreads = processor.ProThreads;
                obj.ProBaseFreq = processor.ProBaseFreq;
                obj.ProMaxTurboFreq = processor.ProMaxTurboFreq;
                obj.ProLithography = processor.ProLithography;
                obj.ProRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.ProCode == processor.ProCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.ProRate = Rates.Sum() / Rates.Count();
                }
                PR.Add(obj);
            }
            return PR;
        }

        public List<PowerSupplyVM> GetNewPSU()
        {
            List<PowerSupply> PowerSupply = _wonder.PowerSupplies.Where(x => x.IsAvailable == true).OrderByDescending(p => p.Psucode).Take(5).ToList();
            List<PowerSupplyVM> PS = new List<PowerSupplyVM>();
            foreach (var powersupply in PowerSupply)
            {
                PowerSupplyVM obj = new PowerSupplyVM();
                obj.Psucode = powersupply.Psucode;
                obj.Psuname = powersupply.Psuname;
                obj.PsubrandId = powersupply.PsubrandId;
                obj.BrandName = powersupply.Psubrand.BrandName;
                obj.Psuprice = powersupply.Psuprice;
                obj.Psuquantity = powersupply.Psuquantity;
                obj.Psuwatt = powersupply.Psuwatt;
                obj.Psucertificate = powersupply.Psucertificate;
                obj.Psurate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Psucode == powersupply.Psucode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Psurate = Rates.Sum() / Rates.Count();
                }
                PS.Add(obj);
            }
            return PS;
        }

        public List<RamVM> GetNewRam()
        {
            List<Ram> Ram = _wonder.Rams.Where(x => x.IsAvailable == true).OrderByDescending(p => p.RamCode).Take(5).ToList();
            List<RamVM> RM = new List<RamVM>();
            foreach (var ram in Ram)
            {
                RamVM obj = new RamVM();
                obj.RamCode = ram.RamCode;
                obj.RamName = ram.RamName;
                obj.RamBrandId = ram.RamBrandId;
                obj.BrandName = ram.RamBrand.BrandName;
                obj.RamPrice = ram.RamPrice;
                obj.RamQuantity = ram.RamQuantity;
                obj.RamSize = ram.RamSize;
                obj.RamFrequency = ram.RamFrequency;
                obj.RamType = ram.RamType;
                obj.Ramkits = ram.Ramkits;
                obj.RamRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.RamCode == ram.RamCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.RamRate = Rates.Sum() / Rates.Count();
                }
                RM.Add(obj);
            }
            return RM;
        }

        public List<SsdVM> GetNewSSD()
        {
            List<Ssd> Ssd = _wonder.Ssds.Where(x => x.IsAvailable == true).OrderByDescending(p => p.Ssdcode).Take(5).ToList();
            List<SsdVM> SD = new List<SsdVM>();
            foreach (var ssd in Ssd)
            {
                SsdVM obj = new SsdVM();
                obj.Ssdcode = ssd.Ssdcode;
                obj.Ssdname = ssd.Ssdname;
                obj.SsdbrandId = ssd.SsdbrandId;
                obj.BrandName = ssd.Ssdbrand.BrandName;
                obj.Ssdprice = ssd.Ssdprice;
                obj.Ssdquantity = ssd.Ssdquantity;
                obj.Ssdsize = ssd.Ssdsize;
                obj.Ssdinterface = ssd.Ssdinterface;
                obj.Ssdrate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Ssdcode == ssd.Ssdcode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Ssdrate = Rates.Sum() / Rates.Count();
                }
                SD.Add(obj);
            }
            return SD;
        }

        public List<GraphicsCardVM> GetNewVGA()
        {
            List<GraphicsCard> GraphicsCard = _wonder.GraphicsCards.Where(x => x.IsAvailable == true).OrderByDescending(p => p.Vgacode).Take(5).ToList();
            List<GraphicsCardVM> GC = new List<GraphicsCardVM>();
            foreach (var graphicscard in GraphicsCard)
            {
                GraphicsCardVM obj = new GraphicsCardVM();
                obj.Vgacode = graphicscard.Vgacode;
                obj.Vganame = graphicscard.Vganame;
                obj.VgabrandId = graphicscard.VgabrandId;
                obj.BrandName = graphicscard.Vgabrand.BrandName;
                obj.Vgaprice = graphicscard.Vgaprice;
                obj.Vgaquantity = graphicscard.Vgaquantity;
                obj.Vram = graphicscard.Vram;
                obj.IntermediateBrandId = graphicscard.IntermediateBrandId;
                obj.Vgarate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.Vgacode == graphicscard.Vgacode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.Vgarate = Rates.Sum() / Rates.Count();
                }
                GC.Add(obj);
            }
            return GC;
        }

        #endregion


        #region TopSelling
        public List<MotherboardVM> GetTopMothers()
        {
            List<MotherboardVM> topProducts = new List<MotherboardVM>();
            var codes = (from O in _wonder.Sales
                         where O.MotherCode != null
                         group O by O.MotherCode into grp
                         orderby grp.Count() descending
                         select grp.Key.ToString()).Take(5);
            foreach (var code in codes)
            {
                MotherboardVM obj = new MotherboardVM();
                var item = _wonder.Motherboards.Where(x => x.MotherCode == code).Select(x => x).FirstOrDefault();
                obj.MotherCode = item.MotherCode;
                obj.MotherName = item.MotherName;
                obj.MotherPrice = item.MotherPrice;
                obj.MotherQuantity = item.MotherQuantity;
                obj.MotherRate = 0;
                //Total Rate from Reviews
                List<decimal> Rates = _wonder.Reviews.Where(x => x.MotherCode == item.MotherCode && x.IsAvailable == true && x.Rate != 0).Select(x => x.Rate).ToList();
                if (Rates.Count() != 0)
                {
                    obj.MotherRate = Rates.Sum() / Rates.Count();
                }
                topProducts.Add(obj);
            }
            return topProducts;
        }

        #endregion


        #region ProductDetails

        public CaseVM CaseDetails(string code)
        {
            CaseVM obj = new CaseVM();

            var product = _wonder.Cases.Where(x => x.CaseCode == code && x.IsAvailable == true).FirstOrDefault();
            if (product != null)
                obj.CaseCode = product.CaseCode;
            obj.CaseName = product.CaseName;
            obj.CaseBrandId = product.CaseBrandId;
            obj.BrandName = product.CaseBrand.BrandName;
            obj.CasePrice = product.CasePrice;
            obj.CaseQuantity = product.CaseQuantity;
            obj.CaseFactorySize = product.CaseFactorySize;
            obj.CaseRate = 0;

            int maxRows = 3;

            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.CaseCode == code && x.IsAvailable == true).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();

            double pageCount = (double)(Data.Count() / Convert.ToDecimal(maxRows));
            obj.PageCount = (int)Math.Ceiling(pageCount);

            obj.CurrentPageIndex = 1;

            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.CaseCode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.CaseRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.CaseRate = obj.CaseRate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        public GraphicsCardVM GraphicsCardDetails(string code)
        {
            GraphicsCardVM obj = new GraphicsCardVM();
            var GraphicsCard = _wonder.GraphicsCards.Where(x => x.Vgacode == code).FirstOrDefault();
            obj.Vgacode = GraphicsCard.Vgacode;
            obj.Vganame = GraphicsCard.Vganame;
            obj.VgabrandId = GraphicsCard.VgabrandId;
            obj.BrandName = GraphicsCard.Vgabrand.BrandName;
            obj.Vgaprice = GraphicsCard.Vgaprice;
            obj.Vgaquantity = GraphicsCard.Vgaquantity;
            obj.Vram = GraphicsCard.Vram;
            obj.IntermediateBrandId = GraphicsCard.IntermediateBrandId;
            obj.Vgarate = 0;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.Vgacode == code).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.Vgacode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.Vgarate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Vgarate = obj.Vgarate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        public HddVM HddDetails(string code)
        {
            HddVM obj = new HddVM();
            var Hdd = _wonder.Hdds.Where(x => x.Hddcode == code).FirstOrDefault();
            obj.Hddcode = Hdd.Hddcode;
            obj.Hddname = Hdd.Hddname;
            obj.HddbrandId = Hdd.HddbrandId;
            obj.BrandName = Hdd.Hddbrand.BrandName;
            obj.Hddprice = Hdd.Hddprice;
            obj.Hddquantity = Hdd.Hddquantity;
            obj.Hddsize = Hdd.Hddsize;
            obj.Hddrpm = Hdd.Hddrpm;
            obj.Hddtype = Hdd.Hddtype;
            obj.Hddrate = 0;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.Hddcode == code).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.Hddcode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.Hddrate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Hddrate = obj.Hddrate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        public MotherboardVM MotherboardDetails(string code)
        {
            MotherboardVM obj = new MotherboardVM();
            var Motherboard = _wonder.Motherboards.Where(x => x.MotherCode == code).FirstOrDefault();
            obj.MotherCode = Motherboard.MotherCode;
            obj.MotherName = Motherboard.MotherName;
            obj.MotherBrandId = Motherboard.MotherBrandId;
            obj.BrandName = Motherboard.MotherBrand.BrandName;
            obj.MotherPrice = Motherboard.MotherPrice;
            obj.MotherQuantity = Motherboard.MotherQuantity;
            obj.MotherSocket = Motherboard.MotherSocket;
            obj.MotherRate = 0;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.MotherCode == code).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.MotherCode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.MotherRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.MotherRate = obj.MotherRate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        public PowerSupplyVM PowerSupplyDetails(string code)
        {
            PowerSupplyVM obj = new PowerSupplyVM();
            var PowerSupply = _wonder.PowerSupplies.Where(x => x.Psucode == code).FirstOrDefault();
            obj.Psucode = PowerSupply.Psucode;
            obj.Psuname = PowerSupply.Psuname;
            obj.PsubrandId = PowerSupply.PsubrandId;
            obj.BrandName = PowerSupply.Psubrand.BrandName;
            obj.Psuprice = PowerSupply.Psuprice;
            obj.Psuquantity = PowerSupply.Psuquantity;
            obj.Psuwatt = PowerSupply.Psuwatt;
            obj.Psucertificate = PowerSupply.Psucertificate;
            obj.Psurate = 0;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.Psucode == code).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.Psucode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.Psurate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Psurate = obj.Psurate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        public ProcessorVM ProcessorDetails(string code)
        {
            ProcessorVM obj = new ProcessorVM();
            var processor = _wonder.Processors.Where(x => x.ProCode == code).FirstOrDefault();
            obj.ProCode = processor.ProCode;
            obj.ProName = processor.ProName;
            obj.ProBrandId = processor.ProBrandId;
            obj.BrandName = processor.ProBrand.BrandName;
            obj.ProPrice = processor.ProPrice;
            obj.ProQuantity = processor.ProQuantity;
            obj.ProCores = processor.ProCores;
            obj.ProSocket = processor.ProSocket;
            obj.ProThreads = processor.ProThreads;
            obj.ProBaseFreq = processor.ProBaseFreq;
            obj.ProMaxTurboFreq = processor.ProMaxTurboFreq;
            obj.ProLithography = processor.ProLithography;
            obj.ProRate = 0;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.ProCode == code).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.ProCode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.ProRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.ProRate = obj.ProRate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        public RamVM RamDetails(string code)
        {
            RamVM obj = new RamVM();
            var Ram = _wonder.Rams.Where(x => x.RamCode == code).FirstOrDefault();
            obj.RamCode = Ram.RamCode;
            obj.RamName = Ram.RamName;
            obj.RamBrandId = Ram.RamBrandId;
            obj.BrandName = Ram.RamBrand.BrandName;
            obj.RamPrice = Ram.RamPrice;
            obj.RamQuantity = Ram.RamQuantity;
            obj.RamSize = Ram.RamSize;
            obj.RamFrequency = Ram.RamFrequency;
            obj.RamType = Ram.RamType;
            obj.Ramkits = Ram.Ramkits;
            obj.RamRate = 0;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.RamCode == code).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.RamCode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.RamRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.RamRate = obj.RamRate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        public SsdVM SsdDetails(string code)
        {
            SsdVM obj = new SsdVM();
            var Ssd = _wonder.Ssds.Where(x => x.Ssdcode == code).FirstOrDefault();
            obj.Ssdcode = Ssd.Ssdcode;
            obj.Ssdname = Ssd.Ssdname;
            obj.SsdbrandId = Ssd.SsdbrandId;
            obj.BrandName = Ssd.Ssdbrand.BrandName;
            obj.Ssdprice = Ssd.Ssdprice;
            obj.Ssdquantity = Ssd.Ssdquantity;
            obj.Ssdsize = Ssd.Ssdsize;
            obj.Ssdinterface = Ssd.Ssdinterface;
            obj.Ssdrate = 0;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.Ssdcode == code).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.Ssdcode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.Ssdrate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.Ssdrate = obj.Ssdrate / Data.Where(x => x.Rate != 0).Count();
            }
            var ratecount = from O in Data
                            group O by O.Rate into grp
                            select new { Rate = grp.Key, Count = grp.Count() };
            List<RateVM> ratecount2 = new List<RateVM>();
            foreach (var item in ratecount)
            {
                RateVM RC = new RateVM();
                RC.Rate = item.Rate;
                RC.Count = item.Count;
                ratecount2.Add(RC);
            }
            obj.RateCount = ratecount2;
            return obj;
        }

        #endregion

        #region comments Pagination
        public CaseVM CaseCommentsPagination(string code, int currentPageIndex)
        {
            CaseVM obj = new CaseVM();

            var Case = _wonder.Cases.Where(x => x.CaseCode == code && x.IsAvailable == true).FirstOrDefault();
            obj.CaseCode = Case.CaseCode;
            obj.CaseName = Case.CaseName;
            obj.CaseBrandId = Case.CaseBrandId;
            obj.BrandName = Case.CaseBrand.BrandName;
            obj.CasePrice = Case.CasePrice;
            obj.CaseQuantity = Case.CaseQuantity;
            obj.CaseFactorySize = Case.CaseFactorySize;
            obj.CaseRate = 0;

            int maxRows = 3;
            List<Review> Data = _wonder.Reviews.Select(X => X).Where(x => x.CaseCode == code && x.IsAvailable == true).Skip((currentPageIndex - 1) * maxRows).Take(maxRows).ToList();
            List<ReviewVM> reviews = new List<ReviewVM>();

            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ProductCode = item.CaseCode;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
                obj.CaseRate += item.Rate;
            }
            obj.Reviews = reviews;
            if (Data.Where(x => x.Rate != 0).Count() != 0)
            {
                obj.CaseRate = obj.CaseRate / Data.Where(x => x.Rate != 0).Count();
            }

            return obj;
        }
        #endregion

        #region ProductsExceptOne

        public List<CaseVM> GetCaseExceptOne(string code)
        {
            List<CaseVM> CA = GetAllCase();
            foreach (var item in CA.ToList())
            {
                if (item.CaseCode == code)
                {
                    CA.Remove(item);
                }
            }
            return CA;
        }

        public List<HddVM> GetHDDExceptOne(string code)
        {
            List<HddVM> HD = GetAllHDD();
            foreach (var item in HD.ToList())
            {
                if (item.Hddcode == code)
                {
                    HD.Remove(item);
                }
            }
            return HD;
        }

        public List<MotherboardVM> GetMotherBoardsExceptOne(string code)
        {
            List<MotherboardVM> MB = GetAllMotherboard();
            foreach (var item in MB.ToList())
            {
                if (item.MotherCode == code)
                {
                    MB.Remove(item);
                }
            }
            return MB;
        }

        public List<ProcessorVM> GetProcessorsExceptOne(string code)
        {
            List<ProcessorVM> PR = GetAllProcessors();
            foreach (var item in PR.ToList())
            {
                if (item.ProCode == code)
                {
                    PR.Remove(item);
                }
            }
            return PR;
        }

        public List<PowerSupplyVM> GetPSUExceptOne(string code)
        {
            List<PowerSupplyVM> PS = GetAllPowerSuply();
            foreach (var item in PS.ToList())
            {
                if (item.Psucode == code)
                {
                    PS.Remove(item);
                }
            }
            return PS;
        }

        public List<RamVM> GetRamExceptOne(string code)
        {
            List<RamVM> RM = GetAllRAM();
            foreach (var item in RM.ToList())
            {
                if (item.RamCode == code)
                {
                    RM.Remove(item);
                }
            }
            return RM;
        }

        public List<SsdVM> GetSSDExceptOne(string code)
        {
            List<SsdVM> SD = GetAllSSD();
            foreach (var item in SD.ToList())
            {
                if (item.Ssdcode == code)
                {
                    SD.Remove(item);
                }
            }
            return SD;
        }

        public List<GraphicsCardVM> GetVGAExceptOne(string code)
        {
            List<GraphicsCardVM> GC = GetAllCard();
            foreach (var item in GC.ToList())
            {
                if (item.Vgacode == code)
                {
                    GC.Remove(item);
                }
            }
            return GC;
        }

        #endregion


        #region CheckOut

        public string CheckOrderCreateAcc(UserVM UserData, SalesVM[] OrderData)
        {
            User Uobj = new User();
            if (_wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).FirstOrDefault() == null)
            {
                Uobj.FirstName = UserData.FName;
                Uobj.LastName = UserData.LName;
                Uobj.Password = UserData.Password;
                Uobj.Phone = UserData.Telephone;
                _wonder.Users.Add(Uobj);
                _wonder.SaveChanges();
                var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone).Select(x => x.UserId).FirstOrDefault();

                foreach (var item in OrderData)
                {
                    Sale Sobj = new Sale();

                    Sobj.UserId = userid;
                    Sobj.Address = item.City + " , " + item.Address;
                    if (item.ProductCode.StartsWith("S"))
                    {
                        Sobj.Ssdcode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("R"))
                    {
                        Sobj.RamCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("C"))
                    {
                        Sobj.CaseCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("V"))
                    {
                        Sobj.Vgacode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("PS"))
                    {
                        Sobj.Psucode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("Pr"))
                    {
                        Sobj.ProCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("M"))
                    {
                        Sobj.MotherCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("H"))
                    {
                        Sobj.Hddcode = item.ProductCode;
                    }

                    Sobj.ProductQuantity = item.ProductQuantity;
                    Sobj.TotalPrice = item.TotalPrice;
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                }
                //Account has been created &Order checked successfully.
                return "success";
            }
            else
            {
                return "This phone is already exist";
            }
        }

        public String CheckOrderSignIn(UserVM UserData, SalesVM[] OrderData)
        {
            User Uobj = new User();

            var resultMsg = "";

            var userid = _wonder.Users.Where(x => x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
            var X = _wonder.Users.Where(x => x.Password == UserData.Password && x.Phone == UserData.Telephone && x.IsAdmin == false).Select(x => x.UserId).FirstOrDefault();
            if (X != 0)
            {
                foreach (var item in OrderData)
                {
                    Sale Sobj = new Sale();

                    Sobj.UserId = userid;

                    if (item.ProductCode.StartsWith("S"))
                    {
                        Sobj.Ssdcode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("R"))
                    {
                        Sobj.RamCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("C"))
                    {
                        Sobj.CaseCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("V"))
                    {
                        Sobj.Vgacode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("PS"))
                    {
                        Sobj.Psucode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("Pr"))
                    {
                        Sobj.ProCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("M"))
                    {
                        Sobj.MotherCode = item.ProductCode;
                    }
                    else if (item.ProductCode.StartsWith("H"))
                    {
                        Sobj.Hddcode = item.ProductCode;
                    }
                    Sobj.ProductQuantity = item.ProductQuantity;
                    Sobj.TotalPrice = item.TotalPrice;
                    if (item.City != null && item.Address != null)
                    {
                        Sobj.Address = item.City + " , " + item.Address;
                        Sobj.DateAndTime = DateTime.Now;
                        _wonder.Sales.Add(Sobj);
                        _wonder.SaveChanges();
                        //Order checked successfully at the address that user entered.
                        resultMsg = "success checked new address";
                    }
                    else
                    {
                        Sobj.Address = _wonder.Sales.Where(x => x.UserId == userid).OrderByDescending(x => x.DateAndTime).Take(1).Select(x => x.Address).FirstOrDefault();
                        Sobj.DateAndTime = DateTime.Now;
                        _wonder.Sales.Add(Sobj);
                        _wonder.SaveChanges();
                        //Order checked successfully at the same address checked before.
                        resultMsg = "success checked old address";
                    }
                }

                return resultMsg;
            }
            else if (userid == 0)
            {
                return "failed phone";
            }
            else if (X == 0)
            {
                return "failed password";
            }
            else
            {
                //Phone or password isn't correct or both !!
                return "failed phone&pass";
            }
        }

        public String CheckOrder(SalesVM[] OrderData)
        {
            //already signed in
            var resultMsg = "";

            foreach (var item in OrderData)
            {
                Sale Sobj = new Sale();
                Sobj.UserId = item.UserID;
                if (item.ProductCode.StartsWith("S"))
                {
                    Sobj.Ssdcode = item.ProductCode;
                }
                else if (item.ProductCode.StartsWith("R"))
                {
                    Sobj.RamCode = item.ProductCode;
                }
                else if (item.ProductCode.StartsWith("C"))
                {
                    Sobj.CaseCode = item.ProductCode;
                }
                else if (item.ProductCode.StartsWith("V"))
                {
                    Sobj.Vgacode = item.ProductCode;
                }
                else if (item.ProductCode.StartsWith("PS"))
                {
                    Sobj.Psucode = item.ProductCode;
                }
                else if (item.ProductCode.StartsWith("Pr"))
                {
                    Sobj.ProCode = item.ProductCode;
                }
                else if (item.ProductCode.StartsWith("M"))
                {
                    Sobj.MotherCode = item.ProductCode;
                }
                else if (item.ProductCode.StartsWith("H"))
                {
                    Sobj.Hddcode = item.ProductCode;
                }
                Sobj.ProductQuantity = item.ProductQuantity;
                Sobj.TotalPrice = item.TotalPrice;
                if (item.City != null && item.Address != null)
                {
                    //add address for the first time Or add another address
                    Sobj.Address = item.City + " , " + item.Address;
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                    //Order checked successfully at the address that user entered.
                    resultMsg = "success checked new address";
                }
                else
                {
                    //get the oldest address from sales table
                    Sobj.Address = _wonder.Sales.Where(x => x.UserId == item.UserID).OrderByDescending(x => x.DateAndTime).Take(1).Select(x => x.Address).FirstOrDefault();
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                    //Order checked successfully at the same address checked before.
                    resultMsg = "success checked old address";
                }
            }
            return resultMsg;
        }
        #endregion


        #region WishList
        public List<WishListVM> GetWishList(int userid)
        {
            List<WishListVM> WishList = new List<WishListVM>();
            var MothersCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.MotherCode != null)).Select(x => x).ToList();
            foreach (var item in MothersCodes)
            {
                //if (_wonder.Motherboards.Where(x => x.MotherCode == item.MotherCode).Select(x => x.IsAvailable).FirstOrDefault() == true)
                WishListVM obj = new WishListVM();
                obj.ProductName = item.MotherCodeNavigation.MotherName;
                obj.ProductPrice = item.MotherCodeNavigation.MotherPrice;
                obj.ProductCode = item.MotherCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var CasesCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.CaseCode != null)).Select(x => x).ToList();
            foreach (var item in CasesCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.CaseCodeNavigation.CaseName;
                obj.ProductPrice = item.CaseCodeNavigation.CasePrice;
                obj.ProductCode = item.CaseCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            var VGAsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Vgacode != null)).Select(x => x).ToList();
            foreach (var item in VGAsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.VgacodeNavigation.Vganame;
                obj.ProductPrice = item.VgacodeNavigation.Vgaprice;
                obj.ProductCode = item.Vgacode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var HDDsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Hddcode != null)).Select(x => x).ToList();
            foreach (var item in HDDsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.HddcodeNavigation.Hddname;
                obj.ProductPrice = item.HddcodeNavigation.Hddprice;
                obj.ProductCode = item.Hddcode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var PSUsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Psucode != null)).Select(x => x).ToList();
            foreach (var item in PSUsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.PsucodeNavigation.Psuname;
                obj.ProductPrice = item.PsucodeNavigation.Psuprice;
                obj.ProductCode = item.Psucode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var ProsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.ProCode != null)).Select(x => x).ToList();
            foreach (var item in ProsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.ProCodeNavigation.ProName;
                obj.ProductPrice = item.ProCodeNavigation.ProPrice;
                obj.ProductCode = item.ProCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var RAMsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.RamCode != null)).Select(x => x).ToList();
            foreach (var item in RAMsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.RamCodeNavigation.RamName;
                obj.ProductPrice = item.RamCodeNavigation.RamPrice;
                obj.ProductCode = item.RamCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }

            var SSDsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.IsAdded == true) && (x.Ssdcode != null)).Select(x => x).ToList();
            foreach (var item in SSDsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.SsdcodeNavigation.Ssdname;
                obj.ProductPrice = item.SsdcodeNavigation.Ssdprice;
                obj.ProductCode = item.Ssdcode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            return WishList;
        }

        public string DeletefromWL(string ProductCode, int userid)
        {
            WishList productRow = _wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode) && x.IsAdded == true).FirstOrDefault();

            if ((ProductCode != null) && productRow != null)
            {
                productRow.IsAdded = false;
                _wonder.SaveChanges();
                return "Deleted Done";
            }
            else
            {
                return "Error";
            }
        }
        public string CheckfromWL(string ProductCode, int userid)
        {
            WishList item = _wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode)).FirstOrDefault();
            if ((ProductCode != null) && (item != null))
            {
                //موجود ف الداتابيز
                if (item.IsAdded == true)
                {
                    return "this product is choosed";
                }
                else
                {
                    return "product not choosed";
                }
            }
            else
            {
                return "product isn't exist";
            }


        }
        public string AddToWL(string ProductCode, int userid)
        {
            WishList productRow = _wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode) && x.IsAdded == false).FirstOrDefault();

            //هاجي هنا لو القلب مش ملون 
            if ((ProductCode != null) && (_wonder.WishLists.Where(x => x.UserId == userid && (x.Ssdcode == ProductCode || x.RamCode == ProductCode || x.CaseCode == ProductCode || x.Vgacode == ProductCode || x.Psucode == ProductCode || x.ProCode == ProductCode || x.MotherCode == ProductCode || x.Hddcode == ProductCode)).FirstOrDefault() == null))
            {
                //في حالة ان البرودكت مش موجود ف الداتابيز خالص 
                WishList Row = new WishList();
                if (ProductCode.StartsWith("S"))
                {
                    Row.Ssdcode = ProductCode;
                }
                else if (ProductCode.StartsWith("R"))
                {
                    Row.RamCode = ProductCode;
                }
                else if (ProductCode.StartsWith("C"))
                {
                    Row.CaseCode = ProductCode;
                }
                else if (ProductCode.StartsWith("V"))
                {
                    Row.Vgacode = ProductCode;
                }
                else if (ProductCode.StartsWith("PS"))
                {
                    Row.Psucode = ProductCode;
                }
                else if (ProductCode.StartsWith("Pr"))
                {
                    Row.ProCode = ProductCode;
                }
                else if (ProductCode.StartsWith("M"))
                {
                    Row.MotherCode = ProductCode;
                }
                else if (ProductCode.StartsWith("H"))
                {
                    Row.Hddcode = ProductCode;
                }
                Row.UserId = userid;
                Row.IsAdded = true;
                _wonder.WishLists.Add(Row);
                if (_wonder.SaveChanges() != 0)
                {
                    return "Saved Done";
                }
                else
                {
                    return "Error";
                }
            }
            else if ((ProductCode != null) && productRow != null)
            {
                //في حالة ان البرودكت موجود ف الداتابيز بس الأديد ب فولس 

                productRow.IsAdded = true;
                _wonder.SaveChanges();
                return "Saved Done";
            }
            return "something wrong";

        }
        #endregion


        #region Search

        public List<Search> SearchProduct(string src)
        {
            List<Search> obj = new List<Search>();
            var MotherBoard = _wonder.Motherboards.Select(i => new { ProductCode = i.MotherCode, ProductName = i.MotherName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in MotherBoard)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            var Processor = _wonder.Processors.Select(i => new { ProductCode = i.ProCode, ProductName = i.ProName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Processor)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            var HDD = _wonder.Hdds.Select(i => new { ProductCode = i.Hddcode, ProductName = i.Hddname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in HDD)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            var Ram = _wonder.Rams.Select(i => new { ProductCode = i.RamCode, ProductName = i.RamName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Ram)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            var VGA = _wonder.GraphicsCards.Select(i => new { ProductCode = i.Vgacode, ProductName = i.Vganame }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in VGA)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            var SSD = _wonder.Ssds.Select(i => new { ProductCode = i.Ssdcode, ProductName = i.Ssdname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in SSD)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            var PSU = _wonder.PowerSupplies.Select(i => new { ProductCode = i.Psucode, ProductName = i.Psuname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in PSU)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            var Case = _wonder.Cases.Select(i => new { ProductCode = i.CaseCode, ProductName = i.CaseName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Case)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchMotherBoard(string src)
        {
            List<Search> obj = new List<Search>();
            var MotherBoard = _wonder.Motherboards.Select(i => new { ProductCode = i.MotherCode, ProductName = i.MotherName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in MotherBoard)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchProcessor(string src)
        {
            List<Search> obj = new List<Search>();
            var Processor = _wonder.Processors.Select(i => new { ProductCode = i.ProCode, ProductName = i.ProName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Processor)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchRam(string src)
        {
            List<Search> obj = new List<Search>();
            var Ram = _wonder.Rams.Select(i => new { ProductCode = i.RamCode, ProductName = i.RamName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Ram)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchSSD(string src)
        {
            List<Search> obj = new List<Search>();
            var SSD = _wonder.Ssds.Select(i => new { ProductCode = i.Ssdcode, ProductName = i.Ssdname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in SSD)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchHDD(string src)
        {
            List<Search> obj = new List<Search>();
            var HDD = _wonder.Hdds.Select(i => new { ProductCode = i.Hddcode, ProductName = i.Hddname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in HDD)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchCase(string src)
        {
            List<Search> obj = new List<Search>();
            var Case = _wonder.Cases.Select(i => new { ProductCode = i.CaseCode, ProductName = i.CaseName }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in Case)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchPowerSupply(string src)
        {
            List<Search> obj = new List<Search>();
            var PSU = _wonder.PowerSupplies.Select(i => new { ProductCode = i.Psucode, ProductName = i.Psuname }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in PSU)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        public List<Search> SearchVGA(string src)
        {
            List<Search> obj = new List<Search>();
            var VGA = _wonder.GraphicsCards.Select(i => new { ProductCode = i.Vgacode, ProductName = i.Vganame }).Where(x => x.ProductName.Contains(src)).ToList();
            foreach (var item in VGA)
            {
                Search x = new Search();
                x.ProductName = item.ProductName;
                x.ProductCode = item.ProductCode;
                obj.Add(x);
            }
            return obj;
        }

        #endregion


        #region Review

        public ReviewVM AddReview(ReviewVM review)
        {
            Review obj = new Review();
            if (review.ProductCode.StartsWith("S"))
            {
                obj.Ssdcode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("R"))
            {
                obj.RamCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("C"))
            {
                obj.CaseCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("V"))
            {
                obj.Vgacode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("PS"))
            {
                obj.Psucode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("Pr"))
            {
                obj.ProCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("M"))
            {
                obj.MotherCode = review.ProductCode;
            }
            else if (review.ProductCode.StartsWith("H"))
            {
                obj.Hddcode = review.ProductCode;
            }
            obj.Comment = review.Comment;
            if (review.CustomerName == null)
            {
                obj.CustomerName = "";
            }
            else
            {
                obj.CustomerName = review.CustomerName;
            }
            obj.Rate = review.Rate;
            review.DateAndTime = DateTime.Now;
            obj.DateAndTime = review.DateAndTime;
            obj.IsAvailable = true;
            _wonder.Reviews.Add(obj);
            _wonder.SaveChanges();

            return review;
        }

        public List<ReviewVM> Reviews(string code)
        {
            List<Review> Data = new List<Review>();
            List<ReviewVM> reviews = new List<ReviewVM>();
            if (code.StartsWith("S"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Ssdcode == code && x.IsAvailable == true).ToList();
            }
            else if (code.StartsWith("R"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.RamCode == code && x.IsAvailable == true).ToList();
            }
            else if (code.StartsWith("C"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.CaseCode == code && x.IsAvailable == true).ToList();
            }
            else if (code.StartsWith("V"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Vgacode == code && x.IsAvailable == true).ToList();
            }
            else if (code.StartsWith("PS"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Psucode == code && x.IsAvailable == true).ToList();
            }
            else if (code.StartsWith("Pr"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.ProCode == code && x.IsAvailable == true).ToList();
            }
            else if (code.StartsWith("M"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.MotherCode == code && x.IsAvailable == true).ToList();
            }
            else if (code.StartsWith("H"))
            {
                Data = _wonder.Reviews.Select(X => X).Where(x => x.Hddcode == code && x.IsAvailable == true).ToList();
            }
            foreach (var item in Data)
            {
                ReviewVM R = new ReviewVM();
                R.ReviewId = item.ReviewId;
                R.IsAvailable = item.IsAvailable;
                R.ProductCode = code;
                R.CustomerName = item.CustomerName;
                R.Comment = item.Comment;
                R.Rate = item.Rate;
                R.DateAndTime = item.DateAndTime;
                reviews.Add(R);
            }
            return reviews;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Getting Brands

        public List<Brand> GetProductBrand()
        {
            var brand = _wonder.Brands.ToList();
            return brand;
        }
        #endregion


        #region Admin Project

        #region Users

        public List<UserVM> GetUsersData()
        {
            List<UserVM> obj = new List<UserVM>();
            var data = _wonder.Users.Where(x => x.IsAdmin == false).Select(x => new { x.UserId, x.FirstName, x.LastName, x.Phone, x.Password }).ToList();
            foreach (var item in data)
            {
                UserVM O = new UserVM();
                O.ID = item.UserId;
                O.Name = item.FirstName + " " + item.LastName;
                O.Telephone = item.Phone;
                O.Password = item.Password;
                O.LatestBuyTime = _wonder.Sales.Where(x => x.UserId == item.UserId).OrderByDescending(x => x.DateAndTime).Take(1).Select(x => x.DateAndTime).FirstOrDefault();
                O.NumberOfTimes = _wonder.Sales.Where(x => x.UserId == item.UserId).GroupBy(x => x.UserId).Select(g => g.Count()).FirstOrDefault();
                obj.Add(O);
            }
            return obj;
        }
        #endregion

        #region Admins

        public List<UserVM> GetAdmins()
        {
            List<UserVM> data = new List<UserVM>();
            var obj = _wonder.Users.Where(x => x.IsAdmin == true).Select(x => new { FName = x.FirstName, LName = x.LastName, Telephone = x.Phone }).ToList();
            foreach (var item in obj)
            {
                UserVM O = new UserVM();
                O.Name = item.FName + " " + item.LName;
                O.Telephone = item.Telephone;
                data.Add(O);
            }
            return data;
        }

        #endregion

        #region Salesvm
        public List<SalesVM> GetProcessor()
        {
            var processor = _wonder.Sales.Where(pro => pro.ProCode != null).Select(Provm => new SalesVM
            {
                UserID = Provm.UserId,
                ProductCode = Provm.ProCodeNavigation.ProName,
                Address = Provm.Address,
                DateAndTime = Provm.DateAndTime,
                ProductQuantity = Provm.ProductQuantity,
                TotalPrice = Provm.TotalPrice
            });
            return processor.ToList();
        }
        public List<SalesVM> GetMotherboard()
        {
            var Mother = _wonder.Sales.Where(moth => moth.MotherCode != null).Select(mothvm => new SalesVM
            {
                UserID = mothvm.UserId,
                ProductCode = mothvm.MotherCodeNavigation.MotherName,
                Address = mothvm.Address,
                DateAndTime = mothvm.DateAndTime,
                ProductQuantity = mothvm.ProductQuantity,
                TotalPrice = mothvm.TotalPrice
            });
            return Mother.ToList();
        }

        public List<SalesVM> GetSDD()
        {
            var ssd = _wonder.Sales.Where(ssd => ssd.Ssdcode != null).Select(ssdvm => new SalesVM
            {
                UserID = ssdvm.UserId,
                ProductCode = ssdvm.SsdcodeNavigation.Ssdname,
                Address = ssdvm.Address,
                DateAndTime = ssdvm.DateAndTime,
                ProductQuantity = ssdvm.ProductQuantity,
                TotalPrice = ssdvm.TotalPrice
            });
            return ssd.ToList();
        }
        public List<SalesVM> GetHDD()
        {
            var ssd = _wonder.Sales.Where(hdd => hdd.Hddcode != null).Select(hddvm => new SalesVM
            {
                UserID = hddvm.UserId,
                ProductCode = hddvm.HddcodeNavigation.Hddname,
                Address = hddvm.Address,
                DateAndTime = hddvm.DateAndTime,
                ProductQuantity = hddvm.ProductQuantity,
                TotalPrice = hddvm.TotalPrice
            });
            return ssd.ToList();
        }

        public List<SalesVM> GetCases()
        {
            var Case = _wonder.Sales.Where(cas => cas.CaseCode != null).Select(Casevm => new SalesVM
            {
                UserID = Casevm.UserId,
                ProductCode = Casevm.CaseCodeNavigation.CaseName,
                Address = Casevm.Address,
                DateAndTime = Casevm.DateAndTime,
                ProductQuantity = Casevm.ProductQuantity,
                TotalPrice = Casevm.TotalPrice
            });
            return Case.ToList();
        }
        public List<SalesVM> GetPowerSupplies()
        {
            var PowerSuply = _wonder.Sales.Where(suply => suply.Psucode != null).Select(suplyvm => new SalesVM
            {
                UserID = suplyvm.UserId,
                ProductCode = suplyvm.PsucodeNavigation.Psuname,
                Address = suplyvm.Address,
                DateAndTime = suplyvm.DateAndTime,
                ProductQuantity = suplyvm.ProductQuantity,
                TotalPrice = suplyvm.TotalPrice
            });
            return PowerSuply.ToList();
        }
        public List<SalesVM> GetGraphicsCard()
        {
            var GraphicCard = _wonder.Sales.Where(GC => GC.Vgacode != null).Select(GCvm => new SalesVM
            {
                UserID = GCvm.UserId,
                ProductCode = GCvm.VgacodeNavigation.Vganame,
                Address = GCvm.Address,
                DateAndTime = GCvm.DateAndTime,
                ProductQuantity = GCvm.ProductQuantity,
                TotalPrice = GCvm.TotalPrice
            });
            return GraphicCard.ToList();
        }
        public List<SalesVM> GetRam()
        {
            var ram = _wonder.Sales.Where(r => r.RamCode != null).Select(ramvm => new SalesVM
            {
                UserID = ramvm.UserId,
                ProductCode = ramvm.RamCodeNavigation.RamName,
                Address = ramvm.Address,
                DateAndTime = ramvm.DateAndTime,
                ProductQuantity = ramvm.ProductQuantity,
                TotalPrice = ramvm.TotalPrice
            }); ;
            return ram.ToList();
        }

        public List<string> GetBrandNames()
        {
            var brands = _wonder.Brands.Select(x => x.BrandName).ToList();
            return brands;
        }

        #endregion


        #endregion


    }
}