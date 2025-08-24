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

        [HttpGet]
        public IActionResult GetReplayData(string asset, DateTime startTime, DateTime endTime)
        {
            // Example data — replace with real logic
            var data = new List<object> {
        new { lat = 30.5, lng = 70.2, time = "2025-08-22T10:00:00" },
        new { lat = 30.6, lng = 70.3, time = "2025-08-22T10:10:00" },
        new { lat = 30.7, lng = 70.4, time = "2025-08-22T10:20:00" },
    };

            return Json(data);
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
