using Doctors.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Bussiness.Service.Abstracts
{
    public  interface IDoctorService
    {

        void AddDoctor(Doctor doctor);
        void RemoveDoctor(int id );
        void Update(int id,Doctor doctor);
        Doctor GetDoctor(Func<Doctor, bool>? func=null);
        List<Doctor> GetAllDoctors(Func<Doctor,bool>? func=null);
    }
}
