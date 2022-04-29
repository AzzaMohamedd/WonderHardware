using System;
using System.Collections.Generic;

#nullable disable

namespace UI.Models
{
    public partial class GraphicsCard
    {
        public GraphicsCard()
        {
            Sales = new HashSet<Sale>();
        }

        public string Vgacode { get; set; }
        public string Vganame { get; set; }
        public byte? VgabrandId { get; set; }
        public int Vgaprice { get; set; }
        public short Vgaquantity { get; set; }
        public byte Vram { get; set; }
        public int? IntermediateBrandId { get; set; }
        public byte? Vgarate { get; set; }

        public virtual Brand Vgabrand { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
