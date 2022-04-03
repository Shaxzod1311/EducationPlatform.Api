using EducationPlatform.Domain.Entities.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Service.Interfaces
{
    internal interface ITeacherService
    {
        Task<Teacher> CreateAsync(Teacher teacher);
        Task<Teacher> GetAsync(Expression<Func<Teacher, bool>> predicate);
        Teacher Delete(long id);
        Teacher Update(Teacher teacher);
        IEnumerable<Teacher> GetAll(Expression<Func<Teacher, bool>> predicate);
    }
}
