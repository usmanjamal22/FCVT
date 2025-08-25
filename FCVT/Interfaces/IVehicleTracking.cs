using FCVT.Models;

namespace FCVT.Interfaces
{
    public interface IVehicleTracking
    {
        Task<IEnumerable<VTLL>> GetVTLL(string UserID);

        Task<IEnumerable<VTBmr>> GetBmr(string Asset, string SDT, string EDT);

        Task<IEnumerable<VTAlarms>> GetVTAlarms(string UserID);

        Task<IEnumerable<VTBmr>> GetReplay(string Asset, string SDT, string EDT);
    }
}
