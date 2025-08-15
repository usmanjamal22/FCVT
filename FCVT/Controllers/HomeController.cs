using FCVT.Interfaces;
using FCVT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security;

namespace FCVT.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleTracking _vehicleTracking;
        public HomeController(IConfiguration configuration, ILogger<HomeController> logger, IVehicleTracking vehicleTracking)
        {
            _configuration = configuration;
            _logger = logger;
            _vehicleTracking = vehicleTracking;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLL()
        {
            var UserID = User.FindFirst("UserID")?.Value;
            var Lst = await _vehicleTracking.GetVTLL(UserID);
            return Json(Lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
