using EducationPlatform.Data.Contexts;
using EducationPlatform.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EducationDbContext dbContext;
        private DbSet<T> dbSet;
        public GenericRepository(EducationDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity) => (await dbSet.AddAsync(entity)).Entity;

        public T Delete(T entity)  => dbSet.Remove(entity).Entity;

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate) => predicate == null ? dbSet : dbSet.Where(predicate);

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate) => await dbSet.FirstOrDefaultAsync(predicate);

        public T Update(T entity) => dbSet.Update(entity).Entity;


    }
}
