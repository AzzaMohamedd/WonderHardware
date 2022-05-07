using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ReviewVM
    {
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public string Comment { get; set; }
        public int? Rate { get; set; }
        public DateTime DateAndTime { get; set; }

    }
}
