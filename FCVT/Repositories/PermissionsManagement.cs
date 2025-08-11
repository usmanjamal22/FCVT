using FCVT.DAL;
using FCVT.Interfaces;
using FCVT.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace FCVT.Repositories
{
    public class PermissionsManagement : IPermissionsManagement
    {
        private readonly DBHelper _dbHelper;

        public PermissionsManagement(DBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<IEnumerable<UserLst>> GetUserLst(string UserName)
        {
            var result = await _dbHelper.GetUserLst(UserName);
            return result;
        }

        public async Task<CommonResponce> AddUsers(string FirstName, string LastName, string UserName, string NIC, string Email
                                            , string PhoneNo, string Address, string PasswordHashed, string UserType)
        {
            var result = await _dbHelper.AddUsers(FirstName, LastName, UserName, NIC, Email, PhoneNo, Address, PasswordHashed, UserType);
            return result;
        }

        public async Task<CommonResponce> DeleteUsers(string ID)
        {
            var result = await _dbHelper.DleteUsers(ID);
            return result;
        }


        public async Task<IEnumerable<RoleLst>> GetRoleLst(string UserName)
        {
            var result = await _dbHelper.GetRoleLst(UserName);
            return result;
        }


        public async Task<CommonResponce> AddRole(string RoleName, string RoleType, string Description)
        {
            var result = await _dbHelper.AddRole(RoleName, RoleType, Description);
            return result;
        }

        public async Task<CommonResponce> DeleteRoles(string ID)
        {
            var result = await _dbHelper.DeleteRoles(ID);
            return result;
        }

        public async Task<IEnumerable<MenuLst>> GetAllMenusLst(string InputType, string RoleId)
        {
            var result = await _dbHelper.GetAllMenusLst(InputType, RoleId);
            return result;
        }

        public async Task<IEnumerable<RoleTypeLst>> GetAllMenuAssetRolesLst(string Roletype)
        {
            var result = await _dbHelper.GetAllMenuAssetRolesLst(Roletype);
            return result;
        }

        public async Task<CommonResponce> AddRoleMenu(string RoleID, string MenuIds, string Loginid)
        {
            var result = await _dbHelper.AddRoleMenu(RoleID, MenuIds, Loginid);
            return result;
        }

        public async Task<CommonResponce> AddRoleAssets(string RoleID, string AssetIds, string Loginid)
        {
            var result = await _dbHelper.AddRoleAssets(RoleID, AssetIds, Loginid);
            return result;
        }

        public async Task<IEnumerable<CommonSelect>> GetUserRoleLst(string InputType)
        {
            var result = await _dbHelper.GetUserRoleLst(InputType);
            return result;
        }

        public async Task<CommonResponce> AddUserRoleMapping(string UserID, string RoleMenuID, string RoleAssetID)
        {
            var result = await _dbHelper.AddUserRoleMapping(UserID, RoleMenuID, RoleAssetID);
            return result;
        }
        public async Task<IEnumerable<UserRoleMapping>> GetUserRoleMapping(string UserID)
        {
            var result = await _dbHelper.GetUserRoleMapping(UserID);
            return result;
        }

    }
}