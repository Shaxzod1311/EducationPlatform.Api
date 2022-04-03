using EducationPlatform.Data.Contexts;
using EducationPlatform.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EducationDbContext dbContext;

        public UnitOfWork(EducationDbContext dbContext)
        {
            this.dbContext = dbContext;

            Teachers = new TeacherRepsitory(dbContext);
            Courses = new CourseRepository(dbContext);
        }

        public ITeacherRepository Teachers { get; }

        public ICourseRepository Courses { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
           await dbContext.SaveChangesAsync();
        }
    }
}
