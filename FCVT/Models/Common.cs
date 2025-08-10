using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FCVT.Models
{
    public class Common
    {
        public string Asset { get; set; }
    }

    public class EVBatteryObject
    {
        public string Percentage { get; set; }
        public string kWh { get; set; }
        public string CycleTotalCapacity { get; set; }
        public string BatteryCycle { get; set; }
        public string BatteryAmpere { get; set; }
        public string BatteryHealth { get; set; }
        public string ExternalVoltag { get; set; }

    }
    public class EVCanHexObject
    {
        public string VehID { get; set; }
        public string GpsDateTime { get; set; }
        public string ExternalVoltag { get; set; }
        public string Can0 { get; set; }
        public string Can1 { get; set; }
        public string Can2 { get; set; }
        public string Can3 { get; set; }
        public string Can4 { get; set; }
        public string Can5 { get; set; }
        public string Can6 { get; set; }
    }


    public class PMenu
    {
        public string Pid { get; set; }
        public string DisplayName { get; set; }
        public string MenuType { get; set; }
        public string PMenuID { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HasGenset { get; set; }
        public string ReportDays { get; set; }
    }

    public class IsMenu
    {
        public string pid { get; set; }
        public string pname { get; set; }
        public string pcontroller { get; set; }
        public string paction { get; set; }
        public string IsSngplUser { get; set; }
        public string ReportDays { get; set; }
        public string HasGenSet { get; set; }
        public List<ChildMenu> Haschild { get; set; }
    }
    public class ChildMenu
    {
        public string cid { get; set; }
        public string cname { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
    }
    public class CommonResponce
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
