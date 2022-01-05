using System;
using System.Collections.Generic;

#nullable disable

namespace AssetWebAPI.Models
{
    public partial class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ModelId { get; set; }
        public Guid ManuFactId { get; set; }



        public Guid? ColorId { get; set; }
        public string Description { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public bool InUse { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public virtual Color Color { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Model Model { get; set; }
    }
}
