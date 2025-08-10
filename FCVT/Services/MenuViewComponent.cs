using Microsoft.AspNetCore.Mvc;
using FCVT.Interfaces;

namespace FCVT.Services
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;

        public MenuViewComponent(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UserName)
        {
            var menuItems = await _menuService.GetMenuItemsAsync(UserName);
            return View(menuItems);
        }

    }
}
