using FCVT.Models;

namespace FCVT.Interfaces
{
    public interface IVehicleTracking
    {
        Task<IEnumerable<VTLL>> GetVTLL(string UserID);
    }
}
