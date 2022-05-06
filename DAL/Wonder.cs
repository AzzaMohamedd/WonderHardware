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
            List<Processor> Processor = _wonder.Processors.ToList();
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
                //obj.ProRate = item.ProRate;
                PR.Add(obj);
            }
            return PR;
        }


        public IEnumerable<ProcessorVM> ProcessorPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<ProcessorVM> PvMs = GetAllProcessors().Skip(Startfromthisrecord).Take(SNum).Select(PVM => new ProcessorVM
            {
                ProName = PVM.ProName,
                ProPrice = PVM.ProPrice,
            });
            return PvMs;
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
            else
            {
                processors = processorVMs.OrderBy(PVM => PVM.ProPrice).ToList();
            }
            return processors;
        }

        public IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllProcessors().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<ProcessorVM> Data = from Pro in Products
                                            join brand in BName
                       on Pro.BrandName.Trim() equals brand
                                            select new ProcessorVM { ProName = Pro.ProName, ProPrice = Pro.ProPrice };
            return Data.Distinct();
        }
        public IEnumerable<ProcessorVM> ProcessorPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<ProcessorVM> processors
                                = GetAllProcessors().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(processor => processor.ProPrice >= min && processor.ProPrice <= max)
                                .Select(Pvm => new ProcessorVM
                                {
                                    ProPrice = Pvm.ProPrice,
                                    ProName = Pvm.ProName
                                });
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
            var Startfromthisrecord = (PageNumber * PageSize) - PageSize;
            IEnumerable<ProcessorVM> PvMs = GetAllProcessors().Skip(Startfromthisrecord).Take(PageSize);

            IEnumerable<ProcessorVM> Data = from Pro in PvMs
                                            join brand in BName
                       on Pro.BrandName.Trim() equals brand
                                            select Pro;
            IEnumerable<ProcessorVM> Products = null;
            if (Id == 1)
            {
                Products = Data.OrderByDescending(PVM => PVM.ProPrice).ToList();
            }
            else
            {
                Products = Data.OrderBy(PVM => PVM.ProPrice).ToList();
            }
            return Products;


        }
        #endregion



        #region MotherBoard

        public List<MotherboardVM> GetAllMotherboard()
        {
            List<Motherboard> Motherboard = _wonder.Motherboards.ToList();
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
                //obj.MotherRate = item.MotherRate;
                MB.Add(obj);
            }
            return MB;
        }

        public IEnumerable<MotherboardVM> MotherboardPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<MotherboardVM> MvMs = GetAllMotherboard().Skip(Startfromthisrecord).Take(SNum).Select(MVM => new MotherboardVM
            {
                MotherPrice = MVM.MotherPrice,
                MotherName = MVM.MotherName
            });
            return MvMs;
        }

        public IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllMotherboard(),
                                       brand => brand.BrandId,
                                       motherboard => motherboard.MotherBrandId,
                                       (brand, motherboard) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllMotherboard().Where(brandNum => brandNum.MotherBrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> MotherboardVMs, int Id)
        {
            IList<MotherboardVM> Motherboard = null;
            if (Id == 1)
            {
                Motherboard = MotherboardVMs.OrderByDescending(MVM => MVM.MotherPrice).ToList();
            }
            else
            {
                Motherboard = MotherboardVMs.OrderBy(MVM => MVM.MotherPrice).ToList();
            }
            return Motherboard;
        }

        public IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllMotherboard().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<MotherboardVM> Data = from Moth in Products
                                              join brand in BName.Distinct()
                         on Moth.BrandName.Trim() equals brand
                                              select new MotherboardVM { MotherName = Moth.MotherName, MotherPrice = Moth.MotherPrice };
            return Data.Distinct();
        }

        public IEnumerable<MotherboardVM> MotherboardPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<MotherboardVM> motherboards
                                = GetAllMotherboard().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(motherboard => motherboard.MotherPrice >= min && motherboard.MotherPrice <= max)
                                .Select(Mvm => new MotherboardVM
                                {
                                    MotherPrice = Mvm.MotherPrice,
                                    MotherName = Mvm.MotherName
                                });
            return motherboards;
        } 
        public IEnumerable<MotherboardVM> MotherboardPaginByBrand(int PNum, int SNum, string[] BName)
        {
            var Products = GetAllMotherboard().Skip((PNum * SNum) - SNum).Take(SNum);
            IEnumerable<MotherboardVM> Data = from Pro in Products
                                            join brand in BName
                       on Pro.BrandName.Trim() equals brand
                                            select new MotherboardVM {  MotherPrice=Pro.MotherPrice,MotherName=Pro.MotherName };
            return Data.Distinct();

        }
        public IEnumerable<MotherboardVM> MotherboardPriceBrand(int PageNumber, int PageSize, int Id, string[] BName){
            var Startfromthisrecord = (PageNumber * PageSize) - PageSize;
            IEnumerable<MotherboardVM> PvMs = GetAllMotherboard().Skip(Startfromthisrecord).Take(PageSize);

            IEnumerable<MotherboardVM> Data = from Pro in PvMs
                                              join brand in BName
                         on Pro.BrandName.Trim() equals brand
                                              select Pro;
            IEnumerable<MotherboardVM> Products = null;
            if (Id == 1)
            {
                Products = Data.OrderByDescending(PVM => PVM.MotherPrice).ToList();
            }
            else
            {
                Products = Data.OrderBy(PVM => PVM.MotherPrice).ToList();
            }
            return Products;


        }
       
        #endregion


        #region HDD

        public List<HddVM> GetAllHDD()
        {
            List<Hdd> HDD = _wonder.Hdds.ToList();
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
                //obj.Hddrate = item.Hddrate;
                HD.Add(obj);
            }
            return HD;
        }

        public IEnumerable<HddVM> HDDPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<HddVM> HDDMs = GetAllHDD().Skip(Startfromthisrecord).Take(SNum).Select(HVM => new HddVM
            {
                Hddname = HVM.Hddname,
                Hddprice = HVM.Hddprice
            });
            return HDDMs;
        }

        public IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers()
        {
            IEnumerable<BrandVM> brandVMs = _wonder.Brands.ToList().Join(GetAllHDD(),
                                       brand => brand.BrandId,
                                       HDD => HDD.HddbrandId,
                                       (brand, HDD) => new BrandVM
                                       {
                                           BrandName = brand.BrandName,
                                           BrandNum = GetAllHDD().Where(brandNum => brandNum.HddbrandId == brand.BrandId).Count()
                                       }

               ).GroupBy(i => i.BrandName).Select(i => i.FirstOrDefault()).ToList();
            return brandVMs;
        }

        public IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> HddVMs, int Id)
        {
            IList<HddVM> hdd = null;
            if (Id == 1)
            {
                hdd = HddVMs.OrderByDescending(HVM => HVM.Hddprice).ToList();
            }
            else
            {
                hdd = HddVMs.OrderBy(HVM => HVM.Hddprice).ToList();
            }
            return hdd;
        }

        public IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int PNumber, int SNumber)
        {
            var Products = GetAllHDD().Skip((PNumber * SNumber) - SNumber).Take(SNumber);
            IEnumerable<HddVM> Data = from hdd in Products
                                      join brand in BName
                         on hdd.BrandName.Trim() equals brand
                                      select new HddVM { Hddname = hdd.Hddname, Hddprice = hdd.Hddprice };
            return Data.Distinct();
        }

        public IEnumerable<HddVM> HDDPrice(int min, int max, int PSize, int NPage)
        {
            IEnumerable<HddVM> Hdds
                                = GetAllHDD().
                                 Skip((PSize * NPage) - PSize).Take(PSize).
                                 Where(hdd => hdd.Hddprice >= min && hdd.Hddprice <= max)
                                 .Select(hdds => new HddVM
                                 {
                                     Hddprice = hdds.Hddprice,
                                     Hddname = hdds.Hddname
                                 });
            return Hdds;
        }
        #endregion


        #region RAM

        public List<RamVM> GetAllRAM()
        {
            List<Ram> Ram = _wonder.Rams.ToList();
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
                //obj.RamRate = item.RamRate;
                RM.Add(obj);
            }
            return RM;
        }

        public IEnumerable<RamVM> RAMPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<RamVM> RVMs = GetAllRAM().Skip(Startfromthisrecord).Take(SNum).Select(RVM => new RamVM
            {
                RamName = RVM.RamName,
                RamPrice = RVM.RamPrice
            });
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
            List<Ssd> Ssd = _wonder.Ssds.ToList();
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
                //obj.Ssdrate = item.Ssdrate;
                SD.Add(obj);
            }
            return SD;
        }

        public IEnumerable<SsdVM> SSDPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<SsdVM> SVMs = GetAllSSD().Skip(Startfromthisrecord).Take(SNum).Select(SVM => new SsdVM
            {
                Ssdname = SVM.Ssdname,
                Ssdprice = SVM.Ssdprice
            });
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
            List<GraphicsCard> GraphicsCard = _wonder.GraphicsCards.ToList();
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
                //obj.Vgarate = item.Vgarate;
                GC.Add(obj);
            }
            return GC;
        }

        public IEnumerable<GraphicsCardVM> CardPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<GraphicsCardVM> CardVMs = GetAllCard().Skip(Startfromthisrecord).Take(SNum).Select(CardVM => new GraphicsCardVM
            {
                Vgaprice = CardVM.Vgaprice,
                Vganame = CardVM.Vganame
            });
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

        public List<CaseVM> GetAllCase()
        {
            List<Case> Case = _wonder.Cases.ToList();
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
                //obj.CaseRate = item.CaseRate;
                CA.Add(obj);
            }
            return CA;
        }

        public IEnumerable<CaseVM> CasePaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<CaseVM> CaseVMs = GetAllCase().Skip(Startfromthisrecord).Take(SNum).Select(CasedVM => new CaseVM
            {
                CaseName = CasedVM.CaseName,
                CasePrice = CasedVM.CasePrice
            });
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
            List<PowerSupply> PowerSupply = _wonder.PowerSupplies.ToList();
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
                //obj.Psurate = item.Psurate;
                PS.Add(obj);
            }
            return PS;
        }

        public IEnumerable<PowerSupplyVM> PowerSuplyPaginations(int PNum, int SNum)
        {
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<PowerSupplyVM> PowerSuplyVMs = GetAllPowerSuply().Skip(Startfromthisrecord).Take(SNum).Select(PsdVM => new PowerSupplyVM
            {
                Psuname = PsdVM.Psuname,
                Psuprice = PsdVM.Psuprice
            });
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

        public List<CaseVM> GetNewCase()
        {
            List<Case> Case = _wonder.Cases.OrderByDescending(p => p.CaseCode).Take(5).ToList();
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
                //obj.CaseRate = item.CaseRate;
                CA.Add(obj);
            }
            return CA;
        }

        public List<HddVM> GetNewHDD()
        {
            List<Hdd> HDD = _wonder.Hdds.OrderByDescending(p => p.Hddcode).Take(5).ToList();
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
                //obj.Hddrate = Hdd.Hddrate;
                HD.Add(obj);
            }
            return HD;
        }

        public List<MotherboardVM> GetNewMotherBoards()
        {
            List<Motherboard> Motherboard = _wonder.Motherboards.OrderByDescending(p => p.MotherCode).Take(5).ToList();
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
                //obj.MotherRate = item.MotherRate;
                MB.Add(obj);
            }
            return MB;
        }

        public List<ProcessorVM> GetNewProcessors()
        {
            List<Processor> Processor = _wonder.Processors.OrderByDescending(p => p.ProCode).Take(5).ToList();
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
                //obj.ProRate = processor.ProRate;
                PR.Add(obj);
            }
            return PR;
        }

        public List<PowerSupplyVM> GetNewPSU()
        {
            List<PowerSupply> PowerSupply = _wonder.PowerSupplies.OrderByDescending(p => p.Psucode).Take(5).ToList();
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
                //obj.Psurate = powersupply.Psurate;
                PS.Add(obj);
            }
            return PS;
        }

        public List<RamVM> GetNewRam()
        {
            List<Ram> Ram = _wonder.Rams.OrderByDescending(p => p.RamCode).Take(5).ToList();
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
                //obj.RamRate = ram.RamRate;
                RM.Add(obj);
            }
            return RM;
        }

        public List<SsdVM> GetNewSSD()
        {
            List<Ssd> Ssd = _wonder.Ssds.OrderByDescending(p => p.Ssdcode).Take(5).ToList();
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
                //obj.Ssdrate = ssd.Ssdrate;
                SD.Add(obj);
            }
            return SD;
        }

        public List<GraphicsCardVM> GetNewVGA()
        {
            List<GraphicsCard> GraphicsCard = _wonder.GraphicsCards.OrderByDescending(p => p.Vgacode).Take(5).ToList();
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
                //obj.Vgarate = graphicscard.Vgarate;
                GC.Add(obj);
            }
            return GC;
        }

        #endregion


        #region ProductDetails

        public CaseVM CaseDetails(string code)
        {
            CaseVM obj = new CaseVM();
            var Case = _wonder.Cases.Where(x => x.CaseCode == code).FirstOrDefault();
            obj.CaseCode = Case.CaseCode;
            obj.CaseName = Case.CaseName;
            obj.CaseBrandId = Case.CaseBrandId;
            obj.BrandName = Case.CaseBrand.BrandName;
            obj.CasePrice = Case.CasePrice;
            obj.CaseQuantity = Case.CaseQuantity;
            obj.CaseFactorySize = Case.CaseFactorySize;
            //obj.CaseRate = Case.CaseRate;
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
            //obj.Vgarate = GraphicsCard.Vgarate;
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
            //obj.Hddrate = Hdd.Hddrate;
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
            //obj.MotherRate = Motherboard.MotherRate;
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
            //obj.Psurate = PowerSupply.Psurate;
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
            //obj.ProRate = processor.ProRate;
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
            //obj.RamRate = Ram.RamRate;
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
            //obj.Ssdrate = Ssd.Ssdrate;
            return obj;
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
            var MothersCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.MotherCode != null)).Select(x => x).ToList();
            foreach (var item in MothersCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.MotherCodeNavigation.MotherName;
                obj.ProductPrice = item.MotherCodeNavigation.MotherPrice;
                obj.ProductCode = item.MotherCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            
            var CasesCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.CaseCode != null)).Select(x => x).ToList();
            foreach (var item in CasesCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.CaseCodeNavigation.CaseName;
                obj.ProductPrice = item.CaseCodeNavigation.CasePrice;
                obj.ProductCode = item.CaseCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            var VGAsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.Vgacode != null)).Select(x => x).ToList();
            foreach (var item in VGAsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.VgacodeNavigation.Vganame;
                obj.ProductPrice = item.VgacodeNavigation.Vgaprice;
                obj.ProductCode = item.Vgacode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            
            var HDDsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.Hddcode != null)).Select(x => x).ToList();
            foreach (var item in HDDsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.HddcodeNavigation.Hddname;
                obj.ProductPrice = item.HddcodeNavigation.Hddprice;
                obj.ProductCode = item.Hddcode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            
            var PSUsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.Psucode != null)).Select(x => x).ToList();
            foreach (var item in PSUsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.PsucodeNavigation.Psuname;
                obj.ProductPrice = item.PsucodeNavigation.Psuprice;
                obj.ProductCode = item.Psucode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            
            var ProsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.ProCode != null)).Select(x => x).ToList();
            foreach (var item in ProsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.ProCodeNavigation.ProName;
                obj.ProductPrice = item.ProCodeNavigation.ProPrice;
                obj.ProductCode = item.ProCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            
            var RAMsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.RamCode != null)).Select(x => x).ToList();
            foreach (var item in RAMsCodes)
            {
                WishListVM obj = new WishListVM();
                obj.ProductName = item.RamCodeNavigation.RamName;
                obj.ProductPrice = item.RamCodeNavigation.RamPrice;
                obj.ProductCode = item.RamCode;
                obj.UserID = item.UserId;
                WishList.Add(obj);
            }
            
            var SSDsCodes = _wonder.WishLists.Where(x => (x.UserId == userid) && (x.Ssdcode != null)).Select(x => x).ToList();
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

        public string DeletefromWL(string ProductCode ,int userid)
        {
            WishList Row=null ;
            if (ProductCode.StartsWith("S"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.Ssdcode == ProductCode).FirstOrDefault();
            }
            else if (ProductCode.StartsWith("R"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.RamCode == ProductCode).FirstOrDefault();
            }
            else if (ProductCode.StartsWith("C"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.CaseCode == ProductCode).FirstOrDefault();
            }
            else if (ProductCode.StartsWith("V"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.Vgacode == ProductCode).FirstOrDefault();
            }
            else if (ProductCode.StartsWith("PS"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.Psucode == ProductCode).FirstOrDefault();
            }
            else if (ProductCode.StartsWith("Pr"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.ProCode == ProductCode).FirstOrDefault();
            }
            else if (ProductCode.StartsWith("M"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.MotherCode == ProductCode).FirstOrDefault();
            }
            else if (ProductCode.StartsWith("H"))
            {
                Row = _wonder.WishLists.Where(x => x.UserId == userid && x.Hddcode == ProductCode).FirstOrDefault();
            }
            
            _wonder.WishLists.Remove(Row);
            if(_wonder.SaveChanges()!=0)
            {
                return "Deleted Done";
            }
            else
            {
                return "Error";
            }
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

        #endregion
    }
}