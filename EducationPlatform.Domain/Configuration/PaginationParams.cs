using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Configuration
{
    public class PaginationParams
    {
        private const int maxPageSize = 30;
        private int pageSize;

        public int PageSize { get => pageSize; set => pageSize = value > maxPageSize ? maxPageSize : value; }
        public int PageIndex { get; set; }

    }
}
