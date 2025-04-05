using Nhom12_EWallet.Models;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface IUserRepository : IRepository<TblUser>
    {
        Task<TblUser>? GetUserByPhoneNumber(string pNumber);
        Task<TblUser>? GetUserByCCCD(string sCCCD);

        Task<TblUser>? GetUserByEmail(string sEmail);
        //void AddUser(TblUser user);

        Task<List<UserManagementVM>> GetAllUsersWithRoleAsync();
    }
}
