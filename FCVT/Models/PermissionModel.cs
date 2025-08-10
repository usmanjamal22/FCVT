using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FCVT.Models
{
    public class PermissionModel
    {
    }

    public class UserLst
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHashed { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public string CreatedOn { get; set; }
    }

    public class UserInsert
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [Required]
        [MinLength(10), MaxLength(10)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[^A-Za-z\d]).{10}$",
         ErrorMessage = "Password must be 10 characters with at least one letter, one number, and one special character.")]
        public string Password { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }

    public class RoleLst
    {
        public string ID { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
    }

    public class RoleInsert
    {
        public string RoleName { get; set; }
        public string RoleType { get; set; }
        public string Description { get; set; }
    }

    public class MenuLst
    {
        public string Id { get; set; }
        public string MenuName { get; set; }
        public string DisplayName { get; set; }
        public string MenuType { get; set; }
        public string PMenuID { get; set; }
        public bool IsAssigned { get; set; }
    }

    public class RoleTypeLst
    {
        public string ID { get; set; }
        public string RoleName { get; set; }
    }

    public class RoleMenuInsert
    {
        public string RoleID { get; set; }
        public string MenuIds { get; set; }
    }

}
