using Doctors.Bussiness.Exceptions;
using Doctors.Bussiness.Service.Abstracts;
using Doctors.Core.Models;
using Doctors.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Bussiness.Service.Concretes
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DoctorService(IDoctorRepository doctorRepository,IWebHostEnvironment webHostEnvironment)
        {
            _doctorRepository = doctorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddDoctor(Doctor doctor)
        {
            if (doctor.ImgFile == null)
            {
                throw new ArgumentNullException("ImgFile", "tapilmadi");
            }
            if (!doctor.ImgFile.ContentType.Contains("image/"))
            {
                throw new FileContentException("ImgFile", "content type error");
            }
            if(doctor.ImgFile.Length > 2097125)
            {
                throw new FileSizeException("ImgFile", "olcusu cox boyukdur");
            }
           string path=_webHostEnvironment.WebRootPath+@"/Upload/Service/"+ doctor.ImgFile.FileName;
            using(FileStream stream=new FileStream(path, FileMode.Create))
            {
                doctor.ImgFile.CopyTo(stream);
            }
            doctor.ImgUrl = doctor.ImgFile.FileName;
            _doctorRepository.Add(doctor);
            _doctorRepository.Commit();
        }

        public List<Doctor> GetAllDoctors(Func<Doctor, bool>? func = null)
        {
           return _doctorRepository.GetAll(func);
        }

        public Doctor GetDoctor(Func<Doctor, bool>? func = null)
        {
            return _doctorRepository.Get(func);
        }

        public void RemoveDoctor(int id)
        {
            var doctor = _doctorRepository.Get(x=>x.Id == id);
            if(doctor==null) {
                throw new Exception();
                    }
            string path = _webHostEnvironment.WebRootPath + @"\Upload/Service\" + doctor.ImgUrl;
            if(!File.Exists(path))
            {
                throw new FileNameNotFoundException("ImgUrl", "Sekil tapilmadi");

            }
            File.Delete(path);
            _doctorRepository.Remove(doctor);
            _doctorRepository.Commit();

        }

        public void Update(int id, Doctor doctor)
        {
          var existDoctor=_doctorRepository.Get(x => x.Id == id);
            if(existDoctor==null)
            {
                throw new NullReferenceException();
            }
            if (doctor.ImgFile != null)
            {
                string filename = doctor.ImgFile.FileName;
                string path=_webHostEnvironment.WebRootPath +@"/Upload/Service/"+ doctor.ImgFile.FileName;
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    doctor.ImgFile.CopyTo(stream);
                }
                FileInfo fileInfo = new FileInfo(path+existDoctor.ImgUrl);
                if(fileInfo.Exists)
                {
                    fileInfo.Delete();

                }
                existDoctor.ImgUrl = filename;

            }
            existDoctor.FullName = doctor.FullName;
            existDoctor.Description = doctor.Description;
            _doctorRepository.Commit();
        }
    }
}
