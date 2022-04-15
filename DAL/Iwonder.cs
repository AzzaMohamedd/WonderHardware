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

        IEnumerable<ProcessorVM> GetProcessorProductsByBrand(string[] BName, int PNumber, int SNumber);
        IEnumerable<ProcessorVM> ProcessorPrice(int min, int max, int PSize, int NPage);

        #endregion
        //  Processor
        // Motherboard
        #region

        IEnumerable<Motherboard> GetAllMotherboard();

        IEnumerable<MotherboardVM> MotherboardPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetMotherboardBrandNamesAndNumbers();

        IEnumerable<MotherboardVM> GetMotherboardProductsByPrice(IEnumerable<MotherboardVM> processorVMs, int Id);

        IEnumerable<MotherboardVM> GetMotherboardProductsByBrand(string[] BName, int PNumber, int SNumber);
        IEnumerable<MotherboardVM> MotherboardPrice(int min, int max, int PSize, int NPage);

        #endregion
        // Motherboard
        // HDD
        #region

        IEnumerable<Hdd> GetAllHDD();

        IEnumerable<HddVM> HDDPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetHDDBrandNamesAndNumbers();

        IEnumerable<HddVM> GetHDDProductsByPrice(IEnumerable<HddVM> HddVMs, int Id);

        IEnumerable<HddVM> GetHDDProductsByBrand(string[] BName, int PNumber, int SNumber);

        #endregion
        // HDD
        // RAM
        #region

        IEnumerable<Ram> GetAllRAM();

        IEnumerable<RamVM> RAMPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetRAMBrandNamesAndNumbers();

        IEnumerable<RamVM> GetRAMProductsByPrice(IEnumerable<RamVM> RamVMs, int Id);

        IEnumerable<RamVM> GetRAMProductsByBrand(string[] BName, int PNumber, int SNumber);

        #endregion
        // RAM
        // SSd
        #region

        IEnumerable<Ssd> GetAllSSD();

        IEnumerable<SsdVM> SSDPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetSSDBrandNamesAndNumbers();

        IEnumerable<SsdVM> GetSSDProductsByPrice(IEnumerable<SsdVM> SsdVMs, int Id);

        IEnumerable<SsdVM> GetSSDProductsByBrand(string[] BName, int PNumber, int SNumber);

        #endregion
        // SSD
        // Graphics Card
        #region

        IEnumerable<GraphicsCardVM> GetCardProductsByBrand(string[] BName, int PNumber, int SNumber);

        IEnumerable<GraphicsCardVM> GetCardVMProductsByPrice(IEnumerable<GraphicsCardVM> CardVMVMs, int Id);

        IEnumerable<BrandVM> GetCardVMBrandNamesAndNumbers();

        IEnumerable<GraphicsCardVM> CardPaginations(int PNum, int SNum);

        IEnumerable<GraphicsCard> GetAllCard();
        #endregion
        //Graphics Card
        // Case
        #region

        IEnumerable<Case> GetAllCase();

        IEnumerable<CaseVM> CasePaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetCaseVMBrandNamesAndNumbers();

        IEnumerable<CaseVM> GetCaseVMProductsByPrice(IEnumerable<CaseVM> caseVMs, int Id);

        IEnumerable<CaseVM> GetCaseProductsByBrand(string[] BName, int PNumber, int SNumber);
        #endregion
        //Case
        // PowerSuply
        #region

        IEnumerable<PowerSupply> GetAllPowerSuply();

        IEnumerable<PowerSupplyVM> PowerSuplyPaginations(int PNum, int SNum);

        IEnumerable<BrandVM> GetPowerSupplyBrandNamesAndNumbers();

        IEnumerable<PowerSupplyVM> GetPowerSupplyProductsByPrice(IEnumerable<PowerSupplyVM> PowerSupplyVMs, int Id);

        IEnumerable<PowerSupplyVM> GetPowerSupplyVMsProductsByBrand(string[] BName, int PNumber, int SNumber);
        #endregion

        // PowerSuply
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

        string CheckOrderCreateAcc(SalesVM sales);

        string CheckOrder(SalesVM sales);

        List<MotherboardVM> GetMotherBoardsExceptOne(string code);

        List<ProcessorVM> GetProcessorsExceptOne(string code);

        List<RamVM> GetRamExceptOne(string code);

        List<GraphicsCardVM> GetVGAExceptOne(string code);

        List<HddVM> GetHDDExceptOne(string code);

        List<SsdVM> GetSSDExceptOne(string code);

        List<PowerSupplyVM> GetPSUExceptOne(string code);

        List<CaseVM> GetCaseExceptOne(string code);

        List<Search> SearchProduct(string src);
    }
}