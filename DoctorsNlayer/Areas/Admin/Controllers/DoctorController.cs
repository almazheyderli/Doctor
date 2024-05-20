using Doctors.Bussiness.Exceptions;
using Doctors.Bussiness.Service.Abstracts;
using Doctors.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsNlayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IActionResult Index()
        {
            var doctors = _doctorService.GetAllDoctors();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _doctorService.AddDoctor(doctor);
            }
            catch (FileContentException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existDoctor = _doctorService.GetDoctor(x => x.Id == id);
            if (existDoctor == null)
            {
                return NotFound();
            }
            _doctorService.RemoveDoctor(existDoctor.Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var oldDoctor = _doctorService.GetDoctor(x => x.Id == id);
            if (oldDoctor == null)
            {
                return NotFound();
            }
            return View(oldDoctor);
        }
        [HttpPost]
        public IActionResult Update(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _doctorService.Update(doctor.Id, doctor);
            return RedirectToAction(nameof(Index));
                }
    }

}
