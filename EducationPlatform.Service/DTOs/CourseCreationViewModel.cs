using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Service.DTOs
{
    public class CourseCreationViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public short Chit { get; set; }
        public IFormFile Image { get; set; }
        public long TeacherId { get; set; }
        public short Duration { get; set; }
    }
}
