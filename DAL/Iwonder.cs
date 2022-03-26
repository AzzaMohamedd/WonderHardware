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
        ProcessorVM ProcessorDetails(string code);
    }
}