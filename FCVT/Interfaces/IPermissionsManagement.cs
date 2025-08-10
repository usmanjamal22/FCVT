using FCVT.Models;

namespace FCVT.Interfaces
{
    public interface IPermissionsManagement
    {
        Task<IEnumerable<UserLst>> GetUserLst(string UserName);

        Task<CommonResponce> AddUsers(string FirstName, string LastName, string UserName, string NIC, string Email, string PhoneNo, string Address, string PasswordHashed, string UserType);

        Task<CommonResponce> DeleteUsers(string ID);

        Task<IEnumerable<RoleLst>> GetRoleLst(string UserName);

        Task<CommonResponce> AddRole(string RoleName, string RoleType, string Description);

        Task<CommonResponce> DeleteRoles(string ID);

        Task<IEnumerable<MenuLst>> GetAllMenusLst(string InputType,string RoleId);

        Task<IEnumerable<RoleTypeLst>> GetAllMenuAssetRolesLst(string Roletype);

        Task<CommonResponce> AddRoleMenu(string RoleID, string MenuIds, string Loginid);

        Task<CommonResponce> AddRoleAssets(string RoleID, string AssetIds, string Loginid);
    }
}
