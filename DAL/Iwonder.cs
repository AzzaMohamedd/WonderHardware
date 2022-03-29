using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace DAL
{
    public interface IWonder
    {
        IEnumerable<Processor> GetAllProcessors();

        IEnumerable<BrandVM> GetBrandNamesAndNumbers();

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