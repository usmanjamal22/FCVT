using Microsoft.AspNetCore.Mvc;
using FCVT.DAL;
using FCVT.Interfaces;
using FCVT.Models;

namespace FCVT.Repositories
{
    public class MenuService : IMenuService
    {
        private readonly DBHelper _dBHelper;
        public MenuService(DBHelper dBHelper)
        {
            _dBHelper = dBHelper;
        }
        
        public async Task<List<IsMenu>> GetMenuItemsAsync(string UserName)
        {
            string SngplUser = string.Empty;
            string ReportDays = string.Empty;
            string HasGenSet = string.Empty;

            var LstMenuP = await _dBHelper.GetMenu(UserName);
            int Count = 0;
            if (LstMenuP != null && LstMenuP.Count() > 0)
            {
                HasGenSet = LstMenuP.FirstOrDefault()?.HasGenset ?? string.Empty;
                var lstMenu = new List<IsMenu>();
                foreach (var item in LstMenuP)
                {
                    var ObjMenu = new IsMenu();
                    //if (!string.Equals(item.PMenuID.ToString(), string.Empty, StringComparison.Ordinal))
                    //    continue;

                    ObjMenu.pid = item.Pid;
                    ObjMenu.pname = item.DisplayName;
                    ObjMenu.pcontroller = item.ControllerName;
                    ObjMenu.paction = item.ActionName;
                    ObjMenu.IsSngplUser = SngplUser;
                    ObjMenu.ReportDays = item.ReportDays;
                    ObjMenu.HasGenSet = HasGenSet;

                    var LstChildMenu = LstMenuP.Where(r => r.PMenuID == item.Pid).ToList();
                    if (LstChildMenu != null && LstChildMenu.Count() > 0)
                    {
                        var lstChild = new List<ChildMenu>();
                        foreach (var itemChildMenu in LstChildMenu)
                        {
                            ChildMenu ObjChild = new ChildMenu()
                            {
                                cid = itemChildMenu.Pid.ToString(),
                                cname = itemChildMenu.DisplayName,
                                controller = itemChildMenu.ControllerName,
                                action = itemChildMenu.ActionName
                            };
                            lstChild.Add(ObjChild);
                        }
                        ObjMenu.Haschild = lstChild;
                    }
                    else
                    {
                        ObjMenu.Haschild = null;
                    }

                    lstMenu.Insert(Count, ObjMenu);
                    Count++;
                }
                return lstMenu;
            }
            return new List<IsMenu>();
        }


    }
}
