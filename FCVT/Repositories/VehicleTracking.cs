using FCVT.DAL;
using FCVT.Interfaces;
using FCVT.Models;

namespace FCVT.Repositories
{
    public class VehicleTracking : IVehicleTracking
    {
        private readonly DBHelper _dbHelper;

        public VehicleTracking(DBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<IEnumerable<VTLL>> GetVTLL(string UserID)
        {
            var result = await _dbHelper.GetVTLL(UserID);
            return result;
        }
    }
}
