using Doctors.Core.Models;
using Doctors.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Data.DAL.RepositoryConcretes
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
