using FCVT.Models;

namespace FCVT.Interfaces
{
    public interface IAuthen
    {
        Task<AuthModel> CheckUserCredential(string UserName,string IP);
    }
}
