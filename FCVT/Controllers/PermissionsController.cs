using FCVT.Interfaces;
using FCVT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Security;
using static System.Net.Mime.MediaTypeNames;

namespace FCVT.Controllers
{
    [Authorize]
    public class PermissionsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPermissionsManagement _permission;

        public PermissionsController(ILogger<HomeController> logger, IPermissionsManagement permission)
        {
            _logger = logger;
            _permission = permission;
        }

        #region User Managements
        public IActionResult Users()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var UserName = User.FindFirst("UserName")?.Value;
            var users = await _permission.GetUserLst(UserName);
            return Json(new { data = users });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserInsert model)
        {
            if (ModelState.IsValid)
            {
                var passwordHasher = new PasswordHasher<string>();
                string hashedPassword = passwordHasher.HashPassword(null, "NewPasswordHashed@01");
                var users = await _permission.AddUsers(model.FirstName, model.LastName, model.UserName, model.NIC, model.Email
                    , model.PhoneNo, model.Address, hashedPassword, "User");
                return Ok();
            }
            return BadRequest("Invalid data");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Invalid user.");

            var users = await _permission.DeleteUsers(id);
            if (users != null)
                return Ok();
            else
                return BadRequest("Failed to delete user.");
        }


        #endregion

        #region Role Management
        public IActionResult Roles()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var UserName = User.FindFirst("UserName")?.Value;
            var users = await _permission.GetRoleLst(UserName);
            return Json(new { data = users });
        }

        [HttpPost]
        public async Task<IActionResult> AddRoles(RoleInsert model)
        {
            if (ModelState.IsValid)
            {
                var users = await _permission.AddRole(model.RoleName, model.RoleType, model.Description);
                return Ok();
            }
            return BadRequest("Invalid data");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Invalid user.");

            var users = await _permission.DeleteRoles(id);
            if (users != null)
                return Ok();
            else
                return BadRequest("Failed to delete user.");
        }

        #endregion

        #region Role Permission
        public IActionResult RolePermission()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuAssetRolesLst(string RoleType)
        {
            var users = await _permission.GetAllMenuAssetRolesLst(RoleType);
            return Json(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuLst(string RoleId)
        {
            var menus = await _permission.GetAllMenusLst("Menu", RoleId);
            var treeData = menus.Select(m => new
            {
                id = m.Id.ToString(),
                parent = (m.PMenuID == null || m.PMenuID == "0") ? "#" : m.PMenuID.ToString(),
                text = m.MenuName,
                state = new { selected = m.IsAssigned }
            });
            return Json(treeData);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleMenu(string roleid, List<string> menuIds)
        {
            if (ModelState.IsValid)
            {
                var menuIdsString = string.Join(",", menuIds);
                var users = await _permission.AddRoleMenu(roleid, menuIdsString, "1");
                return Ok();
            }
            return BadRequest("Invalid data");
        }


        [HttpGet]
        public async Task<IActionResult> GetVehicleByRole(string RoleId)
        {
            var vehicles = await _permission.GetAllMenusLst("Assets", RoleId);
            var treeData = vehicles.Select(v => new
            {
                id = v.Id.ToString(),
                parent = v.PMenuID == null ? "#" : v.PMenuID.ToString(),
                text = v.MenuName,
                state = new { selected = v.IsAssigned }
            });
            return Json(treeData);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleAssets(string roleid, List<string> menuIds)
        {
            if (ModelState.IsValid)
            {
                var menuIdsString = string.Join(",", menuIds);
                var users = await _permission.AddRoleAssets(roleid, menuIdsString, "1");
                return Ok();
            }
            return BadRequest("Invalid data");
        }

        #endregion

        #region Role Assigning
        public IActionResult RoleAssigning()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoleLst(string InputType)
        {
            var Lst = await _permission.GetUserRoleLst(InputType);
            return Json(Lst);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRolesToUser(string userId, string menuRoleId, string assetRoleId)
        {
            if (ModelState.IsValid)
            {
                var users = await _permission.AddUserRoleMapping(userId, menuRoleId, assetRoleId);
                return Ok();
            }
            return BadRequest("Invalid data");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoleMapping(string userID)
        {
            var Lst = await _permission.GetUserRoleMapping(userID);
            return Json(Lst);
        }
        #endregion

        #region Asset Region Mapping
        public IActionResult AssetRegionMapping()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsset()
        {
            var result = await _permission.GetAssets();
            return Json(new { data = result });
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAssetRegionMapping(ModelAssetRegion InpModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var users = await _permission.UpdateAssetRegionMapping(
                        InpModel.Asset,
                        InpModel.Region,
                        InpModel.color,
                        InpModel.Comments
                    );
                    return Ok();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message); // Temp for debugging
                }
            }
            return BadRequest("Invalid data");
        }

        #endregion

    }
}
