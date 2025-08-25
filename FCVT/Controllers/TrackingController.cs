using FCVT.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCVT.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleTracking _vehicleTracking;
        public TrackingController(IConfiguration configuration, ILogger<HomeController> logger, IVehicleTracking vehicleTracking)
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
        public async Task<IActionResult> GetReplayData(string asset, DateTime startTime, DateTime endTime)
        {
            var Lst = await _vehicleTracking.GetReplay(asset, startTime.ToString(), endTime.ToString());
            return Json(Lst);
        }

        [HttpGet]
        public async Task<IActionResult> GetVTAlarms()
        {
            var UserID = User.FindFirst("UserID")?.Value;
            var alarms = await _vehicleTracking.GetVTAlarms(UserID);
            return Json(new { data = alarms });
        }


    }
}
