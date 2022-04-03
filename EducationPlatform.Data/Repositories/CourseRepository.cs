using EducationPlatform.Data.Contexts;
using EducationPlatform.Data.IRepositories;
using EducationPlatform.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(EducationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
