using System;
using System.Collections.Generic;

#nullable disable

namespace AssetWebAPI.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Assets = new HashSet<Asset>();
        }

        public Guid ManuFactId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
