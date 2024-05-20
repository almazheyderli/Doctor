using Doctors.Core.Models;
using Doctors.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Data.DAL.RepositoryConcretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T:BaseEntity,new()
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        void IGenericRepository<T>.Add(T entity)
        {
           _dbContext.Set<T>().Add(entity);
        }

        int IGenericRepository<T>.Commit()
        {
            return _dbContext.SaveChanges();
        }

        T IGenericRepository<T>.Get(Func<T, bool>? func)
        {
         return func==null?
                _dbContext.Set<T>().FirstOrDefault():
                _dbContext.Set<T>().FirstOrDefault(func);
        }

        List<T> IGenericRepository<T>.GetAll(Func<T, bool>? func)
        {
            return func==null?
            _dbContext.Set<T>().ToList():
            _dbContext.Set<T>().Where(func).ToList();
        }

        void IGenericRepository<T>.Remove(T entity)
        {
            _dbContext.Remove(entity);
        }
    }
}
