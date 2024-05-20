using Doctors.Bussiness.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace DoctorsNlayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDoctorService _doctorService;

        public HomeController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IActionResult Index()
        {

            var doctors=_doctorService.GetAllDoctors();
            return View(doctors);
        }
    }
}
