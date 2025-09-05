using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


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
    public class UserRoleMapping
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
    }

    public class ViolationAlarms
    {
        public int SerialNo { get; set; }

        [Column("Device Id")]
        public string DeviceID { get; set; }

        [Column("Gps Sent")]
        public DateTime? GPSSent { get; set; }
        public bool ACCIn { get; set; }

        [Column ("GPS Available")]
        public bool GPSAvailable { get; set; }
        public double Speed { get; set; }
        public string Violation { get; set; }
        public string Location { get; set; }
        public DateTime ReceivedTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Direction { get; set; }
        public string BackupBool4 { get; set; }
        public int Priority { get; set; }
        public DateTime AddOn { get; set; }

    }
}

