using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetWebAPI.DTO
{
    public class ExcelDTO
    {

        public string Name { get; set; }
        public string ManufacturerName { get; set; }

        public string ModelName { get; set; }


        public string ColorName { get; set; }

        public decimal Price { get; set; }
        public bool InUse { get; set; }

        public string Description { get; set; }

    }
}
