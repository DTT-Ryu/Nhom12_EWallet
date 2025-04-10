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

        Task<IEnumerable<UserManagementVM>> GetAllUsers();

        Task<UserManagementVM> GetUserByID(int id);
        Task<List<TblRole>> GetListRole();
        Task<bool> UpdateUserRole(int userId, byte roleId);

        Task<bool> UpdateUserStatus(int userId);

        Task<bool> GetUserByEmail(string email);

        Task<UserManagementVM> UpdateUserInfor(int id, UserManagementVM model);
    }
}
