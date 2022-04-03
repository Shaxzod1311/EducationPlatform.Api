using EducationPlatform.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Data.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        T Delete(T entity);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}
