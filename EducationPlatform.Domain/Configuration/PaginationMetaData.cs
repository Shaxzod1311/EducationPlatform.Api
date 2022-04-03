using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Configuration
{
    public class PaginationMetaData
    {
        public PaginationMetaData(int totalCount, PaginationParams @params)
        {
            TotalCount = totalCount;
            CurrentPage = @params.PageIndex;
            TotalPages = (int)Math.Ceiling(totalCount / (double)@params.PageSize);
        }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

    }
}
