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
        IEnumerable<Processor> GetAll();
        IEnumerable<BrandVM> GetBrandNamesAndNumbers();
        CaseVM CaseDetails(string code);
        GraphicsCardVM GraphicsCardDetails(string code);
        HddVM HddDetails(string code);
        MotherboardVM MotherboardDetails(string code);
        PowerSupplyVM PowerSupplyDetails(string code);
        ProcessorVM ProcessorDetails(string code);
        RamVM RamDetails(string code);
        SsdVM SsdDetails(string code);

    }
}