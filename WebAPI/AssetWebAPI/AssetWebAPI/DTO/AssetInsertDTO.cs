using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetWebAPI.DTO
{
    public class AssetInsertDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ModelId { get; set; }
        public Guid ManuFactId { get; set; }

        public Guid? ColorId { get; set; }

        public decimal Price { get; set; }
        public bool InUse { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public string Description { get; set; }
    }
}
