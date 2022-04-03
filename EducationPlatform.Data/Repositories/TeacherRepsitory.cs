using EducationPlatform.Data.Contexts;
using EducationPlatform.Data.IRepositories;
using EducationPlatform.Domain.Entities.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Data.Repositories
{
    public class TeacherRepsitory : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepsitory(EducationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
