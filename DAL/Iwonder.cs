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

        IEnumerable<BrandVM> GetBrandNamesAndNumbers();
        IEnumerable<ProcessorVM> GetProcessorByPrice(int val);
        IEnumerable<ProcessorVM> TakeProcessor(int val);
        IEnumerable<ProcessorVM> AllBrands(string BName);
        public IEnumerable<ProcessorVM> HiddenBrands(string BName);
        #endregion
        ProcessorVM ProcessorDetails(string code);

        IEnumerable<Motherboard> GetNewMotherBoards();

        IEnumerable<Processor> GetNewProcessors();

        IEnumerable<Ram> GetNewRam();

        IEnumerable<GraphicsCard> GetNewVGA();

        IEnumerable<Hdd> GetNewHDD();

        IEnumerable<Ssd> GetNewSSD();

        IEnumerable<PowerSupply> GetNewPSU();

        IEnumerable<Case> GetNewCase();

    }
}