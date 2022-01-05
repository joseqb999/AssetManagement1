using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetWebAPI.DTO
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;

        public string Name { get; set; }
        public Guid ModelId { get; set; }
        public Guid ManuFactId { get; set; }

        private int recordsPerPage = 10;
        private readonly int maxRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }
            set
            {
                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
    }
}
