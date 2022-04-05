using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Models;

namespace DAL
{
    public interface IWonder
    {
       
       
        //  Processor
        #region
        IEnumerable<Processor> GetAllProcessors();
        IEnumerable<ProcessorVM> ProcessorPaginations(int PNum, int SNum);
        IEnumerable<BrandVM> GetProcessorBrandNamesAndNumbers();
        IEnumerable<ProcessorVM> GetProcessorProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id);
        IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string BName, int PNumber, int SNumber);
        

        #endregion
        //  Processor
        // Motherboard
        #region
        IEnumerable<Motherboard> GetAllMotherboard();
        IEnumerable<MotherboardVM> MotherboardPaginations(int PNum, int SNum);
        IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers();
        IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> processorVMs, int Id);
        IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string BName, int PNumber, int SNumber);

        #endregion
        // Motherboard
        // HDD
        #region
        IEnumerable<Hdd> GetAllHDD();
        IEnumerable<HddVM> HDDPaginations(int PNum, int SNum);
        IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers();
        IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> HddVMs, int Id);
        IEnumerable<HddVM> GetHDDProductsByBrand(string BName, int PNumber, int SNumber);

        #endregion
        // HDD
        // RAM
        #region
        IEnumerable<Ram> GetAllRAM();
        IEnumerable<RamVM> RAMPaginations(int PNum, int SNum);
        IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers();
        IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id);
        IEnumerable<RamVM> GetRAMProductsByBrand(string BName, int PNumber, int SNumber);

        #endregion
        // RAM
        CaseVM CaseDetails(string code);
        GraphicsCardVM GraphicsCardDetails(string code);
        HddVM HddDetails(string code);
        MotherboardVM MotherboardDetails(string code);
        PowerSupplyVM PowerSupplyDetails(string code);
        ProcessorVM ProcessorDetails(string code);
        RamVM RamDetails(string code);
        SsdVM SsdDetails(string code);
        List<MotherboardVM> GetNewMotherBoards();

        List<ProcessorVM> GetNewProcessors();

        List<RamVM> GetNewRam();

        List<GraphicsCardVM> GetNewVGA();

        List<HddVM> GetNewHDD();

        List<SsdVM> GetNewSSD();

        List<PowerSupplyVM> GetNewPSU();

        List<CaseVM> GetNewCase();

        string CheckOrderCreateAcc(CheckOutVM checkOut);
        string CheckOrder(CheckOutVM checkOut);
    }
}