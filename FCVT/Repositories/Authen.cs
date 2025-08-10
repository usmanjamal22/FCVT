using FCVT.DAL;
using FCVT.Interfaces;
using FCVT.Models;

namespace FCVT.Repositories
{
    public class Authen : IAuthen
    {
        private readonly DBHelper _dbHelper;

        public Authen(DBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<AuthModel> CheckUserCredential(string UserName, string IP)
        {
            var result = await _dbHelper.CheckUserCredential(UserName, IP);
            return result;
        }
    }
}
