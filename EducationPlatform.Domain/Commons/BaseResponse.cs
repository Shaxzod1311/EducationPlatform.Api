using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Commons
{
    public class BaseResponse<TSource>
    {
        public int? Code { get; set; } = 200;
        public TSource? Data { get; set; }
        public ErrorResponse Error { get; set; }

    }
}
