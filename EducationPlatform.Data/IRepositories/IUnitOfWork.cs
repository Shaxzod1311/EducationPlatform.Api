using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ITeacherRepository Teachers { get; }
        public ICourseRepository Courses { get; }
        Task SaveChangesAsync();
    }
}
