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


        #region
        // Start Get all Processor
        public IEnumerable<Processor> GetAllProcessors()
        {
            return _wonder.Processors.ToList();
        }
        // End Get all Processor
        // Start Pagination
        public IEnumerable<ProcessorVM> Paginations(int PNum, int SNum)
        {
            IEnumerable<Processor> processorVMs = _wonder.Processors;
            var Startfromthisrecord = (PNum * SNum) - SNum;
            IEnumerable<ProcessorVM> PvMs = processorVMs.Skip(Startfromthisrecord).Take(SNum).Select(PVM => new ProcessorVM
            {
                ProName = PVM.ProName,
                ProPrice = PVM.ProPrice

            });
            return PvMs;

        }
        // End Pagination
        // Start BrandNamesAndNumbers
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
        // End BrandNamesAndNumbers
        public IEnumerable<ProcessorVM> GetProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id)
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
        public IEnumerable<ProcessorVM> GetProductsByBrand(string BName, int PNumber, int SNumber)
        {
            IEnumerable<ProcessorVM> Data = _wonder.Processors.Skip((PNumber * SNumber) - SNumber).Take(SNumber).Where(BVm => BVm.ProBrand.BrandName == BName).Select(pvm => new ProcessorVM
            {
                ProName = pvm.ProName,
                ProPrice = pvm.ProPrice
            });

            return Data;
        }


        #endregion
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
                obj.CasePrice = item.CasePrice;
                obj.CaseQuantity = item.CaseQuantity;
                obj.CaseFactorySize = item.CaseFactorySize;
                obj.CaseRate = item.CaseRate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == item.CaseCode).Select(x => x.ProductImage).Take(1);
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
                obj.Hddprice = Hdd.Hddprice;
                obj.Hddquantity = Hdd.Hddquantity;
                obj.Hddsize = Hdd.Hddsize;
                obj.Hddrpm = Hdd.Hddrpm;
                obj.Hddtype = Hdd.Hddtype;
                obj.Hddrate = Hdd.Hddrate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == Hdd.Hddcode).Select(x => x.ProductImage).Take(1);
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
                obj.MotherPrice = item.MotherPrice;
                obj.MotherQuantity = item.MotherQuantity;
                obj.MotherSocket = item.MotherSocket;
                obj.MotherRate = item.MotherRate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == item.MotherCode).Select(x => x.ProductImage).Take(1);
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
                obj.ProPrice = processor.ProPrice;
                obj.ProQuantity = processor.ProQuantity;
                obj.ProCores = processor.ProCores;
                obj.ProSocket = processor.ProSocket;
                obj.ProThreads = processor.ProThreads;
                obj.ProBaseFreq = processor.ProBaseFreq;
                obj.ProMaxTurboFreq = processor.ProMaxTurboFreq;
                obj.ProLithography = processor.ProLithography;
                obj.ProRate = processor.ProRate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == processor.ProCode).Select(x => x.ProductImage).Take(1);
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
                obj.Psuprice = powersupply.Psuprice;
                obj.Psuquantity = powersupply.Psuquantity;
                obj.Psuwatt = powersupply.Psuwatt;
                obj.Psucertificate = powersupply.Psucertificate;
                obj.Psurate = powersupply.Psurate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == powersupply.Psucode).Select(x => x.ProductImage).Take(1);
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
                obj.RamPrice = ram.RamPrice;
                obj.RamQuantity = ram.RamQuantity;
                obj.RamSize = ram.RamSize;
                obj.RamFrequency = ram.RamFrequency;
                obj.RamType = ram.RamType;
                obj.Ramkits = ram.Ramkits;
                obj.RamRate = ram.RamRate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == ram.RamCode).Select(x => x.ProductImage).Take(1);
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
                obj.Ssdprice = ssd.Ssdprice;
                obj.Ssdquantity = ssd.Ssdquantity;
                obj.Ssdsize = ssd.Ssdsize;
                obj.Ssdinterface = ssd.Ssdinterface;
                obj.Ssdrate = ssd.Ssdrate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == ssd.Ssdcode).Select(x => x.ProductImage).Take(1);
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
                obj.Vgaprice = graphicscard.Vgaprice;
                obj.Vgaquantity = graphicscard.Vgaquantity;
                obj.Vram = graphicscard.Vram;
                obj.IntermediateBrandId = graphicscard.IntermediateBrandId;
                obj.Vgarate = graphicscard.Vgarate;
                obj.Image = _wonder.Images.Where(x => x.ProductCode == graphicscard.Vgacode).Select(x => x.ProductImage).Take(1);
                GC.Add(obj);
            }
            return GC;
        }



        public CaseVM CaseDetails(string code)
        {
            CaseVM obj = new CaseVM() ;
            var Case = _wonder.Cases.Where(x => x.CaseCode == code).FirstOrDefault();
            obj.CaseCode = Case.CaseCode;
            obj.CaseName = Case.CaseName;
            obj.CaseBrandId = Case.CaseBrandId;
            obj.CasePrice = Case.CasePrice;
            obj.CaseQuantity = Case.CaseQuantity;
            obj.CaseFactorySize = Case.CaseFactorySize;
            obj.CaseRate = Case.CaseRate;
            return obj;
        }
        public GraphicsCardVM GraphicsCardDetails(string code)
        {
            GraphicsCardVM obj = new GraphicsCardVM();
            var GraphicsCard = _wonder.GraphicsCards.Where(x => x.Vgacode == code).FirstOrDefault();
            obj.Vgacode = GraphicsCard.Vgacode;
            obj.Vganame = GraphicsCard.Vganame;
            obj.VgabrandId = GraphicsCard.VgabrandId;
            obj.Vgaprice = GraphicsCard.Vgaprice;
            obj.Vgaquantity = GraphicsCard.Vgaquantity;
            obj.Vram = GraphicsCard.Vram;
            obj.IntermediateBrandId = GraphicsCard.IntermediateBrandId;
            obj.Vgarate = GraphicsCard.Vgarate;
            return obj;
        }
        public HddVM HddDetails(string code)
        {
            HddVM obj = new HddVM();
            var Hdd = _wonder.Hdds.Where(x => x.Hddcode == code).FirstOrDefault();
            obj.Hddcode = Hdd.Hddcode;
            obj.Hddname = Hdd.Hddname;
            obj.HddbrandId = Hdd.HddbrandId;
            obj.Hddprice = Hdd.Hddprice;
            obj.Hddquantity = Hdd.Hddquantity;
            obj.Hddsize = Hdd.Hddsize;
            obj.Hddrpm = Hdd.Hddrpm;
            obj.Hddtype = Hdd.Hddtype;
            obj.Hddrate = Hdd.Hddrate;
            return obj;
        }
        public MotherboardVM MotherboardDetails(string code)
        {
            MotherboardVM obj = new MotherboardVM();
            var Motherboard = _wonder.Motherboards.Where(x => x.MotherCode == code).FirstOrDefault();
            obj.MotherCode = Motherboard.MotherCode;
            obj.MotherName = Motherboard.MotherName;
            obj.MotherBrandId = Motherboard.MotherBrandId;
            obj.MotherPrice = Motherboard.MotherPrice;
            obj.MotherQuantity = Motherboard.MotherQuantity;
            obj.MotherSocket = Motherboard.MotherSocket;
            obj.MotherRate = Motherboard.MotherRate;
            obj.Image = _wonder.Images.Where(x => x.ProductCode == code).Select(x => x.ProductImage).Take(4);
            return obj;
        }
        public PowerSupplyVM PowerSupplyDetails(string code)
        {
            PowerSupplyVM obj = new PowerSupplyVM();
            var PowerSupply = _wonder.PowerSupplies.Where(x => x.Psucode == code).FirstOrDefault();
            obj.Psucode = PowerSupply.Psucode;
            obj.Psuname = PowerSupply.Psuname;
            obj.PsubrandId = PowerSupply.PsubrandId;
            obj.Psuprice = PowerSupply.Psuprice;
            obj.Psuquantity = PowerSupply.Psuquantity;
            obj.Psuwatt = PowerSupply.Psuwatt;
            obj.Psucertificate = PowerSupply.Psucertificate;
            obj.Psurate = PowerSupply.Psurate;
            return obj;
        }
        public ProcessorVM ProcessorDetails(string code)
        {
            ProcessorVM obj = new ProcessorVM();
            var processor = _wonder.Processors.Where(x => x.ProCode == code).FirstOrDefault();
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
        public RamVM RamDetails(string code)
        {
            RamVM obj = new RamVM();
            var Ram = _wonder.Rams.Where(x => x.RamCode == code).FirstOrDefault();
            obj.RamCode = Ram.RamCode;
            obj.RamName = Ram.RamName;
            obj.RamBrandId = Ram.RamBrandId;
            obj.RamPrice = Ram.RamPrice;
            obj.RamQuantity = Ram.RamQuantity;
            obj.RamSize = Ram.RamSize;
            obj.RamFrequency = Ram.RamFrequency;
            obj.RamType = Ram.RamType;
            obj.Ramkits = Ram.Ramkits;
            obj.RamRate = Ram.RamRate;
            return obj;
        }
        public SsdVM SsdDetails(string code)
        {
            SsdVM obj = new SsdVM();
            var Ssd = _wonder.Ssds.Where(x => x.Ssdcode == code).FirstOrDefault();
            obj.Ssdcode = Ssd.Ssdcode;
            obj.Ssdname = Ssd.Ssdname;
            obj.SsdbrandId = Ssd.SsdbrandId;
            obj.Ssdprice = Ssd.Ssdprice;
            obj.Ssdquantity = Ssd.Ssdquantity;
            obj.Ssdsize = Ssd.Ssdsize;
            obj.Ssdinterface = Ssd.Ssdinterface;
            obj.Ssdrate = Ssd.Ssdrate;
            return obj;
        }



        public String CheckOrderCreateAcc(CheckOutVM checkOut)
        {
            User Uobj = new User();
            Sale Sobj = new Sale();

            Uobj.FirstName = checkOut.FName;
            Uobj.LastName = checkOut.LName;
            Uobj.Password = checkOut.Password;
            Uobj.Phone = checkOut.Telephone;
            _wonder.Users.Add(Uobj);
            _wonder.SaveChanges();
            var userid = _wonder.Users.Where(x => x.Phone == checkOut.Telephone).Select(x => x.UserId).FirstOrDefault();
            Sobj.UserId = userid;
            Sobj.Address = checkOut.Sales.City + " , " + checkOut.Sales.Address;
            Sobj.DateAndTime = DateTime.Now;
            ///////////////////////////////////////////
            _wonder.Sales.Add(Sobj);
            _wonder.SaveChanges();
            //Account has been created & Order checked successfully.
            return "success";

        }

        public String CheckOrder(CheckOutVM checkOut)
        {
            User Uobj = new User();
            Sale Sobj = new Sale();

            var userid = _wonder.Users.Where(x => x.Phone == checkOut.Telephone).Select(x => x.UserId).FirstOrDefault();

            if (_wonder.Users.Where(x => x.Password == checkOut.Password && x.Phone == checkOut.Telephone) != null)
            {
                Sobj.UserId = userid;
                if (checkOut.Sales.City != null && checkOut.Sales.Address != null)
                {
                    Sobj.Address = checkOut.Sales.City + " , " + checkOut.Sales.Address;
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                    //Order checked successfully at the address that user entered.
                    return "success checked new address";
                }
                else
                {
                    var lastAddress = _wonder.Sales.Where(x => x.UserId == userid).OrderByDescending(x => x.SalesId).Take(1).Select(x => x.Address).FirstOrDefault();
                    Sobj.Address = lastAddress;
                    Sobj.DateAndTime = DateTime.Now;
                    _wonder.Sales.Add(Sobj);
                    _wonder.SaveChanges();
                    //Order checked successfully at the same address checked before.
                    return "success checked old address";
                }

            }
            else
            {
                //Phone or password isn't correct or both !!
                return "failed phone&pass";
            }

        }


    }
}





