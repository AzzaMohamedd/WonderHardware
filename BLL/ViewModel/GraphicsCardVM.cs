using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class GraphicsCardVM
    {
        public string Vgacode { get; set; }
        public string Vganame { get; set; }
        public byte? VgabrandId { get; set; }
        public int Vgaprice { get; set; }
        public short Vgaquantity { get; set; }
        public byte Vram { get; set; }
        public int? IntermediateBrandId { get; set; }
        public byte? Vgarate { get; set; }
        public IEnumerable<byte[]> Image { get; set; }

        public virtual BrandVM Vgabrand { get; set; }
    }
}
