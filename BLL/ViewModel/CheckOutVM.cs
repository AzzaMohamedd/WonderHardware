using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class CheckOutVM
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public int Telephone { get; set; }
        public string Password { get; set; }
        public SalesVM Sales { get; set; }
    }
}
