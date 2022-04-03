using EducationPlatform.Domain.Entities.Teachers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities.Courses
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short Chit { get; set; }
        public string ImageUrl { get; set; }
        public long TeacherId { get; set; }


        [ForeignKey(nameof(TeacherId))]
        public Teacher Teachers { get; set; }
        public short Duration { get; set; }

    }
}
