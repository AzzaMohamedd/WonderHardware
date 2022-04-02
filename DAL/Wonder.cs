﻿using BLL.ViewModel;
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

        // Get all Processor
        #region
        public IEnumerable<Processor> GetAllProcessors()
        {
            return _wonder.Processors.ToList();
        }
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
        // Start Pagination

        // End Pagination
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
        public IEnumerable<ProcessorVM> GetProcessorByPrice(int val)
        {
            IEnumerable<ProcessorVM> processorVMs;
            if (val == 1)
            {
                processorVMs = _wonder.Processors.Select(vm => new ProcessorVM()
                {
                    ProName = vm.ProName,
                    ProPrice = vm.ProPrice,
                }).OrderByDescending(pro => pro.ProPrice);
            }
            else
            {
                processorVMs = _wonder.Processors.Select(vm => new ProcessorVM()
                {
                    ProName = vm.ProName,
                    ProPrice = vm.ProPrice,
                }).OrderBy(pro => pro.ProPrice);
            }
            return processorVMs;



        }
        // Get Product by using Take() Method Extension
        public IEnumerable<ProcessorVM> GetVMs(int id)
        {
            IEnumerable<ProcessorVM> data = null;
            if (id == 1)
            {
                data = _wonder.Processors.Select(vm => new ProcessorVM()
                {
                    ProName = vm.ProName,
                    ProPrice = vm.ProPrice,
                });

            }
            else
            {
                data = _wonder.Processors.Select(vm => new ProcessorVM()
                {
                    ProName = vm.ProName,
                    ProPrice = vm.ProPrice,
                }).Take(id);
            }
            return data;
        }
        public IEnumerable<ProcessorVM> TakeProcessor(int val)
        {
            IEnumerable<ProcessorVM> processorVal = null;
            if (val == 1)
            {
                processorVal = GetVMs(val);
            }
            else if (val == 2)
            {
                processorVal = GetVMs(val);
            }
            else if (val == 3)
            {

                processorVal = GetVMs(val);


            }
            else if (val == 5)
            {
                processorVal = GetVMs(val);
            }
            return processorVal;

        }
        public IEnumerable<ProcessorVM> AllBrands(string BName)
        {
            IEnumerable<ProcessorVM> proVm = _wonder.Brands.Where(brand => brand.BrandName == BName).SelectMany(Bvm => Bvm.Processors).Select(Pvm => new ProcessorVM { ProName = Pvm.ProName, ProPrice = Pvm.ProPrice });
            return proVm;
        }
        public IEnumerable<ProcessorVM> HiddenBrands(string BName)
        {
            IEnumerable<ProcessorVM> proVm = null;
            if (BName != "Intel")
            {
                proVm = _wonder.Brands.Where(brand => brand.BrandName == BName).SelectMany(Bvm => Bvm.Processors).Select(Pvm => new ProcessorVM { ProName = Pvm.ProName, ProPrice = Pvm.ProPrice });

            }
            else
            {
                proVm = _wonder.Brands.Where(brand => brand.BrandName == BName).SelectMany(Bvm => Bvm.Processors).Select(Pvm => new ProcessorVM { ProName = Pvm.ProName, ProPrice = Pvm.ProPrice });
            }
            return proVm;
        }
        #endregion
        public IEnumerable<Case> GetNewCase()
        {
            return _wonder.Cases.OrderByDescending(p => p.CaseCode).Take(5);
        }

        public IEnumerable<Hdd> GetNewHDD()
        {
            return _wonder.Hdds.OrderByDescending(p => p.Hddcode).Take(5);
        }

        public IEnumerable<Motherboard> GetNewMotherBoards()
        {
            return _wonder.Motherboards.OrderByDescending(p => p.MotherCode).Take(5);
        }

        public IEnumerable<Processor> GetNewProcessors()
        {
            return _wonder.Processors.OrderByDescending(p => p.ProCode).Take(5);
        }

        public IEnumerable<PowerSupply> GetNewPSU()
        {
            return _wonder.PowerSupplies.OrderByDescending(p => p.Psucode).Take(5);
        }

        public IEnumerable<Ram> GetNewRam()
        {
            return _wonder.Rams.OrderByDescending(p => p.RamCode).Take(5);
        }

        public IEnumerable<Ssd> GetNewSSD()
        {
            return _wonder.Ssds.OrderByDescending(p => p.Ssdcode).Take(5);
        }

        public IEnumerable<GraphicsCard> GetNewVGA()
        {
            return _wonder.GraphicsCards.OrderByDescending(p => p.Vgacode).Take(5);
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




