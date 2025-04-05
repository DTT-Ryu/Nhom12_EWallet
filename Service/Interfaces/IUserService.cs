using Nhom12_EWallet.Models;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Service.Interfaces
{
    public interface IUserService
    {
        Task<TblUser>? Login(string phoneNumber, string password);
        void SetUserSession (HttpContext httpContext, TblUser user);

        Task Logout(HttpContext httpContext);

        Task<ResultService> Register(RegisterVM model);
    }
}
