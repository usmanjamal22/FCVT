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

        public async Task<IEnumerable<VTBmr>> GetBmr(string Asset, string SDT, string EDT)
        {
            var result = await _dbHelper.GetBmr(Asset, SDT, EDT);
            return result;
        }

        public async Task<IEnumerable<VTBmr>> GetReplay(string Asset, string SDT, string EDT)
        {
            var result = await _dbHelper.GetReplay(Asset, SDT, EDT);
            return result;
        }

        public async Task<IEnumerable<VTAlarms>> GetVTAlarms(string UserID)
        {
            var result = await _dbHelper.GetVTAlarms(UserID);
            return result;
        }

        public async Task<AssetDetail> GetAssetName(string DeviceID)
        {
            var result = await _dbHelper.GetAssetName(DeviceID);
            return result;
        }
    }
}
