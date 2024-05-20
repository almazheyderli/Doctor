using Doctors.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Core.RepositoryAbstracts
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        void Add(T entity);
        int Commit ();
        void Remove(T entity);
        T Get(Func<T, bool>? func=null);
        List<T> GetAll(Func<T,bool>? func=null);
    }
   
}
