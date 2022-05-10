using System;
using System.Collections.Generic;

#nullable disable

namespace DataModel.Models
{
    public partial class GraphicsCard
    {
        public GraphicsCard()
        {
            Reviews = new HashSet<Review>();
            Sales = new HashSet<Sale>();
            WishLists = new HashSet<WishList>();
        }

        public string Vgacode { get; set; }
        public string Vganame { get; set; }
        public byte? VgabrandId { get; set; }
        public int Vgaprice { get; set; }
        public short Vgaquantity { get; set; }
        public byte Vram { get; set; }
        public int? IntermediateBrandId { get; set; }

        public virtual Brand Vgabrand { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
