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

        Task<List<TblUser>> GetAllUsersWithRoleAsync();

        Task<TblUser?> GetUserWithRoleById(int id);
        Task<List<TblRole>> GetAllRolesAsync();

        Task<bool> UpdateUserRole (int userId, byte roleId);
        Task<bool> UpdateUserStatus(int userId);

        //Task<TblUser> UpdateUserInfor(int id, UserManagementVM model);
    }
}
