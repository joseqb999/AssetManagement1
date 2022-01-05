using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetWebAPI.DTO
{
    public class ListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }

        public string ModelName { get; set; }

        public Guid ModelId { get; set; }
        public Guid ManuFactId { get; set; }



        public Guid? ColorId { get; set; }

        public string ColorName { get; set; }

        public decimal Price { get; set; }
        public bool InUse { get; set; }

        public string Description { get; set; }

        public DateTime? PurchaseDate { get; set; }





    }
}
