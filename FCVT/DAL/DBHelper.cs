using Dapper;
using FCVT.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace FCVT.DAL
{
    public class DBHelper
    {
        private readonly IConfiguration _configuration;
        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Authentication
        public async Task<AuthModel> CheckUserCredential(string UserName, string IP)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserName", UserName, DbType.String);
                parameters.Add("@Source", "FCVTWebApp", DbType.String);
                parameters.Add("@IP", IP, DbType.String);

                return await connection.QueryFirstOrDefaultAsync<AuthModel>(
                    "UserLoginCheck_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        #endregion

        #region Menu        
        public async Task<string> IsLoginNumberIsSngplContact(string ContactNumber)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@ContactNumber", ContactNumber, DbType.String);

                var result = await connection.ExecuteScalarAsync<string>(
                "IsSngplContactNumber",
                parameters,
                commandType: CommandType.StoredProcedure
                );
                return result;
            }
        }

        public async Task<IEnumerable<PMenu>> GetMenu(string UserName)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserName", UserName, DbType.String);

                return await connection.QueryAsync<PMenu>(
                    "FCVT_GetMenuDataRight_SP"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                    );
            }
        }


        #endregion

        #region Permission

        #region User Management
        public async Task<IEnumerable<UserLst>> GetUserLst(string UserName)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserName", UserName, DbType.String);

                return await connection.QueryAsync<UserLst>(
                    "FCVT_GetUserLst_SP"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                    );
            }
        }

        public async Task<CommonResponce> AddUsers(string FirstName, string LastName, string UserName, string NIC, string Email
            , string PhoneNo, string Address, string PasswordHashed, string UserType)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", FirstName, DbType.String);
                parameters.Add("@LastName", LastName, DbType.String);
                parameters.Add("@UserName", UserName, DbType.String);
                parameters.Add("@NIC", NIC, DbType.String);
                parameters.Add("@Email", Email, DbType.String);
                parameters.Add("@PhoneNo", PhoneNo, DbType.String);
                parameters.Add("@Address", Address, DbType.String);
                parameters.Add("@PasswordHashed", PasswordHashed, DbType.String);
                parameters.Add("@UserType", UserType, DbType.String);

                return await connection.QueryFirstOrDefaultAsync<CommonResponce>(
                    "FCVT_AddUsers_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }



        public async Task<CommonResponce> DleteUsers(string ID)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ID", ID, DbType.String);

                return await connection.QueryFirstOrDefaultAsync<CommonResponce>(
                    "FCVT_UserDelete_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }


        #endregion

        #region Role Management
        public async Task<IEnumerable<RoleLst>> GetRoleLst(string UserName)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserName", UserName, DbType.String);

                return await connection.QueryAsync<RoleLst>(
                    "FCVT_GetRoleLst_SP"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                    );
            }
        }

        public async Task<CommonResponce> AddRole(string RoleName, string RoleType, string Description)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@RoleName", RoleName, DbType.String);
                parameters.Add("@RoleType", RoleType, DbType.String);
                parameters.Add("@Description", Description, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<CommonResponce>(
                    "FCVT_AddToles_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<CommonResponce> DeleteRoles(string ID)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@ID", ID, DbType.String);

                return await connection.QueryFirstOrDefaultAsync<CommonResponce>(
                    "FCVT_RolesDelete_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        #endregion

        #region Assign Role Permission
        public async Task<IEnumerable<MenuLst>> GetAllMenusLst(string InputType, string RoleId)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@RoleId", RoleId, DbType.String);

                if (InputType == "Menu")
                {
                    return await connection.QueryAsync<MenuLst>(
                        "FCVT_GetAllMenus_SP"
                        , parameters
                        , commandType: CommandType.StoredProcedure
                        );
                }
                else
                {
                    return await connection.QueryAsync<MenuLst>(
                   "FCVT_GetAllAssets_SP"
                   , parameters
                   , commandType: CommandType.StoredProcedure
                   );
                }
            }
        }

        public async Task<IEnumerable<RoleTypeLst>> GetAllMenuAssetRolesLst(string Roletype)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@Roletype", Roletype, DbType.String);

                return await connection.QueryAsync<RoleTypeLst>(
                    "FCVT_GetAllRoles_SP"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                    );
            }
        }


        public async Task<CommonResponce> AddRoleMenu(string RoleID, string MenuIds, string Loginid)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@RoleID", RoleID, DbType.String);
                parameters.Add("@MenuIds", MenuIds, DbType.String);
                parameters.Add("@Loginid", Loginid, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<CommonResponce>(
                    "FCVT_AddRoleMenus_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<CommonResponce> AddRoleAssets(string RoleID, string AssetIds, string Loginid)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@RoleID", RoleID, DbType.String);
                parameters.Add("@AssetIds", AssetIds, DbType.String);
                parameters.Add("@Loginid", Loginid, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<CommonResponce>(
                    "FCVT_AddRoleAssets_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        #endregion

        #region Role Assigning
        public async Task<IEnumerable<CommonSelect>> GetUserRoleLst(string InputType)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@InputType", InputType, DbType.String);

                return await connection.QueryAsync<CommonSelect>(
                    "FCVT_GetUserRoleLst_SP"
                    , parameters
                    , commandType: CommandType.StoredProcedure
                    );

            }
        }

        public async Task<CommonResponce> AddUserRoleMapping(string UserID, string RoleMenuID, string RoleAssetID)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID, DbType.String);
                parameters.Add("@RoleMenuID", RoleMenuID, DbType.String);
                parameters.Add("@RoleAssetID", RoleAssetID, DbType.String);
                return await connection.QueryFirstOrDefaultAsync<CommonResponce>(
                    "FCVT_AddUserRoleMapping_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<IEnumerable<UserRoleMapping>> GetUserRoleMapping(string UserID)
        {
            using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
            {
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", UserID, DbType.String);
                return await connection.QueryAsync<UserRoleMapping>(
                    "FCVT_GetUserRoleMapping_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }


        #endregion

        #endregion

        #region EV Battery Asset
        public async Task<IEnumerable<Common>> GETEVAssetLst(string Cellno)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration["ConnectionStrings:tpl"]))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@Cellno", Cellno, DbType.String);

                    return await connection.QueryAsync<Common>(
                        "TWAPP_GETEVAssetLst_SP",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                var StrError = ex.Message;
                return null;
            }
        }
        #endregion

        #region Asset EV Status
        public async Task<EVCanHexObject> GETEVCanValues(string Asset)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration["ConnectionStrings:teltonika"]))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@RegNo", Asset, DbType.String);

                    return await connection.QueryFirstOrDefaultAsync<EVCanHexObject>(
                    "EVBattery_CanValues_SP",
                    parameters,
                    commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                var StrError = ex.Message;
                return null;
            }
        }
        #endregion


    }
}
