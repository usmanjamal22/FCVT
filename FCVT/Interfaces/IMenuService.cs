using Microsoft.AspNetCore.Mvc;
using FCVT.Models;

namespace FCVT.Interfaces
{
    public interface IMenuService
    {
        Task<List<IsMenu>> GetMenuItemsAsync(string UserName);
    }
}
