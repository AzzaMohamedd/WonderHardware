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
        // Methods For Store View
        #region
        IEnumerable<Processor> GetAllProcessors();
        IEnumerable<ProcessorVM> Paginations(int PNum, int SNum);
        IEnumerable<BrandVM> GetBrandNamesAndNumbers();
        IEnumerable<ProcessorVM> GetProductsByPrice(IEnumerable<ProcessorVM> processorVMs, int Id);
        public IEnumerable<ProcessorVM> GetProductsByBrand(string BName, int PNumber, int SNumber);
        #endregion
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