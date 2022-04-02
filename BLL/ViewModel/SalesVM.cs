using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class SalesVM
    {
        public int UserID { get; set; }
        public string ProductCode { get; set; }
        public int ProductQuantity { get; set; }
        public int TotalPrice { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime DateAndTime { get; set; }
    }
}
