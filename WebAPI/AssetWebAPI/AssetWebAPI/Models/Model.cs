using System;
using System.Collections.Generic;

#nullable disable

namespace AssetWebAPI.Models
{
    public partial class Model
    {
        public Model()
        {
            Assets = new HashSet<Asset>();
        }

        public Guid ModelId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
