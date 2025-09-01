using System.Drawing;
using System.Reflection;

namespace FCVT.Models
{
    public class VehicleTracking
    {
    }

    public class VTLL
    {
        public string Asset { get; set; }
        public string Region { get; set; }
        public string Color { get; set; }
        public string GpsdateTime { get; set; }
        public string StatusText { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Speed { get; set; }
        public string Location { get; set; }
        public string AddOn { get; set; }
    }

    public class VTBmr
    {
        public string Asset { get; set; }
        public string GpsDateTime { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string StatusText { get; set; }
        public string Direction { get; set; }
        public string Mileage { get; set; }
        public string Speed { get; set; }
        public string Location { get; set; }
    }



    public class VTAlarms
    {
        public string Asset { get; set; }
        public string DeviceID { get; set; }
        public string GpsDateTime { get; set; }
        public string Alarm { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class AssetDetail
    {
        public string Asset { get; set; }
        public string DeviceID { get; set; }
    }
}
